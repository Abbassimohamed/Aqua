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
using System.IO.IsolatedStorage;
using System.IO;
namespace RibbonSimplePad
{
    public partial class gestionStock : DevExpress.XtraEditors.XtraForm
    {

        
        public gestionStock()
        {
            InitializeComponent();
        }

        sql_gmao fun = new sql_gmao();
        public static unite form_unite = new unite();
        public static famille form_famille = new famille();
        public static Magasin form_magasin = new Magasin();
        public static string fr, feurInitale, nomFeurInit, uniteInitale, Newunite, EmplaceInitale, NewEmp, test, lib, int_piece,familleinitiale,newfamille;
        public static Double qte,somme;
        public static Double pua, pv , punitaire;
        public static int qte2;
      

        private void gestionStock_Load(object sender, EventArgs e)
        {

            ListeStock();
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                FeurPiece(Convert.ToString(row[10]));
            }
            gridView1.BestFitColumns();
            gridView2.BestFitColumns();
            getFamille3();


        }
        private void ListeStock()
        {
            somme = 0;
            double somme_pix_achat = 0, somme_prix_vent = 0;
            DataTable data = new DataTable();
            data.Clear();
            DataRowView rowView = (DataRowView)lookUpEdit3.GetSelectedDataRow();
            if (rowView == null)
            {
                data = fun.get_allstock();
            }
            else
            {
                DataRowView rowView1 = (DataRowView)lookUpEdit3.GetSelectedDataRow();
                string famille = rowView1[1].ToString();
                if (famille.Trim() == "ALL")
                {
                    data = fun.get_allstock();
                }
                else
                {
                   

                    data = fun.get_allstock2(famille);
                }
            }      
                
            
          
            DataTable dt = new DataTable();
            dt.Clear();

            dt.Columns.Add("code_Piece");
            dt.Columns.Add("libelle_piece");
            dt.Columns.Add("unite_piece");
            dt.Columns.Add("quantite_piece");
            dt.Columns.Add("quantite_reelle");
            dt.Columns.Add("seuil_piece");
            dt.Columns.Add("nature");
            dt.Columns.Add("pua");
            dt.Columns.Add("puv");
            dt.Columns.Add("empalcement_piece");
            dt.Columns.Add("code_feur");
            dt.Columns.Add("puv_rev");
            dt.Columns.Add("couleur");
            dt.Columns.Add("taille");
            dt.Columns.Add("sous_categorie");
            dt.Columns.Add("total_prix_achat");
            dt.Columns.Add("total_prix_vente");
            dt.Columns.Add("id");
           
          


            for (int i = 0; i < data.Rows.Count;i++ )
            {
                DataRow ravi = dt.NewRow();

                ravi["code_Piece"] = data.Rows[i][0];
                ravi["libelle_piece"] = data.Rows[i][1];
                ravi["unite_piece"] = data.Rows[i][2];
                ravi["quantite_piece"] = data.Rows[i][3];
                ravi["quantite_reelle"] = data.Rows[i][4];
                ravi["seuil_piece"] = data.Rows[i][5];
                ravi["nature"] = data.Rows[i][6];
                ravi["pua"] = data.Rows[i][7];
                ravi["puv"] = data.Rows[i][8];
                ravi["empalcement_piece"] = data.Rows[i][9];
                ravi["code_feur"] = data.Rows[i][10];
                ravi["puv_rev"] = data.Rows[i][11];
                ravi["couleur"] = data.Rows[i][12];
                ravi["taille"] = data.Rows[i][13];
                ravi["sous_categorie"] = data.Rows[i][14];
                ravi["id"] = data.Rows[i][15];
               
                ravi["total_prix_achat"] = double.Parse(data.Rows[i][3].ToString().Replace('.',','))*double.Parse(data.Rows[i][7].ToString().Replace('.',','));
                ravi["total_prix_vente"] = double.Parse(data.Rows[i][3].ToString().Replace('.', ',')) * double.Parse(data.Rows[i][8].ToString().Replace('.', ','));

                somme_pix_achat += double.Parse(data.Rows[i][3].ToString().Replace('.', ',')) * double.Parse(data.Rows[i][7].ToString().Replace('.', ','));
                somme_prix_vent += double.Parse(data.Rows[i][3].ToString().Replace('.', ',')) * double.Parse(data.Rows[i][8].ToString().Replace('.', ','));
                //somme += Convert.ToDouble(data.Rows[i][3].ToString().Replace('.',',')) * Convert.ToDouble(data.Rows[i][7].ToString().Replace('.',','));
                dt.Rows.Add(ravi);
               
            }
            DataRow ravi1 = dt.NewRow();

            ravi1["code_Piece"] = "";
            ravi1["libelle_piece"] = "total";
            ravi1["unite_piece"] = "";
            ravi1["quantite_piece"] = "";
            ravi1["quantite_reelle"] = "";
            ravi1["seuil_piece"] = "";
            ravi1["nature"] = "";
            ravi1["pua"] = "";
            ravi1["puv"] ="";
            ravi1["empalcement_piece"] ="";
            ravi1["code_feur"] = "";
            ravi1["puv_rev"] = "";
            ravi1["couleur"] = "";
            ravi1["taille"] = "";
            ravi1["sous_categorie"] = "";
            ravi1["total_prix_achat"] = "" + somme_pix_achat;
            ravi1["total_prix_vente"] = "" + somme_prix_vent;
            ravi1["id"] = "";
          


            dt.Rows.Add(ravi1);
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();

            gridControl1.DataSource = dt;
          
            
            
            DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit mEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            gridControl1.RepositoryItems.Add(mEdit);
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = dt;
            this.gridView1.Columns[0].Caption = "Code pièce";
            this.gridView1.Columns[1].Caption = "Désignation";
            gridView1.Columns[1].ColumnEdit = mEdit;
            gridView1.Columns[1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridView1.Columns[2].Caption = "Unité";
            this.gridView1.Columns[3].Caption = "Quantité";
            this.gridView1.Columns[4].Visible = false;
            this.gridView1.Columns[5].Caption = "Stock alerte";
            this.gridView1.Columns[6].Caption = "Nature";
            this.gridView1.Columns[7].Caption="Prix d'achat";
            this.gridView1.Columns[8].Caption = "Prix de vente";
            this.gridView1.Columns[9].Visible = false;
            this.gridView1.Columns[10].Visible = false;
            this.gridView1.Columns[11].Visible = false;
            this.gridView1.Columns[12].Visible = false;
            this.gridView1.Columns[13].Visible = false;
            this.gridView1.Columns[14].Visible = false;
            this.gridView1.Columns[15].Caption = "Prix total Achat";
            this.gridView1.Columns[16].Caption = "Prix total Vente";
            this.gridView1.Columns[17].Visible=false;// = "Prix total Vente";
            gridView1.OptionsView.ShowAutoFilterRow = true;
        }
        private void FeurPiece(string code)
        {
            gridControl2.DataSource = null;
            gridView2.Columns.Clear();
            gridControl2.DataSource = fun.get_FeurByCode(code);
            this.gridView2.Columns[0].Caption = "Code";
            this.gridView2.Columns[1].Caption = "Raison sociale";
            this.gridView2.Columns[2].Caption = "Nom Responsable";
            this.gridView2.Columns[4].Caption = "Téléphone fixe";
            this.gridView2.Columns[3].Caption = "GSM";
            this.gridView2.Columns[5].Visible = false;
            this.gridView2.Columns[6].Visible = false;
            this.gridView2.Columns[7].Visible = false;
            this.gridView2.Columns[8].Visible = false;
            this.gridView2.Columns[9].Visible = false;
            this.gridView2.Columns[10].Visible = false;
            this.gridView2.Columns[11].Visible = false;
            this.gridView2.Columns[12].Visible = false;
            this.gridView2.Columns[13].Visible = false;
            gridView2.OptionsView.ShowAutoFilterRow = true;
            gridView2.OptionsView.EnableAppearanceEvenRow = true;
        }
        private void getPieceByCode(string code)
        {
            DataTable Piece = new DataTable();
            Piece = fun.get_piecebycode(code);
            feurInitale = "";
            if (Piece.Rows.Count != 0)
            {
                tcode.Text = Piece.Rows[0]["code_piece"].ToString();
                tdesign.Text = Piece.Rows[0]["libelle_piece"].ToString();
                tquantit.Text = Piece.Rows[0]["quantite_piece"].ToString();
                tquantitreel.Text = Piece.Rows[0]["quantite_reelle"].ToString();
                tstocalert.Text = Piece.Rows[0]["seuil_piece"].ToString();
             
                tpunitaire.Text = Piece.Rows[0]["pua"].ToString();
                tpuv.Text = Piece.Rows[0]["puv_rev"].ToString();
                tpvente.Text = Piece.Rows[0]["puv"].ToString();
                if (Piece.Rows[0]["unite_piece"] is DBNull) 
                { 
                    lookUnite.Properties.NullText = ""; 
                    UpdUnite(); 
                } 
                else {
                    lookUnite.EditValue = Piece.Rows[0]["unite_piece"].ToString();
                    UpdUnite();
                    uniteInitale = Piece.Rows[0]["unite_piece"].ToString(); 
                }
                if (Piece.Rows[0]["code_feur"] is DBNull) 
                { 
                    tfr.Properties.NullText = "";
                    UpdFeur(); 
                } 
                else
                {
                    tfr.EditValue = Piece.Rows[0]["code_feur"].ToString();
                    UpdFeur();
                    uniteInitale = Piece.Rows[0]["code_feur"].ToString(); 
                }
                if (Piece.Rows[0]["nature"] is DBNull)
                {
                    lookUpEdit2.Properties.NullText = "";
                    getFamille1();
                }
                else
                {
                    lookUpEdit2.EditValue = Piece.Rows[0]["nature"].ToString();
                    getFamille1();
                    familleinitiale = Piece.Rows[0]["nature"].ToString();
                }
               
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage2;
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                Tid_piece.Text = row[17].ToString();

                getPieceByCode(Convert.ToString(row[0]));
            }
        }
        private void getFamille1()
        {
            DataTable unite = new DataTable();
            unite = fun.get_famille();
            for (int i = 0; i < unite.Rows.Count; i++)
            {
                lookUpEdit2.Properties.ValueMember = "designation_fe";
                lookUpEdit2.Properties.DisplayMember = "designation_fe";
                lookUpEdit2.Properties.DataSource = unite;
                lookUpEdit2.Properties.PopulateColumns();
                lookUpEdit2.Properties.Columns["code_fe"].Visible = false;
                lookUpEdit2.Properties.Columns["designation_fe"].Caption = "Famille";
            }
        }
         private void getFamille3()
        {
            DataTable unite = new DataTable();
            unite = fun.get_famille();
            for (int i = 0; i < unite.Rows.Count; i++)
            {
                
                lookUpEdit3.Properties.ValueMember = "designation_fe";
                lookUpEdit3.Properties.DisplayMember = "designation_fe";
                lookUpEdit3.Properties.DataSource = unite;
                lookUpEdit3.Properties.PopulateColumns();
                lookUpEdit3.Properties.Columns["code_fe"].Visible = false;
                lookUpEdit3.Properties.Columns["designation_fe"].Caption = "Famille";
            }
             
        }
        private void getFamille()
        {
            DataTable unite = new DataTable();
            unite = fun.get_famille();
            for (int i = 0; i < unite.Rows.Count; i++)
            {
                lookUpEdit1.Properties.ValueMember = "designation_fe";
                lookUpEdit1.Properties.DisplayMember = "designation_fe";
                lookUpEdit1.Properties.DataSource = unite;
                lookUpEdit1.Properties.PopulateColumns();
                lookUpEdit1.Properties.Columns["code_fe"].Visible = false;
                lookUpEdit1.Properties.Columns["designation_fe"].Caption = "Famille";
            }
        }
        private void UpdFamille()
        {
            if (xtraTabControl1.SelectedTabPage == xtraTabPage2)
            {
                DataTable unite = new DataTable();
                unite = fun.get_famille();
                for (int i = 0; i < unite.Rows.Count; i++)
                {
                    lookUnite.Properties.ValueMember = "designation_fe";
                    lookUnite.Properties.DisplayMember = "designation_fe";
                    lookUnite.Properties.DataSource = unite;
                    lookUnite.Properties.PopulateColumns();
                    lookUnite.Properties.Columns["code_fe"].Visible = false;
                    lookUnite.Properties.Columns["designation_fe"].Caption = "Famille";
                }
            }
        }
        private void getUnite()
        {
            DataTable unite = new DataTable();
            unite = fun.get_unite();
            for (int i = 0; i < unite.Rows.Count; i++)
            {
                lookUpEdit6.Properties.ValueMember = "designation_unite";
                lookUpEdit6.Properties.DisplayMember = "designation_unite";
                lookUpEdit6.Properties.DataSource = unite;
                lookUpEdit6.Properties.PopulateColumns();
                lookUpEdit6.Properties.Columns["id_unite"].Visible = false;
                lookUpEdit6.Properties.Columns["designation_unite"].Caption = "Unité";
            }
        }
        private void UpdUnite()
        {
            if (xtraTabControl1.SelectedTabPage == xtraTabPage2)
            {
                DataTable unite = new DataTable();
                unite = fun.get_unite();
                for (int i = 0; i < unite.Rows.Count; i++)
                {
                    lookUnite.Properties.ValueMember = "designation_unite";
                    lookUnite.Properties.DisplayMember = "designation_unite";
                    lookUnite.Properties.DataSource = unite;
                    lookUnite.Properties.PopulateColumns();
                    lookUnite.Properties.Columns["id_unite"].Visible = false;
                    lookUnite.Properties.Columns["designation_unite"].Caption = "Unité";
                }
            }
        }
       
        private void getFeur()
        {
            DataTable Feur = new DataTable();
            Feur = fun.get_AllFeur();
            for (int i = 0; i < Feur.Rows.Count; i++)
            {
                lookUpEdit4.Properties.ValueMember = "code_feur";
                lookUpEdit4.Properties.DisplayMember = "responsbale";
                lookUpEdit4.Properties.DataSource = Feur;
                lookUpEdit4.Properties.PopulateColumns();
                lookUpEdit4.Properties.Columns["code_feur"].Caption = "Code Fournisseur";
                lookUpEdit4.Properties.Columns["raison_soc"].Caption = "Raison sociale";
                lookUpEdit4.Properties.Columns["responsbale"].Caption = "Résponsable";
                lookUpEdit4.Properties.Columns["gsm_feur"].Caption = "GSM";
                lookUpEdit4.Properties.Columns["tel_feur"].Caption = "Télephone";
                lookUpEdit4.Properties.Columns["fax_feur"].Visible = false;
                lookUpEdit4.Properties.Columns["adresse_feur"].Visible = false;
                lookUpEdit4.Properties.Columns["cp_feur"].Visible = false;
                lookUpEdit4.Properties.Columns["ville_feur"].Visible = false;
                lookUpEdit4.Properties.Columns["email_feur"].Visible = false;
                lookUpEdit4.Properties.Columns["site_feur"].Visible = false;
                lookUpEdit4.Properties.Columns["tva_feur"].Visible = false;
                lookUpEdit4.Properties.Columns["forme_juriduque"].Visible = false;
                lookUpEdit4.Properties.Columns["mode_pay"].Visible = false;
            }
        }
        private void UpdFeur()
        {
            DataTable Feur = new DataTable();
            Feur = fun.get_AllFeur();
            for (int i = 0; i < Feur.Rows.Count; i++)
            {
                tfr.Properties.ValueMember = "code_feur";
                tfr.Properties.DisplayMember = "responsbale";
                tfr.Properties.DataSource = Feur;
                tfr.Properties.PopulateColumns();
                tfr.Properties.Columns["code_feur"].Caption = "Code Fournisseur";
                tfr.Properties.Columns["raison_soc"].Caption = "Raison sociale";
                tfr.Properties.Columns["responsbale"].Caption = "Résponsable";
                tfr.Properties.Columns["gsm_feur"].Caption = "GSM";
                tfr.Properties.Columns["tel_feur"].Caption = "Télephone";
                tfr.Properties.Columns["fax_feur"].Visible = false;
                tfr.Properties.Columns["adresse_feur"].Visible = false;
                tfr.Properties.Columns["cp_feur"].Visible = false;
                tfr.Properties.Columns["ville_feur"].Visible = false;
                tfr.Properties.Columns["email_feur"].Visible = false;
                tfr.Properties.Columns["site_feur"].Visible = false;
                tfr.Properties.Columns["tva_feur"].Visible = false;
                tfr.Properties.Columns["forme_juriduque"].Visible = false;
                tfr.Properties.Columns["mode_pay"].Visible = false;
            }
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
             double qt, puv;
             
                 DataTable dt_prod = fun.get_stockBycode(textEdit28.Text);
                 if (dt_prod.Rows.Count > 0)
                 {
                     XtraMessageBox.Show("Il existe un produit avec ce code");
                 }
                 else
                 {
                     try
                     {
                         if (textEdit28.Text == "")
                         {
                             textEdit28.Text = "";
                         }
                         if (textEdit9.Text == "")
                         {
                             textEdit9.Text = "";
                         }
                         if (textEdit8.Text == null)
                         {
                             textEdit8.Text = "0";
                         }
                         if (textEdit8.Text == "")
                         {
                             textEdit8.Text = "0";
                         }
                         if (textEdit10.Text == null)
                         {
                             textEdit10.Text = "0";
                         }
                         if (textEdit10.Text == "")
                         {
                             textEdit10.Text = "0";
                         }
                         if (textEdit7.Text == null)
                         {
                             textEdit7.Text = "0";
                         }
                         if (textEdit7.Text == "")
                         {
                             textEdit7.Text = "0";
                         }
                         if (textEdit6.Text == "")
                         {
                             textEdit6.Text = "0";
                         }
                         if (textEdit27.Text == "")
                         {
                             textEdit27.Text = "0";
                         }
                         if (textEdit23.Text == "")
                         {
                             textEdit23.Text = "0";
                         }
                         if (lookUpEdit6.EditValue == null)
                         {
                             lookUpEdit6.EditValue = "";
                         }
                         if (lookUpEdit4.EditValue == null)
                         {
                             lookUpEdit4.EditValue = "";
                         }
                         if (lookUpEdit1.EditValue == null)
                         {
                             lookUpEdit1.EditValue = "";
                         }

                         DateTime ddd;
                         ddd = DateTime.Now;
                         string comm = "ajout piéce au stock";
                         string nature = "nouveau produit";
                         punitaire = 0;
                         double.TryParse(textEdit6.Text.Replace('.', ','), out punitaire);
                         //punitaire = Convert.ToDouble(textEdit6.Text.Replace('.', ','));
                         DataTable stockval = new DataTable();

                         try
                         {
                             stockval = fun.get_piece_byCode(textEdit28.Text);
                         }
                         catch (Exception exception)
                         { }
                         if (stockval.Rows.Count == 0)
                         {


                             fun.set_stock(textEdit28.Text, textEdit9.Text, lookUpEdit6.EditValue.ToString(), textEdit8.Text, textEdit10.Text, textEdit7.Text, lookUpEdit1.EditValue.ToString(),
                               punitaire.ToString(), textEdit23.Text, lookUpEdit4.EditValue.ToString(), textEdit27.Text);
                             fun.alimenter(DateTime.Now, Convert.ToDouble(textEdit8.Text.Replace('.', ',')), comm, DateTime.Now.ToShortTimeString(), textEdit9.Text, nature);

                             labelControl36.Visible = true;
                             textEdit28.Text = "";
                             textEdit9.Text = "";
                             lookUpEdit6.EditValue = null;
                             textEdit8.Text = "";
                             textEdit10.Text = "";
                             textEdit7.Text = "";
                             lookUpEdit1.EditValue = null;
                             textEdit6.Text = "";
                             textEdit23.Text = "";

                             lookUpEdit4.EditValue = null;
                             textEdit27.Text = "";


                             timer1.Start();
                             ListeStock();
                             //}
                         }
                         else
                         {
                             XtraMessageBox.Show("Vous avez déja ce produit alimenter le si vous voulez");
                             textEdit28.Text = "";
                             textEdit9.Text = "";
                             lookUpEdit6.EditValue = null;
                             textEdit8.Text = "";
                             textEdit10.Text = "";
                             textEdit7.Text = "";
                             lookUpEdit1.EditValue = null;
                             textEdit6.Text = "";
                             textEdit23.Text = "";
                             lookUpEdit4.EditValue = null;
                             textEdit27.Text = "";
                         }
                     }


                     catch (Exception exception)
                     {
                         MessageBox.Show("vérifier les valeurs entrées");

                     }
                 }
             
        }
        
        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {

            if (xtraTabControl1.SelectedTabPage == xtraTabPage5)
            {
           
                getUnite();
                getFamille();
                getFeur();
            }

            if (xtraTabControl1.SelectedTabPage == xtraTabPage1)
            {
                ListeStock();
            }
            if (xtraTabControl1.SelectedTabPage == xtraTabPage2)
            {
                int count = gridView1.DataRowCount;
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                    getPieceByCode(Convert.ToString(row[0]));
                    Tid_piece.Text = row[17].ToString();
                 
                }
            }
           

        }
        private void getFeurByCode(string code)
        {
            DataTable feur = new DataTable();
            feur = fun.get_FeurByCode(code);
            if (feur.Rows.Count != 0)
            {
                textEdit21.Text = feur.Rows[0]["code_feur"].ToString();
                textEdit20.Text = feur.Rows[0]["raison_soc"].ToString();
                textEdit19.Text = feur.Rows[0]["responsbale"].ToString();
                textEdit18.Text = feur.Rows[0]["gsm_feur"].ToString();
                textEdit17.Text = feur.Rows[0]["tel_feur"].ToString();
                textEdit16.Text = feur.Rows[0]["fax_feur"].ToString();
                textEdit15.Text = feur.Rows[0]["adresse_feur"].ToString();
                textEdit14.Text = feur.Rows[0]["cp_feur"].ToString();
                comboBoxEdit1.SelectedItem = feur.Rows[0]["ville_feur"].ToString();
                textEdit13.Text = feur.Rows[0]["email_feur"].ToString();
                textEdit12.Text = feur.Rows[0]["site_feur"].ToString();
                comboBoxEdit2.SelectedItem = feur.Rows[0]["tva_feur"].ToString();
                textEdit22.Text = feur.Rows[0]["forme_juriduque"].ToString();
                textEdit11.Text = feur.Rows[0]["mode_pay"].ToString();
            }
        }

