using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Skins;
using DevExpress.Printing;
using DevExpress.XtraPrinting;
using System.Data.SqlClient;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraLayout;
using DevExpress.XtraBars;
using System.IO;
using System.IO.IsolatedStorage;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
namespace RibbonSimplePad
{
    public partial class cBon_livraison : DevExpress.XtraEditors.XtraForm
    {
        //private static int qterest;
        sql_gmao fun = new sql_gmao();
        sql_gmao fun1 = new sql_gmao();
        private static Double totalttc = 0, totalnetht = 0, tottva = 0, totfodec = 0;
        private static Double totalht = 0, prixremis = 0;
        public static DataTable dartt;
        string numero;
        public cBon_livraison()
        {
            InitializeComponent();
            textBox1.Text = listebls.displaybl.ToString();
            getallcmd();
            layoutControl1.Location = new Point(
             this.ClientSize.Width / 2 - layoutControl1.Size.Width / 2,
             6);
            DataTable bb = new DataTable();
            bb = fun.getbl(listebls.displaybl);
            if (bb.Rows.Count != 0)
            {
                if (bb.Rows[0]["etat"].ToString() == "envoyée")
                {
                    barButtonItem2.Enabled = false;
                    barButtonItem2.Caption = "BL Validé";
                }
            }
           
            comboBoxEdit3.Text = "DT";
            comboBoxEdit4.Text = "DT";


            comboBoxEdit1.Visible = false;




            for (int i = 0; i < gridView1.DataRowCount; i++)
            {
                DataRow row = gridView1.GetDataRow(i);
                comboBoxEdit2.EditValue = row[10];
                    Double TOTHT = Convert.ToDouble(row[3].ToString().Replace('.', ',')) * Convert.ToDouble(row[6].ToString().Replace('.', ','));
                    totalnetht += TOTHT;        
                Double totperone = TOTHT - (TOTHT * Convert.ToDouble(row[9].ToString().Replace('.', ',')) / 100);
                prixremis += totperone;
               
              
                totalht += totperone ;
                tottva += (totperone ) * Convert.ToDouble(row[10].ToString().Replace('.', ',')) / 100;
                Double totP = (totperone) + (totperone ) * Convert.ToDouble(row[10].ToString().Replace('.', ',')) / 100;

                totalttc += totP;


            }
            prixremis = totalnetht - prixremis;
            Double percentage = prixremis * 100 / totalnetht;

          
            labelControl23.Text = totalht.ToString();
            labelControl31.Text = (totalttc + Convert.ToDouble(comboBoxEdit5.Text.Replace('.', ','))).ToString();

            if (percentage != 0)
            {
                checkEdit1.Checked = true;
                comboBoxEdit1.EditValue = percentage.ToString();
            }

            DataTable ff = new DataTable();
            ff = fun1.affiche_BL_infos(listebls.displaybl);

            string etat = ff.Rows[0][3].ToString();
            string date_envoie = ff.Rows[0][1].ToString();
            string client = ff.Rows[0][4].ToString();
            string id_clt = ff.Rows[0][2].ToString();
            numero = ff.Rows[0]["numero_bl"].ToString();

            //information sur la société

            DataTable vv = new DataTable();
            vv = fun1.affiche_infos_societe();
            string test_image_1, test_image_2;
            test_image_1 = (vv.Rows[0]["pic_societe"].ToString());
            test_image_2 = (vv.Rows[0]["pic_pied"].ToString());



            if (test_image_1 != "")
            {
                byte[] IMG = (Byte[])(vv.Rows[0]["logo_societe"]);
                MemoryStream mem = new MemoryStream(IMG);
                pictureEdit2.Image = Image.FromStream(mem);
            }
            else { pictureEdit2.Image = null; }


            if (test_image_2 != "")
            {
                byte[] IMG2 = (Byte[])(vv.Rows[0]["imm"]);
                MemoryStream mem = new MemoryStream(IMG2);
                pictureEdit1.Image = Image.FromStream(mem);
            }
            else { pictureEdit1.Image = null; }

            labelControl13.Text = vv.Rows[0]["nom_societe"].ToString();
            labelControl14.Text = vv.Rows[0]["adresse_societe"].ToString();
            labelControl15.Text = vv.Rows[0]["matricule_societe"].ToString();
            labelControl16.Text = vv.Rows[0]["compte"].ToString();
            labelControl35.Text = vv.Rows[0]["banque"].ToString();
            labelControl17.Text = vv.Rows[0]["tel_societe"].ToString();
            labelControl18.Text = vv.Rows[0]["fax_societe"].ToString();
            labelControl19.Text = vv.Rows[0]["email_societe"].ToString();
            labelControl20.Text = vv.Rows[0]["site_societe"].ToString();
            DataTable zz = new DataTable();
            zz = fun.affiche_infos_clt(id_clt.ToString());
            if (zz.Rows.Count != 0)
            {
                labelControl21.Text = zz.Rows[0]["forme_juriduque"].ToString();
            }


            labelControl12.Text = client;
            if (date_envoie != "")
            {
                labelControl25.Text = date_envoie;
            }
            else { labelControl25.Text = System.DateTime.Now.ToLongDateString(); }
           

            if (barButtonItem2.Enabled == false)
            {
                DataTable gg = new DataTable();
                gg = fun.getbl(listebls.displaybl);
                comboBoxEdit1.Text = gg.Rows[0]["remise"].ToString();
                comboBoxEdit2.Text = gg.Rows[0]["tva"].ToString();
                labelControl23.Text = gg.Rows[0]["montant_ht"].ToString();
                labelControl31.Text = gg.Rows[0]["montant_ttc"].ToString();
                comboBoxEdit5.SelectedText = gg.Rows[0]["timbre"].ToString();


                if (comboBoxEdit1.Text == "")
                {
                    checkEdit1.Checked = false;
                    comboBoxEdit1.Visible = false;
                    labelControl26.Visible = false;
                }
                else
                {

                    comboBoxEdit1.Visible = true;
                    checkEdit1.Checked = true;
                }
                comboBoxEdit1.Enabled = false;
                comboBoxEdit2.Enabled = false;
                comboBoxEdit3.Enabled = false;
                comboBoxEdit4.Enabled = false;
                comboBoxEdit5.Enabled = false;
                checkEdit1.Enabled = false;
            }

        }
        private void printReicept()
        {
            PrintDialog printDialog = new PrintDialog();
            PrintDocument printdocument = new PrintDocument();
            printDialog.Document = printdocument;
            printdocument.PrintPage += new PrintPageEventHandler(printdocument_PrintPage);
            DialogResult resultat = printDialog.ShowDialog();
            if (resultat == DialogResult.OK)
            {
                printdocument.Print();
            }


        }


