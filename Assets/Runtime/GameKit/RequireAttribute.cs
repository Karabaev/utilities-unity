using System;
using JetBrains.Annotations;

namespace com.karabaev.utilities.unity
{
  [AttributeUsage(AttributeTargets.Field)]
  [MeansImplicitUse(ImplicitUseKindFlags.Assign)]
  public class RequireAttribute : Attribute { }

  [AttributeUsage(AttributeTargets.Field)]
  [MeansImplicitUse(ImplicitUseKindFlags.Assign)]
  public class RequireInChildrenAttribute : Attribute { }
  
  [AttributeUsage(AttributeTargets.Field)]
  public class RequireInChildrenOnlyAttribute : Attribute { }

  [AttributeUsage(AttributeTargets.Field)]
  public class RequireInChildAttribute : Attribute
  {
    public string Child { get; }

    public RequireInChildAttribute(string child) => Child = child;
  }
  
  [AttributeUsage(AttributeTargets.Field)]
  public class RequireChildRecursiveAttribute : Attribute
  {
    public string Child { get; }

    public RequireChildRecursiveAttribute(string child) => Child = child;
  }
}