using System;
using System.Collections.Generic;

namespace TetrisModel
{
  /// <summary>
  /// Pattern
  /// </summary>
  public class Pattern : IEnumerable<byte>
  {
    private int h;
    private int w;

    /// <summary>
    /// Equivalent coordinates of pattern
    /// </summary>
    private List<byte> coords;

    /// <summary>
    /// Sets the matrix.
    /// </summary>
    /// <value>The matrix.</value>
    protected byte[,] Matrix {
      set
      { 
        var index = 0;
        h = value.GetLength(0);
        w = value.GetLength(1);
        coords = new List<byte>(w * h);
        foreach (var v in value) {
          if (v != 0) coords.Add((byte) (index + 1));
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
    public IEnumerator<byte> GetEnumerator()
    {
      foreach (var i in coords) yield return i;
    }

    /// <summary>
    /// Gets the enumerator.
    /// </summary>
    /// <returns>The enumerator.</returns>
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      throw new NotImplementedException();
    }
  }
}
