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
    public partial class Magasin : DevExpress.XtraEditors.XtraForm
    {
        public Magasin()
        {
            InitializeComponent();
        }
        sql_gmao fun = new sql_gmao();
        public static int id_magasin;
        private void Magasin_Load(object sender, EventArgs e)
        {

        }
        private void GetAllMagasin()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = fun.get_Magasin();
            this.gridView1.Columns[0].Visible = false;
            this.gridView1.Columns[1].Caption = "Désigniation";
            gridView1.OptionsView.ShowAutoFilterRow = true;
        }

        private void Magasin_Activated(object sender, EventArgs e)
        {
            GetAllMagasin();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text += "a";
            if (label1.Text.ToString() == "10aaa")
            {
                labelControl2.Visible = false;
                labelControl3.Visible = false;
                labelControl4.Visible = false;
                label1.Text = "10";
                timer1.Stop();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text == "")
            {
                dxErrorProvider1.Dispose();
                dxErrorProvider1.SetError(textEdit1, "Champ Obligatoire");
            }
            else
            {
                DataTable UniteExist = new DataTable();
                UniteExist = fun.get_MagasinByDes(textEdit1.Text);
                if (UniteExist.Rows.Count != 0)
                {
                    dxErrorProvider1.Dispose();
                    dxErrorProvider1.SetError(textEdit1, "Le Magasin existe déjà!!!");
                }
                else
                {
                    dxErrorProvider1.Dispose();
                    fun.set_Magasin(textEdit1.Text);
                    labelControl2.Visible = true;
                    timer1.Start();
                    textEdit1.Text = "";
                    GetAllMagasin();
                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text == "")
            {
                dxErrorProvider1.Dispose();
                dxErrorProvider1.SetError(textEdit1, "Champ Obligatoire");
            }
            else
            {
                DataTable UniteExist = new DataTable();
                UniteExist = fun.get_MagasinByDes(textEdit1.Text);
                if (UniteExist.Rows.Count != 0)
                {
                    dxErrorProvider1.Dispose();
                    dxErrorProvider1.SetError(textEdit1, "Le Magasin existe déjà!!!");
                }
                else
                {
                    dxErrorProvider1.Dispose();
                    fun.update_Magasin(textEdit1.Text, id_magasin);
                    labelControl3.Visible = true;
                    textEdit1.Text = "";
                    timer1.Start();
                    GetAllMagasin();
                }
            }

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                id_magasin = Convert.ToInt32(row[0]);
                DialogResult dialogResult = XtraMessageBox.Show("Sure de vouloir supprimer ce magasin!!!", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    fun.delete_Magasin(id_magasin);
                    GetAllMagasin();
                    labelControl4.Visible = true;
                    timer1.Start();
                }
            }
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                textEdit1.Text = Convert.ToString(row[1]);
                id_magasin = Convert.ToInt32(row[0]);
            }

        }
    }
}