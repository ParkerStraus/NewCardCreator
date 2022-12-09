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
            Card newCard = new Card(sTitle, sDescription, null);
            if (sImagePath != null) newCard.image = Image.FromFile(sImagePath);

            if (sQuantity == 1)
            {
                FileEncoder.WriteToBinaryFile<Card>(deckpath + "\\" + sTitle + ".card", newCard);
                MessageBox.Show($"{sTitle} card has been added to the deck");
            }
            else if (sQuantity > 1)
            {
                for (int i = 0; i < sQuantity; i++)
                {
                    FileEncoder.WriteToBinaryFile<Card>(deckpath + "\\" + sTitle + "(" + i + ").card", newCard);
                }
                MessageBox.Show($"{sQuantity} {sTitle} cards have been added to the deck");
            }
            
        }


        
    }
}
