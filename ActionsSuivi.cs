using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace RibbonSimplePad
{
    public partial class ActionsSuivi : DevExpress.XtraEditors.XtraForm
    {
        public ActionsSuivi()
        {
            InitializeComponent();
        }
        sql_gmao fun = new sql_gmao();
        private void ActionsSuivi_Load(object sender, EventArgs e)
        {
        }
        private void get_AllActions()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = fun.get_AllActions();
            this.gridView1.Columns[0].Visible = false;
            this.gridView1.Columns[1].Caption = "Actions";
            this.gridView1.Columns[2].Caption = "Utilisateur";
            this.gridView1.Columns[3].Caption = "Date";
        }

        private void ActionsSuivi_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }

        private void ActionsSuivi_Activated(object sender, EventArgs e)
        {
            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridView1.BestFitColumns();
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.EnableAppearanceEvenRow = true;
            get_AllActions();
            gridView1.BestFitColumns();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            fun.vider_Actions();
            get_AllActions();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            gridControl1.ShowRibbonPrintPreview();
        }
    }
}