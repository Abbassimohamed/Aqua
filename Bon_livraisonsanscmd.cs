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
namespace RibbonSimplePad
{
    public partial class Bon_livraisonsanscmd : DevExpress.XtraEditors.XtraForm
    {
        // int qterest;
        private static Double totalttc = 0, totalnetht = 0, tottva = 0, totfodec = 0;
        private static Double totalht = 0, prixremis = 0;
       
        public Bon_livraisonsanscmd()
        {
            InitializeComponent();
           
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
       
          sql_gmao fun = new sql_gmao();
        private void labelControl7_Click(object sender, EventArgs e)
        {
        }
        private void facture_Load(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit mEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            gridControl1.RepositoryItems.Add(mEdit);
      
            
            textBox1.Text = BLNOCommandeBL.numbl.ToString();
            layoutControl1.Location = new Point(
             this.ClientSize.Width / 2 - layoutControl1.Size.Width / 2,
             6);
           
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = fun.get_Allprodbybl(textBox1.Text);
            this.gridView1.Columns[0].Visible = false;
            this.gridView1.Columns[1].Visible = false;
            this.gridView1.Columns[2].Caption = "Désignation";
            gridView1.Columns[2].ColumnEdit = mEdit;
            this.gridView1.Columns[3].Caption = "QTE";
            this.gridView1.Columns[4].Visible = false;
            this.gridView1.Columns[5].Visible = false;
            this.gridView1.Columns[6].Caption = "Prix Unitaire HT";
            this.gridView1.Columns[7].Caption = "Montant HT";
            this.gridView1.Columns[8].Visible = false;
            this.gridView1.Columns[9].Visible = false;
            this.gridView1.Columns[10].Visible = false;
            this.gridView1.Columns[11].Visible = false;
            gridView1.OptionsView.ShowAutoFilterRow = true;


            for (int i = 0; i < gridView1.DataRowCount; i++)
            {
                DataRow row = gridView1.GetDataRow(i);
                comboBoxEdit2.EditValue = row[10];
                totalnetht += Convert.ToDouble(row[3].ToString().Replace('.', ',')) * Convert.ToDouble(row[6].ToString().Replace('.', ','));
                Double TOTHT = Convert.ToDouble(row[3].ToString().Replace('.', ',')) * Convert.ToDouble(row[6].ToString().Replace('.', ','));
                Double totperone = TOTHT - (TOTHT * Convert.ToDouble(row[9].ToString().Replace('.', ',')) / 100);
                prixremis += totperone;


                totalht += totperone;
                tottva += (totperone) * Convert.ToDouble(row[10].ToString().Replace('.', ',')) / 100;
                Double totP = (totperone) + (totperone) * Convert.ToDouble(row[10].ToString().Replace('.', ',')) / 100;

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

            DataTable cc = new DataTable();
            cc = fun.affiche_infos_societe();

            labelControl13.Text = cc.Rows[0]["nom_societe"].ToString();
            labelControl14.Text = cc.Rows[0]["adresse_societe"].ToString();
            labelControl15.Text = cc.Rows[0]["matricule_societe"].ToString();
            labelControl16.Text = cc.Rows[0]["compte"].ToString();
            labelControl35.Text = cc.Rows[0]["banque"].ToString();
            labelControl17.Text = cc.Rows[0]["tel_societe"].ToString();
            labelControl18.Text = cc.Rows[0]["fax_societe"].ToString();
            labelControl19.Text = cc.Rows[0]["email_societe"].ToString();
            labelControl20.Text = cc.Rows[0]["site_societe"].ToString();
            DataTable zz = new DataTable();
            zz = fun.affiche_infos_clt(BLNOCommandeBL.id_clt.ToString());
            if (zz.Rows.Count != 0)
            {
                labelControl21.Text = zz.Rows[0]["forme_juriduque"].ToString();
                labelControl12.Text = zz.Rows[0]["raison_soc"].ToString();
            }
            DataTable dta=new DataTable();

            dta = fun.getbl(BLNOCommandeBL.id_clt);
          
            if (dta.Rows.Count!=0) 
            { 
                labelControl25.Text = dta.Rows[0][1].ToString();
            }
            else { labelControl25.Text = System.DateTime.Now.ToLongDateString(); }

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
            fun.update_commentaire_facture(liste_cde_client.id_fact, barEditItem3.EditValue.ToString());
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


            // Image newImage = Image.FromFile("C:/1.png");
            //  grafic.DrawImage(newImage, 60, 60);

            grafic.FillRectangle(Brushes.White, new Rectangle(40, 200, 250, 30));
            grafic.DrawRectangle(p, new Rectangle(40, 200, 260, 30));
            grafic.DrawString("Document", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(60, 204, 250, 30));
            grafic.DrawString("BON DE LIVRAISON", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(160, 204, 250, 30));

            grafic.DrawString("Numéro:", new Font("Courrier New", 10), new SolidBrush(Color.Black), 50, 250);
            grafic.DrawString(textBox1.Text, new Font("Courrier New", 10), new SolidBrush(Color.Black), 150, 250);
            grafic.DrawString("Date:", new Font("Courrier New", 10), new SolidBrush(Color.Black), 50, 280);
            grafic.DrawString(labelControl25.Text, new Font("Courrier New", 10), new SolidBrush(Color.Black), 140, 280);
            grafic.DrawLine(p, p1, p2);
            grafic.FillRectangle(Brushes.White, new Rectangle(400, 200, 380, 150));
            grafic.DrawRectangle(p, new Rectangle(400, 200, 380, 150));
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

            grafic.FillRectangle(Brushes.White, new Rectangle(40, 400, 100, 40));
            grafic.DrawRectangle(p, new Rectangle(40, 400, 100, 40));
            grafic.DrawString("Prix en TND", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(50, 408, 100, 30));

            grafic.FillRectangle(Brushes.White, new Rectangle(140, 400, 250, 40));
            grafic.DrawRectangle(p, new Rectangle(140, 400, 250, 40));
            grafic.DrawString("REf.Commande", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(160, 408, 250, 30));
            grafic.DrawString("Dépot", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(310, 408, 250, 30));
            Point p3 = new Point(290, 400);
            Point p4 = new Point(290, 440);
            grafic.DrawLine(p, p3, p4);

            grafic.FillRectangle(Brushes.White, new Rectangle(390, 400, 200, 40));
            grafic.DrawRectangle(p, new Rectangle(390, 400, 200, 40));
            grafic.DrawString("Mode de livraison", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(430, 408, 200, 40));

            grafic.FillRectangle(Brushes.White, new Rectangle(590, 400, 190, 40));
            grafic.DrawRectangle(p, new Rectangle(590, 400, 190, 40));
            grafic.DrawString("Moyen de livraison", new Font("Courrier New", 8), new SolidBrush(Color.Black), new Rectangle(592, 408, 190, 40));
            grafic.DrawString("Lieu de livraison", new Font("Courrier New", 8), new SolidBrush(Color.Black), new Rectangle(695, 408, 190, 40));

            grafic.FillRectangle(Brushes.White, new Rectangle(40, 440, 100, 20));
            grafic.DrawRectangle(p, new Rectangle(40, 440, 100, 20));
            grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(70, 442, 100, 20));



            grafic.FillRectangle(Brushes.White, new Rectangle(140, 440, 250, 20));
            grafic.DrawRectangle(p, new Rectangle(140, 440, 250, 20));
            grafic.DrawString("Désignation", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(210, 442, 250, 20));

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

            grafic.FillRectangle(Brushes.White, new Rectangle(640, 440, 50, 20));
            grafic.DrawRectangle(p, new Rectangle(640, 440, 50, 20));
            grafic.DrawString("PU net HT", new Font("Courrier New", 7), new SolidBrush(Color.Black), new Rectangle(640, 442, 50, 20));
            Point p5 = new Point(690, 400);
            Point p6 = new Point(690, 440);
            grafic.DrawLine(p, p5, p6);

            grafic.FillRectangle(Brushes.White, new Rectangle(690, 440, 50, 20));
            grafic.DrawRectangle(p, new Rectangle(690, 440, 50, 20));
            grafic.DrawString("PTHT", new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(700, 442, 50, 20));

            grafic.FillRectangle(Brushes.White, new Rectangle(740, 440, 40, 20));
            grafic.DrawRectangle(p, new Rectangle(740, 440, 40, 20));
            grafic.DrawString("TVA", new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(750, 442, 40, 20));


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

                grafic.DrawString(row[3].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(460, 480 + r, 70, 380));

                // grafic.DrawString(row[6].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(530, 480 + r, 70, 380));

                // grafic.DrawString(row[9].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(600, 480 + r, 50, 380));

                // grafic.DrawString(row[3].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(650, 480 + r, 60, 380));

                //  grafic.DrawString(row[7].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(710, 480 + r, 60, 30));

                grafic.DrawString(row[10].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(750, 480 + r, 40, 30));
                r = r + 30;
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


            grafic.FillRectangle(Brushes.White, new Rectangle(550, 860, 230, 40));
            grafic.DrawRectangle(p, new Rectangle(550, 860, 230, 40));
            grafic.DrawString("Le client / le réceptionnaire", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(590, 862, 230, 40));

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
    }
}