using System;
using com.karabaev.utilities.unity.GameKit;
using com.karabaev.utilities.unity.GameKit.Utils;
using com.karabaev.utilities.unity.playmode.tests;
using com.karabaev.utilities.unity.tests.Editor;
using NUnit.Framework;
using UnityEngine;

namespace com.karabaev.utilities.unity.editor.tests.GameKit.RequireComponent
{
  public class RequireComponentTests
  {
    [Test]
    public void GenericRequireReturnsComponentFromGameObject()
    {
      using var gameObject = TestGameObjectBuilder
        .Create()
        .AddComponent<TestComponent>()
        .Build();

      var actual = gameObject.GameObject.RequireComponent<TestComponent>();
      var expected = gameObject.GameObject.GetComponent<TestComponent>();
      
      Assert.AreSame(expected, actual);
    }

    [Test]
    public void GenericRequireDoesNotReturnNonExistingFromGameObject()
    {
      using var gameObject = TestGameObjectBuilder
        .Create()
        .Build();

      Assert.Throws<NullReferenceException>(() =>
      {
        gameObject.GameObject.RequireComponent<TestComponent>();
      });
    }

    [Test]
    public void GenericRequireReturnsInterfaceFromGameObject()
    {
      using var gameObject = TestGameObjectBuilder
        .Create()
        .AddComponent<TestComponent>()
        .Build();
      
      var actual = gameObject.GameObject.RequireComponent<ITestComponent>();
      var expected = gameObject.GameObject.GetComponent<TestComponent>();
      Assert.AreSame(expected, actual);
    }

    [Test]
    public void GenericRequireDoesNotReturnNonExistingInterfaceFromGameObject()
    {
      using var gameObject = TestGameObjectBuilder
        .Create()
        .Build();

      Assert.Throws<NullReferenceException>(() =>
      {
        gameObject.GameObject.RequireComponent<ITestComponent>();
      });
    }
    
    [Test]
    public void NonGenericRequireReturnsComponentFromGameObject()
    {
      using var gameObject = TestGameObjectBuilder
        .Create()
        .AddComponent<TestComponent>()
        .Build();
      
      var actual = gameObject.GameObject.RequireComponent(typeof(TestComponent));
      var expected = gameObject.GameObject.GetComponent<TestComponent>();
      Assert.AreSame(expected, actual);
    }

    [Test]
    public void NonGenericRequireDoesNotReturnNonExistingFromGameObject()
    {
      using var gameObject = TestGameObjectBuilder
        .Create()
        .Build();

      Assert.Throws<NullReferenceException>(() =>
      {
        gameObject.GameObject.RequireComponent(typeof(TestComponent));
      });
    }
    
    [Test]
    public void NonGenericRequireReturnsInterfaceFromGameObject()
    {
      using var gameObject = TestGameObjectBuilder
        .Create()
        .AddComponent<TestComponent>()
        .Build();
      
      var actual = gameObject.GameObject.RequireComponent(typeof(ITestComponent));
      var expected = gameObject.GameObject.GetComponent<TestComponent>();
      Assert.AreSame(expected, actual);
    }
    
    [Test]
    public void NonGenericRequireDoesNotReturnNonExistingInterfaceFromGameObject()
    {
      using var gameObject = TestGameObjectBuilder
        .Create()
        .Build();

      Assert.Throws<NullReferenceException>(() =>
      {
        gameObject.GameObject.RequireComponent(typeof(ITestComponent));
      });
    }
    
    [Test]
    public void GenericRequireReturnsComponentFromComponent()
    {
      using var transform = TestGameObjectBuilder
        .Create()
        .AddComponent<TestComponent>()
        .Build<Transform>();

      var actual = transform.Component.RequireComponent<TestComponent>();
      var expected = transform.Component.GetComponent<TestComponent>();
      
      Assert.AreSame(expected, actual);
    }

    [Test]
    public void GenericRequireDoesNotReturnNonExistingFromComponent()
    {
      using var transform = TestGameObjectBuilder
        .Create()
        .Build<Transform>();

      Assert.Throws<NullReferenceException>(() =>
      {
        transform.Component.RequireComponent<TestComponent>();
      });
    }
    
    [Test]
    public void GenericRequireReturnsInterfaceFromComponent()
    {
      using var transform = TestGameObjectBuilder
        .Create()
        .AddComponent<TestComponent>()
        .Build<Transform>();
      
      var actual = transform.Component.RequireComponent<ITestComponent>();
      var expected = transform.Component.GetComponent<TestComponent>();
      Assert.AreSame(expected, actual);
    }

    [Test]
    public void GenericRequireDoesNotReturnNonExistingInterfaceFromComponent()
    {
      using var transform = TestGameObjectBuilder
        .Create()
        .Build<Transform>();

      Assert.Throws<NullReferenceException>(() =>
      {
        transform.Component.RequireComponent<ITestComponent>();
      });
    }
    
    [Test]
    public void NonGenericRequireReturnsComponentFromComponent()
    {
      using var transform = TestGameObjectBuilder
        .Create()
        .AddComponent<TestComponent>()
        .Build<Transform>();
      
      var actual = transform.Component.RequireComponent(typeof(TestComponent));
      var expected = transform.Component.GetComponent<TestComponent>();
      Assert.AreSame(expected, actual);
    }

    [Test]
    public void NonGenericRequireDoesNotReturnNonExistingFromComponent()
    {
      using var transform = TestGameObjectBuilder
        .Create()
        .Build<Transform>();

      Assert.Throws<NullReferenceException>(() =>
      {
        transform.Component.RequireComponent(typeof(TestComponent));
      });
    }
    
    [Test]
    public void NonGenericRequireReturnsInterfaceFromComponent()
    {
      using var transform = TestGameObjectBuilder
        .Create()
        .AddComponent<TestComponent>()
        .Build<Transform>();
      
      var actual = transform.Component.RequireComponent(typeof(ITestComponent));
      var expected = transform.Component.GetComponent<TestComponent>();
      Assert.AreSame(expected, actual);
    }
    
    [Test]
    public void NonGenericRequireDoesNotReturnNonExistingInterfaceFromComponent()
    {
      using var transform = TestGameObjectBuilder
        .Create()
        .Build<Transform>();

      Assert.Throws<NullReferenceException>(() =>
      {
        transform.Component.RequireComponent(typeof(ITestComponent));
      });
    }
  }
}