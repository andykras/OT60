using System;
using System.Collections.Generic;

namespace TetrisModel
{
  /// <summary>
  /// Full sprite drawing implementation for Console
  /// </summary>
  public class ConsoleDevice : IDevice
  {
    /// <summary>
    /// Coordinates which were eobtained from sprite
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
    public ConsoleDevice(params string[] sprite)
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
      Console.ForegroundColor = ConsoleHelpers.Convert(color);

      // transform coordinate system
      x = x + (int) Math.Floor(Console.WindowWidth * 0.5 - 1 + 0.5);
      y = -y + (int) Math.Floor(Console.WindowHeight * 0.5 - 1 + 0.5);

      // draw sprite
      foreach (var item in coords) {
        var col = (item - 1) / Width;
        var raw = item - 1 - col * Width;
        var xx = x + raw;
        var yy = y + col;
        var xc = x + (Width - 1) * 0.5;
        var yc = y + (Height - 1) * 0.5;
        var xnew = xc + (xx - xc) * Math.Cos(angle) - (yy - yc) * Math.Sin(angle);
        var ynew = yc + (xx - xc) * Math.Sin(angle) + (yy - yc) * Math.Cos(angle);
//        xnew = Math.Floor(xnew + 0.5);
//        ynew = Math.Floor(ynew + 0.5);
        xnew = Scale(xnew, 0.5);
        ynew = Scale(ynew, 0.5);
        if (xnew < 0 || xnew >= Console.WindowWidth || ynew < 0 || ynew >= Console.WindowHeight) continue;
        Console.SetCursorPosition((int) xnew, (int) ynew);
        Console.Write(sprite[col][raw]);
      }
    }

    const double epsilon = 0.000001;

    static double Scale(double val, double scale)
    {
      var sign = Math.Sign(val);
      val = Math.Abs(val);

      int count = (int) (val / scale);
      double rest = val - count * scale;
      if (Math.Abs(scale - rest) < epsilon) count++;
      return count * scale * sign;
    }
  }
}


