using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CardCreator
{
    public partial class DeckCreation : Form
    {
        public string path;
        public string newpath;
        public DeckCreation()
        {
            InitializeComponent();
        }

        private void CreateDeck(object sender, EventArgs e)
        {
            try
            {
                newpath = path + "\\" + deckNameBox.Text;
                Directory.CreateDirectory(newpath);
                MessageBox.Show("A deck at " + newpath + " has been created.");
                this.Hide();
                this.DialogResult= DialogResult.OK;
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void CancelDeckCreation(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
