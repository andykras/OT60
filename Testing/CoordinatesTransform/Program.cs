using System;
using TetrisModel;
using System.Threading;

namespace CoordinatesTransform
{
  class MainClass
  {
    /// <summary>
    /// The color.
    /// </summary>
    const Color color = Color.DarkGreen;

    /// <summary>
    /// Draw!
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    public static void Main(string[] args)
    {
      Console.CursorVisible = false;
      var mesh = CreateMesh();
      var ang = 0;
      while (true) {
        while (Console.KeyAvailable == false) {
          Clear();
          mesh.Draw();
          Console.SetCursorPosition(0, 0);
          Console.ForegroundColor = ConsoleColor.White;
          Console.Write("angle: {0}", ang / 12.0 * 180);
          Thread.Sleep(25);
        }
        var key = Console.ReadKey(true).Key;
        if (key == ConsoleKey.Escape) break;
        if (key == ConsoleKey.LeftArrow) mesh.Move(-2, 0);
        if (key == ConsoleKey.RightArrow) mesh.Move(2, 0);
        if (key == ConsoleKey.UpArrow) mesh.Move(0, 2);
        if (key == ConsoleKey.DownArrow) mesh.Move(0, -2);
        if (key == ConsoleKey.R) mesh.Rotate(0);
        if (key == ConsoleKey.S) {
          mesh = CreateMesh();
          ang = 0;
        }
        if (key == ConsoleKey.E) mesh.Position(Math.PI / 12 * ++ang);
        if (key == ConsoleKey.W) mesh.Position(Math.PI / 12 * --ang);
      }
    }

    /// <summary>
    /// Creates the mesh.
    /// </summary>
    /// <returns>The mesh.</returns>
    static Mesh CreateMesh()
    {
      const int size = 7;
      var str = new string('■', size);
      var sprite = new string[size];
      for (int i = 0; i < sprite.Length; i++) sprite[i] = str;

      var x = -size * 1.5;
      var y = size * 1.5;
      var mesh = new Mesh();
      mesh.AddUnit(new Cell(x - size * 5, y, color, () => new FastConsoleImplementation(sprite), Registry<PatternFactory>.GetInstanceOf<PyramidePatternFactory>()));
      mesh.AddUnit(new Cell(x + size * 5, y, color, () => new ConsoleImplementation(sprite), Registry<PatternFactory>.GetInstanceOf<PyramidePatternFactory>()));
      mesh.AddUnit(new Cell(x + 2 * size * 5, y, color, () => new ConsoleImplementation("█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█", "█")));
      var mario = new [] { 
        "              ████████  ██████  ",
        "          ████▓▓▓▓▓▓████░░░░░░██",
        "        ██▓▓▓▓▓▓▓▓▓▓▓▓██░░░░░░██",
        "      ██▓▓▓▓▓▓████████████░░░░██",
        "    ██▓▓▓▓▓▓████████████████░░██",
        "    ██▓▓████░░░░░░░░░░░░██████  ",
        "  ████████░░░░░░██░░██░░██▓▓▓▓██",
        "  ██░░████░░░░░░██░░██░░██▓▓▓▓██",
        "██░░░░██████░░░░░░░░░░░░░░██▓▓██",
        "██░░░░░░██░░░░██░░░░░░░░░░██▓▓██",
        "  ██░░░░░░░░████████░░░░██████  ",
        "    ████░░░░░░░░██████████▓▓██  ",
        "      ██████░░░░░░░░░░██▓▓▓▓██  ",
        "  ░░██▓▓▓▓██████████████▓▓██    ",
        "  ██▓▓▓▓▓▓▓▓████░░░░░░████      ",
        "████▓▓▓▓▓▓▓▓██░░░░░░░░░░██      ",
        "████▓▓▓▓▓▓▓▓██░░░░░░░░░░██      ",
        "██████▓▓▓▓▓▓▓▓██░░░░░░████████  ",
        "  ██████▓▓▓▓▓▓████████████████  ",
        "    ██████████████████████▓▓▓▓██",
        "  ██▓▓▓▓████████████████▓▓▓▓▓▓██",
        "████▓▓██████████████████▓▓▓▓▓▓██",
        "██▓▓▓▓██████████████████▓▓▓▓▓▓██",
        "██▓▓▓▓██████████      ██▓▓▓▓████",
        "██▓▓▓▓████              ██████  ",
        "  ████                          "
      };
      mesh.AddUnit(new Cell(x, y, color, () => new ConsoleImplementation(mario)));
      return mesh;
    }

    /// <summary>
    /// Fill screen
    /// </summary>
    static void Clear()
    {
      for (var i = 0; i < Console.BufferHeight; i++) {
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.SetCursorPosition(0, i);
        Console.Write(new String('.', Console.BufferWidth));
      }
    }
  }
}
