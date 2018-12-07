namespace Deo.LaserVg.Sample
{
    class TreeSketch
    {
        Sketch sketch;

        // Thickness of the wood
        const decimal thickness = 0.125m;
        // Extra distance for elements that may burn
        const decimal burnMargin = 0.02m;
        static decimal branchEffect = thickness / 2;

        internal void Make(string outputPath)
        {
            sketch = new Sketch() { Etching = true, Width = 24m, Height = 10.5m, Unit = "in" };

            // Small
            MakeTrees(treeWidth: 3m, treeHeight: 4.5m, numTrees: 4, treeSegments: 2);
            sketch.Move(0, 10 * burnMargin);

            // Large
            MakeTrees(treeWidth: 4m, treeHeight: 5.5m, numTrees: 3, treeSegments: 4);
            sketch.Move(0, 10 * burnMargin);

            // Two rows of medium
            MakeTrees(treeWidth: 3.5m, treeHeight: 5m, numTrees: 2, treeSegments: 3);
            sketch.Move(0, 10 * burnMargin);
            MakeTrees(treeWidth: 3.5m, treeHeight: 5m, numTrees: 2, treeSegments: 3);
            sketch.Move(0, 10 * burnMargin);

            sketch.Save(outputPath);
        }

        private void MakeTrees(decimal treeWidth, decimal treeHeight, int numTrees, int treeSegments)
        {
            var treePartWidth = (treeWidth - thickness) / 2; // W of entire half of tree, without the W of trunk
            var treeSlopeWidth = (treePartWidth + (treeSegments - 1) * branchEffect) / treeSegments;

            sketch.StartGroup($"TreesWith{treeSegments}Segments");

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
                //sketch.Line(thickness, 0);
                // do not draw the flat peak of the other tree.
                // it will be drawn as a single line for all trees at the end
                // instead, just jump to the next tree
                sketch.Move(thickness, 0);
            }

            var endLocation = sketch.Location;

            // make a little offset
            sketch.Move(0, burnMargin);

            // make the bottoms of the trees
            for (int i = 0; i < numTrees; i++)
            {
                sketch.Line(-treePartWidth, 0);

                // notch for another tree
                sketch.Line(0, -treeHeight / 2);
                sketch.Line(-thickness, 0);
                sketch.Line(0, treeHeight / 2);

                sketch.Line(-treePartWidth, 0);

                // acommodate for peak of another tree
                sketch.Move(-thickness, 0);
            }

            // Make a flat horizontal line over all trees
            sketch.Move(-treePartWidth, -burnMargin - treeHeight);
            sketch.Line(endLocation.X - treePartWidth, 0);
            sketch.Move(-endLocation.X + treePartWidth, +burnMargin + treeHeight);

            sketch.EndGroup();
        }
    }
}
