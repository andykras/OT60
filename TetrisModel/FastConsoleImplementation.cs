using System;

namespace TetrisModel
{
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

      foreach (var str in sprite) {
        if (x < 0 || x >= Console.BufferWidth || y < 0 || y >= Console.BufferHeight) return;
        Console.SetCursorPosition((int) x, (int) y);
        Console.Write(str);
        y++;
      }
      
    }

    public int Width { get; private set; }

    public int Height { get; private set; }
  }
}

