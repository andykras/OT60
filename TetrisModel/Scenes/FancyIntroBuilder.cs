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
    Random rnd = new Random();
    public override void BuildIntro(UnitsFactory factory)
    {
      intro = factory.MakeIntroScene();
    }

    public override void BuildBackground()
    {
      var H = Console.WindowHeight;
      var W = Console.WindowWidth;

      var background = new CompositeUnit();

      var stars = new Sprite(() => new FastConsoleDevice(new [] { "." }), () =>
      {
        var matrix = new ushort[H, W];
        for (var i = 0; i < H; i++) for (var j = 0; j < W; j++) matrix[i, j] = rnd.NextDouble() < 0.91 ? (ushort) 0 : (ushort) 1;
        return new Pattern(matrix);
      }, -Console.WindowWidth / 2 + 1, Console.WindowHeight / 2 - 1, Color.White);
      background.AddUnit(stars);

      intro.SetBackground(background);
    }

    public override void BuildTrees()
    {
      var H = 20;
      var W = 30;
      var trees = new Sprite(() => new FastConsoleDevice(new [] { 
        @"  \  ",
        @" /*\ ", 
        @"//|\\",
        @"  |  ",
        @" ___ "
      }), () =>
      {
        var matrix = new ushort[H, W];
        for (var i = 0; i < H; i++) for (var j = 0; j < W; j++) matrix[i, j] = rnd.NextDouble() < 0.85 ? (ushort) 0 : (ushort) 1;
        return new Pattern(matrix);
      }, -Console.WindowWidth / 2 + 10, Console.WindowHeight / 2 - 3, Color.DarkGreen);
      intro.SetTrees(trees, new Shaker(trees));
    }

    public override void BuildAnimation()
    {
      for (var i = 0; i < 150; i++) {
        var snowflake = new Sprite(Registry<IGraphicsFactory>.GetInstanceOf<ConsoleGraphicsFactory>().CreateSnowFlake);
//        var foo = String.Format("{0:D2}", i);
//        var snowflake = new Sprite(() => new FastConsoleDevice(new []{ foo }));
        intro.AddAnimation(snowflake, new Falling(snowflake));
      }

      var falling_piece = new Sprite(() => new FastConsoleDevice(new []{ "[]" }), Registry<PatternFactory>.GetInstanceOf<PyramidePatternFactory>(), 0, Console.WindowHeight / 2, Color.Green);
      intro.AddAnimation(falling_piece, new Rotor(falling_piece));
    }

    public override IntroScene GetIntro()
    {
      return intro;
    }

    public FancyIntroBuilder()
    {
      intro = null;
    }

    private IntroScene intro;
  }
}
