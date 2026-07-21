using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using UnityEngine;

namespace GameKitSourceGenerator.Utils
{
  public static class TypeUtils
  {
    public static bool IsPartial(this BaseTypeDeclarationSyntax syntax)
    {
      return syntax.Modifiers.Any(SyntaxKind.PartialKeyword);
    }
    
    public static bool IsAbstract(this BaseTypeDeclarationSyntax syntax)
    {
      return syntax.Modifiers.Any(SyntaxKind.AbstractKeyword);
    }
    
    public static bool IsType<T>(this ITypeSymbol symbol)
    {
      return symbol.ToDisplayString() == typeof(T).FullName;
    }
    
    public static bool Inherits<T>(this ITypeSymbol type)
    {
      var requiredType = typeof(T).FullName;
      var baseType = type.BaseType;
      while (baseType != null)
      {
        if (baseType.ToDisplayString() == requiredType)
          return true;

        baseType = baseType.BaseType;
      }

      return false;
    }
    
    public static bool DirectlyInherits<T>(this ITypeSymbol type)
    {
      return type.BaseType?.ToDisplayString() == typeof(T).FullName;
    }

    public static bool IsType(this ITypeSymbol symbol, string typeFullName)
    {
      return symbol.ToDisplayString() == typeFullName;
    }
    
    public static bool Inherits(this ITypeSymbol type, string typeFullName)
    {
      var baseType = type.BaseType;
      while (baseType != null)
      {
        if (baseType.ToDisplayString() == typeFullName)
          return true;

        baseType = baseType.BaseType;
      }

      return false;
    }

    public static bool IsGameObject(this IFieldSymbol symbol) => symbol.Type.IsType<GameObject>();
    
    public static bool IsGameObject(this IPropertySymbol symbol) => symbol.Type.IsType<GameObject>();
    
    public static bool IsComponent(this IFieldSymbol symbol) => symbol.Type.Inherits<Component>();
    
    public static bool IsComponent(this IPropertySymbol symbol) => symbol.Type.Inherits<Component>();
  }
}