using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Deo.LaserVg
{
    class SvgExporter
    {
        private const string svgUrl = "http://www.w3.org/2000/svg";
        internal static readonly XNamespace ns = svgUrl;

        internal static void Export(Sketch sketch, string outputPath)
        {
            Console.WriteLine("Exporting...");

            var document = new XDocument();
            var root = new XElement("svg"//,
                //new XAttribute("xmlns", svgUrl), // this doesn't work
                );
            if (sketch.Width != 0)
                root.SetAttributeValue("width", sketch.Width + sketch.Unit);
            if (sketch.Height != 0)
                root.SetAttributeValue("height", sketch.Height + sketch.Unit);
            if (sketch.Width > 0 && sketch.Height > 0)
                root.SetAttributeValue("viewBox", $"0 0 {sketch.Width} {sketch.Height}");

            document.Add(root);

            foreach (var part in sketch.ExportedParts)
            {
                root.Add(part.ToXml());
            }

            Console.WriteLine("Saving...");

            var settings = new XmlWriterSettings()
            {
                Indent = true,
                NewLineOnAttributes = true,
                IndentChars = "  ",
                NewLineChars = "\n",
            };
            using (var writer = XmlWriter.Create(outputPath, settings))
            {
                document.WriteTo(writer);
            }

            Console.WriteLine("Done.");
        }
    }
}
