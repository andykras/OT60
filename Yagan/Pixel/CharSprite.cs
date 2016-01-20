using System;

namespace Yagan
{
  public class CharSprite:Sprite
  {
    private readonly string[] sprite;
    public CharSprite(string[] sprite, double x = 0, double y = 0, double angle = 0, ConsoleColor color = ConsoleColor.White) :
      base(null, angle, color)
    {
      this.sprite = sprite;
      var first = x;
      foreach (var line in sprite) {
        x = first;
        foreach (var symbol in line) {
          Add(new CharPixel(symbol, x, y, Color), symbol != ' ');
          x++;
        }
        y--;
      }
    }

    // can be virtual?
    public override void Accept(IPixelVisitor visitor)
    {
      foreach (var pixel in pixels) {
        pixel.Accept(visitor);
      }
      visitor.Visit(this);
    }

    public override string ToString()
    {
      return ShowLabel ? string.Format("[CharSprite: {0}° {1}:{2} ({3:F2},{4:F2})]", (int) Math.Round(Angle / Math.PI * 180), Width, Height, X, Y) : null;
    }

    public override IPixel Clone()
    {
      return new CharSprite(sprite, X, Y, Angle, Color){ ShowLabel = ShowLabel };
    }
  }
  
}
