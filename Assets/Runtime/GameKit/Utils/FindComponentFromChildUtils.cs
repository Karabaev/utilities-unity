using System;
using UnityEngine;

namespace com.karabaev.utilities.unity.GameKit.Utils
{
  public static class FindComponentFromChildUtils
  {
    public static T RequireComponentFromChild<T>(this Component component, string childPath) where T : class
    {
      return component.gameObject.RequireComponentFromChild<T>(childPath);
    }

    public static Component RequireComponentFromChild(this Component component, string childPath, Type componentType)
    {
      return component.gameObject.RequireComponentFromChild(childPath, componentType);
    }
    
    public static T? GetComponentFromChild<T>(this Component component, string childPath) where T : class
    {
      return component.gameObject.GetComponentFromChild<T>(childPath);
    } 
    
    public static T RequireComponentFromChild<T>(this GameObject gameObject, string childPath) where T : class
    {
      return (gameObject.RequireComponentFromChild(childPath, typeof(T)) as T)!;
    }
    
    public static Component RequireComponentFromChild(this GameObject gameObject, string childPath, Type componentType)
    {
      var result = gameObject.GetComponentFromChild(childPath, componentType);
      if (result != null)
        return result;

      throw new NullReferenceException($"Component is not found in child. " +
                                       $"Object='{gameObject.name}', Child='{childPath}', ComponentType='{componentType.Name}'");
    }
    
    public static T? GetComponentFromChild<T>(this GameObject gameObject, string childPath) where T : class
    {
      return gameObject.GetComponentFromChild(childPath, typeof(T)) as T;
    }

    public static Component? GetComponentFromChild(this GameObject gameObject, string childPath, Type componentType)
    {
      var pathParts = childPath.Split('/');
      return GetComponentFromChildRecursive(pathParts, 0, gameObject, componentType);
    }

    private static Component? GetComponentFromChildRecursive(string[] pathParts, int depth, GameObject currentNode,
      Type componentType)
    {
      if (depth >= pathParts.Length)
        return currentNode.GetComponent(componentType);

      var name = pathParts[depth];

      foreach (Transform child in currentNode.transform)
      {
        if (child.name == name)
        {
          return GetComponentFromChildRecursive(pathParts, depth + 1, child.gameObject, componentType);
        }
      }
      
      return null;
    }
  }
}