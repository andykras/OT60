using System;
using TetrisModel;


namespace GameUnits
{
  class MainClass
  {
    public static void Main(string[] args)
    {
      Console.CursorVisible = false;
      try {
        // first method - creator
        // every unit has its own implementation, even if they're all the same
        Cell.RefDot = false;
        var x = -50;
        var submesh = new Mesh();
        submesh.AddUnit(new Cell(x, 10, Color.Blue, () => new FastConsoleImplementation(
          "[][][]",
          "  []")));
        submesh.AddUnit(new Cell(x + 10, 10, Color.Green, () => new ConsoleImplementation(
          "     ***O", 
          "    0    ",
          "O***     ")));

        var mesh = new Mesh();
        mesh.AddUnit(submesh);
        mesh.AddUnit(new Cell(x, 20, Color.Yellow, () => new FastConsoleImplementation(
          "[-][-][-]", 
          "   [-]")));


        // second method - factory as a creator
        // many units - has one single implementation, pattern flyweight
        var stakan = new Fill(x + 30, 10, 10, 20, Color.Gray, Registry<GraphicsFactory>.GetInstanceOf<ConsoleGraphicsFactory>().CreateFillImplementation);
        mesh.AddUnit(stakan);

        //var stakan2 = new Fill(60, 10, 10, 20, Color.White, Registry<GraphicsFactory>.GetInstanceOf<ConsoleGraphicsFactory>());
        //mesh.AddUnit(stakan2);

        // you can pass factory singleton as a parameter to Cell ctor
        //for (var i = 0; i < 1000; i++) mesh.AddUnit(new Cell(60, 10, Color.Gray, Registry<GraphicsFactory>.GetInstanceOf<ConsoleGraphicsFactory>()));
        // or alternatively pass a lambda creator that uses factory singleton as factory method to create single implementation for all units
        //for (var i = 0; i < 1000; i++) 
        mesh.AddUnit(new Cell(x + 40, 20, Color.Gray, Registry<GraphicsFactory>.GetInstanceOf<ConsoleGraphicsFactory>().CreateCellImplementation));
        mesh.AddUnit(new Cell(x + 35, 20, Color.Gray, Registry<GraphicsFactory>.GetInstanceOf<ConsoleGraphicsFactory>().CreateCellImplementation));

        // pattern test
        mesh.AddUnit(new Cell(1, 1, Color.Green, () => new FastConsoleImplementation("[]"), Registry<PatternFactory>.GetInstanceOf<PyramidePatternFactory>()));  

        mesh.Draw();
        for (int i = 0;; i++) {
          if (ConsoleKey.Escape == Console.ReadKey().Key) break;
          mesh.Clear();
          //mesh.Rotate(0);
          mesh.Position((i + 1) * (Math.PI / 12));
          mesh.Move(1, 0);
          mesh.Draw();
        }

      } catch (Exception ex) {
        Console.WriteLine(ex.InnerException);
      }
    }
  }
}
