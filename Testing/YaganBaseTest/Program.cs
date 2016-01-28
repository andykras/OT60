#define ipix

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


  class IPixelComparer : IEqualityComparer<IPixel>
  {
    static double eps = 1e-5;
    public bool Equals(IPixel p1, IPixel p2)
    {
      return Math.Abs(ConsoleScreen.ToScreenX(p1.X) - ConsoleScreen.ToScreenX(p2.X)) < eps &&
      Math.Abs(ConsoleScreen.ToScreenY(p1.Y) - ConsoleScreen.ToScreenY(p2.Y)) < eps;
    }
    public int GetHashCode(IPixel p)
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

      #if ipix
      var many_pixels = new List<IPixel>();
      #else
      var many_pixels = new List<Pixel>();
      #endif
//      var black = new ColorPainter<PlainSprite>(ConsoleColor.Black);
      //var painter = new CharPainter<Plain>();
      //var painter = new SmartPainter();
      //var painter = new SmartColorPainter();
      var painter = new CharPainter();

      var f = 20;
      var count = 100;
      //var rnd = new Random((int) DateTime.Now.ToBinary());
      var rnd = new Random(42);
      for (var i = 0; i < count; i++) {
        #if ipix
        many_pixels.Add(new CharSprite(new [] { 
          "    ▄▄▄▄▄▄▄    ",
          "▀█████████████▀",
          "    █▄███▄█    ",
          "     █████     ",
          "     █▀█▀█     "
        }, -f + 2 * f * rnd.NextDouble(), -f + 2 * f * rnd.NextDouble(), 0, (ConsoleColor) rnd.Next(1, 15)){ ShowLabel = false });
        #else
        many_pixels.Add(new Pixel(one_device, -f + f * rnd.NextDouble(), -f + f * rnd.NextDouble(), (ConsoleColor) rnd.Next(1, 15)));
        #endif
      }


      var simple = new Painter2();
      //var black = new Painter2Black();
      double xx = 0;
      var timer = 50;//1000 / 60;
      var clear = true;
      double yy = 0;
      double xd = 1;
      double yd = 1;
      var stopWatch = new Stopwatch();
      var stopProgram = new Stopwatch();
      //var set_pixels = new HashSet<Pixel>(new PixelComparer());
      #if ipix
      var set_pixels = new HashSet<IPixel>(new IPixelComparer());
      var set_visitor = new HashVisitor(set_pixels);
      #else
      var set_pixels = new HashSet<Pixel>(new PixelComparer());
      #endif
      ConsoleColor sleepColor = ConsoleColor.DarkGray;
      stopProgram.Start();
      int negativeCount = 0, totalCount = 0;
      double drawTime = 0;
      var width = Console.WindowWidth;
      var height = Console.WindowHeight;
      do {
        stopWatch.Reset();
        stopWatch.Start();

        Console.Clear();
        if (width != Console.WindowWidth || height != Console.WindowHeight)
          painter = new CharPainter();
        painter.Begin();

        //Console.SetWindowSize(1, 1);
        //Console.SetBufferSize(80, 80);
        //Console.SetWindowSize(40, 20);
        //Console.ResetColor();

        //Console.SetCursorPosition(0, 0);
        //Console.Write(new string(' ', Console.WindowWidth * Console.WindowHeight));
        //Console.MoveBufferArea();


//        foreach (var p in set_pixels) {
//          #if ipix 
//          p.Draw(black);
//          #else  
//          p.Draw();
//          #endif
//        }

        //ConsoleScreen.Zoom = 0.03 + 10 + 20 * Math.Sin(dd / 20.0) * Math.Cos(dd / 20.0);
        ConsoleScreen.Zoom = 2.5 * (1 + Math.Sin(xx / 30.0) * Math.Cos(yy / 15.0)) / 2.0;
        Console.BackgroundColor = ConsoleColor.Black;

        var draw_all = ConsoleScreen.Zoom > 1.5;
        draw_all = true;

        foreach (var p in many_pixels) {
          p.Move(xd, yd);
          if (draw_all)
            p.Draw(painter);
          //p.Draw(new RotateWrapper(painter));
        }

        if (!draw_all) {
          set_pixels.Clear();
          //for (var i = count; i > 0; i--) many_pixels[i - 1].Accept(set_visitor);
          foreach (var p in many_pixels) p.Accept(set_visitor);
          //foreach (var p in many_pixels) set_pixels.Add(p);

          // Draw only one pixel at the same place
          foreach (var p in set_pixels) {
            #if ipix 
            p.Draw(painter);
            #else  
          p.Draw();
            #endif
          }
        }

        painter.End();

        var str = string.Format(" Zoom: {0:F2}, dd={1:F1}, Q={2} [{3}], Filter={4}, C={5} ", ConsoleScreen.Zoom, yy, set_pixels.Count, many_pixels.Count, draw_all ? "Off" : "On", CharPainter.Count);
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
        Console.WriteLine(new string('▀', str.Length));

        xx += xd;
        if (xx > 30) xd = -0.3;
        if (xx < -30) xd = +0.3;
        yy += yd;
        if (yy > 15) yd = -0.3;
        if (yy < -15) yd = +0.3;
        stopWatch.Stop();

        var sleep = (int) (timer - stopWatch.ElapsedMilliseconds);
        sleepColor = sleep < 0 ? ConsoleColor.DarkRed : ConsoleColor.DarkGray;
        Console.ForegroundColor = sleepColor;
        Console.Write("{2} = {0:D2}(draw) + {1:D2}(sleep)", stopWatch.ElapsedMilliseconds, sleep, timer);
        drawTime += stopWatch.ElapsedMilliseconds;
        totalCount++;

        if (sleep < 0)
          negativeCount++;
        Thread.Sleep(sleep > 0 ? sleep : 16);
//        if (sleep > 0)
//          Thread.Sleep(sleep);

      } while(stopProgram.ElapsedMilliseconds < 2 * 60 * 1000);
      Console.SetCursorPosition(0, 10);
      Console.ForegroundColor = ConsoleColor.White;
      Console.Write("Draw: {0:F2}(ms), Negative: {3:F0}%({2}/{1})", drawTime / totalCount, totalCount, negativeCount, 100.0 * negativeCount / totalCount);
    }
  }
}
