using System;
using System.Collections.Generic;
using System.Threading;
using TetrisModel;

namespace MainMenu
{
  public class MenuTest:IKeyboardListener
  {
    private readonly ManualResetEvent isActive = new ManualResetEvent(false);

    public void Update(ConsoleKey key)
    {
      if (key == ConsoleKey.Escape)
        isActive.Set();
    }

    private SimpleLock subscribersLock = new SimpleLock();
    private List<IEventListener> subscribers = new List<IEventListener>();
    public void Add(IEventListener listener)
    {
      subscribersLock.Enter();
      subscribers.Add(listener);
      subscribersLock.Exit();
    }

    public void Remove(IEventListener listener)
    {
      subscribersLock.Enter();
      subscribers.Remove(listener);
      subscribersLock.Exit();
    }

    private void FireEvent(GameEvent e)
    {
      subscribersLock.Enter();
      foreach (var listener in subscribers) {
        listener.Update(e);
      }
      subscribersLock.Exit();
    }

    public IntroScene CreateMenu(MainMenuBuilder builder)
    {
      builder.BuildIntro();
      builder.BuildMenu();
      return builder.GetIntro();
    }

    public MenuTest()
    {
      Console.CursorVisible = false;
      Sprite.refDot = true;

      Add(CreateMenu(new MainMenuBuilder(new IntroFactory())));
      FireEvent(GameEvent.IntroStart);

      ConsoleKeyboard.Get.Add(this);
    }

    public void Run()
    {
      isActive.WaitOne();
    }
  }
}
