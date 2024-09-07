using System;
using com.karabaev.utilities.unity.GameKit.Utils;
using com.karabaev.utilities.unity.playmode.tests;
using com.karabaev.utilities.unity.tests.Editor;
using NUnit.Framework;
using UnityEngine;

namespace com.karabaev.utilities.unity.editor.tests.GameKit
{
  public class FindComponentFromParentsTest
  {
    [Test]
    public void RequireReturnsComponentFromParent()
    {
      var obj11 = TestGameObjectBuilder
        .Create()
        .Build<Transform>();
      
      var obj21 = TestGameObjectBuilder
        .Create()
        .Build<Transform>();
      
      var obj1 = TestGameObjectBuilder
        .Create()
        .AddChild(obj11)
        .Build<Transform>();
      
      var obj2 = TestGameObjectBuilder
        .Create()
        .AddChild(obj21)
        .Build<Transform>();
      
      using var root = TestGameObjectBuilder
        .Create()
        .AddChild(obj1)
        .AddChild(obj2)
        .AddComponent<TestComponent>()
        .Build<Transform>();

      var result = obj11.Component.RequireComponentFromParents<TestComponent>();
      var expected = root.Component.GetComponent<TestComponent>();
      Assert.AreSame(expected, result);
    }
    
    [Test]
    public void RequireThrowsWhenComponentIsOnItselfOnly()
    {
      var obj1 = TestGameObjectBuilder
        .Create()
        .AddComponent<TestComponent>()
        .Build<Transform>();
      
      var obj2 = TestGameObjectBuilder
        .Create()
        .Build<Transform>();
      
      using var root = TestGameObjectBuilder
        .Create()
        .AddChild(obj1)
        .AddChild(obj2)
        .Build<Transform>();
      
      Assert.Throws<NullReferenceException>(() =>
      {
        obj1.Component.RequireComponentFromChildren<TestComponent>();
      });
    }

    [Test]
    public void RequireThrowsWhenThereIsNoComponent()
    {
      var obj1 = TestGameObjectBuilder
        .Create()
        .Build<Transform>();
      
      var obj2 = TestGameObjectBuilder
        .Create()
        .Build<Transform>();
      
      using var root = TestGameObjectBuilder
        .Create()
        .AddChild(obj1)
        .AddChild(obj2)
        .Build<Transform>();
      
      Assert.Throws<NullReferenceException>(() =>
      {
        obj1.Component.RequireComponentFromChildren<TestComponent>();
      });
    }

    [Test]
    public void GetReturnsComponentFromParents()
    {
      var obj11 = TestGameObjectBuilder
        .Create()
        .Build<Transform>();
      
      var obj21 = TestGameObjectBuilder
        .Create()
        .Build<Transform>();
      
      var obj1 = TestGameObjectBuilder
        .Create()
        .AddChild(obj11)
        .Build<Transform>();
      
      var obj2 = TestGameObjectBuilder
        .Create()
        .AddChild(obj21)
        .Build<Transform>();
      
      using var root = TestGameObjectBuilder
        .Create()
        .AddChild(obj1)
        .AddChild(obj2)
        .AddComponent<TestComponent>()
        .Build<Transform>();

      var result = obj11.Component.GetComponentFromParents<TestComponent>();
      var expected = root.Component.GetComponent<TestComponent>();
      Assert.AreSame(expected, result);
    }

    [Test]
    public void GetReturnsComponentFromFirstParent()
    {
      var obj11 = TestGameObjectBuilder
        .Create()
        .Build<Transform>();
      
      var obj21 = TestGameObjectBuilder
        .Create()
        .Build<Transform>();
      
      var obj1 = TestGameObjectBuilder
        .Create()
        .AddChild(obj11)
        .AddComponent<TestComponent>()
        .Build<Transform>();
      
      var obj2 = TestGameObjectBuilder
        .Create()
        .AddChild(obj21)
        .AddComponent<TestComponent>()
        .Build<Transform>();
      
     using var root = TestGameObjectBuilder
        .Create()
        .AddChild(obj1)
        .AddChild(obj2)
        .AddComponent<TestComponent>()
        .Build<Transform>();

      var result = obj11.Component.GetComponentFromParents<TestComponent>();
      var expected = obj1.Component.GetComponent<TestComponent>();
      Assert.AreSame(expected, result);
    }
    
    [Test]
    public void GetDoesReturnsNullWhenComponentIsOnItselfOnly()
    {
      var obj1 = TestGameObjectBuilder
        .Create()
        .AddComponent<TestComponent>()
        .Build<Transform>();
      
      var obj2 = TestGameObjectBuilder
        .Create()
        .Build<Transform>();
      
      using var root = TestGameObjectBuilder
        .Create()
        .AddChild(obj1)
        .AddChild(obj2)
        .Build<Transform>();
      
      Assert.Null(obj1.Component.GetComponentFromChildren<TestComponent>());
    }

    [Test]
    public void GetReturnsNullWhenThereIsNoComponent()
    {
      var obj1 = TestGameObjectBuilder
        .Create()
        .Build<Transform>();
      
      var obj2 = TestGameObjectBuilder
        .Create()
        .Build<Transform>();
      
      using var root = TestGameObjectBuilder
        .Create()
        .AddChild(obj1)
        .AddChild(obj2)
        .Build<Transform>();
      
      Assert.Null(obj1.Component.GetComponentFromChildren<TestComponent>());
    }
  }
}