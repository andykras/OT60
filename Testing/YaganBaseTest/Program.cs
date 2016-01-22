using System;
using Yagan;
using System.Threading;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Globalization;

namespace YaganBaseTest
{
  class PixelComparer : IEqualityComparer<Pixel>
  {
    static double eps = 1e-5;
    public bool Equals(Pixel p1, Pixel p2)
    {
      return Math.Abs(ConsoleScreen.ToScreenX(p1.X) - ConsoleScreen.ToScreenX(p2.X)) < eps &&
      Math.Abs(ConsoleScreen.ToScreenY(p1.Y) - ConsoleScreen.ToScreenY(p2.Y)) < eps;
    }
    public int GetHashCode(Pixel p)
    {
      return ConsoleScreen.ToScreenX(p.X) ^ ConsoleScreen.ToScreenY(p.Y);
    }
  }


  class MainClass
  {
    public static void Main(string[] args)
    {
      Console.CursorVisible = false;
      Console.Clear();

//      var one_device = new ConsoleArrayDevice(new [] { 
//        "    ▄▄▄▄▄▄▄    ",
//        "▀█████████████▀",
//        "    █▄███▄█    ",
//        "     █████     ",
//        "     █▀█▀█     "
//      });
      var one_device = new ConsoleCharDevice<char>('*');
      var many_pixels = new List<Pixel>();

      var f = 30;
      var count = 10000;
      var rnd = new Random((int) DateTime.Now.ToBinary());
      for (var i = 0; i < count; i++) {
        many_pixels.Add(new Pixel(one_device, -f + f * rnd.NextDouble(), -f + f * rnd.NextDouble(), (ConsoleColor) rnd.Next(1, 15)));
      }


      var simple = new Painter2();
      var black = new Painter2Black();
      double xx = 0;
      var timer = 120;
      var clear = true;
      double yy = 0;
      double xd = 1;
      double yd = 1;
      var stopWatch = new Stopwatch();
      var set_pixels = new HashSet<Pixel>(new PixelComparer());
      do {
        stopWatch.Reset();
        stopWatch.Start();
        //ConsoleScreen.Zoom = 0.03 + 10 + 20 * Math.Sin(dd / 20.0) * Math.Cos(dd / 20.0);
        ConsoleScreen.Zoom = 1.5 * (1 + Math.Sin(xx / 30.0) * Math.Cos(yy / 15.0)) / 2.0;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Clear();
        foreach (var p in many_pixels) {
          p.Move(xd, yd);
        }

        set_pixels.Clear();
        for (var i = count; i > 0; i--) set_pixels.Add(many_pixels[i - 1]);
        //foreach (var p in many_pixels) set_pixels.Add(p);

        // Draw only one pixel at the same place
        foreach (var p in set_pixels) p.Draw();

        var str = string.Format(" Zoom: {0:F2}, dd={1:F1}, Q={2} [{3}] ", ConsoleScreen.Zoom, yy, set_pixels.Count, many_pixels.Count);
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.SetCursorPosition(1, 1);
        Console.Write(new string('▃', str.Length));
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.SetCursorPosition(1, 2);
        Console.Write(str);
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.SetCursorPosition(1, 3);
        Console.Write(new string('▀', str.Length));

        xx += xd;
        if (xx > 30) xd = -0.3;
        if (xx < -30) xd = +0.3;
        yy += yd;
        if (yy > 15) yd = -0.3;
        if (yy < -15) yd = +0.3;
        stopWatch.Stop();

        var sleep = (int) (timer - stopWatch.ElapsedMilliseconds);
        if (sleep > 0)
          Thread.Sleep(sleep);
      } while(true);
    }
  }
}
