using System;
using Yagan;

namespace YaganBaseTest
{
  class MainClass
  {
    public static void Main(string[] args)
    {
      var device = new ConsoleDevice('@');
      var painter = new PixelPainter(device);

      new Pixel(0, 0, ConsoleColor.Green).Draw(painter);
      new Pixel(0, 1, ConsoleColor.White, () => Console.Write('*')).Draw(painter);
      var foo = new CharPixel('T', 0, -1, ConsoleColor.Red);
      foo.Representation = () => Console.Write((int) foo.Value);
      //foo.RepresentationOnScreen = dev => dev.Draw(foo.X, foo.Y, foo.Color, () => Console.Write(foo.Value));
      //foo.RepresentationOnScreen(device);

      var d = new ConsoleDevice(foo.Value);
      foo.Draw(new PixelPainter(d));

      //foo.Draw(painter);
    }
  }
}
