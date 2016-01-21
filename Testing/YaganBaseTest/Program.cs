using System;
using Yagan;

namespace YaganBaseTest
{
  class MainClass
  {
    public static void Main(string[] args)
    {
      var foo = new CharPixel('@', 0, 0, ConsoleColor.Yellow);
      var painter = new CharPainter<Plain>();
      foo.Draw(painter);

      var bar = new CharSprite(new []{ "@@@@@@@" }, 0, -10, Math.PI / 3, ConsoleColor.Red);
      bar.Draw(new CharPainter<Rotate>());

      var line = new CharSprite(new []{ "......." }, 0, 10, Math.PI / 3, ConsoleColor.Green);
      var clone = new CloneSprite(line);
      clone.Move(-1, -1);

      var clone2 = line.Clone();
      clone2.Move(-2, -2);

      line.Draw(new CharPainter2());
      clone.Draw(new CharPainter2());
      clone2.Draw(new CharPainter2());

      Console.ReadKey();

      var gray = new GrayPainter<Rotate>();
      foo.Draw(gray);
      bar.Draw(gray);

      line.Draw(new GrayPainter<Plain>());
      clone.Draw(new GrayPainter<Plain>());
      clone2.Draw(new GrayPainter<Plain>());
    }
  }
}
