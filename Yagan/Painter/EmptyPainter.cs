namespace Yagan
{
  public abstract class EmptyPainter:IPainter
  {
    protected readonly  IPainter Painter;
    protected EmptyPainter(IPainter painter = null)
    {
      Painter = painter;
    }
    public virtual void Draw(CharPixel pixel)
    {
      if (Painter != null) Painter.Draw(pixel);
    }
    public virtual void Draw(Sprite sprite)
    {
      if (Painter != null) Painter.Draw(sprite);
    }
    public virtual void Begin()
    {
      if (Painter != null) Painter.Begin();
    }
    public virtual void End()
    {
      if (Painter != null) Painter.End();
    }
  }
}
