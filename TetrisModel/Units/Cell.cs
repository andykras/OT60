using System;

namespace TetrisModel
{
  /// <summary>
  /// Sprite.
  /// </summary>
  public class Cell : GameUnit
  {
    public double Width { get { return device.Width; } }

    private Color color;
    private GameUnitImplementation device;
    protected Pattern pattern;

    public Cell(double x, double y, Color c, Func<GameUnitImplementation> creator, PatternFactory patternFactory) : base(x, y)
    {
      device = creator();
      pattern = patternFactory.CreatePattern();
      color = c;
    }

    public Cell(double x, double y, Color c, Func<GameUnitImplementation> creator) : this(x, y, c, creator, Registry<PatternFactory>.GetInstanceOf<BoxPatternFactory>())
    {
    }

    public Cell(double x, double y, Color c, GraphicsFactory factory) : this(x, y, c, factory.CreateCellImplementation)
    {
    }

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

      // draw reference point
      Console.ForegroundColor = ConsoleColor.Red;
      var xr = x + (int) Math.Floor(Console.BufferWidth * 0.5 - 1 + 0.5);
      var yr = -y + (int) Math.Floor(Console.BufferHeight * 0.5 - 1 + 0.5);
      if (xr < 0 || xr >= Console.BufferWidth || yr < 0 || yr >= Console.BufferHeight) return;
      Console.SetCursorPosition((int) xr, (int) yr);
      Console.Write("X");
    }

    public override void Clear()
    {
      var col = color;
      color = Color.Black;
      Draw();
      color = col;
    }
  }
}

