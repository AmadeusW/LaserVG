using System.Collections.Generic;
using System.Xml.Linq;

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

        public XElement ToXml()
        {
            var node = new XElement(XName.Get("line"),
                "line", string.Join(' ', Segments));
            return node;
        }
    }
}
