using System.Linq;
using com.karabaev.utilities.unity.GameKit;
using com.karabaev.utilities.unity.GameKit.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using GameKitSourceGenerator.Utils;

namespace GameKitSourceGenerator.GenerationUtils
{
  public static class RequiredFromChildGenerationUtils
  {
    public static bool TryGetRequiredFromChildPath(IFieldSymbol symbol, out string? childPath)
    {
      childPath = null;
      
      if (!symbol.IsComponent())
        return false;

      return TryGetRequiredChildPath(symbol, out childPath);
    }
    
    public static bool TryGetRequiredFromChildPath(IPropertySymbol symbol, out string? childPath)
    {
      childPath = null;
      
      if (!symbol.IsComponent())
        return false;

      return TryGetRequiredChildPath(symbol, out childPath);
    }
    
    public static string GetLine(string name, string type, string childPath)
    {
      return $@"      {name} = this.RequireComponentFromChild<{type}>(""{childPath}"");";
    }

    private static bool TryGetRequiredChildPath(ISymbol symbol, out string? childPath)
    {
      childPath = null;
      
      var attributes = symbol.GetAttributes();
      var requiredAttributeName = typeof(RequiredFromChildAttribute).FullName;
      var attributeData = attributes.FirstOrDefault(a => a.AttributeClass?.ToDisplayString() == requiredAttributeName);

      return attributeData.TryGetFirstStringConstructorArg(out childPath);
    }
  }
}