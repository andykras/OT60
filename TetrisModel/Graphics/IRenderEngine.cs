using System;
using System.Collections.Generic;
using System.Threading;

namespace TetrisModel
{
  public interface IRenderEngine
  {
    bool Enabled { get; }
    void Add(IGameUnit obj);
    void Remove(IGameUnit obj);
    void Start(IGameUnit scene);
    void Stop();
    void SetBackground(Color background);
  }
}
