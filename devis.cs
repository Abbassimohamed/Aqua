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
using DevExpress.XtraGrid.Columns;
using System.Drawing.Drawing2D;
namespace RibbonSimplePad
{
    public partial class devis : DevExpress.XtraEditors.XtraForm
    {
       
        public devis()
        {
            InitializeComponent();

            DataTable ff = new DataTable();
            ff = fun.get_etat_devv(liste_devis.id_devis);

            string etat = ff.Rows[0]["etat"].ToString();

            if (etat == "envoyé")
            {
                barEditItem2.Enabled = false;
            }
           
        }

        sql_gmao fun = new sql_gmao();
        double total_ht = 0;
        private void facture_Load(object sender, EventArgs e)
        {
           
      
            layoutControl1.Location = new Point(
             this.ClientSize.Width / 2 - layoutControl1.Size.Width / 2,
      6);
          
            comboBoxEdit3.Text = "DT";
            comboBoxEdit4.Text = "DT";
            
            comboBoxEdit1.Visible = false;
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = fun.get_piece_from_devis(liste_devis.id_devis);
            DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit mEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            gridControl1.RepositoryItems.Add(mEdit);
            this.gridView1.Columns[0].Visible = false;
            this.gridView1.Columns[1].Caption = "Ref";
            this.gridView1.Columns[2].Caption = "Désignation";
            gridView1.Columns[2].ColumnEdit = mEdit;
            this.gridView1.Columns[3].Caption = "QTE";
            this.gridView1.Columns[4].Visible = false;
            this.gridView1.Columns[5].Visible = false;
            this.gridView1.Columns[6].Visible = false;
            this.gridView1.Columns[7].Visible = false;
            this.gridView1.Columns[8].Visible = false;
            this.gridView1.Columns[9].Caption = "Prix Unit HT";
            this.gridView1.Columns[10].Caption = "Montant HT";

            gridView1.OptionsView.ShowAutoFilterRow = true;

            gridView1.BestFitColumns();

           

            DataTable ff = new DataTable();
            ff = fun.get_etat_devv(liste_devis.id_devis);
            
            string etat = ff.Rows[0]["etat"].ToString();
            string date_envoie = ff.Rows[0]["date_envoie"].ToString();
            string client = ff.Rows[0]["client"].ToString();
            string id_clt = ff.Rows[0]["id_clt"].ToString(); ;
            barEditItem3.EditValue = ff.Rows[0]["comm_fact"].ToString();
            if(etat=="envoyé")
            {
                barEditItem2.Enabled=false;
            }
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
                  }
            else {
            }
            //information sur la société
            DataTable cc = new DataTable();
            cc = fun.affiche_infos_societe();

            labelControl13.Text = cc.Rows[0]["nom_societe"].ToString();
            labelControl14.Text = cc.Rows[0]["adresse_societe"].ToString();
            labelControl15.Text = cc.Rows[0]["matricule_societe"].ToString();
           
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
            if (date_envoie != "") { labelControl25.Text = date_envoie; }
            else { labelControl25.Text = System.DateTime.Now.ToLongDateString(); }


            if (labelControl1.Text.Length == 1)
            { labelControl1.Text = "000"; }
            else if (labelControl1.Text.Length == 2)
            { labelControl1.Text = "00"; }
            else if (labelControl1.Text.Length == 3)
            { labelControl1.Text = "0"; }
            else { labelControl1.Text = ""; }

            labelControl1.Text += liste_devis.id_devis;
            this.Text += labelControl1.Text;


