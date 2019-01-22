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
namespace RibbonSimplePad
{
    public partial class test : DevExpress.XtraEditors.XtraForm
    {
       
        public test()
        {
            InitializeComponent();
           
        }

        sql_gmao fun = new sql_gmao();
       
        private void facture_Load(object sender, EventArgs e)
        {
           
      
            layoutControl1.Location = new Point(
             this.ClientSize.Width / 2 - layoutControl1.Size.Width / 2,
      6);
           // layoutControl1.Anchor = AnchorStyles.None; 


           


            comboBoxEdit3.Text = "DT";
            comboBoxEdit4.Text = "DT";
            double total_ht=0;

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
               // pictureEdit1.Image = Image.FromStream(mem);
            }
            else { //pictureEdit1.Image = null; 
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



            for (int i = 0; i < gridView1.DataRowCount; i++)
            {
                DataRow row = gridView1.GetDataRow(i);
                int totP = Convert.ToInt32(row[3]) * Convert.ToInt32(row[9]);
                total_ht += totP;
                labelControl40.Text = total_ht.ToString(); ;

            }

            if (barButtonItem2.Enabled == false)
            {

                
                DataTable gg = new DataTable();
                gg = fun.get_etat_devv(liste_devis.id_devis);
                comboBoxEdit1.Text=gg.Rows[0]["remise"].ToString();
                comboBoxEdit2.Text = gg.Rows[0]["tva"].ToString();
                labelControl40.Text = gg.Rows[0]["montant_ht"].ToString();
                labelControl31.Text = gg.Rows[0]["montant_ttc"].ToString();
                comboBoxEdit3.Text = gg.Rows[0]["dev"].ToString();
                comboBoxEdit4.Text = gg.Rows[0]["dev"].ToString();


                label1.Text = gg.Rows[0]["timbre"].ToString();



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
                    label1.Enabled = false;


               
                
               
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
            int fretcable = 0;
            int installmisajour = 0;
            value_before_remise = Convert.ToDouble(labelControl40.Text);
            if (checkEdit2.Checked == true)
            {
                if (comboBoxEdit1.Text == "")
                {
                    XtraMessageBox.Show("Choisir ou saisir un taux de remise ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    value_before_remise = Convert.ToDouble(labelControl40.Text);
                    temp = (value_before_remise * Convert.ToDouble(comboBoxEdit1.Text)) / 100;
                    value_after_remise = value_before_remise - temp;
                  

                    if (comboBoxEdit2.Text == "")
                    {
                        XtraMessageBox.Show("Choisir ou saisir un taux de TVA ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    else
                    {
                        temp2 = (value_after_remise * Convert.ToDouble(comboBoxEdit2.Text)) / 100;
                        //timbre = Int32.Parse(textEdit1.Text.ToString());
                        timbre = Convert.ToDouble(label1.Text);
                        if (textBox2.Text == "" || textBox3.Text == "")
                        {
                            XtraMessageBox.Show("insérez les valeurs manquants");

                        }
                        else
                        {
                            fretcable = int.Parse(textBox2.Text);
                            installmisajour = int.Parse(textBox3.Text);
                            final = value_after_remise + temp2 + timbre + fretcable + installmisajour;
                            labelControl31.Text = final.ToString();
                        }
                    }


                }
            }

            else
            {

                value_after_remise = value_before_remise;
                temp2 = (value_after_remise * Convert.ToDouble(comboBoxEdit2.Text)) / 100;
                //timbre = Int32.Parse(textEdit1.Text.ToString());
                timbre = Convert.ToDouble(label1.Text);
                if (textBox2.Text == "" || textBox3.Text == "")
                {
                    XtraMessageBox.Show("insérez les valeurs manquants");

                }
                else
                {
                    fretcable = int.Parse(textBox2.Text);
                    installmisajour = int.Parse(textBox3.Text);
                    final = value_after_remise + temp2 + timbre + fretcable + installmisajour;
                    labelControl31.Text = final.ToString();
                }
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

        private void xtraScrollableControl2_Click(object sender, EventArgs e)
        {

        }

        private void labelControl12_Click(object sender, EventArgs e)
        {

        }

        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl31_Click(object sender, EventArgs e)
        {

        }

        
       

       
    }
}