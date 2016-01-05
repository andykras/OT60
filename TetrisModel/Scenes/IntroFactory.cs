using System;

namespace TetrisModel
{
  public class IntroFactory : UnitsFactory
  {
    private int Height;
    private int Width;

    Random rnd;

    public IntroFactory()
    {
      Height = Console.WindowHeight;
      Width = Console.WindowWidth;
      rnd = new Random(Height * Width + Height ^ Width);
    }

    public override IntroScene MakeIntroScene()
    {
      var engine = new ConsoleRenderEngine();
      return new IntroScene(engine);
    }

    public override IGameUnit MakeStars()
    {
      Func<IDevice> device = () => new FastConsoleDevice(new [] { "." });
      Func<Pattern> pattern = () =>
      {
        var matrix = new ushort[Height, Width];
        for (var i = 0; i < Height; i++) for (var j = 0; j < Width; j++) matrix[i, j] = rnd.NextDouble() < 0.91 ? (ushort) 0 : (ushort) 1;
        return new Pattern(matrix);
      };
      return new Sprite(device, pattern, -Width / 2 + 1, Height / 2 - 1);
    }

    public override IGameUnit MakeTrees()
    {
      var H = 20;
      var W = 30;
      Func<IDevice> device = () => new FastConsoleDevice(new [] { 
        @"  \  ",
        @" /*\ ", 
        @"//|\\",
        @"  |  ",
        @" ___ "
      });
      Func<Pattern> pattern = () =>
      {
        var matrix = new ushort[H, W];
        for (var i = 0; i < H; i++) for (var j = 0; j < W; j++) matrix[i, j] = rnd.NextDouble() < 0.85 ? (ushort) 0 : (ushort) 1;
        return new Pattern(matrix);
      };
      return new Sprite(device, pattern, -Width / 2 + 10, Height / 2 - 3, Color.DarkGreen);
    }

    public override IGameUnit MakeSnowFlake()
    {
      //        var foo = String.Format("{0:D2}", i);
      //        var snowflake = new Sprite(() => new FastConsoleDevice(new []{ foo }));
      return new Sprite(Registry<IGraphicsFactory>.GetInstanceOf<ConsoleGraphicsFactory>().CreateSnowFlake);
    }

    public override CompositeUnit MakeComposite()
    {
      return new CompositeUnit();
    }

    public override IGameUnit MakePiece()
    {
      Func<IDevice> device = () => new FastConsoleDevice(new []{ "[]" });
      var pattern = Registry<PatternFactory>.GetInstanceOf<PyramidePatternFactory>();
      return new Sprite(device, pattern, 0, Height / 2, Color.Green);
    }
  }

}
