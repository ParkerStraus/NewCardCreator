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
    public partial class CardView : Form
    {
        public string titl;
        public string description;
        public Image image;
        public CardView()
        {
            InitializeComponent();
        }

        private void CardView_Load(object sender, EventArgs e)
        {
            this.title.Text = titl;
            this.desc.Text = description;
            this.pictureBox1.Image = image;
        }
    }
}
