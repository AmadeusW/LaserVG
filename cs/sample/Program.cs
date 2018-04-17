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
        static decimal branchEffect = thickness / treeSegments;

        static void Main(string[] args)
        {
            var sketch = new Sketch() { Etching = true, Width = 1m, Height = 0.5m, StrokeWidthEtching = 0.1m, Unit="in" };

            for (int i = 0; i <= numTrees; i++) // less or equals because we want to draw a "half tree"
            {
                // rising edge
                for (int s = 0; s < treeSegments; s++)
                {
                    sketch.Line(treeHalfBase / treeSegments, treeHeight / treeSegments);
                    if (s < treeSegments - 1) // don't do it on the last iteration
                        sketch.Line(-branchEffect, 0);
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
                    sketch.Line(treeHalfBase / treeSegments, -treeHeight / treeSegments);
                    sketch.Line(-branchEffect, 0);
                }

                // the other tree will have a flat peak
                sketch.Line(thickness, 0);
            }

            // make a little offset
            sketch.Move(0, burnMargin);
            // make a line back to origin
            for (int i = 0; i <= numTrees; i++)
            {
                sketch.Line(-treeHalfBase, 0);

                // the last tree is only one half
                if (i == numTrees)
                    continue;

                // notch for another tree
                sketch.Line(0, -treeHeight / 2);
                sketch.Line(thickness, 0);
                sketch.Line(0, treeHeight / 2);

                sketch.Line(-treeHalfBase, 0);

                // acommodate for peak of another tree
                sketch.Line(-thickness, 0);
            }
                

            sketch.Save("../../../../../out/trees.svg");
        }
    }
}
