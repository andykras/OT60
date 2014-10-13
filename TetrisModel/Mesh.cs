using System;
using System.Collections.Generic;

namespace TetrisModel
{
  public class Mesh : CompositeUnit // в Cell свой CellImp который нам не нужен, поэтому наследуемся от GameUnit
  {
    /// <summary>
    /// угол поворота
    /// </summary>
    //private double rotation;


    public Mesh() : base(0, 0)
    {
//      pattern = patternFactory.create();
//      units = MeshBuilder.Build();
      //rotation = angle;
      //var cell1 = new Cell<ConsoleGraphicsFactory,CellConsoleClassic>(x, y, Color.Green);
      //AddUnit(cell1);
      //var cell2 = new Cell<ConsoleGraphicsFactory,CellConsole>(x + cell1.Width + 10, y, Color.Red, "==");
      //AddUnit(cell2);
    }

    //    public override void Draw()
    //    {
    //      foreach (var unit in units) unit.Draw();
    //      //      foreach (var item in pattern) {
    //      //        var col = (int) ((item - 1) / pattern.Width);
    //      //        var raw = item - 1 - col * pattern.Width;
    //      //        var xx = x + raw;
    //      //        var yy = y + col;
    //      //
    //      //        var xc = x + 0.5 * (pattern.Width - 1);
    //      //        var yc = y + 0.5 * (pattern.Height - 1);
    //      //
    //      //        var xnew = (xx - xc) * Math.Cos(angle) - (yy - yc) * Math.Sin(angle);
    //      //        var ynew = (xx - xc) * Math.Sin(angle) + (yy - yc) * Math.Cos(angle);
    //      //
    //      //        xnew += xc;
    //      //        ynew += yc;
    //      //
    //      //        xnew = x + (xnew - x) * cell.Width;
    //      //        ynew = y + (ynew - y) * cell.Height;
    //      //
    //      //        pool.GetEnumerator().Current.Draw(xnew, ynew, rotation, color);
    //      //        pool.GetEnumerator().MoveNext();
    //      //      }
    //    }


    public void SetRotation(double r)
    {
      //rotation = r;
    }

    /// <summary>
    /// пул реализаций из которого по очереди будут браться имплементации для отрисовки
    /// </summary>
    //private List<CellImplementation> pool;
  }
}

