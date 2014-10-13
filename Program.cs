using System;
using TetrisModel;
using System.Threading;
using System.Collections.Generic;
using System.Globalization;

namespace OT60
{
  class MainClass
  {

    /// <summary>
    /// The entry point of the program, where the program control starts and ends.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    public static void Main(string[] args)
    {
      Console.CursorVisible = false;
      Console.WriteLine("OT60 is a clone of original Tetris from Elektronika 60"); 

      var submesh = new Mesh();
      submesh.AddUnit(new Cell(10, 10, Color.Blue, () => new FastConsoleImplementation(
        "[][][]",
        "  []")));
      submesh.AddUnit(new Cell(20, 10, Color.Green, () => new ConsoleImplementation(
        "     ***O", 
        "    0    ",
        "O***     ")));

      var mesh = new Mesh();
      mesh.AddUnit(submesh);
      mesh.AddUnit(new Cell(10, 20, Color.Yellow, () => new FastConsoleImplementation(
        "[-][-][-]", 
        "   [-]")));


      // fill example
      var stakan = new Fill(30, 10, 10, 20, Color.White, () => Registry<GraphicsFactory>.GetInstanceOf<ConsoleGraphicsFactory>().CreateFillImplementation());
      mesh.AddUnit(stakan);

      // flyweight example
      for (var i = 0; i < 1000; i++) mesh.AddUnit(new Cell(60, 10, Color.Gray, Registry<GraphicsFactory>.GetInstanceOf<ConsoleGraphicsFactory>()));
      for (var i = 0; i < 1000; i++) mesh.AddUnit(new Cell(80, 10, Color.Gray, () => Registry<GraphicsFactory>.GetInstanceOf<ConsoleGraphicsFactory>().CreateFillImplementation()));
        


      mesh.Draw();
      for (int i = 0;; i++) {
        if (ConsoleKey.Escape == Console.ReadKey().Key) break;
        mesh.Clear();
        //mesh.Rotate(1);
        mesh.Position(i * (Math.PI / 12));
        mesh.Draw();
      }
    }
  }
}
