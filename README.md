# LaserVG
Programmatically create SVG files for laser cutting


![sample](https://user-images.githubusercontent.com/1673956/49623969-9bd18900-f984-11e8-9416-558a035fa7aa.png)

## To get started

* Add reference to the `LaserVG.Library` to create the SVG. (NuGet package will be available soon)
* Create instance of `Deo.LaserVg.Sketch`
* Invoke methods to move the cursor and place SVG elements
* Ultimately, invoke `Save` method to produce the SVG file.

See the `LaserVG.Sample` for examples how to interact with the library.

## Example

Inspired by George Nees' 1968 "Schotter",
this example shows groups with transforms and building squares out of lines.

![image](https://user-images.githubusercontent.com/1673956/49634886-da7e3800-f9b2-11e8-8d6d-2b32861d777c.png)

```
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

            sketch.StartGroup("Schotter", $"translate({Edge}, {Edge})"); // this group adds margin
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
```
