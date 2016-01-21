using System;

namespace Yagan
{
  public interface IStrategy
  {
    void Draw(Sprite sprite, IPainter painter);
  }


  public class Plain:IStrategy
  {
    public void Draw(Sprite sprite, IPainter painter)
    {
      foreach (var pixel in sprite) {
        pixel.Draw(painter);
      }
    }
  }


  public class Rotate:IStrategy
  {
    public void Draw(Sprite sprite, IPainter painter)
    {
      var xc = sprite.X + sprite.Width / 2;
      var yc = sprite.Y - sprite.Height / 2;
      foreach (var pixel in sprite) {
        var xn = xc + (pixel.X - xc) * Math.Cos(sprite.Angle) + (pixel.Y - yc) * Math.Sin(sprite.Angle);
        var yn = yc - (pixel.X - xc) * Math.Sin(sprite.Angle) + (pixel.Y - yc) * Math.Cos(sprite.Angle);
        var rep = pixel.Clone();
        rep.Set(xn, yn);
        rep.Draw(painter);
      }
    }
  }


  public class CharPainter<TStrategy>:EmptyPainter where TStrategy : IStrategy, new()
  {
    readonly TStrategy strategy = new TStrategy();

    public override void Draw(CharPixel pixel)
    {
      ConsoleScreen.Draw(pixel.X, pixel.Y, pixel.Color, () => Console.Write(pixel.Value));
    }

    public override void Draw(Sprite sprite)
    {
      strategy.Draw(sprite, this);
      ConsoleScreen.Draw(sprite.X + sprite.Width, sprite.Y, sprite.Color, () => Console.Write(sprite));
    }
  }


  public class GrayPainter<TStrategy> : CharPainter<TStrategy> where TStrategy : IStrategy, new()
  {
    readonly TStrategy strategy = new TStrategy();

    public override void Draw(CharPixel pixel)
    {
      ConsoleScreen.Draw(pixel.X, pixel.Y, ConsoleColor.DarkGray, () => Console.Write(pixel.Value));
    }

    public override void Draw(Sprite sprite)
    {
      strategy.Draw(sprite, this);
      ConsoleScreen.Draw(sprite.X + sprite.Width, sprite.Y, ConsoleColor.DarkGray, () => Console.Write(sprite));
    }
  }


  public class CharPainter2:EmptyPainter
  {
    readonly IStrategy strategy;
    public CharPainter2(IStrategy strategy)
    {
      this.strategy = strategy;
    }
    public CharPainter2()
    {
      this.strategy = new Plain();
    }

    public override void Draw(CharPixel pixel)
    {
      ConsoleScreen.Draw(pixel.X, pixel.Y, pixel.Color, () => Console.Write(pixel.Value));
    }

    public override void Draw(Sprite sprite)
    {
      strategy.Draw(sprite, this);
      ConsoleScreen.Draw(sprite.X + sprite.Width, sprite.Y, sprite.Color, () => Console.Write(sprite));
    }
  }
}
