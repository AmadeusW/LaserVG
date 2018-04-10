using System;

namespace Deo.LaserVg.Sample
{
    class Program
    {
        const decimal treeHeight = 4.5m;
        const decimal treeHalfBase = 2m;
        const int numTrees = 5;

        static void Main(string[] args)
        {
            Console.WriteLine("Building trees");
            var sketch = new Sketch();
            sketch.MoveTo(0, treeHeight);
            for (int i = 0; i < numTrees; i++)
            {
                sketch.Line(treeHalfBase, -treeHeight);
                sketch.Line(treeHalfBase, treeHeight);
            }
            sketch.Save("trees.svg");
        }
    }
}
