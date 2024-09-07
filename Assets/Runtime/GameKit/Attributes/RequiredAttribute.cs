﻿using System;
using JetBrains.Annotations;

namespace com.karabaev.utilities.unity.GameKit.Attributes
{
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
  [MeansImplicitUse(ImplicitUseKindFlags.Assign)]
  public class RequiredAttribute : Attribute { }
}