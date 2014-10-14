using System;

namespace TetrisModel
{
  /// <summary>
  /// Game unit implementation.
  /// </summary>
  public interface GameUnitImplementation
  {
    int Width { get; }
    int Height { get; }

    void Draw(double x, double y, double angle, Color color);
  }
}

