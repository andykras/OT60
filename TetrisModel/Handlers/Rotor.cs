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
  class Rotor : IKeyboardListener,IHandler
  {
    private int speed;
    private readonly ManualResetEvent isActive = new ManualResetEvent(true);
    private Thread rotor;
    public Rotor(IGameUnit unit, double delta = Math.PI / 2, int speed = 1000)
    {
      this.speed = speed;

      rotor = new Thread(() =>
      {
        while (true) {
          isActive.WaitOne();
          unit.Rotate(delta);
          unit.Move(0, -1);
          Thread.Sleep(this.speed);
        }
      }){ IsBackground = true };
      rotor.Start();

      ConsoleKeyboard.Get.Add(this);
    }

    public void Update(ConsoleKey key)
    {
      if (key == ConsoleKey.Add && speed > 100) speed -= 100;
      else if (key == ConsoleKey.Subtract) speed += 100;
    }

    public void Start()
    {
      Task.Factory.StartNew(() => ConsoleKeyboard.Get.Add(this));
      isActive.Set();
    }

    public void Stop()
    {
      isActive.Reset();
      Task.Factory.StartNew(() => ConsoleKeyboard.Get.Remove(this));
    }
  }
}
