using System;

namespace TetrisModel
{
  public class SimpleTetrisBuilder : TetrisBuilder
  {
    UnitsFactory factory;
    public SimpleTetrisBuilder(UnitsFactory factory)
    {
      this.factory = factory;
    }

    public override void BuildTetris()
    {
      tetris = factory.MakeTetrisScene();
      //var board = factory.MakeBoard();
      //var piece = factory.MakePiece();

      //board.AddUnit(piece);

      //tetris.AddUnit(board);
      //tetris.AddUnit(piece);
    }

    public override TetrisScene GetTetris()
    {
      return tetris;
    }

    private TetrisScene tetris;
  }
}

