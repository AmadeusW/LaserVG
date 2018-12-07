namespace Deo.LaserVg.Sample
{
    class RulerSketch
    {
        Sketch sketch;

        internal void Make(string outputPath)
        {
            sketch = new Sketch() { Etching = true, Width = 4.5m, Height = 0.5m, Unit = "in" };
            var partDistance = 0.02m;

            MakeLabel("1/2");
            MakeRulerPart(0.5m);
            sketch.Move(partDistance, 0);
            MakeLabel("1/2+", "1/32");
            MakeRulerPart(0.5m + 1 / 32m);
            sketch.Move(partDistance, 0);
            MakeLabel("1/2+", "1/16");
            MakeRulerPart(0.5m + 1 / 16m);
            sketch.Move(partDistance, 0);
            MakeLabel("1/2+", "1/8");
            MakeRulerPart(0.5m + 1 / 8m);
            sketch.Move(partDistance, 0);
            MakeLabel("1/2+", "1/4");
            MakeRulerPart(0.5m + 1 / 4m);
            sketch.Move(partDistance, 0);
            MakeLabel("1 inch");
            MakeRulerPart(1m);
            sketch.Move(partDistance, 0);

            sketch.Save(outputPath);
        }

        private void MakeLabel(params string[] labels)
        {
            var x = 0.02m;
            var y = 20 / 96m;
            var dy = 20 / 96m;
            var fontSize = 16 / 96m;

            foreach (var label in labels)
            {
                sketch.Text(label, fontSize, x, y);
                y += dy;
            }
        }

        private void MakeRulerPart(decimal width)
        {
            sketch.Line(width, 0);
            sketch.Line(0, 0.5m);
            sketch.Line(-width, 0);
            sketch.Line(0, -0.5m);
            sketch.Move(width, 0);
        }
    }
}
