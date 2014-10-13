//using System;
//using System.Security.Permissions;
//
//namespace TetrisModel
//{
//  /// <summary>
//  /// Tetris factory - набор фабричных методов.
//  /// Работает одновремнно как AbstractFactory и ConcreteFactory
//  /// Самый распространенный вариант реализации паттерна асбтрактная фабрика
//  /// </summary>
//  public class TetrisFactory
//  {
//    public TetrisFactory()
//    {
//    }
//
//    /// <summary>
//    /// Создает объект Tetris, который является контейнером для всех игровых объектов
//    /// </summary>
//    /// <returns>The tetris.</returns>
//    public virtual Tetris MakeTetris()
//    {
//      return new Tetris();
//    }
//
//    /// <summary>
//    /// Создает стакан тетриса
//    /// </summary>
//    /// <returns>The board.</returns>
//    public virtual Board MakeBoard()
//    {
//      return null;
//      //return new Board(0, 0);
//    }
//
//    /// <summary>
//    /// Создает фигурку - тетрамино например
//    /// </summary>
//    /// <returns>The piece.</returns>
//    public virtual Piece MakePiece()
//    {
//      return new Piece();
//    }
//  }
//
//
//  public class Piece
//  {
//
//  }
//}
//
