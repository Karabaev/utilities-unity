using UnityEngine;

namespace com.karabaev.utilities.unity.GameKit
{
  public abstract class GameKitComponent : MonoBehaviour
  {
    private void Awake()
    {
      InitializeReferences();
      OnAwake();
    }

    protected abstract void InitializeReferences();

    protected virtual void OnAwake() { }
  }
}