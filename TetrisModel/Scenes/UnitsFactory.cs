using System;
using System.Security.Permissions;

namespace TetrisModel
{
  /// <summary>
  /// Tetris factory - набор фабричных методов.
  /// Работает одновремнно как AbstractFactory и ConcreteFactory
  /// Самый распространенный вариант реализации паттерна асбтрактная фабрика
  /// </summary>
  public class UnitsFactory
  {
    protected UnitsFactory()
    {
    }

    /// <summary>
    /// Создает объект Tetris, который является контейнером для всех игровых объектов
    /// </summary>
    /// <returns>The tetris.</returns>
    public virtual TetrisScene MakeTetrisScene()
    {
      return null;
    }

    public virtual IntroScene MakeIntroScene()
    {
      return null;
    }

    /// <summary>
    /// Создает стакан тетриса
    /// </summary>
    /// <returns>The board.</returns>
    public virtual Board MakeBoard()
    {
      return null;
      //return new Board(0, 0);
    }

    /// <summary>
    /// Создает фигурку - тетрамино например
    /// </summary>
    /// <returns>The piece.</returns>
    public virtual IGameUnit MakePiece()
    {
      return null;
    }

    public virtual IGameUnit MakeStars()
    {
      return null;
    }

    public virtual IGameUnit MakeTrees()
    {
      return null;
    }
    public virtual IGameUnit MakeSnowFlake()
    {
      return null;
    }

    public virtual CompositeUnit MakeComposite()
    {
      return null;
    }

  }

}

