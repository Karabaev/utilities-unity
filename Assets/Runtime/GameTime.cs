using System;
using System.Globalization;
using JetBrains.Annotations;
using UnityEngine;

namespace com.karabaev.utilities.unity
{
    [PublicAPI]
    public readonly struct GameTime
    {
        public static readonly GameTime Min = new(float.MinValue);
        public static readonly GameTime Max = new(float.MaxValue);

        public readonly float Seconds;

        public static GameTime Now => new(Time.time);

        public GameTime AddSeconds(in float seconds) => new(Seconds + seconds);

        public GameTime AddSeconds(in double seconds) => new(Seconds + (float) seconds);

        public GameTime Add(in TimeSpan timeSpan) => new((float) (Seconds + timeSpan.TotalSeconds));

        public static GameTime FromSeconds(in float seconds) => new(in seconds);

        public static GameTime FromTimeSpan(in TimeSpan timeSpan) => new((float) timeSpan.TotalSeconds);

        public override string ToString() => Seconds.ToString(CultureInfo.InvariantCulture);

        public bool Equals(GameTime other) => this == other;

        public override bool Equals(object? obj) => obj is GameTime other && this == other;

        public override int GetHashCode() => Seconds.GetHashCode();

        public static bool operator ==(in GameTime a, in GameTime b) => Mathf.Abs(a.Seconds - b.Seconds) < Mathf.Epsilon;

        public static bool operator !=(in GameTime a, in GameTime b) => !(a == b);

        public static bool operator >(in GameTime a, in GameTime b) => a.Seconds > b.Seconds;

        public static bool operator <(in GameTime a, in GameTime b) => a.Seconds < b.Seconds;

        public static bool operator >=(in GameTime a, in GameTime b) => a.Seconds >= b.Seconds;

        public static bool operator <=(in GameTime a, in GameTime b) => a.Seconds <= b.Seconds;

        public static GameTime operator -(in GameTime a, in GameTime b) => new(a.Seconds - b.Seconds);

        public static GameTime operator +(in GameTime a, in GameTime b) => new(a.Seconds + b.Seconds);

        private GameTime(in float seconds) => Seconds = seconds;
    }
}