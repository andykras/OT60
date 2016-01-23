namespace Yagan
{
  public interface IPainter
  {
    void Draw(CharPixel pixel);
    void Draw(Sprite sprite);
    void Begin();
    void End();
  }
}
