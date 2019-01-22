using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace RibbonSimplePad
{
    public partial class gestionFeur : DevExpress.XtraEditors.XtraForm
    {
        public gestionFeur()
        {
            InitializeComponent();
        }
        sql_gmao fun = new sql_gmao();
        private void gestionFeur_Load(object sender, EventArgs e)
        {
            xtraTabPage2.PageVisible = false;
            xtraTabPage3.PageVisible = false;
            ListeFeur();

            gridView1.BestFitColumns();
        }

        private void ListeFeur()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = fun.get_AllFeur();
            this.gridView1.Columns[0].Caption = "Code";
            this.gridView1.Columns[1].Caption = "Raison sociale";
            this.gridView1.Columns[2].Caption = "Nom Résponsable";
            this.gridView1.Columns[4].Caption = "GSM";
            this.gridView1.Columns[3].Caption = "Téléphone";
            this.gridView1.Columns[5].Visible = false;
            this.gridView1.Columns[6].Visible = false;
            this.gridView1.Columns[7].Visible = false;
            this.gridView1.Columns[8].Visible = false;
            this.gridView1.Columns[9].Visible = false;
            this.gridView1.Columns[10].Visible = false;
            this.gridView1.Columns[11].Visible = false;
            this.gridView1.Columns[12].Visible = false;
            this.gridView1.Columns[13].Visible = false;
            this.gridView1.Columns[14].Visible = false;
            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridView1.OptionsView.EnableAppearanceEvenRow = true;
        }
        private void getFeurByCode(string code)
        {
            DataTable feur = new DataTable();
            feur = fun.get_FeurByCode(code);
            if (feur.Rows.Count != 0)
            {
                textEdit1.Text = feur.Rows[0]["code_feur"].ToString();
                textEdit2.Text = feur.Rows[0]["raison_soc"].ToString();
                textEdit3.Text = feur.Rows[0]["responsbale"].ToString();
                textEdit4.Text = feur.Rows[0]["gsm_feur"].ToString();
                textEdit5.Text = feur.Rows[0]["tel_feur"].ToString();
                textEdit6.Text = feur.Rows[0]["fax_feur"].ToString();
                textEdit7.Text = feur.Rows[0]["adresse_feur"].ToString();
                textEdit8.Text = feur.Rows[0]["cp_feur"].ToString();
                comboBoxEdit1.SelectedItem = feur.Rows[0]["ville_feur"].ToString();
                textEdit9.Text = feur.Rows[0]["email_feur"].ToString();
                textEdit10.Text = feur.Rows[0]["site_feur"].ToString();
                comboBoxEdit2.SelectedItem = feur.Rows[0]["tva_feur"].ToString();
                textEdit23.Text = feur.Rows[0]["forme_juriduque"].ToString();
                textEdit11.Text = feur.Rows[0]["mode_pay"].ToString();
            }
        }
        private void simpleButton8_Click(object sender, EventArgs e)
        {
            DataTable dt_fr = fun.get_frBycode(textEdit22.Text);
            if (dt_fr.Rows.Count > 0)
             {
                 XtraMessageBox.Show("Il existe un Fournisseur avec ce code");
             }
             else
             {
                 if (textEdit22.Text == "")
                 {
                     dxErrorProvider1.Dispose();
                     dxErrorProvider1.SetError(textEdit22, "Champ obligatoire");
                 }
                 else if (textEdit20.Text == "")
                 {
                     dxErrorProvider1.Dispose();
                     dxErrorProvider1.SetError(textEdit20, "Champ obligatoire");
                 }
                 else
                 {
                     DataTable feur = new DataTable();
                     feur = fun.get_FeurByCode(textEdit22.Text);
                     if (feur.Rows.Count != 0)
                     {
                         dxErrorProvider1.Dispose();
                         dxErrorProvider1.SetError(textEdit22, "Code Fournisseur déjà existe");
                     }
                     else
                     {
                         dxErrorProvider1.Dispose();
                         fun.set_feur(textEdit22.Text, textEdit21.Text, textEdit20.Text, textEdit19.Text, textEdit18.Text, textEdit17.Text, textEdit16.Text, textEdit15.Text, comboBoxEdit6.Text, textEdit14.Text, textEdit13.Text, comboBoxEdit5.Text, textEdit24.Text, textEdit12.Text);
                         labelControl51.Text = "Fournisseur ajoutée avec succées";
                         labelControl51.Visible = true;
                         timer1.Start();
                         ListeFeur();
                         xtraTabControl1.SelectedTabPage = xtraTabPage1;
                         xtraTabPage3.PageVisible = false;
                     }
                 }
             }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            xtraTabPage3.PageVisible = true;
            xtraTabControl1.SelectedTabPage = xtraTabPage3;
            textEdit12.Text = "";
            textEdit13.Text = "";
            textEdit14.Text = "";
            textEdit15.Text = "";
            textEdit16.Text = "";
            textEdit17.Text = "";
            textEdit18.Text = "";
            textEdit19.Text = "";
            textEdit20.Text = "";
            textEdit21.Text = "";
            textEdit22.Text = "";
            textEdit24.Text = "";
            comboBoxEdit5.Text = "5%";
            comboBoxEdit6.Text = "Ariana";
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                getFeurByCode(Convert.ToString(row[0]));
                Tid_fr.Text = row[14].ToString();
            }
            xtraTabPage2.PageVisible = true;
            xtraTabControl1.SelectedTabPage = xtraTabPage2;
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage1;
            xtraTabPage2.PageVisible = false;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                fun.delete_feur(Convert.ToString(row[0]));
            }
            ListeFeur();
        }

        private void xtraTabControl1_Selected(object sender, DevExpress.XtraTab.TabPageEventArgs e)
        {

           
            ListeFeur();
           
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            gridControl1.ShowRibbonPrintPreview();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (textEdit3.Text == "")
            {
                //dxErrorProvider1.Dispose();
                //dxErrorProvider1.SetError(textEdit3, "Champ obligatoire");
            }
            else
            {
                dxErrorProvider1.Dispose();
                fun.update_Feur(textEdit2.Text, textEdit3.Text, textEdit4.Text, textEdit5.Text, textEdit6.Text, textEdit7.Text, textEdit8.Text, comboBoxEdit1.Text, textEdit9.Text, textEdit10.Text, comboBoxEdit2.Text, textEdit23.Text, textEdit11.Text, textEdit1.Text,int.Parse(Tid_fr.Text));
                labelControl29.Visible = true;
                timer1.Start();
                ListeFeur();
                xtraTabControl1.SelectedTabPage = xtraTabPage1;
                xtraTabPage2.PageVisible = false;
            }
        }

        private void gestionFeur_Activated(object sender, EventArgs e)
        {
            if (login1.depart == "Utilisateur")
            {
                //ajout droit
                if (login1.aj_feur == "OUI") { simpleButton1.Enabled = true; xtraTabPage3.PageEnabled = true; }
                else { simpleButton1.Enabled = false; xtraTabPage3.PageEnabled = false; }
                //modifier droit
                if (login1.mod_feur == "OUI") { simpleButton2.Enabled = true; xtraTabPage2.PageEnabled = true; }
                else { simpleButton2.Enabled = false; xtraTabPage2.PageEnabled = false; }
                //supprimer droit
                if (login1.supp_feur == "OUI") { simpleButton3.Enabled = true; }
                else { simpleButton3.Enabled = false; }
                //exporter droit
                if (login1.feur_doc == "OUI") { simpleButton4.Enabled = true; }
                else { simpleButton4.Enabled = false; }
            }
            if (login1.depart == "Administrateur")
            {
                simpleButton1.Enabled = true; xtraTabPage3.PageEnabled = true;
                simpleButton2.Enabled = true; xtraTabPage2.PageEnabled = true;
                simpleButton3.Enabled = true; simpleButton4.Enabled = true;

            }
            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridView1.BestFitColumns();
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.EnableAppearanceEvenRow = true;

            ListeFeur();
            Form1.wait = 1;
            Form1.load = 1;
        }

        private void gestionFeur_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text += "a";
            if (label1.Text.ToString() == "10aaa")
            {
                labelControl29.Visible = false;
                labelControl51.Visible = false;
                label1.Text = "10";
                timer1.Stop();
            }

        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage1;
            xtraTabPage3.PageVisible = false;
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            //int count = gridView1.DataRowCount;
            //if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            //{
            //    System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            //    getFeurByCode(Convert.ToString(row[0]));
            //    Tid_fr.Text = row[14].ToString();
            //}
            
        }
    }
}