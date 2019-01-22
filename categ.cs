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
    public partial class categ : DevExpress.XtraEditors.XtraForm
    {
        sql_gmao fun = new sql_gmao();
        public static int id_categ;
        public static string type;
        public categ()
        {
            InitializeComponent();
        }

        private void GetAllMagasin()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();

          
            if (compta.categggg == "encaissement")
            {
                string a = "encaissement";
                gridControl1.DataSource = fun.get_categ(a);
              
            }

            if (compta.categggg == "decaissement")
            {
                string a = "decaissement";
                gridControl1.DataSource = fun.get_categ(a);
               
            }
            
         
            this.gridView1.Columns[0].Visible = false;
            this.gridView1.Columns[1].Caption = "Désigniation";
            this.gridView1.Columns[2].Visible = false;
            gridView1.OptionsView.ShowAutoFilterRow = true;
        }

        private void categ_Load(object sender, EventArgs e)
        {

        }

        private void categ_Activated(object sender, EventArgs e)
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
                    UniteExist = fun.get_categ2(textEdit1.Text);
                    if (UniteExist.Rows.Count != 0)
                    {
                        dxErrorProvider1.Dispose();
                        dxErrorProvider1.SetError(textEdit1, "La catégorie existe déjà!!!");
                    }
                    else
                    {
                        dxErrorProvider1.Dispose();

                    
                        if (compta.categggg == "encaissement")
                        { type = "encaissement"; }
                        if (compta.categggg == "decaissement")
                        { type = "decaissement"; }
                        fun.set_categ_encaiss(textEdit1.Text, type);
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
                UniteExist = fun.get_categ2(textEdit1.Text);
                if (UniteExist.Rows.Count != 0)
                {
                    dxErrorProvider1.Dispose();
                    dxErrorProvider1.SetError(textEdit1, "La catégorie existe déjà!!!");
                }
                else
                {
                    dxErrorProvider1.Dispose();
                    fun.update_categ_encaiss(textEdit1.Text, id_categ);
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
                id_categ = Convert.ToInt32(row[0]);
                DialogResult dialogResult = XtraMessageBox.Show("Sure de vouloir supprimer cette catégorie!!!", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    fun.delete_categ_encaiss(id_categ);
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
                id_categ = Convert.ToInt32(row[0]);
            }
        }
    }
}