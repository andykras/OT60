using System;

namespace TetrisModel
{

  /// <summary>
  /// Sprite.
  /// </summary>
  public class Sprite : IGameUnit
  {
    public event InvalidateEventHandler InvalidateEvent;

    private double x;
    private double y;
    private double angle;

    public double Angle { get { return angle; } }

    public static bool refDot = true;

    //    public double Width { get { return device.Width; } }

    //    private int state;
    protected Color color;
    //    protected Color background;
    private IDevice device;
    protected Pattern pattern;

    public Sprite(Func<IDevice> deviceCreator, Func<Pattern> patternCreator, double x = 0, double y = 0, Color color = Color.White, double angle = 0)
    {
      this.x = x;
      this.y = y;
      this.angle = angle;
      this.color = color;
      device = deviceCreator();
      pattern = patternCreator();
    }

    public Sprite(Func<IDevice> deviceCreator, PatternFactory patternFactory, double x = 0, double y = 0, Color color = Color.White, double angle = 0) :
      this(deviceCreator, patternFactory.CreatePattern, x, y, color, angle)
    {
    }

    public Sprite(Func<IDevice> deviceCreator, double x = 0, double y = 0, Color color = Color.White, double angle = 0) :
      this(deviceCreator, Registry<PatternFactory>.GetInstanceOf<BoxPatternFactory>(), x, y, color, angle)
    {
    }

    public Sprite(double x = 0, double y = 0, Color color = Color.White, double angle = 0) :
      this(() => new ConsoleDevice(""), Registry<PatternFactory>.GetInstanceOf<BoxPatternFactory>(), x, y, color, angle)
    {
    }

    /// <summary>
    /// Отрисовка спрайта на устройстве - ничего не знает о способе отрисовки - Console, DirectX, OpenGL...
    /// </summary>
    public void Draw()
    {
      foreach (var item in pattern) {
        var col = (item - 1) / pattern.Width;
        var raw = item - 1 - col * pattern.Width;
        var xx = x + raw;
        var yy = y - col;

        var xc = x + 0.5 * (pattern.Width - 1);
        var yc = y - 0.5 * (pattern.Height - 1);

        var xnew = xc + (xx - xc) * Math.Cos(angle) + (yy - yc) * Math.Sin(angle);
        var ynew = yc - (xx - xc) * Math.Sin(angle) + (yy - yc) * Math.Cos(angle);

        xnew = x + (xnew - x) * device.Width;
        ynew = y + (ynew - y) * device.Height;

        device.Draw(xnew, ynew, angle, color);
      }

      // draw reference point, for _DEBUG_ purpose
      if (refDot) {
        //Console.ForegroundColor = color == background ? ConsoleHelpers.Convert(background) : ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.Red;
        var xr = x + (int) Math.Floor(Console.WindowWidth * 0.5 - 1 + 0.5);
        var yr = -y + (int) Math.Floor(Console.WindowHeight * 0.5 - 1 + 0.5);
        if (xr < 0 || xr >= Console.WindowWidth || yr < 0 || yr >= Console.WindowHeight) return;
        Console.SetCursorPosition((int) xr, (int) yr);
        Console.Write("X");
      }
    }

    //    public override void Clear()
    //    {
    //      var col = color;
    //      color = background;
    //      Draw();
    //      color = col;
    //    }

    //    public double Angle()
    //    {
    //      return angle / Math.PI * 180;
    //    }


    public void Position(double x, double y, double angle)
    {
      Update(x, y, angle);
    }

    public void Rotate(double da)
    {
      Update(x, y, angle + da);
    }

    //    public override void Rotate(int steps)
    //    {
    //      if (++state > 3) state = 0;
    //      const double step = 0.5 * Math.PI; // шаг в 90˚͋
    //      for (var k = 1; k < steps; k++) Rotate((state - 1) * step + (steps == 0 ? step : step / steps) * k);
    //      Rotate(state * step);
    //    }

    public void Move(double dx, double dy)
    {
      Position(x + dx, y + dy, angle);
    }

    protected void Update(double x, double y, double angle)
    {
      this.x = x;
      this.y = y;
      this.angle = angle;
      if (InvalidateEvent != null) InvalidateEvent();
    }

    public void SetColor(Color color)
    {
      this.color = color;
    }
  }
}

