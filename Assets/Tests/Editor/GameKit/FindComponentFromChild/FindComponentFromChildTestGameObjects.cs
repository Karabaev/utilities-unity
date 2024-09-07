using com.karabaev.utilities.unity.playmode.tests;
using com.karabaev.utilities.unity.tests.Editor;
using UnityEngine;

namespace com.karabaev.utilities.unity.editor.tests.GameKit.FindComponentFromChild
{
  public class FindComponentFromChildTestGameObjects
  {
    public Transform Root1 = null!;
    public Transform Root2_WithComponent = null!;
    public Transform L1_1 = null!;
    public Transform L1_2 = null!;
    public Transform L2_1_WithComponent = null!;
    public Transform L3_1 = null!;
    
    public void SetUp()
    {
      L3_1 = TestGameObjectBuilder
        .Create("l3_1")
        .Build<Transform>();
      
      L2_1_WithComponent = TestGameObjectBuilder
        .Create("l2_1")
        .AddComponent<TestComponent>()
        .AddChild(L3_1)
        .Build<Transform>();
      
      L1_1 = TestGameObjectBuilder
        .Create("l1_1")
        .AddChild(L2_1_WithComponent)
        .Build<Transform>();
      
      L1_2 = TestGameObjectBuilder
        .Create("l1_2")
        .Build<Transform>();
      
      Root1 = TestGameObjectBuilder
        .Create("Root")
        .AddChild(L1_1)
        .AddChild(L1_2)
        .Build<Transform>();
      
      Root2_WithComponent = TestGameObjectBuilder
        .Create("Root2")
        .AddComponent<TestComponent>()
        .Build<Transform>();
    }

    public void TearDown()
    {
      Object.DestroyImmediate(Root1.gameObject);
      Object.DestroyImmediate(Root2_WithComponent.gameObject);
    }
  }
}