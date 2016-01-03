using System;
using System.Threading.Tasks;
using System.Runtime.Remoting.Contexts;

namespace TetrisModel
{
  /// <summary>
  /// Mesh of Game Units
  /// </summary>
  public sealed class Fill : CompositeUnit
  {
    private int w = 0;
    private int h = 0;

    private int N = 0;
    private int M = 0;


    //    private Color color;

    //    private static Random rnd = new Random();

    //    private double angle = 0;

    public Fill(double x, double y, int n, int m, Color c, Func<IDevice> deviceCreator) :
      base(x, y, 0)
    {
//      color = c;
      N = n;
      M = m;
      var tmp = deviceCreator();
      w = tmp.Width;
      h = tmp.Height;
      var pos = 1;
      for (var i = 0; i < N; i++) for (var j = 0; j < M; j++) {
          if (pos > 15) pos = 1;
          //AddUnit(new Sprite(deviceCreator, x + i * w, y + j * h, (Color) (1 + rnd.Next(15))));
          AddUnit(new Sprite(deviceCreator, x + i * w, y + j * h, (Color) pos++));
        }
      //for (var i = 0; i < N; i++) for (var j = 0; j < M; j++) AddUnit(new Cell(x + i * w, y + j * h, color, deviceCreator));
    }


    public Fill(double x, double y, int n, int m, Color c, GraphicsFactory factory) :
      this(x, y, n, m, c, factory.CreateFill)
    {
    }

    public override void Draw()
    {
      var item = 0;
      foreach (var unit in units) {
        item = item == M * N ? 1 : item + 1;
        var col = (int) ((item - 1) / N);
        var raw = item - 1 - col * N;
        var xx = x + raw;
        var yy = y + col;
    
        var xc = x + 0.5 * (N - 1);
        var yc = y + 0.5 * (M - 1);
    
        var xnew = (xx - xc) * Math.Cos(angle) + (yy - yc) * Math.Sin(angle);
        var ynew = -(xx - xc) * Math.Sin(angle) + (yy - yc) * Math.Cos(angle);

        xnew += xc;
        ynew += yc;
    
        xnew = x + (xnew - x) * w;
        ynew = y + (ynew - y) * h;
    
        unit.Position(xnew, ynew, angle);
        unit.Draw();
      }
    }

    public override void Position(double x, double y, double angle)
    {
      this.x = x;
      this.y = y;
      this.angle = angle;
    }

    public override void Rotate(double da)
    {
      this.angle += da;
    }

    //    public override void Rotate(int steps)
    //    {
    //    }

    public override void Move(double dx, double dy)
    {
      Position(x + dx, y + dy, angle);
    }

  }
}

