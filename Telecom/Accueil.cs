using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Telecom
{
    public partial class Accueil : Form
    {
        public Accueil()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            CrAb sa = new CrAb();
            sa.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            CrLi sl = new CrLi();
            sl.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            CrSr ssr = new CrSr();
            ssr.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Gf sf = new Gf();
            sf.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 sform = new Form1();
            sform.Show();
        }
    }
}
