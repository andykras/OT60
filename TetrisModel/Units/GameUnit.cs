using System;
using System.Security.Policy;

namespace TetrisModel
{
  public interface IGameUnit
  {
    event InvalidateEventHandler InvalidateEvent;

    double Angle { get; }

    bool Enable { get; set; }

    /// <summary>
    /// Draw this unit
    /// </summary>
    void Draw();

    //    /// <summary>
    //    /// Clear this unit
    //    /// </summary>
    //    public abstract void Clear();

    /// <summary>
    /// Set Position at the (x,y,a)
    /// </summary>
    /// <param name="x">x coord</param>
    /// <param name="y">y coord</param>
    /// <param name = "angle"></param>
    void Position(double x, double y, double angle);

    /// <summary>
    /// Rotate at the angle
    /// </summary>
    /// <param name="da">delta angle</param>
    void Rotate(double da);

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
    void Move(double dx, double dy);

    /// <summary>
    /// Change color
    /// </summary>
    /// <param name="color">Color</param>
    void SetColor(Color color);
  }
}

