using System;

namespace Deo.LaserVg.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            new RulerSketch().Make(@"../../../../../out/ruler.svg");
            new TreeSketch().Make(@"../../../../../out/trees.svg");
        }
    }
}
