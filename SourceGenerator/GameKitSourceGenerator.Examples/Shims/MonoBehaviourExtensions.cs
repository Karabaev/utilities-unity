using UnityEngine;

// ReSharper disable once CheckNamespace
namespace com.karabaev.utilities.unity.GameKit.Utils
{
  public static class MonoBehaviourExtensions
  {
    public static T RequireComponent<T>(this MonoBehaviour component) where T : new()
    {
      Console.WriteLine("RequireComponent");
      return new T();
    }

    public static GameObject RequireChild(this Component component, string childPath)
    {
      return new GameObject();
    }
    
    public static T RequireComponentFromChild<T>(this Component component, string childPath) where T : Component, new()
    {
      return new T();
    }
    
    public static T RequireComponentFromChildren<T>(this Component component) where T : Component, new()
    {
      return new T();
    }
    
    public static T RequireComponentFromParents<T>(this Component component) where T : Component, new()
    {
      return new T();
    }
  }
}

