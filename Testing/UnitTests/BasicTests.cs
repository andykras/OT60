using NUnit.Framework;
using System;
using TetrisModel;

namespace CompositeUnitTests
{
  [TestFixture()]
  public class BasicTests
  {
    [Test()]
    public void ColorsTest()
    {
      Assert.AreEqual((int) ConsoleColor.Black, (int) Color.Black);
    }

    [Test()]
    public void HierarchyTest()
    {
      var root = new CompositeUnit();
      var unit = new CompositeUnit();
      var leaf1 = new Sprite();
      unit.AddUnit(leaf1);
      root.AddUnit(unit);
      ;
      var leaf2 = new Sprite();
      root.AddUnit(leaf2);
      Assert.IsTrue(root.Enable);
    }
  }
}

