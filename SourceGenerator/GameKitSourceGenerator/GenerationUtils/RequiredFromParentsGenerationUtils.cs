using com.karabaev.utilities.unity.GameKit;
using com.karabaev.utilities.unity.GameKit.Attributes;
using Microsoft.CodeAnalysis;
using GameKitSourceGenerator.Utils;

namespace GameKitSourceGenerator.GenerationUtils
{
  public static class RequiredFromParentsGenerationUtils
  {
    public static bool IsRequiredFromParents(IFieldSymbol symbol)
    {
      return symbol.IsComponent() && symbol.HasAttribute<RequiredFromParentsAttribute>();
    }
    
    public static bool IsRequiredFromParents(IPropertySymbol symbol)
    {
      if (!symbol.IsComponent())
        return false;

      return symbol.IsComponent() && symbol.HasAttribute<RequiredFromParentsAttribute>();
    }

    public static string GetLine(string name, string type)
    {
      return $@"      {name} = this.RequireComponentFromParents<{type}>();";
    }
  }
}