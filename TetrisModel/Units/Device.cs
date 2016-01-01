using System;

namespace TetrisModel
{
  /// <summary>
  /// Game unit device
  /// </summary>
  public interface IDevice
  {
    int Width { get; }
    int Height { get; }

    void Draw(double x, double y, double angle, Color color);
  }
}

