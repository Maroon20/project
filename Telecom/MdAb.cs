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
    public partial class MdAb : Form
    {
        public string CIN { get { return textBox1.Text; } set { textBox1.Text = value; } }
        public string prenom { get { return textBox2.Text; } set { textBox2.Text = value; } }
        public string nom { get { return textBox3.Text; } set { textBox3.Text = value; }  }
        public string adresse { get { return textBox5.Text; } set { textBox5.Text = value; } }
        public MdAb()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }
    }
}
