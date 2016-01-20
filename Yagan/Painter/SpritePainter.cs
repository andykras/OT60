using System;

namespace Yagan
{
  public class SpritePainter:PixelPainter
  {
    public SpritePainter() :
      base(null)
    {
    }

    public override void Draw(Sprite sprite)
    {
      var xc = sprite.X + sprite.Width / 2;
      var yc = sprite.Y - sprite.Height / 2;
      foreach (var pixel in sprite) {
        var xn = xc + (pixel.X - xc) * Math.Cos(sprite.Angle) + (pixel.Y - yc) * Math.Sin(sprite.Angle);
        var yn = yc - (pixel.X - xc) * Math.Sin(sprite.Angle) + (pixel.Y - yc) * Math.Cos(sprite.Angle);
        var rep = pixel.Clone();
        rep.Set(xn, yn);
        rep.Draw(this);
      }
      ConsoleScreen.Draw(sprite.X + sprite.Width, sprite.Y, sprite.Color, () => Console.Write(sprite));
    }
  }
}
