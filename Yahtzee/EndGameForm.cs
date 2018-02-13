using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee
{
    public partial class EndGameForm : Form
    {
        Yahtzee Form1 = new Yahtzee();//initialize form1(Yahtzee) into this form.
        public EndGameForm(string DataRecieved)
        {
            InitializeComponent();

            lblScore.Text = "Congratulations on Scoring: " + DataRecieved + " Points";//Set lable to disply text and score.
        }

        private void btnExit_Click(object sender, EventArgs e)//Button call for exit.
        {
            Form1.Close();//Close hidden yahtzee form.
            Close();//close this form.
        }

        private void btnNewGame_Click(object sender, EventArgs e)//Button call for new game.
        {
            this.Close();//Close this form.
            Form1.Visible = true;//Open Main Yahtzee form.
        }
    }
}
