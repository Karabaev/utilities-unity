using Microsoft.CodeAnalysis;

namespace GameKitSourceGenerator.Utils
{
  public static class AttributeUtils
  {
    public static bool TryGetFirstStringConstructorArg(this AttributeData? attributeData, out string? value)
    {
      if (attributeData == null || attributeData.ConstructorArguments.Length == 0)
      {
        value = null;
        return false;
      }

      value = attributeData.ConstructorArguments[0].Value?.ToString();
      return true;
    } 
  }
}