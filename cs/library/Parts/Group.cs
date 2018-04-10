using System.Collections.Generic;

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

        public object Serialize()
        {
            // add beginning
            // for all parts, serialize
            // add ending
            return null;
        }
    }
}
