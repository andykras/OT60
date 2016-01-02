using System;

namespace TetrisModel
{
  /// <summary>
  /// Sprite.
  /// </summary>
  public class Sprite : GameUnit
  {
    //    public event InvalidateEventHandler Invalidate;

    public static bool refDot = true;

    public double Width { get { return device.Width; } }

    //    private int state;
    private Color color;
    private Color background;
    private IDevice device;
    protected Pattern pattern;

    public Sprite(double x, double y, Color c, Color b, Func<IDevice> deviceCreator, Func<Pattern> patternCreator) :
      base(x, y)
    {
      device = deviceCreator();
      pattern = patternCreator();
      color = c;
      background = b;
    }

    public Sprite(double x, double y, Color c, Color b, Func<IDevice> deviceCreator, PatternFactory patternFactory) :
      this(x, y, c, b, deviceCreator, patternFactory.CreatePattern)
    {
    }

    public Sprite(double x, double y, Color c, Color b, Func<IDevice> deviceCreator) :
      this(x, y, c, b, deviceCreator, Registry<PatternFactory>.GetInstanceOf<BoxPatternFactory>())
    {
    }

    public Sprite(double x, double y, Color c, Func<IDevice> deviceCreator, Func<Pattern> patternCreator) :
      this(x, y, c, Color.Black, deviceCreator, patternCreator)
    {
    }
    public Sprite(double x, double y, Color c, Func<IDevice> deviceCreator, PatternFactory patternFactory) :
      this(x, y, c, Color.Black, deviceCreator, patternFactory)
    {
    }

    public Sprite(double x, double y, Color c, Func<IDevice> deviceCreator) :
      this(x, y, c, Color.Black, deviceCreator)
    {
    }

    /// <summary>
    /// Отрисовка спрайта на устройстве - ничего не знает о способе отрисовки - Console, DirectX, OpenGL...
    /// </summary>
    public override void Draw()
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
        Console.ForegroundColor = color == background ? ConsoleHelpers.Convert(background) : ConsoleColor.Red;
        var xr = x + (int) Math.Floor(Console.WindowWidth * 0.5 - 1 + 0.5);
        var yr = -y + (int) Math.Floor(Console.WindowHeight * 0.5 - 1 + 0.5);
        if (xr < 0 || xr >= Console.WindowWidth || yr < 0 || yr >= Console.WindowHeight) return;
        Console.SetCursorPosition((int) xr, (int) yr);
        Console.Write("X");
      }
    }

    public override void Clear()
    {
      var col = color;
      color = background;
      Draw();
      color = col;
    }

    //    public double Angle()
    //    {
    //      return angle / Math.PI * 180;
    //    }


    public override void Position(double x, double y)
    {
//      this.x = x;
//      this.y = y;
      Update(x, y, angle);
    }

    public override void Rotate(double angle)
    {
      //this.angle = angle;
      Update(x, y, angle);
    }

    //    public override void Rotate(int steps)
    //    {
    //      if (++state > 3) state = 0;
    //      const double step = 0.5 * Math.PI; // шаг в 90˚͋
    //      for (var k = 1; k < steps; k++) Rotate((state - 1) * step + (steps == 0 ? step : step / steps) * k);
    //      Rotate(state * step);
    //    }

    public override void Move(int dx, int dy)
    {
      Position(x + dx, y + dy);
    }

    private void Update(double x, double y, double angle)
    {
      Clear();
      this.x = x;
      this.y = y;
      this.angle = angle;
      Draw();
      //if (Invalidate != null) Invalidate();
    }

  }
}

