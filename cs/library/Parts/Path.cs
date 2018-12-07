using System;
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
        /// Adds a straight line segment to a position relative to the current cursor position.
        /// </summary>
        /// <param name="delta">Displacement</param>
        internal void AddLine(Point delta)
        {
            Segments.Add($"l {delta}");
        }

        /// <summary>
        /// Adds a straight line segment from the current cursor position to a target position.
        /// </summary>
        /// <param name="scaled">Target position</param>
        internal void AddLineTo(Point point)
        {
            Segments.Add($"L {point}");
        }

        public XElement ToXml()
        {
            var node = new XElement("path",
                new XAttribute("stroke-width", Stroke),
                new XAttribute("d", string.Join(' ', Segments)),
                new XAttribute("fill", "none"),
                new XAttribute("stroke", "#000")
            );
            return node;
        }

        public override string ToString()
        {
            return string.Join(' ', Segments);
        }
    }
}
