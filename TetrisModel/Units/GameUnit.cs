using System;

namespace TetrisModel
{
  public abstract class GameUnit
  {
    protected double x;
    protected double y;
    protected double angle;

    protected GameUnit(double x, double y)
    {
      this.x = x;
      this.y = y;
      angle = 0;
    }

    /// <summary>
    /// Draw this unit
    /// </summary>
    public abstract void Draw();

    /// <summary>
    /// Clear this unit
    /// </summary>
    public abstract void Clear();

    /// <summary>
    /// Set Position at the (x,y)
    /// </summary>
    /// <param name="x">x coord</param>
    /// <param name="y">y coord</param>
    public abstract void Position(double x, double y);

    /// <summary>
    /// Rotate at the angle
    /// </summary>
    /// <param name="angle">Angle</param>
    public abstract void Rotate(double angle);

    //    /// <summary>
    //    /// Rotate the specified steps
    //    /// </summary>
    //    /// <param name="steps">Steps to rotate</param>
    //    public abstract void Rotate(int steps);

    /// <summary>
    /// Move at the delta
    /// </summary>
    /// <param name="dx">delta x</param>
    /// <param name="dy">delta y</param>
    public abstract void Move(int dx, int dy);
  }
}

