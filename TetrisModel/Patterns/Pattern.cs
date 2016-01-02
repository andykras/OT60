using System;
using System.Collections;
using System.Collections.Generic;

namespace TetrisModel
{
  /// <summary>
  /// Pattern
  /// </summary>
  public class Pattern : IEnumerable<ushort>
  {
    private int h;
    private int w;

    public Pattern(ushort[,] matrix = null)
    {
      Matrix = matrix;
    }

    /// <summary>
    /// Equivalent coordinates of pattern
    /// </summary>
    private List<ushort> coords;

    /// <summary>
    /// Sets the matrix.
    /// </summary>
    /// <value>The matrix.</value>
    public ushort[,] Matrix {
      set
      { 
        if (value == null) return;
        var index = 0;
        h = value.GetLength(0);
        w = value.GetLength(1);
        coords = new List<ushort>(w * h);
        foreach (var v in value) {
          if (v != 0) coords.Add((ushort) (index + 1));
          index++;
        }
      }
    }

    /// <summary>
    /// Width of the matrix
    /// </summary>
    /// <value>The width.</value>
    public int Width { get { return w; } }

    /// <summary>
    /// Height of the matrix
    /// </summary>
    /// <value>The height.</value>
    public int Height { get { return h; } }

    /// <summary>
    /// Enumerator
    /// </summary>
    /// <returns>The enumerator.</returns>
    public IEnumerator<ushort> GetEnumerator()
    {
      foreach (var position in coords) yield return position;
    }

    /// <summary>
    /// Gets the explicit enumerator
    /// </summary>
    /// <returns>The enumerator.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }
  }
}
