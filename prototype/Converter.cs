using System;
using System.IO;

namespace Deo.LaserVg
{
    class Converter
    {
        internal static void Convert(string inputPath, string outputPath)
        {
            var input = File.ReadAllText(inputPath);
            File.WriteAllText(outputPath, input.ToUpperInvariant());
        }
    }
}
