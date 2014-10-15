using System;

namespace TetrisModel
{
  /// <summary>
  /// Simple (plain) drawing - angle of sprite will be ignored
  /// </summary>
  public class FastConsoleImplementation : GameUnitImplementation
  {
    /// <summary>
    /// The sprite.
    /// </summary>
    private readonly string[] sprite;

    /// <summary>
    /// Initializes a new instance of the <see cref="TetrisModel.FastConsoleImplementation"/> class.
    /// </summary>
    /// <param name="sprite">Sprite.</param>
    public FastConsoleImplementation(params string[] sprite)
    {
      this.sprite = sprite;
      Height = sprite.Length; // число строк
      Width = sprite[Height - 1].Length; // число столбцов (элементов в любой строке)
    }

    /// <summary>
    /// Draw the specified x, y, angle and color.
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <param name="angle">Angle.</param>
    /// <param name="color">Color.</param>
    public void Draw(double x, double y, double angle, Color color)
    {
      Console.ForegroundColor = (ConsoleColor) Enum.Parse(typeof(ConsoleColor), color.ToString());

      // transform coordinate system
      x = x + (int) Math.Floor(Console.BufferWidth * 0.5 - 1 + 0.5);
      y = -y + (int) Math.Floor(Console.BufferHeight * 0.5 - 1 + 0.5);

      // draw sprite, note: angle will be ignored
      foreach (var str in sprite) {
        var xnew = Math.Floor(x + 0.5);
        var ynew = Math.Floor(y + 0.5);
        if (xnew < 0 || xnew >= Console.BufferWidth || ynew < 0 || ynew >= Console.BufferHeight) return;
        Console.SetCursorPosition((int) xnew, (int) ynew);
        Console.Write(str);
        y++;
      }
      
    }

    public int Width { get; private set; }

    public int Height { get; private set; }
  }
}

