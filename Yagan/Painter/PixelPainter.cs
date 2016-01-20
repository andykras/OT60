using System;

namespace Yagan
{
  public interface IDevice
  {
    void Draw(double cartX, double cartY, ConsoleColor color);
  }


  public class ConsoleDevice:IDevice
  {
    char symbol;
    public ConsoleDevice(char symbol)
    {
      this.symbol = symbol;
    }
    public void Draw(double cartX, double cartY, ConsoleColor color)
    {
      ConsoleScreen.Draw(cartX, cartY, color, () => Console.Write(symbol));
    }
  }


  public class PixelPainter:IPainter
  {
    IDevice device;
    public PixelPainter(IDevice device)
    {
      this.device = device;
    }

    public virtual void Draw(Pixel pixel)
    {
      ConsoleScreen.Draw(pixel.X, pixel.Y, pixel.Color, pixel.Paint ?? (() => Console.Write("DEFAULT")));
    }

    public virtual void Draw(CharPixel pixel)
    {
      //ConsoleScreen.Draw(pixel.X, pixel.Y, pixel.Color, () => Console.Write(pixel.Value));
      device.Draw(pixel.X, pixel.Y, pixel.Color);
      pixel.Draw();
    }

    public virtual void Draw(Sprite sprite)
    {
      foreach (var pixel in sprite) {
        pixel.Draw(this);
      }
      ConsoleScreen.Draw(sprite.X + sprite.Width, sprite.Y, sprite.Color, () => Console.Write(sprite));
    }
  }
}
