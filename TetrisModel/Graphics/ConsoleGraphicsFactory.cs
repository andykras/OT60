using System;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Threading;
using System.IO;

namespace TetrisModel
{
  /// <summary>
  /// Console graphics factory.
  /// </summary>
  public sealed class ConsoleGraphicsFactory : IGraphicsFactory
  {
    static ConsoleGraphicsFactory()
    {
      new ConsoleGraphicsFactory();
    }

    private ConsoleGraphicsFactory()
    {
      Registry<IGraphicsFactory>.Register(this);

      // todo: read all from settings
      //var sprite = Registry<Settings>.GetInstanceOf<TetrisSettings>().GetSprite();
      //var sprite = new[]{ "===", " . ", "===" };
      cell = new ConsoleDevice(new []{ "██", "██" });
      fill = new ConsoleDevice(new []{ "..", ".." });
      snowflake = new FastConsoleDevice(new []{ "*" });

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

    public IDevice CreateSnowFlake()
    {
      return snowflake;
    }

    readonly IDevice cell;
    readonly IDevice fill;
    readonly IDevice snowflake;
  }
}

