using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TetrisModel
{
  public static class ConsoleHelpers
  {
    private class SetBackground:IDisposable
    {
      public SetBackground(ConsoleColor newColor)
      {
        oldColor = Console.BackgroundColor;
        Console.BackgroundColor = newColor;
      }
      ConsoleColor oldColor;
      public void Dispose()
      {
        Console.BackgroundColor = oldColor;
      }
    }

    public static ConsoleColor Convert(Color c)
    {
      return (ConsoleColor) Enum.Parse(typeof(ConsoleColor), c.ToString());
    }
    
    /// <summary>
    /// Fills the screen rect
    /// </summary>
    /// <param name="from">From.</param>
    /// <param name="to">To.</param>
    /// <param name="left">Left.</param>
    /// <param name="right">Right.</param>
    /// <param name="color">Color.</param>
    public static void FillRect(int from, int to, int left, int right, ConsoleColor color)
    {
      //Console.ForegroundColor = color;
      using (new SetBackground(color)) {
        for (var i = from; i < to; i++) {
          Console.SetCursorPosition(left, i);
          Console.Write(new String(' ', right - left));
        }
      }
    }

    /// <summary>
    /// Draws the window
    /// </summary>
    /// <param name="text">Text.</param>
    /// <param name="margin">Margin.</param>
    public static void DrawWindow(string[] text, int margin = 1)
    {
      var width = GetMaxWidth(text) + 2 * margin;
      var x = Console.WindowWidth / 2 - width / 2;
      var y = Console.WindowHeight / 4 - text.Length / 2;
      DrawWindow(x, y, text, margin);
    }

    /// <summary>
    /// Draws the window
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <param name="text">Text.</param>
    /// <param name="margin">Margin.</param>
    /// <param name = "width"></param>
    /// <param name = "color"></param>
    /// <param name = "background"></param>
    /// <param name = "drawShadow"></param>
    public static void DrawWindow(int x, int y, string[] text, int margin = 1, int width = -1, ConsoleColor color = ConsoleColor.Black, ConsoleColor background = ConsoleColor.DarkCyan, bool drawShadow = true)
    {
      var h = text.Length;
      var w = width;  
      if (w == -1) w = GetMaxWidth(text);
      if (drawShadow) FillRect(y + 1, y + h + 1 + 2 * margin, x + 1, x + w + 1 + 2 * margin, ConsoleColor.Black);
      FillRect(y, y + h + 2 * margin, x, x + w + 2 * margin, background);

      using (new SetBackground(background)) {
        DrawBorder(x, y, h, w, margin, ConsoleColor.White);
        PrintText(x + margin, y + margin, text, color);
      }
    }

    static void DrawBorder(int x, int y, int h, int w, int margin, ConsoleColor color)
    {
      if (margin == 0) return;
      Console.ForegroundColor = color;
      var l = w + 2 * margin - 2;
      Console.SetCursorPosition(x, y);
      Console.Write("╔" + new String('═', l) + "╗");
      for (var i = 1; i < h - 1 + 2 * margin; i++) {
        Console.SetCursorPosition(x, y + i);
        Console.Write("║" + new String(' ', l) + "║");
      }
      Console.SetCursorPosition(x, y + h + 2 * margin - 1);
      Console.Write("╚" + new String('═', l) + "╝");
    }

    static int GetMaxWidth(List<string> text)
    {
      int w = -1;
      text.ForEach((s) => w = Math.Max(w, s.Length));
      return w;
    }

    static int GetMaxWidth(string[] text)
    {
      return GetMaxWidth(new List<string>(text));
    }

    static void PrintText(int x, int y, string[] text, ConsoleColor color)
    {
      Console.ForegroundColor = color;
      for (var i = 0; i < text.Length; i++) {
        Console.SetCursorPosition(x, y + i);
        Console.Write(text[i]);
      }
    }

  }
}

