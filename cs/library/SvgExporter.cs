using System;
using System.Collections.Generic;
using System.IO;

namespace Deo.LaserVg
{
    class SvgExporter
    {
        internal static void Export(IEnumerable<IPart> parts, string outputPath)
        {
            // Create thet XML elements
            foreach (var part in parts)
            {
                part.Serialize();
            }
            //File.WriteAllText(outputPath, input.ToUpperInvariant());
        }
    }
}
