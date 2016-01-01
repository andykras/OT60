using System;
using System.Net;

namespace TetrisModel
{
  /// <summary>
  /// Graphics factory.
  /// </summary>
  public interface GraphicsFactory
  {
    IDevice CreateCell();
    IDevice CreateFill();
  }
}

