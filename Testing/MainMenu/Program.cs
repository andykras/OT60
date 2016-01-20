using System;
using TetrisModel;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;

namespace MainMenu
{
  class MainClass
  {
    public static void Main(string[] args)
    {
      new MenuTest().Run();
      Console.SetCursorPosition(0, 0);
    }
  }
}
