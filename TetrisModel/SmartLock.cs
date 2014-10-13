using System;
using System.Threading;

namespace TetrisModel
{
  /// <summary>
  /// Smart lock written by myself
  /// It can be used when you need to block some piece of code with exclusive access
  /// </summary>
  public class SmartLock
  {
    private int nonblocking = 0;
    private int blocking = 0;

    // false - you shall not pass, true - all pass
    private readonly ManualResetEvent nonblockers = new ManualResetEvent(true);

    // false - you shall not pass, true - first pass
    private readonly AutoResetEvent blockers = new AutoResetEvent(false);

    /// <summary>
    /// All threads that does not require critical section are starting from here
    /// </summary>
    public void In()
    {
      nonblockers.WaitOne();
      Interlocked.Increment(ref nonblocking); // count if nonblocking thread has been passed
    }

    /// <summary>
    /// All threads that does not require critical section are exiting from here
    /// </summary>
    public void Out()
    {
      if (0 == Interlocked.Decrement(ref nonblocking) && 0 != Interlocked.CompareExchange(ref blocking, 0, 0)) blockers.Set(); // let them (blocking threads) go
    }

    /// <summary>
    /// Exclusive threads are entering here
    /// </summary>
    public void Enter()
    {
      if (Interlocked.Increment(ref blocking) == 1) nonblockers.Reset(); // we shall stop all new comers
      else blockers.WaitOne(); // and stop itself if there is already exist one blocking thread
      if (0 != Interlocked.CompareExchange(ref nonblocking, 0, 0)) blockers.WaitOne(); // we can't go if there is anyone else thread
    }

    /// <summary>
    /// Exclusive threads are exiting here
    /// </summary>
    public void Exit()
    {
      if (0 != Interlocked.Decrement(ref blocking)) blockers.Set();
      else nonblockers.Set();
    }
  }
}

