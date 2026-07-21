using com.karabaev.utilities.unity.GameKit;
using com.karabaev.utilities.unity.GameKit.Attributes;
using Microsoft.CodeAnalysis;
using GameKitSourceGenerator.Utils;

namespace GameKitSourceGenerator.GenerationUtils
{
  public static class RequiredFromChildrenGenerationUtils
  {
    public static bool IsRequiredFromChildren(IFieldSymbol symbol)
    {
      return symbol.IsComponent() && symbol.HasAttribute<RequiredFromChildrenAttribute>();
    }
    
    public static bool IsRequiredFromChildren(IPropertySymbol symbol)
    {
      return symbol.IsComponent() && symbol.HasAttribute<RequiredFromChildrenAttribute>();
    }

    public static string GetLine(string name, string type)
    {
      return $@"      {name} = this.RequireComponentFromChildren<{type}>();";
    }
  }
}