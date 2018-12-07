using System;
using System.Collections.Generic;
using System.Text;

namespace Deo.LaserVg.Sample
{
    class MoireSketch
    {
        Sketch sketch;

        decimal Width => 3.0m;
        decimal Height => 3.0m;

        internal void Make(string outputPath)
        {
            sketch = new Sketch() { Etching = true, Width = Width, Height = Height, Unit = "in", StrokeWidthEtching = 0.01m };
            // TODO: add groups that rotate the lines
            DrawLines(60, 0.05m);
            DrawLines(60, 0.03m);
            sketch.Save(outputPath);
        }

        internal void DrawLines(int lineCount, decimal separation)
        {
            sketch.MoveTo(0, 0);
            for (int i = 0; i < lineCount; i++)
            {
                sketch.Line(Width, 0);
                sketch.Move(-Width, separation);
            }
        }
    }
}
