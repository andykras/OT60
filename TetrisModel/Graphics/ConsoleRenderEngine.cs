using System;
using System.Collections.Generic;
using System.Threading;

namespace TetrisModel
{
  public class ConsoleRenderEngine : IRenderEngine, IKeyboardListener
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
    private IScene scene;
    public ConsoleRenderEngine(IScene scene)
    {
      Interlocked.Increment(ref Count);
      this.scene = scene;
      scene.InvalidateEvent += Update;
      background = ConsoleHelpers.Convert(scene.Background);
      render = new Thread(Render){ IsBackground = true, Name = Count.ToString() };
      render.Start();
      Enable = true;
      ConsoleKeyboard.Get.Add(this);
    }

    private bool run = true;
    public void Stop()
    {
      scene.InvalidateEvent -= Update;
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
      var N = int.Parse(Thread.CurrentThread.Name);
      Console.ForegroundColor = ConsoleColor.White;
      var color = (N + 3) % 15;
      Console.BackgroundColor = (ConsoleColor) color;
//      Console.SetCursorPosition(0, int.Parse(Thread.CurrentThread.Name) - 1);
//      Console.Write(new String(' ', Console.WindowWidth));
      Console.SetCursorPosition(0, int.Parse(Thread.CurrentThread.Name) - 1);
      Console.Write("#{2}: Total scene objects: {0}, Key {1} pressed", objects.Count, key, N);
    }

    private ConsoleColor background = ConsoleColor.Black;
    private void ClearDevice()
    {
      Console.BackgroundColor = background;
      ConsoleHelpers.FillRect(background);
    }
  }
  
}
