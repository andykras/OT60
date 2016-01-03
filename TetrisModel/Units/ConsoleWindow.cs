using System;

namespace TetrisModel
{
  /// <summary>
  /// about 
  /// </summary>
  public class ConsoleWindow:IGameUnit
  {
    public bool Enable { get; set; } = true;
    public bool Visible { get { return true; } }

    private ConsoleColor b;

    private ConsoleColor color;
    private ConsoleColor ground;
    private ConsoleColor shadow;
    private ConsoleColor border;
    private string[] text;
    private bool useCoord = true;
    private bool useNativeColor = false;

    private double x;
    private double y;

    public double Angle { get { return 0; } }


    public ConsoleWindow(double x, double y, Color b, string[] text, Color color, Color ground, Color shadow, Color border)
    {
      this.x = x;
      this.y = y;
      this.b = ConsoleHelpers.Convert(b);
      this.color = ConsoleHelpers.Convert(color);
      this.ground = ConsoleHelpers.Convert(ground);
      this.shadow = ConsoleHelpers.Convert(shadow);
      this.border = ConsoleHelpers.Convert(border);
      this.text = text;
    }

    public ConsoleWindow(Color b, string[] text)
    {
      this.b = ConsoleHelpers.Convert(b);
      this.text = text;
      useCoord = false;
      useNativeColor = true;
    }

    public void Draw()
    {
      if (!Enable) return;
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

    //    public void Clear()
    //    {
    //      if (useCoord)
    //        ConsoleHelpers.DrawWindow((int) x, (int) y, text, 1, -1, b, b, true, b, b);
    //      else
    //        ConsoleHelpers.DrawWindow(text, 1, -1, b, b, true, b, b);
    //    }

    public event InvalidateEventHandler InvalidateEvent;
    public void Position(double x, double y, double angle)
    {
      throw new NotImplementedException();
    }
    public void Rotate(double da)
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
  }
  
}
