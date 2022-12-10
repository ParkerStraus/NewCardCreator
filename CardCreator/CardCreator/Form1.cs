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
                label2.Text = filePath;
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

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("About\n\n" +
                "Hello! Thank you for using this virtual deck/card builder!\n\n" +
                "Here are some quick steps to using this program.\n\n" +
                "1: To create a new deck, click 'File' in the top left and select 'New Deck'.\n" +
                "Next, choose the folder or directory where you would like to save the cards to your deck to.\n" +
                "1.a: Alternatively, if you already have a deck made, click 'Load Deck' and select the folder of the deck.\n\n" +
                "2: To create new cards and add them to your deck, click the 'Add New Cards' button.\n" +
                "This will take you to the card creation menu.\n\n" +
                "3: In the Card Creator menu, you can enter your card's name, description, and an image associated with it.\n" +
                "After entering the card's data, click the 'Add to Deck' button to add the cards to your deck.\n\n" +
                "4: To draw and play cards from your deck, click 'Deck' in the top left and click 'Draw Cards.'\n" +
                "This will take you to the play field.\n\n" +
                "5: In the play field, you can draw a card from your deck by clicking 'Draw Card'.\n" +
                "This will draw a random card from your deck and place it into your hand.\n\n" +
                "6: To play a card in your hand, select the card in your hand and click the 'Play Card' button.\n\n" +
                "7: To replace the cards into your deck, click the 'Reshuffle' button.\n\n" +
                "That's basically all there is to it! Have fun!");
        }
    }
}
