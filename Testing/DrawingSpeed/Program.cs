using System;

namespace DrawingSpeed
{
  class MainClass
  {
    public static void Main(string[] args)
    {
      // General TODO:
      // coordinates transform выделить в отдельный singleton (TransformPhysicalToDeviceCoordinates, Scale - цена деления, ... ScreenWidth, ScreeHeight)

      // новый проект drawing speed - 
      // добавить два класса от fast console implementation 
      // с continue и с посимвольной отрисовкой. 

      // для main взять последний код с потоками! - OriginalTetris Program.cs

      new DrawSceneTest().Run();
      Console.SetCursorPosition(0, 0);
    }
  }
}
