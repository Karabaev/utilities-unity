using System;
using JetBrains.Annotations;

namespace com.karabaev.utilities.unity.GameKit.Attributes
{
  [AttributeUsage(AttributeTargets.Field)]
  [MeansImplicitUse(ImplicitUseKindFlags.Assign)]
  public class RequiredFromChildrenAttribute : Attribute { }
}