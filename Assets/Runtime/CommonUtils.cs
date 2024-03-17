using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace com.karabaev.utilities.unity
{
  [PublicAPI]
  public static class CommonUtils
  {
    public static Vector3 To3D(this Vector2 source) => new(source.x, 0, source.y);

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
  }
}