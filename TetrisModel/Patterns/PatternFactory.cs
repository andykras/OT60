using System;
using System.Collections.Generic;

namespace TetrisModel
{
  public abstract class PatternFactory
  {
    /// <summary>
    /// Type of Different Patterns
    /// </summary>
    protected enum PatternType
    {
      Pyramide,
      LinePiece,
      Square,
      Zquiggle,
      Squiggle,
      BootLeft,
      BootRight,
      Box,
      LastPattern
    }

    /// <summary>
    /// Pattern's creator
    /// </summary>
    /// <returns>The pattern.</returns>
    abstract public Pattern CreatePattern();

    /// <summary>
    /// Predefined patterns (referring to flyweight pattern some of the Patterns can be a flyweights)
    /// </summary>
    protected Dictionary<PatternType,Pattern> Patterns = new Dictionary<PatternType, Pattern>();
  }
}

