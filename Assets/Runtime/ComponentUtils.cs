using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace com.karabaev.utilities.unity
{
  public static class ComponentUtils
  {
    public static RectTransform RectTransform(this Component component) => component.transform.ToRect();

    public static void Toggle(this Component component) => component.SetActive(!component.gameObject.activeSelf);

    public static void SetActive(this Component component, bool active) => component.gameObject.SetActive(active);

    public static void DestroyObject(this Component component) => Object.Destroy(component.gameObject);

    public static UniTask DestroyObjectAsync(this Component component) => component.gameObject.DestroyObjectAsync();

    public static void EnsureDestroyTriggerAdded(this Component component) => component.gameObject.EnsureDestroyTriggerAdded();
  }
}