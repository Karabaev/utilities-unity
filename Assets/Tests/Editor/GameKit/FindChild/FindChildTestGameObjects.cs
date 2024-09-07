using com.karabaev.utilities.unity.tests.Editor;
using UnityEngine;

namespace com.karabaev.utilities.unity.editor.tests.GameKit.FindChild
{
  public class FindChildTestGameObjects
  {
    public Transform Root1 = null!;
    public Transform Root2 = null!;
    public Transform L1_1 = null!;
    public Transform L1_2 = null!;
    public Transform L2_1 = null!;
    
    public void SetUp()
    {
      L2_1 = TestGameObjectBuilder
        .Create("l2_1")
        .Build<Transform>();
      
      L1_1 = TestGameObjectBuilder
        .Create("l1_1")
        .AddChild(L2_1)
        .Build<Transform>();
      
      L1_2 = TestGameObjectBuilder
        .Create("l1_2")
        .Build<Transform>();
      
      Root1 = TestGameObjectBuilder
        .Create("Root")
        .AddChild(L1_1)
        .AddChild(L1_2)
        .Build<Transform>();
      
      Root2 = TestGameObjectBuilder
        .Create("Root2")
        .Build<Transform>();
    }

    public void TearDown()
    {
      Object.DestroyImmediate(Root1.gameObject);
      Object.DestroyImmediate(Root2.gameObject);
    }
  }
}