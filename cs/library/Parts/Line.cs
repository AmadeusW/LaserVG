using System.Collections.Generic;

namespace Deo.LaserVg.Parts
{
    class Line : IPart
    {
        IList<string> Segments { get; }

        internal Line(Point origin)
        {
            Segments = new List<string>();
            Segments.Add($"M {origin}");
        }

        /// <summary>
        /// Adds a straight line segment
        /// </summary>
        /// <param name="delta"></param>
        internal void AddSegment(Point delta)
        {
            Segments.Add($"L {delta}");
        }

        public object Serialize()
        {
            return string.Join(' ', Segments);
        }
    }
}
