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
        byte[] image;
        public Card( string name, string description, byte[] image)
        {
            this.name = name;
            this.description = description;
            this.image = image;
        }

        public System.Drawing.Bitmap GetImageFromBytes()
        {
            if(image != null)
            {

                Bitmap bmp;
                using (var ms = new MemoryStream(image))
                {
                    bmp = new Bitmap(ms);
                }
                return bmp;
            }
            else return null;
        }
    }
}
