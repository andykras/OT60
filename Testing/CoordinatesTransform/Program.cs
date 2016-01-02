using System;
using TetrisModel;
using System.Threading;
using System.ComponentModel.Design;
using System.IO.IsolatedStorage;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Reflection;

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

    /// <summary>
    /// Scene
    /// </summary>
    static IGameUnit mesh;

    static IGameUnit about;
    static IGameUnit help;

    static bool toggleHelp = true;
    static bool toggleAbout = false;

    static int ang = 0;
    static int step = 2;

    /// <summary>
    /// Consoles the resize event.
    /// </summary>
    /// <param name="height">Height.</param>
    /// <param name="width">Width.</param>
    static void ConsoleResizeEvent(int height, int width)
    {
      about = CreateAbout(width);
      help = CreateHelp();
      ClearScreen();
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

      mesh = CreateMesh();
      about = CreateAbout(Console.WindowWidth);
      help = CreateHelp();
      ClearScreen();

      mesh.InvalidateEvent += Update;

      while (true) {
        info(step);
        if (toggleHelp)
          help.Draw();
        if (toggleAbout)
          about.Draw();
        var key = Console.ReadKey(false).Key;
        ConsoleHelpers.FillRect(Console.BackgroundColor);

        if (key == ConsoleKey.F1 || key == ConsoleKey.F) {
          toggleAbout = !toggleAbout;
//          about.Clear();
          mesh.Draw();
        }
        if (key == ConsoleKey.H) {
          toggleHelp = !toggleHelp;
//          help.Clear();
          mesh.Draw();
        }
        if (key == ConsoleKey.Escape) break;
        else if (key == ConsoleKey.LeftArrow) mesh.Move(-step, 0);
        else if (key == ConsoleKey.RightArrow) mesh.Move(step, 0);
        else if (key == ConsoleKey.UpArrow) mesh.Move(0, step);
        else if (key == ConsoleKey.DownArrow) mesh.Move(0, -step);
        else if (key == ConsoleKey.E) mesh.Rotate(Math.PI / 12);
        else if (key == ConsoleKey.W) mesh.Rotate(-Math.PI / 12);
        else if (key == ConsoleKey.S) {
          ang = 0;
          step = 2;
          mesh = CreateMesh();
          about = CreateAbout(Console.WindowWidth);
          help = CreateHelp();
          ClearScreen();
        }
        else if (key == ConsoleKey.Subtract && step > 1) {
          step--;
          mesh.Draw();
        }
        else if (key == ConsoleKey.Add) {
          step++;
          mesh.Draw();
        }
        else {
          mesh.Draw();
        }
      }
      Console.SetCursorPosition(0, 0);
    }

    static void Update()
    {
      mesh.Draw();
    }

    /// <summary>
    /// Creates the mesh.
    /// </summary>
    /// <returns>The mesh.</returns>
    static IGameUnit CreateMesh()
    {
      const int size = 7;
      var str = new string('■', size);
      var fill_sprite = new string[size];
      for (int i = 0; i < fill_sprite.Length; i++) fill_sprite[i] = str;

      var x = -size * 1.5;
      var y = size * 1.5;
      var mesh = new CompositeUnit();
      mesh.AddUnit(new Sprite(() => new FastConsoleDevice(fill_sprite), Registry<PatternFactory>.GetInstanceOf<PyramidePatternFactory>(), x - size * 5, y + 0.5 * size, color));
      mesh.AddUnit(new Sprite(() => new ConsoleDevice(fill_sprite), Registry<PatternFactory>.GetInstanceOf<PyramidePatternFactory>(), x + size * 5, y + 0.5 * size, color));
      mesh.AddUnit(new Sprite(() => new ConsoleDevice("█", "9", "8", "7", "6", "5", "4", "3", "2", "1", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "█"), x + 2 * size * 5, y, color));

      var skull_sprite = new [] {
        "    ▄▄▄▄▄▄▄    ",
        "▀█████████████▀",
        "    █▄███▄█    ",
        "     █████     ",
        "     █▀█▀█     "
      };
      mesh.AddUnit(new Sprite(() => new FastConsoleDevice(skull_sprite), Registry<PatternFactory>.GetInstanceOf<PyramidePatternFactory>(), x - size * 7, y - 2.5 * size, color));
      mesh.AddUnit(new Sprite(() => new ConsoleDevice(skull_sprite), Registry<PatternFactory>.GetInstanceOf<PyramidePatternFactory>(), x + size * 5, y - 2.5 * size, color));

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
      mesh.AddUnit(new Sprite(() => new ConsoleDevice(mario_sprite), x, y, color)); // simple 1-sprite element

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
      mesh.AddUnit(new Sprite(() => new ConsoleDevice(foo), x, y - 27, Color.Black));

      return mesh;
    }

    static IGameUnit CreateAbout(int width)
    {
      var text = new [] { 
        "         Coordinate Transform Test",
        "",
        "Rotate and move scene as much as you can.",
        "andykras (c) 2015-2016",
        "                          OT60 unit tests"
      };
      var about = new ConsoleWindow(width - 50, 5, background, text, Color.White, Color.DarkBlue, Color.Black, Color.Gray);
      return about;
    }

    static IGameUnit CreateHelp()
    {
      var text = new [] {
        "H                     - Toggle this Help",
        "F1                    - Show About",
        "Esc                   - Exit",
        "Left, Right, Up, Down - Move",
        "W,E                   - Rotate",
        "S                     - Restart",
        "Plus|Minus            - Increase|Decrease Step",
      };
      var help = new ConsoleWindow(background, text);
      return help;
    }

    /// <summary>
    /// Fill screen
    /// </summary>
    static void ClearScreen()
    {
      ConsoleHelpers.FillRect(0, Console.WindowHeight, 0, Console.WindowWidth, ConsoleHelpers.Convert(background));
      mesh.Draw();
      if (toggleHelp)
        help.Draw();
      if (toggleAbout)
        about.Draw();
      info(step);
    }

    static void info(int step)
    {
      ConsoleHelpers.DrawWindow(0, 0, new[]{ String.Format("angle: {0}, step: {1}", 180 * mesh.Angle / Math.PI, step) }, 0, Console.WindowWidth, ConsoleColor.White, ConsoleColor.DarkRed, false);
    }
  }
}
