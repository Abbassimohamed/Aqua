using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace RibbonSimplePad
{
    public partial class SuiviClt : DevExpress.XtraEditors.XtraForm
    {
        sql_gmao fun = new sql_gmao();


        public SuiviClt()
        {
            InitializeComponent();
            clients();
        }
        private void clients()
        {

            lookUpEdit1.Properties.DataSource = null;
            DataTable Allclients = new DataTable();
            Allclients = fun.get_clients();

            lookUpEdit1.Properties.DataSource = Allclients;
            LookUpColumnInfo info;


            lookUpEdit1.Properties.ValueMember = "code_clt";
            lookUpEdit1.Properties.DisplayMember = "raison_soc";
            lookUpEdit1.Properties.PopulateColumns();

            LookUpColumnInfoCollection coll = lookUpEdit1.Properties.Columns;

            info = new LookUpColumnInfo("code_clt", 0);
            info.Caption = "Code client";
            coll.Add(info);
            info = new LookUpColumnInfo("raison_soc", 0);
            info.Caption = "Raison sociale";
            coll.Add(info);
            info = new LookUpColumnInfo("responsbale", 0);
            info.Caption = "Responsable";
            coll.Add(info);


        }


        private void button1_Click_2(object sender, EventArgs e)
        {
            double x = 0;
            double y = 0;
            DataRowView rowView = (DataRowView)lookUpEdit1.GetSelectedDataRow();
            if (rowView == null)
            {
                XtraMessageBox.Show("Selectionner le client");
            }
            else
            {

                DataRow rows = rowView.Row;
                DataTable dt_Sclt = fun.get_CltSuivi(rows[0].ToString(), dateTimePicker1.Value, dateTimePicker2.Value);
               
                double gain = 0, pua = 0,qt=0;

               
               

                DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit mEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
                gridControl1.RepositoryItems.Add(mEdit);
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = "yyyy-MM-dd h:mm tt";
                dateTimePicker2.Format = DateTimePickerFormat.Custom;
                dateTimePicker2.CustomFormat = "yyyy-MM-dd h:mm tt";

                gridControl1.DataSource = null;
                gridView1.Columns.Clear();

                gridControl1.DataSource = dt_Sclt;
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
                this.gridView1.Columns[10].Caption = "Montant TTC";
                this.gridView1.Columns[11].Visible = false;


                gridView1.OptionsView.ShowAutoFilterRow = true;

                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    DataRow row = gridView1.GetDataRow(i);
                    var rwx = gridView1.GetDataRow(i);
                    DataTable dt_fact = fun.get_etat_factvente(int.Parse(row[6].ToString()));
                    if (dt_fact.Rows.Count > 0)
                    {
                        rwx[6] = dt_fact.Rows[0]["numero_fact"].ToString();
                    }
                    x += double.Parse(row[10].ToString().Replace('.', ','));
                    y += double.Parse(row[3].ToString().Replace('.', ','));

                    try
                    {
                        DataTable dt_prix_prod = fun.prix_achat_prod(row[1].ToString());
                        pua = double.Parse(dt_prix_prod.Rows[0]["pua"].ToString().Replace('.', ','));

                        double.TryParse(row[3].ToString().Replace('.', ','), out qt);
                        pua = pua * qt;
                        gain += double.Parse(row[10].ToString().Replace('.', ',')) - pua;
                    }
                    catch (Exception ff)
                    {
                    }


                }
                DataRow drw = dt_Sclt.NewRow();
                drw[9] = "Total vente :";
                drw[10] = x.ToString("0.000");
                dt_Sclt.Rows.Add(drw);
                gridView1.RefreshData();
                label2.Text = x.ToString();
                label4.Text = y.ToString();
                label6.Text = gain.ToString();
                gain = 0;
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            gridView1.ShowRibbonPrintPreview();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void SuiviClt_Load(object sender, EventArgs e)
        {

        }

       



    }
}