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
    public partial class FactureBLs : DevExpress.XtraEditors.XtraForm
    {
        sql_gmao fun = new sql_gmao();
        public float x = 0;
        public float y = 0;
           
        public FactureBLs()
        {
            InitializeComponent();
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit mEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            gridControl1.RepositoryItems.Add(mEdit);
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd h:mm tt";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "yyyy-MM-dd h:mm tt";
            
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = fun.get_PSales(textBox1.Text, dateTimePicker1.Value, dateTimePicker2.Value);
            this.gridView1.Columns[0].Visible = false;
            this.gridView1.Columns[1].Caption = "REF";
            this.gridView1.Columns[2].Caption = "Designation";
            gridView1.Columns[2].ColumnEdit = mEdit;
            this.gridView1.Columns[3].Caption = "QTE";
            this.gridView1.Columns[4].Caption = "Code client";
            this.gridView1.Columns[5].Visible = false;
            this.gridView1.Columns[6].Caption = "Facture N°";
            this.gridView1.Columns[7].Visible = false;
            this.gridView1.Columns[8].Visible = false;
            this.gridView1.Columns[9].Caption = "Prix Unitaire HT";
            this.gridView1.Columns[10].Caption = "Montant HT";
            this.gridView1.Columns[11].Visible=false;
          

            gridView1.OptionsView.ShowAutoFilterRow = true;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                DataRow row = gridView1.GetDataRow(i);
                x += Convert.ToInt32(row[10]);
                y += Convert.ToInt32(row[3]);

            }

            label2.Text = x.ToString();
            label4.Text = y.ToString();

        }

        
    }
}