            total_ht = 0;
            for (int i = 0; i < gridView1.DataRowCount; i++)
            {
                DataRow row = gridView1.GetDataRow(i);
                int totP = Convert.ToInt32(row[3].ToString().Replace('.',',')) * Convert.ToInt32(row[9].ToString().Replace('.',','));
                total_ht += totP;
                

            }
            labelControl40.Text = total_ht.ToString();
            labelControl31.Text = total_ht.ToString();
            if (etat == "envoyé")
            {
                barButtonItem2.Enabled = false;
            }
            if (barButtonItem2.Enabled == false)
            {

                
                DataTable gg = new DataTable();
                gg = fun.get_etat_devv(liste_devis.id_devis);
                comboBoxEdit1.Text = gg.Rows[0]["remise"].ToString();
                comboBoxEdit2.Text = gg.Rows[0]["tva"].ToString();
                labelControl40.Text = gg.Rows[0]["montant_ht"].ToString();
                labelControl31.Text = gg.Rows[0]["montant_ttc"].ToString();
                comboBoxEdit3.Text = gg.Rows[0]["dev"].ToString();
                comboBoxEdit4.Text = gg.Rows[0]["dev"].ToString();


                comboBoxEdit5.Text = gg.Rows[0]["timbre"].ToString();



                if (comboBoxEdit1.Text == "")
                {
                    checkEdit2.Checked = false;
                    comboBoxEdit1.Visible = false;
                    // labelControl26.Visible = false;
                }
                else
                {

                    comboBoxEdit1.Visible = true;
                    checkEdit2.Checked = true;
                }
                comboBoxEdit1.Enabled = false;
                comboBoxEdit2.Enabled = false;
                comboBoxEdit3.Enabled = false;
                comboBoxEdit4.Enabled = false;
                    checkEdit2.Enabled= false;
                    comboBoxEdit5.Enabled = false;


               
                
               
            }


        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit2.Checked == true)
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

            double temp, temp2, final;

