using System;
using System.Collections.Generic;
using System.Threading;

namespace TetrisModel
{
  public interface IRenderEngine
  {
    bool Enable { get; set; }
    void Add(IGameUnit obj);
    void Remove(IGameUnit obj);
    void Update();
    void Start(IGameUnit scene);
    void Stop();
    void SetBackground(Color background);
  }
}
