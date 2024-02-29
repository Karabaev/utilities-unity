using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace com.karabaev.utilities.unity
{
  [PublicAPI]
  public static class CommonUtils
  {
#region GameObject

    public static T? GetComponentInChildrenOnly<T>(this GameObject gameObject) where T : Component
    {
      foreach(Transform child in gameObject.transform)
      {
        var found = child.GetComponent<T>();
        return found ? found : GetComponentInChildrenOnly<T>(child.gameObject);
      }

      return null;
    }

    public static T RequireComponentInChildrenOnly<T>(this GameObject gameObject) where T : Component
    {
      foreach(Transform child in gameObject.transform)
      {
        var found = child.GetComponentInChildren<T>();

        if(found)
          return found!;
      }

      throw new NullReferenceException($"Component {typeof(T).Name} not found in children of {gameObject.name}");
    }

    public static Component RequireComponent(this GameObject gameObject, Type componentType) =>
      gameObject.GetComponent(componentType) ?? throw new NullReferenceException($"Component {componentType.Name} not found in object {gameObject.name}");

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

#endregion

#region Transform

    public static Transform? GetChildRecursive(this Transform transform, string name)
    {
      foreach(Transform child in transform)
      {
        if(child.name == name)
          return child;

        var found = GetChildRecursive(child, name);
        if(found != null)
          return found;
      }

      return null;
    }

    public static Transform RequireChildRecursive(this Transform transform, string name) =>
      GetChildRecursive(transform, name) ?? throw new NullReferenceException();

    public static T? GetComponentInChild<T>(this Transform transform, string childName) where T : Component
    {
      foreach(Transform child in transform)
      {
        if(child.name == childName)
          return child.GetComponent<T>();

        var found = GetComponentInChild<T>(child, childName);
        if(found)
          return found;
      }

      return null;
    }

    public static T RequireComponentInChild<T>(this Transform transform, string childName) where T : Component =>
      GetComponentInChild<T>(transform, childName) ?? throw new NullReferenceException();

    public static T? GetComponentInChildrenNotRecursive<T>(this Transform transform) where T : Component
    {
      foreach(Transform child in transform)
      {
        var component = child.GetComponent<T>();

        if(component)
          return component;
      }

      return null;
    }

    public static T? GetComponentInChildrenNotRecursive<T>(this Transform transform, string childName) where T : Component
    {
      foreach(Transform child in transform)
      {
        if(child.name == childName)
          return child.GetComponent<T>();
      }

      return null;
    }

    public static List<T> GetComponentsInChildrenNotRecursive<T>(this Transform transform) where T : Component
    {
      var result = new List<T>();
      foreach(Transform child in transform)
      {
        var component = child.GetComponent<T>();

        if(component)
          result.Add(component);
      }

      return result;
    }

    public static RectTransform ToRect(this Transform transform) => (RectTransform)transform;

    public static void AddChild(this Transform transform, Transform child) => child.SetParent(transform, false);

    public static void AddChild(this Transform transform, Component child) => child.transform.SetParent(transform, false);

    public static void AddChild(this Transform transform, GameObject child) => child.transform.SetParent(transform, false);

    public static void ClearChildren(this Transform transform)
    {
      foreach(Transform child in transform)
        Object.Destroy(child.gameObject);
    }

    public static void LookAt2D(this Transform transform, Transform target) => transform.LookAt2D(target.position);

    public static void LookAt2D(this Transform transform, Vector3 target)
    {
      var direction = target - transform.position;
      direction.y = transform.forward.y;
      transform.forward = direction;
    }

#endregion

#region Component

    public static T? GetComponentInChildrenOnly<T>(this Component component) where T : Component =>
      GetComponentInChildrenOnly<T>(component.gameObject);

    public static T RequireComponentInChildrenOnly<T>(this Component component) where T : Component =>
      RequireComponentInChildrenOnly<T>(component.gameObject);

    public static T RequireComponentInChild<T>(this Component component, string childName) where T : Component =>
      GetComponentInChild<T>(component, childName) ??
      throw new NullReferenceException($"Component {typeof(T).Name} not found on child {childName} of object {component.name}");

    public static T? GetComponentInChild<T>(this Component component, string childName) where T : Component =>
      GetComponentInChild<T>(component.transform, childName);

    public static T? GetComponentInSiblings<T>(this Component component) where T : Component
    {
      if(component.transform.parent.AsNullable() == null)
        return null;

      foreach(Transform child in component.transform.parent)
      {
        if(ReferenceEquals(child, component.transform))
          continue;

        var found = child.GetComponent<T>();

        if(found.AsNullable() != null)
          return found;
      }

      return null;
    }

    public static T RequireComponentInSiblings<T>(this Component component) where T : Component =>
      GetComponentInSiblings<T>(component) ?? throw new NullReferenceException($"Component {typeof(T).Name} not found in siblings of object {component.name}");

    public static T RequireComponent<T>(this Component component) where T : class => RequireComponent<T>(component.gameObject);

    public static T RequireComponentInChildren<T>(this Component component, bool includeInactive = true) =>
      component.GetComponentInChildren<T>(includeInactive) ??
      throw new NullReferenceException($"Component {typeof(T).Name} not found in children of object {component.name}");

    public static T RequireComponentInParent<T>(this Component component) where T : class
    {
      var parent = component.transform.parent;

      if(!parent)
        throw new NullReferenceException($"Object {component.name} don't has parent");

      return parent.RequireComponent<T>();
    }

    public static T RequireComponentInParents<T>(this Component component) where T : class
    {
      var parent = component.transform.parent;

      while(parent != null)
      {
        var result = parent.GetComponent<T>();
        
        if(result != null)
        {
          return result;
        }
        
        parent = parent.parent;
      }
      
      throw new NullReferenceException($"Component not found in parents of object. ObjectName={component.name} ComponentType={typeof(T).Name}");
    }

    public static Transform RequireChildRecursive(this Component component, string name) =>
      component.transform.GetChildRecursive(name) ?? throw new NullReferenceException($"Child '{name}' not found in object '{component.name}'");

    public static RectTransform RectTransform(this Component component) => component.transform.ToRect();

    public static void Toggle(this Component component) =>
      component.SetActive(!component.gameObject.activeSelf);

    public static void SetActive(this Component component, bool active) => component.gameObject.SetActive(active);

    public static void DestroyObject(this Component component) => Object.Destroy(component.gameObject);

    public static UniTask DestroyObjectAsync(this Component component) => DestroyObjectAsync(component.gameObject);

    public static void EnsureDestroyTriggerAdded(this Component component) => EnsureDestroyTriggerAdded(component.gameObject);

    public static void CheckSingleComponent<T>(this T component) where T : MonoBehaviour
    {
      var components = component.GetComponents<T>();

      if(components.Length > 1)
        throw new Exception($"There are more than 1 {typeof(T).Name} on {component.name}");
    }

#endregion

#region Vectors

    public static Vector3 To3D(this Vector2 source) => new(source.x, 0, source.y);

#endregion

#region Other

    public static T? AsNullable<T>(this T? reference) where T : class
    {
      if(reference == null || reference.Equals(null))
        return null;

      if(reference is Object unityObject)
        return unityObject ? reference : null;

      return reference;
    }
    
    public static TComponent NewObject<TComponent>(string name, Transform? parent = null) where TComponent : Component
    {
      var obj = new GameObject(name);

      if(parent != null)
        parent.AddChild(obj);

      return obj.AddComponent<TComponent>();
    }

#endregion
  }
}