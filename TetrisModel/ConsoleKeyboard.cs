using System;
using System.Collections.Generic;
using System.Threading;

namespace TetrisModel
{
  public interface IKeyboardListener
  {
    void Update(ConsoleKey key);
  }


  public class ConsoleKeyboard
  {
    private bool stop = false;
    private readonly List<IKeyboardListener> subscribers = new List<IKeyboardListener>();
    readonly static SimpleLock simple = new SimpleLock();

    public static ConsoleKeyboard Get { get; }

    static ConsoleKeyboard()
    {
      Get = new ConsoleKeyboard();
    }

    private ConsoleKeyboard()
    {
      new Thread(delegate()
      {
        while (!stop) Fire(Console.ReadKey(true).Key);
      }){ IsBackground = true }.Start();
    }

    public void Stop()
    {
      stop = true;
    }

    public void Add(IKeyboardListener listener)
    {
      simple.Enter();
      subscribers.Add(listener);
      simple.Exit();
    }

    public void Remove(IKeyboardListener listener)
    {
      simple.Enter();
      subscribers.Remove(listener);
      simple.Exit();
    }

    private void Fire(ConsoleKey key)
    {
      simple.Enter();
      foreach (var listener in subscribers) listener.Update(key);
      simple.Exit();
    }
  }
  
}
