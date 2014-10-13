using System;
using System.Collections.Generic;

namespace TetrisModel
{
  public abstract class CompositeUnit : GameUnit
  {
    protected CompositeUnit(double x, double y) : base(x, y)
    {
      units = new List<GameUnit>();
    }

    public override void AddUnit(GameUnit unit)
    {
      units.Add(unit);
    }

    public override void RemoveUnit(GameUnit unit)
    {
      units.Remove(unit);
    }

    public override void Position(double xx, double yy, double a)
    {
      base.Position(xx, yy, a);
      foreach (var unit in units) unit.Position(xx, yy, a);
    }

    public override void Position(double xx, double yy)
    {
      base.Position(xx, yy);
      foreach (var unit in units) unit.Position(xx, yy);
    }

    public override void Position(double a)
    {
      base.Position(a);
      foreach (var unit in units) unit.Position(a);
    }

    public override void Rotate(int steps)
    {
      base.Rotate(steps);
      foreach (var unit in units) unit.Rotate(steps);
    }

    public override void Draw()
    {
      foreach (var unit in units) unit.Draw();
    }

    public override void Clear()
    {
      foreach (var unit in units) unit.Clear();
    }

    protected List<GameUnit> units;
  }
}

