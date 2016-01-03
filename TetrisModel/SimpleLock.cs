using System;
using System.Threading;

namespace TetrisModel
{

  public class SimpleLock
  {
    private int waiters = 0;
    private readonly AutoResetEvent waiterLock = new AutoResetEvent(false);

    public void Enter()
    {
      if (Interlocked.Increment(ref waiters) == 1) return; 
      waiterLock.WaitOne(); 
    }

    public void Exit()
    {
      if (Interlocked.Decrement(ref waiters) == 0) return;
      waiterLock.Set(); 
    }
  }
  
}
