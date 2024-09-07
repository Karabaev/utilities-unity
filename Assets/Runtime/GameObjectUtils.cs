using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace com.karabaev.utilities.unity
{
  public static class GameObjectUtils
  {
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