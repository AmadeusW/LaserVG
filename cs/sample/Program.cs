using System;

namespace Deo.LaserVg.Sample
{
    class Program
    {
        // Thickness of the wood
        const decimal thickness = 0.25m;
        // Extra distance for elements that may burn
        const decimal burnMargin = 0.01m;        
        static decimal branchEffect = thickness/2;

        static Sketch sketch;

        static void Main(string[] args)
        {
            sketch = new Sketch() { Etching = true, Width = 1m, Height = 0.5m, StrokeWidthEtching = 0.05m, Unit="in" };

            MakeTrees(treeWidth: 3m, treeHeight: 4.5m, numTrees: 5, treeSegments: 2);
            sketch.Move(0, 10 * burnMargin);
            MakeTrees(treeWidth: 3.5m, treeHeight: 5m, numTrees: 4, treeSegments: 3);
            sketch.Move(0, 10 * burnMargin);
            MakeTrees(treeWidth: 4m, treeHeight: 5.5m, numTrees: 4, treeSegments: 4);
            sketch.Move(0, 10 * burnMargin);

            sketch.Save("../../../../../out/trees.svg");
        }

        private static void MakeTrees(decimal treeWidth, decimal treeHeight, int numTrees, int treeSegments)
        {
            var treePartWidth = (treeWidth - thickness) / 2; // W of entire half of tree, without the W of trunk
            var treeSlopeWidth = (treePartWidth + (treeSegments - 1) * branchEffect) / treeSegments;

            for (int i = 0; i <= numTrees; i++) // less or equals because we want to draw a "half tree"
            {
                // rising edge
                for (int s = 0; s < treeSegments - 1; s++)
                {
                    sketch.Line(treeSlopeWidth, treeHeight / treeSegments);
                    sketch.Line(-branchEffect, 0);
                }
                // top segment of the rising edge
                sketch.Line(treeSlopeWidth, treeHeight / treeSegments);

                // the last tree only has the rising edge
                if (i == numTrees)
                    continue;

                // notch for another tree
                sketch.Line(0, -treeHeight / 2);
                sketch.Line(thickness, 0);
                sketch.Line(0, treeHeight / 2);

                // falling edge
                for (int s = 0; s < treeSegments - 1; s++)
                {
                    sketch.Line(treeSlopeWidth, -treeHeight / treeSegments);
                    sketch.Line(-branchEffect, 0);
                }
                // bottom segment of the falling edge
                sketch.Line(treeSlopeWidth, -treeHeight / treeSegments);

                // the other tree will have a flat peak
                sketch.Line(thickness, 0);
            }

            // make a little offset
            sketch.Move(0, burnMargin);
            // make a line back to origin
            for (int i = 0; i <= numTrees; i++)
            {
                sketch.Line(-treePartWidth, 0);

                // the last tree is only one half
                if (i == numTrees)
                    continue;

                // notch for another tree
                sketch.Line(0, -treeHeight / 2);
                sketch.Line(-thickness, 0);
                sketch.Line(0, treeHeight / 2);

                sketch.Line(-treePartWidth, 0);

                // acommodate for peak of another tree
                sketch.Move(-thickness, 0);
            }
        }
    }
}
