using System;
using System.Collections.Generic;

namespace Deo.LaserVg
{
    public class Sketch
    {
        /// <summary>
        /// Current location of the laser when drawing lines
        /// </summary>
        public Point Location { get; private set; }

        /// <summary>
        /// Whether laser is etching or cutting
        /// </summary>
        public bool Etching { get; set; }

        /// <summary>
        /// Container of all parts known so far
        /// </summary>
        private IList<IPart> Parts;

        /// <summary>
        /// Currently built line
        /// </summary>
        private Parts.Line LineBuilder;

        /// <summary>
        /// Create a new sketch. This instance builds up information about shapes to cut.
        /// </summary>
        public Sketch()
        {
            Parts = new List<IPart>();
            Location = (0, 0);
        }
        
        /// <summary>
        /// Saves the sketch as SVG file.
        /// </summary>
        /// <param name="fileName"></param>
        public void Save(string fileName)
        {
            TryFinishLine();
            SvgExporter.Export(Parts, fileName);
        }

        /// <summary>
        /// Inserts raw svg markup
        /// </summary>
        /// <param name="svg">Markup to insert verbatim</param>
        public void Raw(string svg)
        {
            TryFinishLine();
            Parts.Add(new Parts.Raw(svg));
        }

        public Point MoveTo(decimal x, decimal y) => MoveTo((x, y));
        public Point MoveTo((decimal x, decimal y) point)
        {
            TryFinishLine();

            Location = point;
            return Location;
        }

        public Point Move(decimal x, decimal y) => Move((x, y));
        public Point Move((decimal x, decimal y) delta)
        {
            TryFinishLine();

            Location = (Location.X + delta.x, Location.Y + delta.y);
            return Location;
        }

        public Point LineTo(decimal x, decimal y) => LineTo((x, y));
        public Point LineTo((decimal x, decimal y) point)
        {
            TryStartLine();

            var delta = point - Location;
            LineBuilder.AddSegment(delta);

            Location = point;
            return Location;
        }

        public Point Line(decimal x, decimal y) => Line((x, y));
        public Point Line((decimal x, decimal y) delta)
        {
            TryStartLine();

            LineBuilder.AddSegment(delta);
            Location = (Location.X + delta.x, Location.Y + delta.y);
            return Location;
        }

        /// <summary>
        /// Attempts to create a new line.
        /// Does nothing if a line is already being built.
        /// </summary>
        private void TryStartLine()
        {
            if (LineBuilder == null)
                LineBuilder = new Parts.Line(Location);
        }

        /// <summary>
        /// Attempts to save the line that is being built.
        /// Does nothing if no line is being built.
        /// </summary>
        private void TryFinishLine()
        {
            if (LineBuilder == null)
                return;
            Parts.Add(LineBuilder);
            LineBuilder = null;
        }
    }
}
