using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardCreator
{
    [Serializable]
    public class Card
    {
        public string name { get; set; }
        public string description { get; set; }
        public Image image;
        public Card( string name, string description, Image image)
        {
            this.name = name;
            this.description = description;
            this.image = image;
        }
    }
}
