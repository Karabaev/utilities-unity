using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using com.karabaev.utilities.unity.GameKit;
using GameKitSourceGenerator.GenerationUtils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using GameKitSourceGenerator.Utils;

namespace GameKitSourceGenerator
{
  [Generator]
  public class Generator : IIncrementalGenerator
  {
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
      if (!System.Diagnostics.Debugger.IsAttached)
        System.Diagnostics.Debugger.Launch();

      var provider = context.SyntaxProvider
        .CreateSyntaxProvider(
          (n, _) => n is ClassDeclarationSyntax,
          CalculateTypesInfo)
        .Where(t => t != null)
        .Select((t, _) => t!);
      
      context.RegisterSourceOutput(provider, GenerateCode);
    }
    
    private GeneratorTypeInfo? CalculateTypesInfo(GeneratorSyntaxContext context, CancellationToken _)
    {
      var typeSyntax = (ClassDeclarationSyntax)context.Node;
      var typeSymbol = typeSyntax.RequireSymbol(context);
      
      if (!typeSymbol.Inherits<GameKitComponent>())
        return null;

      var requiredMembers = new List<(string name, string type)>();
      var requiredChildMembers = new List<(string name, string childPath)>();
      var requiredFromChildMembers = new List<(string name, string type, string childPath)>();
      var requiredFromChildrenMembers = new List<(string name, string type)>();
      var requiredFromParentsMembers = new List<(string name, string type)>();
      
      foreach (var memberSyntax in typeSyntax.Members)
      {
        if (memberSyntax is FieldDeclarationSyntax fieldSyntax)
        {
          var fieldSymbol = fieldSyntax.RequireSymbol(context);
          if (RequiredGenerationUtils.IsRequired(fieldSymbol))
          {
            requiredMembers.Add((fieldSyntax.RequireName(), fieldSymbol.Type.ToDisplayString()));
          } else if (RequiredChildGenerationUtils.TryGetRequiredChildPath(fieldSymbol, out var childPath))
          {
            requiredChildMembers.Add((fieldSyntax.RequireName(), childPath!));
          } else if (RequiredFromChildGenerationUtils.TryGetRequiredFromChildPath(fieldSymbol, out childPath))
          {
            requiredFromChildMembers.Add((fieldSyntax.RequireName(), fieldSymbol.Type.ToDisplayString(), childPath!));
          } else if (RequiredFromChildrenGenerationUtils.IsRequiredFromChildren(fieldSymbol))
          {
            requiredFromChildrenMembers.Add((fieldSyntax.RequireName(), fieldSymbol.Type.ToDisplayString()));
          } else if (RequiredFromParentsGenerationUtils.IsRequiredFromParents(fieldSymbol))
          {
            requiredFromParentsMembers.Add((fieldSyntax.RequireName(), fieldSymbol.Type.ToDisplayString()));
          }
          continue;
        }
        
        if (memberSyntax is PropertyDeclarationSyntax propertySyntax)
        {
          var propertySymbol = propertySyntax.RequireSymbol(context);
          if (RequiredGenerationUtils.IsRequired(propertySymbol))
          {
            requiredMembers.Add((propertySyntax.Identifier.Text, propertySymbol.Type.ToDisplayString()));
          } else if (RequiredChildGenerationUtils.TryGetRequiredChildPath(propertySymbol, out var childPath))
          {
            requiredChildMembers.Add((propertySyntax.Identifier.Text, childPath!));
          } else if (RequiredFromChildGenerationUtils.TryGetRequiredFromChildPath(propertySymbol, out childPath))
          {
            requiredFromChildMembers.Add((propertySyntax.Identifier.Text, propertySymbol.Type.ToDisplayString(), childPath!));
          } else if (RequiredFromChildrenGenerationUtils.IsRequiredFromChildren(propertySymbol))
          {
            requiredFromChildrenMembers.Add((propertySyntax.Identifier.Text, propertySymbol.Type.ToDisplayString()));
          } else if (RequiredFromParentsGenerationUtils.IsRequiredFromParents(propertySymbol))
          {
            requiredFromParentsMembers.Add((propertySyntax.Identifier.Text, propertySymbol.Type.ToDisplayString()));
          }
        }
      }

      var typeNamespace = typeSymbol.ContainingNamespace.IsGlobalNamespace
        ? null
        : typeSymbol.ContainingNamespace.ToString();
      return new GeneratorTypeInfo
      {
        TypeName = typeSymbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
        FileName = typeSyntax.Identifier.Text,
        NameSpace = typeNamespace,
        DirectlyInheritsGameKitComponent = typeSymbol.DirectlyInherits<GameKitComponent>(),
        RequiredMembers = requiredMembers,
        RequiredChildMembers = requiredChildMembers,
        RequiredFromChildMembers = requiredFromChildMembers,
        RequiredFromChildrenMembers = requiredFromChildrenMembers,
        RequiredFromParentsMembers = requiredFromParentsMembers
      };
    }
    
    private void GenerateCode(SourceProductionContext context, GeneratorTypeInfo typeInfo)
    {
      using var sourceStream = new MemoryStream();
      using var writer = new StreamWriter(sourceStream, Encoding.UTF8);

      writer.WriteLine("using com.karabaev.utilities.unity.GameKit.Utils;");
      writer.WriteLine();
      writer.WriteLine($"namespace {typeInfo.NameSpace}");
      writer.WriteLine("{");
      writer.WriteLine($"  public partial class {typeInfo.TypeName}");
      writer.WriteLine("  {");
      writer.WriteLine("    protected override void InitializeReferences()");
      writer.WriteLine("    {");
      
      if (!typeInfo.DirectlyInheritsGameKitComponent)
      {
        writer.WriteLine("      base.InitializeReferences();");
      }
      
      foreach (var (name, type) in typeInfo.RequiredMembers)
        writer.WriteLine(RequiredGenerationUtils.GetLine(name, type));
      
      foreach (var (name, childPath) in typeInfo.RequiredChildMembers)
        writer.WriteLine(RequiredChildGenerationUtils.GetLine(name, childPath));
      
      foreach (var (name, type, childPath) in typeInfo.RequiredFromChildMembers)
        writer.WriteLine(RequiredFromChildGenerationUtils.GetLine(name, type, childPath));
      
      foreach (var (name, type) in typeInfo.RequiredFromChildrenMembers)
        writer.WriteLine(RequiredFromChildrenGenerationUtils.GetLine(name, type));
      
      foreach (var (name, type) in typeInfo.RequiredFromParentsMembers)
        writer.WriteLine(RequiredFromParentsGenerationUtils.GetLine(name, type));
      
      writer.WriteLine("    }"); // method
      writer.WriteLine("  }"); // type
      writer.WriteLine("}"); // namespace

      writer.Flush();
      var sourceText = SourceText.From(sourceStream, Encoding.UTF8, canBeEmbedded: true);
      context.AddSource($"{typeInfo.FileName}_Initialization.g.cs", sourceText);
    }
  }
}