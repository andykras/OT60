using System;

namespace TetrisModel
{
  public class IntroFactory : UnitsFactory
  {
    public IntroFactory()
    {
    }

    public override IRenderEngine MakeRenderEngine()
    {
      return new ConsoleRenderEngine();
    }

    public override IntroScene MakeIntroScene()
    {
      return new IntroScene(MakeRenderEngine());
    }
  }

}
