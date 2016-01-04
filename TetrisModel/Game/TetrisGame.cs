using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace TetrisModel
{
  public class TetrisGame:IKeyboardListener
  {
    //    public event InvalidateEventHandler InvalidateEvent;

    private ManualResetEvent isActive = new ManualResetEvent(false);

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
      builder.BuildIntro(new IntroFactory());
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


    public TetrisGame()
    {
      Console.CursorVisible = false;
      Sprite.refDot = false;

      //CreateEngine();
      var mainMenu = CreateIntro(new FancyIntroBuilder());
      //mainMenu.InvalidateEvent += Invalidate;
      Add(mainMenu);
      FireEvent(GameEvent.IntroStart);

      var scene = CreateMainScene(new SimpleTetrisBuilder());

//      Registry<KeyboardEvents>.GetInstanceOf<KeyboardEvents>().Add(this);
      ConsoleKeyboard.Get.Add(this);
    }

    public void Run()
    {
      isActive.WaitOne();
    }

    //    private Rotor rotor;
    //    private ConsoleRenderEngine engine;
    //    private IGameUnit foo;
    //    readonly static SimpleLock simple = new SimpleLock();
    //    private bool autoUpdate = false;
    //    private void CreateEngine()
    //    {
    //      simple.Enter();
    //      if (rotor != null) rotor.Stop();
    //      if (foo != null) foo.InvalidateEvent -= Invalidate;
    //      //if (engine != null) engine.Stop(this);
    //      foo = new Sprite(() => new FastConsoleDevice(new []{ "[]" }), Registry<PatternFactory>.GetInstanceOf<PyramidePatternFactory>(), 0, Console.WindowHeight / 2, Color.Green);
    //      foo.InvalidateEvent += Invalidate;
    //      engine = new ConsoleRenderEngine(this);
    //
    //
    //      var rnd = new Random();
    //      var H = Console.WindowHeight;
    //      var W = Console.WindowWidth;
    //
    //
    //      var builder = new FancyIntroBuilder();
    //      CreateIntro(builder);
    //      engine.Add(builder.GetIntro());
    //
    ////      var stars = new Sprite(() => new ConsoleDevice(new [] { "." }),
    ////                             () =>
    ////      {
    ////        var matrix = new ushort[H, W];
    ////        for (var i = 0; i < H; i++) for (var j = 0; j < W; j++) matrix[i, j] = rnd.NextDouble() < 0.91 ? (ushort) 0 : (ushort) 1;
    ////        return new Pattern(matrix);
    ////      }, -Console.WindowWidth / 2 + 1, Console.WindowHeight / 2 - 1, Color.White);
    ////      engine.Add(stars);
    //      //engine.Add(trees);
    //
    ////      var trees = new CompositeUnit();
    ////      W = Console.WindowWidth;
    ////      H = Console.WindowHeight;
    ////      for (var i = 0; i < 150; i++) {
    ////        var x = -W / 2 + rnd.Next(W);
    ////        var y = -H / 2 + rnd.Next(H);
    ////        var tree = new Sprite(() => new FastConsoleDevice(new [] {
    ////          @"  \  ",
    ////          @" /*\ ",
    ////          @"//|\\",
    ////          @"  |  ",
    ////          @" ___ "
    ////        }), x, y, Color.DarkGreen);
    ////        //tree.InvalidateEvent += Invalidate;
    ////        trees.AddUnit(tree);
    ////      }
    //      H = 20;
    //      W = 30;
    //      var trees = new Sprite(() => new FastConsoleDevice(new [] {
    //        @"  \  ",
    //        @" /*\ ",
    //        @"//|\\",
    //        @"  |  ",
    //        @" ___ "
    //      }), () =>
    //      {
    //        var matrix = new ushort[H, W];
    //        for (var i = 0; i < H; i++) for (var j = 0; j < W; j++) matrix[i, j] = rnd.NextDouble() < 0.85 ? (ushort) 0 : (ushort) 1;
    //        return new Pattern(matrix);
    //      }, -Console.WindowWidth / 2 + 10, Console.WindowHeight / 2 - 3, Color.DarkGreen);
    //
    //      trees.InvalidateEvent += Invalidate;
    //      engine.Add(trees);
    //
    //      var shaker = new Shaker(trees);
    //
    //      engine.Add(foo);
    //      rotor = new Rotor(foo);
    //
    //      simple.Exit();
    //    }

    //    private void Invalidate()
    //    {
    //      if (InvalidateEvent != null) InvalidateEvent();
    //    }
  }
}
