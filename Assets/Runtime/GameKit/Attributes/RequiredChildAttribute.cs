using System;
using JetBrains.Annotations;

namespace com.karabaev.utilities.unity.GameKit.Attributes
{
  [AttributeUsage(AttributeTargets.Field)]
  [MeansImplicitUse(ImplicitUseKindFlags.Assign)]
  public class RequiredChildAttribute : Attribute
  {
    public string ChildPath { get; }

    public RequiredChildAttribute(string childPath) => ChildPath = childPath;
  }
}