using System;
using UnityEngine;

namespace com.karabaev.utilities.unity.GameKit.Utils
{
  public static class FindComponentFromParentsUtils
  {
    public static T RequireComponentFromParents<T>(this Component component) where T : class
    {
      return component.gameObject.RequireComponentFromParents<T>();
    }

    public static T? GetComponentFromParents<T>(this Component component) where T : class
    {
      return component.gameObject.GetComponentFromParents<T>();
    }
    
    public static T RequireComponentFromParents<T>(this GameObject gameObject) where T : class
    {
      return (gameObject.RequireComponentFromParents(typeof(T)) as T)!;
    }
    
    public static T? GetComponentFromParents<T>(this GameObject gameObject) where T : class
    {
      return gameObject.GetComponentFromParents(typeof(T)) as T;
    }
    
    public static Component RequireComponentFromParents(this GameObject gameObject, Type componentType)
    {
      var result = gameObject.GetComponentFromParents(componentType);
      if (result != null)
        return result;

      throw new NullReferenceException($"Component is not found in parents. Object='{gameObject.name}', ComponentType='{componentType.Name}'");
    }
    
    public static Component? GetComponentFromParents(this GameObject gameObject, Type componentType)
    {
      return GetComponentFromParentsRecursive(gameObject.transform.parent, componentType);
    }

    private static Component? GetComponentFromParentsRecursive(Transform? transform, Type componentType)
    {
      if (transform == null)
        return null;

      if (transform.TryGetComponent(componentType, out var foundComponent))
        return foundComponent;
      
      return GetComponentFromParentsRecursive(transform.parent, componentType);
    }
  }
}