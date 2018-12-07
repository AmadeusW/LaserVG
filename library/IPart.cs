using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Deo.LaserVg
{
    internal interface IPart
    {
        XElement ToXml();
    }
}
