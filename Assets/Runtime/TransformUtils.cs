using UnityEngine;
using Object = UnityEngine.Object;

namespace com.karabaev.utilities.unity
{
  public static class TransformUtils
  {
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
  }
}