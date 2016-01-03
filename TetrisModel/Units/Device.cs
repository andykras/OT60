using System;

namespace TetrisModel
{
  /// <summary>
  /// device which is to render objects
  /// </summary>
  public interface IDevice
  {
    int Width { get; }
    int Height { get; }

    bool Draw(double x, double y, double angle, Color color);
  }
}

