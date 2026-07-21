using com.karabaev.utilities.unity.GameKit.Attributes;
using Microsoft.CodeAnalysis;
using GameKitSourceGenerator.Utils;

namespace GameKitSourceGenerator.GenerationUtils
{
  public static class RequiredGenerationUtils
  {
    public static bool IsRequired(IFieldSymbol symbol)
    {
      return symbol.HasAttribute<RequiredAttribute>();
    }
    
    public static bool IsRequired(IPropertySymbol symbol)
    {
      return symbol.HasAttribute<RequiredAttribute>();
    }

    public static string GetLine(string name, string type)
    {
      return $"      {name} = this.RequireComponent<{type}>();";
    }
  }
}