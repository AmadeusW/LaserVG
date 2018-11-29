using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Deo.LaserVg
{
    class SvgExporter
    {
        internal static void Export(Sketch sketch, string outputPath)
        {
            Console.WriteLine("Exporting...");

            XNamespace ns = "http://www.w3.org/2000/svg";
            var document = new XDocument();
            var root = new XElement("svg");
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
            UnfuckXml(root, ns);

            Console.WriteLine("Saving...");

            EnsureDirectoryExists(outputPath);

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

        /// <summary>
        /// Recursively appends the <see cref="XNamespace"/> to every <see cref="XElement"/> of the tree.
        /// SVG requires us to set the namespace on the root <svg> element.
        /// The evil .NET XML implementation requires us to set the namespace on every element.
        /// Without explicitly setting the namespace, svg elements would receive an empty namespace
        /// and would not render. Of course, there is no way to set the default namespace for the entire document.
        /// To work around this horrible mess, we traverse the entire tree and set the namespace.
        /// </summary>
        /// <param name="element">Visited <see cref="XElement"/></param>
        /// <param name="ns">the <see cref="XNamespace"/> that needs to be added to every <see cref="XElement"/></param>
        private static void UnfuckXml(XElement element, XNamespace ns)
        {
            element.Name = ns + element.Name.LocalName;
            foreach (XElement child in element.Elements())
            {
                UnfuckXml(child, ns);
            }
        }

        private static void EnsureDirectoryExists(string outputPath)
        {
            var directoryName = Path.GetDirectoryName(outputPath);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
        }
    }
}
