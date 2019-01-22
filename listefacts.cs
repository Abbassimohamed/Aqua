using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;

namespace RibbonSimplePad
{
    public partial class listefacts : DevExpress.XtraEditors.XtraForm
    {
        public listefacts()
        {
            InitializeComponent();
        }
        sql_gmao fun = new sql_gmao();
        public static int id_fact,id_clt;
        public static int id_bl;
        public static string etat_commande, id_factRass;
        private void liste_cde_client_Activated(object sender, EventArgs e)
        {
            if (login1.depart == "Utilisateur")
            {
                //facture
                if (login1.fact == "OUI") { barButtonItem1.Enabled = true; }
                else { barButtonItem1.Enabled = false; }
                //bon livraison
                if (login1.bon_liv == "OUI") { barButtonItem2.Enabled = true; }
                else { barButtonItem2.Enabled = false; }
                //bon sortie
                if (login1.bon_sort == "OUI") { barButtonItem5.Enabled = true; }
                else { barButtonItem5.Enabled = false; }
                //exporter droit
                if (login1.clt_doc == "OUI") { simpleButton3.Enabled = true; }
                else { simpleButton3.Enabled = false; }
                //supp
                if (login1.supp_cde_clt == "OUI") { simpleButton2.Enabled = true; }
                else { simpleButton2.Enabled = false; }
                //valider bon commande
                if (login1.valid_cde_clt == "OUI") { barButtonItem3.Enabled = true; }
                else { barButtonItem3.Enabled = false; }

            }
            if (login1.depart == "Administrateur")
            {
                barButtonItem1.Enabled = true; barButtonItem2.Enabled = true;
                simpleButton2.Enabled = true; barButtonItem3.Enabled = true;
                simpleButton3.Enabled = true; barButtonItem5.Enabled = true;

            }
            Liste_cde_clt();
            Form1.load = 1;

            Form1.wait = 1;
            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridView1.BestFitColumns();
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.EnableAppearanceEvenRow = true;
        }
        private void Liste_cde_clt()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = fun.get_Facts();
            this.gridView1.Columns[0].Caption = "Numero facture";
            this.gridView1.Columns[1].Caption = "Date";
            this.gridView1.Columns[2].Visible = false;
            this.gridView1.Columns[3].Visible = false;
            this.gridView1.Columns[4].Visible = false;
            this.gridView1.Columns[5].Caption = "Client";
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
            this.gridView1.Columns[20].Visible = false;
            this.gridView1.Columns[21].Visible = false;
            this.gridView1.Columns[22].Visible = false;
            this.gridView1.Columns[23].Visible = false;
            this.gridView1.Columns[24].Visible = false;
            this.gridView1.Columns[25].Visible = false;
           
            gridView1.OptionsView.ShowAutoFilterRow = true;
           
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                int code = Convert.ToInt32(row[0]);
                DialogResult dialogResult = XtraMessageBox.Show("Sure de vouloir supprimer la commande client n° " + code + " , les piéces rattachées seront aussi supprimées!", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    string b = "";
                    string a = "ajoute";
                    sql_gmao dd = new sql_gmao();
                    dd.delete_piece_from_cde_clt(code);//supprimer les pieces de commande fournisseur

                    dd.delete_cde_clt(code);//supprimer la commande fournisseur
                    Liste_cde_clt();

                }
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                id_bl = Convert.ToInt32(row[0]);

                Bon_livraison bl = new Bon_livraison();
                bl.ShowDialog();
            }
        }

        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {

                int count = gridView1.DataRowCount;
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                    id_fact = Convert.ToInt32(row[0]);
                    etat_commande = row[4].ToString();
                    id_clt = Convert.ToInt32(row[3].ToString());
                    if (row[4].ToString() == "en cours")
                    {
                        barButtonItem1.Visibility = BarItemVisibility.Never;
                        barButtonItem2.Visibility = BarItemVisibility.Never;
                        barButtonItem3.Visibility = BarItemVisibility.Always;
                        barButtonItem5.Visibility = BarItemVisibility.Never;
                        barButtonItem6.Visibility = BarItemVisibility.Never;

                    }
                  

                }
                if (etat_commande == "validée")
                {
                    barButtonItem1.Visibility = BarItemVisibility.Always;
                    barButtonItem2.Visibility = BarItemVisibility.Always;
                    barButtonItem5.Visibility = BarItemVisibility.Always;
                    barButtonItem6.Visibility = BarItemVisibility.Always;
                    barButtonItem3.Visibility = BarItemVisibility.Never;

                }
               

                if (etat_commande == "Annulée")
                {
                    barButtonItem1.Visibility = BarItemVisibility.Always;
                    barButtonItem2.Visibility = BarItemVisibility.Always;
                    barButtonItem5.Visibility = BarItemVisibility.Always;
                    barButtonItem3.Visibility = BarItemVisibility.Never;
                    barButtonItem6.Visibility = BarItemVisibility.Never;
                   

                }
               




                Point pt = this.Location;
                pt.Offset(this.Left + e.X, this.Top + e.Y);
                popupMenu1.ShowPopup(this.barManager1, Control.MousePosition);
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                id_fact = Convert.ToInt32(row[0]);
                
                facture fact = new facture();
                fact.ShowDialog();
            }
        }

        private void liste_cde_client_Load(object sender, EventArgs e)
        {

        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            string etat111 = "validée";
            fun.valider_cde_clt(id_fact, etat111);
            Liste_cde_clt();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Liste_cde_clt();
        }

       
        private void liste_cde_client_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
           

            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                id_fact = Convert.ToInt32(row[0]);

          
            }

            DataTable dd = new DataTable();
            dd = fun.get_piece44(id_fact);
            int qté;
            string libelle;
            foreach (DataRow row in dd.Rows)
            {
                qté= Convert.ToInt32(row["quantite_piece_u"]);
                libelle= row["libelle_piece_u"].ToString();


                fun.update_stock_after_annul(qté, libelle);
            }
            string etat111 = "Annulée";
            fun.Annuler_cde_clt(id_fact, etat111);

             Liste_cde_clt();

             raison rr = new raison();
             rr.ShowDialog();



 


        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            memoEdit1.Visible = false;
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                id_fact = Convert.ToInt32(row[0]);
                string comm = row["com2"].ToString();
                memoEdit1.Text ="Raison d'annulation de la commande: "+ comm;
                if (memoEdit1.Text == "Raison d'annulation de la commande: ")
                { memoEdit1.Visible = false; }
                else { memoEdit1.Visible = true; }


            }
        }

       

      

       
    }
}