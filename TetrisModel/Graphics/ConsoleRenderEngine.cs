using System;
using System.Collections.Generic;
using System.Threading;

namespace TetrisModel
{
  public class ConsoleRenderEngine : IRenderEngine, IKeyboardListener
  {
    public bool Enabled { get; private set; }

    public ConsoleRenderEngine()
    {
      stop = false;
      Interlocked.Increment(ref threadID);
      new Thread(Render){ IsBackground = true, Name = threadID.ToString() }.Start();
      ConsoleKeyboard.Get.Add(this);
    }

    public void Invalidate()
    {
      invalidate.Set();
    }

    public void Start(IGameUnit scene)
    {
      if (Enabled)
        return;
      this.scene = scene;
      scene.InvalidateEvent += Invalidate;
      Invalidate();
      Enabled = true;
    }

    public void Stop()
    {
      if (!Enabled)
        return;
      Enabled = false;
      scene.InvalidateEvent -= Invalidate;
      invalidate.Reset();
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

    public void SetBackground(Color background)
    {
      this.background = ConsoleHelpers.Convert(background);
    }

    public void Update(ConsoleKey key)
    {
      if (Enabled) {
        this.key = key;
        if (key == ConsoleKey.I) toggleShowInfo = !toggleShowInfo;
        if (key == ConsoleKey.Escape) stop = true;
        Invalidate();
      }
    }

    private void Render()
    {
      while (!stop) {
        invalidate.WaitOne();
        simple.Enter();
        ClearDevice();
        for (var i = 0; i < objects.Count; i++) objects[i].Draw();
        //foreach (var obj in objects) obj.Draw();
        if (toggleShowInfo)
          ShowInfo();
        simple.Exit();
        invalidate.Reset();
        Thread.Sleep(50);
      }
    }

    private void ShowInfo()
    {
      var id = int.Parse(Thread.CurrentThread.Name);
      Console.ForegroundColor = ConsoleColor.White;
      var color = (id + 3) % 15;
      Console.BackgroundColor = (ConsoleColor) color;
//      Console.SetCursorPosition(0, id - 1);
//      Console.Write(new String(' ', Console.WindowWidth));
      Console.SetCursorPosition(0, id - 1);
      Console.Write("#{2}: Total scene objects: {0}, Key {1} pressed", objects.Count, key, id);
    }

    private void ClearDevice()
    {
      Console.BackgroundColor = background;
      ConsoleHelpers.FillRect(background);
    }

    private static int threadID;
    private ConsoleKey key;
    private bool toggleShowInfo = true;
    private bool stop;

    ManualResetEvent invalidate = new ManualResetEvent(false);
    readonly static SimpleLock simple = new SimpleLock();

    private IGameUnit scene;
    List<IGameUnit> objects = new List<IGameUnit>();

    private ConsoleColor background = ConsoleColor.Black;
  }
  
}
