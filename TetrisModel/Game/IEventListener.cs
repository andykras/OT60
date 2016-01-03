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
  public interface IEventListener
  {
    void Update(GameEvent e);
  }
}
