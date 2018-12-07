using System;
using System.IO;

namespace Deo.LaserVg.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var outDir = @"../../../../out";
            new RulerSketch().Make(Path.Combine(outDir, "ruler.svg"));
            new TreeSketch().Make(Path.Combine(outDir, "trees.svg"));
            new MoireSketch().Make(Path.Combine(outDir, "moire.svg"));
        }
    }
}
