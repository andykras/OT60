using System;
using System.Collections.Generic;
using System.Web;
using System.Security.Policy;

namespace TetrisModel
{
  public class CompositeUnit : IGameUnit
  {
    public bool Enable { get; set; } = true;

    public event InvalidateEventHandler InvalidateEvent;

    protected List<IGameUnit> units = new List<IGameUnit>();

    protected double x;
    protected double y;
    protected double angle;

    public double Angle { get { return angle; } }

    public CompositeUnit(double x, double y, double angle)
    {
      this.x = x;
      this.y = y;
      this.angle = angle;
    }

    public CompositeUnit() :
      this(0, 0, 0)
    {
    }

    public virtual void AddUnit(IGameUnit unit)
    {
      units.Add(unit);
    }

    public virtual void RemoveUnit(IGameUnit unit)
    {
      units.Remove(unit);
    }

    //    public override void Position(double xx, double yy, double a)
    //    {
    //      base.Position(xx, yy, a);
    //      foreach (var unit in units) unit.Position(xx, yy, a);
    //    }

    public virtual void Position(double x, double y, double angle)
    {
      var da = angle - this.angle;
      var dx = x - this.x;
      var dy = y - this.y;
      foreach (var unit in units) {
        unit.Rotate(da);
        unit.Move(dx, dy);
      }
      this.angle = angle;
      this.x = x;
      this.y = y;
      Invalidate();
    }

    public virtual void Rotate(double da)
    {
      this.angle += da;
      foreach (var unit in units) unit.Rotate(da);
      Invalidate();
    }

    //    public override void Rotate(int steps)
    //    {
    //      foreach (var unit in units) unit.Rotate(steps);
    //    }

    public virtual void Move(double dx, double dy)
    {
      this.x += dx;
      this.y += dy;
      foreach (var unit in units) unit.Move(dx, dy);
      Invalidate();
    }

    public virtual void Draw()
    {
      if (!Enable) return;
      foreach (var unit in units) unit.Draw();
    }

    public bool Visible {
      get
      {
        foreach (var unit in units) {
          if (unit.Visible)
            return true;
        }
        return false;
      }
    }

    //    public override void Clear()
    //    {
    //      foreach (var unit in units) unit.Clear();
    //    }

    public void SetColor(Color color)
    {
      throw new NotImplementedException();
    }

    protected void Invalidate()
    {
      if (InvalidateEvent != null) InvalidateEvent();
    }
  }
}

