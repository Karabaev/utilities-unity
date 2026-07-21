using com.karabaev.utilities.unity.GameKit;
using com.karabaev.utilities.unity.GameKit.Attributes;
using UnityEngine;

namespace GameKitSourceGenerator.Examples
{
  public partial class Test1 { }
  
  public partial class Test2 : GameKitComponent { }
  
  public partial class Test3 : GameKitComponent
  {
    [Required] private Transform _field = null!;
  }

  public abstract partial class Test4Abstract : GameKitComponent
  {
    [Required] private Transform _field = null!;
  }
  
  public partial class Test4 : Test4Abstract
  {
    [Required] private Transform _field1 = null!;
  }
  
  public partial class GenericTest<T> : GameKitComponent
  {
    [Required] private Transform _field1 = null!;
  }
  
  public partial class ExampleGameKitComponent : GameKitComponent
  {
    [Required] private Transform _field1 = null!;
    [Required] private Transform Property1 { get; set; } = null!;
    [Required] private GameObject _error1 = null!;
    [Required] private GameObject Error2 { get; set; } = null!;
  
    [RequiredChild("Child123/Child321")] private GameObject _child = null!;
    [RequiredChild("Child123/Child321")] public GameObject ChildProperty { get; private set; } = null!;
    [RequiredChild("Child123/Child321")] private Transform _error3 = null!;
    [RequiredChild("Child123/Child321")] private Transform Error4 { get; set; } = null!;
    
    [RequiredFromChild("Child333/Child555")] private Transform _transformFromChild = null!;
    [RequiredFromChild("Child333/Child555")] private Transform TransformFromChild { get; set; } = null!;
    [RequiredFromChild("Child333/Child555")] private GameObject _error5 = null!;
    [RequiredFromChild("Child333/Child555")] private GameObject Error6 { get; set; } = null!;
    
    [RequiredFromChildren] private Transform _transformFromChildren = null!;
    [RequiredFromChildren] private Transform TransformFromChildren { get; set; } = null!;
    [RequiredFromChildren] private GameObject _error7 = null!;
    [RequiredFromChildren] private GameObject Error8 { get; set; } = null!;
    
    [RequiredFromParents] private Transform _transformFromParents = null!;
    [RequiredFromParents] private Transform TransformFromParents { get; set; } = null!;
    [RequiredFromParents] private GameObject _error9 = null!;
    [RequiredFromParents] private GameObject Error10 { get; set; } = null!;
  }
}