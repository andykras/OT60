using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Diagnostics;
using System.Linq.Expressions;

namespace TetrisModel
{
  class Falling:IHandler
  {
    private readonly ManualResetEvent isActive = new ManualResetEvent(true);
    private Thread shaker;
    private static int x = 0;
    private static int y = 0;
    private static SimpleLock locker = new SimpleLock();
    public Falling(IGameUnit unit, int speed = 1000)
    {
      if (x == 0 && y == 0) {
        x = -Console.WindowWidth / 2;
        y = Console.WindowHeight / 2 - 10;
      }
      shaker = new Thread(() =>
      {
        var rnd = new Random(unit.GetHashCode());
        while (true) {
          //          locker.Enter();
          var x = -Console.WindowWidth / 2 + rnd.Next(Console.WindowWidth);
          var y = -Console.WindowHeight / 2 + rnd.Next(Console.WindowHeight);
          //          x += 4;
          //          if (x > Console.WindowWidth / 2) {
          //            x = -Console.WindowWidth / 2;
          //          }
          unit.Position(x, y, 0);
          //          unit.Draw();
          //          locker.Exit();
          var end = 1000 + rnd.NextDouble() * 1000;
          var time = 0;
          var d = 30;
          while (time < end && unit.Visible) {
            isActive.WaitOne();
            //unit.Move((int) Math.Pow(-1, ++iter), 0);
            //unit.Rotate(Math.PI / 120);
            unit.Move(0, -0.2);
            //Thread.Sleep(1 + rnd.Next(speed));
            Thread.Sleep(d);
            time += d;
          }
//          if (!unit.Visible) {
//            int foo = 1;
//          }
        }
        //unit.Enable = false;
      }){ IsBackground = true };
      shaker.Start();
    }

    public void Start()
    {
      isActive.Set();
    }

    public void Stop()
    {
      isActive.Reset();
    }
  }
}
