
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Telecom
{
    public partial class CrSr : Form
    {
        public CrSr()
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
        

                    MySqlCommand cmd = new MySqlCommand("INSERT INTO service(numS,descS,prixS,type,ISP) VALUES(@numS,@descS,@prixS,@type,@ISP)", cn);
                    cmd.Parameters.AddWithValue("@numS", textBox1.Text);
                    cmd.Parameters.AddWithValue("@descS", textBox2.Text);
                     cmd.Parameters.AddWithValue("@prixS", int.Parse(textBox3.Text));
                    cmd.Parameters.AddWithValue("@type", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@ISP", comboBox2.Text);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    MessageBox.Show("ajout avec success");



                
                
            }

        

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            verif_numero(e);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            verif_numero(e);
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
            listView1.Items.Clear();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM service", cn);
            using (MySqlDataReader lire = cmd.ExecuteReader())
            {
                while (lire.Read())
                {
                    string numS = lire["numS"].ToString();
                    string descS = lire["descS"].ToString();
                    string prixS = lire["prixS"].ToString();
                    string type = lire["type"].ToString();
                    string ISP = lire["ISP"].ToString();
                    listView1.Items.Add(new ListViewItem(new[] { numS, descS, prixS, type,ISP }));
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
                string numS = element.SubItems[0].Text;
                MySqlCommand cmd = new MySqlCommand("DELETE FROM service WHERE numS=@numS", cn);
                cmd.Parameters.AddWithValue("@numS", numS);
                cmd.ExecuteNonQuery();
                element.Remove();
                MessageBox.Show("element supprimer de la base de données");

            }
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
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM service WHERE numS=@numS", cn);
            cmd.Parameters.AddWithValue("@numS", textBox4.Text);
            listView1.Items.Clear();
            using (MySqlDataReader lire = cmd.ExecuteReader())
            {
                while (lire.Read())
                {
                    string numS = lire["numS"].ToString();
                    string descS = lire["descS"].ToString();
                    string prixS = lire["prixS"].ToString();
                    string type = lire["type"].ToString();
                    string ISP = lire["ISP"].ToString();
                    listView1.Items.Add(new ListViewItem(new[] { numS, descS, prixS, type, ISP }));


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
                string numS = element.SubItems[0].Text;
                string descS = element.SubItems[1].Text;
                string prixS = element.SubItems[2].Text;
                string type = element.SubItems[3].Text;
                string ISP = element.SubItems[4].Text;
                using (MdSr m = new MdSr())
                {
                    m.numS = numS;
                    m.descS = descS;
                    m.prixS = prixS;
                    m.type = type;
                    m.ISP = ISP;
                    if (m.ShowDialog() == DialogResult.Yes)
                    {
                        MySqlCommand cmd = new MySqlCommand("UPDATE service SET descS=@descS,prixS=@prixS, type=@type, ISP=@ISP WHERE numS=@numS", cn);
                        cmd.Parameters.AddWithValue("@numS", m.numS);
                        cmd.Parameters.AddWithValue("@desc", m.descS);
                        cmd.Parameters.AddWithValue("@prixS", m.prixS);
                        cmd.Parameters.AddWithValue("@type", type);
                        cmd.Parameters.AddWithValue("@ISP", ISP);
                        cmd.ExecuteNonQuery();
                        element.SubItems[1].Text = m.descS;
                        element.SubItems[2].Text = m.prixS;
                        element.SubItems[3].Text = m.type;
                        element.SubItems[4].Text = m.ISP;
                        MessageBox.Show("element modifier de la base de données");


                    }
                }

            }
        }
    }
}
