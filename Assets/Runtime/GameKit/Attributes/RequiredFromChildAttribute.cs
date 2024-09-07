using System;
using JetBrains.Annotations;

namespace com.karabaev.utilities.unity.GameKit.Attributes
{
  [AttributeUsage(AttributeTargets.Field)]
  [MeansImplicitUse(ImplicitUseKindFlags.Assign)]
  public class RequiredFromChildAttribute : Attribute
  {
    public string ChildPath { get; }

    public RequiredFromChildAttribute(string childPath) => ChildPath = childPath;
  }
}