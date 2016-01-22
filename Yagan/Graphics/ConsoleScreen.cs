using System;
using System.Threading;
using System.Globalization;

namespace Yagan
{
  public static class ConsoleScreen
  {
    public static double Zoom = 1.0;

    public static double Fit(double value, double limit, double epsilon = 0.000001)
    {
      var sign = Math.Sign(value);
      value = Math.Abs(value);

      int count = (int) (value / limit);
      double rest = value - count * limit;
      if (Math.Abs(limit - rest) < epsilon) count++;
      return count * limit * sign;
    }

    public static int ToScreenX(double x)
    {
      return (int) Fit(Zoom * x + Console.WindowWidth / 2, 0.5);
    }
    public  static int ToScreenY(double y)
    {
      return (int) Fit(Console.WindowHeight / 2 - Zoom * y, 0.5);
    }

    public static void Draw(double cartX, double cartY, ConsoleColor color, Action draw)
    {
      var x = ToScreenX(cartX);
      var y = ToScreenY(cartY);
      if (x < 0 || x >= Console.WindowWidth || y < 0 || y >= Console.WindowHeight) return;
      Console.SetCursorPosition(x, y);
      Console.ForegroundColor = color;
      draw();
    }

    public static void Print(string[] text, bool alignRight = false)
    {
      var y = text.Length;
      foreach (var line in text) Print(line, y--, alignRight);
    }

    public static void Print(string text, int y, bool alignRight = false)
    {
      var x = alignRight ? Console.WindowWidth - text.Length : 0;
      y = Console.WindowHeight - y;
      if (x < 0 || x >= Console.WindowWidth || y < 0 || y >= Console.WindowHeight) return;
      Console.SetCursorPosition(x, y);
      Console.Write(text);
    }

    public static void Clear()
    {
      Console.BackgroundColor = ConsoleColor.Black;
      Console.Clear();
    }

    public static void Fill(char symbol = ' ', ConsoleColor color = ConsoleColor.Black)
    {
      Console.BackgroundColor = ConsoleColor.Black;
      var line = new string(symbol, Console.WindowWidth);
      Console.ForegroundColor = color;
      for (var i = 0; i < Console.WindowHeight; i++) {
        Console.SetCursorPosition(0, i);
        Console.Write(line);
      }
    }

    public static void SetDotAsSeparator()
    {
      Console.CursorVisible = false;
      var info = (CultureInfo) Thread.CurrentThread.CurrentCulture.Clone();
      info.NumberFormat.NumberDecimalSeparator = ".";
      Thread.CurrentThread.CurrentCulture = info;
    }
  }
  
}
