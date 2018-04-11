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

        internal static void Export(IEnumerable<IPart> parts, string outputPath)
        {
            Console.WriteLine("Exporting...");

            var document = new XDocument();
            var root = new XElement("svg"//,
                //new XAttribute("xmlns", svgUrl), // this doesn't work
                // new XAttribute("width", "5cm")
                );
            document.Add(root);

            foreach (var part in parts)
            {
                root.Add(part.ToXml());
            }

            Console.WriteLine("Saving...");

            var settings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\n"
            };
            using (var writer = XmlWriter.Create(outputPath, settings))
            {
                document.WriteTo(writer);
            }

            Console.WriteLine("Done.");
        }
    }
}
