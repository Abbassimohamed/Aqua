using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using RibbonSimplePad.Report;
using DevExpress.XtraReports.UI;

namespace RibbonSimplePad
{
    public partial class liste_devis : DevExpress.XtraEditors.XtraForm
    {
        public liste_devis()
        {
            InitializeComponent();
        }

        public static string etat, id_clt, etat_envoie;
        public static int id_devis;
        sql_gmao fun = new sql_gmao();
        private void liste_devis_Load(object sender, EventArgs e)
        {

        }

        public void Liste_devis()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = fun.get_devis();
            this.gridView1.Columns[0].Caption = "Code Devis";
            this.gridView1.Columns[1].Caption = "Date d'ajout de Devis";
            this.gridView1.Columns[2].Visible = false;
            this.gridView1.Columns[3].Visible = false;
            this.gridView1.Columns[4].Caption = "Etat de Devis ";
            this.gridView1.Columns[5].Caption = "Client";

            this.gridView1.Columns[6].Visible = false;
            this.gridView1.Columns[7].Visible = false;
            this.gridView1.Columns[8].Visible = false;
            this.gridView1.Columns[9].Visible = false;
            this.gridView1.Columns[10].Visible = false;
            this.gridView1.Columns[11].Visible = false;
            this.gridView1.Columns[12].Visible = false;
            gridView1.OptionsView.ShowAutoFilterRow = true;
        }

        private void liste_devis_Activated(object sender, EventArgs e)
        {
            if (login1.depart == "Utilisateur")
            {
                //ajout droit
                if (login1.ajout_devis == "OUI") { simpleButton4.Enabled = true; }
                else { simpleButton4.Enabled = false;}
              
                //supprimer droit
                if (login1.supp_devis == "OUI") { simpleButton2.Enabled = true; }
                else { simpleButton2.Enabled = false; }
                //exporter droit
                if (login1.devis_doc == "OUI") { simpleButton3.Enabled = true; }
                else { simpleButton3.Enabled = false; }

            }
            if (login1.depart == "Administrateur")
            {
                simpleButton4.Enabled = true;
                simpleButton2.Enabled = true;
                simpleButton3.Enabled = true;
            }
            
            Liste_devis();
            Form1.load = 1;

            Form1.wait = 1;
            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridView1.BestFitColumns();
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.EnableAppearanceEvenRow = true;
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            //int count = gridView1.DataRowCount;
            //if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            //{
            //    System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            //    id_devis = Convert.ToInt32(row[0]);
            //    id_clt = row[3].ToString();
            //    etat_envoie = row[4].ToString();
               
            //    etat = "modif";
            //    devis dev = new devis();
            //    dev.ShowDialog();
            //}
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            
            etat = "ajout";
            CreationDevis cre_dev = new CreationDevis(this);
            cre_dev.ShowDialog();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                int code1 = Convert.ToInt32(row[0]);
                DialogResult dialogResult = XtraMessageBox.Show("Sure de vouloir supprimer le Devis n° " + code1 + " !", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {

                    fun.delete_piece_from_devis(code1);//supprimer les pieces de commande fournisseur

                    fun.delete_devis1(code1);//supprimer la commande fournisseur
                    Liste_devis();

                }
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                id_devis = Convert.ToInt32(row[0]);
                DataTable dt = new DataTable();
                 DataTable dt1 = new DataTable();
                 int numcmd = get_maxcmd() + 1;
                dt=fun.get_piece_from_devis(id_devis);
                dt1=fun.get_etat_devv(id_devis);
                foreach (DataRow row1 in dt.Rows)
                {

                     Double varia= Convert.ToDouble(row1[3].ToString().Replace('.', ','))*Convert.ToDouble( row1[9].ToString());
                    //  insert_piecee_commande1(string codep, string lib, Double quantit, string unite, string puv,string remise,string ttva, string idclt, string id_cmd, string pv,string qtrest)
                     fun.insert_piecee_commande1(row1[1].ToString(), row1[2].ToString(), Convert.ToDouble(row1[3].ToString().Replace('.', ',')), row1[13].ToString(), row1[9].ToString(),row1[11].ToString(),row1[12].ToString(), dt1.Rows[0][10].ToString(),numcmd.ToString(),row1[10].ToString(),row1[3].ToString());// dt1.Rows[0][9].ToString(), row1[3].ToString(), numcmd.ToString(),row1[10].ToString())
            
                }
               // fun.insert_into_Commandepasse(tnbcmd.Text, id_clt, etat, lookUpEdit1.Text, etat_fac, textBox1.Text, timbre);
                fun.insert_into_Commandepasse("", dt1.Rows[0][3].ToString(), "en cours", dt1.Rows[0][5].ToString(), "en cours", dt1.Rows[0][8].ToString(), dt1.Rows[0][11].ToString());
                MessageBox.Show("la commande est ajoutée avec succées veuillez consulter la liste des commandes crée");
           
              
            }
        }
        private int get_maxcmd()
        {
            int y = 0;
            int x = 0;
            DataTable dt = new DataTable();
            DataTable dtbl = new DataTable();
            DataTable data = new DataTable();
            dt = fun.getcountcmd("CommandeClient");

            if (dt.Rows.Count == 0)
            {
                data = fun.getcurrentvalue("CommandeClient");

                if (Convert.ToInt32(data.Rows[0][0]) == 0)
                {
                    fun.resetautoincrement("CommandeClient", 0);
                    y = Convert.ToInt32(data.Rows[0][0]);
                }
                else
                {
                    fun.resetautoincrement("CommandeClient", 0);
                    data = fun.getcurrentvalue("CommandeClient");
                    y = Convert.ToInt32(data.Rows[0][0]);
                }
            }
            else
            {
                dtbl = fun.get_max_Commande();
                x = Convert.ToInt32(dtbl.Rows[0][0]);
                fun.resetautoincrement("CommandeClient", x);
                data = fun.getcurrentvalue("CommandeClient");
                y = Convert.ToInt32(data.Rows[0][0]);

            }

            return y;

        }
        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Point pt = this.Location;
                pt.Offset(this.Left + e.X, this.Top + e.Y);
                popupMenu1.ShowPopup(this.barManager1, Control.MousePosition);
            }
        }

        private void liste_devis_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Liste_devis();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            gridView1.ShowRibbonPrintPreview();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                int idDevi = Convert.ToInt32(row[0]);
                string des = "Mise à jour devis: (" + idDevi + "  ver accpté)";
                fun.update_etat_devis(idDevi, "accepté", des);
                Liste_devis();
            }
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                int idDevi = Convert.ToInt32(row[0]);

                DevisReport report = new DevisReport(idDevi);
                report.ShowPreview();
               
            }
        }

        private void gridControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                int idDevi = Convert.ToInt32(row[0]);

                DevisReport report = new DevisReport(idDevi);
                report.ShowPreview();

            }
        }

       
    }
}