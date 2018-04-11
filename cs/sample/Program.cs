using System;

namespace Deo.LaserVg.Sample
{
    class Program
    {
        // Thickness of the wood
        const decimal thickness = 0.25m;
        // Extra distance for elements that may burn
        const decimal burnMargin = 0.01m;

        // Basics
        const int numTrees = 5;
        const decimal treeHeight = 4.5m;
        const decimal treeHalfBase = 2m;

        // Cosmetics
        static int treeSegments = 3;
        static decimal treeWedge = thickness / treeSegments;

        static void Main(string[] args)
        {
            var sketch = new Sketch() { Etching = false };

            for (int i = 0; i <= numTrees; i++) // less or equals because we want to draw a "half tree"
            {
                // rising edge
                for (int s = 0; s < treeSegments; s++)
                {
                    sketch.Line(treeHalfBase / treeSegments, treeHeight / treeSegments);
                    sketch.Line(-treeWedge, 0);
                }

                // the last tree is only one edge
                if (i == numTrees)
                    continue;

                // notch for another tree
                sketch.Line(0, -treeHeight / 2);
                sketch.Line(thickness, 0);
                sketch.Line(0, treeHeight / 2);

                // falling edge
                for (int s = 0; s < treeSegments; s++)
                {
                    sketch.Line(-treeHalfBase / treeSegments, -treeHeight / treeSegments);
                    sketch.Line(treeWedge, 0);
                }

                // the other tree will have a flat peak
                sketch.Line(thickness, 0);
            }

            // remember the location
            var endpoint = sketch.Location;
            // make a line back to origin
            sketch.Move(0, burnMargin);
            sketch.Line(-endpoint.X, 0);

            sketch.Save("../../../../../out/trees.svg");
        }
    }
}
