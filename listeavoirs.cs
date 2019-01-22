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
using RibbonSimplePad.Report;
using DevExpress.XtraReports.UI;

namespace RibbonSimplePad
{
    public partial class listeavoirs : DevExpress.XtraEditors.XtraForm
    {
        public listeavoirs()
        {
            InitializeComponent();
        }
        sql_gmao fun = new sql_gmao();
        public static int id_fact,id_clt;
        public static int id_bl;
        public static Double somme, totht, totttc, totfodec, tottva, totretenusrc;
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
                somme = 0;
                totht=0;
                totttc=0;
                totfodec=0;
                tottva = 0;
                totretenusrc = 0;
            DataTable data = new DataTable();
            data.Clear();
            data = fun.selectfromavoirs();

            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Numéro");
            dt.Columns.Add("Numero Avoir");
            dt.Columns.Add("Date d'ajout");
            dt.Columns.Add("Code client");
            dt.Columns.Add("Etat");
            dt.Columns.Add("Client");
            dt.Columns.Add("montant HT");
            dt.Columns.Add("montant TTC");
            dt.Columns.Add("TVA");
            dt.Columns.Add("Remise");
            dt.Columns.Add("timbre");
            dt.Columns.Add("comment");
            dt.Columns.Add("N° commande");
            dt.Columns.Add("Nfact");
            dt.Columns.Add("id");
            
           

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow ravi = dt.NewRow();
                ravi["Numéro"] = data.Rows[i][12];
                ravi["Numero Avoir"] = data.Rows[i][0];
                ravi["Date d'ajout"] = data.Rows[i][1];
                ravi["Code client"] = data.Rows[i][2];
                ravi["Etat"] = data.Rows[i][3];
                ravi["Client"] = data.Rows[i][4];
                ravi["montant HT"] = data.Rows[i][5];
                ravi["montant TTC"] = data.Rows[i][6];
                ravi["TVA"] = data.Rows[i][7];
                ravi["Remise"] = data.Rows[i][8];
                ravi["timbre"] = data.Rows[i][9];
                ravi["comment"] = data.Rows[i][10];
                ravi["N° commande"] = data.Rows[i][11];
                ravi["Nfact"] = data.Rows[i][13];
                ravi["id"] = data.Rows[i][0];
              



                totht += Convert.ToDouble(data.Rows[i][5].ToString().Replace('.', ','));
                totttc += Convert.ToDouble(data.Rows[i][6].ToString().Replace('.', ','));
              
                tottva += Convert.ToDouble(data.Rows[i][7].ToString().Replace('.', ','));
               
                dt.Rows.Add(ravi);

            }
            DataRow ravi1 = dt.NewRow();

            ravi1["Numéro"] = "total";
            ravi1["Numero Avoir"] = "";
            ravi1["Date d'ajout"] = "";
            ravi1["Code client"] = "";
            ravi1["Etat"] = "";
            ravi1["Client"] = "";
            ravi1["montant HT"] ="" +totht;
            ravi1["montant TTC"] = "" + totttc;
            ravi1["TVA"] = "" + tottva;
            ravi1["Remise"] = "";
            ravi1["timbre"] = "";
            ravi1["comment"] ="";
            ravi1["N° commande"] = "";
            ravi1["Nfact"] = "";
            ravi1["id"] = "";
            
          
            
          



            dt.Rows.Add(ravi1);
                 
         
            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = dt;
            this.gridView1.Columns[0].Caption = "Numéro avoir";
            this.gridView1.Columns[1].Visible = false;
            this.gridView1.Columns[2].Caption = "Date d'ajout";
            this.gridView1.Columns[3].Visible = false;
            this.gridView1.Columns[4].Caption = "Etat";
            this.gridView1.Columns[5].Caption = "Client";
            this.gridView1.Columns[6].Visible = false;
            this.gridView1.Columns[7].Caption = "Montant TTC";
            this.gridView1.Columns[8].Visible = false;
            this.gridView1.Columns[9].Visible = false;
            this.gridView1.Columns[10].Visible = false;
            this.gridView1.Columns[11].Visible = false;
            this.gridView1.Columns[12].Visible = false;
            this.gridView1.Columns[13].Caption = "Numéro facture";
            this.gridView1.Columns[14].Visible = false;
            
           
           
           
           
            gridView1.OptionsView.ShowAutoFilterRow = true;
           
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
          
        }

       



        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
              int count = gridView1.DataRowCount;
              if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
              {
                  System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                  int code = Convert.ToInt32(row[14]);
                  string etat111 = "validée";
                  fun.updateEtatAvoir(code, etat111);
                  string numero_fact=row[13].ToString();
                  fun.updateEtatAvoirFactureVente(numero_fact, true);
                  DataTable dt_piece_avoir = fun.selectPieceAvoirsByNum(code);
                  foreach (DataRow item in dt_piece_avoir.Rows)
                  {
                      double qt = double.Parse(item["quantite_piece"].ToString().Replace('.', ','));
                      string id_prod = item["id_prod"].ToString();
                      fun.update_sousstock_avoir2(qt, id_prod);
                  }
                  Liste_cde_clt();
              }
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

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            try{
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                int code = Convert.ToInt32(row[0]);
                DialogResult dialogResult = XtraMessageBox.Show("Sure de vouloir supprimer la facture  n° " + code + "", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    string b = "";
                    string a = "ajoute";
                    sql_gmao dd = new sql_gmao();
                    dd.delete_piece_avoir(code);//supprimer les pieces de commande fournisseur

                    dd.delete_avoir(code);//supprimer la commande fournisseur
                    Liste_cde_clt();

                }
            }
            }
            catch (Exception tt)
            {
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int count = gridView1.DataRowCount;
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                    id_fact = Convert.ToInt32(row[0].ToString());
                    //facturing f = new facturing();
                    //f.ShowDialog();
                     AvoirReport rep = new AvoirReport(id_fact);
                    rep.ShowPreview();
                }
            }
            catch (Exception tt)
            {
            }
        }

        private void listefactvente_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            Liste_cde_clt();
        }

        private void panelControl3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            
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
                    id_clt = Convert.ToInt32(row[8].ToString());
                    if (row[4].ToString() == "en cours")
                    {
                        //va
                        barButtonItem3.Visibility = BarItemVisibility.Always;
                        //mo
                        barButtonItem7.Visibility = BarItemVisibility.Always;
                        Point pt = this.Location;
                        pt.Offset(this.Left + e.X, this.Top + e.Y);
                        popupMenu1.ShowPopup(this.barManager1, Control.MousePosition);
                    }
                    else
                    {
                        //va
                        barButtonItem3.Visibility = BarItemVisibility.Never;
                        //mo
                        barButtonItem7.Visibility = BarItemVisibility.Never;
                    }
                }

            }
        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
             int count = gridView1.DataRowCount;
             if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
             {
                 System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                 int code = Convert.ToInt32(row[0]);
                 int id = Convert.ToInt32(row[14]);
                 Avoircommande avoir = new Avoircommande(code,id);
                 avoir.ShowDialog();
             }
        }

       

      

       
    }
}