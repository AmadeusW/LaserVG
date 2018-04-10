using System;
using System.IO;

namespace Deo.LaserVg
{
    class SvgExporter
    {
        internal static void Export(Sketch sketch, string outputPath)
        {
            foreach (var part in sketch.Parts)
            {
                var serialized = part.Serialize();
            }
            //File.WriteAllText(outputPath, input.ToUpperInvariant());
        }
    }
}
