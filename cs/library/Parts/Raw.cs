namespace Deo.LaserVg.Parts
{
    class Raw : IPart
    {
        string Body { get; }

        internal Raw(string raw)
        {
            Body = raw;
        }

        public object Serialize()
        {
            return Body;
        }
    }
}
