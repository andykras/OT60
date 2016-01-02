using System;
using System.Threading;
using TetrisModel;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace DrawingSpeed
{
  interface IKeyboardListener
  {
    void Update(ConsoleKey key);
  }


  class KeyboardEvents
  {
    private readonly List<IKeyboardListener> listeners = new List<IKeyboardListener>();
    readonly static SimpleLock simple = new SimpleLock();

    static KeyboardEvents()
    {
      new KeyboardEvents();
    }

    private KeyboardEvents()
    {
      Registry<KeyboardEvents>.Register(this);
      new Thread(() =>
      {
        while (true) {
          Fire(Console.ReadKey(true).Key);
        }

      }){ IsBackground = true }.Start();
    }

    public void Add(IKeyboardListener listener)
    {
      simple.Enter();
      listeners.Add(listener);
      simple.Exit();
    }

    public void Remove(IKeyboardListener listener)
    {
      simple.Enter();
      listeners.Remove(listener);
      simple.Exit();
    }

    private void Fire(ConsoleKey key)
    {
      simple.Enter();
      foreach (var listener in listeners) {
        listener.Update(key);
      }
      simple.Exit();
    }
  }


  class Rotor : IKeyboardListener
  {
    private int speed;

    private Thread rotor;
    public Rotor(IGameUnit unit, double delta = Math.PI / 2, int speed = 1000)
    {
      this.speed = speed;

      rotor = new Thread(() =>
      {
        while (true) {
          unit.Rotate(delta);
          unit.Move(0, -1);
          Thread.Sleep(this.speed);
        }
      }){ IsBackground = true };
      rotor.Start();

      Registry<KeyboardEvents>.GetInstanceOf<KeyboardEvents>().Add(this);
    }

    public void Update(ConsoleKey key)
    {
      if (key == ConsoleKey.Add && speed > 100) speed -= 100;
      else if (key == ConsoleKey.Subtract) speed += 100;
    }

    public void Stop()
    {
      rotor.Abort();
      Registry<KeyboardEvents>.GetInstanceOf<KeyboardEvents>().Remove(this);
    }
  }


  class Shaker
  {
    public Shaker(IGameUnit unit, int speed = 1000)
    {
      new Thread(() =>
      {
        var rnd = new Random(unit.GetHashCode());
        var iter = 0;
        while (true) {
          //unit.Move((int) Math.Pow(-1, ++iter), 0);
          unit.Rotate(Math.PI / 120);
          //Thread.Sleep(1 + rnd.Next(speed));
          Thread.Sleep(100);
        }
      }){ IsBackground = true }.Start();
    }
  }


  class TetrisGame:IKeyboardListener
  {
    public event InvalidateEventHandler InvalidateEvent;

    private ManualResetEvent isActive = new ManualResetEvent(false);

    private Color background = Color.Blue;

    public void Update(ConsoleKey key)
    {
      Console.SetCursorPosition(0, 0);
      Console.WriteLine("                                                   ");
      Console.SetCursorPosition(0, 0);
      Console.WriteLine("Key {0} pressed", key);

      if (key == ConsoleKey.Escape)
        isActive.Set();

      if (key == ConsoleKey.Spacebar) {
        autoUpdate = !autoUpdate;
        Task.Factory.StartNew(CreateScene);
      }
    }

    public TetrisGame()
    {
      Console.CursorVisible = false;
      Sprite.refDot = true;

      CreateScene();

      Registry<KeyboardEvents>.GetInstanceOf<KeyboardEvents>().Add(this);
    }

    public void Run()
    {
      isActive.WaitOne();
    }

    private Rotor rotor;
    private Scene scene;
    private IGameUnit foo;
    readonly static SimpleLock simple = new SimpleLock();
    private bool autoUpdate = false;
    private void CreateScene()
    {
      simple.Enter();
      if (rotor != null) rotor.Stop();
      if (foo != null) foo.InvalidateEvent -= Invalidate;
      if (scene != null) scene.Stop(this);
      foo = new Sprite(() => new FastConsoleDevice(new []{ "[]" }), Registry<PatternFactory>.GetInstanceOf<PyramidePatternFactory>(), 0, Console.WindowHeight / 2, Color.Green);
      foo.InvalidateEvent += Invalidate;
      scene = new Scene(this);


      scene.Add(new Background(background, () => new ConsoleBackground()));

      var rnd = new Random();
      var H = Console.WindowHeight;
      var W = Console.WindowWidth;

      var stars = new Sprite(() => new ConsoleDevice(new [] { "." }), 
                             () =>
      {
        var matrix = new ushort[H, W];
        for (var i = 0; i < H; i++) for (var j = 0; j < W; j++) matrix[i, j] = rnd.NextDouble() < 0.91 ? (ushort) 0 : (ushort) 1;
        return new Pattern(matrix);
      }, -Console.WindowWidth / 2 + 1, Console.WindowHeight / 2 - 1, Color.White);
      scene.Add(stars);
      //scene.Add(trees);

//      var trees = new CompositeUnit();
//      W = Console.WindowWidth;
//      H = Console.WindowHeight;
//      for (var i = 0; i < 150; i++) {
//        var x = -W / 2 + rnd.Next(W);
//        var y = -H / 2 + rnd.Next(H);
//        var tree = new Sprite(() => new FastConsoleDevice(new [] { 
//          @"  \  ",
//          @" /*\ ", 
//          @"//|\\",
//          @"  |  ",
//          @" ___ "
//        }), x, y, Color.DarkGreen);
//        //tree.InvalidateEvent += Invalidate;
//        trees.AddUnit(tree);
//      }
      H = 20;
      W = 30;
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

      trees.InvalidateEvent += Invalidate;
      scene.Add(trees);

      var shaker = new Shaker(trees);

      scene.Add(foo);
      rotor = new Rotor(foo);

      simple.Exit();
    }

    private void Invalidate()
    {
      if (InvalidateEvent != null) InvalidateEvent();
    }
  }


  class ConsoleBackground : IDevice
  {
    public void Draw(double x, double y, double angle, Color color)
    {
      var b = ConsoleHelpers.Convert(color);
      Console.BackgroundColor = b;
      ConsoleHelpers.FillRect(0, Height, 0, Width, b);
    }
    public int Width { get { return Console.WindowWidth; } }
    public int Height { get { return Console.WindowHeight; } }
  }


  class Background : IGameUnit
  {
    public event InvalidateEventHandler InvalidateEvent;

    private Color background;
    private IDevice device;

    public Background(Color background, Func<IDevice> deviceCreator)
    {
      this.background = background;
      this.device = deviceCreator();
    }

    public void Draw()
    {
      device.Draw(0, 0, 0, background);
    }

    public void Position(double x, double y, double angle)
    {
      throw new NotImplementedException();
    }

    public void Move(double dx, double dy)
    {
      throw new NotImplementedException();
    }

    public void SetColor(Color color)
    {
      throw new NotImplementedException();
    }

    public void Rotate(double da)
    {
      throw new NotImplementedException();
    }

    public double Angle { get { throw new NotImplementedException(); } }
  }


  class Scene : IKeyboardListener
  {
    public void Update(ConsoleKey key)
    {
      if (key == ConsoleKey.S) toggleShowTotal = !toggleShowTotal;
      Update();
    }

    public static int Count;

    private bool toggleShowTotal = true;

    ManualResetEvent draw = new ManualResetEvent(false);
    List<IGameUnit> objects = new List<IGameUnit>();
    readonly static SimpleLock simple = new SimpleLock();

    private Thread render;
    public Scene(TetrisGame game)
    {
      Interlocked.Increment(ref Count);
      game.InvalidateEvent += Update;
      render = new Thread(Render){ IsBackground = true, Name = Count.ToString() };
      render.Start();
      Registry<KeyboardEvents>.GetInstanceOf<KeyboardEvents>().Add(this);
    }

    public void Stop(TetrisGame game)
    {
      game.InvalidateEvent -= Update;
      render.Abort();
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
      while (true) {
        draw.WaitOne();
        simple.Enter();
        for (var i = 0; i < objects.Count; i++) objects[i].Draw();
        //foreach (var obj in objects) obj.Draw();
        if (toggleShowTotal)
          ShowTotal();
        simple.Exit();
        draw.Reset();
      }
    }

    private void ShowTotal()
    {
      Console.SetCursorPosition(0, int.Parse(Thread.CurrentThread.Name) - 1);
      Console.ForegroundColor = ConsoleColor.White;
      Console.Write("Total drawing objects: {0}", objects.Count);
    }
  }


  class SimpleLock
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
