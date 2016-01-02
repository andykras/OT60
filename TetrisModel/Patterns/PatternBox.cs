using System;

namespace TetrisModel
{
  class PatternBox : Pattern
  {
    public PatternBox()
    {
      Matrix = new ushort[,] { { 1 } };
    }
  }
}

