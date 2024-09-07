using System;
using UnityEngine;

namespace com.karabaev.utilities.unity.GameKit.Utils
{
  public static class RequireComponentUtils
  {
    public static T RequireComponent<T>(this Component component) where T : class
    {
      return component.gameObject.RequireComponent<T>();
    }

    public static Component RequireComponent(this Component component, Type componentType)
    {
      return RequireComponent(component.gameObject, componentType);
    }

    public static T RequireComponent<T>(this GameObject gameObject) where T : class
    {
      return (RequireComponent(gameObject, typeof(T)) as T)!;
    }

    public static Component RequireComponent(this GameObject gameObject, Type componentType)
    {
      if (gameObject.TryGetComponent(componentType, out var component))
        return component;

      throw new NullReferenceException($"Specified component was not found on object. Object='{gameObject.name}', ComponentType='{componentType.Name}'");
    }
  }
}