using System.Collections.Generic;
using System.Xml.Linq;

namespace Deo.LaserVg.Parts
{
    class Group : IPart, IPartOwner
    {
        IList<IPart> Parts { get; }
        string Title { get; }
        string TransformRaw { get; }

        internal Group(string title = null, string transform = null)
        {
            Parts = new List<IPart>();
            Title = title;
            TransformRaw = transform;
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

            if (!string.IsNullOrWhiteSpace(TransformRaw))
                group.Add(new XAttribute("transform", TransformRaw));

            foreach (var part in Parts)
            {
                var child = part.ToXml();
                group.Add(child);
            }
            return group;
        }
    }
}
