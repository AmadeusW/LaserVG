using System.Xml.Linq;

namespace Deo.LaserVg.Parts
{
    class Raw : IPart
    {
        XElement Node { get; }

        internal Raw(string raw)
        {
            Node = XElement.Parse(raw);
        }

        public XElement ToXml()
        {
            return Node;
        }
    }
}
