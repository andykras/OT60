namespace Yagan
{
  public interface IPainter
  {
    void Draw(Pixel pixel);
    void Draw(CharPixel pixel);
    void Draw(Sprite sprite);
  }
}
