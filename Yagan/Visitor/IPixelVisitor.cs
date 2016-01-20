namespace Yagan
{
  public interface IPixelVisitor
  {
    void Visit(IPixel pixel);
    void Visit(CharSprite sprite);
    void Visit(Sprite sprite);
  }
}
