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
    public partial class facturing : DevExpress.XtraEditors.XtraForm
    {
        String codetva;
        string numero_ = "";
        //private static int qterest;
        private static Double total_ht = 0, prixremis = 0, totalnetht = 0, retres;
        private static Double total_ttc = 0, montanttva = 0, montantremis = 0, montantfodec = 0;
        public facturing()
        {
            InitializeComponent();
            
          
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
        private void DrawRoundedRectangle1(Graphics gfx, Rectangle Bounds, int CornerRadius, Pen DrawPen, Color FillColor)
        {
            GraphicsPath gfxPath = new GraphicsPath();

            DrawPen.EndCap = DrawPen.StartCap = LineCap.Round;

            gfxPath.AddArc(Bounds.X, Bounds.Y, CornerRadius, CornerRadius, 180, 90);
            gfxPath.AddArc(Bounds.X + Bounds.Width - CornerRadius, Bounds.Y, CornerRadius, CornerRadius, 90, 90);
            gfxPath.AddArc(Bounds.X + Bounds.Width - CornerRadius, Bounds.Y + Bounds.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);
            gfxPath.AddArc(Bounds.X, Bounds.Y + Bounds.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);
            gfxPath.CloseAllFigures();

            gfx.FillPath(new SolidBrush(FillColor), gfxPath);
            gfx.DrawPath(DrawPen, gfxPath);
        }
        string yesno = "";
          sql_gmao fun = new sql_gmao();
        private void labelControl7_Click(object sender, EventArgs e)
        {
        }
        private void facture_Load(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit mEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            gridControl1.RepositoryItems.Add(mEdit);
      
           
            textBox1.Text = listefactvente.id_fact.ToString();

            layoutControl1.Location = new Point(
       this.ClientSize.Width / 2 - layoutControl1.Size.Width / 2,
      6);
            DataTable bb = new DataTable();
            bb = fun.get_etat_factvente(listefactvente.id_fact);
            if (bb.Rows.Count != 0)
            {
                if (bb.Rows[0]["etat"].ToString() == "envoyée")
                {
                    barButtonItem2.Enabled = false;
                    barButtonItem2.Caption = "Facture envoyée";
                }
            }
            if (liste_cde_client.etat_commande == "Annulée")
            { barButtonItem2.Enabled = false;
            barEditItem3.Enabled = false;
            }
            comboBoxEdit3.Text = "DT";
            comboBoxEdit4.Text = "DT";
            Double total_ht=0;

             comboBoxEdit1.Visible = false;
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = fun.selectpiece_factbycodefact(listefactvente.id_fact); 
            this.gridView1.Columns[0].Visible = false;
            this.gridView1.Columns[1].Visible = false;
            this.gridView1.Columns[2].Caption = "Désignation";
            gridView1.Columns[2].ColumnEdit = mEdit;
            this.gridView1.Columns[3].Caption = "QTE";
            this.gridView1.Columns[4].Visible = false;
            this.gridView1.Columns[5].Visible = false;
            this.gridView1.Columns[6].Visible = false;
            this.gridView1.Columns[7].Visible = false;
            this.gridView1.Columns[8].Visible = false;
            this.gridView1.Columns[9].Caption = "Prix Unitaire HT";
            this.gridView1.Columns[10].Caption = "Montant HT";
            this.gridView1.Columns[11].Visible = false;
            this.gridView1.Columns[12].Visible = false;
            this.gridView1.Columns[13].Visible = false;
            this.gridView1.Columns[14].Visible = false;
            gridView1.OptionsView.ShowAutoFilterRow = true;
            for (int i = 0; i < gridView1.DataRowCount; i++)
            {
                DataRow row = gridView1.GetDataRow(i);
                Double totP = Convert.ToDouble(row[3].ToString().Replace('.', ',')) * Convert.ToDouble(row[9].ToString().Replace('.', ','));
                Double totrem = totP - totP * Convert.ToDouble(row[14].ToString().Replace('.', ',')) / 100;
                totalnetht += totP;

                //Double totc = Convert.ToDouble(row[10].ToString().Replace('.', ','));
              
                total_ht += totrem;
                Double totc = (totrem) + (totrem ) * Convert.ToDouble(row[13].ToString().Replace('.', ',')) / 100;
                total_ttc += totc;
                montantremis += totP * Convert.ToDouble(row[14].ToString().Replace('.', ',')) / 100;
                prixremis += totrem;
               
                montanttva += (totrem) * Convert.ToDouble(row[13].ToString().Replace('.', ',')) / 100;

                comboBoxEdit2.EditValue = row[13].ToString();
              
            }
            prixremis = totalnetht - prixremis;
            Double percentage = prixremis * 100 / totalnetht;
            if (percentage != 0)
            {
                checkEdit1.Checked = true;
                comboBoxEdit1.Text = percentage.ToString();
            }
            DataTable ff = new DataTable();
            ff = fun.get_etat_factvente(listefactvente.id_fact);
            
            string etat = ff.Rows[0]["etat"].ToString();
            string date_envoie = ff.Rows[0]["date_ajout"].ToString();
            string client = ff.Rows[0]["client"].ToString();
            string id_clt = ff.Rows[0]["id_clt"].ToString();
            labelControl27.Text = ff.Rows[0]["id_fact"].ToString();
            labelControl23.Text = ff.Rows[0]["montant_ht"].ToString();
            labelControl31.Text = ff.Rows[0]["montant_ttc"].ToString();
             numero_ = ff.Rows[0]["numero_fact"].ToString();
         
            labelControl25.Text = Convert.ToDateTime( date_envoie).ToShortDateString();
            DataTable vv = new DataTable();
            vv = fun.affiche_infos_societe();
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
            //information sur la société
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
            zz = fun.affiche_infos_clt(id_clt.ToString());
            if (zz.Rows.Count != 0)
            {
                labelControl21.Text = zz.Rows[0]["forme_juriduque"].ToString();
            }
            

            labelControl12.Text = client;
           
           
        }

       
        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked == true)
            {
                comboBoxEdit1.Visible = true;
            }
            else { comboBoxEdit1.Visible = false; }
        }

        private void comboBoxEdit2_TextChanged(object sender, EventArgs e)
        {

            if (barButtonItem2.Enabled == true)
            {
                calcule();
            }
            }
         
        private void calcule()
        {

            Double temp, temp2, final;

            Double timbre = 0;
            Double value_after_remise = 0;
            Double value_before_remise = 0;

            value_before_remise = Convert.ToDouble(labelControl23.Text.Replace('.', ','));
            if (checkEdit1.Checked == true)
            {
                if (comboBoxEdit1.Text == "")
                {
                    XtraMessageBox.Show("Choisir ou saisir un taux de remise ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    value_before_remise = Convert.ToDouble(labelControl23.Text.Replace('.', ','));
                    temp = (value_before_remise * Convert.ToDouble(comboBoxEdit1.Text.Replace('.', ','))) / 100;
                    value_after_remise = value_before_remise - temp;
                  

                    if (comboBoxEdit2.Text == "")
                    {
                        XtraMessageBox.Show("Choisir ou saisir un taux de TVA ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    else
                    {
                        temp2 = (value_after_remise * Convert.ToDouble(comboBoxEdit2.Text.Replace('.', ','))) / 100;

                        timbre = Convert.ToDouble(comboBoxEdit5.Text.Replace('.', ','));

                        final = value_after_remise + temp2 + timbre;
                            //labelControl31.Text = final.ToString();
                        
                        }
                }
            }

            else
            {

                value_after_remise = value_before_remise;
                temp2 = (value_after_remise * Convert.ToDouble(comboBoxEdit2.Text.Replace('.', ','))) / 100;
                //timbre = Int32.Parse(textEdit1.Text.ToString());
                timbre = Convert.ToDouble(comboBoxEdit5.Text.Replace('.', ','));

                final = value_after_remise + temp2 + timbre;
                    labelControl31.Text = final.ToString();
               

            }
        
        }

        private void comboBoxEdit1_TextChanged(object sender, EventArgs e)
        {
            if (barButtonItem2.Enabled == true)
            {
                calcule();
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
              for (int i = 0; i < gridView1.DataRowCount; i++)
                {
                    string req_iddd = gridView1.GetRowCellValue(i, "id_piece").ToString();
                    Double qte_piece = Convert.ToDouble(gridView1.GetRowCellValue(i, "quantite_piece").ToString().Replace('.', ','));
                    string lib_piece = (gridView1.GetRowCellValue(i, "libelle_piece")).ToString();
                    string code_piece = (gridView1.GetRowCellValue(i, "code_art")).ToString();
                    DataTable nn = new DataTable();
                    // tester si on a en stock les quantités disponibles
                    nn = fun.test_dispo_piece(lib_piece);
                    Double qte_en_stock;
                    qte_en_stock = Convert.ToDouble(nn.Rows[0]["quantite_piece"].ToString().Replace('.', ','));
                    if (qte_piece > qte_en_stock)
                    {
                      yesno = "no";
                        //si quantité de piece indisponible alerte
                        XtraMessageBox.Show("facture non validé (" + lib_piece + ") est indisponible en Stock .Penser à passer ce produit en commande!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        yesno = "yes";
                    }
                }
                if (yesno == "yes")
                {
                    //si ts va bien  
                    for (int i = 0; i < gridView1.DataRowCount; i++)
                    {
                        DataRow row = gridView1.GetDataRow(i);

                        Double qtepiece = Convert.ToDouble(gridView1.GetRowCellValue(i, "quantite_piece").ToString().Replace('.', ','));
                      string  codep = (gridView1.GetRowCellValue(i, "code_art")).ToString();
                        //reserver ces piece et mettre à jour les quantités en stock
                        fun.update_sousstock_after_accept(qtepiece,codep );



                    }
                   
                }
                fun.insertintofacture(liste_cde_client.id_clt.ToString(), labelControl25.Text, etat, labelControl12.Text, liste_cde_client.id_fact.ToString(), labelControl23.Text, comboBoxEdit1.Text, labelControl31.Text, comboBoxEdit5.Text, comboBoxEdit2.Text);
                barButtonItem2.Enabled = false;
                barButtonItem2.Caption = "Facture envoyée";


                // si son etat initial est différent de a
              

                
            
            }

        private void repositoryItemTextEdit1_Leave(object sender, EventArgs e)
        {
            fun.update_commentaire_facture(liste_cde_client.id_fact, barEditItem3.EditValue.ToString());
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
        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 1)
            {
                words += NumberToWords(number / 1000) + " mille ";
                number %= 1000;
            }
            if ((number / 1000) == 1)
            {
                words += " mille ";
                number %= 1000;
            }

            if ((number / 100) > 1)
            {
                words += NumberToWords(number / 100) + " cent ";
                number %= 100;
            }
            if ((number / 100) == 1)
            {
                words += " cent ";
                number %= 100;
            }
            if (number > 0)
            {
                // if (words != "")
                //;

                var unitsMap = new[] { "zero", "un", "deux", "trois", "quatre", "cinq", "six", "sept", "huit", "neuf", "dix", "onze", "douze", "treize", "quatorze", "quinze", "seize", "dix-sept", "dix-huit", "dix-neuf" };
                var tensMap = new[] { "zero", "dix", "vingt", "trente", "quarante", "cinquante", "soixante", "soixante-dix", "quatre-vingts", "quatre-vingt-dix" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }
        public static String NumberToWords1(String T)
        {
            String s1 = "";
            String s2 = "";
            String s = "";
            int i = 0;
            //String T= number.ToString();

            while (T[i] != ',' && i < T.Length)
            {
                s1 += T[i];
                i++;
            }
            int j = i + 1;
            while (j < T.Length)
            {


                s2 += T[j];
                j++;

            }
          //  MessageBox.Show(s2);
            s = NumberToWords(Convert.ToInt32(s1)) + " DINARS, " + NumberToWords(Convert.ToInt32(s2)) + "millimes";

            return (s);
        }

        private void textEdit1_Leave(object sender, EventArgs e)
        {
            
        }

        private void facture_FormClosing(object sender, FormClosingEventArgs e)
        {
            fun.update_id_facture(liste_cde_client.id_fact, textBox1.Text);
           
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {

            {
                DataTable client = new DataTable();
                client = fun.get_cltByDesign(labelControl12.Text);
                Graphics grafic = e.Graphics;
                Font font = new Font("Courrier New", 12);
                float fontHeight = font.GetHeight();
                Point p1 = new Point(150, 200);
                Point p2 = new Point(150, 230);
                String uneDate = System.DateTime.Now.ToShortDateString();
                Pen p = new Pen(Brushes.Black, 1.2f);

                //  Image newImage = Image.FromFile("C:/1.png");
                // grafic.FillRectangle(Brushes.White, new Rectangle(40, 30, 250, 30));
                // grafic.DrawImage(newImage, 60, 60);
                grafic.DrawArc(p, new Rectangle(40, 200, 250, 30), 180, 90);
                //  grafic.DrawRectangle(p, new Rectangle(40, 200, 250, 30));
                DrawRoundedRectangle(grafic, new Rectangle(40, 200, 250, 30), 20, p, Color.White);
                //grafic.FillRectangle(Brushes.White, new Rectangle(40, 200, 250, 30));
                //grafic.DrawRectangle(p, new Rectangle(40, 200, 250, 30));
                grafic.DrawString("Document", new Font("Courrier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(60, 204, 250, 30));
                grafic.DrawString("FACTURE", new Font("Courrier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(170, 204, 250, 30));
                grafic.DrawString("Numéro:", new Font("Courrier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), 50, 250);
                grafic.DrawString(numero_, new Font("Courrier New", 10), new SolidBrush(Color.Black), 150, 250);
                grafic.DrawString("Date:", new Font("Courrier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), 50, 280);
                grafic.DrawString(Convert.ToDateTime( labelControl25.Text).ToShortDateString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), 140, 280);
                grafic.DrawLine(p, p1, p2);
                DrawRoundedRectangle(grafic, new Rectangle(400, 200, 380, 120), 30,p, Color.White);
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
                grafic.DrawString("Prix en", new Font("Courrier New",9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(60, 402, 100, 30));
                grafic.DrawString("TND", new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(70, 422, 100, 30));


                grafic.FillRectangle(Brushes.White, new Rectangle(140, 400, 250, 40));
                grafic.DrawRectangle(p, new Rectangle(140, 400, 250, 40));
                grafic.DrawString("Bon de livraison N°", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(200, 402, 250, 30));
                grafic.DrawString(textBox1.Text, new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(210, 420, 100, 30));
                grafic.DrawString("du", new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(230, 420, 100, 30));
                grafic.DrawString(Convert.ToDateTime(labelControl25.Text).ToShortDateString(), new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(250, 420, 100, 30));
                // grafic.DrawString("ttt", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(190, 420, 100, 30));


                grafic.FillRectangle(Brushes.White, new Rectangle(390, 400, 210, 40));
                grafic.DrawRectangle(p, new Rectangle(390, 400, 210, 40));
                grafic.DrawString("Ref.Commande", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(430, 402, 250, 30));

                grafic.FillRectangle(Brushes.White, new Rectangle(600, 400, 180, 40));
                grafic.DrawRectangle(p, new Rectangle(600, 400, 180, 40));
                grafic.DrawString("Mode de paiement", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(640, 402, 180, 30));
                grafic.DrawString(client.Rows[0][13].ToString(), new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(660, 420, 340, 150));

                grafic.FillRectangle(Brushes.White, new Rectangle(40, 440, 100, 60));
                grafic.DrawRectangle(p, new Rectangle(40, 440, 100, 60));
                grafic.DrawString("Code", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(70, 442, 100, 60));



                grafic.FillRectangle(Brushes.White, new Rectangle(140, 440, 250, 60));
                grafic.DrawRectangle(p, new Rectangle(140, 440, 250, 60));
                grafic.DrawString("Désignation", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(210, 442, 250, 60));

                grafic.FillRectangle(Brushes.White, new Rectangle(390, 440, 50, 60));
                grafic.DrawRectangle(p, new Rectangle(390, 440, 50, 60));
                grafic.DrawString("UN", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(392, 442, 50, 60));

                grafic.FillRectangle(Brushes.White, new Rectangle(440, 440, 50, 60));
                grafic.DrawRectangle(p, new Rectangle(440, 440, 50, 60));
                grafic.DrawString("Qté", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(450, 442, 50, 60));

                grafic.FillRectangle(Brushes.White, new Rectangle(490, 440, 60, 60));
                grafic.DrawRectangle(p, new Rectangle(490, 440, 60, 60));
                grafic.DrawString("PUHT", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(500, 442, 60, 60));

                grafic.FillRectangle(Brushes.White, new Rectangle(550, 440, 50, 60));
                grafic.DrawRectangle(p, new Rectangle(550, 440, 50, 60));
                grafic.DrawString("%REM", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(552, 442, 50, 60));

                grafic.FillRectangle(Brushes.White, new Rectangle(600, 440, 60, 60));
                grafic.DrawRectangle(p, new Rectangle(600, 440, 60, 60));
                grafic.DrawString("PU N HT", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(603, 442, 60, 60));

                grafic.FillRectangle(Brushes.White, new Rectangle(660, 440, 70, 60));
                grafic.DrawRectangle(p, new Rectangle(660, 440, 70, 60));
                grafic.DrawString("PTHT", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(670, 442, 70, 60));

                grafic.FillRectangle(Brushes.White, new Rectangle(730, 440, 50, 60));
                grafic.DrawRectangle(p, new Rectangle(730, 440, 50, 60));
                grafic.DrawString("TVA", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(740, 442, 50, 60));



                grafic.FillRectangle(Brushes.White, new Rectangle(40, 460, 100, 380));
                grafic.DrawRectangle(p, new Rectangle(40, 460, 100, 380));
                // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(40, 462, 150, 380));

                grafic.FillRectangle(Brushes.White, new Rectangle(140, 460, 250, 380));
                grafic.DrawRectangle(p, new Rectangle(140, 460, 250, 380));
                // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20));

                grafic.FillRectangle(Brushes.White, new Rectangle(390, 460, 50, 380));
                grafic.DrawRectangle(p, new Rectangle(390, 460, 50, 380));
                // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

                grafic.FillRectangle(Brushes.White, new Rectangle(440, 460, 50, 380));
                grafic.DrawRectangle(p, new Rectangle(440, 460, 50, 380));
                // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

                grafic.FillRectangle(Brushes.White, new Rectangle(490, 460, 60, 380));
                grafic.DrawRectangle(p, new Rectangle(490, 460, 60, 380));
                // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

                grafic.FillRectangle(Brushes.White, new Rectangle(550, 460, 50, 380));
                grafic.DrawRectangle(p, new Rectangle(550, 460, 50, 380));
                // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

                grafic.FillRectangle(Brushes.White, new Rectangle(600, 460, 60, 380));
                grafic.DrawRectangle(p, new Rectangle(600, 460, 60, 380));
                // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

                grafic.FillRectangle(Brushes.White, new Rectangle(660, 460, 70, 380));
                grafic.DrawRectangle(p, new Rectangle(660, 460, 70, 380));
                // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

                grafic.FillRectangle(Brushes.White, new Rectangle(730, 460, 50, 380));
                grafic.DrawRectangle(p, new Rectangle(730, 460, 50, 380));
                // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))
                int r = 0;
                StringFormat sf = new StringFormat();
                //sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;
                
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    DataRow row = gridView1.GetDataRow(i);
                    // grafic.DrawRectangle(p, new Rectangle(10, 300+r, 80, 30));
                    codetva = row[13].ToString();
                    grafic.DrawString(row[1].ToString(), new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(50, 470 + r, 150, 30));
                    grafic.DrawString(row[2].ToString(), new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(150, 470 + r, 200, 30), sf);
                    grafic.DrawString(row[11].ToString(), new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(390, 470 + r, 50, 30), sf);
                    grafic.DrawString(row[3].ToString(), new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(450, 470 + r, 50, 30), sf);
                    grafic.DrawString(String.Format("{0:0.000}", Convert.ToDouble(row[9].ToString().Replace('.', ','))), new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(490, 470 + r, 60, 30), sf);
                    grafic.DrawString(row[14].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(560, 470 + r, 50, 30), sf);
                    grafic.DrawString(String.Format("{0:0.000}", Convert.ToDouble(row[9].ToString().Replace('.', ','))), new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(601, 470 + r, 70, 30), sf);
                    grafic.DrawString(String.Format("{0:0.000}", Convert.ToDouble(row[10].ToString().Replace('.', ','))), new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(661, 470 + r, 70, 30), sf);
                    grafic.DrawString(row[13].ToString(), new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(740, 470 + r, 60, 30));
                    r = r + 30;
                }
                Double total = Convert.ToDouble(labelControl23.Text.Replace('.', ',')) + Convert.ToDouble(labelControl23.Text.Replace('.', ',')) * 18 / 100;
                // DrawRoundedRectangle(grafic, new Rectangle(40, 860, 310, 40), 20, p, Color.White);
               grafic.FillRectangle(Brushes.Gainsboro, new Rectangle(40, 860, 70, 20));
               grafic.DrawRectangle(p, new Rectangle(40, 860, 70, 20));
              //  DrawRoundedRectangle1(grafic, new Rectangle(40, 860, 70, 20), 20, p, Color.White);
                grafic.DrawString("Base HT", new Font("Courrier New", 8, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(45, 863, 70, 20));
                grafic.FillRectangle(Brushes.Gainsboro, new Rectangle(110, 860, 70, 20));
                grafic.DrawRectangle(p, new Rectangle(110, 860, 70, 20));
                grafic.DrawString("Code TVA", new Font("Courrier New", 8, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(110, 863, 70, 20));
                grafic.FillRectangle(Brushes.Gainsboro, new Rectangle(180, 860, 70, 20));
                grafic.DrawRectangle(p, new Rectangle(180, 860, 70, 20));
                grafic.DrawString("Taux TVA", new Font("Courrier New",8, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(185, 863, 70, 20));
                grafic.FillRectangle(Brushes.Gainsboro, new Rectangle(250, 860, 85, 20));
                grafic.DrawRectangle(p, new Rectangle(250, 860, 85, 20));
                grafic.DrawString("Montant TVA", new Font("Courrier New", 8, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(260, 863, 100, 20));


                grafic.FillRectangle(Brushes.White, new Rectangle(40, 880, 70, 20));
                grafic.DrawRectangle(p, new Rectangle(40, 880, 70, 20));

                grafic.DrawString(String.Format("{0:0.000}", Convert.ToDouble(labelControl23.Text.Replace('.', ','))), new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(40, 882, 60, 20));
                grafic.FillRectangle(Brushes.White, new Rectangle(110, 880, 70, 20));
                grafic.DrawRectangle(p, new Rectangle(110, 880, 70, 20));
                //grafic.DrawString("TVA", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(750, 442, 60, 20));
                grafic.DrawString(codetva, new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(130, 882, 60, 20));

                grafic.FillRectangle(Brushes.White, new Rectangle(180, 880, 70, 20));
                grafic.DrawRectangle(p, new Rectangle(180, 880, 70, 20));
                //grafic.DrawString("TVA", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(750, 442, 60, 20));
                grafic.DrawString(String.Format("{0:0.00}", Convert.ToDouble(codetva)), new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(190, 882, 60, 20));

                grafic.FillRectangle(Brushes.White, new Rectangle(250, 880, 85, 20));
                grafic.DrawRectangle(p, new Rectangle(250, 880, 85, 20));
                //grafic.DrawString("TVA", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(750, 442, 60, 20));
                grafic.DrawString(String.Format("{0:0.000}", total), new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(260, 882, 100, 20));

                DrawRoundedRectangle(grafic, new Rectangle(450, 860, 330, 100), 20, p, Color.White);
                //grafic.FillRectangle(Brushes.White, new Rectangle(450, 860, 330, 20));
                //grafic.DrawRectangle(p, new Rectangle(450, 860, 330, 20));
                Point p3 = new Point(450,880);
                Point p4 = new Point(780, 880);

                Point p5 = new Point(450, 940);
                Point p6 = new Point(780, 940);
                StringFormat format = new StringFormat();
                //format.LineAlignment = StringAlignment.Center;
                format.Alignment = StringAlignment.Near;
                grafic.DrawString("Total HT", new Font("Courrier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(460, 862, 330, 20));

                grafic.DrawString(String.Format("{0:0.000}", Convert.ToDouble(labelControl23.Text.Replace('.', ','))), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(700, 862, 330, 20), format);
                grafic.DrawLine(p, p3, p4);

                grafic.FillRectangle(Brushes.White, new Rectangle(450, 880, 330, 20));
                grafic.DrawRectangle(p, new Rectangle(450, 880, 330, 20));
                grafic.DrawString("Total net HT", new Font("Courrier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(460, 882, 330, 20));
                grafic.DrawString(String.Format("{0:0.000}", Convert.ToDouble(labelControl23.Text.Replace('.', ','))), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(700, 882, 330, 20));



                grafic.FillRectangle(Brushes.White, new Rectangle(450, 900, 330, 20));
                grafic.DrawRectangle(p, new Rectangle(450, 900, 330, 20));
                grafic.DrawString("Total TVA", new Font("Courrier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(460, 902, 330, 20));
                grafic.DrawString(String.Format("{0:0.000}", total), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(700, 902, 330, 20));

                grafic.FillRectangle(Brushes.White, new Rectangle(450, 920, 330, 20));
                grafic.DrawRectangle(p, new Rectangle(450, 920, 330, 20));
                grafic.DrawString("Timbre fiscal", new Font("Courrier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(460, 922, 330, 20));
             
                grafic.DrawString("0.500", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(700, 922, 330, 20));
                Double ttc = total + 0.500;

                grafic.DrawLine(p, p5, p6);
                //grafic.FillRectangle(Brushes.White, new Rectangle(450, 940, 330, 20));
                //grafic.DrawRectangle(p, new Rectangle(450, 940, 330, 20));
                grafic.DrawString("Total TTC", new Font("Courrier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(460, 942, 330, 20));
                grafic.DrawString(String.Format("{0:0.000}", ttc), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(700, 942, 330, 20));
              
                StringFormat stringFormat = new StringFormat();
                stringFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
               // grafic.DrawString("LJHHHHH", new Font("Courrier New", 9), new SolidBrush(Color.Black), 50,1100);
               // grafic.DrawString("LJHHHHHKKKKKKKKKKKK", new Font("Courrier New", 9), new SolidBrush(Color.Black), 50, 1130);
                grafic.DrawString("Arretée la présente facture en TTC à la somme de :", new Font("Courrier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), 40, 950);

                grafic.DrawString(NumberToWords1(String.Format("{0:0.000}", ttc)), new Font("Courrier New", 10), new SolidBrush(Color.Black), 40, 980);
                //   grafic.DrawString("fffffffff", new Font("Courrier New", 10), new SolidBrush(Color.Black), 40, 980);
                grafic.DrawString("Le client déclaré avoir pris connaissance et accepté nos conditions générales de vente", new Font("Courrier New", 9), new SolidBrush(Color.Black), 40, 1050);
                /*      grafic.DrawString("Z.L M'ghira 3-Rue Nabeul-2082-Fouchana.Tunisia/Email:info@aquatun.com/website: www.aquatun.com", new Font("Courrier New", 8), new SolidBrush(Color.Black), 40, 1050);
                      grafic.DrawString("Tél : (+216)7A 409 215- (+216)71 409 237/ Fax: +216 71 409 223", new Font("Courrier New", 8), new SolidBrush(Color.Black), 40, 1070);
                      grafic.DrawString("Sarl.cap.Soc 75 000.000 TND-TVA:1214920 C/A/MOOO-RCN°B24161292011-CD : 1214920C", new Font("Courrier New", 8), new SolidBrush(Color.Black), 40, 1090);
                      grafic.DrawString("RIB:BIAT 08047020021000116515/BTK 200060600011211369270", new Font("Courrier New", 8), new SolidBrush(Color.Black), 40, 1110);
                      */
            }
        }

      
        
    }
}