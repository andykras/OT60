using System;

namespace Yagan
{
  public interface IDevice
  {
    void Draw(Pixel pixel);
  }


  public class ConsoleCharDevice<T>: IDevice
  {
    T representation;
    public ConsoleCharDevice(T c)
    {
      representation = c;
    }

    public void Draw(Pixel pixel)
    {
      ConsoleScreen.Draw(pixel.X, pixel.Y, pixel.Color, () => Console.Write(representation));
    }
  }


  public class ConsoleArrayDevice: IDevice
  {
    string[] sprite;
    public ConsoleArrayDevice(string[] sprite)
    {
      this.sprite = sprite;
    }

    public void Draw(Pixel pixel)
    {
      var y = pixel.Y;
      foreach (var line in sprite) {
        ConsoleScreen.Draw(pixel.X, y--, pixel.Color, () => Console.Write(line));
      }
    }
  }


  public interface IPainter2
  {
    Pixel Canvas(Pixel pixel);
  }


  public class Painter2:IPainter2
  {
    public Pixel Canvas(Pixel pixel)
    {
      return pixel.Clone();
    }
  }


  public class Painter2Black:IPainter2
  {
    public Pixel Canvas(Pixel pixel)
    {
      var p = pixel.Clone();
      p.Color = ConsoleColor.Black;
      return p;
    }
  }


  public class BlackPixel:Pixel
  {
    public BlackPixel(Pixel pixel) :
      base(pixel.device, pixel.X, pixel.Y, ConsoleColor.Black)
    {
    }
  }


  public class Pixel
  {
    public virtual double X { get; protected set; }
    public virtual double Y { get; protected set; }
    public ConsoleColor Color { get; set; }

    public IDevice device;
    public Pixel(IDevice device, double x = 0, double y = 0, ConsoleColor color = ConsoleColor.White)
    {
      this.device = device;
      X = x;
      Y = y;
      Color = color;
    }

    public virtual void Move(double dx, double dy)
    {
      X += dx;
      Y += dy;
    }

    public virtual void Draw(IPainter2 painter)
    {
      device.Draw(painter.Canvas(this));
    }

    public virtual void Draw()
    {
      device.Draw(this);
    }

    public void Set(double x, double y, ConsoleColor color)
    {
      X = x;
      Y = y;
      Color = color;
    }

    public Pixel Clone()
    {
      return new Pixel(device, X, Y, Color);
    }
  }


  public class CharPixel:IPixel
  {
    public virtual double X { get; protected set; }
    public virtual double Y { get; protected set; }
    public ConsoleColor Color { get; }
    public virtual int Width { get { return 1; } }
    public virtual int Height { get { return 1; } }

    readonly char pixel;
    public char Value { get { return pixel; } }

    public CharPixel(char pixel = (char) 0, double x = 0, double y = 0, ConsoleColor color = ConsoleColor.White)
    {
      this.pixel = pixel;
      X = x;
      Y = y;
      Color = color;
    }

    public virtual void Move(double dx, double dy)
    {
      X += dx;
      Y += dy;
    }

    public virtual void Accept(IPixelVisitor visitor)
    {
      visitor.Visit(this);
    }

    public virtual void Draw(IPainter painter)
    {
      painter.Draw(this);
    }

    public void Set(double x, double y)
    {
      X = x;
      Y = y;
    }

    public IPixel Clone()
    {
      return new CharPixel(pixel, X, Y, Color);
    }
  }
}
