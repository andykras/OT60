namespace Yagan
{
  public class EmptyPainter:IPainter
  {
    public void Draw(Pixel pixel)
    {
    }

    public virtual void Draw(CharPixel pixel)
    {
    }

    public virtual void Draw(Sprite sprite)
    {
    }
  }
  
}
