using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Deo.LaserVg
{
    class SvgExporter
    {
        internal static void Export(IEnumerable<IPart> parts, string outputPath)
        {
            var document = new XDocument();
            var root = new XElement("svg");
            foreach (var part in parts)
            {
                root.Add(part.ToXml());
            }
            document.Add(root);
            var writer = XmlWriter.Create(outputPath);
            document.WriteTo(writer);
        }
    }
}
