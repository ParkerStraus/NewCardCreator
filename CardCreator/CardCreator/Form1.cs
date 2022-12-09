using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace CardCreator
{
    public partial class DeckViewMenu : Form
    {
        public List<Card> cards = new List<Card>();
        public string filePath;

        public DeckViewMenu()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            if(filePath != null)
            {
                loadDeckActual();
            }
        }

        private void newcardbutton_Click(object sender, EventArgs e)
        {
            this.Hide();
            CardCreator f2 = new CardCreator();
            f2.Show();
            f2.deckpath = filePath;
        }


        private void loadDeck(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = folderBrowserDialog.SelectedPath;
            }

            //string[] files = Directory.GetFiles(filePath);
            //System.Windows.Forms.MessageBox.Show(files.Length + " card(s) found in " + filePath);
            //check if cards are in folder
            loadDeckActual();

        }

        void loadDeckActual()
        {

            DataGridViewRow baserow = (DataGridViewRow)CardList.Rows[0].Clone();

            CardList.Rows.Clear();

            string[] files = Directory.GetFiles(filePath);
            

            foreach (string file in files)
            {
                DataGridViewRow item = baserow;
                if(Path.GetExtension(file).EndsWith(".card"))
                {

                    Card newCard = FileEncoder.ReadFromBinaryFile<Card>(file);
                    Image image = newCard.image;
                    CardList.Rows.Add(newCard.name, newCard.description, image);
                    cards.Add(newCard);
                }
            }
        }

        private void exitProgram(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newDeck(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                string filelocation = folderBrowserDialog.SelectedPath;
                using (var form = new DeckCreation())
                {
                    form.path = filelocation;
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        filePath = form.newpath;
                        pathLabel.Text = "Current Path: "+form.newpath;
                    }
                }

            }
        }

        private void drawCardMenu(object sender, EventArgs e)
        {
            this.Hide();
            DrawCards form3 = new DrawCards();
            form3.deckCards = cards;
            form3.cards = cards;
            form3.Show();
            form3.initmenu = this;
        }
    }
}
