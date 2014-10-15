using System;
using TetrisModel;
using System.Threading;
using System.Runtime.InteropServices;

namespace CoordinatesTransform
{
  class MainClass
  {
    public static void Main(string[] args)
    {
      Console.CursorVisible = false;
      // 0
//      const int size = 5;
//      var sprite = new[]{ "*****", "*   *", "* o *", "*   *", "*****" };

      // 1
      const int size = 5;
      var str = new string('*', size);
      var sprite = new string[size];
      for (int i = 0; i < sprite.Length; i++) sprite[i] = str;

      var x = -size * 1.5;
      var y = size * 1.5;
      var cell = new Mesh();
      cell.AddUnit(new Cell(x - size * 4, y, Color.Green, () => new FastConsoleImplementation(sprite), Registry<PatternFactory>.GetInstanceOf<PyramidePatternFactory>()));
      cell.AddUnit(new Cell(x + size * 4, y, Color.Green, () => new ConsoleImplementation(sprite), Registry<PatternFactory>.GetInstanceOf<PyramidePatternFactory>()));
      cell.AddUnit(new Cell(0, 0, Color.Green, () => new ConsoleImplementation("O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O")));
      cell.AddUnit(new Cell(x, y, Color.Green, () => new ConsoleImplementation(sprite)));
      var ang = 0;
      while (true) {
        while (Console.KeyAvailable == false) {
          Clear();
          cell.Draw();
          Thread.Sleep(10);
        }
        var key = Console.ReadKey(true).Key;
        if (key == ConsoleKey.Escape) break;
        if (key == ConsoleKey.LeftArrow) cell.Move(-1, 0);
        if (key == ConsoleKey.RightArrow) cell.Move(1, 0);
        if (key == ConsoleKey.UpArrow) cell.Move(0, 1);
        if (key == ConsoleKey.DownArrow) cell.Move(0, -1);
        if (key == ConsoleKey.R) cell.Rotate(0);
        if (key == ConsoleKey.S) cell.Position(x, y, 0);
        if (key == ConsoleKey.E) cell.Position(Math.PI / 12 * ++ang);
      }
    }

    static void Clear()
    {
      for (var i = 0; i < Console.BufferHeight; i++) {
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.SetCursorPosition(0, i);
        Console.Write(new String('-', Console.BufferWidth));
      }
    }
  }
}
