using System;

namespace TetrisModel
{
  /// <summary>
  /// about 
  /// </summary>
  public class ConsoleWindow:GameUnit
  {
    private ConsoleColor b;
    private ConsoleColor color;
    private ConsoleColor ground;
    private ConsoleColor shadow;
    private ConsoleColor border;
    private string[] text;
    private bool useCoord = true;
    private bool useNativeColor = false;

    public ConsoleWindow(double x, double y, Color b, string[] text, Color color, Color ground, Color shadow, Color border) :
      base(x, y)
    {
      this.b = ConsoleHelpers.Convert(b);
      this.color = ConsoleHelpers.Convert(color);
      this.ground = ConsoleHelpers.Convert(ground);
      this.shadow = ConsoleHelpers.Convert(shadow);
      this.border = ConsoleHelpers.Convert(border);
      this.text = text;
    }

    public ConsoleWindow(Color b, string[] text) :
      base(0, 0)
    {
      this.b = ConsoleHelpers.Convert(b);
      this.text = text;
      useCoord = false;
      useNativeColor = true;
    }

    public override void Draw()
    {
      if (!useNativeColor) {
        if (useCoord)
          ConsoleHelpers.DrawWindow((int) x, (int) y, text, 1, -1, color, ground, true, shadow, border);
        else
          ConsoleHelpers.DrawWindow(text, 1, -1, color, ground, true, shadow, border);
      }
      else {
        if (useCoord)
          ConsoleHelpers.DrawWindow((int) x, (int) y, text);
        else
          ConsoleHelpers.DrawWindow(text);
      }
    }

    public override void Clear()
    {
      if (useCoord)
        ConsoleHelpers.DrawWindow((int) x, (int) y, text, 1, -1, b, b, true, b, b);
      else
        ConsoleHelpers.DrawWindow(text, 1, -1, b, b, true, b, b);
    }

    public override void Position(double x, double y)
    {
    }

    public override void Rotate(double angle)
    {
    }

    public override void Move(int dx, int dy)
    {
    }
  }
  
}
