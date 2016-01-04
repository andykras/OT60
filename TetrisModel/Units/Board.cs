using System;

namespace TetrisModel
{
  public class Board : CompositeUnit
  {
    public Board(double x, double y, int n, int m, Color c, Func<IDevice> deviceCreator) :
      base(x, y, 0)
    {
    }
  }
}

