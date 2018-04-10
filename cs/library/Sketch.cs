using System;
using System.Collections.Generic;

namespace Deo.LaserVg
{
    public class Sketch
    {
        internal IList<IPart> Parts;

        public Sketch()
        {
            Parts = new List<IPart>();
        }

        /// <summary>
        /// Inserts raw svg markup
        /// </summary>
        /// <param name="svg">Markup to insert verbatim</param>
        public void Raw(string svg)
        {

        }

        public (decimal x, decimal y) MoveTo((decimal x, decimal y) point)
        {
            return point;
        }
    }
}
