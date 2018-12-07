using System;
using System.Collections.Generic;
using System.Text;

namespace Deo.LaserVg.Sample
{
    class Schotter
    {
        double RotationFactor = 4;
        double TranslationFactor = 0.2;
        decimal Edge = 1.0m;
        int Width = 4;
        int Height = 7;

        Sketch sketch;
        Random random;

        internal void Make(string outputPath)
        {
            sketch = new Sketch() { Etching = true, Width = Width+Edge, Height = Height+Edge, Unit = "in" };
            random = new Random();

            sketch.StartGroup("Schotter", $"translate({Edge/2}, {Edge / 2})");
            for (int row = 0; row < Height; row++)
                for (int col = 0; col < Width; col++)
                    MakeSquare(row, col);
            sketch.EndGroup();

            sketch.Save(outputPath);
        }

        private void MakeSquare(int row, int col)
        {
            var rotation = (random.NextDouble() * row - row * 0.5) * RotationFactor;
            var dx = (decimal)((random.NextDouble() * row - row * 0.5) * TranslationFactor);
            var dy = (decimal)((random.NextDouble() * row - row * 0.5) * TranslationFactor);

            sketch.StartGroup(string.Empty, $"rotate({rotation} {col * Edge + Edge / 2} {row * Edge + Edge / 2}) translate({col * Edge + dx} {row * Edge + dy})");
            sketch.MoveTo(0, 0);
            sketch.Line(Edge, 0);
            sketch.Line(0, Edge);
            sketch.Line(-Edge, 0);
            sketch.Line(0, -Edge);
            sketch.EndGroup();
        }
    }
}
