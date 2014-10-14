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
        var yy = y + col;

        var xc = x + 0.5 * (pattern.Width - 1);
        var yc = y + 0.5 * (pattern.Height - 1);

        var xnew = xc + (xx - xc) * Math.Cos(angle) - (yy - yc) * Math.Sin(angle);
        var ynew = yc + (xx - xc) * Math.Sin(angle) + (yy - yc) * Math.Cos(angle);

        xnew = x + (xnew - x) * device.Width;
        ynew = y + (ynew - y) * device.Height;

        device.Draw(xnew, ynew, angle, color);
      }

//      Console.ForegroundColor = ConsoleColor.Red;
//      Console.SetCursorPosition((int) x, (int) y - 2);
//      Console.Write("{0}, {1}, {2}", x, y, angle);
//      device.Draw(x, y, angle, color);
//      Console.ForegroundColor = ConsoleColor.Red;
//      Console.SetCursorPosition((int) x, (int) y);
//      Console.Write("*");
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

