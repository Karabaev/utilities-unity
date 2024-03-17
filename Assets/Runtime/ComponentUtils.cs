using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace com.karabaev.utilities.unity
{
  public static class ComponentUtils
  {
    public static T? GetComponentInChildrenOnly<T>(this Component component) where T : Component =>
      component.gameObject.GetComponentInChildrenOnly<T>();

    public static T RequireComponentInChildrenOnly<T>(this Component component) where T : Component =>
      component.gameObject.RequireComponentInChildrenOnly<T>();

    public static T RequireComponentInChild<T>(this Component component, string childName) where T : Component =>
      GetComponentInChild<T>(component, childName) ??
      throw new NullReferenceException($"Component {typeof(T).Name} not found on child {childName} of object {component.name}");

    public static Component RequireComponentInChild(this Component component, string childName, Type componentType) =>
      GetComponentInChild(component, childName, componentType) ??
      throw new NullReferenceException($"Specified component was not found in child. Object={component.gameObject.name}, Child={childName}, ComponentType={componentType.Name}");
    
    public static T? GetComponentInChild<T>(this Component component, string childName) where T : Component =>
      (T?)component.GetComponentInChild(childName, typeof(T));

    public static Component? GetComponentInChild(this Component component, string childName, Type componentType)
    {
      foreach(Transform child in component.transform)
      {
        if(child.name == childName)
          return child.GetComponent(componentType);

        var found = GetComponentInChild(child, childName, componentType);
        if(found)
          return found;
      }

      return null;
    }

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

    public static T RequireComponent<T>(this Component component) where T : class => component.gameObject.RequireComponent<T>();

    public static T RequireComponentInChildren<T>(this Component component, bool includeInactive = true) where T : Component =>
      (T)RequireComponentInChildren(component, typeof(T), includeInactive);

    public static Component RequireComponentInChildren(this Component component, Type componentType, bool includeInactive = true)
    {
      return component.GetComponentInChildren(componentType, includeInactive) ??
        throw new NullReferenceException($"Specified component was not found in children. ComponentType={componentType.Name}, Object={component.gameObject.name}");
    }

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

    public static GameObject RequireChildRecursive(this Component component, string name) =>
      component.GetChildRecursive(name) ?? throw new NullReferenceException($"Child '{name}' not found in object '{component.name}'");

    public static GameObject? GetChildRecursive(this Component component, string name)
    {
      foreach(Transform child in component.transform)
      {
        if(child.name == name)
          return child.gameObject;

        var found = GetChildRecursive(child, name);
        if(found != null)
          return found;
      }

      return null;
    }
    
    public static RectTransform RectTransform(this Component component) => component.transform.ToRect();

    public static void Toggle(this Component component) =>
      component.SetActive(!component.gameObject.activeSelf);

    public static void SetActive(this Component component, bool active) => component.gameObject.SetActive(active);

    public static void DestroyObject(this Component component) => Object.Destroy(component.gameObject);

    public static UniTask DestroyObjectAsync(this Component component) => component.gameObject.DestroyObjectAsync();

    public static void EnsureDestroyTriggerAdded(this Component component) => component.gameObject.EnsureDestroyTriggerAdded();

    public static void CheckSingleComponent<T>(this T component) where T : MonoBehaviour
    {
      var components = component.GetComponents<T>();

      if(components.Length > 1)
        throw new Exception($"There are more than 1 {typeof(T).Name} on {component.name}");
    }
    
    public static T? GetComponentInChildrenNotRecursive<T>(this Component component) where T : Component
    {
      foreach(Transform child in component.transform)
      {
        var found = child.GetComponent<T>();

        if(found)
          return found;
      }

      return null;
    }

    public static T? GetComponentInChildrenNotRecursive<T>(this Component component, string childName) where T : Component
    {
      foreach(Transform child in component.transform)
      {
        if(child.name == childName)
          return child.GetComponent<T>();
      }

      return null;
    }

    public static IReadOnlyList<T> GetComponentsInChildrenNotRecursive<T>(this Component component) where T : Component
    {
      var result = new List<T>();
      foreach(Transform child in component.transform)
      {
        var found = child.GetComponent<T>();

        if(found)
          result.Add(found);
      }

      return result;
    }
  }
}