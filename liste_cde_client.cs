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
    public partial class liste_cde_client : DevExpress.XtraEditors.XtraForm
    {
        public liste_cde_client()
        {
            InitializeComponent();
            //Listecmdclt();
        }
       public static Double sommettc;
        sql_gmao fun = new sql_gmao();
        public static int id_fact, id_clt, id_commande = 0, idbl;
        public static Double tottc = 0;
        public static int id_bl;
        public static string etat_commande, id_factRass, id_cmd = "",timbre;

        
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
            
            Form1.load = 1;
             Listecmdclt();
            Form1.wait = 1;
            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridView1.BestFitColumns();
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.EnableAppearanceEvenRow = true;

        }
        
        private void Listecmdclt()
        {
             sommettc = 0;
            DataTable data = new DataTable();
            data.Clear();
            data = fun.get_CmdClient();

            DataTable dt = new DataTable();
            dt.Clear();

            dt.Columns.Add("id_commande");
            dt.Columns.Add("n_boncmd");
            dt.Columns.Add("date");
            dt.Columns.Add("etatcmd");
            dt.Columns.Add("client");
            dt.Columns.Add("etatfacture");
            dt.Columns.Add("etatbl");
            dt.Columns.Add("etatbnsortie");
            dt.Columns.Add("id_clt");
            dt.Columns.Add("montant_ht");
            dt.Columns.Add("montant_ttc");
            dt.Columns.Add("timbre");
            dt.Columns.Add("remise");
            dt.Columns.Add("tva");

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow ravi = dt.NewRow();

                ravi["id_commande"] = data.Rows[i][0];
                ravi["n_boncmd"] = data.Rows[i][1];
                ravi["date"] = data.Rows[i][2];             
                ravi["etatcmd"] = data.Rows[i][3];
                ravi["client"] = data.Rows[i][4];
                ravi["etatfacture"] = data.Rows[i][5];
                ravi["etatbl"] = data.Rows[i][6];
                ravi["etatbnsortie"] = data.Rows[i][7];
                ravi["id_clt"] = data.Rows[i][8];
                ravi["montant_ht"] = data.Rows[i][9];
                ravi["montant_ttc"] = data.Rows[i][10];
                ravi["timbre"] = data.Rows[i][11];
                ravi["remise"] = data.Rows[i][12];
                ravi["tva"] = data.Rows[i][13];
                sommettc += Convert.ToDouble(data.Rows[i][10].ToString().Replace('.', ','));
                
                dt.Rows.Add(ravi);

            }
            //sommettc = Convert.ToDouble(updatesum().ToString().Replace('.', ','));
            DataRow ravi1 = dt.NewRow();

            ravi1["id_commande"] = "Total";
            ravi1["n_boncmd"] = "";
            ravi1["date"] = "";
            ravi1["etatcmd"] = "";
            ravi1["client"] = "";
            ravi1["etatfacture"] = "";
            ravi1["etatbl"] = "";
            ravi1["etatbnsortie"] = "";
            ravi1["id_clt"] = "";
            ravi1["montant_ht"] = "";
            ravi1["montant_ttc"] = sommettc.ToString();
            ravi1["timbre"] = "";
            ravi1["remise"] = "";
            ravi1["tva"] = "";

            


            dt.Rows.Add(ravi1);

            gridControl1.DataSource = null;
            tottc = 0;
            gridView1.Columns.Clear();
            gridControl1.DataSource = dt;
            this.gridView1.Columns[0].Caption = "Code Commande Client";
            this.gridView1.Columns[1].Caption = "N° bon de commande reçu";
            this.gridView1.Columns[2].Caption = "date de commande";
            this.gridView1.Columns[3].Caption = "Etat de commande ";
            this.gridView1.Columns[4].Caption = "Client";
            this.gridView1.Columns[5].Visible = false;
            this.gridView1.Columns[6].Visible = false;
            this.gridView1.Columns[7].Visible = false;
            this.gridView1.Columns[8].Visible = false;
            this.gridView1.Columns[9].Visible = false;
            this.gridView1.Columns[10].Caption = "prix TTC";
            this.gridView1.Columns[11].Visible = false;
            this.gridView1.Columns[12].Visible = false;
            this.gridView1.Columns[13].Visible = false;
            gridView1.OptionsView.ShowAutoFilterRow = true;

            updatesum();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int count = gridView1.DataRowCount;
            DialogResult dialogResult = XtraMessageBox.Show("Sure de vouloir supprimer la commande client , les piéces rattachées seront aussi supprimées!", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    foreach (int i in gridView1.GetSelectedRows())
                    {
                        try
                        {
                            System.Data.DataRow row = gridView1.GetDataRow(i);
                            int code = Convert.ToInt32(row[0]);

                            string b = "";
                            string a = "ajoute";
                            sql_gmao dd = new sql_gmao();
                            dd.delete_piece_from_cde_clt(code);//supprimer les pieces de commande fournisseur

                            dd.deletecmdClt(code);//supprimer la commande fournisseur
                        }
                        catch (Exception ex) { }
                       
                    }
                   
                  
              
                }
                Listecmdclt();
            }
        }


        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                id_commande = Convert.ToInt32(row[0]);
                id_cmd = row[1].ToString();
                timbre = row[11].ToString();
                CommandeBL cmdbl = new CommandeBL();
                cmdbl.Show();
            }
        }

        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {

                    int count = gridView1.DataRowCount;
                    if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                    {
                        System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                        id_fact = Convert.ToInt32(row[0]);
                        etat_commande = row[3].ToString();
                        id_clt = Convert.ToInt32(row[8].ToString());
                        if (row[3].ToString() == "en cours")
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
            catch (Exception er) { }
        }





        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            string etat111 = "validée";
            fun.valider_cde_cltPasser(id_fact, etat111);
            Listecmdclt();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Listecmdclt();
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
                qté = Convert.ToInt32(row["quantite_piece_u"]);
                libelle = row["libelle_piece_u"].ToString();


               // fun.update_stock_after_annul(qté, libelle);
            }
            string etat111 = "Annulée";
            fun.Annuler_cde_clt(id_fact, etat111);

            Listecmdclt();

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
                try
                {

                    id_fact = Convert.ToInt32(row[0]);
                }
                catch (Exception ee) { }

                memoEdit1.Text = "Raison d'annulation de la commande: ";
                if (memoEdit1.Text == "Raison d'annulation de la commande: ")
                { memoEdit1.Visible = false; }
                else { memoEdit1.Visible = true; }


            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            Listecmdclt();

        }

        public void updatesum()
        {
            tottc = 0;
            if (gridView1.DataRowCount != 0)
            {
              /*  for (int i = 0; i < gridView1.RowCount; i++)
                {

                    DataRow row1 = gridView1.GetDataRow(i);
                    tottc += Convert.ToDouble(row1[10].ToString().Replace('.', ','));

                }*/
                textBox1.Text = sommettc.ToString();
               
            }
            else

                textBox1.Text = sommettc.ToString();
           

        }

        private void gridControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                int count = gridView1.DataRowCount;
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {

                    DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                    id_commande = Convert.ToInt32(row[0].ToString());
                    id_clt = Convert.ToInt32(row[8].ToString());
                    id_cmd = row[1].ToString();
                    tottc = Convert.ToDouble(row[10].ToString());

                    modifierCommande passercmd = new modifierCommande();
                    passercmd.Show();
                }

            }
            catch (Exception exce)
            { }

        }
        private int get_maxcmd()
        {
            int x = 0;
            int y = 0;
            DataTable dt = new DataTable();
            DataTable dat = new DataTable();
            dt = fun.get_max_Commande();
            if (dt.Rows[0][0] == DBNull.Value)
            {
                fun.resetautoincrement("CommandeClient", 0);
                y = 0;

            }

            else
            {
                x = Convert.ToInt32(dt.Rows[0][0]);
                fun.resetautoincrement("CommandeClient", x);
                dt = fun.get_max_Commande();
                y = Convert.ToInt32(dt.Rows[0][0]);

            }
            return y + 1;


        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            gridControl1.ShowRibbonPrintPreview();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void liste_cde_client_Load(object sender, EventArgs e)
        {

        }
    }
}