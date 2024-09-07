using System;
using com.karabaev.utilities.unity.GameKit.Utils;
using com.karabaev.utilities.unity.playmode.tests;
using com.karabaev.utilities.unity.tests.Editor;
using NUnit.Framework;
using UnityEngine;

namespace com.karabaev.utilities.unity.editor.tests.GameKit
{
  public class FindComponentFromChildrenTests
  {
    [Test]
    public void RequireReturnsComponentFromChild()
    {
      var obj11 = TestGameObjectBuilder
        .Create()
        .Build();
      
      var obj21 = TestGameObjectBuilder
        .Create()
        .AddComponent<TestComponent>()
        .Build();
      
      var obj1 = TestGameObjectBuilder
        .Create()
        .AddChild(obj11)
        .Build();
      
      var obj2 = TestGameObjectBuilder
        .Create()
        .AddChild(obj21)
        .Build();
      
      using var root = TestGameObjectBuilder
        .Create()
        .AddChild(obj1)
        .AddChild(obj2)
        .Build<Transform>();

      var result = root.Component.RequireComponentFromChildren<TestComponent>();
      var expected = obj21.GameObject.GetComponent<TestComponent>();
      Assert.AreSame(expected, result);
    }

    [Test]
    public void RequireThrowsWhenComponentIsOnRootOnly()
    {
      var obj1 = TestGameObjectBuilder
        .Create()
        .Build();
      
      var obj2 = TestGameObjectBuilder
        .Create()
        .Build();
      
      using var root = TestGameObjectBuilder
        .Create()
        .AddChild(obj1)
        .AddChild(obj2)
        .AddComponent<TestComponent>()
        .Build<Transform>();
      
      Assert.Throws<NullReferenceException>(() =>
      {
        root.Component.RequireComponentFromChildren<TestComponent>();
      });
    }

    [Test]
    public void RequireThrowsWhenThereIsNoComponent()
    {
      var obj1 = TestGameObjectBuilder
        .Create()
        .Build();
      
      var obj2 = TestGameObjectBuilder
        .Create()
        .Build();
      
      using var root = TestGameObjectBuilder
        .Create()
        .AddChild(obj1)
        .AddChild(obj2)
        .Build<Transform>();
      
      Assert.Throws<NullReferenceException>(() =>
      {
        root.Component.RequireComponentFromChildren<TestComponent>();
      });
    }

    [Test]
    public void GetReturnsComponentFromChild()
    {
      var obj11 = TestGameObjectBuilder
        .Create()
        .Build();
      
      var obj21 = TestGameObjectBuilder
        .Create()
        .AddComponent<TestComponent>()
        .Build();
      
      var obj1 = TestGameObjectBuilder
        .Create()
        .AddChild(obj11)
        .Build();
      
      var obj2 = TestGameObjectBuilder
        .Create()
        .AddChild(obj21)
        .Build();
      
      using var root = TestGameObjectBuilder
        .Create()
        .AddChild(obj1)
        .AddChild(obj2)
        .Build<Transform>();

      var result = root.Component.GetComponentFromChildren<TestComponent>();
      var expected = obj21.GameObject.GetComponent<TestComponent>();
      Assert.AreSame(expected, result);
    }

    [Test]
    public void GetDoesReturnsNullWhenComponentIsOnRootOnly()
    {
      var obj1 = TestGameObjectBuilder
        .Create()
        .Build();
      
      var obj2 = TestGameObjectBuilder
        .Create()
        .Build();
      
      using var root = TestGameObjectBuilder
        .Create()
        .AddChild(obj1)
        .AddChild(obj2)
        .AddComponent<TestComponent>()
        .Build<Transform>();
      
      Assert.Null(root.Component.GetComponentFromChildren<TestComponent>());
    }

    [Test]
    public void GetReturnsNullWhenThereIsNoComponent()
    {
      var obj1 = TestGameObjectBuilder
        .Create()
        .Build();
      
      var obj2 = TestGameObjectBuilder
        .Create()
        .Build();
      
      using var root = TestGameObjectBuilder
        .Create()
        .AddChild(obj1)
        .AddChild(obj2)
        .Build<Transform>();
      
      Assert.Null(root.Component.GetComponentFromChildren<TestComponent>());
    }
  }
}