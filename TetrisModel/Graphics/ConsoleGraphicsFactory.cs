using System;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Threading;

namespace TetrisModel
{
  /// <summary>
  /// Console graphics factory.
  /// </summary>
  public sealed class ConsoleGraphicsFactory : GraphicsFactory
  {
    static ConsoleGraphicsFactory()
    {
      new ConsoleGraphicsFactory();
    }

    private ConsoleGraphicsFactory()
    {
      Registry<GraphicsFactory>.Register(this);

      // todo: read all from settings
      //var sprite = Registry<Settings>.GetInstanceOf<TetrisSettings>().GetSprite();
      //var sprite = new[]{ "===", " . ", "===" };
      cell = new ConsoleDevice(new []{ "██", "██" });
      fill = new ConsoleDevice(new []{ "..", ".." });

      if (cell.Width != fill.Width || cell.Height != fill.Height) throw new SizeException("ConsoleGraphicsFactory: Size of cell and fill MUST BE THE SAME. Terminated");
    }

    public IDevice CreateCell()
    {
      return cell;
    }

    public IDevice CreateFill()
    {
      return fill;
    }

    readonly IDevice cell;
    readonly IDevice fill;
  }
}

