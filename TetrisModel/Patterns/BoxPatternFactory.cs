using System;

namespace TetrisModel
{
  public class BoxPatternFactory : PatternFactory
  {
    static BoxPatternFactory()
    {
      new BoxPatternFactory();
    }

    private BoxPatternFactory()
    {
      Registry<PatternFactory>.Register(this);
    }

    public override Pattern CreatePattern()
    {
      if (!patterns.ContainsKey(PatternType.Box)) {
        patterns[PatternType.Box] = new PatternBox();
      }
      return patterns[PatternType.Box];
    }
  }
}

