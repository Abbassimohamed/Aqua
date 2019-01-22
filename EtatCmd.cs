using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using RibbonSimplePad.Report;


namespace RibbonSimplePad
{
    public partial class EtatCmd : DevExpress.XtraEditors.XtraForm
    {
        int id_cmd ;
        public EtatCmd(int idcmd)
        {
            InitializeComponent();
            id_cmd = idcmd;
        }
       
        sql_gmao fun = new sql_gmao();
        private void XtraForm1_Load(object sender, EventArgs e)
        {
            ReportCmd Xtra = new ReportCmd(id_cmd);
           // XtraReport3 Xtra = new XtraReport3();
            gmao_bdDataSet dat = new gmao_bdDataSet();
            dat.EndInit();
            DataTable dt_cmd_fr = fun.affiche_cmdFR_infos(id_cmd);
            dat.Clear();
            
            if (dt_cmd_fr.Rows.Count > 0)
            {
                //info commande fr

                gmao_bdDataSet.CommandefrRow dr_cdm_fr = dat.Commandefr.NewCommandefrRow();
                dr_cdm_fr.id_commande =int.Parse( dt_cmd_fr.Rows[0]["id_commande"].ToString());
                dr_cdm_fr.date = dt_cmd_fr.Rows[0]["date"].ToString().Substring(0,10);
                dr_cdm_fr.etatcmd = dt_cmd_fr.Rows[0]["etatcmd"].ToString();
                dr_cdm_fr.etatfacture = dt_cmd_fr.Rows[0]["etatfacture"].ToString();

                dr_cdm_fr.etatbl = dt_cmd_fr.Rows[0]["etatbl"].ToString();
                dr_cdm_fr.etatbnsortie = dt_cmd_fr.Rows[0]["etatbnsortie"].ToString();
                dr_cdm_fr.id_clt = dt_cmd_fr.Rows[0]["id_clt"].ToString();
                dr_cdm_fr.montant_ht = dt_cmd_fr.Rows[0]["montant_ht"].ToString();
                dr_cdm_fr.montant_ttc = dt_cmd_fr.Rows[0]["montant_ttc"].ToString();
                dr_cdm_fr.timbre = dt_cmd_fr.Rows[0]["timbre"].ToString();
                dr_cdm_fr.remise = dt_cmd_fr.Rows[0]["remise"].ToString();
                dr_cdm_fr.tva = dt_cmd_fr.Rows[0]["tva"].ToString();
                
                
                try
                {
                    dat.Commandefr.Rows.Add(dr_cdm_fr);
                    //dat.Commandefr.AcceptChanges();
                    
                }
                catch (Exception ex) { }
                

                // info fournisseur
                gmao_bdDataSet.fournisseurRow rw_for = dat.fournisseur.NewfournisseurRow();
                DataTable dt_fr = fun.get_FeurByCode(dt_cmd_fr.Rows[0]["id_clt"].ToString());

                rw_for.code_feur ="Code fournisseur / supplier code :"+ dt_fr.Rows[0]["code_feur"].ToString();
                rw_for.raison_soc ="Fournisseur / supplier:"+ dt_fr.Rows[0]["raison_soc"].ToString();
                rw_for.responsbale = dt_fr.Rows[0]["responsbale"].ToString();
                rw_for.gsm_feur = dt_fr.Rows[0]["gsm_feur"].ToString();
                rw_for.tel_feur ="Tél. / phone:"+ dt_fr.Rows[0]["tel_feur"].ToString();
                rw_for.fax_feur ="Fax / fax:"+ dt_fr.Rows[0]["fax_feur"].ToString();
                rw_for.adresse_feur ="Adresse / adress:"+ dt_fr.Rows[0]["adresse_feur"].ToString();
                rw_for.cp_feur ="Code postal / zip code :"+dt_fr.Rows[0]["cp_feur"].ToString();
                rw_for.ville_feur ="Ville / city :"+ dt_fr.Rows[0]["ville_feur"].ToString();
                rw_for.email_feur = dt_fr.Rows[0]["email_feur"].ToString();
                rw_for.site_feur = dt_fr.Rows[0]["site_feur"].ToString();
                rw_for.tva_feur = dt_fr.Rows[0]["tva_feur"].ToString();
                rw_for.forme_juriduque ="MF :"+ dt_fr.Rows[0]["forme_juriduque"].ToString();
                rw_for.mode_pay = dt_fr.Rows[0]["mode_pay"].ToString();
                try
                {
                    dat.fournisseur.Rows.Add(rw_for);
                }
                catch (Exception er) { }

                //info piece commande fr
                var piec_cmd_fr =new  gmao_bdDataSet.piece_commandefrDataTable();
               
                DataTable dt_piece=fun.get_list_piece_from_cdefr(id_cmd);
                int i=0;
                foreach (DataRow item in dt_piece.Rows)
                {
                    gmao_bdDataSet.piece_commandefrRow dr_piec_cmd_fr = dat.piece_commandefr.Newpiece_commandefrRow();
                    dr_piec_cmd_fr.id_piece =int.Parse( item["id_piece"].ToString());

                    dr_piec_cmd_fr.code_art = item["code_art"].ToString();
                    dr_piec_cmd_fr.libelle_piece = item["libelle_piece"].ToString();
                    dr_piec_cmd_fr.quantite_piece = item["quantite_piece"].ToString();
                    dr_piec_cmd_fr.id_clt = item["id_clt"].ToString();
                    dr_piec_cmd_fr.etat = item["etat"].ToString();
                    dr_piec_cmd_fr.puv = item["puv"].ToString();
                    dr_piec_cmd_fr.totvente = item["totvente"].ToString();
                    dr_piec_cmd_fr.id_commande =item["id_commande"].ToString();
                    dr_piec_cmd_fr.remise  = item["remise"].ToString();
                    dr_piec_cmd_fr.ttva = item["ttva"].ToString();
                    dr_piec_cmd_fr.unit = item["unit"].ToString();
                    dr_piec_cmd_fr.qterest = item["qterest"].ToString();

                    dat.piece_commandefr.Rows.Add(dr_piec_cmd_fr); 
                    //dr_piec_cmd_fr.AcceptChanges();
                    //dat.piece_commandefr.Rows.InsertAt(dr_piec_cmd_fr, i); i++;
                    
                    //dat.piece_commandefr.DataSet.AcceptChanges();
                    
                   
                    
                   
                    
                }
               
                //if (dt_piece.Rows.Count < 10)
                //{
                //    int nb = 10 - dt_piece.Rows.Count;
                //    for (int i = 0; i < nb; i++)
                //    {
                //        gmao_bdDataSet.piece_commandefrRow dr_piec_cmd_fr = dat.piece_commandefr.Newpiece_commandefrRow();
                //        dr_piec_cmd_fr.id_piece = i;

                //        dr_piec_cmd_fr.code_art = "  ";
                //        dr_piec_cmd_fr.libelle_piece = "  ";
                //        dr_piec_cmd_fr.quantite_piece = "  ";
                //        dr_piec_cmd_fr.id_clt = "  ";
                //        dr_piec_cmd_fr.etat = "  ";
                //        dr_piec_cmd_fr.puv = "  ";
                //        dr_piec_cmd_fr.totvente = "  ";
                //        dr_piec_cmd_fr.id_commande = "  ";
                //        dr_piec_cmd_fr.remise = "  ";
                //        dr_piec_cmd_fr.ttva = "  " ;
                //        dr_piec_cmd_fr.unit = "  ";
                //        dr_piec_cmd_fr.qterest = "  ";

                //        dat.piece_commandefr.Rows.Add(dr_piec_cmd_fr);
                //    }
                //}
            }
            dat.AcceptChanges();
            dat.EndInit();
            dat.AcceptChanges();
            
            Xtra.DataSource = dat;
           // Xtra.DataMember = "piece_commandefr";
            //DevisReport dev_rep = new DevisReport();
            //FacturePfReport dev_rep = new FacturePfReport();
           // FactureReport dev_rep = new FactureReport();
            ReportCmd dev_rep = new ReportCmd(id_cmd);
            documentViewer1.DocumentSource = dev_rep;
        }

        private void printPreviewBarItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void printPreviewBarItem24_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        
    }
}