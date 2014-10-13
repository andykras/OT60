using System;

namespace TetrisModel
{
  public abstract class GameUnit
  {
    protected GameUnit(double x, double y)
    {
      this.x = x;
      this.y = y;
      angle = 0;
      state = 0;
    }

    public  virtual void AddUnit(GameUnit unit)
    {
    }
    public virtual void RemoveUnit(GameUnit unit)
    {
    }

    protected double x;
    protected double y;
    protected double angle;
    private int state;

    public abstract void Draw();
    public abstract void Clear();

    public double Angle()
    {
      return angle / Math.PI * 180;
    }

    public virtual void Position(double xx, double yy, double a)
    {
      x = xx;
      y = yy;
      angle = a;
      if (Invalidate != null) Invalidate();
    }

    public virtual void Position(double xx, double yy)
    {
      x = xx;
      y = yy;
      //Position(xx, yy, angle);
    }

    public virtual void Position(double a)
    {
      angle = a;
      //Position(x, y, a);
    }

    public virtual void Rotate(int steps)
    {
      if (++state > 3) state = 0;
      const double step = 0.5 * Math.PI; // шаг в 90˚͋
      for (var k = 1; k < steps; k++) Position((state - 1) * step + (steps == 0 ? step : step / steps) * k);
      Position(state * step);
    }

    public event InvalidateEventHandler Invalidate;
  }
}

