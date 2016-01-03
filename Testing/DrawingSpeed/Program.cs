using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using TetrisModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Diagnostics;
using System.Linq.Expressions;

namespace DrawingSpeed
{
  class Rotor : IKeyboardListener,IHandler
  {
    private int speed;
    private readonly ManualResetEvent isActive = new ManualResetEvent(true);
    private Thread rotor;
    public Rotor(IGameUnit unit, double delta = Math.PI / 2, int speed = 1000)
    {
      this.speed = speed;

      rotor = new Thread(() =>
      {
        while (true) {
          isActive.WaitOne();
          unit.Rotate(delta);
          unit.Move(0, -1);
          Thread.Sleep(this.speed);
        }
      }){ IsBackground = true };
      rotor.Start();

      ConsoleKeyboard.Get.Add(this);
//      Registry<KeyboardEvents>.GetInstanceOf<KeyboardEvents>().Add(this);
    }

    public void Update(ConsoleKey key)
    {
      if (key == ConsoleKey.Add && speed > 100) speed -= 100;
      else if (key == ConsoleKey.Subtract) speed += 100;
    }

    public void Start()
    {
      //Task.Factory.StartNew(() => Registry<KeyboardEvents>.GetInstanceOf<KeyboardEvents>().Add(this));
      Task.Factory.StartNew(() => ConsoleKeyboard.Get.Add(this));
      isActive.Set();
    }

    public void Stop()
    {
      isActive.Reset();
//      Task.Factory.StartNew(() => Registry<KeyboardEvents>.GetInstanceOf<KeyboardEvents>().Remove(this));
      Task.Factory.StartNew(() => ConsoleKeyboard.Get.Remove(this));
    }
  }


  interface IHandler
  {
    void Start();
    void Stop();
  }


  class Shaker:IHandler
  {
    private readonly ManualResetEvent isActive = new ManualResetEvent(true);
    private Thread shaker;
    public Shaker(IGameUnit unit, int speed = 1000)
    {
      shaker = new Thread(() =>
      {
        var rnd = new Random(unit.GetHashCode());
        var iter = 0;
        while (true) {
          isActive.WaitOne();
          //unit.Move((int) Math.Pow(-1, ++iter), 0);
          unit.Rotate(Math.PI / 120);
          //Thread.Sleep(1 + rnd.Next(speed));
          Thread.Sleep(100);
        }
      }){ IsBackground = true };
      shaker.Start();
    }

    public void Start()
    {
      isActive.Set();
    }

    public void Stop()
    {
      isActive.Reset();
    }
  }


  class Falling:IHandler
  {
    private readonly ManualResetEvent isActive = new ManualResetEvent(true);
    private Thread shaker;
    private static int x = 0;
    private static int y = 0;
    private static SimpleLock locker = new SimpleLock();
    public Falling(IGameUnit unit, int speed = 1000)
    {
      if (x == 0 && y == 0) {
        x = -Console.WindowWidth / 2;
        y = Console.WindowHeight / 2 - 10;
      }
      shaker = new Thread(() =>
      {
        var rnd = new Random(unit.GetHashCode());
        while (true) {
//          locker.Enter();
          var x = -Console.WindowWidth / 2 + rnd.Next(Console.WindowWidth);
          var y = -Console.WindowHeight / 2 + rnd.Next(Console.WindowHeight);
//          x += 4;
//          if (x > Console.WindowWidth / 2) {
//            x = -Console.WindowWidth / 2;
//          }
          unit.Position(x, y, 0);
//          unit.Draw();
//          locker.Exit();
          var end = 1000 + rnd.NextDouble() * 1000;
          var time = 0;
          var d = 30;
          while (time < end && unit.Visible) {
            isActive.WaitOne();
            //unit.Move((int) Math.Pow(-1, ++iter), 0);
            //unit.Rotate(Math.PI / 120);
            unit.Move(0, -0.2);
            //Thread.Sleep(1 + rnd.Next(speed));
            Thread.Sleep(d);
            time += d;
          }
          if (!unit.Visible) {
            int foo = 1;
          }
        }
        unit.Enable = false;
      }){ IsBackground = true };
      shaker.Start();
    }

    public void Start()
    {
      isActive.Set();
    }

    public void Stop()
    {
      isActive.Reset();
    }
  }


  enum GameEvent
  {
    IntroStart,
    IntroStop,
    IntroToggleBackground,
    IntroToggleTrees
  }


  interface IEventListener
  {
    void Update(GameEvent e);
  }


  class Intro:CompositeUnit,IEventListener
  {
    public event InvalidateEventHandler InvalidateEvent;

