using System.Linq;
using com.karabaev.utilities.unity.GameKit;
using com.karabaev.utilities.unity.GameKit.Attributes;
using Microsoft.CodeAnalysis;
using GameKitSourceGenerator.Utils;

namespace GameKitSourceGenerator.GenerationUtils
{
  public static class RequiredChildGenerationUtils
  {
    public static bool TryGetRequiredChildPath(IFieldSymbol symbol, out string? childPath)
    {
      childPath = null;
      
      if (!symbol.IsGameObject())
        return false;

      return TryGetRequiredChildPath((ISymbol) symbol, out childPath);
    }
    
    public static bool TryGetRequiredChildPath(IPropertySymbol symbol, out string? childPath)
    {
      childPath = null;
      
      if (!symbol.IsGameObject())
        return false;

      return TryGetRequiredChildPath((ISymbol) symbol, out childPath);
    }
    
    public static string GetLine(string name, string childPath)
    {
      return @$"      {name} = this.RequireChild(""{childPath}"");";
    }

    private static bool TryGetRequiredChildPath(ISymbol symbol, out string? childPath)
    {
      childPath = null;
      
      var attributes = symbol.GetAttributes();
      var requiredAttributeName = typeof(RequiredChildAttribute).FullName;
      var attributeData = attributes.FirstOrDefault(a => a.AttributeClass?.ToDisplayString() == requiredAttributeName);
      
      return attributeData.TryGetFirstStringConstructorArg(out childPath);
    }
  }
}