            double timbre = 0;
            double value_after_remise = 0;
            double value_before_remise = 0;
            value_before_remise = Convert.ToDouble(labelControl40.Text);
            if (checkEdit2.Checked == true)
            {
                if (comboBoxEdit1.Text == "")
                {
                    XtraMessageBox.Show("Choisir ou saisir un taux de remise ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    //value_before_remise = Convert.ToDouble(labelControl40.Text);
                    //temp = (value_before_remise * Convert.ToDouble(comboBoxEdit1.Text)) / 100;
                    value_after_remise = Convert.ToDouble(labelControl40.Text);
                   // labelControl40.Text = value_after_remise.ToString();
                  

                    if (comboBoxEdit2.Text == "")
                    {
                        XtraMessageBox.Show("Choisir ou saisir un taux de TVA ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    else
                    {
                        temp2 = (value_after_remise * Convert.ToDouble(comboBoxEdit2.Text)) / 100;
                        //timbre = Int32.Parse(textEdit1.Text.ToString());
                        timbre = 0;
                        double.TryParse(comboBoxEdit5.Text.Replace('.', ','),out timbre);
                       
                        
                            final = value_after_remise + temp2 + timbre ;
                            labelControl31.Text = final.ToString();
                        
                    }


                }
            }

            else
            {

                value_after_remise = Convert.ToDouble(labelControl40.Text);
                temp2 = (value_after_remise * Convert.ToDouble(comboBoxEdit2.Text)) / 100;
                //timbre = Int32.Parse(textEdit1.Text.ToString());
                timbre = 0;// Convert.ToDouble(label1.Text);
                double.TryParse(comboBoxEdit5.Text.Replace('.', ','), out timbre);
                   
                    final = value_after_remise + temp2 + timbre ;
                    labelControl31.Text = final.ToString();
               
            }
        
        }

        private void comboBoxEdit1_TextChanged(object sender, EventArgs e)
        {

        }



        private void comboBoxEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {

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
            if (checkEdit2.Checked == true && comboBoxEdit1.Text == "")
            {
                XtraMessageBox.Show("Choisir ou saisir un taux de remise avant ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 
            }

            else if (comboBoxEdit2.Text == "")
            {
                XtraMessageBox.Show("Choisir ou saisir un taux de TVA avant ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            else
            {


                ////allow only decimal

                //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                //{
                //    e.Handled = true;
                //}

                //// only allow one decimal point
                //if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                //{
                //    e.Handled = true;
                //}

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
           
            layoutControl1.ShowRibbonPrintPreview();
            
           
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            string etat= "envoyé";

           

            if (checkEdit2.Checked == true)
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

            fun.update_etat_devis(etat, liste_devis.id_devis, System.DateTime.Now.ToLongDateString(), labelControl40.Text, labelControl31.Text, comboBoxEdit2.Text, comboBoxEdit1.Text, label1.Text, comboBoxEdit3.Text);

                        barButtonItem2.Enabled = false;
                        barButtonItem2.Caption = "Devis envoyé";
                      
            
        }

        private void repositoryItemTextEdit1_Leave(object sender, EventArgs e)
        {
            fun.update_commentaire_devis(liste_devis.id_devis, barEditItem3.EditValue.ToString());
        }

        private void labelControl3_Click(object sender, EventArgs e)
        {

        }

        private void labelControl34_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void labelControl34_EditValueChanged(object sender, EventArgs e)
        {

            if (checkEdit2.Checked == true)
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

        private void labelControl34_KeyPress(object sender, KeyPressEventArgs e)
        {

           
           
        }

        private void devis_Activated(object sender, EventArgs e)
        {


            if (login1.depart == "Utilisateur")
            {
                //exporter droit
                if (login1.devis_doc == "OUI") { barButtonItem1.Enabled= true; }
                else { barButtonItem1.Enabled = false; }
                

            }
            if (login1.depart == "Administrateur")
            {
                barButtonItem1.Enabled = true;

            }
            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridView1.BestFitColumns();
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.EnableAppearanceEvenRow = true;


            Form1.load = 1;

            Form1.wait = 1;
        }

        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkEdit2.Checked == true)
                {

                    comboBoxEdit1.Visible = true;
                    
                    double tva = 0;
                    double remise = 0;

                    if (double.TryParse(comboBoxEdit2.EditValue.ToString(), out tva) && double.TryParse(comboBoxEdit1.EditValue.ToString(), out remise))
                    {
                        double prix_ht = total_ht; 
                        remise = (prix_ht * remise) / 100;
                        labelControl40.Text=string.Format("{0}",prix_ht-remise);
                        double timbre = 0;
                        double.TryParse(comboBoxEdit5.Text.Replace('.', ','), out timbre);
                        prix_ht -=remise;
                        double prix_final = prix_ht + ((prix_ht * tva) / 100)  + timbre;
                        labelControl31.Text = string.Format("{0}", prix_final);
                    }
                }
                else
                {
                    comboBoxEdit1.Visible = false;
                    labelControl40.Text = string.Format("{0}", total_ht);
                    double prix_ht = double.Parse(labelControl40.Text.Replace('.', ','));
                    double tva = 0;
                    if (double.TryParse(comboBoxEdit2.EditValue.ToString(), out tva))
                    {
                        labelControl31.Text = "";
                        double timbre = 0;
                        double.TryParse(comboBoxEdit5.Text.Replace('.', ','), out timbre);
                        double prix_final = prix_ht + ((prix_ht * tva) / 100)+timbre;
                        labelControl31.Text = string.Format("{0}", prix_final);
                    }
                }
            }

            catch (Exception ex) { }
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (barButtonItem2.Enabled == true)
            {
                calcule();
            }
        }

        private void comboBoxEdit2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxEdit1_TextChanged_1(object sender, EventArgs e)
        {
            if (barButtonItem2.Enabled == true)
            {
                double remise=0;
                if (double.TryParse(comboBoxEdit1.EditValue.ToString(), out remise))
                {
                    double prix_ht = total_ht;
                    remise = (prix_ht * remise) / 100;
                    labelControl40.Text = string.Format("{0}", prix_ht - remise);
                }
                calcule();
            }
        }

        private void comboBoxEdit5_TextChanged(object sender, EventArgs e)
        {
            if (barButtonItem2.Enabled == true)
            {
                calcule();
            }
        }

        private void barButtonItem2_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            string etat = "envoyé";



            if (checkEdit2.Checked == true)
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

            fun.update_etat_devis(etat, liste_devis.id_devis, System.DateTime.Now.ToLongDateString(), labelControl40.Text, labelControl31.Text, comboBoxEdit2.Text, comboBoxEdit1.Text, comboBoxEdit5.Text, comboBoxEdit3.Text);

            barButtonItem2.Enabled = false;
            barButtonItem2.Caption = "Devis envoyé";
        }

        private void barButtonItem1_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            printPreviewDialog1.ShowDialog();
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

            s = NumberToWords(Convert.ToInt32(s1)) + " DINARS, " + NumberToWords(Convert.ToInt32(s2)) + " millimes";

            return (s);
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
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            {
                DataTable client = new DataTable();
                client = fun.get_cltByDesign(labelControl12.Text);
                Graphics grafic = e.Graphics;
                Font font = new Font("Courrier New", 12);
                float fontHeight = font.GetHeight();
                String uneDate = System.DateTime.Now.ToShortDateString();
                Pen p = new Pen(Brushes.Black, 1.2f);
                grafic.DrawString("Devis PROFORMA", new Font("Courrier New", 14, FontStyle.Bold), new SolidBrush(Color.Black), 300, 150);

                //System.Drawing.Graphics g = this.CreateGraphics();
                //g.FillRoundedRectangle(brush, x, y, width, height, arcRadius);
                //g.drawronded
                Rectangle rec = new Rectangle(100, 100, 100, 100);
                DrawRoundedRectangle(grafic, new Rectangle(40, 200, 270, 80), 30, p, Color.Gainsboro);
                grafic.DrawString("N°:", new Font("Courrier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(45, 210, 270, 70));
                grafic.DrawString(labelControl1.Text, new Font("Courrier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(145, 210, 270, 70));
                grafic.DrawString("Date:", new Font("Courrier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(45, 235, 270, 70));
                grafic.DrawString(labelControl25.Text, new Font("Courrier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(145, 235, 270, 70));
                grafic.DrawString("Code Client:", new Font("Courrier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(45, 260, 270, 70));
                grafic.DrawString(client.Rows[0][0].ToString(), new Font("Courrier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(145, 260, 270, 70));

                DrawRoundedRectangle(grafic, new Rectangle(320, 200, 480, 80), 30, p, Color.Gainsboro);
                grafic.DrawString("Droit M :", new Font("Courrier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(325, 210, 480, 80));
                grafic.DrawString("Adresse:", new Font("Courrier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(325, 235, 480, 80));
                grafic.DrawString(client.Rows[0][6].ToString(), new Font("Courrier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(425, 235, 480, 80));
                grafic.DrawString("TVA Client:", new Font("Courrier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(325, 260, 480, 80));

                DrawRoundedRectangle(grafic, new Rectangle(40, 290, 760, 470), 30, p, Color.White);
                Point p1 = new Point(40, 320);
                Point p2 = new Point(800, 320);
                grafic.DrawLine(p, p1, p2);
                grafic.DrawString("Réference", new Font("Courrier New", 8, FontStyle.Bold), new SolidBrush(Color.Black), 50, 295);
                Point p3 = new Point(110, 290);
                Point p4 = new Point(110, 760);
                grafic.DrawLine(p, p3, p4);
                grafic.DrawString("Désignation", new Font("Courrier New", 8, FontStyle.Bold), new SolidBrush(Color.Black), 180, 295);


                Point p5 = new Point(330, 290);
                Point p6 = new Point(330, 760);
                grafic.DrawLine(p, p5, p6);
                grafic.DrawString("Quantité", new Font("Courrier New", 8, FontStyle.Bold), new SolidBrush(Color.Black), 340, 295);


                Point p7 = new Point(400, 290);
                Point p8 = new Point(400, 760);
                grafic.DrawLine(p, p7, p8);
                grafic.DrawString("Prix Unit.", new Font("Courrier New", 8, FontStyle.Bold), new SolidBrush(Color.Black), 410, 292);
                grafic.DrawString("HT", new Font("Courrier New", 8, FontStyle.Bold), new SolidBrush(Color.Black), 422, 305);

                Point p9 = new Point(480, 290);
                Point p10 = new Point(480, 760);
                grafic.DrawLine(p, p9, p10);
                grafic.DrawString("Prix Unit.", new Font("Courrier New", 8, FontStyle.Bold), new SolidBrush(Color.Black), 505, 292);
                grafic.DrawString("TTC", new Font("Courrier New", 8, FontStyle.Bold), new SolidBrush(Color.Black), 522, 305);

                Point p11 = new Point(580, 290);
                Point p12 = new Point(580, 760);
                grafic.DrawLine(p, p11, p12);
                grafic.DrawString("Remise", new Font("Courrier New", 8, FontStyle.Bold), new SolidBrush(Color.Black), 590, 295);

                Point p13 = new Point(650, 290);
                Point p14 = new Point(650, 760);
                grafic.DrawLine(p, p13, p14);
                grafic.DrawString("Montant Net", new Font("Courrier New", 8, FontStyle.Bold), new SolidBrush(Color.Black), 660, 292);
                grafic.DrawString("HT", new Font("Courrier New", 8, FontStyle.Bold), new SolidBrush(Color.Black), 680, 305);

                Point p15 = new Point(750, 290);
                Point p16 = new Point(750, 760);
                grafic.DrawLine(p, p15, p16);
                grafic.DrawString("Taux", new Font("Courrier New", 8, FontStyle.Bold), new SolidBrush(Color.Black), 760, 292);
                grafic.DrawString("TVA", new Font("Courrier New", 8, FontStyle.Bold), new SolidBrush(Color.Black), 760, 305);

                int r = 0;
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    DataRow row = gridView1.GetDataRow(i);
                    // grafic.DrawRectangle(p, new Rectangle(10, 300+r, 80, 30));
                    //codetva = row[13].ToString();
                    grafic.DrawString(row[1].ToString(), new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(50, 340 + r, 150, 30));
                    grafic.DrawString(row[2].ToString(), new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(120, 340 + r, 200, 30));
                    grafic.DrawString(row[3].ToString(), new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(340, 340 + r, 50, 30));
                    grafic.DrawString(row[9].ToString(), new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(410, 340 + r, 50, 30));
                   // grafic.DrawString(String.Format("{0:0.000}", Convert.ToDouble(row[14].ToString().Replace('.', ','))), new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(590, 340 + r, 60, 30));
                    grafic.DrawString(row[10].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(660, 340 + r, 50, 30));
                    // grafic.DrawString(String.Format("{0:0.000}", Convert.ToDouble(row[13].ToString().Replace('.', ','))), new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(750, 340 + r, 70, 30));


                    r = r + 30;
                }


                DrawRoundedRectangle(grafic, new Rectangle(40, 770, 100, 30), 20, p, Color.White);
                grafic.DrawString("TAUX", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(70, 775, 100, 30));
                DrawRoundedRectangle(grafic, new Rectangle(40, 810, 100, 100), 20, p, Color.White);
                DrawRoundedRectangle(grafic, new Rectangle(40, 920, 360, 30), 20, p, Color.White);
                grafic.DrawString("TOTAUX", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(55, 925, 360, 30));
                grafic.DrawString("Arretée le présent Proformat à la somme de:", new Font("Courrier New", 9), new SolidBrush(Color.Black), 55, 960);
                grafic.DrawString(NumberToWords1(labelControl31.Text), new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), 55, 980);
                DrawRoundedRectangle(grafic, new Rectangle(40, 1000, 100, 80), 20, p, Color.White);
                grafic.DrawString("N.B.", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(80, 1030, 100, 80));

                DrawRoundedRectangle(grafic, new Rectangle(150, 770, 250, 30), 20, p, Color.White);
                grafic.DrawString("BASE", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(180, 775, 250, 30));
                grafic.DrawString("MONTANT", new Font("tomate", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(310, 775, 250, 30));
                DrawRoundedRectangle(grafic, new Rectangle(150, 810, 250, 100), 20, p, Color.White);

                DrawRoundedRectangle(grafic, new Rectangle(150, 1000, 410, 80), 20, p, Color.White);


                DrawRoundedRectangle(grafic, new Rectangle(410, 770, 150, 30), 20, p, Color.White);
                grafic.DrawString("Reçu par", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(460, 775, 150, 30));
                DrawRoundedRectangle(grafic, new Rectangle(410, 810, 150, 100), 20, p, Color.White);

                DrawRoundedRectangle(grafic, new Rectangle(570, 770, 230, 140), 20, p, Color.White);
                grafic.DrawString("TOTAL HT", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(580, 775, 230, 140));
                grafic.DrawString("NET HT", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(580, 800, 230, 140));
                grafic.DrawString("BASE TVA", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(580, 835, 230, 140));
                grafic.DrawString("MT TVA", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(580, 860, 230, 140));

                DrawRoundedRectangle(grafic, new Rectangle(570, 920, 230, 60), 20, p, Color.Gainsboro);
                grafic.DrawString("A PAYER", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(580, 940, 230, 60));
                grafic.DrawString(labelControl31.Text, new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(680, 940, 230, 60));
                DrawRoundedRectangle(grafic, new Rectangle(570, 1000, 230, 80), 20, p, Color.White);
                grafic.DrawString("Service Commercial", new Font("Courrier New", 9, FontStyle.Bold), new SolidBrush(Color.Black), new Rectangle(620, 1005, 230, 80));

                StringFormat stringFormat = new StringFormat();
                stringFormat.FormatFlags = StringFormatFlags.DirectionVertical;
                //stringFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
                grafic.DrawString("Siège social", new Font("Courrier New", 8), new SolidBrush(Color.Black), 40, 1090, stringFormat);
                Point p17 = new Point(60, 1090);
                Point p18 = new Point(60, 1160);
                grafic.DrawLine(new Pen(Brushes.Red, 2f), p17, p18);
                grafic.DrawString("124 Bis Rue 13 Aout", new Font("Courrier New", 8), new SolidBrush(Color.Black), 65, 1090);
                grafic.DrawString("Z.I.Poudrière 1", new Font("Courrier New", 8), new SolidBrush(Color.Black), 65, 1110);
                grafic.DrawString("3002 Sfax Tunisie", new Font("Courrier New", 8), new SolidBrush(Color.Black), 65, 1130);

                grafic.DrawString("Tél.      Fax", new Font("Courrier New", 8), new SolidBrush(Color.Black), 200, 1090, stringFormat);
                Point p19 = new Point(220, 1090);
                Point p20 = new Point(220, 1160);
                grafic.DrawLine(new Pen(Brushes.Red, 2f), p19, p20);
                grafic.DrawString("(+216)74 286 081", new Font("Courrier New", 8), new SolidBrush(Color.Black), 225, 1090);
                grafic.DrawString("(+216)74 286 081", new Font("Courrier New", 8), new SolidBrush(Color.Black), 225, 1110);
                grafic.DrawString("(+216)74 286 081", new Font("Courrier New", 8), new SolidBrush(Color.Black), 225, 1140);

                grafic.DrawString("E-mail", new Font("Courrier New", 8), new SolidBrush(Color.Black), 360, 1090, stringFormat);
                Point p21 = new Point(380, 1090);
                Point p22 = new Point(380, 1160);
                grafic.DrawLine(new Pen(Brushes.Red, 2f), p21, p22);
                grafic.DrawString("topdistribution@planet.tn", new Font("Courrier New", 8), new SolidBrush(Color.Black), 385, 1090);

                Point p23 = new Point(550, 1090);
                Point p24 = new Point(550, 1160);
                grafic.DrawLine(new Pen(Brushes.Red, 2f), p23, p24);
                grafic.DrawString("M.F.: 1136325B/A/M 000", new Font("Courrier New", 8), new SolidBrush(Color.Black), 555, 1090);
                grafic.DrawString("R.C: B083522.2010", new Font("Courrier New", 8), new SolidBrush(Color.Black), 555, 1110);
                grafic.DrawString("RIB: 03 7000190115 007854 BNA", new Font("Courrier New", 8), new SolidBrush(Color.Black), 555, 1130);
                grafic.DrawString("C.D.: 843766K", new Font("Courrier New", 8), new SolidBrush(Color.Black), 555, 1150);
            }
        }



        
       
    }
}