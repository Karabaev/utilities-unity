using System;
using com.karabaev.utilities.unity.GameKit;
using com.karabaev.utilities.unity.GameKit.Utils;
using NUnit.Framework;

namespace com.karabaev.utilities.unity.editor.tests.GameKit.FindChild
{
  public class RequiredChildTests
  {
    private readonly FindChildTestGameObjects _gameObjects = new();
    
    [SetUp]
    public void SetUp() => _gameObjects.SetUp();

    [TearDown]
    public void TearDown() => _gameObjects.TearDown();

    [Test]
    public void FindsChildByPath()
    {
      var actual = _gameObjects.Root1.RequireChild($"{_gameObjects.L1_1.name}/{_gameObjects.L2_1.name}");
      Assert.AreSame(_gameObjects.L2_1.gameObject, actual);
    }

    [Test]
    public void FindsL1ChildByName()
    {
      var actual = _gameObjects.Root1.RequireChild($"{_gameObjects.L1_1.name}");
      Assert.AreSame(_gameObjects.L1_1.gameObject, actual);
    }

    [Test]
    public void DoesNotFindL2ChildByName()
    {
      Assert.Throws<NullReferenceException>(() =>
      {
        _gameObjects.Root1.RequireChild($"{_gameObjects.L2_1.name}");
      });
    }
    
    [Test]
    public void DoesNotFindNotExistingChildByPath()
    {
      Assert.Throws<NullReferenceException>(() =>
      {
        _gameObjects.Root1.RequireChild($"{_gameObjects.L1_1.name}/{_gameObjects.Root2.name}");
      });
      Assert.Throws<NullReferenceException>(() =>
      {
        _gameObjects.Root2.RequireChild($"{_gameObjects.Root1.name}");
      });
    }
  }
}