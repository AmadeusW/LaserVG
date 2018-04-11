using System.Collections.Generic;
using System.Xml.Linq;

namespace Deo.LaserVg.Parts
{
    class Path : IPart
    {
        IList<string> Segments { get; }
        decimal Stroke { get; }

        internal Path(Sketch sketch)
        {
            Segments = new List<string>();
            Segments.Add($"M {sketch.Location}");
            Stroke = sketch.Etching ? sketch.StrokeWidthEtching : sketch.StrokeWidthCutting;
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
                new XAttribute("stroke-width", Stroke),
                string.Join(' ', Segments));
            return node;
        }

        public override string ToString()
        {
            return string.Join(' ', Segments);
        }
    }
}
