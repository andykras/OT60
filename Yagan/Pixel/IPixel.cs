using System;

namespace Yagan
{
  public interface IPixel
  {
    double X { get; }
    double Y { get; }
    ConsoleColor Color { get; }
    int Width { get; }
    int Height { get; }
    void Set(double x, double y);
    void Move(double dx, double dy);
    void Accept(IPixelVisitor visitor);
    void Draw(IPainter painter);
    IPixel Clone();
  }
}
