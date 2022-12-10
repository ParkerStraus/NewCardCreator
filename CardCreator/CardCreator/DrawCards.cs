using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardCreator
{
    public partial class DrawCards: Form
    {
        public DeckViewMenu initmenu;
        public List<Card> cards;
        public List<Card> deckCards;
        public List<Card> handCards = new List<Card>();
        public DrawCards()
        {
            InitializeComponent();
        }

        private void DrawCards_Load(object sender, EventArgs e)
        {
            deckCards = cards;
            LoadCards();
        }

        public void LoadCards()
        {
            CardList.Rows.Clear();
            foreach (Card card in deckCards)
            {
                CardList.Rows.Add(card.name, card.description, card.image);
            }
            handView.Rows.Clear();
            foreach (Card card in handCards)
            {
                handView.Rows.Add(card.name, card.description, card.image);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            if(deckCards.Count <= 0)
            {
                MessageBox.Show("There are no more cards in the deck. \nPress \"Reshuffle Cards\" to bring cards back to deck.");
            }
            else
            {
                int i = random.Next(0, deckCards.Count - 1);
                handCards.Add(deckCards.ElementAt(i));

                deckCards.Remove(deckCards.ElementAt(i));
                LoadCards();
            }

        }

        private void shuffleCards(object sender, EventArgs e)
        {
            foreach(Card card in handCards)
            {
                deckCards.Add(card);
            }
            handCards.Clear();
            LoadCards();
        }

        private void playCard(object sender, EventArgs e)
        {
            try
            {
                int i = handView.CurrentCell.RowIndex;
                handView.Rows.RemoveAt(i);
            }
            catch (Exception exception)
            {
                MessageBox.Show("No cards are in your hand to play.\n");
            }
        }

        private void ExitToMenu(object sender, EventArgs e)
        {
            DeckViewMenu form1 = initmenu;
            form1.Show();
            this.Close();
        }
    }
}
