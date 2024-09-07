using System;
using com.karabaev.utilities.unity.GameKit;
using com.karabaev.utilities.unity.GameKit.Utils;
using com.karabaev.utilities.unity.playmode.tests;
using NUnit.Framework;

namespace com.karabaev.utilities.unity.editor.tests.GameKit.FindComponentFromChild
{
  public class FindComponentFromChildFromComponentTests
  {
    private readonly FindComponentFromChildTestGameObjects _testGameObjects = new();
    
    [SetUp]
    public void SetUp() => _testGameObjects.SetUp();

    [TearDown]
    public void TearDown() => _testGameObjects.TearDown();

    [Test]
    public void GetReturnsFromChild()
    {
      var path = $"{_testGameObjects.L1_1.name}/" +
                 $"{_testGameObjects.L2_1_WithComponent.name}";
      var result = _testGameObjects.Root1.GetComponentFromChild<TestComponent>(path);
      var expected = _testGameObjects.L2_1_WithComponent.GetComponent<TestComponent>();
      
      Assert.AreSame(expected, result);
    }

    [Test]
    public void RequireReturnsFromChild()
    {
      var path = $"{_testGameObjects.L1_1.name}/" +
                 $"{_testGameObjects.L2_1_WithComponent.name}";
      var result = _testGameObjects.Root1.RequireComponentFromChild<TestComponent>(path);
      var expected = _testGameObjects.L2_1_WithComponent.GetComponent<TestComponent>();
      
      Assert.AreSame(expected, result);
    }
    
    [Test]
    public void GetDoesNotReturnFromChildWhereThereIsNoComponent()
    {
      var path = $"{_testGameObjects.L1_1.name}/" +
                 $"{_testGameObjects.L2_1_WithComponent.name}/" +
                 $"{_testGameObjects.L3_1.name}";
      var result = _testGameObjects.Root1.GetComponentFromChild<TestComponent>(path);
      Assert.Null(result);
    }
    
    [Test]
    public void RequireDoesNotReturnFromChildWhereThereIsNoComponent()
    {
      var path = $"{_testGameObjects.L1_1.name}/" +
                 $"{_testGameObjects.L2_1_WithComponent.name}/" +
                 $"{_testGameObjects.L3_1.name}";
      
      Assert.Throws<NullReferenceException>(() =>
      {
        _testGameObjects.Root1.RequireComponentFromChild<TestComponent>(path);
      });
    }

    [Test]
    public void GetDoesNotReturnFromRoot()
    {
      var result = _testGameObjects.Root2_WithComponent
        .GetComponentFromChild<TestComponent>($"{_testGameObjects.Root2_WithComponent.name}");
      Assert.Null(result);
    }
    
    [Test]
    public void RequireDoesNotReturnFromRoot()
    {
      Assert.Throws<NullReferenceException>(() =>
      {
        _testGameObjects.Root2_WithComponent
          .RequireComponentFromChild<TestComponent>($"{_testGameObjects.Root2_WithComponent.name}");
      });
    }
  }
}