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
    public partial class listebls : DevExpress.XtraEditors.XtraForm
    {
        public listebls()
        {
            InitializeComponent();
           Liste_cde_clt();
           
        }
        sql_gmao fun = new sql_gmao();
        public static int id_factt, id_fact, id_clt, displaybl, idcmd, idfcmd, idcmande, idbl;
        public static int id_bl;
        public static DataTable data;
        public Double tottc, totalfactureht, totalfacturettc;
        public static string etat_commande, client, num_bl, num_facture;
       
        private void Liste_cde_clt()
        {
            tottc = 0;
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = fun.get_BLS();
            this.gridView1.Columns[0].Visible=false;// = "Numero bl";
            this.gridView1.Columns[1].Caption = "date";
            this.gridView1.Columns[2].Visible = false;
            this.gridView1.Columns[3].Caption = "Etat";
            this.gridView1.Columns[4].Caption = "Client";
            this.gridView1.Columns[5].Visible = false;
            this.gridView1.Columns[6].Visible = false;
            this.gridView1.Columns[7].Visible = false;
            this.gridView1.Columns[8].Visible = false;
            this.gridView1.Columns[9].Visible = false;
            this.gridView1.Columns[10].Visible = false;
            this.gridView1.Columns[11].Caption="N° commande";
            this.gridView1.Columns[12].Caption="Reference";
            this.gridView1.Columns[13].Caption = "Numero bl";
            this.gridView1.Columns[14].Visible = false;
            this.gridView1.Columns[15].Visible = false;
            this.gridView1.Columns[16].Visible = false;
            gridView1.OptionsView.ShowAutoFilterRow = true;
            updatesum();

        }

       
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            //Tnumfact.Visible = true;
            //simpleButton5.Visible = true;
            //labelControl1.Visible = true;
            //labelControl2.Visible=true;
            //dateEdit1.Visible=true;
            //labelControl3.Visible = true;
            //textEdit1.Visible = true;
            string num_fact = NumFact().ToString();
            //num_facture = DateTime.Now.Year.ToString().Substring(2, 2) + " /" + num_fact;
            Tnumfact.Text = num_fact;
            //DateTime.Now.Year.ToString().Substring(2,2)+" /"+ 
            List<int> LidBl = new List<int>();
                foreach (int i in gridView1.GetSelectedRows())
                {
                   
                    DataRow row = gridView1.GetDataRow(i);
                   
                   LidBl.Add(Convert.ToInt32(row[0].ToString()));
                }
                FactureVente factvent = new FactureVente(LidBl);
                factvent.ShowDialog();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
          
                DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                displaybl = Convert.ToInt32(row[0].ToString());
                idcmd = Convert.ToInt32(row[11].ToString());
                BLReport report = new BLReport(displaybl);
                report.ShowPreview();
                //DateTime dt = Convert.ToDateTime(row[1].ToString());
                //num_bl = "";
                //num_bl =dt.Year.ToString().Substring(2,2)+"/ "+row[13].ToString();
                //cBon_livraison cbl = new cBon_livraison();
                //cbl.Show();
           
        }
        public void updatesum()
        {
            tottc = 0;

            for (int i = 0; i < gridView1.RowCount; i++)
            {

                DataRow row1 = gridView1.GetDataRow(i);
                tottc += Convert.ToDouble(row1[6].ToString().Replace('.', ','));

            }


            textBox1.Text = tottc.ToString();

        }
        private int get_maxfact()
        {
            int y = 0;
            int x = 0;
            DataTable dt = new DataTable();
            DataTable dtbl = new DataTable();
            DataTable data = new DataTable();
            dt = fun.getcountcmd("facturevente");

            if (dt.Rows.Count == 0)
            {
                data = fun.getcurrentvalue("facturevente");

                if (Convert.ToInt32(data.Rows[0][0]) == 0)
                {
                    fun.resetautoincrement("facturevente", 0);
                    y = Convert.ToInt32(data.Rows[0][0]);
                }
                else
                {
                    fun.resetautoincrement("facturevente", 0);
                    data = fun.getcurrentvalue("facturevente");
                    y = Convert.ToInt32(data.Rows[0][0]);
                }
            }
            else
            {
                dtbl = fun.get_max_Factvente();
                x = Convert.ToInt32(dtbl.Rows[0][0]);
                fun.resetautoincrement("facturevente", x);
                data = fun.getcurrentvalue("facturevente");
                y = Convert.ToInt32(data.Rows[0][0]);

            }

            return y;

        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            sql_gmao dd = new sql_gmao();
            int count = gridView1.DataRowCount;
            DialogResult dialogResult = XtraMessageBox.Show("Sure de vouloir supprimer le BL ", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    foreach (int i in gridView1.GetSelectedRows())
                    {
                        System.Data.DataRow row = gridView1.GetDataRow(i);
                        int code = Convert.ToInt32(row[0]);
                        dd.delete_BL(code);//supprimer les pieces de commande fournisseur
                        dd.delete_piece_bl(code);

                    }
                    Liste_cde_clt();
                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            gridControl1.ShowRibbonPrintPreview();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Liste_cde_clt();
        }

        public int NumFact()
        {

            int y = 0;
            int x = 0;
            DataTable dt = new DataTable();
            DataTable dr = new DataTable();
            dr = fun.ListFact();
            if (dr.Rows.Count == 0)
            {
                return 1;
            }

            dt = fun.max_num_Factvent();
            if (dt.Rows.Count == 0)
            {
                return 1;
            }
           // string max = dt.Rows[0]["max"].ToString();
            if (dt.Rows[0]["max"].ToString() == "")
            {

                y = 1;

            }
            else
            {
                y = int.Parse(dt.Rows[0]["max"].ToString()) + 1;

            }

            return y;
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            Tnumfact.Visible = false;
            simpleButton5.Visible = false;
            labelControl1.Visible = false;
            labelControl2.Visible=false;
            dateEdit1.Visible=false;
            labelControl3.Visible = false;
            textEdit1.Visible = false;
            string num_fact = Tnumfact.Text;
            int num_f = 0;
            int.TryParse(Tnumfact.Text, out num_f);
            DataTable dt_fact_n = fun.GetFactByNum(num_f);
            if (dt_fact_n.Rows.Count > 0)
            {
                XtraMessageBox.Show("Il existe une facture avec ce numéro");
            }
            else
            {
                if (dateEdit1.Text == "")
                {
                    MessageBox.Show("Entrer la date du facture");
                }
                else
                {
                    data = new DataTable();
                    data.Clear();
                    data.Columns.Add("code piece");
                    data.Columns.Add("code_art");
                    data.Columns.Add("libelle_piece");
                    data.Columns.Add("quantite_piece");
                    data.Columns.Add("id_clt");
                    data.Columns.Add("etat");
                    data.Columns.Add("puv");
                    data.Columns.Add("totvente");
                    data.Columns.Add("id_commande");
                    data.Columns.Add("remise");
                    data.Columns.Add("ttva");
                    data.Columns.Add("unit");

                    id_factt = get_maxfact() + 1;

                    if (gridView1.GetSelectedRows().Count() != 0)
                    {
                        totalfacturettc = 0;
                        totalfactureht = 0;
                        string tva = "0", timbre = "0";
                        double tt_ht = 0, tt_ttc = 0, prix = 0;
                        List<string> l_bl = new List<string>();
                        foreach (int i in gridView1.GetSelectedRows())
                        {
                            DataTable datatable = new DataTable();
                            DataRow row = gridView1.GetDataRow(i);
                            DataTable dt = new DataTable();
                            dt = fun.getbl(Convert.ToInt32(row[0].ToString()));
                            id_clt = Convert.ToInt32(dt.Rows[0][2].ToString());
                            client = dt.Rows[0][4].ToString();
                            idfcmd = Convert.ToInt32(dt.Rows[0][11].ToString());
                            idbl = Convert.ToInt32(dt.Rows[0][0].ToString());
                            datatable = fun.get_Allprodbybl(row[0].ToString());
                            l_bl.Add(row[13].ToString());
                            if (dt.Rows[0][5] != DBNull.Value)
                            {
                                totalfactureht += Convert.ToDouble(dt.Rows[0][5].ToString());
                            }
                            else
                            {
                                totalfactureht += 0;
                            }
                            if (dt.Rows[0][6] != DBNull.Value)
                            {
                                totalfacturettc += Convert.ToDouble(dt.Rows[0][6].ToString());
                            }
                            else
                            {
                                totalfacturettc += 0;
                            }


                            if (datatable.Rows.Count != 0)
                            {

                                foreach (DataRow row1 in datatable.Rows)
                                {
                                    //DataRow newpc = data.NewRow();
                                    //newpc["code piece"] = row1[0];
                                    //newpc["code_art"] = row1[1];
                                    //newpc["libelle_piece"] = row1[2];
                                    //newpc["quantite_piece"] = Convert.ToDouble(row1[3].ToString().Replace('.', ','));
                                    //newpc["id_clt"] = row1[4];
                                    //newpc["etat"] = "validée";
                                    //newpc["puv"] = row1[6];
                                    //newpc["totvente"] = row1[7];
                                    //newpc["id_commande"] = row1[8];
                                    //newpc["remise"] = row1[9];
                                    //newpc["ttva"] = row1[10];
                                    //newpc["unit"] = row1[11];

                                    //data.Rows.Add(newpc);
                                    timbre = dt.Rows[0][9].ToString();
                                    prix = double.Parse(row1[3].ToString().Replace('.', ',')) * double.Parse(row1[6].ToString().Replace('.', ','));
                                    prix = prix - ((prix * double.Parse(row1[9].ToString().Replace('.', ',')) / 100));
                                    tt_ht += prix;
                                    prix = prix + ((prix * double.Parse(row1[10].ToString().Replace('.', ',')) / 100));
                                    tt_ttc += prix;
                                    DataTable dt_piec = fun.get_piece_fact(id_factt, row1[1].ToString());
                                    double qt = 0, pvv = 0;
                                    if (dt_piec.Rows.Count > 0)
                                    {
                                        qt = double.Parse(dt_piec.Rows[0]["quantite_piece_u"].ToString().Replace('.', ','));
                                        pvv = double.Parse(dt_piec.Rows[0]["pv"].ToString().Replace('.', ','));
                                        qt = qt + Convert.ToDouble(row1[3].ToString().Replace('.', ','));
                                        pvv = pvv + double.Parse(row1[7].ToString().Replace('.', ','));
                                        string dt_up = fun.update_piece_factt(qt, row1[1].ToString(), id_factt, pvv);
                                    }
                                    else
                                    {
                                        fun.insert_piecee_fact(row1[2].ToString(), row1[1].ToString(), Convert.ToDouble(row1[3].ToString().Replace('.', ',')), row1[4].ToString(), row1[5].ToString(), id_factt, row1[6].ToString(), row1[7].ToString(), dt.Rows[0][12].ToString(), row1[9].ToString(), row1[10].ToString(), row1[11].ToString());
                                    }
                                    tva = row1[10].ToString();
                                    prix = 0;

                                }


                            }
                        }
                        string dd = dateEdit1.Text.Substring(0, 10);//.ToString();
                        dd = dd.Substring(0, 10);
                        num_fact = Tnumfact.Text;
                        string list_bl = "";
                        for (int g = 0; g < l_bl.Count; g++)
                        {
                            list_bl += l_bl[g].Trim() + "/";
                        }
                        if (textEdit1.Text == "")
                        {
                            list_bl =  list_bl.Substring(0, list_bl.Count() - 1);
                        }
                        else
                        {
                            list_bl += textEdit1.Text;// list_bl.Substring(0, list_bl.Count() - 1);
                        }
                        fun.insertintofacturevente(id_clt.ToString(), dd, "en cours", client, idfcmd.ToString(), tt_ttc.ToString("0.000"), "0", tt_ht.ToString("0.000"), timbre, tva, "0", "0", num_fact, idbl, list_bl);

                    }
                    FactureReport report = new FactureReport(id_factt);
                    report.ShowPreview();
                    //num_facture =  NumFact().ToString(); 
                    //facture fact = new facture();
                    //fact.Show();
                }
            }
            Tnumfact.Text = "";
        }

        private void listebls_Load(object sender, EventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {

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
                        id_bl = Convert.ToInt32(row[0]);
                        Point pt = this.Location;
                        pt.Offset(this.Left + e.X, this.Top + e.Y);
                        popupMenu1.ShowPopup(this.barManager1, Control.MousePosition);
                    }
                }
            }
            catch (Exception ce)
            {
            }
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                id_bl = Convert.ToInt32(row[0]);
                string etat = row[3].ToString();
                if (etat == "validée")
                {
                    XtraMessageBox.Show("Ce Bl est déja validée");
                }
                else
                {
                    fun.update_etat_bonlivraison("validée", id_bl);
                    DataTable dtt = fun.get_Allprodbybl(id_bl.ToString());
                    foreach (DataRow rw in dtt.Rows)
                    {
                        fun.update_sousstock_after_accept2(double.Parse(rw[3].ToString().Replace('.', ',')), rw[1].ToString());

                    }
                }
                Liste_cde_clt();
                
            }
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                id_bl = Convert.ToInt32(row[0]);
                string etat = row[3].ToString();
                if (etat.Trim() == "en cours")
                {
                    ModifierBL2 modif = new ModifierBL2(id_bl);
                    modif.ShowDialog();
                }
                else
                {
                    XtraMessageBox.Show("Le Bl est " + etat);
                }
            }
        }
        }
    }
