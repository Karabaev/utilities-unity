using System;
using System.Reflection;
using UnityEngine;

namespace com.karabaev.utilities.unity.GameKit
{
  public abstract class GameKitComponent : MonoBehaviour
  {
    private void OnValidate()
    {
      var fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

      foreach(var field in fields)
      {
        HandleRequire(field);
        HandleRequireInChildren(field);
        HandleRequireInChildrenOnly(field);
        HandleRequireInChild(field);
        HandleRequireChildRecursive(field);
      }
      
      OnValidateInternal();
    }

    protected virtual void OnValidateInternal() { }

    private void HandleRequire(FieldInfo field)
    {
      var attribute = field.GetCustomAttribute<RequireAttribute>();
      if(attribute == null)
        return;
      field.SetValue(this, gameObject.RequireComponent(field.FieldType));
    }

    private void HandleRequireInChildren(FieldInfo field)
    {
      var attribute = field.GetCustomAttribute<RequireInChildrenAttribute>();
      if(attribute == null)
        return;
      field.SetValue(this, this.RequireComponentInChildren(field.FieldType));
    }

    private void HandleRequireInChildrenOnly(FieldInfo field)
    {
      var attribute = field.GetCustomAttribute<RequireInChildrenOnlyAttribute>();
      if(attribute == null)
        return;
      field.SetValue(this, gameObject.RequireComponentInChildrenOnly(field.FieldType));
    }

    private void HandleRequireInChild(FieldInfo field)
    {
      var attribute = field.GetCustomAttribute<RequireInChildAttribute>();
      if(attribute == null)
        return;
      field.SetValue(this, this.RequireComponentInChild(attribute.Child, field.FieldType));
    }

    private void HandleRequireChildRecursive(FieldInfo field)
    {
      var attribute = field.GetCustomAttribute<RequireChildRecursiveAttribute>();
      if(attribute == null)
        return;
      if(field.FieldType != typeof(GameObject))
        throw new InvalidOperationException($"Field type is not GameObject. Component={GetType().Name}, Field={field.Name}");
      field.SetValue(this, this.RequireChildRecursive(attribute.Child));
    }
  }
}