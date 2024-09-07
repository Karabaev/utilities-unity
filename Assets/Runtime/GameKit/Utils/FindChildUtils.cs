using System;
using UnityEngine;

namespace com.karabaev.utilities.unity.GameKit.Utils
{
  public static class FindChildUtils
  {
    public static GameObject RequireChild(this Component component, string path)
    {
      return component.GetChild(path) ??
             throw new NullReferenceException($"Child is not found in object. Object='{component.name}' ChildPath='{path}'");
    }

    public static GameObject? GetChild(this Component component, string path)
    {
      var pathParts = path.Split('/');
      return GetChildRecursive(pathParts, 0, component.gameObject);
    }

    private static GameObject? GetChildRecursive(string[] pathParts, int depth, GameObject currentNode)
    {
      if (depth >= pathParts.Length)
        return currentNode;

      var name = pathParts[depth];

      foreach (Transform child in currentNode.transform)
      {
        if (child.name == name)
        {
          return GetChildRecursive(pathParts, depth + 1, child.gameObject);
        }
      }

      return null;
    }
  }
}