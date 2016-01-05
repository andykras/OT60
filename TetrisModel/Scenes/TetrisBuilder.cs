using System;

namespace TetrisModel
{
  public class TetrisBuilder
  {
    // IRenderEngine engine, ITetrisGame game
    public virtual void BuildTetris()
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

