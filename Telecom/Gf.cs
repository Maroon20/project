using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
namespace Telecom
{
    public partial class Gf : Form
    {
        MySqlConnection cn;
        MySqlCommand cmd1;
        MySqlCommand cmd2;
        MySqlCommand cmd3;
        MySqlCommand cmd4;
        MySqlDataReader myReader = null;
        public Gf()
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
            /* Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
             PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("Facture.pdf", FileMode.Create));
             doc.Open();
             Paragraph paragraph = new Paragraph("Societe de telecommunication");
             doc.Add(paragraph);
             doc.Close(); */
            this.Hide();
            Form3 Fo3 = new Form3();
            Fo3.Show();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            verif_numero(e);
        }

        private void button3_Click(object sender, EventArgs e)
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

            MySqlCommand cmd = new MySqlCommand("SELECT mois,annee FROM facture,consommation WHERE  consommation.NumF=@facture.NumF", cn);
            cmd.Parameters.AddWithValue("@facture.NumF", textBox2.Text);
            myReader = cmd.ExecuteReader();



            while (myReader.Read())
            {
                textBox1.Text = (myReader["mois"].ToString());
                textBox3.Text = (myReader["annee"].ToString());
                string c = "0021670686486";
                textBox4.Text = c.ToString();

            }

            myReader.Close();
            MySqlCommand cmd2 = new MySqlCommand("SELECT nbUniteInternet,nbUniteVocale FROM consommation,facture WHERE  consommation.NumF=@facture.NumF", cn);

            cmd2.Parameters.AddWithValue("@facture.NumF", textBox2.Text);

            myReader = cmd2.ExecuteReader();
            while (myReader.Read())
            {
                textBox5.Text = (myReader["nbUniteVocale"].ToString());
                textBox6.Text = (myReader["nbUniteInternet"].ToString());


            }
            myReader.Close();
            MySqlCommand cmd3 = new MySqlCommand("SELECT * FROM abonne,facture WHERE   abonne.NumF=@facture.NumF", cn);
            cmd3.Parameters.AddWithValue("@facture.NumF", textBox2.Text);
            myReader = cmd3.ExecuteReader();
            while (myReader.Read())
            {
                textBox13.Text = (myReader["CIN"].ToString());
                textBox7.Text = (myReader["nom"].ToString());
                textBox8.Text = (myReader["prenom"].ToString());
                textBox9.Text = (myReader["adresse"].ToString());
                        
            }
            myReader.Close();
            MySqlCommand cmd4 = new MySqlCommand("SELECT privUniteInternet,privUniteVocale FROM consommation WHERE  consommation.NumF=@facture.NumF", cn);
            cmd4.Parameters.AddWithValue("@facture.NumF", textBox2.Text);
            myReader = cmd4.ExecuteReader();
            while (myReader.Read())
            {
                string a = (myReader["privUniteInternet"].ToString());
                string b = (myReader["privUniteVocale"].ToString());
                int i = int.Parse(a);
                int j = int.Parse(b);
                int uniV = int.Parse(textBox5.Text);
                int uniI = int.Parse(textBox6.Text);
                int montI = i * uniI;
                int montV = j * uniV;
                textBox10.Text = (montV.ToString());
                textBox11.Text = (montI.ToString());
                int montt = int.Parse(textBox10.Text) + int.Parse(textBox11.Text);
                textBox12.Text = (montt.ToString());


            }
            myReader.Close();
            cn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "" || textBox9.Text == "" || textBox10.Text == "" || textBox11.Text == "" || textBox12.Text == "" || textBox13.Text == "")
            {
                MessageBox.Show("Tous les champs sont obligatoire ! ");
            }
            else
            {

                Document document = new Document();
                FolderBrowserDialog folder = new FolderBrowserDialog();

                DialogResult result = folder.ShowDialog();



                PdfWriter.GetInstance(document, new FileStream(folder.SelectedPath + "\\Attestation d'inscription de " + textBox7.Text + " " + textBox8.Text + ".pdf", FileMode.Create));

                document.Open();

                Paragraph p1 = new Paragraph("                Société de télécommunication");
                Paragraph p2 = new Paragraph("                 Parc Technologique El Ghazela, Ariana 2088");
                Paragraph p3 = new Paragraph("         Facture\n \n \n");
                Paragraph p4 = new Paragraph("Numéro facture             :     " + textBox2.Text);
                Paragraph p5 = new Paragraph("Date         :    " + textBox1.Text + "/" + textBox3.Text);
                Paragraph p6 = new Paragraph("Numéro d' appelle      :     " + textBox4.Text);
                Paragraph p7 = new Paragraph("nbUnite_vocal:         " + textBox5.Text);
                Paragraph p8 = new Paragraph("nbUnite_internet     :         " + textBox6.Text);
                Paragraph p9 = new Paragraph("CIN:" + textBox13.Text);
                Paragraph p10 = new Paragraph("Nom Abonnee:" + textBox7.Text);
                Paragraph p11 = new Paragraph("PreNom Abonnee:" + textBox8.Text);
                Paragraph p12 = new Paragraph("Adresse:" + textBox9.Text);
                Paragraph p13 = new Paragraph("Montant_vocal:" + textBox10.Text);
                Paragraph p14 = new Paragraph("Montant_internet:" + textBox11.Text);
                Paragraph p15 = new Paragraph("Montant_facture:" + textBox12.Text);
                Paragraph p16 = new Paragraph("--------------------------------------------------------");
                Paragraph p17 = new Paragraph("                                               Signature");
                Paragraph p18 = new Paragraph("           Merci pour votre fidélité");

                document.Add(p1);
                document.Add(p2);
                document.Add(p3);
                document.Add(p4);
                document.Add(p5);
                document.Add(p6);
                document.Add(p7);
                document.Add(p8);
                document.Add(p9);
                document.Add(p10);
                document.Add(p11);
                document.Add(p12);
                document.Add(p13);
                document.Add(p14);
                document.Add(p15);
                document.Add(p16);
                document.Add(p17);
                document.Add(p18);
                document.Close();
                MessageBox.Show("le fichier a ete creer avec succees");
            }
            
            }
    }
}