    public Intro(IRenderEngine engine, ITetrisGame game)
    {
      this.engine = engine;
      game.Add(this);
    }

    public void Update(GameEvent e)
    {
      if (e == GameEvent.IntroStart) {
        foreach (var handler in handlers) handler.Start();
        if (engine != null) engine.Enable = true;
      }
      else if (e == GameEvent.IntroStop) {
        foreach (var handler in handlers) handler.Stop();
        if (engine != null) engine.Enable = false;
      }
      else if (e == GameEvent.IntroToggleBackground) {
        background.Enable = !background.Enable;
      }
      else if (e == GameEvent.IntroToggleTrees) {
        trees.Enable = !trees.Enable;
        if (trees.Enable)
          treesHandler.Start();
        else
          treesHandler.Stop();
      }
    }

    IGameUnit background;
    public void SetBackground(IGameUnit unit)
    {
      background = unit;
      Add(unit, null);
    }

    IGameUnit trees;
    IHandler treesHandler;
    public void SetTrees(IGameUnit unit, IHandler handler)
    {
      trees = unit;
      treesHandler = handler;
      Add(unit, handler);
    }

    public void AddAnimation(IGameUnit unit, IHandler handler = null)
    {
      Add(unit, handler);
    }

    private void Add(IGameUnit unit, IHandler handler)
    {
      if (handler != null)
        handlers.Add(handler);
      AddUnit(unit);
      unit.InvalidateEvent += Invalidate;
      if (engine != null) {
        engine.Add(unit);
        engine.Update();
      }
    }

    public void Invalidate()
    {
      if (InvalidateEvent != null) InvalidateEvent();
    }

    private List<IHandler> handlers = new List<IHandler>();
    private IRenderEngine engine;
  }


  class IntroBuilder
  {
    public virtual void BuildIntro(IRenderEngine engine, ITetrisGame game)
    {
    }
    public virtual void BuildBackground()
    {
    }
    public virtual void BuildMenu()
    {
    }
    public virtual void BuildAnimation()
    {
    }
    public virtual void BuildTrees()
    {
    }

    public virtual Intro GetIntro()
    {
      return null;
    }

    protected IntroBuilder()
    {
    }
  }


  class FancyIntroBuilder : IntroBuilder
  {
    Random rnd = new Random();
    public override void BuildIntro(IRenderEngine engine, ITetrisGame game)
    {
      intro = new Intro(engine, game);
    }

    public override void BuildBackground()
    {
      var H = Console.WindowHeight;
      var W = Console.WindowWidth;

      var background = new CompositeUnit();

      var stars = new Sprite(() => new FastConsoleDevice(new [] { "." }), () =>
      {
        var matrix = new ushort[H, W];
        for (var i = 0; i < H; i++) for (var j = 0; j < W; j++) matrix[i, j] = rnd.NextDouble() < 0.91 ? (ushort) 0 : (ushort) 1;
        return new Pattern(matrix);
      }, -Console.WindowWidth / 2 + 1, Console.WindowHeight / 2 - 1, Color.White);
      background.AddUnit(stars);

      intro.SetBackground(background);
    }

    public override void BuildTrees()
    {
      var H = 20;
      var W = 30;
      var trees = new Sprite(() => new FastConsoleDevice(new [] { 
        @"  \  ",
        @" /*\ ", 
        @"//|\\",
        @"  |  ",
        @" ___ "
      }), () =>
      {
        var matrix = new ushort[H, W];
        for (var i = 0; i < H; i++) for (var j = 0; j < W; j++) matrix[i, j] = rnd.NextDouble() < 0.85 ? (ushort) 0 : (ushort) 1;
        return new Pattern(matrix);
      }, -Console.WindowWidth / 2 + 10, Console.WindowHeight / 2 - 3, Color.DarkGreen);
      intro.SetTrees(trees, new Shaker(trees));
    }

    public override void BuildAnimation()
    {
      for (var i = 0; i < 150; i++) {
        var snowflake = new Sprite(Registry<IGraphicsFactory>.GetInstanceOf<ConsoleGraphicsFactory>().CreateSnowFlake);
//        var foo = String.Format("{0:D2}", i);
//        var snowflake = new Sprite(() => new FastConsoleDevice(new []{ foo }));
        intro.AddAnimation(snowflake, new Falling(snowflake));
      }

      var falling_piece = new Sprite(() => new FastConsoleDevice(new []{ "[]" }), Registry<PatternFactory>.GetInstanceOf<PyramidePatternFactory>(), 0, Console.WindowHeight / 2, Color.Green);
      intro.AddAnimation(falling_piece, new Rotor(falling_piece));
    }

