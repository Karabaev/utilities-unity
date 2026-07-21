using System.Collections.Generic;

namespace GameKitSourceGenerator
{
  public class GeneratorTypeInfo
  {
    public string TypeName = null!;
    public string FileName = null!;
    public bool DirectlyInheritsGameKitComponent;
    public string? NameSpace;
    public List<(string name, string type)> RequiredMembers = null!;
    public List<(string name, string childPath)> RequiredChildMembers = null!;
    public List<(string name, string type, string childPath)> RequiredFromChildMembers = null!;
    public List<(string name, string type)> RequiredFromChildrenMembers = null!;
    public List<(string name, string type)> RequiredFromParentsMembers = null!;
  }
}