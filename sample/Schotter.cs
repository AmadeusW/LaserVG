using Deo.LaserVg;
using System;

namespace Deo.LaserVg.Sample
{
    class Schotter
    {
        double RotationFactor = 0.6;
        double TranslationFactor = 0.03;
        decimal Edge = 1.0m;
        int Width = 4;
        int Height = 7;

        Sketch sketch;
        Random random;

        internal void Make(string outputPath)
        {
            sketch = new Sketch() { Etching = true, Width = Width+2*Edge, Height = Height+2*Edge, Unit = "in" };
            random = new Random();

            sketch.StartGroup(transform: $"translate({Edge}, {Edge})"); // this group adds margin
            for (int row = 0; row < Height; row++)
                for (int col = 0; col < Width; col++)
                    MakeSquare(row, col);
            sketch.EndGroup();

            sketch.Save(outputPath);
        }

        private double RandomInExponentialRange(double range) => random.NextDouble() * range * range - (range * range / 2);

        private void MakeSquare(int row, int col)
        {
            var rotation = RandomInExponentialRange(row) * RotationFactor;
            var dx = (decimal)(RandomInExponentialRange(row) * TranslationFactor);
            var dy = (decimal)(RandomInExponentialRange(row) * TranslationFactor);

            sketch.StartGroup(transform: $"rotate({rotation} {col * Edge + Edge / 2} {row * Edge + Edge / 2}) translate({col * Edge + dx} {row * Edge + dy})");
            sketch.MoveTo(0, 0);
            sketch.Line(Edge, 0);
            sketch.Line(0, Edge);
            sketch.Line(-Edge, 0);
            sketch.Line(0, -Edge);
            sketch.EndGroup();
        }
    }
}
