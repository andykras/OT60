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
      if (!patterns.ContainsKey(PatternType.Pyramide)) {
        patterns[PatternType.Pyramide] = new PatternPyramide();
      }
      return patterns[PatternType.Pyramide];
    }
  }
}

