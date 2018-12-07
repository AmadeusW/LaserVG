using System;
using System.Collections.Generic;

namespace Deo.LaserVg
{
    public class Sketch : IPartOwner
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
        /// Stroke width for paths to be etched. Default is 0.05.
        /// </summary>
        public decimal StrokeWidthEtching { get; set; } = 0.05m;

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
        /// Stores groups we're currently in. Group on top of the stack receives new items.
        /// </summary>
        private Stack<IPartOwner> groups;

        /// <summary>
        /// Gets the <see cref="IPartOwner"/> that will receive new items:
        /// The most nested group, or the SVG root.
        /// </summary>
        private IPartOwner CurrentPartOwner
        {
            get
            {
                return groups.Count > 0 ? groups.Peek() : this;
            }
        }

        /// <summary>
        /// Create a new sketch. This instance builds up information about shapes to cut.
        /// </summary>
        public Sketch()
        {
            parts = new List<IPart>();
            groups = new Stack<IPartOwner>();
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
            CurrentPartOwner.Add(new Parts.Raw(svg));
        }

        /// <summary>
        /// Creates a group. Subsequent elements will be added to this group until <see cref="EndGroup"/> is called.
        /// </summary>
        /// <param name="name"></param>
        public void StartGroup(string name)
        {
            var newGroup = new Parts.Group(name);
            CurrentPartOwner.Add(newGroup);
            groups.Push(newGroup);
        }

        /// <summary>
        /// Closes a group.
        /// </summary>
        public void EndGroup()
        {
            groups.Pop();
        }

        /// <summary>
        /// Moves the cursor to a target position
        /// </summary>
        /// <returns>New cursor position</returns>
        public Point MoveTo(decimal x, decimal y) => MoveTo((x, y));

        /// <summary>
        /// Moves the cursor to a target position
        /// </summary>
        /// <returns>New cursor position</returns>
        public Point MoveTo((decimal x, decimal y) point)
        {
            TryFinishPath();

            Location = (Point)point * Scale;

            TryStartPath(); // TryStartPath uses the current value of Location
            return Location;
        }

        /// <summary>
        /// Moves the cursor a specific distance
        /// </summary>
        /// <param name="deltaX">X displacement from the current cursor position</param>
        /// <param name="deltaY">Y displacement from the current cursor position</param>
        /// <returns>New cursor position</returns>
        public Point Move(decimal deltaX, decimal deltaY) => Move((deltaX, deltaY));

        /// <summary>
        /// Moves the cursor a specific distance
        /// </summary>
        /// <param name="delta">Displacement</param>
        /// <returns>New cursor position</returns>
        public Point Move((decimal x, decimal y) delta)
        {
            TryFinishPath();

            var scaled = (Point)delta * Scale;
            Location = Location + scaled;

            TryStartPath(); // TryStartPath uses the current value of Location
            return Location;
        }

        /// <summary>
        /// Draws a straight line from the current cursor position to a target position.
        /// Moves the cursor position to the target position.
        /// </summary>
        /// <param name="x">X coordinate of the target position</param>
        /// <param name="y">Y coordinate of the target position</param>
        /// <returns>New cursor position</returns>
        public Point LineTo(decimal x, decimal y) => LineTo((x, y));

        /// <summary>
        /// Draws a straight line from the current cursor position to a target position.
        /// Moves the cursor position to the target position.
        /// </summary>
        /// <param name="point">Coordinates of the target position</param>
        /// <returns>New cursor position</returns>
        public Point LineTo((decimal x, decimal y) point)
        {
            TryStartPath();

            var scaled = (Point)point * Scale;
            pathBuilder.AddLineTo(scaled);

            Location = scaled;
            return Location;
        }

        /// <summary>
        /// Draws a straight line from the current cursor position to a position relative to the current cursor position.
        /// Moves the cursor position to the tip of the line.
        /// </summary>
        /// <param name="deltaX">X displacement from the current cursor position</param>
        /// <param name="deltaY">Y displacement from the current cursor position</param>
        /// <returns>New cursor position</returns>
        public Point Line(decimal deltaX, decimal deltaY) => Line((deltaX, deltaY));

        /// <summary>
        /// Draws a straight line from the current cursor position to a position relative to the current cursor position.
        /// Moves the cursor position to the tip of the line.
        /// </summary>
        /// <param name="delta">Displacement</param>
        /// <returns>New cursor position</returns>
        public Point Line((decimal x, decimal y) delta)
        {
            TryStartPath();

            var scaled = (Point)delta * Scale;
            pathBuilder.AddLine(scaled);

            Location = Location + scaled;
            return Location;
        }

        /// <summary>
        /// Renders text at a current cursor position, displaced by <paramref name="deltaX"/> and <paramref name="deltaY"/>.
        /// </summary>
        /// <param name="text">Text content</param>
        /// <param name="fontSize">Font size</param>
        /// <param name="deltaX">X displacement from the current cursor position</param>
        /// <param name="deltaY">Y displacement from the current cursor position</param>
        public void Text(string text, decimal fontSize, decimal deltaX, decimal deltaY)
        {
            CurrentPartOwner.Add(new Parts.Text(text, fontSize, Location.X + deltaX * Scale, Location.Y + deltaY * Scale));
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
            CurrentPartOwner.Add(pathBuilder);
            pathBuilder = null;
        }

        /// <summary>
        /// Adds new part to the collection of parts.
        /// This should be the only way <see cref="parts"/> is mutated.
        /// </summary>
        /// <param name="part"></param>
        void IPartOwner.Add(IPart part)
        {
            parts.Add(part);
        }
    }
}
