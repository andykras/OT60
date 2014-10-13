using System;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Collections.Generic;

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

      //var sprite = Registry<Settings>.GetInstanceOf<TetrisSettings>().GetSprite();
      //var sprite = new[]{ "===", " . ", "===" };
      var sprite = "[]";
      cell = new ConsoleImplementation(sprite);
      fill = new ConsoleImplementation(" .");
    }

    public GameUnitImplementation CreateCellImplementation()
    {
      return cell;
    }

    public GameUnitImplementation CreateFillImplementation()
    {
      return fill;
    }

    readonly GameUnitImplementation cell;
    readonly GameUnitImplementation fill;
  }
}

