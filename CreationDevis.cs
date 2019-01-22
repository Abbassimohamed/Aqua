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
using DevExpress.XtraEditors.DXErrorProvider;

namespace RibbonSimplePad
{
    public partial class CreationDevis : DevExpress.XtraEditors.XtraForm
    {
        DataTable darttab;// = new DataTable();
        liste_devis list_dev;
        int idrow;
        public CreationDevis(liste_devis listDevis)
        {
            InitializeComponent();
            list_dev = listDevis;
            clients();
            pieces();

            darttab = new DataTable();
            darttab.Clear();
            darttab.Columns.Add("code_piece");
            darttab.Columns.Add("libelle_piece");
            darttab.Columns.Add("quantite_piece");
            darttab.Columns.Add("id_clt");
            
            darttab.Columns.Add("puv");
            darttab.Columns.Add("pv");
            darttab.Columns.Add("remise");
            darttab.Columns.Add("ttva");
            darttab.Columns.Add("unite");
            tnumcommandebase.Text= (get_max_id_devis() + 1).ToString();


        }
        sql_gmao fun = new sql_gmao();
        private void CreationDevis_Load(object sender, EventArgs e)
        {
            dxValidationProvider1.ValidationMode=ValidationMode.Manual;
            
        }
        private void clients()
        {
            //get All stock
            lookUpEdit1.Properties.DataSource = null;
            DataTable Allclients = new DataTable();
            Allclients = fun.get_Allclt();

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
        private void pieces()
        {
            //get All stock
            lookUpEdit2.Properties.DataSource = null;
            DataTable Allclients = new DataTable();
            Allclients = fun.get_allstock();
            
                lookUpEdit2.Properties.ValueMember = "code_piece";
                lookUpEdit2.Properties.DisplayMember = "libelle_piece";
                lookUpEdit2.Properties.DataSource = Allclients;
                lookUpEdit2.Properties.PopulateColumns();


                LookUpColumnInfoCollection coll = lookUpEdit2.Properties.Columns;
                LookUpColumnInfo info;
                info = new LookUpColumnInfo("code_piece", 0);
                info.Caption = "Code Produit";
                coll.Add(info);
                info = new LookUpColumnInfo("libelle_piece", 0);
                info.Caption = "Libellé Produit";
                coll.Add(info);
                info = new LookUpColumnInfo("quantite_piece", 0);
                info.Caption = "Qantité Disponible";
                coll.Add(info);
                info = new LookUpColumnInfo("puv", 0);
                info.Caption = "Prix Unitaire de Vente";
                coll.Add(info);
                info = new LookUpColumnInfo("puv_rev", 0);
                info.Caption = "Prix revendeur";
                coll.Add(info);
                info = new LookUpColumnInfo("unite_piece", 0);
                info.Caption = "Unité";
                coll.Add(info);
            

                

            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataRowView rowView = (DataRowView)lookUpEdit2.GetSelectedDataRow();
                
                DataRowView rowView1 = (DataRowView)lookUpEdit1.GetSelectedDataRow();

                if (rowView == null || rowView1==null)
                {
                    XtraMessageBox.Show("Entrer code clien et code produit");
                }else
                {
                
                
                    if(dxValidationProvider1.Validate()==true)
                    {
                    try
                        {
                            DataRow row = rowView.Row;
                            DataRow row1 = rowView1.Row;
                            double prix_v = 0, qt = 0, porcentageTva = 0, prix = 0, remise = 0;
                            string code_prod = row[0].ToString();
                            string unite = row[2].ToString();
                            string codeclt = row1[0].ToString();
                            DataRow newpc = darttab.NewRow();

                            newpc["code_piece"] = code_prod;
                            newpc["libelle_piece"] = txt_designation.Text;
                            newpc["quantite_piece"] = double.Parse(tquantit.Text.Replace('.', ','));
                            newpc["id_clt"] = codeclt;
                            newpc["puv"] = tpu.Text.Replace('.', ',');
                        //quantite
                            prix_v = double.Parse(tpu.Text.Replace('.', ','));
                            qt = double.Parse(tquantit.Text.ToString().Replace('.', ','));
                            prix_v = prix_v * qt;
                        ////remise
                            remise = (prix_v * Convert.ToDouble(textEdit2.Text.Replace('.', ','))) / 100;
                            prix_v = prix_v - remise;
                        ////tva
                            porcentageTva = (prix_v * Convert.ToDouble(textEdit1.Text.Replace('.', ',')) / 100);
                           prix_v = prix_v + porcentageTva;
                            
                            
                            
                            newpc["pv"] = prix_v;
                            newpc["remise"] = textEdit2.Text.Replace('.', ',');
                            newpc["ttva"] = textEdit1.Text.Replace('.', ',');
                            newpc["unite"] = unite;
                            darttab.Rows.Add(newpc);
                            getalldatatable();
                            updatesum();
                            prix_v = 0; qt = 0; porcentageTva = 0; prix = 0; remise = 0;
                        }
                        catch (Exception ex) { }
                  }
              }
                updatesum();

        }
        public void updatesum()
        {
            double prixtotc =0;
            double.TryParse(textBox5.Text.Replace('.', ','), out prixtotc);
            for (int i = 0; i < gridView1.RowCount; i++)
            {

                DataRow row1 = gridView1.GetDataRow(i);
                double prix=double.Parse(row1[5].ToString().Replace('.',','));
                prixtotc += prix;
                

            }


            textBox1.Text = prixtotc.ToString();

        }
        public double montant_HTV()
        {
            double prixtotc = 0, prix = 0,  remise = 0;

            for (int i = 0; i < gridView1.RowCount; i++)
            {

                DataRow row1 = gridView1.GetDataRow(i);
                
                
                
                
                prix = Convert.ToDouble(row1[5].ToString().Replace('.', ','));
                //remise = (prix * Convert.ToDouble(row1[6].ToString().Replace('.', ','))) / 100;
                //prix = prix - remise;
                prixtotc += prix;
                prix = 0;
                
                remise = 0;

            }


            return prixtotc;

        }
        private void getalldatatable()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = darttab;

            this.gridView1.Columns[0].Caption = "Code Produit";
            this.gridView1.Columns[1].Caption = "Désignation";
            this.gridView1.Columns[2].Caption = "Quantité";
            this.gridView1.Columns[3].Visible = false;
            this.gridView1.Columns[4].Caption = "PUV";
            this.gridView1.Columns[5].Caption = "PV";
            this.gridView1.Columns[6].Caption = "Remise";
            this.gridView1.Columns[7].Caption = "TVA";
            this.gridView1.Columns[8].Caption = "Unité";
            
            gridView1.OptionsView.ShowAutoFilterRow = true;

        }
        private int get_max_id_devis()
        {
            int y = 0;
            int x = 0;
            DataTable dt = new DataTable();
            DataTable dtbl = new DataTable();
            DataTable data = new DataTable();
            dt = fun.getcountcmd("devis");

            if (dt.Rows.Count == 0)
            {
                data = fun.getcurrentvalue("devis");

                if (Convert.ToInt32(data.Rows[0][0]) == 0)
                {
                    fun.resetautoincrement("devis", 0);
                    y = Convert.ToInt32(data.Rows[0][0]);
                }
                else
                {
                    fun.resetautoincrement("devis", 0);
                    data = fun.getcurrentvalue("devis");
                    y = Convert.ToInt32(data.Rows[0][0]);
                }
            }
            else
            {
                dtbl = fun.get_max_devis();
                x = Convert.ToInt32(dtbl.Rows[0][0]);
                fun.resetautoincrement("devis", x);
                data = fun.getcurrentvalue("devis");
                y = Convert.ToInt32(data.Rows[0][0]);

            }

            return y;

        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            string etat = "en cours";
            DataRowView rowView = (DataRowView)lookUpEdit2.GetSelectedDataRow();
                DataRow row = rowView.Row;
                DataRowView rowView1 = (DataRowView)lookUpEdit1.GetSelectedDataRow();
                DataRow row1 = rowView1.Row;
                
                string code_prod = row[0].ToString();
                string codeclt = row1[0].ToString();
                double remise = 0,ttch=0,ttv=0;
                //ttv=montant_HTV();
                //remise = (ttv*double.Parse(textEdit2.Text.Replace('.',',')))/100;
                ttch =montant_HTV();
                int newId = get_max_id_devis() + 1;
                var res = fun.insertdb__into_devis(DateTime.Now.ToString(), etat, DateTime.Now.ToString(), "DT", "", textBox5.Text, "", row1[1].ToString(), textBox1.Text, ttch.ToString(), codeclt, tnbcmd.Text, textEdit4.Text);
                

               
                
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    DataRow rows = gridView1.GetDataRow(i);
                    fun.insert_piecee_devis2(rows[1].ToString(), rows[0].ToString(), rows[2].ToString(), newId, rows[4].ToString(), rows[5].ToString(), rows[6].ToString(), rows[7].ToString(), rows[8].ToString());

                }
                list_dev.Liste_devis();
                remise = 0;
                ttch = 0;
                this.Close();

        }

