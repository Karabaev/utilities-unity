using System.Collections.Generic;
using com.karabaev.utilities.unity.GameKit;
using com.karabaev.utilities.unity.GameKit.Utils;
using UnityEngine;

namespace com.karabaev.utilities.unity.tests.Editor
{
  public class EditorModeTestGameObjectFactory : IGameObjectFactory
  {
    public readonly Dictionary<GameObject, List<GameObject>> CreatedObjects = new();

    public GameObject Create(GameObject template, Transform parent)
    {
      var result = Object.Instantiate(template, parent);
      result.Awake();

      if (!CreatedObjects.TryGetValue(template, out var createdObjects))
      {
        createdObjects = new List<GameObject>();
        CreatedObjects.Add(template, createdObjects);
      }
      
      createdObjects.Add(result);
      return result;
    }

    public T Create<T>(T template, Transform parent) where T : Component
    {
      return Create(template.gameObject, parent).RequireComponent<T>();
    }

    public void Dispose(GameObject gameObject)
    {
      gameObject.OnDestroy();
      
      foreach (var (_, createdObjects) in CreatedObjects)
      {
        if (createdObjects.Remove(gameObject))
          break;
      }
      
      Object.DestroyImmediate(gameObject);
    }
  }
}