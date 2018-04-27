using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Deo.LaserVg.Parts
{
    class Text : IPart
    {
        private string text;
        private decimal fontSize;
        private decimal x;
        private decimal y;

        public Text(string text, decimal fontSize, decimal x, decimal y)
        {
            this.text = text;
            this.fontSize = fontSize;
            this.x = x;
            this.y = y;
        }

        public XElement ToXml()
        {
            return new XElement("text", text,
                new XAttribute("font-size", fontSize),
                new XAttribute("font-family", "Verdana"),
                new XAttribute("x", x),
                new XAttribute("y", y)
            );
        }
    }
}
