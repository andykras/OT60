using System;

namespace TetrisModel
{
  public class TetrisBuilder
  {
    // IRenderEngine engine, ITetrisGame game
    public virtual void BuildTetris(UnitsFactory factory)
    {
    }

    public virtual void BuildBoard()
    {
    }

    public virtual void BuildPiece()
    {
    }

    public virtual TetrisScene GetTetris()
    {
      return null;
    }

    protected TetrisBuilder()
    {
    }
  }
}

