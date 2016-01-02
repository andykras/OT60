using System;
using System.Threading;
using TetrisModel;

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


      while (true) {
        while (Console.KeyAvailable == false) {
//        mesh.Draw();
//        info(ang, step);
//        if (togglehelp)
//          help();
//        if (needToClearScreen) {
//          needToClearScreen = false;
//          ClearScreen();
//        }
          Thread.Sleep(25);
        }
        var key = Console.ReadKey(true).Key;
//        mesh.Clear();
//        if (key == ConsoleKey.Escape) break;
//        if (key == ConsoleKey.LeftArrow) mesh.Move(-step, 0);
//        if (key == ConsoleKey.RightArrow) mesh.Move(step, 0);
//        if (key == ConsoleKey.UpArrow) mesh.Move(0, step);
//        if (key == ConsoleKey.DownArrow) mesh.Move(0, -step);
//        if (key == ConsoleKey.E) mesh.Rotate(Math.PI / 12 * ++ang);
//        if (key == ConsoleKey.W) mesh.Rotate(Math.PI / 12 * --ang);
//        if (key == ConsoleKey.S) {
//          ClearScreen();
//          mesh = CreateMesh();
//          ang = 0;
//          step = 2;
//        }
//        if (key == ConsoleKey.Subtract && step > 1) step--;
//        if (key == ConsoleKey.Add) step++;
//        if (key == ConsoleKey.H) {
//          togglehelp = !togglehelp;
//          ClearScreen();
//        }
//      }
      }
    }
  }
}
