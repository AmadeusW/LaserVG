using System.Collections.Generic;
using System.Xml.Linq;

namespace Deo.LaserVg.Parts
{
    class Group : IPart
    {
        IList<IPart> Parts { get; }
        string Name { get; }

        internal Group(string name)
        {
            Parts = new List<IPart>();
            Name = name;
        }

        internal void Add(IPart part)
        {
            Parts.Add(part);
        }

        public XElement ToXml()
        {
            var group = new XElement("g");
            foreach (var part in Parts)
            {
                var child = part.ToXml();
                group.Add(child);
            }
            return group;
        }
    }
}
