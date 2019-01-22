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
using DevExpress.XtraPrintingLinks;
using RibbonSimplePad.Report;
using DevExpress.XtraReports.UI;
namespace RibbonSimplePad
{
    public partial class Liste_cde : DevExpress.XtraEditors.XtraForm
    {
        public Liste_cde()
        {
            InitializeComponent();
            bar1.Visible = true;
            grid();
          
        }
        string type="";
        public Liste_cde(string type)
        {
            InitializeComponent();
            this.type = type;
            bar1.Visible = true;
            grid2();

        }
        sql_gmao fun = new sql_gmao();
        public static int  req_code,id_commande;
        public static string req_emetteur, req_date_env, req_date_cre, req_etat, req_non_feur, req_code_feur;
        private void Liste_cde_Load(object sender, EventArgs e)
        {
           
        }
        private void grid()
        {
            //charger liste des commandes fournisseur
            gridControl1.DataSource = null;
            gridControl1.RefreshDataSource();
            gridView1.Columns.Clear();
            gridControl1.DataSource = fun.list_cdefr();
            this.gridView1.Columns[0].Caption = "Code";
            this.gridView1.Columns[1].Caption = "Date de création";
            this.gridView1.Columns[2].Caption = "etat";
            this.gridView1.Columns[3].Caption = "Fournisseur";
            this.gridView1.Columns[4].Visible = false;
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
            this.gridView1.Columns[15].Visible = false;
            this.gridView1.Columns[16].Visible = false;
            this.gridView1.Columns[17].Visible = false;
            this.gridView1.Columns[18].Visible = false;
            this.gridView1.Columns[19].Visible = false;
        
            
           
            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridView1.OptionsView.EnableAppearanceEvenRow = true;
            
        }
        private void grid2()
        {
            //charger liste des commandes fournisseur
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.RefreshDataSource();
            gridControl1.DataSource = fun.list_cdefrrecu();
            this.gridView1.Columns[0].Caption = "Code";
            this.gridView1.Columns[1].Caption = "Date de création";
            this.gridView1.Columns[2].Caption = "etat";
            this.gridView1.Columns[3].Caption = "Fournisseur";
            this.gridView1.Columns[4].Visible = false;
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
            this.gridView1.Columns[15].Visible = false;
            this.gridView1.Columns[16].Visible = false;
            this.gridView1.Columns[17].Visible = false;
            this.gridView1.Columns[18].Visible = false;
            this.gridView1.Columns[19].Visible = false;



            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridView1.OptionsView.EnableAppearanceEvenRow = true;

        }
      
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            //recuperer les informations de commande fournisseur
            
                int id_cde;
                GridHitInfo celclick = gridView1.CalcHitInfo(gridControl1.PointToClient(Control.MousePosition));
                if (celclick.InRow)
                {
                    int count = gridView1.DataRowCount;
                    if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                    {
                        System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                        id_cde = Convert.ToInt32(row[0]);
                        id_commande = Convert.ToInt32(row[0]);

                        req_non_feur = row[6].ToString();
                        DataTable dt = new DataTable();
                        sql_gmao ss = new sql_gmao();
                        dt = ss.get_list_cdefr(id_cde);
                        req_code = Convert.ToInt32(dt.Rows[0]["id_commande"]);
                        req_emetteur = dt.Rows[0]["client"].ToString();
                        req_code_feur = dt.Rows[0]["id_clt"].ToString();
                        req_date_cre = dt.Rows[0]["date"].ToString();
                        req_date_env = dt.Rows[0]["date"].ToString();
                        req_etat = dt.Rows[0]["etatcmd"].ToString();
                        EtatCmd report = new EtatCmd(req_code);
                        report.ShowDialog();
                        //BoncmdFr al = new BoncmdFr();
                        //al.ShowDialog();
                    }
                
            }
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridControl1.ShowRibbonPrintPreview();
        }
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }
        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {
            //l'administrateur seulement peut supprimer des commandes fournisseur 
            if (this.type == "")
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {

                    Point pt = this.Location;
                    pt.Offset(this.Left + e.X, this.Top + e.Y);
                    popupMenu1.ShowPopup(this.barManager1, Control.MousePosition);

                }
            }
        }
        private void Liste_cde_FormClosing(object sender, FormClosingEventArgs e)
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
                labelControl9.Visible = false;
                label1.Text = "10";
                timer1.Stop();
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (type != "")
            {
                grid2();
            }
            else
            {
                grid();
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int count = gridView1.DataRowCount;
            DialogResult dialogResult = XtraMessageBox.Show("Sure de vouloir supprimer la commande  , les piéces rattachées seront aussi supprimées!", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    foreach (int i in gridView1.GetSelectedRows())
                    {
                        System.Data.DataRow row = gridView1.GetDataRow(i);
                        int code = Convert.ToInt32(row[0]);

                        string b = "";
                        string a = "ajoute";
                        sql_gmao dd = new sql_gmao();
                        dd.delete_piece_from_cde_fr(code);//supprimer les pieces de commande fournisseur

                        dd.deletecmdfr(code);//supprimer la commande fournisseur

                    }



                }
                if (type != "")
                {
                    grid2();
                }
                else
                {
                    grid();
                }
            }
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                int code = Convert.ToInt32(row[0]);
                string etat = "";
                etat = row[2].ToString();
                if (etat == "reçu")
                    {
                        MessageBox.Show("Commande déja recu");
                    }
                    else
                    {
                DataTable dt = new DataTable();
                dt = fun.get_list_piece_from_cdefr(code);
                foreach (DataRow row1 in dt.Rows)
                {
                    DataTable dat = new DataTable();
                    dat = fun.get_infos_piece(row1[1].ToString());
                    Double a = Convert.ToDouble(row1[3]) + Convert.ToDouble(dat.Rows[0][3]);
                   
                    fun.update_stock_aftercmdreceiving(a.ToString(), row1[1].ToString());

                }
                fun.update_etatcmdfr("reçu", code.ToString());
                MessageBox.Show("Le stock a été mis à jour");
                    }
            }
            if (type != "")
            {
                grid2();
            }
            else
            {
                grid();
            }
        }

        private void barButtonItem3_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int count = gridView1.DataRowCount;

            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                foreach (int i in gridView1.GetSelectedRows())
                {
                    System.Data.DataRow row = gridView1.GetDataRow(i);
                    int code = Convert.ToInt32(row[0].ToString());
                    string etat = row[2].ToString();
                    if (etat == "en cours")
                    {

                        passerCommandeFr cmd_fr = new passerCommandeFr(code);
                        cmd_fr.ShowDialog();
                    }
                    else
                    {
                        XtraMessageBox.Show("Cette commande est : " + etat);
                    }

                }

            }
            if (type != "")
            {
                grid2();
            }
            else
            {
                grid();
            }
                
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                int code = Convert.ToInt32(row[0]);


                fun.update_etatcmdfr("accepter", code.ToString());
                grid();
            }
            catch (Exception ex)
            {
            }
            if (type != "")
            {
                grid2();
            }
            else
            {
                grid();
            }
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int count = gridView1.DataRowCount;

            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                foreach (int i in gridView1.GetSelectedRows())
                {
                    System.Data.DataRow row = gridView1.GetDataRow(i);
                    int code = Convert.ToInt32(row[0].ToString());
                    EtatCmd report = new EtatCmd(code);
                    report.ShowDialog();
                    

                }

            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}