using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace TetrisModel
{
  /// <summary>
  /// Registry of all singletons
  /// </summary>
  public static class Registry<I> where I : class
  {
    /// <summary>
    /// The registry.
    /// </summary>
    private static readonly List<I> registry = new List<I>();

    /// <summary>
    /// For thread-safe operations
    /// </summary>
    private static readonly SmartLock smart = new SmartLock();

    /// <summary>
    /// You do not need to call it explicitly
    /// Every Singleton must call it by itself
    /// </summary>
    /// <param name="singleton">instance</param>
    public static void Register(I singleton)
    {
      if (singleton == null) return;
      smart.Enter();
      registry.Add(singleton);
      smart.Exit();
    }

    /// <summary>
    /// Get an instance or initialize it if doesn't exist
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static I GetInstanceOf<T>() where T : class, I
    {
      RuntimeHelpers.RunClassConstructor(typeof(T).TypeHandle);
      smart.In();
      var instance = registry.OfType<T>().FirstOrDefault();
      smart.Out();
      if (instance == null) {
        smart.Enter();
        instance = registry.OfType<T>().FirstOrDefault();
        smart.Exit();
      }
      return instance;
    }
  }
}

