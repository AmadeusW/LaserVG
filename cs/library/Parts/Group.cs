using System.Collections.Generic;
using System.Xml.Linq;

namespace Deo.LaserVg.Parts
{
    class Group : IPart, IPartOwner
    {
        IList<IPart> Parts { get; }
        string Title { get; }

        internal Group(string title)
        {
            Parts = new List<IPart>();
            Title = title;
        }

        void IPartOwner.Add(IPart part)
        {
            Parts.Add(part);
        }

        public XElement ToXml()
        {
            var group = new XElement("g");

            if (!string.IsNullOrWhiteSpace(Title))
                group.Add(new XElement("title", Title));

            foreach (var part in Parts)
            {
                var child = part.ToXml();
                group.Add(child);
            }
            return group;
        }
    }
}
