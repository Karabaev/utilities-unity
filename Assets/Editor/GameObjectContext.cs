using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace com.karabaev.utilities.unity.tests.Editor
{
  public class GameObjectContext : IDisposable
  {
    public readonly GameObject GameObject;

    public GameObjectContext(GameObject gameObject) => GameObject = gameObject;

    public void Dispose() => Object.DestroyImmediate(GameObject);
    
    public static implicit operator GameObject(GameObjectContext context) => context.GameObject;
  }
}