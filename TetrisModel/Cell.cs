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

    public Cell(double x, double y, Color c, Func<GameUnitImplementation> creator) : base(x, y)
    {
      device = creator();
      color = c;
    }

    public Cell(double x, double y, Color c, GraphicsFactory factory) : base(x, y)
    {
      device = factory.CreateCellImplementation();
      color = c;
    }

    public override void Draw()
    {
//      Console.ForegroundColor = ConsoleColor.Red;
//      Console.SetCursorPosition((int) x, (int) y - 2);
//      Console.Write("{0}, {1}, {2}", x, y, angle);
      device.Draw(x, y, angle, color);
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

