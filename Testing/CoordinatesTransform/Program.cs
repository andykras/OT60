using System;
using TetrisModel;
using System.Threading;
using System.ComponentModel.Design;
using System.IO.IsolatedStorage;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace CoordinatesTransform
{
  class MainClass
  {
    /// <summary>
    /// The color.
    /// </summary>
    const Color color = Color.Green;

    /// <summary>
    /// The background color.q
    /// </summary>
    const Color background = Color.Blue;

    static bool needToClearScreen = false;

    /// <summary>
    /// Consoles the resize event.
    /// </summary>
    /// <param name="height">Height.</param>
    /// <param name="width">Width.</param>
    static void ConsoleResizeEvent(int height, int width)
    {
      needToClearScreen = true;
    }

    /// <summary>
    /// Draw!
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    public static void Main(string[] args)
    {
      Console.CursorVisible = false;
      Console.BackgroundColor = ConsoleHelpers.Convert(background);

      new Thread(() =>
      {
        var height = Console.WindowHeight;
        var width = Console.WindowWidth;
        while (true) {
          if (height != Console.WindowHeight || width != Console.WindowWidth) {
            height = Console.WindowHeight;
            width = Console.WindowWidth;
            ConsoleResizeEvent(height, width);
          }
          Thread.Sleep(100);
        }
      }){ IsBackground = true }.Start();

      ClearScreen();

      var mesh = CreateMesh();
      var ang = 0;
      var step = 2;
      var togglehelp = true;
      while (true) {
        while (Console.KeyAvailable == false) {
          mesh.Draw();
          info(ang, step);
          if (togglehelp) help();
          if (needToClearScreen) {
            needToClearScreen = false;
            ClearScreen();
          }
          Thread.Sleep(25);
        }
        mesh.Clear();
        var key = Console.ReadKey(true).Key;
        if (key == ConsoleKey.Escape) break;
        if (key == ConsoleKey.LeftArrow) mesh.Move(-step, 0);
        if (key == ConsoleKey.RightArrow) mesh.Move(step, 0);
        if (key == ConsoleKey.UpArrow) mesh.Move(0, step);
        if (key == ConsoleKey.DownArrow) mesh.Move(0, -step);
        if (key == ConsoleKey.R) mesh.Rotate(0);
        if (key == ConsoleKey.E) mesh.Rotate(Math.PI / 12 * ++ang);
        if (key == ConsoleKey.W) mesh.Rotate(Math.PI / 12 * --ang);
        if (key == ConsoleKey.S) {
          ClearScreen();
          mesh = CreateMesh();
          ang = 0;
          step = 2;
        }
        if (key == ConsoleKey.Subtract && step > 1) step--;
        if (key == ConsoleKey.Add) step++;
        if (key == ConsoleKey.H) {
          togglehelp = !togglehelp;
          ClearScreen();
        }
      }
    }

    /// <summary>
    /// Creates the mesh.
    /// </summary>
    /// <returns>The mesh.</returns>
    static CompositeUnit CreateMesh()
    {
      const int size = 7;
      var str = new string('■', size);
      var fill_sprite = new string[size];
      for (int i = 0; i < fill_sprite.Length; i++) fill_sprite[i] = str;

      var x = -size * 1.5;
      var y = size * 1.5;
      var mesh = new CompositeUnit();
      mesh.AddUnit(new Sprite(x - size * 5, y + 0.5 * size, color, background, () => new FastConsoleDevice(fill_sprite), Registry<PatternFactory>.GetInstanceOf<PyramidePatternFactory>()));
      mesh.AddUnit(new Sprite(x + size * 5, y + 0.5 * size, color, background, () => new ConsoleDevice(fill_sprite), Registry<PatternFactory>.GetInstanceOf<PyramidePatternFactory>()));
      mesh.AddUnit(new Sprite(x + 2 * size * 5, y, color, background, () => new ConsoleDevice("█", "9", "8", "7", "6", "5", "4", "3", "2", "1", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "█")));

      var skull_sprite = new [] { 
        "    ▄▄▄▄▄▄▄    ", 
        "▀█████████████▀",
        "    █▄███▄█    ",
        "     █████     ",
        "     █▀█▀█     "
      };
      mesh.AddUnit(new Sprite(x - size * 7, y - 2.5 * size, color, background, () => new FastConsoleDevice(skull_sprite), Registry<PatternFactory>.GetInstanceOf<PyramidePatternFactory>()));
      mesh.AddUnit(new Sprite(x + size * 5, y - 2.5 * size, color, background, () => new ConsoleDevice(skull_sprite), Registry<PatternFactory>.GetInstanceOf<PyramidePatternFactory>()));

      var mario_sprite = new [] { 
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
      mesh.AddUnit(new Sprite(x, y, color, background, () => new ConsoleDevice(mario_sprite))); // simple 1-sprite element

      var foo = new [] { 
        @"        ",
        @"        ",
        @"        ",
        @"+------+",
        @"+  \/  +",
        @"+  /\  +",
        @"+------+",
        @"        ",
        @"        ",
        @"        ",
      };
      mesh.AddUnit(new Sprite(x, y - 27, Color.Black, background, () => new ConsoleDevice(foo)));

      return mesh;
    }

    /// <summary>
    /// Fill screen
    /// </summary>
    static void ClearScreen()
    {
      ConsoleHelpers.FillRect(0, Console.WindowHeight, 0, Console.WindowWidth, ConsoleHelpers.Convert(background));
    }

    /// <summary>
    /// Help text.
    /// </summary>
    static void help()
    {
      ConsoleHelpers.DrawWindow(new [] {
        "Esc                   - Exit",
        "Left, Right, Up, Down - Move",
        "W,E or R              - Rotate",
        "S                     - Restart",
        "Plus|Minus            - Increase|Decrease Step",
        "H                     - Toggle Help"
      });
    }

    static void info(int ang, int step)
    {
      ConsoleHelpers.DrawWindow(0, 0, new[]{ String.Format("angle: {0}, step: {1}", ang / 12.0 * 180, step) }, 0, Console.WindowWidth, ConsoleColor.White, ConsoleColor.DarkRed, false);
    }
  }
}