        private void labelControl7_Click(object sender, EventArgs e)
        {
        }



        private void getallcmd()
        {
            DataTable dt = new DataTable();
            dt = fun.get_Allprodbybl(listebls.displaybl.ToString());

            dartt = new DataTable();
            dartt.Clear();
            dartt.Columns.Add("id_piece");
            dartt.Columns.Add("code_art");
            dartt.Columns.Add("libelle_piece");
            dartt.Columns.Add("quantite_piece");
            dartt.Columns.Add("id_clt");
            dartt.Columns.Add("etat");
            dartt.Columns.Add("puv");
            dartt.Columns.Add("totvente");
            dartt.Columns.Add("id_commande");
            dartt.Columns.Add("remise");
            dartt.Columns.Add("ttva");
            dartt.Columns.Add("unit");
            foreach (DataRow row in dt.Rows)
            {
                DataRow newpc = dartt.NewRow();
                newpc["id_piece"] = row[0];
                newpc["code_art"] = row[1];
                newpc["libelle_piece"] = row[2];
                newpc["quantite_piece"] = Convert.ToDouble(row[3].ToString().Replace('.', ','));
                newpc["id_clt"] = row[4];
                newpc["etat"] = "validée";
                newpc["puv"] = row[6];
                newpc["totvente"] = row[7];
                newpc["id_commande"] = row[8];
                newpc["remise"] = row[9];
                newpc["ttva"] = row[10];
                newpc["unit"] = row[11];

                dartt.Rows.Add(newpc);
            }
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = dartt;
            this.gridView1.Columns[0].Visible = false;
            this.gridView1.Columns[1].Caption = "Ref°";
            this.gridView1.Columns[2].Caption = "Désignation";
            this.gridView1.Columns[3].Caption = "Qté ";
            this.gridView1.Columns[4].Visible = false;
            this.gridView1.Columns[5].Visible = false;
            this.gridView1.Columns[6].Visible = false;
            this.gridView1.Columns[7].Caption = "Prix ttc";
            this.gridView1.Columns[8].Visible = false;
            this.gridView1.Columns[9].Visible = false; ;
            this.gridView1.Columns[10].Visible = false;
            this.gridView1.Columns[11].Visible = false;
            gridView1.OptionsView.ShowAutoFilterRow = true;



        }
        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked == true)
            {
                comboBoxEdit1.Visible = true;
            }
            else { comboBoxEdit1.Visible = false; }
        }



        private void calcule()
        {

            Double temp, temp2, final;

            Double timbre = 0;
            Double value_after_remise = 0;
            Double value_before_remise = 0;

            value_before_remise = Convert.ToDouble(labelControl23.Text);
            if (checkEdit1.Checked == true)
            {
                if (comboBoxEdit1.Text == "")
                {
                    XtraMessageBox.Show("Choisir ou saisir un taux de remise ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    value_before_remise = Convert.ToDouble(labelControl23.Text);
                    temp = (value_before_remise * Convert.ToDouble(comboBoxEdit1.Text)) / 100;
                    value_after_remise = value_before_remise - temp;


                    if (comboBoxEdit2.Text == "")
                    {
                        XtraMessageBox.Show("Choisir ou saisir un taux de TVA ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    else
                    {
                        temp2 = (value_after_remise * Convert.ToDouble(comboBoxEdit2.Text)) / 100;

                        timbre = Convert.ToDouble(comboBoxEdit5.Text);

                        final = value_after_remise + temp2 + timbre;
                        //  labelControl31.Text = final.ToString();

                    }
                }
            }

            else
            {

                value_after_remise = value_before_remise;
                temp2 = (value_after_remise * Convert.ToDouble(comboBoxEdit2.Text)) / 100;
                //timbre = Int32.Parse(textEdit1.Text.ToString());
                timbre = Convert.ToDouble(comboBoxEdit5.Text);

                final = value_after_remise + temp2 + timbre;
            //   labelControl31.Text = final.ToString();


            }

        }

        private void comboBoxEdit1_TextChanged(object sender, EventArgs e)
        {
            if (barButtonItem2.Enabled == true)
            {
               // calcule();
            }
        }

        private void comboBoxEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {

            //allow only numeric
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void comboBoxEdit2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //allow only numeric
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (checkEdit1.Checked == true && comboBoxEdit1.Text == "")
            {
                XtraMessageBox.Show("Choisir ou saisir un taux de remise avant ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            else if (comboBoxEdit2.Text == "")
            {
                XtraMessageBox.Show("Choisir ou saisir un taux de TVA avant ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            else
            {
                if (barButtonItem2.Enabled == true)
                {
                    calcule();
                }
            }

        }

        private void comboBoxEdit3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxEdit4.Text = comboBoxEdit3.Text;
        }

        private void comboBoxEdit4_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxEdit3.Text = comboBoxEdit4.Text;
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            printPreviewDialog1.ShowDialog();

        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {


            string etat = "envoyée";
            if (checkEdit1.Checked == true)
            {
                if (comboBoxEdit1.Text == "")
                {
                    XtraMessageBox.Show("Choisir ou saisir un taux de remise!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }

            if (comboBoxEdit2.Text == "")
            {
                XtraMessageBox.Show("Choisir ou saisir un taux de TVA avant ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            fun.update_etat_commandeclt1(etat, Convert.ToInt32(textBox1.Text), DateTime.Now.ToString(), labelControl23.Text, labelControl31.Text, comboBoxEdit2.Text, comboBoxEdit1.Text, comboBoxEdit5.Text);
            fun.update_bonlivraison(etat, Convert.ToInt32(textBox1.Text), DateTime.Now.ToString(), labelControl23.Text, labelControl31.Text, comboBoxEdit2.Text, comboBoxEdit1.Text, comboBoxEdit5.Text);
            //update_bonlivraison(string etat, int code, string date_env, string ht, string ttc, string tva, string remise, string timbre)   
            barButtonItem2.Enabled = false;
            barButtonItem2.Caption = "Facture envoyée";

        }

        private void repositoryItemTextEdit1_Leave(object sender, EventArgs e)
        {
            fun.update_commentaire_facture(listebls.idcmd, barEditItem3.EditValue.ToString());
        }


        private void labelControl34_EditValueChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked == true)
            {
                if (comboBoxEdit1.Text == "")
                {
                    XtraMessageBox.Show("Choisir ou saisir un taux de remise!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }

            if (comboBoxEdit2.Text == "")
            {
                XtraMessageBox.Show("Choisir ou saisir un taux de TVA avant ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }

            if (barButtonItem2.Enabled == true)
            {
                calcule();
            }
        }

        private void facture_Activated(object sender, EventArgs e)
        {
            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridView1.BestFitColumns();
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.EnableAppearanceEvenRow = true;
        }

        private void textEdit1_Leave(object sender, EventArgs e)
        {

        }
        void printdocument_PrintPage(Object Sender, PrintPageEventArgs e)
        {

            Graphics grafic = e.Graphics;
            Font font = new Font("Courrier New", 12);
            float fontHeight = font.GetHeight();
            Point p1 = new Point(150, 200);
            Point p2 = new Point(150, 230);
            String uneDate = System.DateTime.Now.ToShortDateString();
            Pen p = new Pen(Brushes.Black, 1.2f);


            Image newImage = Image.FromFile("C:/1.png");
            grafic.DrawImage(newImage, 60, 60);

            grafic.FillRectangle(Brushes.White, new Rectangle(40, 200, 250, 30));
            grafic.DrawRectangle(p, new Rectangle(40, 200, 260, 30));
            grafic.DrawString("Document", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(60, 204, 250, 30));
            grafic.DrawString("BON DE LIVRAISON", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(160, 204, 250, 30));
            
            grafic.DrawString("Numéro:", new Font("Courrier New", 10), new SolidBrush(Color.Black), 50, 250);
            grafic.DrawString(numero, new Font("Courrier New", 10), new SolidBrush(Color.Black), 150, 250);
            grafic.DrawString("Date:", new Font("Courrier New", 10), new SolidBrush(Color.Black), 50, 280);
            grafic.DrawString(labelControl25.Text, new Font("Courrier New", 10), new SolidBrush(Color.Black), 140, 280);
            grafic.DrawLine(p, p1, p2);
            grafic.FillRectangle(Brushes.White, new Rectangle(400, 200, 400, 150));
            grafic.DrawRectangle(p, new Rectangle(400, 200, 400, 150));
            grafic.DrawString("Client :", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(402, 202, 400, 150));
            grafic.DrawString(labelControl12.Text, new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(462, 202, 400, 150));
            grafic.DrawString("Adresse :", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(402, 227, 400, 150));
            grafic.DrawString("Code postal :", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(402, 252, 400, 150));
            grafic.DrawString("Ville :", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(552, 252, 400, 150));
            grafic.DrawString("tél :", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(402, 277, 400, 150));
            grafic.DrawString("FAX :", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(552, 277, 400, 150));
            grafic.DrawString("MF :", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(402, 302, 400, 150));
            grafic.DrawString(labelControl21.Text, new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(442, 302, 400, 150));
            grafic.DrawString("Code Client :", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(402, 327, 400, 150));

            grafic.FillRectangle(Brushes.White, new Rectangle(40, 400, 150, 40));
            grafic.DrawRectangle(p, new Rectangle(40, 400, 150, 40));
            grafic.DrawString("Prix en TND", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(70, 408, 250, 30));

            grafic.FillRectangle(Brushes.White, new Rectangle(190, 400, 200, 40));
            grafic.DrawRectangle(p, new Rectangle(190, 400, 200, 40));
            grafic.DrawString("REf.Commande", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(200, 408, 250, 30));
            grafic.DrawString("Dépot", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(330, 408, 250, 30));
            Point p3 = new Point(310, 400);
            Point p4 = new Point(310, 440);
            grafic.DrawLine(p, p3, p4);

            grafic.FillRectangle(Brushes.White, new Rectangle(390, 400, 200, 40));
            grafic.DrawRectangle(p, new Rectangle(390, 400, 200, 40));
            grafic.DrawString("Mode de livraison", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(430, 408, 200, 40));

            grafic.FillRectangle(Brushes.White, new Rectangle(590, 400, 210, 40));
            grafic.DrawRectangle(p, new Rectangle(590, 400, 210, 40));
            grafic.DrawString("Moyen de livraison", new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(592, 408, 250, 40));
            grafic.DrawString("Lieu de livraison", new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(702, 408, 250, 40));

            grafic.FillRectangle(Brushes.White, new Rectangle(40, 440, 150, 20));
            grafic.DrawRectangle(p, new Rectangle(40, 440, 150, 20));
            grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20));



            grafic.FillRectangle(Brushes.White, new Rectangle(190, 440, 200, 20));
            grafic.DrawRectangle(p, new Rectangle(190, 440, 200, 20));
            grafic.DrawString("Désignation", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(240, 442, 200, 20));

            grafic.FillRectangle(Brushes.White, new Rectangle(390, 440, 60, 20));
            grafic.DrawRectangle(p, new Rectangle(390, 440, 60, 20));
            grafic.DrawString("UN", new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(400, 442, 60, 20));

            grafic.FillRectangle(Brushes.White, new Rectangle(450, 440, 70, 20));
            grafic.DrawRectangle(p, new Rectangle(450, 440, 70, 20));
            grafic.DrawString("Qté", new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(460, 442, 70, 20));

            grafic.FillRectangle(Brushes.White, new Rectangle(520, 440, 70, 20));
            grafic.DrawRectangle(p, new Rectangle(520, 440, 70, 20));
            grafic.DrawString("PUHT", new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(530, 442, 70, 20));

            grafic.FillRectangle(Brushes.White, new Rectangle(590, 440, 50, 20));
            grafic.DrawRectangle(p, new Rectangle(590, 440, 50, 20));
            grafic.DrawString("%REM", new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(595, 442, 50, 20));

            grafic.FillRectangle(Brushes.White, new Rectangle(640, 440, 60, 20));
            grafic.DrawRectangle(p, new Rectangle(640, 440, 60, 20));
            grafic.DrawString("PU N HT", new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(650, 442, 60, 20));
            Point p5 = new Point(700, 400);
            Point p6 = new Point(700, 440);
            grafic.DrawLine(p, p5, p6);

            grafic.FillRectangle(Brushes.White, new Rectangle(700, 440, 60, 20));
            grafic.DrawRectangle(p, new Rectangle(700, 440, 60, 20));
            grafic.DrawString("PTHT", new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(710, 442, 60, 20));

            grafic.FillRectangle(Brushes.White, new Rectangle(760, 440, 40, 20));
            grafic.DrawRectangle(p, new Rectangle(760, 440, 40, 20));
            grafic.DrawString("TVA", new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(770, 442, 40, 20));
            grafic.DrawString("eee", new Font("Courrier New", 10), new SolidBrush(Color.Black), 650, 642);

            grafic.FillRectangle(Brushes.White, new Rectangle(40, 460, 150, 380));
            grafic.DrawRectangle(p, new Rectangle(40, 460, 150, 380));
            // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(40, 462, 150, 380));

            grafic.FillRectangle(Brushes.White, new Rectangle(190, 460, 200, 380));
            grafic.DrawRectangle(p, new Rectangle(190, 460, 200, 380));
            // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20));

            grafic.FillRectangle(Brushes.White, new Rectangle(390, 460, 60, 380));
            grafic.DrawRectangle(p, new Rectangle(390, 460, 60, 380));
            // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

            grafic.FillRectangle(Brushes.White, new Rectangle(450, 460, 70, 380));
            grafic.DrawRectangle(p, new Rectangle(450, 460, 70, 380));
            // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

            grafic.FillRectangle(Brushes.White, new Rectangle(520, 460, 70, 380));
            grafic.DrawRectangle(p, new Rectangle(520, 460, 70, 380));
            // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

            grafic.FillRectangle(Brushes.White, new Rectangle(590, 460, 50, 380));
            grafic.DrawRectangle(p, new Rectangle(590, 460, 50, 380));
            // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

            grafic.FillRectangle(Brushes.White, new Rectangle(640, 460, 60, 380));
            grafic.DrawRectangle(p, new Rectangle(640, 460, 60, 380));
            // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

            grafic.FillRectangle(Brushes.White, new Rectangle(700, 460, 60, 380));
            grafic.DrawRectangle(p, new Rectangle(700, 460, 60, 380));
            // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

            grafic.FillRectangle(Brushes.White, new Rectangle(760, 460, 40, 380));
            grafic.DrawRectangle(p, new Rectangle(760, 460, 40, 380));
            // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))
            int r = 0;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                DataRow row = gridView1.GetDataRow(i);

                grafic.DrawString(row[1].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(50, 480 + r, 150, 380));


                grafic.DrawString(row[2].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(200, 480 + r, 200, 380));


                grafic.DrawString(row[11].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(400, 480 + r, 60, 380));

                grafic.DrawString(row[3].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(460, 480 + r, 70, 380));

                grafic.DrawString(row[6].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(530, 480 + r, 70, 380));

                grafic.DrawString(row[9].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(600, 480 + r, 50, 380));

                grafic.DrawString(row[3].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(650, 480 + r, 60, 380));

                grafic.DrawString(row[7].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(710, 480 + r, 60, 30));

                grafic.DrawString(row[10].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(770, 480 + r, 40, 30));
                r = r + 40;
            }



            grafic.FillRectangle(Brushes.White, new Rectangle(40, 860, 130, 40));
            grafic.DrawRectangle(p, new Rectangle(40, 860, 130, 40));
            grafic.DrawString("Le magasinier", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(50, 862, 130, 40));
            grafic.FillRectangle(Brushes.White, new Rectangle(170, 860, 130, 40));
            grafic.DrawRectangle(p, new Rectangle(170, 860, 130, 40));
            grafic.DrawString("Le controleur", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(180, 862, 130, 40));
            grafic.FillRectangle(Brushes.White, new Rectangle(300, 860, 130, 40));
            grafic.DrawRectangle(p, new Rectangle(300, 860, 130, 40));
            grafic.DrawString("Le transporteur", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(310, 862, 130, 40));



            grafic.FillRectangle(Brushes.White, new Rectangle(40, 900, 130, 60));
            grafic.DrawRectangle(p, new Rectangle(40, 900, 130, 60));
            //grafic.DrawString("TVA", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(750, 442, 60, 20));
            grafic.FillRectangle(Brushes.White, new Rectangle(170, 900, 130, 60));
            grafic.DrawRectangle(p, new Rectangle(170, 900, 130, 60));
            //grafic.DrawString("TVA", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(750, 442, 60, 20));
            grafic.FillRectangle(Brushes.White, new Rectangle(300, 900, 130, 60));
            grafic.DrawRectangle(p, new Rectangle(300, 900, 130, 60));
            //grafic.DrawString("TVA", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(750, 442, 60, 20));


            grafic.FillRectangle(Brushes.White, new Rectangle(550, 860, 250, 40));
            grafic.DrawRectangle(p, new Rectangle(550, 860, 250, 40));
            grafic.DrawString("Le client / le réceptionnaire", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(590, 862, 250, 40));

            grafic.FillRectangle(Brushes.White, new Rectangle(550, 900, 250, 60));
            grafic.DrawRectangle(p, new Rectangle(550, 900, 250, 60));
            //  grafic.DrawString("Total TTC", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(460, 942, 350, 20));


            grafic.DrawString("Le client avoir pris connaissance et accepté nos conditions générales de vente", new Font("Courrier New", 9), new SolidBrush(Color.Black), 40, 1000);
            grafic.DrawString("Z.L M'ghira 3-Rue Nabeul-2082-Fouchana.Tunisia/Email:info@aquatun.com/website: www.aquatun.com", new Font("Courrier New", 8), new SolidBrush(Color.Black), 40, 1050);
            grafic.DrawString("Tél : (+216)7A 409 215- (+216)71 409 237/ Fax: +216 71 409 223", new Font("Courrier New", 8), new SolidBrush(Color.Black), 40, 1070);
            grafic.DrawString("Sarl.cap.Soc 75 000.000 TND-TVA:1214920 C/A/MOOO-RCN°B24161292011-CD : 1214920C", new Font("Courrier New", 8), new SolidBrush(Color.Black), 40, 1090);
            grafic.DrawString("RIB:BIAT 08047020021000116515/BTK 200060600011211369270", new Font("Courrier New", 8), new SolidBrush(Color.Black), 40, 1110);

        }
        private void DrawRoundedRectangle(Graphics gfx, Rectangle Bounds, int CornerRadius, Pen DrawPen, Color FillColor)
        {
            GraphicsPath gfxPath = new GraphicsPath();

            DrawPen.EndCap = DrawPen.StartCap = LineCap.Round;

            gfxPath.AddArc(Bounds.X, Bounds.Y, CornerRadius, CornerRadius, 180, 90);
            gfxPath.AddArc(Bounds.X + Bounds.Width - CornerRadius, Bounds.Y, CornerRadius, CornerRadius, 270, 90);
            gfxPath.AddArc(Bounds.X + Bounds.Width - CornerRadius, Bounds.Y + Bounds.Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
            gfxPath.AddArc(Bounds.X, Bounds.Y + Bounds.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);
            gfxPath.CloseAllFigures();

            gfx.FillPath(new SolidBrush(FillColor), gfxPath);
            gfx.DrawPath(DrawPen, gfxPath);
        }
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics grafic = e.Graphics;
            Font font = new Font("Courrier New", 12);
            float fontHeight = font.GetHeight();
            Point p1 = new Point(150, 200);
            Point p2 = new Point(150, 230);
            String uneDate = System.DateTime.Now.ToShortDateString();
            Pen p = new Pen(Brushes.Black, 1.2f);


            // Image newImage = Image.FromFile("C:/1.png");
            //  grafic.DrawImage(newImage, 60, 60);
            DataTable client = new DataTable();
            client = fun.get_cltByDesign(labelControl12.Text);
            //grafic.FillRectangle(Brushes.White, new Rectangle(40, 200, 250, 30));
            //grafic.DrawRectangle(p, new Rectangle(40, 200, 260, 30));
            DrawRoundedRectangle(grafic, new Rectangle(40, 200, 280, 30), 20, p, Color.White);
            grafic.DrawString("Document", new Font("Courrier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(60, 204, 250, 30));
            grafic.DrawString("BON DE LIVRAISON", new Font("Courrier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(155, 204, 250, 30));

            grafic.DrawString("Numéro:", new Font("Courrier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), 50, 250);
            grafic.DrawString(numero, new Font("Courrier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), 150, 250);
            grafic.DrawString("Date:", new Font("Courrier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), 50, 280);
            grafic.DrawString(Convert.ToDateTime(labelControl25.Text).ToShortDateString(), new Font("Courrier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), 140, 280);
            grafic.DrawLine(p, p1, p2);

            DrawRoundedRectangle(grafic, new Rectangle(400, 200, 380, 120), 30, p, Color.White);
            //grafic.FillRectangle(Brushes.White, new Rectangle(400, 200, 380, 120));
            //grafic.DrawRectangle(p, new Rectangle(400, 200, 380, 120));
            grafic.DrawString("Client :", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(402, 202, 340, 150));
            grafic.DrawString(labelControl12.Text, new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(472, 202, 340, 150));
            grafic.DrawString("Adresse :", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(402, 220, 340, 150));
            grafic.DrawString(client.Rows[0][6].ToString(), new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(472, 220, 340, 150));
            grafic.DrawString("Code postal :", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(402, 240, 340, 150));
            grafic.DrawString(client.Rows[0][7].ToString(), new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(490, 240, 340, 150));
            grafic.DrawString("Ville :", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(552, 240, 340, 150));
            grafic.DrawString(client.Rows[0][8].ToString(), new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(600, 240, 340, 150));
            grafic.DrawString("tél :", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(402, 260, 340, 150));
            grafic.DrawString(client.Rows[0][3].ToString(), new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(452, 260, 340, 150));
            grafic.DrawString("FAX :", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(552, 260, 340, 150));
            grafic.DrawString(client.Rows[0][5].ToString(), new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(600, 260, 340, 150));
            grafic.DrawString("MF :", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(402, 280, 340, 150));
            grafic.DrawString(client.Rows[0][12].ToString(), new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(452, 280, 340, 150));
            grafic.DrawString("Code Client :", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(402, 300, 340, 150));
            grafic.DrawString(client.Rows[0][0].ToString(), new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(500, 300, 340, 150));

            grafic.FillRectangle(Brushes.White, new Rectangle(40, 400, 100, 40));
            grafic.DrawRectangle(p, new Rectangle(40, 400, 100, 40));
            grafic.DrawString("Prix en", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(60, 402, 100, 30));
            grafic.DrawString("TND", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(70, 422, 100, 30));

            grafic.FillRectangle(Brushes.White, new Rectangle(140, 400, 250, 40));
            grafic.DrawRectangle(p, new Rectangle(140, 400, 250, 40));
            grafic.DrawString("REf.Commande", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(160, 402, 250, 30));
            grafic.DrawString("Dépot", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(310, 402, 250, 30));
            Point p3 = new Point(290, 400);
            Point p4 = new Point(290, 440);
            grafic.DrawLine(p, p3, p4);

            grafic.FillRectangle(Brushes.White, new Rectangle(390, 400, 200, 40));
            grafic.DrawRectangle(p, new Rectangle(390, 400, 200, 40));
            grafic.DrawString("Mode de livraison", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(430, 402, 200, 40));

            grafic.FillRectangle(Brushes.White, new Rectangle(590, 400, 190, 40));
            grafic.DrawRectangle(p, new Rectangle(590, 400, 190, 40));
            grafic.DrawString("Moyen de livraison", new Font("Courrier New", 7, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(592, 402, 190, 40));
            grafic.DrawString("Lieu de livraison", new Font("Courrier New", 7, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(695, 402, 190, 40));

            grafic.FillRectangle(Brushes.White, new Rectangle(40, 440, 100, 20));
            grafic.DrawRectangle(p, new Rectangle(40, 440, 100, 20));
            grafic.DrawString("Code", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(70, 442, 100, 20));



            grafic.FillRectangle(Brushes.White, new Rectangle(140, 440, 250, 20));
            grafic.DrawRectangle(p, new Rectangle(140, 440, 250, 20));
            grafic.DrawString("Désignation", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(210, 442, 250, 20));

            grafic.FillRectangle(Brushes.White, new Rectangle(390, 440, 60, 20));
            grafic.DrawRectangle(p, new Rectangle(390, 440, 60, 20));
            grafic.DrawString("UN", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(400, 442, 60, 20));

            grafic.FillRectangle(Brushes.White, new Rectangle(450, 440, 70, 20));
            grafic.DrawRectangle(p, new Rectangle(450, 440, 70, 20));
            grafic.DrawString("Qté", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(470, 442, 70, 20));

            grafic.FillRectangle(Brushes.White, new Rectangle(520, 440, 70, 20));
            grafic.DrawRectangle(p, new Rectangle(520, 440, 70, 20));
            grafic.DrawString("PUHT", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(530, 442, 70, 20));

            grafic.FillRectangle(Brushes.White, new Rectangle(590, 440, 50, 20));
            grafic.DrawRectangle(p, new Rectangle(590, 440, 50, 20));
            grafic.DrawString("%REM", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(595, 442, 50, 20));

            grafic.FillRectangle(Brushes.White, new Rectangle(640, 440, 50, 20));
            grafic.DrawRectangle(p, new Rectangle(640, 440, 50, 20));
            grafic.DrawString("PU N HT", new Font("Courrier New", 7, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(640, 442, 50, 20));
            Point p5 = new Point(690, 400);
            Point p6 = new Point(690, 440);
            grafic.DrawLine(p, p5, p6);

            grafic.FillRectangle(Brushes.White, new Rectangle(690, 440, 50, 20));
            grafic.DrawRectangle(p, new Rectangle(690, 440, 50, 20));
            grafic.DrawString("PTHT", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(700, 442, 50, 20));

            grafic.FillRectangle(Brushes.White, new Rectangle(740, 440, 40, 20));
            grafic.DrawRectangle(p, new Rectangle(740, 440, 40, 20));
            grafic.DrawString("TVA", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(750, 442, 40, 20));


            grafic.FillRectangle(Brushes.White, new Rectangle(40, 460, 100, 380));
            grafic.DrawRectangle(p, new Rectangle(40, 460, 100, 380));
            // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(40, 462, 150, 380));

            grafic.FillRectangle(Brushes.White, new Rectangle(140, 460, 250, 380));
            grafic.DrawRectangle(p, new Rectangle(140, 460, 250, 380));
            // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20));

            grafic.FillRectangle(Brushes.White, new Rectangle(390, 460, 60, 380));
            grafic.DrawRectangle(p, new Rectangle(390, 460, 60, 380));
            // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

            grafic.FillRectangle(Brushes.White, new Rectangle(450, 460, 70, 380));
            grafic.DrawRectangle(p, new Rectangle(450, 460, 70, 380));
            // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

            grafic.FillRectangle(Brushes.White, new Rectangle(520, 460, 70, 380));
            grafic.DrawRectangle(p, new Rectangle(520, 460, 70, 380));
            // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

            grafic.FillRectangle(Brushes.White, new Rectangle(590, 460, 50, 380));
            grafic.DrawRectangle(p, new Rectangle(590, 460, 50, 380));
            // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

            grafic.FillRectangle(Brushes.White, new Rectangle(640, 460, 50, 380));
            grafic.DrawRectangle(p, new Rectangle(640, 460, 50, 380));
            // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

            grafic.FillRectangle(Brushes.White, new Rectangle(690, 460, 50, 380));
            grafic.DrawRectangle(p, new Rectangle(690, 460, 50, 380));
            // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

            grafic.FillRectangle(Brushes.White, new Rectangle(740, 460, 40, 380));
            grafic.DrawRectangle(p, new Rectangle(740, 460, 40, 380));
            // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))
            int r = 0;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                DataRow row = gridView1.GetDataRow(i);

                grafic.DrawString(row[1].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(50, 480 + r, 100, 380));


                grafic.DrawString(row[2].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(150, 480 + r, 250, 380));


                // grafic.DrawString(row[11].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(400, 480 + r, 60, 380));

                grafic.DrawString(row[3].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(470, 480 + r, 70, 380));

                // grafic.DrawString(row[6].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(530, 480 + r, 70, 380));

                // grafic.DrawString(row[9].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(600, 480 + r, 50, 380));

                // grafic.DrawString(row[3].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(650, 480 + r, 60, 380));

                //  grafic.DrawString(row[7].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(710, 480 + r, 60, 30));

                grafic.DrawString(row[10].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(750, 480 + r, 40, 30));
                r = r + 30;
            }



            grafic.FillRectangle(Brushes.White, new Rectangle(40, 860, 130, 40));
            grafic.DrawRectangle(p, new Rectangle(40, 860, 130, 40));
            grafic.DrawString("Le magasinier", new Font("Courrier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(50, 862, 130, 40));
            grafic.FillRectangle(Brushes.White, new Rectangle(170, 860, 130, 40));
            grafic.DrawRectangle(p, new Rectangle(170, 860, 130, 40));
            grafic.DrawString("Le controleur", new Font("Courrier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(180, 862, 130, 40));
            grafic.FillRectangle(Brushes.White, new Rectangle(300, 860, 130, 40));
            grafic.DrawRectangle(p, new Rectangle(300, 860, 130, 40));
            grafic.DrawString("Le transporteur", new Font("Courrier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(310, 862, 130, 40));



            grafic.FillRectangle(Brushes.White, new Rectangle(40, 900, 130, 60));
            grafic.DrawRectangle(p, new Rectangle(40, 900, 130, 60));
            //grafic.DrawString("TVA", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(750, 442, 60, 20));
            grafic.FillRectangle(Brushes.White, new Rectangle(170, 900, 130, 60));
            grafic.DrawRectangle(p, new Rectangle(170, 900, 130, 60));
            //grafic.DrawString("TVA", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(750, 442, 60, 20));
            grafic.FillRectangle(Brushes.White, new Rectangle(300, 900, 130, 60));
            grafic.DrawRectangle(p, new Rectangle(300, 900, 130, 60));
            //grafic.DrawString("TVA", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(750, 442, 60, 20));


            grafic.FillRectangle(Brushes.White, new Rectangle(550, 860, 230, 40));
            grafic.DrawRectangle(p, new Rectangle(550, 860, 230, 40));
            grafic.DrawString("Le client / le réceptionnaire", new Font("Courrier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(590, 862, 230, 40));

            grafic.FillRectangle(Brushes.White, new Rectangle(550, 900, 230, 60));
            grafic.DrawRectangle(p, new Rectangle(550, 900, 230, 60));
            //  grafic.DrawString("Total TTC", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(460, 942, 350, 20));


            grafic.DrawString("Le client déclaré avoir pris connaissance et accepté nos conditions générales de vente", new Font("Courrier New", 9), new SolidBrush(Color.Black), 40, 1000);
            /*  grafic.DrawString("Z.L M'ghira 3-Rue Nabeul-2082-Fouchana.Tunisia/Email:info@aquatun.com/website: www.aquatun.com", new Font("Courrier New", 8), new SolidBrush(Color.Black), 40, 1050);
           grafic.DrawString("Tél : (+216)7A 409 215- (+216)71 409 237/ Fax: +216 71 409 223", new Font("Courrier New", 8), new SolidBrush(Color.Black), 40, 1070);
           grafic.DrawString("Sarl.cap.Soc 75 000.000 TND-TVA:1214920 C/A/MOOO-RCN°B24161292011-CD : 1214920C", new Font("Courrier New", 8), new SolidBrush(Color.Black), 40, 1090);
           grafic.DrawString("RIB:BIAT 08047020021000116515/BTK 200060600011211369270", new Font("Courrier New", 8), new SolidBrush(Color.Black), 40, 1110);
           */
        }

        private void cBon_livraison_Load(object sender, EventArgs e)
        {

        }


    }
}