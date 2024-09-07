using UnityEngine;

namespace com.karabaev.utilities.unity
{
  public interface IGameObjectFactory
  {
    GameObject Create(GameObject template, Transform parent);

    T Create<T>(T template, Transform parent) where T : Component;

    void Dispose(GameObject gameObject);
  }
}