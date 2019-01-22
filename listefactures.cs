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
    public partial class listefactures : DevExpress.XtraEditors.XtraForm
    {
        public listefactures()
        {
            InitializeComponent();
            Liste_cde_clt();
        }
        sql_gmao fun = new sql_gmao();
        public static int id_fact,id_clt,idfactt;
        public static int id_bl;
        public static Double tottc = 0;
        public static string etat_commande, id_factRass;
        //private void liste_cde_client_Activated(object sender, EventArgs e)
        //{
        //    if (login1.depart == "Utilisateur")
        //    {
        //        //facture
        //        if (login1.fact == "OUI") { barButtonItem1.Enabled = true; }
        //        else { barButtonItem1.Enabled = false; }
        //        //bon livraison
        //        if (login1.bon_liv == "OUI") { barButtonItem2.Enabled = true; }
        //        else { barButtonItem2.Enabled = false; }
        //        //bon sortie
        //        if (login1.bon_sort == "OUI") { barButtonItem5.Enabled = true; }
        //        else { barButtonItem5.Enabled = false; }
        //        //exporter droit
        //        if (login1.clt_doc == "OUI") { simpleButton3.Enabled = true; }
        //        else { simpleButton3.Enabled = false; }
        //        //supp
        //        if (login1.supp_cde_clt == "OUI") { simpleButton2.Enabled = true; }
        //        else { simpleButton2.Enabled = false; }
        //        //valider bon commande
        //        if (login1.valid_cde_clt == "OUI") { barButtonItem3.Enabled = true; }
        //        else { barButtonItem3.Enabled = false; }

        //    }
        //    if (login1.depart == "Administrateur")
        //    {
        //        barButtonItem1.Enabled = true; barButtonItem2.Enabled = true;
        //        simpleButton2.Enabled = true; barButtonItem3.Enabled = true;
        //        simpleButton3.Enabled = true; barButtonItem5.Enabled = true;

        //    }
        //    Liste_cde_clt();
        //    Form1.load = 1;
         
        //    Form1.wait = 1;
        //    gridView1.OptionsView.ShowAutoFilterRow = true;
        //    gridView1.BestFitColumns();
        //    gridView1.OptionsBehavior.Editable = false;
        //    gridView1.OptionsView.EnableAppearanceEvenRow = true;
        //}
        //
        private void Liste_cde_clt()
        {
            
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = fun.get_facture();
            this.gridView1.Columns[0].Caption = "Numero facture";
            this.gridView1.Columns[1].Visible = false;
            this.gridView1.Columns[2].Visible = false;
            this.gridView1.Columns[3].Caption = "Code Client";
            this.gridView1.Columns[4].Caption = "Etat";
            this.gridView1.Columns[5].Caption = "Nom Client";
            this.gridView1.Columns[6].Caption = "Etat Facture";
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
            updatesum();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
           int count = gridView1.DataRowCount;
           DialogResult dialogResult = XtraMessageBox.Show("Sure de vouloir supprimer la facture ", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
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
                       dd.delete_facture(code);
                       dd.delete_piece_fact(code);
                   }
               Liste_cde_clt();
               }
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

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            gridControl1.ShowRibbonPrintPreview();
        }
        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                idfactt = Convert.ToInt32(row[22].ToString());
                id_fact = Convert.ToInt32(row[0].ToString());
                facturing fc = new facturing();
                fc.ShowDialog();
            }
            catch (Exception exception)
            { 
            }
        }

       
        public void updatesum()
        {
            tottc = 0.500;

            for (int i = 0; i < gridView1.RowCount; i++)
            {

                DataRow row1 = gridView1.GetDataRow(i);
                tottc += Convert.ToDouble(row1[15].ToString().Replace('.', ','));

            }


            textBox1.Text = tottc.ToString();

        }

      

       
    }
}