    public override Intro GetIntro()
    {
      return intro;
    }

    public FancyIntroBuilder()
    {
      intro = null;
    }

    private Intro intro;
  }


  interface ITetrisGame
  {
    event InvalidateEventHandler InvalidateEvent;
    void Add(IEventListener listener);
    void Remove(IEventListener listener);
  }


  class TetrisGame:IKeyboardListener,ITetrisGame
  {
    public event InvalidateEventHandler InvalidateEvent;

    private ManualResetEvent isActive = new ManualResetEvent(false);

    private Color background = Color.Blue;

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

    public Intro CreateIntro(IntroBuilder builder)
    {
      builder.BuildIntro(new ConsoleRenderEngine(this), this);
      builder.BuildBackground();
      builder.BuildTrees();
      builder.BuildAnimation();
      builder.BuildMenu();
      return builder.GetIntro();
    }

    public TetrisGame()
    {
      Console.CursorVisible = false;
      Sprite.refDot = false;

      //CreateEngine();
      var mainMenu = CreateIntro(new FancyIntroBuilder());
      mainMenu.InvalidateEvent += Invalidate;

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

    private void Invalidate()
    {
      if (InvalidateEvent != null) InvalidateEvent();
    }
  }


  interface IRenderEngine
  {
    bool Enable { get; set; }
    void Add(IGameUnit obj);
    void Remove(IGameUnit obj);
    void Update();
  }


  class ConsoleRenderEngine : IRenderEngine, IKeyboardListener
  {
    public bool Enable { get; set; }

    private ConsoleKey key;
    public void Update(ConsoleKey key)
    {
      this.key = key;
      if (key == ConsoleKey.I) toggleShowInfo = !toggleShowInfo;
      Update();
    }

    public static int Count;

    private bool toggleShowInfo = true;

    ManualResetEvent draw = new ManualResetEvent(false);
    List<IGameUnit> objects = new List<IGameUnit>();
    readonly static SimpleLock simple = new SimpleLock();

    private Thread render;
    public ConsoleRenderEngine(ITetrisGame game)
    {
      Interlocked.Increment(ref Count);
      game.InvalidateEvent += Update;
      render = new Thread(Render){ IsBackground = true, Name = Count.ToString() };
      render.Start();
      Enable = true;
      //Registry<KeyboardEvents>.GetInstanceOf<KeyboardEvents>().Add(this);
      ConsoleKeyboard.Get.Add(this);
    }

    private bool run = true;
    public void Stop(ITetrisGame game)
    {
      game.InvalidateEvent -= Update;
      run = false;
    }

    public void Add(IGameUnit obj)
    {
      simple.Enter();
      objects.Add(obj);
      simple.Exit();
    }

    public void Remove(IGameUnit obj)
    {
      simple.Enter();
      objects.Remove(obj);
      simple.Exit();
    }


    public void Update()
    {
      draw.Set();
    }

    private void Render()
    {
      while (run) {
        draw.WaitOne();
        if (Enable) {
          simple.Enter();
          ClearDevice();
          for (var i = 0; i < objects.Count; i++) objects[i].Draw();
          //foreach (var obj in objects) obj.Draw();
          if (toggleShowInfo)
            ShowInfo();
          simple.Exit();
        }
        Thread.Sleep(50);
        draw.Reset();
      }
    }

    private void ShowInfo()
    {
      Console.SetCursorPosition(0, int.Parse(Thread.CurrentThread.Name) - 1);
      Console.ForegroundColor = ConsoleColor.White;
      Console.BackgroundColor = ConsoleColor.Red;
      Console.Write("Total drawing objects: {0}, Key {1} pressed", objects.Count, key);
    }

    private Color background = Color.Blue;
    private void ClearDevice()
    {
      var b = ConsoleHelpers.Convert(background);
      Console.BackgroundColor = b;
      ConsoleHelpers.FillRect(b);
    }
  }


  class MainClass
  {
    
    public static void Main(string[] args)
    {
      // coordinates transform выделить в отдельный singleton (TransformPhysicalToDeviceCoordinates, Scale - цена деления, ... ScreenWidth, ScreeHeight)

      // новый проект drawing speed - 
      // добавить два класса от fast console implementation 
      // с continue и с посимвольной отрисовкой. 

      // для main взять последний код с потоками! - OriginalTetris Program.cs

      var game = new TetrisGame();
      game.Run();

      Console.SetCursorPosition(0, 0);
    }
  }
}