        private void lookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView rowView = (DataRowView)lookUpEdit2.GetSelectedDataRow();
            DataRow row = rowView.Row;
            txt_designation.Text=row[1].ToString();
            tpu.Text = row[8].ToString();
        }

        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl5_Click(object sender, EventArgs e)
        {

        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl9_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            //FactureDevis fac_dev = new FactureDevis(77);
            //fac_dev.ShowDialog();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            DataRowView rowView = (DataRowView)lookUpEdit2.GetSelectedDataRow();
            if (rowView == null)
            {
                XtraMessageBox.Show("Selectionner un produit");
            }
            else
            {
                DataRow row = rowView.Row;
                if (checkEdit1.Checked == true)
                {
                    tpu.Text = row[4].ToString();
                    tpu.Enabled = false;
                }
                else
                {
                    tpu.Enabled = true;
                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
               DataRowView rowView = (DataRowView)lookUpEdit2.GetSelectedDataRow();

               DataRowView rowView1 = (DataRowView)lookUpEdit1.GetSelectedDataRow();
               DataRow row2 = rowView.Row;
               DataRow row1 = rowView1.Row;
               double prix_v = 0, qt = 0, porcentageTva = 0, prix = 0, remise = 0;
               string code_prod = row2[0].ToString();
               string unite = row2[2].ToString();
               string codeclt = row1[0].ToString();
               var row = gridView1.GetDataRow(idrow);
               row[0] = code_prod;
               row[1] = txt_designation.Text;
               row[2] = double.Parse(tquantit.Text.Replace('.', ','));
               row[3] = codeclt;
               row[4] = tpu.Text.Replace('.', ',');
               //quantite
               prix_v = double.Parse(tpu.Text.Replace('.', ','));
               qt = double.Parse(tquantit.Text.ToString().Replace('.', ','));
               prix_v = prix_v * qt;
               ////remise
               remise = (prix_v * Convert.ToDouble(textEdit2.Text.Replace('.', ','))) / 100;
               prix_v = prix_v - remise;
               ////tva
               porcentageTva = (prix_v * Convert.ToDouble(textEdit1.Text.Replace('.', ',')) / 100);
               prix_v = prix_v + porcentageTva;



               row[5] = prix_v;
               row[6] = textEdit2.Text.Replace('.', ',');
               row[7] = textEdit1.Text.Replace('.', ',');
               row[8] = unite;
               var rdt = darttab.Rows[idrow];
                //dt
               rdt[0] = code_prod;
               rdt[1] = txt_designation.Text;
               rdt[2] = double.Parse(tquantit.Text.Replace('.', ','));
               rdt[3] = codeclt;
               rdt[4] = tpu.Text.Replace('.', ',');
               rdt[5] = prix_v;
               rdt[6] = textEdit2.Text.Replace('.', ',');
               rdt[7] = textEdit1.Text.Replace('.', ',');
               rdt[8] = unite;

               updatesum();
               simpleButton2.Enabled = false;
            }
            catch (Exception ext)
            {

            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            simpleButton2.Enabled = true;
            try
            {
                int count = gridView1.DataRowCount;
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    idrow = gridView1.FocusedRowHandle;

                    DataRow row = gridView1.GetDataRow(idrow);
                   lookUpEdit2.EditValue= row[0].ToString();
                    txt_designation.Text=row[1].ToString();
                    tquantit.Text= row[2].ToString();
                    //row[3] = codeclt;
                   tpu.Text= row[4].ToString();
                   
                  
                    textEdit2.Text=row[6].ToString();
                   textEdit1.Text= row[7].ToString();
                }
            }
            catch (Exception er) { }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                int count = gridView1.DataRowCount;
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                    darttab.Rows[idrow].Delete();
                    getalldatatable();
                    updatesum();
                }
            }
            catch (Exception ert) { }
        }

        private void labelControl8_Click(object sender, EventArgs e)
        {

        }

        

        
    }
}