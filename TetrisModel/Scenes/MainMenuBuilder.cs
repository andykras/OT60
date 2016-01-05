using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace TetrisModel
{

  public class MainMenuBuilder : IntroBuilder
  {
    public MainMenuBuilder(UnitsFactory factory)
    {
      intro = null;
      this.factory = factory;
    }

    public override void BuildIntro()
    {
      intro = factory.MakeIntroScene();
    }

    public override void BuildMenu()
    {
      // TODO: build menu
    }

    public override IntroScene GetIntro()
    {
      return intro;
    }

    private IntroScene intro;
    private readonly UnitsFactory factory;
  }
}
