using System;

namespace Yagan
{
  public class Pixel:IPixel
  {
    public virtual double X { get; protected set; }
    public virtual double Y { get; protected set; }
    public ConsoleColor Color { get; }
    public virtual int Width { get { return 1; } }
    public virtual int Height { get { return 1; } }

    public Action Paint { get; private set; }

    public Pixel(double x = 0, double y = 0, ConsoleColor color = ConsoleColor.White, Action paint = null)
    {
      Paint = paint;
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
      return new Pixel(X, Y, Color, Paint);
    }
  }
}

