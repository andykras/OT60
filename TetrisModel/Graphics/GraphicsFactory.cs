using System;
using System.Net;

namespace TetrisModel
{
  /// <summary>
  /// Graphics factory.
  /// </summary>
  public interface IGraphicsFactory
  {
    IDevice CreateCell();
    IDevice CreateFill();
    IDevice CreateSnowFlake();
  }
}

