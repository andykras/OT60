namespace Yagan
{
  public class EmptyPainter:IPainter
  {
    public virtual void Draw(CharPixel pixel)
    {
    }

    public virtual void Draw(Sprite sprite)
    {
    }

    public void Begin()
    {
    }
    public void End()
    {
    }
  }
  
}
