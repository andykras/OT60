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
  public class Intro:CompositeUnit,IEventListener
  {
    public event InvalidateEventHandler InvalidateEvent;

    public Intro(IRenderEngine engine, ITetrisGame game)
    {
      this.engine = engine;
      game.Add(this);
    }

    public void Update(GameEvent e)
    {
      if (e == GameEvent.IntroStart) {
        foreach (var handler in handlers) handler.Start();
        if (engine != null) engine.Enable = true;
      }
      else if (e == GameEvent.IntroStop) {
        foreach (var handler in handlers) handler.Stop();
        if (engine != null) engine.Enable = false;
      }
      else if (e == GameEvent.IntroToggleBackground) {
        background.Enable = !background.Enable;
      }
      else if (e == GameEvent.IntroToggleTrees) {
        trees.Enable = !trees.Enable;
        if (trees.Enable)
          treesHandler.Start();
        else
          treesHandler.Stop();
      }
    }

    IGameUnit background;
    public void SetBackground(IGameUnit unit)
    {
      background = unit;
      Add(unit, null);
    }

    IGameUnit trees;
    IHandler treesHandler;
    public void SetTrees(IGameUnit unit, IHandler handler)
    {
      trees = unit;
      treesHandler = handler;
      Add(unit, handler);
    }

    public void AddAnimation(IGameUnit unit, IHandler handler = null)
    {
      Add(unit, handler);
    }

    private void Add(IGameUnit unit, IHandler handler)
    {
      if (handler != null)
        handlers.Add(handler);
      AddUnit(unit);
      unit.InvalidateEvent += Invalidate;
      if (engine != null) {
        engine.Add(unit);
        engine.Update();
      }
    }

    public void Invalidate()
    {
      if (InvalidateEvent != null) InvalidateEvent();
    }

    private List<IHandler> handlers = new List<IHandler>();
    private IRenderEngine engine;
  }
}
