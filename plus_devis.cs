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
    public partial class plus_devis : DevExpress.XtraEditors.XtraForm
    {
        public plus_devis()
        {
            InitializeComponent();
        }
        sql_gmao fun = new sql_gmao();
        public static string code, lib;
        public static Double puv;
        private void plus_devis_Load(object sender, EventArgs e)
        {
            pieces();
        }

        private void pieces()
        {
            //get All stock
            lookupedit1.Properties.DataSource = null;
            DataTable Allclients = new DataTable();
            Allclients = fun.get_stock();
            for (int i = 0; i < Allclients.Rows.Count; i++)
            {
                lookupedit1.Properties.ValueMember = "code_piece";
                lookupedit1.Properties.DisplayMember = "libelle_piece";
                lookupedit1.Properties.DataSource = Allclients;
                lookupedit1.Properties.PopulateColumns();
                lookupedit1.Properties.Columns["code_piece"].Caption = "Code Produit";
                lookupedit1.Properties.Columns["libelle_piece"].Caption = "Libellé Produit";
                lookupedit1.Properties.Columns["unite_piece"].Visible = false;
                lookupedit1.Properties.Columns["quantite_piece"].Caption = "Qantité Disponible";
                lookupedit1.Properties.Columns["quantite_reelle"].Visible = false;
                lookupedit1.Properties.Columns["seuil_piece"].Visible = false;
                lookupedit1.Properties.Columns["nature"].Visible = false;
                lookupedit1.Properties.Columns["pua"].Visible = false;
                lookupedit1.Properties.Columns["puv"].Caption = "Prix Unitaire de Vente";
                lookupedit1.Properties.Columns["empalcement_piece"].Visible = false;
                lookupedit1.Properties.Columns["code_feur"].Visible = false;
                lookupedit1.Properties.Columns["puv_rev"].Visible = false;
               
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked == true)
            {
                memoEdit1.Enabled = true;
                lookupedit1.Visible = false;
                textEdit2.Enabled = true;
                checkEdit2.Visible = false;
            }

            else
            {
                memoEdit1.Enabled = false;
                lookupedit1.Visible = true;
                textEdit2.Enabled = false;
                checkEdit2.Visible = true;
            }
        }

        private void lookupedit1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable tt = new DataTable();
                tt = fun.get_infos_piece(lookupedit1.EditValue.ToString());
                if (tt.Rows.Count != 0)
                {
                    lib = tt.Rows[0]["libelle_piece"].ToString();
                    puv = Convert.ToDouble(tt.Rows[0]["puv"].ToString().Replace('.',','));
                    code = lookupedit1.EditValue.ToString();

                    textEdit2.Text = ("" + puv);
                }
                checkEdit2.Visible = true;
            }
            catch (Exception exce)
            { }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

          
            if (checkEdit1.Checked == false)
            {
                if (lookupedit1.Text=="Choisir Parmi Liste de Produits" || textEdit1.Text == "" || textEdit2.Text == "")
                {
                    XtraMessageBox.Show("Veuillez remplir les champs manquants ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else {

                    double pv;
                    pv = Convert.ToDouble((textEdit1.Text)) * Convert.ToDouble(textEdit2.Text);
                    fun.insert_piecee_devis(lib,code, textEdit1.Text,ajout_devis.id_devis,textEdit2.Text,pv.ToString()); }
            }

            else {

                if (memoEdit1.Text=="" || textEdit1.Text == "" || textEdit2.Text == "")
                {
                    XtraMessageBox.Show("Veuillez remplir les champs manquants ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 return;
                }
                else {

                    Double pv;
                    pv = Convert.ToDouble((textEdit1.Text).Replace('.',',')) * Convert.ToDouble(textEdit2.Text);
                    fun.insert_piecee_devis(memoEdit1.Text, code, textEdit1.Text, ajout_devis.id_devis, textEdit2.Text, pv.ToString()); }
            }


            this.Close();
            }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {

            if (checkEdit2.Checked == true)
            { 
            
            DataTable tt = new DataTable();
            tt = fun.get_infos_piece(lookupedit1.EditValue.ToString());
            if (tt.Rows.Count != 0)
            {
               
                Double rev = Convert.ToDouble(tt.Rows[0]["puv_rev"]);
              

                textEdit2.Text = rev.ToString();
            }
            }


             if (checkEdit2.Checked == false)
            { 
            
            DataTable tt = new DataTable();
            tt = fun.get_infos_piece(lookupedit1.EditValue.ToString());
            if (tt.Rows.Count != 0)
            {
               
                Double puv = Convert.ToDouble(tt.Rows[0]["puv"]);
              

                textEdit2.Text = puv.ToString();
            }
            }
        }

        }






        }

    
