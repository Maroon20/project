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
    public partial class MdLi : Form
    {
        public string numL { get { return textBox1.Text; } set { textBox1.Text = value; } }
        public string numS { get { return textBox2.Text; } set { textBox2.Text = value; } }
        
        public MdLi()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }
    }
}
