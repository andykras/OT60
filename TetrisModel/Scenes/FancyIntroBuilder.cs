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
  public class FancyIntroBuilder : IntroBuilder
  {
    public override void BuildIntro()
    {
      intro = factory.MakeIntroScene();
    }

    public override void BuildBackground()
    {
      var background = factory.MakeComposite();
      var stars = factory.MakeStars();
      background.AddUnit(stars);
      //background.AddUnit(factory.MakeTrees());
      intro.SetBackground(background);
    }

    public override void BuildTrees()
    {
      var trees = factory.MakeTrees();
      intro.SetTrees(trees, new Shaker(trees));
    }

    public override void BuildAnimation()
    {
      for (var i = 0; i < 150; i++) {
        var snowflake = factory.MakeSnowFlake();
        intro.AddAnimation(snowflake, new Falling(snowflake));
      }

      var piece = factory.MakePiece();
      intro.AddAnimation(piece, new Rotor(piece));
    }

    public override IntroScene GetIntro()
    {
      return intro;
    }

    public FancyIntroBuilder(UnitsFactory factory)
    {
      intro = null;
      this.factory = factory;
    }

    private IntroScene intro;
    private readonly UnitsFactory factory;
  }
}
