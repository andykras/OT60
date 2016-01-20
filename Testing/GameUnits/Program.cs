using System;
using TetrisModel;


namespace Yagan
{
  class MainClass
  {
    public static void Main(string[] args)
    {
      Console.CursorVisible = false;
      try {
        // first method - creator
        // every unit has its own implementation, even if they're all the same
        Sprite.refDot = false;
        var x = -0.5 * Console.WindowWidth;
        var submesh = new CompositeUnit();
        submesh.AddUnit(new Sprite(() => new FastConsoleDevice(
          "[][][]",
          "  []"), x, 10, Color.Blue));
        submesh.AddUnit(new Sprite(() => new ConsoleDevice(
          "   X     ",
          "   .     ",
          "   .     ",
          "   . ...X", 
          "    0    ",
          "X... .   ",
          "     .   ",
          "     .   ",
          "     X   "
        ), x + 10, 10, Color.Green));

        var mesh = new CompositeUnit();
        mesh.AddUnit(submesh);
        mesh.AddUnit(new Sprite(() => new FastConsoleDevice(
          "[-][-][-]", 
          "   [-]"), x, 20, Color.Yellow));


        // second method - factory as a creator
        // many units - has one single implementation, pattern flyweight
        const int stakan_W = 10;
        const int stakan_H = 10;
        var stakan = new Fill(x + 30, 5, stakan_W, stakan_H, Color.Gray, Registry<IGraphicsFactory>.GetInstanceOf<ConsoleGraphicsFactory>().CreateFill);
        mesh.AddUnit(stakan);

        var stakan_by_pattern1 = new Sprite(() => new ConsoleDevice("..", ".."), // or you can use CreateFillImplementation which is reading pattern from settings
                                            () =>
        {
          var matrix = new ushort[stakan_H, stakan_W];
          for (var i = 0; i < stakan_H; i++) for (var j = 0; j < stakan_W; j++) matrix[i, j] = 1;
          return new Pattern(matrix);
        }, x + 55, 23, Color.Gray);

        // stakan_by_pattern2 is equivalent of stakan_by_pattern1
        var stakan_by_pattern2 = new Sprite(() => new ConsoleDevice("..", ".."),
                                            () => new Pattern(new ushort[,] {          
          { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
          { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
          { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
          { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
          { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
          { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
          { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
          { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
          { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, 
          { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        }), x + 55, 23, Color.Gray);

        mesh.AddUnit(stakan_by_pattern1);
        mesh.AddUnit(stakan_by_pattern2);

        //var stakan2 = new Fill(60, 10, 10, 20, Color.White, Registry<GraphicsFactory>.GetInstanceOf<ConsoleGraphicsFactory>());
        //mesh.AddUnit(stakan2);

        // you can pass factory singleton as a parameter to Cell ctor
        //for (var i = 0; i < 1000; i++) mesh.AddUnit(new Cell(60, 10, Color.Gray, Registry<GraphicsFactory>.GetInstanceOf<ConsoleGraphicsFactory>()));
        // or alternatively pass a lambda creator that uses factory singleton as factory method to create single implementation for all units
        //for (var i = 0; i < 1000; i++) 
        mesh.AddUnit(new Sprite(Registry<IGraphicsFactory>.GetInstanceOf<ConsoleGraphicsFactory>().CreateCell, x + 40, 20, Color.Gray));
        mesh.AddUnit(new Sprite(Registry<IGraphicsFactory>.GetInstanceOf<ConsoleGraphicsFactory>().CreateCell, x + 35, 20, Color.Gray));

        // pattern test
        mesh.AddUnit(new Sprite(() => new FastConsoleDevice("[]"), Registry<PatternFactory>.GetInstanceOf<PyramidePatternFactory>(), x, 1, Color.Green));  

        mesh.Draw();
        while (true) {
          if (ConsoleKey.Escape == Console.ReadKey().Key) break;
          //mesh.Clear();
          //mesh.Rotate(0);
          ConsoleHelpers.FillRect();
          mesh.Rotate(Math.PI / 12);
          mesh.Move(1, 0);
          mesh.Draw();
        }
        Console.SetCursorPosition(0, 0);
      }
      catch (Exception ex) {
        Console.WriteLine(ex.Data);
        Console.WriteLine(ex.Message);
        Console.WriteLine(ex.InnerException);
      }
    }
  }
}
