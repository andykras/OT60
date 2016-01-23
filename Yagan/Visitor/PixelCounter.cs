using System.Collections.Generic;

namespace Yagan
{
  public class HashVisitor : IPixelVisitor
  {
    HashSet<IPixel> set;
    public HashVisitor(HashSet<IPixel> set)
    {
      this.set = set;
    }
    public void Visit(IPixel pixel)
    {
      set.Add(pixel);
    }

    public void Visit(CharSprite sprite)
    {
    }

    public void Visit(Sprite sprite)
    {
    }
  }


  public class PixelCounter : IPixelVisitor
  {
    int charPixelCounter;
    public int TotalCharPixels { get { return charPixelCounter; } }

    int charSpriteCounter;
    public int TotalCharSprites { get { return charSpriteCounter; } }

    int spriteCounter;
    public int TotalSprites { get { return spriteCounter; } }

    public void Visit(IPixel pixel)
    {
      charPixelCounter++;
    }

    public void Visit(CharSprite sprite)
    {
      charSpriteCounter++;
    }

    public void Visit(Sprite sprite)
    {
      spriteCounter++;
    }
  }
}
