using System;

namespace TetrisModel
{
  public class PyramidePatternFactory : PatternFactory
  {
    static PyramidePatternFactory()
    {
      new PyramidePatternFactory();
    }

    private PyramidePatternFactory()
    {
      Registry<PatternFactory>.Register(this);
    }

    public override Pattern CreatePattern()
    {
      if (!Patterns.ContainsKey(PatternType.Pyramide)) {
        Patterns[PatternType.Pyramide] = new PatternPyramide();
      }
      return Patterns[PatternType.Pyramide];
    }
  }
}

