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
    public partial class MdSr : Form
    {
        public string numS { get { return textBox1.Text; } set { textBox1.Text = value; } }
        public string descS { get { return textBox2.Text; } set { textBox2.Text = value; } }
        public string prixS { get { return textBox3.Text; } set { textBox3.Text = value; } }
        public string type { get { return comboBox1.Text; } set { comboBox1.Text = value; } }
        public string ISP { get { return comboBox2.Text; } set { comboBox2.Text = value; } }
        public MdSr()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }
    }
}
