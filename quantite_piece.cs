using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
namespace RibbonSimplePad
{
    public partial class quantite_piece : DevExpress.XtraEditors.XtraForm
    {
        public quantite_piece()
        {
            InitializeComponent();
        }
        sql_gmao fun = new sql_gmao();
        public static int quantite, id, req_id_cde, max_cde;
        public static string req_feur;
        private void quantite_piece_Load(object sender, EventArgs e)
        {

            
        }
       

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text == "" || textEdit1.Text == "0")
            {
                XtraMessageBox.Show("Veuillez saisir une quantité!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
          
            
            if (textEdit1.Text == "")
            { dxErrorProvider1.Dispose(); dxErrorProvider1.SetError(textEdit1, "Aucune quantité saisie"); }
            else
            {
                dxErrorProvider1.Dispose();
                quantite = Convert.ToInt32(textEdit1.Text);
              
               
                // passation de commande à partir d'interface de stock
                if (gestionStock.test == "y")
                {
                    DataTable dt = new DataTable();
                    dt = fun.test_dispo_piece(gestionStock.lib);
                    if (dt.Rows.Count != 0)
                    {
                        //recuperer le code fournisseur de piece
                        req_feur = dt.Rows[0]["code_feur"].ToString();
                    }
                    string etat = "Mise en attente";
                    DataTable dd = new DataTable();
                    //voir si il y a de commande en attente avec le mm fournisseur
                    dd = fun.select_from_cde(etat, req_feur);
                    if (dd.Rows.Count != 0)
                    {
                        string id_cde = dd.Rows[0][id].ToString();
                        string c = gestionStock.lib;
                        DataTable gg = new DataTable();
                        string f = "ajoute";
                        string etat_cde = "Mise en attente";
                        DataTable jj = new DataTable();
                        //selectionner l'id de commande en cours pour le mm fournisseur
                        jj = fun.select_max_id_cde(req_feur, etat_cde);
                        if (jj.Rows.Count != 0)
                        { max_cde = Convert.ToInt32(jj.Rows[0][0]); }
                        //accéder pour recuperer les infos de piece 
                        gg = fun.select_etoile_from_piece(req_feur, c, f, max_cde);
                        if (gg.Rows.Count != 0)
                        {
                            // si piece existante dans la commande additionner la quantité
                            DialogResult dialogResult = XtraMessageBox.Show("La piece est deja ajoutée au bon de commande! 'Oui' pour addistionner la quantité 'Non' pour annuler   ", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                            if (dialogResult == DialogResult.Yes)
                            {
                                fun.update_qte_piece2(Convert.ToInt32(textEdit1.Text), req_feur, gestionStock.lib);
                                int pv = Convert.ToInt32(gestionStock.pua) * Convert.ToInt32(textEdit1.Text);
                                fun.update_puv_piece2(pv, req_feur, gestionStock.lib);
                            }

                               

                            else if (dialogResult == DialogResult.No)
                            {

                            }
                        }
                        else
                        {
                            string d = "ajoute";
                            string z = "Mise en attente";
                            DataTable tt = new DataTable();
                            tt = fun.get_id_cde(z, req_feur);
                            if (tt.Rows.Count != 0)
                            { req_id_cde = Convert.ToInt32(tt.Rows[0]["id"]); }
                            // ajouter une piece et mettre à jour l'etat de piece dans la commande
                            fun.update_piece_etat_FromStock(gestionStock.int_piece, d, req_id_cde);

                            fun.insert_pieceFromStock(gestionStock.lib, Convert.ToInt32(textEdit1.Text), req_feur, d, req_id_cde, gestionStock.int_piece, Convert.ToInt32(gestionStock.pua), Convert.ToInt32(gestionStock.pua) * Convert.ToInt32(textEdit1.Text));
                        }

                    }
                    else
                    {
                        DataTable aa = new DataTable();
                        //recuperer le nom de fournisseur
                        aa = fun.get_FeurByCode(req_feur);
                        string d = aa.Rows[0]["raison_soc"].ToString();
                        string w = "ajoute";
                        //ajouter une commande
                        fun.insert_into_commande1(req_feur, etat, d);
                        string z = "Mise en attente";
                        DataTable tt = new DataTable();
                        tt = fun.get_id_cde(z, req_feur);
                        if (tt.Rows.Count != 0)
                        { req_id_cde = Convert.ToInt32(tt.Rows[0]["id"]); }
                        fun.update_piece_etatFromStock(gestionStock.int_piece, w, req_id_cde);
                        fun.insert_pieceFromStock(gestionStock.lib, Convert.ToInt32(textEdit1.Text), req_feur, w, req_id_cde, gestionStock.int_piece, Convert.ToInt32(gestionStock.pua), Convert.ToInt32(gestionStock.pua) * Convert.ToInt32(textEdit1.Text));
                    }

                }

                if (bon_achat.etat == 1)
                {
                    fun.update_piece_etat2(bon_achat.req_id, Convert.ToInt32(textEdit1.Text));
                }

            }



            if (gestionStock.test == "alimenter")
            {
                fun.update_stock_after_aliment(Convert.ToInt32(textEdit1.Text), gestionStock.int_piece);
                XtraMessageBox.Show("Piéces ajoutées en stock, Actualiser", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


          
            bon_achat.etat = 0;
            gestionStock.test = "";
            this.Close();






























        }
        private void quantite_piece_Activated(object sender, EventArgs e)
        {
            textEdit1.Focus();
        }
    }
}