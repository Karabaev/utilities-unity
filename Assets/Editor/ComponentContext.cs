using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace com.karabaev.utilities.unity.tests.Editor
{
  public class ComponentContext<T> : IDisposable where T : Component
  {
    public readonly T Component;

    public ComponentContext(T component) => Component = component;

    public void Dispose() => Object.DestroyImmediate(Component.gameObject);
    
    public static implicit operator T(ComponentContext<T> context) => context.Component;
  }
}