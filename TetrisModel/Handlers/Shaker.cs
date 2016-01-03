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
  class Shaker:IHandler
  {
    private readonly ManualResetEvent isActive = new ManualResetEvent(true);
    private Thread shaker;
    public Shaker(IGameUnit unit, int speed = 1000)
    {
      shaker = new Thread(() =>
      {
        var rnd = new Random(unit.GetHashCode());
        var iter = 0;
        while (true) {
          isActive.WaitOne();
          //unit.Move((int) Math.Pow(-1, ++iter), 0);
          unit.Rotate(Math.PI / 120);
          //Thread.Sleep(1 + rnd.Next(speed));
          Thread.Sleep(100);
        }
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
