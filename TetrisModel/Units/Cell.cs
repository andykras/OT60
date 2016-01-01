using System;

namespace TetrisModel
{
  /// <summary>
  /// Sprite.
  /// </summary>
  public class Cell : GameUnit
  {
    public static bool RefDot = true;

    public double Width { get { return device.Width; } }

    private Color color;
    private Color background;
    private GameUnitImplementation device;
    protected Pattern pattern;

    public Cell(double x, double y, Color c, Color b, Func<GameUnitImplementation> deviceCreator, PatternFactory patternFactory) : base(x, y)
    {
      device = deviceCreator();
      pattern = patternFactory.CreatePattern();
      color = c;
      background = b;
    }

    public Cell(double x, double y, Color c, Color b, Func<GameUnitImplementation> deviceCreator) : this(x, y, c, b, deviceCreator, Registry<PatternFactory>.GetInstanceOf<BoxPatternFactory>())
    {
    }

    public Cell(double x, double y, Color c, Color b, GraphicsFactory factory) : this(x, y, c, b, factory.CreateCellImplementation)
    {
    }

    public Cell(double x, double y, Color c, Func<GameUnitImplementation> deviceCreator, PatternFactory patternFactory) : this(x, y, c, Color.Black, deviceCreator, patternFactory)
    {
    }

    public Cell(double x, double y, Color c, Func<GameUnitImplementation> deviceCreator) : this(x, y, c, Color.Black, deviceCreator)
    {
    }

    public Cell(double x, double y, Color c, GraphicsFactory factory) : this(x, y, c, Color.Black, factory)
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
      if (RefDot) {
        Console.ForegroundColor = color == background ? (ConsoleColor) Enum.Parse(typeof(ConsoleColor), background.ToString()) : ConsoleColor.Red;
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
  }
}

