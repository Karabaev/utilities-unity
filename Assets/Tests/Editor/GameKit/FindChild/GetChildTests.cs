using com.karabaev.utilities.unity.GameKit;
using com.karabaev.utilities.unity.GameKit.Utils;
using NUnit.Framework;

namespace com.karabaev.utilities.unity.editor.tests.GameKit.FindChild
{
  public class GetChildTests
  {
    private readonly FindChildTestGameObjects _gameObjects = new();
    
    [SetUp]
    public void SetUp() => _gameObjects.SetUp();

    [TearDown]
    public void TearDown() => _gameObjects.TearDown();

    [Test]
    public void FindsChildByPath()
    {
      var actual = _gameObjects.Root1.GetChild($"{_gameObjects.L1_1.name}/{_gameObjects.L2_1.name}");
      Assert.AreSame(_gameObjects.L2_1.gameObject, actual);
    }

    [Test]
    public void FindsL1ChildByName()
    {
      var actual = _gameObjects.Root1.GetChild($"{_gameObjects.L1_1.name}");
      Assert.AreSame(_gameObjects.L1_1.gameObject, actual);
    }

    [Test]
    public void DoesNotFindL2ChildByName()
    {
      var actual = _gameObjects.Root1.GetChild($"{_gameObjects.L2_1.name}");
      Assert.Null(actual);
    }
    
    [Test]
    public void DoesNotFindNotExistingChildByPath()
    {
      var actual = _gameObjects.Root1.GetChild($"{_gameObjects.L1_1.name}/{_gameObjects.Root2.name}");
      Assert.Null(actual);
      
      actual = _gameObjects.Root2.GetChild($"{_gameObjects.Root1.name}");
      Assert.Null(actual);
    }
  }
}