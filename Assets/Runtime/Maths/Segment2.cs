using UnityEngine;

namespace com.karabaev.utilities.unity.Maths
{
  public readonly struct Segment2
  {
    private readonly Vector2 _start;
    private readonly Vector2 _end;

    public static Vector2? GetIntersection(Segment2 a, Segment2 b)
    {
      var denominator = (b._end.y - b._start.y) * (a._end.x - a._start.x) -
        (b._end.x - b._start.x) * (a._end.y - a._start.y);

      // Отрезки параллельны или совпадают
      if(denominator == 0)
        return null;

      var ua = ((b._end.x - b._start.x) * (a._start.y - b._start.y) - (b._end.y - b._start.y) * (a._start.x - b._start.x)) / denominator;
      var ub = ((a._end.x - a._start.x) * (a._start.y - b._start.y) - (a._end.y - a._start.y) * (a._start.x - b._start.x)) / denominator;

      // Точка пересечения находится за пределами отрезков
      if(ua is < 0 or > 1 || ub is < 0 or > 1)
        return null;

      // Точка пересечения находится на обоих отрезках
      var x = a._start.x + ua * (a._end.x - a._start.x);
      var y = a._start.y + ua * (a._end.y - a._start.y);
      return new Vector2(x, y);
    }

    public Segment2(Vector2 start, Vector2 end)
    {
      _start = start;
      _end = end;
    }
  }
}