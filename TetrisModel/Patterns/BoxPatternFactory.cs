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
      if (!Patterns.ContainsKey(PatternType.Box)) {
        Patterns[PatternType.Box] = new PatternBox();
      }
      return Patterns[PatternType.Box];
    }
  }
}

