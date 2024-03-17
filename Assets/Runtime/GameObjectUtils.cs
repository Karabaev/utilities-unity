using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace com.karabaev.utilities.unity
{
  public static class GameObjectUtils
  {
    public static T? GetComponentInChildrenOnly<T>(this GameObject gameObject) where T : Component
    {
      foreach(Transform child in gameObject.transform)
      {
        var found = child.GetComponent<T>();
        return found ? found : GetComponentInChildrenOnly<T>(child.gameObject);
      }

      return null;
    }

    public static T RequireComponentInChildrenOnly<T>(this GameObject gameObject) where T : Component =>
      (T)RequireComponentInChildrenOnly(gameObject, typeof(T));

    public static Component RequireComponentInChildrenOnly(this GameObject gameObject, Type componentType)
    {
      foreach(Transform child in gameObject.transform)
      {
        var found = child.GetComponentInChildren(componentType);

        if(found)
          return found!;
      }

      throw new NullReferenceException($"Specified component was not found in children. ComponentType={componentType.Name}, Object={gameObject.name}");
    }

    public static Component RequireComponent(this GameObject gameObject, Type componentType)
    {
      return gameObject.GetComponent(componentType) 
     ?? throw new NullReferenceException($"Specified component was not found on object. ComponentType={componentType.Name}, Object={gameObject.name}");
    }

    public static T RequireComponent<T>(this GameObject gameObject) where T : class => (RequireComponent(gameObject, typeof(T)) as T)!;

    public static void DestroyObject(this GameObject gameObject) => Object.Destroy(gameObject);

    public static UniTask DestroyObjectAsync(this GameObject gameObject)
    {
      var trigger = gameObject.GetAsyncDestroyTrigger();
      Object.Destroy(gameObject);
      return trigger.OnDestroyAsync();
    }

    public static void EnsureDestroyTriggerAdded(this GameObject gameObject) => gameObject.GetAsyncDestroyTrigger();

    public static void AddChild(this GameObject gameObject, GameObject child) => child.transform.SetParent(gameObject.transform, false);
    
    public static void AddChild(this GameObject gameObject, Transform child) => child.SetParent(gameObject.transform, false);
    
    public static void AddChild(this GameObject gameObject, Component child) => child.transform.SetParent(gameObject.transform, false);
  }
}