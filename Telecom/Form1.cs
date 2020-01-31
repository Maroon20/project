using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Telecom
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        MySqlConnection cn;
               private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection Sqlcon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JEdhib\Documents\Dataaa.mdf;Integrated Security=True;Connect Timeout=30");
            String query = "select * from LOGIN where USERNAME ='" + textBox1.Text.Trim() + "'and PASSWORD='" + textBox2.Text.Trim() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query,Sqlcon);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1) { 
            
            Accueil ss = new Accueil();
                this.Hide();
                ss.Show();
            }
            else
            {
                MessageBox.Show("Vérifier votre Identifiant et Password");
            }


            if (button1.Text == "LOGIN")
            {
                cn = new MySqlConnection("SERVER=localhost;PORT=3306;DATABASE=mabasex1;UID=root;PWD=");
                
                try
                {
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                     
                    }

                }


                catch (Exception ex) { MessageBox.Show(ex.Message); }
 
            }


           





        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked==true)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }
    }
}
