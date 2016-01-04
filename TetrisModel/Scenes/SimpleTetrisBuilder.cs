using System;

namespace TetrisModel
{
  public class SimpleTetrisBuilder : TetrisBuilder
  {
    public SimpleTetrisBuilder()
    {
    }

    public override void BuildTetris(UnitsFactory factory)
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