        private void xtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl2.SelectedTabPage == xtraTabPage4)
            {
                int count = gridView2.DataRowCount;
                if (count != 0 && gridView2.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    System.Data.DataRow row = gridView2.GetDataRow(gridView2.FocusedRowHandle);
                    getFeurByCode(Convert.ToString(row[0]));
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage5;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                fun.delete_pieceFromStock(Convert.ToString(row[0]));
                labelControl35.Visible = true;
                timer1.Start();
                ListeStock();
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (tcode.Text == "")
            {
                tcode.Text = "";
                
            }
            
            if (tdesign.Text == "")
            {
                tdesign.Text = "";
            }
           
            if (tquantit.Text == null)
            {
                tquantit.Text = "0";
            }
            if (tstocalert.Text == "")
            {
                tstocalert.Text = "0";
            }
            if (tquantitreel.Text == "")
            {
                tquantitreel.Text = "0";
            }
            if (tpuv.Text == "")
            {
                tpuv.Text = "0";
            }
            if (tfr.EditValue == null)
            {
                tfr.EditValue = "";
            }
            if (lookUnite.EditValue == null)
            {
                lookUnite.EditValue = "";
            }
            if (tpvente.Text == "")
            {
                tpvente.Text = "0";
            }
            if (tpunitaire.Text == "")
            {
                tpunitaire.Text = "0";
            }
           
                string newfeur;
                if (nomFeurInit == tfr.Text) { newfeur = feurInitale; } else { newfeur = tfr.EditValue.ToString(); }
                if (uniteInitale == lookUnite.Text) { Newunite = uniteInitale; } else { Newunite = lookUnite.EditValue.ToString(); }
                if (uniteInitale == lookUnite.Text) { Newunite = uniteInitale; } else { Newunite = lookUnite.EditValue.ToString(); }
                if (familleinitiale == lookUpEdit2.Text) { newfamille = familleinitiale; } else { newfamille = lookUpEdit2.EditValue.ToString(); }
                fun.update_Stock2(tcode.Text, tdesign.Text, Newunite, tquantit.Text, tquantitreel.Text, tstocalert.Text, newfamille, tpunitaire.Text, tpvente.Text, newfeur, tpuv.Text,int.Parse(Tid_piece.Text));
                labelControl34.Visible = true;
                timer1.Start();
                ListeStock();
            
        }

     
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage1;
        }

        //private void lookUpEdit3_EditValueChanged(object sender, EventArgs e)
        //{
        //    fr = tfr.EditValue.ToString();
        //}


        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                test = "y";
                lib = row[1].ToString();
                pua = Convert.ToDouble(row[7].ToString().Replace('.', ','));

                int_piece = Convert.ToString(row[0]);
                quantite_piece qq = new quantite_piece();
                qq.ShowDialog();
            }
        }

        private void gestionStock_Activated(object sender, EventArgs e)
        {
            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridView1.BestFitColumns();
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.EnableAppearanceEvenRow = true;


            gridView2.BestFitColumns();
            gridView2.OptionsBehavior.Editable = false;
            gridView2.OptionsView.EnableAppearanceEvenRow = true;

            if (login1.depart == "Utilisateur")
            {
                //ajout droit
                if (login1.ajou_stock == "OUI") { simpleButton1.Enabled = true; xtraTabPage5.PageEnabled = true; }
                else { simpleButton1.Enabled = false; xtraTabPage5.PageEnabled = false; }
                //modifier droit
                if (login1.modif_stock == "OUI") { simpleButton2.Enabled = true; xtraTabPage2.PageEnabled = true; }
                else { simpleButton2.Enabled = false; xtraTabPage2.PageEnabled = false; }
                //supprimer droit
                if (login1.supp_stock == "OUI") { simpleButton3.Enabled = true; }
                else { simpleButton3.Enabled = false; }
              
               
                //gerer magasins 
           
                //passer en commande
                if (login1.passer_cde == "OUI") { barButtonItem1.Enabled = true; }
                else { barButtonItem1.Enabled = false; }
                //alimenter
            
                //sortie_stock
              
            }
            if (login1.depart == "Administrateur")
            {
                simpleButton1.Enabled = true; xtraTabPage5.PageEnabled = true;
                simpleButton2.Enabled = true; xtraTabPage2.PageEnabled = true;
             
                barButtonItem1.Enabled = true; barButtonItem2.Enabled = true; barButtonItem4.Enabled = true;
             
            }
            ListeStock();
            Form1.wait = 1;
            Form1.load = 1;
        }

        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {


            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {

                Point pt = this.Location;
                pt.Offset(this.Left + e.X, this.Top + e.Y);
                popupMenu1.ShowPopup(this.barManager1, Control.MousePosition);
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                test = "alimenter";
                int_piece = Convert.ToString(row[0]);
                lib = row[1].ToString();
                alimenter al = new alimenter();
                al.ShowDialog();
            }

        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListeStock();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            label1.Text += "a";
            if (label1.Text.ToString() == "10aaa")
            {
                labelControl34.Visible = false;
                labelControl35.Visible = false;
                labelControl36.Visible = false;
                label1.Text = "10";
                timer1.Stop();
            }
        }

        private void gestionStock_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            form_unite.ShowDialog();
            form_unite.BringToFront();
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            form_magasin.ShowDialog();
            form_magasin.BringToFront();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                test = "delimenter";
                int_piece = Convert.ToString(row[0]);
                lib = row[1].ToString();
                qte = Convert.ToDouble(row[3].ToString().Replace('.', ','));

                delimenter dl = new delimenter();
                dl.ShowDialog();
            }

        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);

                int id_piece;
                id_piece = Convert.ToInt32(row[0].ToString());

                DataTable bb = new DataTable();
                bb = fun.affiche_infos_feur(id_piece);

            }
        }
       
     
      
     

        private void barManager2_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaisirQte qtite = new SaisirQte();
            qtite.Show();
        }

        private void simpleButton23_Click(object sender, EventArgs e)
        {
            getUnite();
            getFamille();

            getFeur();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Amortissement amor = new Amortissement();
            amor.Show();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
                        
            
            gridControl1.ShowPrintPreview();
         
        }

        private void simpleButton9_Click_1(object sender, EventArgs e)
        {

            form_famille.ShowDialog();
            form_famille.BringToFront();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);

                string id_fr;
                id_fr = row[10].ToString();
                FeurPiece(id_fr);



            }
        }

        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton15_Click(object sender, EventArgs e)
        {
            ListeStock();
        }

        private void lookUpEdit3_TextChanged(object sender, EventArgs e)
        {
            ListeStock();
        }

       

            
        }

    
}