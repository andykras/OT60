using System;
using System.Net;

namespace TetrisModel
{
  /// <summary>
  /// Graphics factory.
  /// </summary>
  public interface GraphicsFactory
  {
    GameUnitImplementation CreateCellImplementation();
    GameUnitImplementation CreateFillImplementation();
  }
}

