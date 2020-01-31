using MySql.Data.MySqlClient;
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
    public partial class CrLi : Form
    {
        public CrLi()
        {
            InitializeComponent();
        }
        public void verif_numero(KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Accueil ss = new Accueil();
            ss.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection cn;
            cn = new MySqlConnection("SERVER=localhost;PORT=3306;DATABASE=mabasex1;UID=root;PWD=");
            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

            }


            catch (Exception ex) { MessageBox.Show(ex.Message); }


            MySqlCommand cmd = new MySqlCommand("INSERT INTO ligne(numL,numS) VALUES(@numL,@numS)", cn);
            
            cmd.Parameters.AddWithValue("@numL", int.Parse(textBox1.Text));
            cmd.Parameters.AddWithValue("@numS", int.Parse(textBox2.Text));
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            MessageBox.Show("ajout avec success");

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            verif_numero(e);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            verif_numero(e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MySqlConnection cn;
            cn = new MySqlConnection("SERVER=localhost;PORT=3306;DATABASE=mabasex1;UID=root;PWD=");
            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

            }


            catch (Exception ex) { MessageBox.Show(ex.Message); }
            listView1.Items.Clear();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM ligne", cn);
            using (MySqlDataReader lire = cmd.ExecuteReader())
            {
                while (lire.Read())
                {
                    string numL = lire["numL"].ToString();
                    string numS = lire["numS"].ToString();
                    listView1.Items.Add(new ListViewItem(new[] { numL, numS }));
                }
            }
        }

        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MySqlConnection cn;
            cn = new MySqlConnection("SERVER=localhost;PORT=3306;DATABASE=mabasex1;UID=root;PWD=");
            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

            }


            catch (Exception ex) { MessageBox.Show(ex.Message); }

            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem element = listView1.SelectedItems[0];
                string numL = element.SubItems[0].Text;
                MySqlCommand cmd = new MySqlCommand("DELETE FROM ligne WHERE numL=@numL", cn);
                cmd.Parameters.AddWithValue("@numL", numL);
                cmd.ExecuteNonQuery();
                element.Remove();
                MessageBox.Show("element supprimer de la base de données");

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MySqlConnection cn;
            cn = new MySqlConnection("SERVER=localhost;PORT=3306;DATABASE=mabasex1;UID=root;PWD=");
            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

            }


            catch (Exception ex) { MessageBox.Show(ex.Message); }
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM ligne WHERE numL=@numL", cn);
            cmd.Parameters.AddWithValue("@numL", textBox3.Text);
            listView1.Items.Clear();
            using (MySqlDataReader lire = cmd.ExecuteReader())
            {
                while (lire.Read())
                {
                    string numL = lire["numL"].ToString();
                    string numS = lire["numS"].ToString();
                    listView1.Items.Add(new ListViewItem(new[] { numL, numS }));


                }

            }
        }

        private void modifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MySqlConnection cn;
            cn = new MySqlConnection("SERVER=localhost;PORT=3306;DATABASE=mabasex1;UID=root;PWD=");
            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

            }


            catch (Exception ex) { MessageBox.Show(ex.Message); }
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem element = listView1.SelectedItems[0];
                string numL = element.SubItems[0].Text;
                string numS = element.SubItems[1].Text;
                using (MdLi m = new MdLi())
                {
                    m.numL = numL;
                    m.numS = numS;
                    if (m.ShowDialog() == DialogResult.Yes)
                    {
                        MySqlCommand cmd = new MySqlCommand("UPDATE ligne SET numS=@numS WHERE numL=@numL", cn);
                        cmd.Parameters.AddWithValue("@numL", m.numL);
                        cmd.Parameters.AddWithValue("@numS", m.numS);
                        cmd.ExecuteNonQuery();
                        element.SubItems[1].Text = m.numS;
                        MessageBox.Show("element modifier de la base de données");


                    }
                }

            }
        }
    }
}
