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
        /// Whether the laser is etching or cutting
        /// </summary>
        public bool Etching { get; set; }

        /// <summary>
        /// Stroke width for paths to be cut. Default is 0.001.
        /// </summary>
        public decimal StrokeWidthCutting { get; set; } = 0.001m;

        /// <summary>
        /// Stroke width for paths to be etched. Default is 0.1.
        /// </summary>
        public decimal StrokeWidthEtching { get; set; } = 0.1m;

        /// <summary>
        /// Width of the sketch. If 0, it won't be recorded in svg.
        /// </summary>
        public decimal Width { get; set; } = 0m;

        /// <summary>
        /// Height of the sketch. If 0, it won't be recorded in svg.
        /// </summary>
        public decimal Height { get; set; } = 0m;

        /// <summary>
        /// Unit to use in conjunction with <see cref="Width"/> and <see cref="Height"/>
        /// </summary>
        public string Unit { get; set; } = string.Empty;

        /// <summary>
        /// Multiplier for all coordinates
        /// </summary>
        public decimal Scale { get; set; } = 1m;

        /// <summary>
        /// Elements that will be exported
        /// </summary>
        internal IEnumerable<IPart> ExportedParts => parts;

        /// <summary>
        /// Container of all parts known so far
        /// </summary>
        private IList<IPart> parts;

        /// <summary>
        /// Currently built path
        /// </summary>
        private Parts.Path pathBuilder;

        /// <summary>
        /// Create a new sketch. This instance builds up information about shapes to cut.
        /// </summary>
        public Sketch()
        {
            parts = new List<IPart>();
            Location = (0, 0);
        }
        
        /// <summary>
        /// Saves the sketch as SVG file.
        /// </summary>
        /// <param name="fileName"></param>
        public void Save(string fileName)
        {
            TryFinishPath();
            SvgExporter.Export(this, fileName);
        }

        /// <summary>
        /// Inserts raw svg markup
        /// </summary>
        /// <param name="svg">Markup to insert verbatim</param>
        public void Raw(string svg)
        {
            TryFinishPath();
            parts.Add(new Parts.Raw(svg));
        }

        public Point MoveTo(decimal x, decimal y) => MoveTo((x, y));
        public Point MoveTo((decimal x, decimal y) point)
        {
            TryFinishPath();
            TryStartPath();

            Location = (Point)point * Scale;
            return Location;
        }

        public Point Move(decimal deltaX, decimal deltaY) => Move((deltaX, deltaY));
        public Point Move((decimal x, decimal y) delta)
        {
            TryFinishPath();
            TryStartPath();

            var scaled = (Point)delta * Scale;
            Location = Location + delta;
            return Location;
        }

        public Point LineTo(decimal x, decimal y) => LineTo((x, y));
        public Point LineTo((decimal x, decimal y) point)
        {
            TryStartPath();

            var scaled = (Point)point * Scale;
            pathBuilder.AddLineTo(scaled);

            Location = scaled;
            return Location;
        }

        public Point Line(decimal deltaX, decimal deltaY) => Line((deltaX, deltaY));
        public Point Line((decimal x, decimal y) delta)
        {
            TryStartPath();

            var scaled = (Point)delta * Scale;
            pathBuilder.AddLine(scaled);

            Location = Location + scaled;
            return Location;
        }

        /// <summary>
        /// Attempts to create a new line.
        /// Does nothing if a line is already being built.
        /// </summary>
        private void TryStartPath()
        {
            if (pathBuilder == null)
                pathBuilder = new Parts.Path(this);
        }

        /// <summary>
        /// Attempts to save the line that is being built.
        /// Does nothing if no line is being built.
        /// </summary>
        private void TryFinishPath()
        {
            if (pathBuilder == null)
                return;
            parts.Add(pathBuilder);
            pathBuilder = null;
        }
    }
}
