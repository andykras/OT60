﻿using System;

namespace TetrisModel
{
  public class PatternPyramide : Pattern
  {
    public PatternPyramide()
    {
      Matrix = new ushort[,] {
        { 0, 1, 0 }, 
        { 1, 1, 1 }, 
        { 0, 0, 0 }
      };
    }
  }
}

