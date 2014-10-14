using System;

namespace TetrisModel
{
  class PatternBox : Pattern
  {
    public PatternBox()
    {
      Matrix = new byte[,] { { 1 } };
    }
  }
}

