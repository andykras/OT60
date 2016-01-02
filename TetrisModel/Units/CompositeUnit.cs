using System;
using System.Collections.Generic;

namespace TetrisModel
{
  public class CompositeUnit : GameUnit
  {
    protected List<GameUnit> units = new List<GameUnit>();

    public CompositeUnit(double x, double y) :
      base(x, y)
    {
    }

    public CompositeUnit() :
      this(0, 0)
    {
    }

    public void AddUnit(GameUnit unit)
    {
      units.Add(unit);
    }

    public void RemoveUnit(GameUnit unit)
    {
      units.Remove(unit);
    }

    //    public override void Position(double xx, double yy, double a)
    //    {
    //      base.Position(xx, yy, a);
    //      foreach (var unit in units) unit.Position(xx, yy, a);
    //    }

    public override void Position(double x, double y)
    {
      foreach (var unit in units) unit.Position(x, y);
    }

    public override void Rotate(double angle)
    {
      foreach (var unit in units) unit.Rotate(angle);
    }

    //    public override void Rotate(int steps)
    //    {
    //      foreach (var unit in units) unit.Rotate(steps);
    //    }

    public override void Move(int dx, int dy)
    {
      foreach (var unit in units) unit.Move(dx, dy);
    }

    public override void Draw()
    {
      foreach (var unit in units) unit.Draw();
    }

    public override void Clear()
    {
      foreach (var unit in units) unit.Clear();
    }
  }
}

