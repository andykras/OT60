using System;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Specialized;
using System.Text;

namespace Yagan
{
  //  public class SmartPainter:IPainter
  //  {
  //    string rep;
  //    char[] canvas;
  //
  //    public void Begin()
  //    {
  //      rep = new string(' ', Console.WindowWidth * Console.WindowHeight);
  //      canvas = rep.ToCharArray();
  //    }
  //
  //    public void Draw(CharPixel pixel)
  //    {
  //      var x = ConsoleScreen.ToScreenX(pixel.X);
  //      var y = ConsoleScreen.ToScreenY(pixel.Y);
  //      if (x < 0 || x >= Console.WindowWidth || y < 0 || y >= Console.WindowHeight) return;
  //      var i = Console.BufferWidth * y + x;
  //      canvas[i] = pixel.Value;
  //    }
  //
  //    public void Draw(Sprite sprite)
  //    {
  //      foreach (var pixel in sprite) {
  //        pixel.Draw(this);
  //      }
  //    }
  //
  //    public void End()
  //    {
  //      Console.SetCursorPosition(0, 0);
  //      Console.ForegroundColor = ConsoleColor.White;
  //      Console.Write(canvas);
  //    }
  //  }
  //
  //
  public class SmartColorPainter:IPainter
  {
    char[] canvas;
    ConsoleColor[] colors;
    int width, height;
  
    public void Begin()
    {
      width = Console.WindowWidth;
      height = Console.WindowHeight;
      //var rep = new string(' ', width * height);
      //canvas = rep.ToCharArray();
      canvas = new char[width * height];
      colors = new ConsoleColor[width * height];
    }
  
    public void Draw(CharPixel pixel)
    {
      var x = ConsoleScreen.ToScreenX(pixel.X);
      var y = ConsoleScreen.ToScreenY(pixel.Y);
      if (x < 0 || x >= width || y < 0 || y >= height) return;
      var i = width * y + x;
      canvas[i] = pixel.Value;
      colors[i] = pixel.Color;
    }
  
    public void Draw(Sprite sprite)
    {
      foreach (var pixel in sprite) {
        pixel.Draw(this);
      }
    }
  
    public void End()
    {
      CharPainter.Count = 0;
      for (var x = 0; x < width; x++)
        for (var y = 0; y < height; y++) {
          var i = width * y + x;
          if (canvas[i] == 0) continue;
          CharPainter.Count++;
          try {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = colors[i];
            Console.Write(canvas[i]);
          }
          catch (Exception ex) {
            return;
          }
        }
  
    }
  }


  public class RotateWrapper:EmptyPainter
  {
    public RotateWrapper(IPainter painter) :
      base(painter)
    {
    }
    public void Draw(Sprite sprite)
    {
      var xc = sprite.X + sprite.Width / 2;
      var yc = sprite.Y - sprite.Height / 2;
      foreach (var pixel in sprite) {
        var xn = xc + (pixel.X - xc) * Math.Cos(sprite.Angle) + (pixel.Y - yc) * Math.Sin(sprite.Angle);
        var yn = yc - (pixel.X - xc) * Math.Sin(sprite.Angle) + (pixel.Y - yc) * Math.Cos(sprite.Angle);
        var rep = pixel.Clone();
        rep.Set(xn, yn);
        rep.Draw(Painter);
      }
    }
  }


  public class CharPainter:EmptyPainter
  {
    char[] canvas;
    ConsoleColor[] colors;
    int width, height, size;
    StringBuilder line;
    public static int Count = 0;

    public CharPainter()
    {
      width = Console.WindowWidth;
      height = Console.WindowHeight;
      size = 0;
      canvas = new char[width * height];
      colors = new ConsoleColor[width * height];
      line = new StringBuilder(width);
    }

    public override void Begin()
    {
      if (size == 0)
        size = width * height;
      else
        for (var i = 0; i < size; i++) canvas[i] = '\0';
    }

    public override void Draw(CharPixel pixel)
    {
      var x = ConsoleScreen.ToScreenX(pixel.X);
      var y = ConsoleScreen.ToScreenY(pixel.Y);
      if (x < 0 || x >= width || y < 0 || y >= height) return;
      var i = width * y + x;
      canvas[i] = pixel.Value;
      colors[i] = pixel.Color;
    }

    public override void End()
    {
      // to prevent exception on resize
      try {
        Count = 0;
        for (var y = 0; y < height; y++) {
          var x = 0;
          while (x < width) {
            var i = width * y + x;
            var color = colors[i];
            line.Clear();
            while (i < size && canvas[i] != 0 && colors[i] == color) {
              line.Append(canvas[i]);
              i++;
            }
            if (line.Length > 0) {
              Count++;
              Console.SetCursorPosition(x, y);
              Console.ForegroundColor = color;
              Console.Write(line);
            }
            x += line.Length + 1;
          }
        }
      }
      catch {
        return;
      }
    }

    public override void Draw(Sprite sprite)
    {
      foreach (var pixel in sprite) {
        pixel.Draw(this);
      }
      ConsoleScreen.Draw(sprite.X + sprite.Width, sprite.Y, sprite.Color, () => Console.Write(sprite));
    }
  }


  //  public class GrayPainter<TStrategy> : CharPainter<TStrategy> where TStrategy : IPainter, new()
  //  {
  //    readonly TStrategy strategy = new TStrategy();
  //
  //    public override void Draw(CharPixel pixel)
  //    {
  //      ConsoleScreen.Draw(pixel.X, pixel.Y, ConsoleColor.DarkGray, () => Console.Write(pixel.Value));
  //    }
  //
  //    public override void Draw(Sprite sprite)
  //    {
  //      strategy.Draw(sprite);
  //      ConsoleScreen.Draw(sprite.X + sprite.Width, sprite.Y, ConsoleColor.DarkGray, () => Console.Write(sprite));
  //    }
  //  }
  //
  //
  //  public class ColorPainter<TStrategy> : CharPainter<TStrategy> where TStrategy : IPainter, new()
  //  {
  //    readonly TStrategy strategy = new TStrategy();
  //    ConsoleColor color;
  //
  //    public ColorPainter(ConsoleColor color)
  //    {
  //      this.color = color;
  //    }
  //
  //    public override void Draw(CharPixel pixel)
  //    {
  //      ConsoleScreen.Draw(pixel.X, pixel.Y, color, () => Console.Write(pixel.Value));
  //    }
  //
  //    public override void Draw(Sprite sprite)
  //    {
  //      strategy.Draw(sprite);
  //      ConsoleScreen.Draw(sprite.X + sprite.Width, sprite.Y, color, () => Console.Write(sprite));
  //    }
  //  }


  //  public class CharPainter2:EmptyPainter
  //  {
  //    readonly IPainter strategy;
  //    public CharPainter2(IPainter strategy)
  //    {
  //      this.strategy = strategy;
  //    }
  //    public CharPainter2()
  //    {
  //      this.strategy = new Plain();
  //    }
  //
  //    public override void Draw(CharPixel pixel)
  //    {
  //      ConsoleScreen.Draw(pixel.X, pixel.Y, pixel.Color, () => Console.Write(pixel.Value));
  //    }
  //
  //    public override void Draw(Sprite sprite)
  //    {
  //      strategy.Draw(sprite);
  //      ConsoleScreen.Draw(sprite.X + sprite.Width, sprite.Y, sprite.Color, () => Console.Write(sprite));
  //    }
  //  }
}
