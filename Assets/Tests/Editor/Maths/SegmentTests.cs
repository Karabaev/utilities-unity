using com.karabaev.utilities.unity.Maths;
using NUnit.Framework;
using UnityEngine;

namespace com.karabaev.utilities.unity.editor.tests.Maths
{
  [TestFixture]
  public class SegmentTests
  {
    [Test]
    public void GetIntersectionReturnsNullWhenSegmentsAreParallel()
    {
      var segment1 = new Segment2(new Vector2(1.0f, 1.0f), new Vector2(1.0f, 5.0f));
      var segment2 = new Segment2(new Vector2(2.0f, 1.0f), new Vector2(2.0f, 10.0f));
      var intersection = Segment2.GetIntersection(segment1, segment2);
      Assert.IsNull(intersection);
    }

    [Test]
    public void GetIntersectionReturnsNullWhenSegmentsAreSame()
    {
      var segment1 = new Segment2(new Vector2(1.0f, 1.0f), new Vector2(1.0f, 5.0f));
      var segment2 = new Segment2(new Vector2(1.0f, 1.0f), new Vector2(1.0f, 5.0f));
      var intersection = Segment2.GetIntersection(segment1, segment2);
      Assert.IsNull(intersection);
    }

    [Test]
    public void GetIntersectionReturnsStartPointWhenSegmentsHaveSameStartPoint()
    {
      var startPoint = new Vector2(1.0f, 1.0f);
      var segment1 = new Segment2(startPoint, new Vector2(1.0f, 5.0f));
      var segment2 = new Segment2(startPoint, new Vector2(5.0f, 8.0f));
      var intersection = Segment2.GetIntersection(segment1, segment2);
      Assert.AreEqual(startPoint, intersection);
    }

    [Test]
    public void GetIntersectionReturnsEndPointWhenSegmentsHaveSameEndPoint()
    {
      var segment1 = new Segment2(new Vector2(1.0f, 1.0f), new Vector2(1.0f, 5.0f));
      var segment2 = new Segment2(new Vector2(99.0f, 99.0f), new Vector2(1.0f, 5.0f));
      var intersection = Segment2.GetIntersection(segment1, segment2);
      Assert.AreEqual(new Vector2(1.0f, 5.0f), intersection);
    }

    private static object[] _getIntersectionTestCases =
    {
      new object[]
      {
        new Vector2(0, 0), new Vector2(1, 1), // segment1
        new Vector2(1, 0), new Vector2(0, 1), // segment2
        new Vector2(0.5f, 0.5f)
      }, // expectedIntersection
      new object[]
      {
        new Vector2(1, 1), new Vector2(1, 100), // segment1
        new Vector2(-50, 50), new Vector2(50, 50), // segment2
        new Vector2(1, 50)
      }, // expectedIntersection
      new object[]
      {
        new Vector2(-50, -50), new Vector2(50, 50), // segment1
        new Vector2(-50, 50), new Vector2(50, -50), // segment2
        new Vector2(0, 0)
      }, // expectedIntersection
    };

    [TestCaseSource(nameof(_getIntersectionTestCases))]
    public void GetIntersectionTests(Vector2 segment1Start, Vector2 segment1End,
      Vector2 segment2Start, Vector2 segment2End,
      Vector2? expectedIntersection)
    {
      var segment1 = new Segment2(segment1Start, segment1End);
      var segment2 = new Segment2(segment2Start, segment2End);
      var intersection = Segment2.GetIntersection(segment1, segment2);
      Assert.AreEqual(expectedIntersection, intersection);
    }
  }
}