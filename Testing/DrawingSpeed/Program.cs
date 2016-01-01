using System;

namespace DrawingSpeed
{
  class MainClass
  {
    public static void Main(string[] args)
    {
      // coordinates transform выделить в отдельный singleton (TransformPhysicalToDeviceCoordinates, Scale - цена деления, ... ScreenWidth, ScreeHeight)

      // новый проект drawing speed - 
      // добавить два класса от fast console implementation 
      // с continue и с посимвольной отрисовкой. 

      // для main взять последний код с потоками! - OriginalTetris Program.cs


      const double foo = 1.0;
      var bar = foo; 

      Console.Write("Test{0}", args.Length);
      Console.ReadKey();
    }
  }
}
