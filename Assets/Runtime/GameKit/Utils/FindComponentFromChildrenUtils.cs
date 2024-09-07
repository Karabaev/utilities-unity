using System;
using UnityEngine;

namespace com.karabaev.utilities.unity.GameKit.Utils
{
  public static class FindComponentFromChildrenUtils
  {
    public static T RequireComponentFromChildren<T>(this Component component) where T : class
    {
      return component.gameObject.RequireComponentFromChildren<T>();
    }
    
    public static T? GetComponentFromChildren<T>(this Component component) where T : class
    {
      return component.gameObject.GetComponentFromChildren<T>();
    }
    
    public static T RequireComponentFromChildren<T>(this GameObject gameObject) where T : class
    {
      return (gameObject.RequireComponentFromChildren(typeof(T)) as T)!;
    }
    
    public static T? GetComponentFromChildren<T>(this GameObject gameObject) where T : class
    {
      return gameObject.GetComponentFromChildren(typeof(T)) as T;
    }
    
    public static Component RequireComponentFromChildren(this GameObject gameObject, Type componentType)
    {
      var result = gameObject.GetComponentFromChildren(componentType);
      if (result != null)
        return result;

      throw new NullReferenceException($"Component is not found in children. Object='{gameObject.name}', ComponentType='{componentType.Name}'");
    }
    
    public static Component? GetComponentFromChildren(this GameObject gameObject, Type componentType)
    {
      return GetComponentFromChildrenRecursive(gameObject, componentType);
    }

    private static Component? GetComponentFromChildrenRecursive(GameObject gameObject, Type componentType)
    {
      foreach (Transform child in gameObject.transform)
      {
        if (child.TryGetComponent(componentType, out var foundComponent))
          return foundComponent;

        foundComponent = GetComponentFromChildrenRecursive(child.gameObject, componentType);
        if (foundComponent)
          return foundComponent;
      }

      return null;
    }
  }
}