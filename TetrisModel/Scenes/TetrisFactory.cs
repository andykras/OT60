using System;

namespace TetrisModel
{
  /// <summary>
  /// создает конкретный тетрис
  /// </summary>
  public class TetrisFactory : UnitsFactory
  {
    public TetrisFactory()
    {
    }

    public override TetrisScene MakeTetrisScene()
    {
      return new TetrisScene();
    }

  }
}

