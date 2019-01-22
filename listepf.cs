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
    public partial class listepf : DevExpress.XtraEditors.XtraForm
    {
        public listepf()
        {
            InitializeComponent();
            Liste_cde_clt();
        }
        sql_gmao fun = new sql_gmao();
        public static int id_fact,id_clt,idfactt;
        public static int id_bl;
        public static Double tottc = 0;
        public static string etat_commande, id_factRass;
      
        private void Liste_cde_clt()
        {
            
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = fun.get_facturepf();
            this.gridView1.Columns[0].Visible=false;// = "Numero facture";
            this.gridView1.Columns[1].Caption = "Date d'ajout";
            this.gridView1.Columns[2].Visible = false;
            this.gridView1.Columns[3].Caption = "Etat";
            this.gridView1.Columns[4].Caption = "Nom Client";
            this.gridView1.Columns[5].Visible = false;
            this.gridView1.Columns[6].Caption = "Montant TTC";
            this.gridView1.Columns[7].Visible = false;
            this.gridView1.Columns[8].Visible = false;
            this.gridView1.Columns[9].Visible = false;
            this.gridView1.Columns[10].Caption = "Commentaire";
            this.gridView1.Columns[11].Caption = "Ref commande";
            this.gridView1.Columns[12].Caption = "Numero facture";
            this.gridView1.Columns[12].AppearanceCell.TextOptions.HAlignment = (DevExpress.Utils.HorzAlignment)HorizontalAlignment.Center;
           
           
            gridView1.OptionsView.ShowAutoFilterRow = true;
            updatesum();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
           int count = gridView1.DataRowCount;
           DialogResult dialogResult = XtraMessageBox.Show("Sure de vouloir supprimer la facture PF ", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
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
                       dd.delete_facturepf(code);
                       dd.delete_piece_factpf(code);
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
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                //idfactt = Convert.ToInt32(row[22].ToString());
                id_fact = Convert.ToInt32(row[0].ToString());

                FacturePfReport report = new FacturePfReport(id_fact);
                report.ShowPreviewDialog();
                //facturingpf fc = new facturingpf();
                //fc.ShowDialog();
          
           
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listepf_Load(object sender, EventArgs e)
        {

        }

      

       
    }
}