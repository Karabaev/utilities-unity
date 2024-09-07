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

    private void OnValidate()
    {
      InitializeReferences();
      OnValidateInternal();
    }

    protected abstract void InitializeReferences();

    protected virtual void OnAwake() { }
    
    protected virtual void OnValidateInternal() { }
  }
}