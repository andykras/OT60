using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace TetrisModel
{
  public class IntroBuilder
  {
    public virtual void BuildIntro(IRenderEngine engine, ITetrisGame game)
    {
    }
    public virtual void BuildBackground()
    {
    }
    public virtual void BuildMenu()
    {
    }
    public virtual void BuildAnimation()
    {
    }
    public virtual void BuildTrees()
    {
    }

    public virtual Intro GetIntro()
    {
      return null;
    }

    protected IntroBuilder()
    {
    }
  }
}
