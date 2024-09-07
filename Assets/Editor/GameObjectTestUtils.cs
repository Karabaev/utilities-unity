using com.karabaev.utilities.unity.GameKit;
using com.karabaev.utilities.unity.GameKit.Utils;
using UnityEngine;

namespace com.karabaev.utilities.unity.tests.Editor
{
  public static class GameObjectTestUtils
  {
    public static void Awake(this MonoBehaviour component) => component.TryCallPrivateMethod("Awake");
    public static void Start(this MonoBehaviour component) => component.TryCallPrivateMethod("Start");
    public static void Update(this MonoBehaviour component) => component.TryCallPrivateMethod("Update");
    
    public static void OnDestroy(this MonoBehaviour component) => component.TryCallPrivateMethod("OnDestroy");

    public static void Awake(this GameObject gameObject)
    {
      foreach (var child in gameObject.GetComponentsInChildren<MonoBehaviour>())
        child.Awake();
    }
    
    public static void Start(this GameObject gameObject)
    {
      foreach (var child in gameObject.GetComponentsInChildren<MonoBehaviour>())
        child.Start();
    }
    
    public static void Update(this GameObject gameObject)
    {
      foreach (var child in gameObject.GetComponentsInChildren<MonoBehaviour>())
        child.Update();
    }
    
    public static void OnDestroy(this GameObject gameObject)
    {
      foreach (var child in gameObject.GetComponentsInChildren<MonoBehaviour>())
        child.OnDestroy();
    }

    public static bool ContainsObject<T>(this GameObject gameObject, string name) where T : MonoBehaviour
    {
      return gameObject.transform.GetComponentFromChild<T>(name) != null;
    }
  }
}