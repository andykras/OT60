using System;
using TetrisModel;
using System.Threading;
using System.Collections.Generic;

namespace DrawingSpeed
{
  public class DrawSceneTest:IKeyboardListener,IScene
  {
    public event InvalidateEventHandler InvalidateEvent;

    private ManualResetEvent isActive = new ManualResetEvent(false);

    private Color background = Color.Blue;
    public Color Background { get { return background; } }

    private bool anim = true;
    public void Update(ConsoleKey key)
    {
      if (key == ConsoleKey.Escape)
        isActive.Set();
      else if (key == ConsoleKey.Spacebar) {
        anim = !anim;
        FireEvent(anim ? GameEvent.IntroStart : GameEvent.IntroStop);
      }
      else if (key == ConsoleKey.B)
        FireEvent(GameEvent.IntroToggleBackground);
      else if (key == ConsoleKey.T)
        FireEvent(GameEvent.IntroToggleTrees);
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

    public IntroScene CreateIntro(IntroBuilder builder)
    {
      builder.BuildIntro(new ConsoleRenderEngine(this), this);
      builder.BuildBackground();
      builder.BuildTrees();
      builder.BuildAnimation();
      builder.BuildMenu();
      return builder.GetIntro();
    }

    public TetrisScene CreateMainScene(TetrisBuilder builder)
    {
      builder.BuildTetris(new TetrisFactory());
      return builder.GetTetris();
    }

    public DrawSceneTest()
    {
      Console.CursorVisible = false;
      Sprite.refDot = false;

      var mainMenu = CreateIntro(new FancyIntroBuilder());
      mainMenu.InvalidateEvent += Invalidate;

      ConsoleKeyboard.Get.Add(this);
    }

    public void Run()
    {
      isActive.WaitOne();
    }

    private void Invalidate()
    {
      if (InvalidateEvent != null) InvalidateEvent();
    }
  }
  
}
