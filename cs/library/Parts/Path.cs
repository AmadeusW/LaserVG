using System.Collections.Generic;
using System.Xml.Linq;

namespace Deo.LaserVg.Parts
{
    class Path : IPart
    {
        IList<string> Segments { get; }

        internal Path(Point origin)
        {
            Segments = new List<string>();
            Segments.Add($"M {origin}");
        }

        /// <summary>
        /// Adds a straight line segment
        /// </summary>
        /// <param name="delta"></param>
        internal void AddLine(Point delta)
        {
            Segments.Add($"L {delta}");
        }

        public XElement ToXml()
        {
            var node = new XElement("path",
                string.Join(' ', Segments));
            return node;
        }

        public override string ToString()
        {
            return string.Join(' ', Segments);
        }
    }
}
