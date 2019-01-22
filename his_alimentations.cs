using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils.Animation;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Text.RegularExpressions;

namespace RibbonSimplePad
{
    public partial class his_alimentations : DevExpress.XtraEditors.XtraForm
    {
        public his_alimentations()
        {
            InitializeComponent();
        }
        sql_gmao fun = new sql_gmao();
      
        private void ListeStock()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = fun.get_his_alimentations();
            this.gridView1.Columns[0].Visible = false;
            this.gridView1.Columns[1].Caption = "Date";
            this.gridView1.Columns[2].Caption = "Heure";
            this.gridView1.Columns[3].Caption = "Piéce";
            this.gridView1.Columns[4].Caption = "Quantité";
            this.gridView1.Columns[5].Caption = "Commentaire";
            this.gridView1.Columns[6].Caption = "Nature";
    
            gridView1.OptionsView.ShowAutoFilterRow = true;
        }

        private void his_alimentations_Load(object sender, EventArgs e)
        {
            ListeStock();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ListeStock();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            gridControl1.ShowRibbonPrintPreview();
        }

        private void his_alimentations_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }

        private void his_alimentations_Activated(object sender, EventArgs e)
        {
            Form1.load = 1;

            Form1.wait = 1;
            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridView1.BestFitColumns();
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.EnableAppearanceEvenRow = true;
        }
        
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                int code = Convert.ToInt32(row[0]);
                DialogResult dialogResult = XtraMessageBox.Show("Sure de vouloir supprimer l'enregistrement n° " + code + "", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    
                    fun.delete_aliment(code);
                    ListeStock();
                }
            }
        }

       
    }
}