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
    public partial class listefactvente : DevExpress.XtraEditors.XtraForm
    {
        public listefactvente()
        {
            InitializeComponent();
        }
        sql_gmao fun = new sql_gmao();
        public static int id_fact,id_clt;
        public static int id_bl;
        public static Double somme, totht, totttc, totfodec, tottva, totretenusrc;
        public static string etat_commande, id_factRass,num_fact;
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
            data = fun.selectfromfacturevente();

            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Numero2facture");
            dt.Columns.Add("Numero facture");
            dt.Columns.Add("Date d'ajout");
            dt.Columns.Add("Code fournisseur");
            dt.Columns.Add("Etat");
            dt.Columns.Add("Fournisseur");
            dt.Columns.Add("montant HT");
            dt.Columns.Add("montant TTC");
            dt.Columns.Add("TVA");
            dt.Columns.Add("Remise");
            dt.Columns.Add("timbre");
            dt.Columns.Add("comment");
            dt.Columns.Add("N° commande");
            dt.Columns.Add("Fodec");
            dt.Columns.Add("Retenue à la source");
            dt.Columns.Add("num_bl");
            dt.Columns.Add("avoir",typeof(System.Boolean));

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow ravi = dt.NewRow();
                ravi["Numero2facture"] = data.Rows[i][14];
                ravi["Numero facture"] = data.Rows[i][0];
                ravi["Date d'ajout"] = data.Rows[i][1];
                ravi["Code fournisseur"] = data.Rows[i][2];
                ravi["Etat"] = data.Rows[i][3];
                ravi["Fournisseur"] = data.Rows[i][4];
                ravi["montant HT"] = data.Rows[i][5];
                ravi["montant TTC"] = data.Rows[i][6];
                ravi["TVA"] = data.Rows[i][7];
                ravi["Remise"] = data.Rows[i][8];
                ravi["timbre"] = data.Rows[i][9];
                ravi["comment"] = data.Rows[i][10];
                ravi["N° commande"] = data.Rows[i][11];
                ravi["Fodec"] = data.Rows[i][12];
                ravi["Retenue à la source"] = data.Rows[i][13];
                ravi["num_bl"] = data.Rows[i][15];
                ravi["avoir"] = data.Rows[i][18];

                Boolean avoir = false;
                Boolean.TryParse(data.Rows[i][18].ToString(), out avoir);
                if (!avoir)
                {
                    totht += Convert.ToDouble(data.Rows[i][5].ToString().Replace('.', ','));
                    totttc += Convert.ToDouble(data.Rows[i][6].ToString().Replace('.', ',')) + double.Parse(data.Rows[i][9].ToString().Replace('.', ',')); ;
                    totfodec += Convert.ToDouble(data.Rows[i][12].ToString().Replace('.', ','));
                    tottva += Convert.ToDouble(data.Rows[i][7].ToString().Replace('.', ','));
                    totretenusrc += Convert.ToDouble(data.Rows[i][13].ToString().Replace('.', ','));
                }
                dt.Rows.Add(ravi);

            }
            DataRow ravi1 = dt.NewRow();
            ravi1["Numero2facture"] = "";
            ravi1["Numero facture"] = "total";
            ravi1["Date d'ajout"] = "";
            ravi1["Code fournisseur"] = "";
            ravi1["Etat"] = "";
            ravi1["Fournisseur"] = "";
            ravi1["montant HT"] = "" + totht;
            ravi1["montant TTC"] = "" + totttc;
            ravi1["TVA"] = "" + tottva;
            ravi1["Remise"] = "";
            ravi1["timbre"] = "";
            ravi1["comment"] = "";
            ravi1["N° commande"] = "";
            ravi1["Fodec"] = "" + totfodec;
            ravi1["Retenue à la source"] = "" + totretenusrc;
            ravi1["avoir"] = false;



            dt.Rows.Add(ravi1);
                 
         
            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = dt;
            this.gridView1.Columns[0].Caption = "Numero facture";
            this.gridView1.Columns[1].Visible=false;// = "Numero facture";
            this.gridView1.Columns[2].Caption = "Date d'ajout";
            this.gridView1.Columns[3].Visible = false;
            this.gridView1.Columns[4].Visible=false;// = "Etat";
            this.gridView1.Columns[5].Caption = "Fournisseur";
            this.gridView1.Columns[6].Caption = "Montant_HT";
            this.gridView1.Columns[7].Caption = "Montant TTC";
            this.gridView1.Columns[8].Caption = "TVA";
            this.gridView1.Columns[9].Visible = false;
            this.gridView1.Columns[10].Visible = false;
            this.gridView1.Columns[11].Visible = false;
            this.gridView1.Columns[12].Caption = "N° commande";
            this.gridView1.Columns[13].Visible = false;
            this.gridView1.Columns[14].Visible=false;// = "Retenue à la source";
            this.gridView1.Columns[15].Visible=false;// = "Retenue à la source";
            this.gridView1.Columns[16].Caption = "Avoir";
            //this.gridView1.Columns[16].UnboundType= DevExpress.Data.UnboundColumnType.Boolean;//. =Type.GetType("Boolean");
            //DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit edit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            //edit.ValueChecked = "Y";
            //edit.ValueUnchecked = "N";
            //edit.ValueGrayed = "-";
            //this.gridView1.Columns[16].ColumnEdit = edit;
            gridView1.OptionsView.ShowAutoFilterRow = true;
           
        }

        private void simpleButton2_Click(object sender, EventArgs e)
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

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            try
            {
                int count = gridView1.DataRowCount;
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                    int code = Convert.ToInt32(row[1]);
                    DialogResult dialogResult = XtraMessageBox.Show("Sure de vouloir supprimer la facture  n° " + code + "", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                    {
                        string b = "";
                        string a = "ajoute";
                        sql_gmao dd = new sql_gmao();
                        dd.delete_piece_fromfactvente(code);//supprimer les pieces de commande fournisseur

                        dd.deletefacturevente(code);//supprimer la commande fournisseur
                        Liste_cde_clt();

                    }
                }
            }
            catch (Exception et) { }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {  int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
           {
               System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
               id_fact = Convert.ToInt32(row[1].ToString());
               FactureVente fatvent = new FactureVente(id_fact);
               fatvent.ShowDialog();
               //FactureReport report = new FactureReport(id_fact);
               //report.ShowPreview(); 
               //string dt = row[2].ToString();
               //num_fact = Convert.ToDateTime(dt).Year.ToString().Substring(2, 2) + " /" + row[0].ToString();
               //facturing f = new facturing();
               //f.ShowDialog();
                }
        }

        private void listefactvente_Load(object sender, EventArgs e)
        {

        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                id_fact = Convert.ToInt32(row[1].ToString());
                //FactureVente fatvent = new FactureVente(id_fact);
                //fatvent.ShowDialog();
                FactureReport report = new FactureReport(id_fact);
                report.ShowPreview();
                //string dt = row[2].ToString();
                //num_fact = Convert.ToDateTime(dt).Year.ToString().Substring(2, 2) + " /" + row[0].ToString();
                //facturing f = new facturing();
                //f.ShowDialog();
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

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            Liste_cde_clt();
        }

       

      

       
    }
}