using System;
using System.Collections.Generic;
using com.karabaev.utilities.unity.GameKit.Utils;
using UnityEngine;

namespace com.karabaev.utilities.unity.tests.Editor
{
  public class TestGameObjectBuilder
  {
    private string _gameObjectName = null!;
    private readonly List<Type> _components = new();
    private readonly List<GameObject> _createdChildren = new();
    private readonly List<(string, Type)> _childrenInfo = new();

    public static TestGameObjectBuilder Create(string name = "")
    {
      var builder = new TestGameObjectBuilder
      {
        _gameObjectName = name
      };
      return builder;
    }
    
    public TestGameObjectBuilder AddComponent<T>() where T : Component
    {
      _components.Add(typeof(T));
      return this;
    }

    public TestGameObjectBuilder AddChild(GameObject child)
    {
      _createdChildren.Add(child);
      return this;
    }
    
    public TestGameObjectBuilder AddChild(Transform child) => AddChild(child.gameObject);

    public TestGameObjectBuilder AddChild<T>(string childName)
    {
      _childrenInfo.Add((childName, typeof(T)));
      return this;
    }
    
    public GameObjectContext Build()
    {
      var result = new GameObject(_gameObjectName, _components.ToArray());
      _createdChildren.ForEach(c => result.AddChild(c));
      foreach (var (childName, childComponent) in _childrenInfo)
      {
        var child = new GameObject(childName, childComponent);
        result.AddChild(child);
      }

      return new GameObjectContext(result);
    }

    public ComponentContext<T> Build<T>() where T : Component
    {
      var gameObjectContext = Build();
      return new ComponentContext<T>(gameObjectContext.GameObject.RequireComponent<T>());
    }
  }
}