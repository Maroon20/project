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
    public partial class CrAb : Form
    {
        DataSet ds;
        public CrAb()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Accueil ss = new Accueil();
            ss.Show();
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


        public void verif_Char(KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
            {
                e.Handled = true;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
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


            MySqlCommand cmd = new MySqlCommand("INSERT INTO abonne(CIN,prenom,nom,adresse,NumF) VALUES(@CIN,@prenom,@nom,@adresse,@NumF)", cn);

            cmd.Parameters.AddWithValue("@CIN", int.Parse(textBox1.Text));
            cmd.Parameters.AddWithValue("@prenom", textBox2.Text);
            cmd.Parameters.AddWithValue("@nom", textBox3.Text);
            cmd.Parameters.AddWithValue("@adresse", textBox5.Text);
            cmd.Parameters.AddWithValue("@NumF", comboBox1.SelectedItem);
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            MessageBox.Show("ajout avec success");
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            verif_numero(e);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            verif_Char(e);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            verif_Char(e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MySqlConnection cn;
            cn = new MySqlConnection("SERVER=localhost;PORT=3306;DATABASE=mabasex1;UID=root;PWD=");
            MySqlCommand command = new MySqlCommand("select NumF from facture", cn);
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            ds = new DataSet();
            da.Fill(ds, "facture");
            foreach (DataRow x in ds.Tables[0].Rows)
            {

                comboBox1.Items.Add(x[0].ToString());
            }
            try
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

            }


            catch (Exception ex) { MessageBox.Show(ex.Message); }
            listView1.Items.Clear();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM abonne", cn);
            using (MySqlDataReader lire = cmd.ExecuteReader())
            {
                while (lire.Read())
                {
                    string CIN = lire["CIN"].ToString();
                    string prenom = lire["prenom"].ToString();
                    string nom = lire["nom"].ToString();
                    string adresse = lire["adresse"].ToString();
                    listView1.Items.Add(new ListViewItem(new[] { CIN, prenom, nom, adresse }));
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
                string CIN = element.SubItems[0].Text;
                MySqlCommand cmd = new MySqlCommand("DELETE FROM abonne WHERE CIN=@CIN",cn);
                cmd.Parameters.AddWithValue("@CIN", CIN);
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
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM abonne WHERE CIN=@CIN", cn);
            cmd.Parameters.AddWithValue("@CIN", textBox4.Text);
            listView1.Items.Clear();
            using (MySqlDataReader lire = cmd.ExecuteReader())
            {
                while (lire.Read())
                {
                    string CIN = lire["CIN"].ToString();
                    string prenom = lire["prenom"].ToString();
                    string nom = lire["nom"].ToString();
                    string adresse = lire["adresse"].ToString();
                    listView1.Items.Add(new ListViewItem(new[] { CIN, prenom, nom, adresse }));


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
                string CIN = element.SubItems[0].Text;
                string prenom = element.SubItems[1].Text;
                string nom = element.SubItems[2].Text;
                string adresse = element.SubItems[3].Text;
                using (MdAb m = new MdAb())
                {
                    m.CIN = CIN;
                    m.prenom = prenom;
                    m.nom = nom;
                    m.adresse = adresse; 
                    if (m.ShowDialog() == DialogResult.Yes)
                    {
                        MySqlCommand cmd = new MySqlCommand("UPDATE abonne SET prenom=@prenom,nom=@nom, adresse=@adresse WHERE CIN=@CIN", cn);
                        cmd.Parameters.AddWithValue("@CIN", m.CIN);
                        cmd.Parameters.AddWithValue("@prenom", m.prenom);
                        cmd.Parameters.AddWithValue("@nom", m.nom);
                        cmd.Parameters.AddWithValue("@adresse", adresse);
                        cmd.ExecuteNonQuery();
                        element.SubItems[1].Text = m.prenom;
                        element.SubItems[2].Text = m.nom;
                        element.SubItems[3].Text = m.adresse;
                        MessageBox.Show("element modifier de la base de données");


                    }
                }

            }
        }
    }
}
    
                

