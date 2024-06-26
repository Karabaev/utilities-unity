using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace com.karabaev.utilities.unity
{
  [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
  public static class RandomUtils
  {
    private const string PossibleChars = "abcdefghijklmnopqrstuvwxyz0123456789";

    public static string RandomString(int num = 10)
    {
      var result = new char[num];
      
      while(num-- > 0)
        result[num] = PossibleChars[UnityEngine.Random.Range(0, PossibleChars.Length)];

      return new string(result);
    }
    
    public static bool Random(float probability)
    {
      if(probability is < 0.0f or > 1.0f)
        throw new ArgumentOutOfRangeException(nameof(probability), "Probability must be between 0 and 1");
      
      return UnityEngine.Random.value <= probability;
    }

    public static int PickRandomByWeights(IEnumerable<int> weights)
    {
      var totalWeight = weights.Sum();
      var probe = UnityEngine.Random.value;
      var s = 0f;

      var i = 0;
      foreach(var weight in weights)
      {
        s += (float)weight / totalWeight;
        if(s >= probe)
          return i;

        i++;
      }

      return -1;
    }

    public static int PickRandomByProbes(IEnumerable<float> probes)
    {
      var total = probes.Sum();
      var randomPoint = UnityEngine.Random.value * total;

      var i = 0;
      foreach(var probe in probes)
      {
        if(randomPoint < probe)
        {
          return i;
        }

        randomPoint -= probe;
        i++;
      }

      return -1;
    }

    /// <summary>
    /// https://stackoverflow.com/questions/48087/select-n-random-elements-from-a-listt-in-c-sharp
    /// </summary>
    public static IEnumerable<T> PickRandomCollection<T>(this IEnumerable<T> initialCollection, int pickCount)
    {
      var available = initialCollection.Count();
      var needed = pickCount;

      foreach(var item in initialCollection)
      {
        if(UnityEngine.Random.Range(0, available) < needed) // random.Next(available) < needed
        {
          needed--;
          yield return item;

          if(needed == 0)
            break;
        }

        available--;
      }
    }

    public static T PickRandom<T>(this List<T> collection)
    {
      var index = UnityEngine.Random.Range(0, collection.Count);
      return collection[index];
    }

    public static T PickRandom<T>(this IReadOnlyList<T> collection)
    {
      var index = UnityEngine.Random.Range(0, collection.Count);
      return collection[index];
    }

    public static T PickRandom<T>(this IEnumerable<T> collection)
    {
      var index = UnityEngine.Random.Range(0, collection.Count());
      return collection.ElementAt(index);
    }

    public static T PickRandom<T>(this T[] collection)
    {
      var index = UnityEngine.Random.Range(0, collection.Length);
      return collection[index];
    }
    
    public static Vector3 GetRandomPointOnCircle(float minRadius, float maxRadius)
    {
      var spawnDirection = UnityEngine.Random.onUnitSphere;
      spawnDirection.y = 0;
      spawnDirection.Normalize();
      var radius = UnityEngine.Random.Range(minRadius, maxRadius);
      return spawnDirection * radius;
    }
    
    public static void Shuffle<T>(this IList<T> collection, ref Random random)
    {
      for(var i = collection.Count - 1; i > 0; i--)
      {
        var randomColumn = random.NextInt(collection.Count);
        (collection[i], collection[randomColumn]) = (collection[randomColumn], collection[i]);
      }
    }

    public static Random Create() => new((uint)Guid.NewGuid().GetHashCode());
  }
}