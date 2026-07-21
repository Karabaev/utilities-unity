using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GameKitSourceGenerator.Utils
{
  public static class CommonUtils
  {
    public static ITypeSymbol RequireSymbol(this BaseTypeDeclarationSyntax syntax, GeneratorSyntaxContext context)
    {
      return context.SemanticModel.GetDeclaredSymbol(syntax)!;
    }

    public static IFieldSymbol RequireSymbol(this FieldDeclarationSyntax syntax, GeneratorSyntaxContext context)
    {
      return (IFieldSymbol) context.SemanticModel.GetDeclaredSymbol(syntax.Declaration.Variables.First())!;
    }
    
    public static IPropertySymbol RequireSymbol(this PropertyDeclarationSyntax syntax, GeneratorSyntaxContext context)
    {
      return context.SemanticModel.GetDeclaredSymbol(syntax)!;
    }

    public static string RequireName(this FieldDeclarationSyntax syntax)
    {
      return syntax.Declaration.Variables.First().Identifier.Text;
    }
    
    public static IFieldSymbol RequireSymbol(this FieldDeclarationSyntax field,
      GeneratorExecutionContext context)
    {
      return (IFieldSymbol) context.Compilation
        .GetSemanticModel(field.SyntaxTree)
        .GetDeclaredSymbol(field.Declaration.Variables.First())!;
    }
    
    public static IPropertySymbol RequireSymbol(this PropertyDeclarationSyntax property,
      GeneratorExecutionContext context)
    {
      return context.Compilation
        .GetSemanticModel(property.SyntaxTree)
        .GetDeclaredSymbol(property)!;
    }
    
    public static bool HasAttribute<T>(this ISymbol symbol) where T : Attribute
    {
      var attributes = symbol.GetAttributes();
      var requiredAttributeName = typeof(T).FullName;
      return attributes.Any(a => a.AttributeClass?.ToDisplayString() == requiredAttributeName);
    }
  }
}