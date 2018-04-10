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
            foreach (var part in parts)
            {
                document.Add(part.ToXml());
            }
            var writer = XmlWriter.Create(outputPath);
            document.WriteTo(writer);
        }
    }
}
