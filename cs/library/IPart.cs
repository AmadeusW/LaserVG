using System;
using System.Collections.Generic;
using System.Text;

namespace Deo.LaserVg
{
    internal interface IPart
    {
        string Id { get; }
        object Serialize();
    }
}
