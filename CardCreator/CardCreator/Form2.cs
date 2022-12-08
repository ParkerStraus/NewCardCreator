using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

namespace CardCreator
{
    public partial class CardCreator : Form
    {
        public string deckpath = "";
        public static string sTitle = "";
        public static string sDescription = "";
        public static string sImagePath = null;
        public static int sQuantity = 0;

        public CardCreator()
        {
            InitializeComponent();
        }

        private void attachImage(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    sImagePath = openFileDialog1.FileName;
                    pictureBox1.Image = Image.FromFile(sImagePath);
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private void clearImage(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            sImagePath = null;
        }

        private void exitToMenu(object sender, EventArgs e)
        {
            this.Hide();
            DeckViewMenu f1 = new DeckViewMenu();
            f1.filePath = deckpath;
            f1.Show();
        }

        private void addToDeck(object sender, EventArgs e)
        {
            sTitle = titleText.Text;
            sDescription = descText.Text;
            sQuantity = (int) quantity.Value;
            byte[] sImageBytes = null;
            if (sImagePath != null) sImageBytes = ImageToBytes(sImagePath);
            Card newCard = new Card(sTitle,sDescription,sImageBytes);

            FileEncoder.WriteToBinaryFile<Card>(deckpath+"\\" + sTitle + ".card", newCard);

        }

        private byte[] ImageToBytes(string imagepath)
        {
            ImageConverter imageConverter = new ImageConverter();
            return (byte[])imageConverter.ConvertTo((Bitmap)Image.FromFile(imagepath), typeof(byte[]));
            
        }


        
    }
}
