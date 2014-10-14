using System;
using System.Collections.Generic;

namespace TetrisModel
{
  /// <summary>
  /// Cell console.
  /// </summary>
  public class ConsoleImplementation : GameUnitImplementation
  {
    /// <summary>
    /// The coords.
    /// </summary>
    private readonly List<int> coords;

    /// <summary>
    /// The sprite.
    /// </summary>
    private readonly string[] sprite;

    /// <summary>
    /// Initializes a new instance of the <see cref="TetrisModel.ConsoleImplementation"/> class.
    /// </summary>
    /// <param name="sprite">Sprite.</param>
    public ConsoleImplementation(params string[] sprite)
    {
      this.sprite = sprite;
      var index = 0;
      Height = sprite.Length; // число строк
      Width = sprite[Height - 1].Length; // число столбцов (элементов в любой строке)
      coords = new List<int>(Width * Height);
      foreach (var str in sprite) {
        foreach (var ch in str) {
          if (ch != ' ') coords.Add(index + 1);
          index++;
        }
      }
    }

    /// <summary>
    /// Gets the width.
    /// </summary>
    /// <value>The width.</value>
    public int Width { get; private set; }

    /// <summary>
    /// Gets the height.
    /// </summary>
    /// <value>The height.</value>
    public int Height { get; private set; }

    /// <summary>
    /// Draw the specified x, y, angle and color.
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <param name="angle">Angle.</param>
    /// <param name="color">Color.</param>
    public virtual void Draw(double x, double y, double angle, Color color)
    {
      Console.ForegroundColor = (ConsoleColor) Enum.Parse(typeof(ConsoleColor), color.ToString());
      foreach (var item in coords) {
        var col = (item - 1) / Width;
        var raw = item - 1 - col * Width;
        var xx = x + raw;
        var yy = y + col;
        var xc = x + (Width - 1) * 0.5;
        var yc = y + (Height - 1) * 0.5;
        var xnew = xc + (xx - xc) * Math.Cos(angle) - (yy - yc) * Math.Sin(angle);
        var ynew = yc + (xx - xc) * Math.Sin(angle) + (yy - yc) * Math.Cos(angle);
        if (xnew < 0 || xnew >= Console.BufferWidth || ynew < 0 || ynew >= Console.BufferHeight) continue;
        Console.SetCursorPosition((int) xnew, (int) ynew);
        //Console.SetCursorPosition((int) Math.Round(xnew), (int) Math.Round(ynew));
        //Console.SetCursorPosition((int) Math.Floor(xnew), (int) Math.Floor(ynew));
        Console.Write(sprite[col][raw]);
      }
    }
  }
}


