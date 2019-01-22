using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace RibbonSimplePad
{
    class sql_gmao
    {
        public static string test_con, etat_euip;
        public SqlDataReader dr;
        public SqlDataAdapter da;
        public static string strconnection = "Data Source= .\\SQLEXPRESS;Initial Catalog=db_aqua;Integrated Security=True";

        // public static string strconnection = "Data Source= .\\SQLEXPRESS;Initial Catalog=test;Integrated Security=True";
       
        public static SqlConnection conn = new SqlConnection(strconnection);
    
        public string return_ch()
        {
            return strconnection;
        }
        public string strcon()
        {
            return strconnection;
        }
        public void connexion()
        {
            conn.Open();
            test_con = "true";

        }
        //fermeture de la connexion à la base des donnees
        public void closecon()
        {
            conn.Close();
            test_con = "false";
        }
        public void function_test()
        {
            //if (test_connexion() == false)
            //{
            //    try
            //    {
            //        closecon();
            //    }
            //    catch { }
            //    connexion();
            //}
            //else
            //{ }
            if (conn.State.ToString().Equals("Open"))
            {
                conn.Close();
            }
            if (conn.State.ToString().Equals("Closed"))
            {

                conn.Open();
            }


        }
        public string update_etat_devis(int idDevi, string etat, string des)
        {
            function_test();
            string req = "UPDATE devis SET etat = @etat WHERE id =@id";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@etat", (object)etat));
            cmd.Parameters.Add(new SqlParameter("@id", (object)idDevi));
            cmd.ExecuteNonQuery();
            suivi_actions(des);
            return "";
        }
        public string update_etat_devis(string etat, int code, string date_env, string ht, string ttc, string tva, string remise, string timbre, string dev)
        {
            function_test();
            string req = "UPDATE devis SET etat ='" + etat + "', date_envoie='" + date_env + "', montant_ht ='" + ht + "', montant_ttc='" + ttc + "',tva ='" + tva + "', remise='" + remise + "', timbre='" + timbre + "',  dev='" + dev + "' WHERE id ='" + code + "'";
            suivi_actions("Devis n°: ( " + code + " )  marqué comme envoyé.");
            return DataExcute(req);
        }
        public void UpdatetKey(string keys, DateTime last_date)
        {
             function_test();
             string req = "UPDATE  keyTab SET keys=@keys, last_date=@last_date where id='1'";
             SqlCommand cmd = new SqlCommand(req, conn);
             cmd.Parameters.Add(new SqlParameter("@keys", (object)keys));
             cmd.Parameters.Add(new SqlParameter("@last_date", (object)last_date));
             cmd.ExecuteNonQuery();
        }
        public void Update_last_day_Key( DateTime last_date)
        {
            function_test();
            string req = "UPDATE  keyTab SET  last_date=@last_date where id='1'";
            SqlCommand cmd = new SqlCommand(req, conn);

            cmd.Parameters.Add(new SqlParameter("@last_date", (object)last_date));
            cmd.ExecuteNonQuery();
        }
        public DataTable getKey()
        {
            function_test();
            string rq_select = "SELECT *  FROM keyTab";
            SqlCommand cmd = new SqlCommand(rq_select, conn);
            
            DataTable dt = new DataTable();
            try
            {
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception hh)
            {
            }
            return dt;
        }
        public string insert_piecee_devis2(string lib, string codep, string qp, int idf, string puv, string pv,string remise,string tva,string unite)
        {
            function_test();
            string req = "INSERT INTO piece_devis (libelle_piece_u, code_piece_u, quantite_piece_u, id_fact, puv, pv,remise,ttva,unite) VALUES"+
                " (@libelle_piece_u,@code_piece_u,@quantite_piece_u,@id_fact,@puv,@pv,@remise,@ttva,@unite)";
            
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@libelle_piece_u", (object)lib));
            cmd.Parameters.Add(new SqlParameter("@code_piece_u", (object)codep));
            cmd.Parameters.Add(new SqlParameter("@quantite_piece_u", (object)qp));
            cmd.Parameters.Add(new SqlParameter("@id_fact", (object)idf));
            cmd.Parameters.Add(new SqlParameter("@puv", (object)puv));
            cmd.Parameters.Add(new SqlParameter("@pv", (object)pv));
            cmd.Parameters.Add(new SqlParameter("@remise", (object)remise));
            cmd.Parameters.Add(new SqlParameter("@ttva", (object)tva));
            cmd.Parameters.Add(new SqlParameter("@unite", (object)unite));
            cmd.ExecuteNonQuery();
            suivi_actions("Ajout piéce : ( " + lib + " ) avec la quantité : ( " + qp + " ) au devis n° ( " + idf + " ).");
            return "";
        }
        public string insertdb__into_devis(string date_ajout, string etat, string date_envoie, string dev, string tva,
           string timbre, string remise, string client, string montant_ttc, string montant_ht, string id_clt, string comm_fact,string mode_livraison)
        {
            function_test();
            string req = "INSERT INTO devis (date_ajout, etat, date_envoie,id_clt, client,comm_fact,dev,remise,timbre,montant_ttc,tva,montant_ht,mode_livraison)" +
            " VALUES (@date_ajout,@etat,@date_envoie,@id_clt,@client,@comm_fact,@dev,@remise,@timbre,@montant_ttc,@tva,@montant_ht,@mode_livraison)";
            
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@date_ajout", (object)System.DateTime.Now.ToString()));
            cmd.Parameters.Add(new SqlParameter("@etat", (object)etat));
            cmd.Parameters.Add(new SqlParameter("@date_envoie", (object)date_envoie));
            cmd.Parameters.Add(new SqlParameter("@id_clt", (object)id_clt));
            cmd.Parameters.Add(new SqlParameter("@client", (object)client));
            cmd.Parameters.Add(new SqlParameter("@comm_fact", (object)comm_fact));
            cmd.Parameters.Add(new SqlParameter("@dev", (object)dev));
            cmd.Parameters.Add(new SqlParameter("@remise", (object)remise));
            cmd.Parameters.Add(new SqlParameter("@timbre", (object)timbre));
            cmd.Parameters.Add(new SqlParameter("@montant_ttc", (object)montant_ttc));
            cmd.Parameters.Add(new SqlParameter("@tva", (object)tva));
            cmd.Parameters.Add(new SqlParameter("@montant_ht", (object)montant_ht));
            cmd.Parameters.Add(new SqlParameter("@mode_livraison", (object)mode_livraison));
            cmd.ExecuteNonQuery();
                suivi_actions("Création d'un devis.");

                return "";

        }
        public string updatefacturevente(int code, string etat, string ht, string ttc, string tva, string remise, string timbre, string fodec, string retres)
        {
            function_test();
            string req = "UPDATE facturevente SET etat ='" + etat + "', date_ajout='" + System.DateTime.Now.ToString() + "', montant_ht ='" + ht + "', montant_ttc='" + ttc + "',tva ='" + tva + "', remise='" + remise + "' , timbre='" + timbre + "', faudec ='" + fodec + "',retenusource ='" + retres + "'  WHERE id ='" + code + "'";
            suivi_actions("Facture n°: ( " + code + " )  marquée comme envoyée.");
            return DataExcute(req);
        }
        public Boolean test_connexion()
        {
            if (test_con == "true")
                return (true);
            else
                return (false);

        }
        public DataTable DataReturn(string rq)
        {
            DataSet ds = new DataSet();
            DataTable data = new DataTable();
            try
            {
                
                ds.Reset();
                data.Reset();
                da = new SqlDataAdapter(rq, conn);
                da.Fill(ds, "table");
                data = ds.Tables["table"];
            }
            catch (Exception er)
            {
            }
            return data;
        }
        public string DataExcute(string rq)
        {
            try
            {
                string req = rq;
                SqlCommand cmd = new SqlCommand(req, conn);
                cmd.ExecuteNonQuery();
                return "true";
            }
            catch (Exception ss)
            {
                return (ss.Message);
            }
        }
        public DataTable GetBlByNum(int num)
        {

            function_test();
            req_select = "SELECT *  FROM bon_livraison where numero_bl=@numero_bl";

            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@numero_bl", (object)num));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public DataTable GetProdByQtRest(string code, double qt)
        {
            function_test();
            req_select = "SELECT quantite_piece  FROM stock where cast(quantite_piece as real)<@qt and code_piece=@code";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@qt", (object)qt));
            cmd.Parameters.Add(new SqlParameter("@code", (object)code));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public DataTable max_num_bl2()
        {

            function_test();
            req_select = "SELECT MAX(cast(numero_bl as int)) as max FROM bon_livraison ";

            return DataReturn(req_select);
        }
        public DataTable max_num_Factvent()
        {

            function_test();
            req_select = "SELECT MAX(cast(numero_fact as int)) as max FROM facturevente ";

            return DataReturn(req_select);
        }
        public DataTable GetFactByNum(int numero)
        {

            function_test();
            req_select = "SELECT * FROM facturevente where numero_fact=@numero_fact";

            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@numero_fact", (object)numero));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public DataTable GetListNumFact()
        {

            function_test();
            req_select = "SELECT numero_fact FROM facturevente";

            SqlCommand cmd = new SqlCommand(req_select, conn);
           
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public DataTable Listbl()
        {

            function_test();
            req_select = "SELECT * FROM bon_livraison";

           SqlCommand cmd = new SqlCommand(req_select, conn);
       
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public DataTable ListFact()
        {

            function_test();
            req_select = "SELECT * FROM facturevente";

            SqlCommand cmd = new SqlCommand(req_select, conn);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        //*********************************** INSERT ***********************************
        //Suivi des actions effectuée par chaque utilisateur (log)
        public string setRelArt_SousArt(string a, string b, Double c, Double d)
        {
            function_test();

            string req = "INSERT INTO Art_SousArt (codeArt, codeSousArt, quantite,punitaire) VALUES ('" + a + "','" + b + "'," + c + "," + d + ")";
            return DataExcute(req);
        }
        public DataTable get_max_Factvente()
        {
            function_test();
            req_select = "SELECT MAX(id) FROM facturevente ";

            return DataReturn(req_select);
        }
        public string delete_piece_fromfactvente(int code)
        {
            function_test();
            string req = "DELETE FROM piece_fact WHERE id_fact='" + code + "'";

            return DataExcute(req);
        }
        public string update_compte_cl(string code, Double montant)
        {
            function_test();
            string req = "UPDATE compte_cl SET solde = solde +'" + montant + "',debit = debit +'" + montant + "'  WHERE code_cl ='" + code + "'";
            suivi_actions("Validation de la compte client : (" + code + ")");
            return DataExcute(req);
        }
        public DataTable selectfromfacturevente()
        {
            function_test();
            req_select = "SELECT * FROM facturevente ";
            SqlCommand cmd = new SqlCommand(req_select, conn);
           
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public string deletefacturevente(int code)
        {
            function_test();
            string req = "DELETE FROM facturevente WHERE id='" + code + "'";
            suivi_actions("Suppression de la facture client  n° " + code + ".");
            return DataExcute(req);
        }
        public string insert_piecee_fact(string l, string z, Double q, string c, string d, int e, string puv, string pv, string idcmd, string remise, string tva,string unite)
        {
            function_test();
            string req = "INSERT INTO piece_fact (libelle_piece_u, code_piece_u, quantite_piece_u, id_clt, etat, id_fact, puv, pv,id_commande,remise,tva,etat2) VALUES"+
" (@libelle_piece_u,@code_piece_u,@quantite_piece_u,@id_clt,@etat,@id_fact,@puv,@pv,@id_commande,@remise,@tva,@etat2)";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@libelle_piece_u", (object)l));
            cmd.Parameters.Add(new SqlParameter("@code_piece_u", (object)z));
            cmd.Parameters.Add(new SqlParameter("@quantite_piece_u", (object)q));
            cmd.Parameters.Add(new SqlParameter("@id_clt", (object)c));
            cmd.Parameters.Add(new SqlParameter("@etat", (object)d));
            cmd.Parameters.Add(new SqlParameter("@id_fact", (object)e));
            cmd.Parameters.Add(new SqlParameter("@puv", (object)puv));
            cmd.Parameters.Add(new SqlParameter("@pv", (object)pv));
            cmd.Parameters.Add(new SqlParameter("@id_commande", (object)idcmd));
            cmd.Parameters.Add(new SqlParameter("@remise", (object)remise));
            cmd.Parameters.Add(new SqlParameter("@tva", (object)tva));
            cmd.Parameters.Add(new SqlParameter("@etat2", (object)unite));
            cmd.ExecuteNonQuery();
            suivi_actions("Ajout piéce : ( " + l + " ) avec la quantité : ( " + q + " ) au commande client n° ( " + e + " ).");
            return "";
        }
        public void updatefacturevente(string id_clt, string date, string etat, string client, string nbcommande, string montantttc, string remise, string montanthtc, string timbre, string tva, string fodec, string retres, string numero, string L_num_bl, int id)
        {
            function_test();
            string req = "update  facturevente set  id_clt=@id_clt, date_ajout=@date_ajout, etat=@etat, client=@client,id_fact=@id_fact,montant_ttc=@montant_ttc,remise=@remise,montant_ht=@montant_ht,timbre=@timbre,tva=@tva,faudec=@faudec,retenusource=@retenusource,numero_fact=@numero_fact,L_num_bl=@L_num_bl where id=@id";
                SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@id_clt", (object)id_clt));
            cmd.Parameters.Add(new SqlParameter("@date_ajout", (object)date));
            cmd.Parameters.Add(new SqlParameter("@etat", (object)etat));
            cmd.Parameters.Add(new SqlParameter("@client", (object)client));
            cmd.Parameters.Add(new SqlParameter("@id_fact", (object)nbcommande));
            cmd.Parameters.Add(new SqlParameter("@montant_ttc", (object)montantttc));
            cmd.Parameters.Add(new SqlParameter("@remise", (object)remise));
            cmd.Parameters.Add(new SqlParameter("@montant_ht", (object)montanthtc));
            cmd.Parameters.Add(new SqlParameter("@timbre", (object)timbre));
            cmd.Parameters.Add(new SqlParameter("@tva", (object)tva));
            cmd.Parameters.Add(new SqlParameter("@faudec", (object)fodec));
            cmd.Parameters.Add(new SqlParameter("@retenusource", (object)retres));
            cmd.Parameters.Add(new SqlParameter("@numero_fact", (object)numero));
            cmd.Parameters.Add(new SqlParameter("@id", (object)id));
           // cmd.Parameters.Add(new SqlParameter("@id_bl", (object)id_bl));
            cmd.Parameters.Add(new SqlParameter("@L_num_bl", (object)L_num_bl));
            cmd.ExecuteNonQuery();
            suivi_actions("mise à jour facture vente pour le client " + client + ".");
        }
        public string insertintofacturevente(string id_clt, string date, string etat, string client, string nbcommande, string montantttc, string remise, string montanthtc, string timbre, string tva, string fodec, string retres, string numero, int id_bl, string L_num_bl)
        {
            function_test();
            string req = "INSERT INTO facturevente (id_clt, date_ajout, etat, client,id_fact,montant_ttc,remise,montant_ht,timbre,tva,faudec,retenusource,numero_fact,id_bl,L_num_bl) VALUES"+
 " (@id_clt,@date_ajout,@etat,@client,@id_fact,@montant_ttc,@remise,@montant_ht,@timbre,@tva,@faudec,@retenusource,@numero_fact,@id_bl,@L_num_bl)";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@id_clt", (object)id_clt));
            cmd.Parameters.Add(new SqlParameter("@date_ajout", (object)date));
            cmd.Parameters.Add(new SqlParameter("@etat", (object)etat));
            cmd.Parameters.Add(new SqlParameter("@client", (object)client));
            cmd.Parameters.Add(new SqlParameter("@id_fact", (object)nbcommande));
            cmd.Parameters.Add(new SqlParameter("@montant_ttc", (object)montantttc));
            cmd.Parameters.Add(new SqlParameter("@remise", (object)remise));
            cmd.Parameters.Add(new SqlParameter("@montant_ht", (object)montanthtc));
            cmd.Parameters.Add(new SqlParameter("@timbre", (object)timbre));
            cmd.Parameters.Add(new SqlParameter("@tva", (object)tva));
            cmd.Parameters.Add(new SqlParameter("@faudec", (object)fodec));
            cmd.Parameters.Add(new SqlParameter("@retenusource", (object)retres));
            cmd.Parameters.Add(new SqlParameter("@numero_fact", (object)numero));
            cmd.Parameters.Add(new SqlParameter("@id_bl", (object)id_bl));
            cmd.Parameters.Add(new SqlParameter("@L_num_bl", (object)L_num_bl));
            cmd.ExecuteNonQuery();
            suivi_actions("Ajout d’une facture pour le client " + client + ".");
            return "";
        }
        public string insert_piecee_commande(string codep, string design, string qte, string idclt, string etat, string puv, string totvente, string id_commande)
        {
            function_test();
            string req = "INSERT INTO piece_commande (code_art,libelle_piece, quantite_piece, id_clt, etat, puv, totvente,id_commande) VALUES ('" + codep + "', '" + design + "','" + qte + "','" + idclt + "', '" + etat + "','" + puv + "', '" + totvente + "', '" + id_commande + "')";
            suivi_actions("Ajout piéce : ( " + codep + " ) avec la quantité : ( " + codep + " ) au commande client n° ( " + etat + " ).");
            return DataExcute(req);
        }
        public string insert_piecee_bl(string codep, string design, string qte, string idclt, string etat, string puv, string totvente, int id_commande, string remise, string tva, string unit)
        {
            function_test();
            string req = "INSERT INTO piece_bl (code_art,libelle_piece, quantite_piece, id_clt, etat, puv, totvente,id_commande,remise,ttva,unit) VALUES"+
 " (@code_art,@libelle_piece,@quantite_piece,@id_clt,@etat,@puv,@totvente,@id_commande,@remise,@ttva,@unit)";


            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@code_art", (object)codep));
            cmd.Parameters.Add(new SqlParameter("@libelle_piece", (object)design));
            cmd.Parameters.Add(new SqlParameter("@quantite_piece", (object)qte));
            cmd.Parameters.Add(new SqlParameter("@id_clt", (object)idclt));
            cmd.Parameters.Add(new SqlParameter("@etat", (object)etat));
            cmd.Parameters.Add(new SqlParameter("@puv", (object)puv));
            cmd.Parameters.Add(new SqlParameter("@totvente", (object)totvente));
            cmd.Parameters.Add(new SqlParameter("@id_commande", (object)id_commande));
            cmd.Parameters.Add(new SqlParameter("@remise", (object)remise));
            cmd.Parameters.Add(new SqlParameter("@ttva", (object)tva));
            cmd.Parameters.Add(new SqlParameter("@unit", (object)unit));
            cmd.ExecuteNonQuery();
           
            suivi_actions("Ajout piéce : ( " + codep + " ) avec la quantité : ( " + codep + " ) au commande client n° ( " + etat + " ).");
            return "";
        }
        public string insert_piecee_factpf(string codep, string design, string qte, string idclt, string etat, string puv, string totvente, string id_commande, string remise, string tva, string unit)
        {
            function_test();
            string req = "INSERT INTO piece_factpf (code_art,libelle_piece, quantite_piece, id_clt, etat, puv, totvente,id_factpf,remise,ttva,unit) VALUES ('" + codep + "', '" + design + "','" + qte + "','" + idclt + "', '" + etat + "','" + puv + "', '" + totvente + "', '" + id_commande + "', '" + remise + "', '" + tva + "','" + unit + "')";
            suivi_actions("Ajout piéce : ( " + codep + " ) avec la quantité : ( " + codep + " ) au commande client n° ( " + etat + " ).");
            return DataExcute(req);
        }
        public DataTable get_etat_factvente(int code)
        {
            function_test();
            req_select = "SELECT * FROM facturevente  WHERE id =@id";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@id", (object)code));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public string insert_piecee_factachat(string codep, string design, string qte, string idclt, string etat, string puv, string totvente, string id_commande, string remise, string tva, string unit)
        {
            function_test();
            string req = "INSERT INTO piece_factachat (code_art,libelle_piece, quantite_piece, id_clt, etat, puv, totvente,id_factpf,remise,ttva,unit) VALUES ('" + codep + "', '" + design + "','" + qte + "','" + idclt + "', '" + etat + "','" + puv + "', '" + totvente + "', '" + id_commande + "', '" + remise + "', '" + tva + "','" + unit + "')";
            suivi_actions("Ajout piéce : ( " + codep + " ) avec la quantité : ( " + codep + " ) au facture achat n° ( " + id_commande + " ).");
            return DataExcute(req);
        }
        public string insert_into_CommandeClient(string id_clt, string etat, string client, string etat_fac_bl, string code_bcommande)
        {
            function_test();
            string req = "INSERT INTO CommandeClient ( n_boncmd, date, etatcmd, client, etatfacture, etatbl, etatbnsortie,id_clt) VALUES ('" + code_bcommande + "','" + System.DateTime.Now.ToString() + "', '" + etat + "', '" + client + "', '" + etat_fac_bl + "', '" + etat_fac_bl + "',  '" + etat_fac_bl + "','" + id_clt + "')";
            suivi_actions("Ajout d’une commandeclient pour le client " + client + ".");
            return DataExcute(req);
        }

        public string insert_into_bl(string id_clt, string etat, string client, string code_bcommande, string id_cmd, string prixtotc, string timbre)
        {
            function_test();
            string req = "INSERT INTO bon_livraison ( nbcmd, date_ajout, etat, client,id_clt,id_commande,montant_ttc,timbre) VALUES"+
 " (@nbcmd,@date_ajout',@etat,@client,@id_clt,@id_commande,@montant_ttc,@timbre)";

            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@nbcmd", (object)code_bcommande));
            cmd.Parameters.Add(new SqlParameter("@date_ajout", (object)System.DateTime.Today.ToString()));
            cmd.Parameters.Add(new SqlParameter("@etat", (object)etat));
            cmd.Parameters.Add(new SqlParameter("@client", (object)client));
            cmd.Parameters.Add(new SqlParameter("@id_clt", (object)id_clt));
            cmd.Parameters.Add(new SqlParameter("@id_commande", (object)id_cmd));
            cmd.Parameters.Add(new SqlParameter("@montant_ttc", (object)prixtotc));
            cmd.Parameters.Add(new SqlParameter("@timbre", (object)timbre));
            cmd.ExecuteNonQuery();
            suivi_actions("Ajout d’une bl pour le client " + client + ".");
            
            return "";
        }
        public string insert_into_bl2(string id_clt, string etat, string client, string code_bcommande, string id_cmd, string prixtotc, string timbre, string numero, string mode_livraison, string moyen_livraison, string lieu_livraison, string date_ajout)
        {
            function_test();
            string req = "INSERT INTO bon_livraison ( nbcmd, date_ajout, etat, client,id_clt,id_commande,montant_ttc,timbre,numero_bl,mode_livraison,moyen_livraison,lieu_livraison) VALUES"+
 " (@nbcmd,@date_ajout,@etat,@client,@id_clt,@id_commande,@montant_ttc,@timbre,@numero_bl,@mode_livraison,@moyen_livraison,@lieu_livraison)";

            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@nbcmd", (object)code_bcommande));
            cmd.Parameters.Add(new SqlParameter("@date_ajout", (object)date_ajout));
            cmd.Parameters.Add(new SqlParameter("@etat", (object)etat));
            cmd.Parameters.Add(new SqlParameter("@client", (object)client));
            cmd.Parameters.Add(new SqlParameter("@id_clt", (object)id_clt));
            cmd.Parameters.Add(new SqlParameter("@id_commande", (object)id_cmd));
            cmd.Parameters.Add(new SqlParameter("@montant_ttc", (object)prixtotc));
            cmd.Parameters.Add(new SqlParameter("@timbre", (object)timbre));
            cmd.Parameters.Add(new SqlParameter("@numero_bl", (object)numero));
            cmd.Parameters.Add(new SqlParameter("@mode_livraison", (object)mode_livraison));
            cmd.Parameters.Add(new SqlParameter("@moyen_livraison", (object)moyen_livraison));
            cmd.Parameters.Add(new SqlParameter("@lieu_livraison", (object)lieu_livraison));
            cmd.ExecuteNonQuery();

            suivi_actions("Ajout d’une bl pour le client " + client + ".");

            return "";
        }
        public string insert_extrait_cl(DateTime datee, string design, string montant, string client, string type, string banque, string n_cheque, DateTime echeance, string type1, string retenusource)
        {
            function_test();
            try
            {
                string req = " insert into extrait_client (datee, design, montant,client,type,banque,n_cheque,echeance,type1,retenusource) values (@datee, @design, @montant, @client, @type,@banque,@n_cheque,@echeance,@type1,@retenusource)";
                SqlCommand cmd = new SqlCommand(req, sql_gmao.conn);



                cmd.Parameters.Add(new SqlParameter("@datee", (object)datee));
                cmd.Parameters.Add(new SqlParameter("@design", (object)design));
                cmd.Parameters.Add(new SqlParameter("@montant", (object)montant));
                cmd.Parameters.Add(new SqlParameter("@client", (object)client));
                cmd.Parameters.Add(new SqlParameter("@type", (object)type));
                cmd.Parameters.Add(new SqlParameter("@banque", (object)banque));
                cmd.Parameters.Add(new SqlParameter("@n_cheque", (object)n_cheque));
                cmd.Parameters.Add(new SqlParameter("@echeance", (object)echeance));
                cmd.Parameters.Add(new SqlParameter("@type1", (object)type1));
                cmd.Parameters.Add(new SqlParameter("@retenusource", (object)retenusource));

                cmd.ExecuteNonQuery();
                suivi_actions("Ajout d’un extrait");
                return "true";



            }
            catch (Exception ss)
            {
                return (ss.Message);
            }
        }
        public DataTable get_soldecl(string idclt)
        {
            function_test();
            //req_select = "SELECT solde FROM compte_cl WHERE code_cl=" + idclt ;
            req_select = "SELECT * FROM compte_cl WHERE code_cl='" + idclt + "'";
            // id_piece=" + codeP+";  

            return DataReturn(req_select);
        }
        public DataTable get_soldefr(string idclt)
        {
            function_test();
            //req_select = "SELECT solde FROM compte_cl WHERE code_cl=" + idclt ;
            req_select = "SELECT * FROM compte_fr WHERE code_fr='" + idclt + "'";
            // id_piece=" + codeP+";  

            return DataReturn(req_select);
        }
        public string insert_into_factpf(string id_clt, string etat, string client, string code_bcommande, string prixtotc, string timbre, string tva, int numero, string n_bl)
        {
            function_test();
            string req = "INSERT INTO facturePerformat ( id_cmd,date_ajout, etat, client,id_clt,montant_ttc,timbre,tva,numero,n_bl) VALUES ('" + code_bcommande + "','" + System.DateTime.Today.ToString() + "', '" + etat + "', '" + client + "', '" + id_clt + "','" + prixtotc + "','" + timbre + "','" + tva + "','" + numero + "','"+n_bl+"')";
            suivi_actions("Ajout d’une Facturee proformat pour le client " + client + ".");
            return DataExcute(req);
        }
        public string insert_into_factachat(string id_fr, string etat, string fournisseur, string code_bcommande, string prixtotc, string timbre, string tva)
        {
            function_test();
            string req = "INSERT INTO factureachat ( id_cmd,date_ajout, etat, fournisseur,id_fr,montant_ttc,timbre,tva) VALUES ('" + code_bcommande + "','" + System.DateTime.Today.ToString() + "', '" + etat + "', '" + fournisseur + "', '" + id_fr + "','" + prixtotc + "','" + timbre + "','" + tva + "')";
            suivi_actions("Ajout d’une Facture achat pour le fournisseur " + fournisseur + ".");
            return DataExcute(req);
        }
        public string insert_into_Commandepasse(string code_bcommande, string id_clt, string etat, string client, string etat_fac_bl, string prixtotc, string timbre)
        {
            function_test();
            string req = "INSERT INTO CommandeClient ( n_boncmd, date, etatcmd, client, etatfacture, etatbl, etatbnsortie,id_clt,montant_ttc,timbre) VALUES"+
  " (@n_boncmd,@date,@etatcmd,@client,@etatfacture,@etatbl,@etatbnsortie,@id_clt,@montant_ttc,@timbre)";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@n_boncmd", (object)code_bcommande));
            cmd.Parameters.Add(new SqlParameter("@date", (object)System.DateTime.Now.ToString()));
            cmd.Parameters.Add(new SqlParameter("@etatcmd", (object)etat_fac_bl));
            cmd.Parameters.Add(new SqlParameter("@client", (object)client));
            cmd.Parameters.Add(new SqlParameter("@etatfacture", (object)etat_fac_bl));
            cmd.Parameters.Add(new SqlParameter("@etatbl", (object)etat_fac_bl));
            cmd.Parameters.Add(new SqlParameter("@etatbnsortie", (object)etat_fac_bl));
            cmd.Parameters.Add(new SqlParameter("@id_clt", (object)id_clt));
            cmd.Parameters.Add(new SqlParameter("@montant_ttc", (object)prixtotc));
            cmd.Parameters.Add(new SqlParameter("@timbre", (object)timbre));
            cmd.ExecuteNonQuery();
            suivi_actions("Ajout d’une commandeclient pour le client " + client + ".");
            return "";
        }
        public string insert_compte_client(string code_cl, Double solde, Double debit, Double credit)
        {
            function_test();
            try
            {
                string req = " insert into compte_cl (code_cl, solde, debit,credit) values (@code_cl, @solde, @debit, @credit)";
                SqlCommand cmd = new SqlCommand(req, sql_gmao.conn);



                cmd.Parameters.Add(new SqlParameter("@code_cl", (object)code_cl));
                cmd.Parameters.Add(new SqlParameter("@solde", (object)solde));
                cmd.Parameters.Add(new SqlParameter("@debit", (object)debit));
                cmd.Parameters.Add(new SqlParameter("@credit", (object)credit));


                cmd.ExecuteNonQuery();
                suivi_actions("Ajout d’un compte client");
                return "true";


            }
            catch (Exception ss)
            {
                return (ss.Message);
            }
        }

        public string insert_into_Commandefr(string id_clt, string etat, string client, string etat_fac_bl, string prixtotc, string timbre, string ref_cmd, string ref_pf, string mode_payement, string contact, string mode_liv, string delai_livraison, string delai_payement)
        {
            function_test();
            string req = "INSERT INTO Commandefr (  date, etatcmd, client, etatfacture, etatbl, etatbnsortie,id_clt,montant_ttc,timbre,ref_cmd,ref_pf,mode_payement,contact,mode_liv,delai_livraison,delai_payement) VALUES "+
 "(@date,@etatcmd,@client,@etatfacture,@etatbl,@etatbnsortie,@id_clt,@montant_ttc,@timbre,@ref_cmd,@ref_pf,@mode_payement,@contact,@mode_liv,@delai_livraison,@delai_payement)";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@date", (object)System.DateTime.Now.ToString()));
            cmd.Parameters.Add(new SqlParameter("@etatcmd", (object)etat));
            cmd.Parameters.Add(new SqlParameter("@client", (object)client));
            cmd.Parameters.Add(new SqlParameter("@etatfacture", (object)etat_fac_bl));
            cmd.Parameters.Add(new SqlParameter("@etatbl", (object)etat_fac_bl));
            cmd.Parameters.Add(new SqlParameter("@etatbnsortie", (object)etat_fac_bl));
            cmd.Parameters.Add(new SqlParameter("@id_clt", (object)id_clt));
            cmd.Parameters.Add(new SqlParameter("@montant_ttc", (object)prixtotc));
            cmd.Parameters.Add(new SqlParameter("@timbre", (object)timbre));
            cmd.Parameters.Add(new SqlParameter("@ref_cmd", (object)ref_cmd));
            cmd.Parameters.Add(new SqlParameter("@ref_pf", (object)ref_pf));
            cmd.Parameters.Add(new SqlParameter("@mode_payement", (object)mode_payement));
            cmd.Parameters.Add(new SqlParameter("@contact", (object)contact));
            cmd.Parameters.Add(new SqlParameter("@mode_liv", (object)mode_liv));
            cmd.Parameters.Add(new SqlParameter("@delai_livraison", (object)delai_livraison));
            cmd.Parameters.Add(new SqlParameter("@delai_payement", (object)delai_payement));

            cmd.ExecuteNonQuery();
           
            suivi_actions("Ajout d’une commandefr pour le client " + client + ".");
            return "";
        }
        public string updat_into_Commandepasse(string code_bcommande, string id_clt, string client, string code)
        {
            function_test();
            string req = "UPDATE CommandeClient SET n_boncmd='" + code_bcommande + "', client='" + client + "' ,id_clt='" + id_clt + "' WHERE id_commande='" + code + "'";
            suivi_actions("Ajout d’une commandeclient pour le client " + client + ".");
            return DataExcute(req);
        }
        public string insert_into_factclt(string id_clt, string etat, string client, string etat_fac_bl, string code_bcommande)
        {
            function_test();
            string req = "INSERT INTO factclt ( n_boncmd, date, etatcmd, client, etatfacture, etatbl, etatbnsortie,id_clt) VALUES ('" + code_bcommande + "','" + System.DateTime.Now.ToString() + "', '" + etat + "', '" + client + "', '" + etat_fac_bl + "', '" + etat_fac_bl + "',  '" + etat_fac_bl + "','" + id_clt + "')";
            suivi_actions("Ajout d’une commandeclient pour le client " + client + ".");
            return DataExcute(req);
        }
        public string suivi_actions(string actions)
        {
            function_test();//login1.pseudo
            DateTime histDate = DateTime.Now;
            string req = "INSERT INTO historique (des_hist, useur_hist, date_hist)"
                + " VALUES (@des_hist,@useur_hist,@date_hist)";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@des_hist", (object)actions));
            cmd.Parameters.Add(new SqlParameter("@useur_hist", (object)login1.pseudo));
            cmd.Parameters.Add(new SqlParameter("@date_hist", (object)histDate));

            cmd.ExecuteNonQuery();
            return "";
        }



        public string set_Mesures(string des, string unite, int valSup, int valAct, string codeEq, string codeInt, string dateMes)
        {
            function_test();
            string req = "INSERT INTO mesures (designation_mesures,unite_mesures,limite_supp_mesures,valeur_actuelle_mesures,code_eq,code_intervenant,date_mesure) VALUES('" + des + "','" + unite + "','" + valSup + "','" + valAct + "','" + codeEq + "','" + codeInt + "','" + dateMes + "')";
            suivi_actions("Ajout mesures : (" + des + ") sous le charge de : (" + codeInt + ").");
            return DataExcute(req);
        }
        //ajouter un fichier du contrat
        public string insert_retour(string des, string clt, string dat, string comm, byte[] imm, string type, byte[] c)
        {
            function_test();
            try
            {
                string req = " insert into retour (descri, client, datee, commentaire, extension, type, imagee) values (@descri, @client, @datee, @commentaire, @extension, @type, @imagee)";
                SqlCommand cmd = new SqlCommand(req, sql_gmao.conn);


                cmd.Parameters.Add(new SqlParameter("@descri", (object)des));
                cmd.Parameters.Add(new SqlParameter("@client", (object)clt));
                cmd.Parameters.Add(new SqlParameter("@datee", (object)dat));
                cmd.Parameters.Add(new SqlParameter("@commentaire", (object)comm));
                cmd.Parameters.Add(new SqlParameter("@extension", (object)imm));
                cmd.Parameters.Add(new SqlParameter("@type", (object)type));
                cmd.Parameters.Add(new SqlParameter("@imagee", (object)c));

                cmd.ExecuteNonQuery();
                suivi_actions("Ajout d’un retour client");
                return "true";



            }
            catch (Exception ss)
            {
                return (ss.Message);
            }
        }



        
        //stock prix achat
        public DataTable prix_achat_prod(string code)
        {
            function_test();
            req_select = "SELECT pua FROM stock WHERE code_piece =@code_piece";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@code_piece", (object)code));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public string insert_retour2(string des, string clt, string dat, string comm, byte[] imm, string type, byte[] c)
        {
            function_test();
            try
            {
                string req = " insert into retour2 (descri, client, datee, commentaire, extension, type, imagee) values (@descri, @client, @datee, @commentaire, @extension, @type, @imagee)";
                SqlCommand cmd = new SqlCommand(req, sql_gmao.conn);


                cmd.Parameters.Add(new SqlParameter("@descri", (object)des));
                cmd.Parameters.Add(new SqlParameter("@client", (object)clt));
                cmd.Parameters.Add(new SqlParameter("@datee", (object)dat));
                cmd.Parameters.Add(new SqlParameter("@commentaire", (object)comm));
                cmd.Parameters.Add(new SqlParameter("@extension", (object)imm));
                cmd.Parameters.Add(new SqlParameter("@type", (object)type));
                cmd.Parameters.Add(new SqlParameter("@imagee", (object)c));

                cmd.ExecuteNonQuery();
                suivi_actions("Ajout d’une commande client reçu");
                return "true";



            }
            catch (Exception ss)
            {
                return (ss.Message);
            }
        }

        public string insert_retour3(string des, string clt, string dat, string comm, byte[] imm, string type, byte[] c)
        {
            function_test();
            try
            {
                string req = " insert into retour3 (descri, client, datee, commentaire, extension, type, imagee) values (@descri, @client, @datee, @commentaire, @extension, @type, @imagee)";
                SqlCommand cmd = new SqlCommand(req, sql_gmao.conn);


                cmd.Parameters.Add(new SqlParameter("@descri", (object)des));
                cmd.Parameters.Add(new SqlParameter("@client", (object)clt));
                cmd.Parameters.Add(new SqlParameter("@datee", (object)dat));
                cmd.Parameters.Add(new SqlParameter("@commentaire", (object)comm));
                cmd.Parameters.Add(new SqlParameter("@extension", (object)imm));
                cmd.Parameters.Add(new SqlParameter("@type", (object)type));
                cmd.Parameters.Add(new SqlParameter("@imagee", (object)c));

                cmd.ExecuteNonQuery();
                suivi_actions("Ajout d’une commande Fournisseur reçu");
                return "true";



            }
            catch (Exception ss)
            {
                return (ss.Message);
            }
        }

        public string insert_encaiss(DateTime datee, string design, string categ, string montant, string comment, string clt, string type1, Byte[] ext, string chemin, Byte[] image)
        {
            function_test();
            try
            {
                string req = " insert into conta (datee, design, categ, montant, comment, clt, extension, type, imagee, type1) values (@datee, @design, @categ, @montant, @comment, @clt, @extension, @type, @imagee, @type1)";
                SqlCommand cmd = new SqlCommand(req, sql_gmao.conn);



                cmd.Parameters.Add(new SqlParameter("@datee", (object)datee));
                cmd.Parameters.Add(new SqlParameter("@design", (object)design));
                cmd.Parameters.Add(new SqlParameter("@categ", (object)categ));
                cmd.Parameters.Add(new SqlParameter("@montant", (object)montant));
                cmd.Parameters.Add(new SqlParameter("@comment", (object)comment));
                cmd.Parameters.Add(new SqlParameter("@clt", (object)clt));
                cmd.Parameters.Add(new SqlParameter("@extension", (object)ext));
                cmd.Parameters.Add(new SqlParameter("@type", (object)chemin));
                cmd.Parameters.Add(new SqlParameter("@imagee", (object)image));
                cmd.Parameters.Add(new SqlParameter("@type1", (object)type1));

                cmd.ExecuteNonQuery();
                suivi_actions("Ajout d’un encaissement");
                return "true";



            }
            catch (Exception ss)
            {
                return (ss.Message);
            }
        }

        public string insert_charge(DateTime datee, string design, string categ, float montant, string comment, int duredevie, Byte[] extension, string ext, Byte[] imgdata)
        {
            try
            {
                string req = " insert into conta (datee, design, categ, montant, comment, clt, extension, type, imagee, type1) values (@datee, @design, @categ, @montant, @comment, @clt, @extension, @type, @imagee, @type1)";
                SqlCommand cmd = new SqlCommand(req, sql_gmao.conn);



                cmd.Parameters.Add(new SqlParameter("@datee", (object)datee));
                cmd.Parameters.Add(new SqlParameter("@design", (object)design));
                cmd.Parameters.Add(new SqlParameter("@categ", (object)categ));
                cmd.Parameters.Add(new SqlParameter("@montant", (object)montant));
                cmd.Parameters.Add(new SqlParameter("@comment", (object)comment));
                // cmd.Parameters.Add(new SqlParameter("@clt", (object)clt));
                cmd.Parameters.Add(new SqlParameter("@extension", (object)ext));
                //cmd.Parameters.Add(new SqlParameter("@type", (object)chemin));
                // cmd.Parameters.Add(new SqlParameter("@imagee", (object)image));
                // cmd.Parameters.Add(new SqlParameter("@type1", (object)type1));

                cmd.ExecuteNonQuery();
                suivi_actions("Ajout d’un encaissement");
                return "true";



            }
            catch (Exception ss)
            {
                return (ss.Message);
            }



        }


        public string insert_decaiss(DateTime datee, string design, string categ, string montant, string comment, string feur, string type1, Byte[] ext, string chemin, Byte[] image)
        {
            function_test();
            try
            {
                string req = " insert into conta (datee, design, categ, montant, comment, feur, extension, type, imagee, type1) values (@datee, @design, @categ, @montant, @comment, @feur, @extension, @type, @imagee, @type1)";
                SqlCommand cmd = new SqlCommand(req, sql_gmao.conn);



                cmd.Parameters.Add(new SqlParameter("@datee", (object)datee));
                cmd.Parameters.Add(new SqlParameter("@design", (object)design));
                cmd.Parameters.Add(new SqlParameter("@categ", (object)categ));
                cmd.Parameters.Add(new SqlParameter("@montant", (object)montant));
                cmd.Parameters.Add(new SqlParameter("@comment", (object)comment));
                cmd.Parameters.Add(new SqlParameter("@feur", (object)feur));
                cmd.Parameters.Add(new SqlParameter("@extension", (object)ext));
                cmd.Parameters.Add(new SqlParameter("@type", (object)chemin));
                cmd.Parameters.Add(new SqlParameter("@imagee", (object)image));
                cmd.Parameters.Add(new SqlParameter("@type1", (object)type1));

                cmd.ExecuteNonQuery();
                suivi_actions("Ajout d’un décaissement");
                return "true";



            }
            catch (Exception ss)
            {
                return (ss.Message);
            }
        }

        public string t_estt(string a, string b, string c)
        {
            function_test();
            string req = "INSERT INTO historique (des_hist, useur_hist, date_hist) VALUES('" + a + "', '" + b + "', '" + c + "')";
            return DataExcute(req);
        }


        public string set_info_supp(string code, string a, string b)
        {
            function_test();
            string req = "insert into info_supp_eq (designation_info_supp,contenu_info_supp,code_eq)values('" + a + "','" + b + "','" + code + "')";
            suivi_actions("Ajout d’une information supplimentaire : ( " + a + " ) pour l’équipement : ( " + code + " ).");
            return DataExcute(req);
        }
        public string set_compteur(string des, string unite, string lisup, string valact, string datRe, string code_eq)
        {
            function_test();
            string req = "INSERT INTO compteur(designation_compteur,unite_compteur,limite_supp_compteur,valeur_compteur ,date_releve,code_eq)VALUES('" + des + "','" + unite + "','" + lisup + "','" + valact + "','" + datRe + "','" + code_eq + "')";
            suivi_actions("Ajout d’une compteur : ( " + des + " ) pour l’équipement : ( " + code_eq + " ).");
            return DataExcute(req);
        }
        public string set_intervention(string b, string c, string d, string e, string f, string g, string h, string k, string code_eq, string interv, string exter, string dateclot)
        {
            function_test();
            string req = "INSERT INTO intervention (titre_intervention,description_intervention,etat_intervention,categorie_intervention,date_intervention,heure_debut_intervention,heure_fin_intervention,temps_total_intervention,code_eq,code_intervenant,code_soustraitant,date_fin_intervention)VALUES('" + b + "','" + c + "','" + d + "','" + e + "','" + f + "','" + g + "','" + h + "','" + k + "','" + code_eq + "','" + interv + "','" + exter + "','" + dateclot + "')";
            suivi_actions("Ajout d’une intervention : ( " + b + " ) pour l’équipement : ( " + code_eq + " ) suivi par l’intervenent interne : ( " + interv + " ).");
            return DataExcute(req);
        }
        public string set_interventionWithoutDate(string b, string c, string d, string e, string f, string g, string h, string k, string code_eq, string interv, string exter)
        {
            function_test();
            string req = "INSERT INTO intervention (titre_intervention,description_intervention,etat_intervention,categorie_intervention,date_intervention,heure_debut_intervention,heure_fin_intervention,temps_total_intervention,code_eq,code_intervenant,code_soustraitant)VALUES('" + b + "','" + c + "','" + d + "','" + e + "','" + f + "','" + g + "','" + h + "','" + k + "','" + code_eq + "','" + interv + "','" + exter + "')";
            suivi_actions("Ajout d’une intervention : ( " + b + " ) pour l’équipement : ( " + code_eq + " ) suivi par l’intervenent interne : ( " + interv + " ).");
            return DataExcute(req);
        }
        public string set_piece_utilise(string p, string l, string q, string c)
        {
            function_test();
            string req = "INSERT INTO piece (libelle_piece_u,quantite_piece_u,code_intervention) VALUES('" + l + "','" + q + "','" + c + "')";
            suivi_actions("Ajout d’une piece : ( " + l + " ) avec le code piéce : ( " + p + " ) pour l’intervention : ( " + c + " ).");
            return DataExcute(req);
        }

        public string set_familleEquipement(string famillle)
        {
            function_test();
            string req = "INSERT INTO Famille_equipement (designation_fe) VALUES(@designation_fe)";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@designation_fe", (object)famillle));
            cmd.ExecuteNonQuery();
            suivi_actions("Ajout d’une Fmille d’équipement : ( " + famillle + " ).");
            return "";
        }
        public string set_SousfamilleEquipement(string sousfamillle, int codefamille)
        {
            function_test();
            string req = "INSERT INTO SousFamille_eq (designation_sfe,code_fe) VALUES('" + sousfamillle + "','" + codefamille + "')";
            suivi_actions("Ajout d’une sous famille : ( " + sousfamillle + " ) pour la famille : ( " + codefamille + " ).");
            return DataExcute(req);
        }
        public string set_feur(string code_feur, string raison_soc, string responsbale, string gsm_feur, string tel_feur, string fax_feur, string adresse_feur, string cp_feur, string ville_feur, string email_feur, string site_feur, string tva_feur, string forme_juriduque, string mode_pay)
        {
            function_test();
            string req = "INSERT INTO fournisseur(code_feur,raison_soc,responsbale,gsm_feur,tel_feur,fax_feur,adresse_feur,cp_feur,ville_feur,email_feur,site_feur,tva_feur,forme_juriduque,mode_pay)"+
                " VALUES(@code_feur,@raison_soc,@responsbale,@gsm_feur,@tel_feur,@fax_feur,@adresse_feur,@cp_feur,@ville_feur,@email_feur,@site_feur,@tva_feur,@forme_juriduque,@mode_pay)";

            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@code_feur", (object)code_feur));
            cmd.Parameters.Add(new SqlParameter("@raison_soc", (object)raison_soc));
            cmd.Parameters.Add(new SqlParameter("@responsbale", (object)responsbale));
            cmd.Parameters.Add(new SqlParameter("@gsm_feur", (object)gsm_feur));
            cmd.Parameters.Add(new SqlParameter("@tel_feur", (object)tel_feur));
            cmd.Parameters.Add(new SqlParameter("@fax_feur", (object)fax_feur));
            cmd.Parameters.Add(new SqlParameter("@adresse_feur", (object)adresse_feur));
            cmd.Parameters.Add(new SqlParameter("@cp_feur", (object)cp_feur));
            cmd.Parameters.Add(new SqlParameter("@ville_feur", (object)ville_feur));
            cmd.Parameters.Add(new SqlParameter("@email_feur", (object)email_feur));
            cmd.Parameters.Add(new SqlParameter("@site_feur", (object)site_feur));
            cmd.Parameters.Add(new SqlParameter("@tva_feur", (object)tva_feur));
            cmd.Parameters.Add(new SqlParameter("@forme_juriduque", (object)forme_juriduque));
            cmd.Parameters.Add(new SqlParameter("@mode_pay", (object)mode_pay));
            cmd.ExecuteNonQuery();
            suivi_actions("Ajout d’un fournisseur sous le code : ( " + code_feur + " ) leur responsable : ( " + responsbale + " ).");
            return DataExcute(req);
        }



        public string set_clt(string raison_soc, string responsbale, string gsm_feur, string tel_feur, string fax_feur,
            string adresse_feur, string cp_feur, string ville_feur, string email_feur, string site_feur, string tva_feur, string forme_juriduque, string mode_pay,string code)
        {
            function_test();
            string req = "INSERT INTO client (raison_soc,responsbale,gsm_clt,tel_clt,fax_clt,adresse_clt,cp_clt,ville_clt,email_clt,site_clt,tva_clt,forme_juriduque,mode_pay,code) VALUES"+
 "(@raison_soc,@responsbale,@gsm_clt,@tel_clt,@fax_clt,@adresse_clt,@cp_clt,@ville_clt,@email_clt,@site_clt,@tva_clt,@forme_juriduque,@mode_pay,@code)";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@raison_soc", (object)raison_soc));
            cmd.Parameters.Add(new SqlParameter("@responsbale", (object)responsbale));
            cmd.Parameters.Add(new SqlParameter("@gsm_clt", (object)gsm_feur));
            cmd.Parameters.Add(new SqlParameter("@tel_clt", (object)tel_feur));
            cmd.Parameters.Add(new SqlParameter("@fax_clt", (object)fax_feur));
            cmd.Parameters.Add(new SqlParameter("@adresse_clt", (object)adresse_feur));
            cmd.Parameters.Add(new SqlParameter("@cp_clt", (object)cp_feur));
            cmd.Parameters.Add(new SqlParameter("@ville_clt", (object)ville_feur));
            cmd.Parameters.Add(new SqlParameter("@email_clt", (object)email_feur));
            cmd.Parameters.Add(new SqlParameter("@site_clt", (object)site_feur));
            cmd.Parameters.Add(new SqlParameter("@tva_clt", (object)tva_feur));
            cmd.Parameters.Add(new SqlParameter("@forme_juriduque", (object)forme_juriduque));
            cmd.Parameters.Add(new SqlParameter("@mode_pay", (object)mode_pay));
            cmd.Parameters.Add(new SqlParameter("@code", (object)code));
            
            DataTable dt = new DataTable();
            cmd.ExecuteNonQuery();
           
            suivi_actions("Ajout d’un Client sous le code : ( " + raison_soc + " ), le responsable : ( " + responsbale + " ).");
            return "";
        }



        public int set_stock(string code_piece, string libelle_piece, string unite_piece, string quantite_piece, string quantite_reelle, string seuil_piece, string nature, string pua, string puv, string code_feur, string puv_rev)
        {
            function_test();
            string req = "INSERT INTO stock(code_piece,libelle_piece,unite_piece,quantite_piece, quantite_reelle,seuil_piece, nature, pua,puv,code_feur, puv_rev)"
                + " VALUES(@code_piece,@libelle_piece,@unite_piece,@quantite_piece,@quantite_reelle,@seuil_piece,@nature,@pua,@puv,@code_feur,@puv_rev)";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@code_piece", (object)code_piece));
            cmd.Parameters.Add(new SqlParameter("@libelle_piece", (object)libelle_piece));
            cmd.Parameters.Add(new SqlParameter("@unite_piece", (object)unite_piece));
            cmd.Parameters.Add(new SqlParameter("@quantite_piece", (object)quantite_piece));
            cmd.Parameters.Add(new SqlParameter("@quantite_reelle", (object)quantite_reelle));
            cmd.Parameters.Add(new SqlParameter("@seuil_piece", (object)seuil_piece));
            cmd.Parameters.Add(new SqlParameter("@nature", (object)nature));
            cmd.Parameters.Add(new SqlParameter("@pua", (object)pua));
            cmd.Parameters.Add(new SqlParameter("@puv", (object)puv));
            cmd.Parameters.Add(new SqlParameter("@code_feur", (object)code_feur));
            cmd.Parameters.Add(new SqlParameter("@puv_rev", (object)puv_rev));
            
            
            cmd.ExecuteNonQuery();
            suivi_actions("Ajout d’une piéce : ( " + libelle_piece + " ) avec la quantité : ( " + quantite_piece + " ).");
           // DataExcute(req);
            return 1;
        }
        public string insert_piece(string l, int q, int c)
        {
            function_test();
            string req = "INSERT INTO piece (libelle_piece_u, quantite_piece_u, UniqueID) VALUES ('" + l + "','" + q + "','" + c + "')";
            suivi_actions("Réservation piéce : ( " + l + " ) avec la quantité : ( " + q + " ) pour une intervention préventive.");
            return DataExcute(req);
        }

        public string insert_piecee(string l, int q, string c, string d, int e, int f, int pua, int pv)
        {
            function_test();
            string req = "INSERT INTO piece (libelle_piece_u, quantite_piece_u, id_feur, etat, id_cde, req_piece,pua, pv ) VALUES ('" + l + "','" + q + "','" + c + "', '" + d + "', '" + e + "', '" + f + "', , '" + pua + "', '" + pv + "')";
            suivi_actions("Ajout piéce : ( " + l + " ) avec la quantité : ( " + q + " ) au commande.");
            return DataExcute(req);
        }



        public string insert_piecee_devis(string lib, string codep, string qp, int idf, string puv, string pv)
        {
            function_test();
            string req = "INSERT INTO piece_devis (libelle_piece_u, code_piece_u, quantite_piece_u, id_fact, puv, pv) VALUES ('" + lib + "', '" + codep + "','" + qp + "','" + idf + "', '" + puv + "',  '" + pv + "')";
            suivi_actions("Ajout piéce : ( " + lib + " ) avec la quantité : ( " + qp + " ) au devis n° ( " + idf + " ).");
            return DataExcute(req);
        }

        public string insert_piecee_fact(string l, string z, Double q, string c, string d, int e, string puv, string pv, string idcmd)
        {
            function_test();
            string req = "INSERT INTO piece_fact (libelle_piece_u, code_piece_u, quantite_piece_u, id_clt, etat, id_fact, puv, pv,id_commande) VALUES ('" + l + "', '" + z + "','" + q + "','" + c + "', '" + d + "', '" + e + "', '" + puv + "', '" + pv + "', '" + idcmd + "')";
            suivi_actions("Ajout piéce : ( " + l + " ) avec la quantité : ( " + q + " ) au commande client n° ( " + e + " ).");
            return DataExcute(req);
        }
        public string insert_piecee_commande1(string codep, string lib, Double quantit, string unite, string puv, string remise, string ttva, string idclt, string id_cmd, string pv, string qtrest)
        {

            function_test();
            string req = "INSERT INTO piece_commande (code_art, libelle_piece, quantite_piece,unit, id_clt,  puv,remise,ttva,id_commande,totvente,qterest)"+
" VALUES (@code_art,@libelle_piece,@quantite_piece,@unit,@id_clt,@puv,@remise,@ttva,@id_commande,@totvente,@qterest)";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@code_art", (object)codep));
            cmd.Parameters.Add(new SqlParameter("@libelle_piece", (object)lib));
            cmd.Parameters.Add(new SqlParameter("@quantite_piece", (object)quantit));
            cmd.Parameters.Add(new SqlParameter("@unit", (object)unite));
            cmd.Parameters.Add(new SqlParameter("@id_clt", (object)idclt));
            cmd.Parameters.Add(new SqlParameter("@puv", (object)puv));
            cmd.Parameters.Add(new SqlParameter("@remise", (object)remise));
            cmd.Parameters.Add(new SqlParameter("@ttva", (object)ttva));
            cmd.Parameters.Add(new SqlParameter("@id_commande", (object)id_cmd));
            cmd.Parameters.Add(new SqlParameter("@totvente", (object)pv));
            cmd.Parameters.Add(new SqlParameter("@qterest", (object)qtrest));
            cmd.ExecuteNonQuery();
            suivi_actions("Ajout cmd : ( " + lib + " ) avec la quantité : ( " + quantit + " ) au commande client n° ( " + pv + " ).");
            return "";
        }
        public string insert_piecee_commandefr(string codep, string lib, Double quantit, string unite, string puv, string remise, string ttva, string idclt, string id_cmd, string pv, string qtrest)
        {

            function_test();
            string req = "INSERT INTO piece_commandefr (code_art, libelle_piece, quantite_piece,unit, id_clt,  puv,remise,ttva,id_commande,totvente,qterest) VALUES"+
"(@code_art,@libelle_piece,@quantite_piece,@unit,@id_clt,@puv,@remise,@ttva,@id_commande,@totvente,@qterest)";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@code_art", (object)codep));
            cmd.Parameters.Add(new SqlParameter("@libelle_piece", (object)lib));
            cmd.Parameters.Add(new SqlParameter("@quantite_piece", (object)quantit));
            cmd.Parameters.Add(new SqlParameter("@unit", (object)unite));
            cmd.Parameters.Add(new SqlParameter("@id_clt", (object)idclt));
            cmd.Parameters.Add(new SqlParameter("@puv", (object)puv));
            cmd.Parameters.Add(new SqlParameter("@remise", (object)remise));
            cmd.Parameters.Add(new SqlParameter("@ttva", (object)ttva));
            cmd.Parameters.Add(new SqlParameter("@id_commande", (object)id_cmd));
            cmd.Parameters.Add(new SqlParameter("@totvente", (object)pv));
            cmd.Parameters.Add(new SqlParameter("@qterest", (object)qtrest));
            cmd.ExecuteNonQuery();
           
            suivi_actions("Ajout cmd fr: ( " + lib + " ) avec la quantité : ( " + quantit + " ) au commande client n° ( " + pv + " ).");
            return "";
        }
        public string insert_piecee_bl1(string codep, string lib, Double quantit, string unite, string puv, string remise, string ttva, string idclt, string id_cmd, string pv)
        {

            function_test();
            string req = "INSERT INTO piece_bl (code_art, libelle_piece, quantite_piece,unit, id_clt,  puv,remise,ttva,id_commande,totvente) VALUES ('" + codep + "','" + lib + "', " + quantit + ",'" + unite + "','" + idclt + "', '" + puv + "', '" + remise + "', '" + ttva + "', '" + id_cmd + "','" + pv + "')";

            suivi_actions("Ajout cmd : ( " + lib + " ) avec la quantité : ( " + quantit + " ) au commande client n° ( " + pv + " ).");
            return DataExcute(req);
        }

        // ajouter technicien interne avec image
        public string insert_technicien(byte[] imgdata, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s10, string c1, string np)
        {
            function_test();
            string aa = " insert into intervenant (nom_intervenant,  prenom_intervenant ,  email_intervenant ,  tel_intervenant ,   mobile_intervenant ,  poste_intervenant, Image, pic, code_intervenant, etat1, ResourceName) values (@nom_intervenant,  @prenom_intervenant ,  @email ,  @tel1 ,   @tel2 ,  @poste_intervenant , @imagee, @pic, @code_intervenant, @etat1, @np)";
            SqlCommand cmd = new SqlCommand(aa, conn);
            cmd.Parameters.Add(new SqlParameter("@nom_intervenant", (object)s3));
            cmd.Parameters.Add(new SqlParameter("@prenom_intervenant", (object)s2));
            cmd.Parameters.Add(new SqlParameter("@email", (object)s6));
            cmd.Parameters.Add(new SqlParameter("@tel1", (object)s4));
            cmd.Parameters.Add(new SqlParameter("@tel2", (object)s5));
            cmd.Parameters.Add(new SqlParameter("@poste_intervenant", (object)s7));
            cmd.Parameters.Add(new SqlParameter("@etat1", (object)c1));
            cmd.Parameters.Add(new SqlParameter("@imagee", (object)imgdata));
            cmd.Parameters.Add(new SqlParameter("@pic", (object)s8));
            cmd.Parameters.Add(new SqlParameter("@code_intervenant", (object)s10));
            cmd.Parameters.Add(new SqlParameter("@np", (object)np));
            cmd.ExecuteNonQuery();
            return "true";
        }

        // ajouter technicien interne sans image
        public string insert_technicien2(string s2, string s3, string s4, string s5, string s6, string s7, string s10, string c1, string np)
        {
            function_test();
            string aa = " insert into intervenant (nom_intervenant,  prenom_intervenant ,  email_intervenant ,  tel_intervenant ,   mobile_intervenant ,  poste_intervenant, code_intervenant, etat1, ResourceName) values (@nom_intervenant,  @prenom_intervenant ,  @email_intervenant ,  @tel_intervenant ,   @mobile_intervenant ,  @poste_intervenant , @code_intervenant, @etat1, @np)";
            SqlCommand cmd = new SqlCommand(aa, conn);
            cmd.Parameters.Add(new SqlParameter("@nom_intervenant", (object)s3));
            cmd.Parameters.Add(new SqlParameter("@prenom_intervenant", (object)s2));
            cmd.Parameters.Add(new SqlParameter("@email_intervenant", (object)s6));
            cmd.Parameters.Add(new SqlParameter("@tel_intervenant", (object)s4));
            cmd.Parameters.Add(new SqlParameter("@mobile_intervenant", (object)s5));
            cmd.Parameters.Add(new SqlParameter("@poste_intervenant", (object)s7));
            cmd.Parameters.Add(new SqlParameter("@etat1", (object)c1));
            cmd.Parameters.Add(new SqlParameter("@np", (object)np));
            cmd.Parameters.Add(new SqlParameter("@code_intervenant", (object)s10));
            cmd.ExecuteNonQuery();
            return "true";
        }
        //ajouter sous traitant avec image 
        public string insert_sous_traitant(byte[] imgdata, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s9, string s10, string s11, string c1)
        {
            function_test();
            string aa = " insert into sous_traitant (code_soustraitant,  raison_sociale_soustraitant ,  nom_responsable_soustraitant ,  tel_soustraitant ,   mobile_soustraitant ,  email_soustraitant, Image, pic, fonction, fax, prenom_responsable_soustraitant, etat) values (@code_soustraitant,  @raison_sociale_soustraitant ,  @nom_responsable_soustraitant ,  @tel_soustraitant ,   @mobile_soustraitant ,  @email_soustraitant , @imagee, @pic, @fonction, @fax, @prenom_responsable_soustraitant, @etat)";
            SqlCommand cmd = new SqlCommand(aa, conn);
            cmd.Parameters.Add(new SqlParameter("@code_soustraitant", (object)s10));
            cmd.Parameters.Add(new SqlParameter("@raison_sociale_soustraitant", (object)s9));
            cmd.Parameters.Add(new SqlParameter("@nom_responsable_soustraitant", (object)s3));
            cmd.Parameters.Add(new SqlParameter("@tel_soustraitant", (object)s4));
            cmd.Parameters.Add(new SqlParameter("@mobile_soustraitant", (object)s5));
            cmd.Parameters.Add(new SqlParameter("@email_soustraitant", (object)s6));
            cmd.Parameters.Add(new SqlParameter("@fonction", (object)s7));
            cmd.Parameters.Add(new SqlParameter("@fax", (object)s11));
            cmd.Parameters.Add(new SqlParameter("@prenom_responsable_soustraitant", (object)s2));
            cmd.Parameters.Add(new SqlParameter("@imagee", (object)imgdata));
            cmd.Parameters.Add(new SqlParameter("@pic", (object)s8));
            cmd.Parameters.Add(new SqlParameter("@etat", (object)c1));
            cmd.ExecuteNonQuery();
            return "true";
        }

        //ajouter sous traitant sans image 
        public string insert_sous_traitant2(string s2, string s3, string s4, string s5, string s6, string s7, string s9, string s10, string s11, string c1)
        {
            function_test();
            string aa = " insert into sous_traitant (code_soustraitant,  raison_sociale_soustraitant ,  nom_responsable_soustraitant ,  tel_soustraitant ,   mobile_soustraitant ,  email_soustraitant, fonction, fax, prenom_responsable_soustraitant, etat) values (@code_soustraitant,  @raison_sociale_soustraitant ,  @nom_responsable_soustraitant ,  @tel_soustraitant ,   @mobile_soustraitant ,  @email_soustraitant , @fonction, @fax, @prenom_responsable_soustraitant, @etat)";
            SqlCommand cmd = new SqlCommand(aa, conn);
            cmd.Parameters.Add(new SqlParameter("@code_soustraitant", (object)s10));
            cmd.Parameters.Add(new SqlParameter("@raison_sociale_soustraitant", (object)s9));
            cmd.Parameters.Add(new SqlParameter("@nom_responsable_soustraitant", (object)s3));
            cmd.Parameters.Add(new SqlParameter("@tel_soustraitant", (object)s4));
            cmd.Parameters.Add(new SqlParameter("@mobile_soustraitant", (object)s5));
            cmd.Parameters.Add(new SqlParameter("@email_soustraitant", (object)s6));
            cmd.Parameters.Add(new SqlParameter("@fonction", (object)s7));
            cmd.Parameters.Add(new SqlParameter("@fax", (object)s11));
            cmd.Parameters.Add(new SqlParameter("@prenom_responsable_soustraitant", (object)s2));
            cmd.Parameters.Add(new SqlParameter("@etat", (object)c1));
            cmd.ExecuteNonQuery();
            return "true";
        }
        //ajouter utilisateur de type gestion de stock
        public string insert_user_gestion_stock(string x1, string x2, string x3, string x4, string x5, string x6, string x7, string x8, string x9, string x10, string x11, string x12, string x13, string x14, string x15, string x16, string x17, string x18, string x19, string x20, string x21, string x22, string x23, string x24, string x25, string x26, string x27, string x28, string x29, string x30, string x31, string x32, string x33, string x34, string x35, string x36, string x37, string x38, string x39, string x40, string x41, string x42, string x43, string x44)
        {
            function_test();
            string req = "insert into droit (nom, prenom, fonction, depart, gsm, email, login, passwd, visualiser_stock, ajouter_stock, modifier_stock, supprimer_stock, ges_uni, ges_mag, doc_stock,"+
  " passer_cde, aliment, sortie_prod, alim, sortie_sto, vis_clt, ajout_clt, mod_clt, supp_clt, doc_clt, supp_cde_clt, vali_cde_clt, fact, bon_liv, bon_sortie, vis_feur, aj_feur,"+
   " mod_feur, supp_feur, doc_feur, supp_cde_feur, vis_dev, aj_devis, supp_dev, doc_dev, stat, noti, conta, stat_conta)values"+
   "(@nom,@prenom,@fonction,@depart,@gsm,@email,@login,@passwd,@visualiser_stock,@ajouter_stock,@modifier_stock,@supprimer_stock,@ges_uni,@ges_mag,@doc_stock"+
   ",@passer_cde,@aliment,@sortie_prod,@alim,@sortie_sto,@vis_clt,@ajout_clt,@mod_clt,@supp_clt,@doc_clt,@supp_cde_clt,@vali_cde_clt,@fact,@bon_liv,@bon_sortie,@vis_feur,@aj_feur"+
   ",@mod_feur,@supp_feur,@doc_feur,@supp_cde_feur,@vis_dev,@aj_devis,@supp_dev,@doc_dev,@stat,@noti,@conta,@stat_conta)";

            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@nom", (object)x1));
            cmd.Parameters.Add(new SqlParameter("@prenom", (object)x2));
            cmd.Parameters.Add(new SqlParameter("@fonction", (object)x3));
            cmd.Parameters.Add(new SqlParameter("@depart", (object)x4));
            cmd.Parameters.Add(new SqlParameter("@gsm", (object)x5));
            cmd.Parameters.Add(new SqlParameter("@email", (object)x6));
            cmd.Parameters.Add(new SqlParameter("@login", (object)x7));
            cmd.Parameters.Add(new SqlParameter("@passwd", (object)x8));
            cmd.Parameters.Add(new SqlParameter("@visualiser_stock", (object)x9));
            cmd.Parameters.Add(new SqlParameter("@ajouter_stock", (object)x10));
            cmd.Parameters.Add(new SqlParameter("@modifier_stock", (object)x11));
            cmd.Parameters.Add(new SqlParameter("@supprimer_stock", (object)x12));
            cmd.Parameters.Add(new SqlParameter("@ges_uni", (object)x13));
            cmd.Parameters.Add(new SqlParameter("@ges_mag", (object)x14));
            cmd.Parameters.Add(new SqlParameter("@doc_stock", (object)x15));
            cmd.Parameters.Add(new SqlParameter("@passer_cde", (object)x16));
            cmd.Parameters.Add(new SqlParameter("@aliment", (object)x17));
            cmd.Parameters.Add(new SqlParameter("@sortie_prod", (object)x18));
            cmd.Parameters.Add(new SqlParameter("@alim", (object)x19));
            cmd.Parameters.Add(new SqlParameter("@sortie_sto", (object)x20));
            cmd.Parameters.Add(new SqlParameter("@vis_clt", (object)x21));
            cmd.Parameters.Add(new SqlParameter("@ajout_clt", (object)x22));
            cmd.Parameters.Add(new SqlParameter("@mod_clt", (object)x23));
            cmd.Parameters.Add(new SqlParameter("@supp_clt", (object)x24));
            cmd.Parameters.Add(new SqlParameter("@doc_clt", (object)x25));
            cmd.Parameters.Add(new SqlParameter("@supp_cde_clt", (object)x26));
            cmd.Parameters.Add(new SqlParameter("@vali_cde_clt", (object)x27));
            cmd.Parameters.Add(new SqlParameter("@fact", (object)x28));
            cmd.Parameters.Add(new SqlParameter("@bon_liv", (object)x29));
            cmd.Parameters.Add(new SqlParameter("@bon_sortie", (object)x30));
            cmd.Parameters.Add(new SqlParameter("@vis_feur", (object)x31));
            cmd.Parameters.Add(new SqlParameter("@aj_feur", (object)x32));
            cmd.Parameters.Add(new SqlParameter("@mod_feur", (object)x33));
            cmd.Parameters.Add(new SqlParameter("@supp_feur", (object)x34));
            cmd.Parameters.Add(new SqlParameter("@doc_feur", (object)x35));
            cmd.Parameters.Add(new SqlParameter("@supp_cde_feur", (object)x36));
            cmd.Parameters.Add(new SqlParameter("@vis_dev", (object)x37));
            cmd.Parameters.Add(new SqlParameter("@aj_devis", (object)x38));
            cmd.Parameters.Add(new SqlParameter("@supp_dev", (object)x39));
            cmd.Parameters.Add(new SqlParameter("@doc_dev", (object)x40));
            cmd.Parameters.Add(new SqlParameter("@stat", (object)x41));
            cmd.Parameters.Add(new SqlParameter("@noti", (object)x42));
            cmd.Parameters.Add(new SqlParameter("@conta", (object)x43));
            cmd.Parameters.Add(new SqlParameter("@stat_conta", (object)x44));
            cmd.ExecuteNonQuery();
           
            suivi_actions("Ajout des droits à : " + x1 + " " + x2);
            return "";
        }
        // ajouter une unité
        public string set_Unite(string unite)
        {
            function_test();
            string req = "INSERT INTO unite (designation_unite) VALUES(@designation_unite)";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@designation_unite", (object)unite));
            cmd.ExecuteNonQuery();
            suivi_actions("Ajout d’une unité: (" + unite + ")");
            return "";
        }
        // ajouter une Magasin de stock
        public string set_Magasin(string magasin)
        {
            function_test();
            string req = "INSERT INTO empalcement (des_emp) VALUES(@des_emp)";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@des_emp", (object)magasin));
            cmd.ExecuteNonQuery();
            suivi_actions("Ajout d’un Magasin\\Emplacement: (" + magasin + ")");
            return "";
        }


        public string set_categ_encaiss(string cat, string type)
        {
            function_test();
            string req = "INSERT INTO categ (des, type) VALUES('" + cat + "', '" + type + "' )";
            suivi_actions("Ajout d’une catégorie d'encaissement: (" + cat + ")");
            return DataExcute(req);
        }




        //ajouter utilisateur de type admin
        public string insert_user_admin(string x1, string x2, string x3, string x4, string x5, string x6, string x7, string x8)
        {
            function_test();
            string req = "insert into droit (nom, prenom, fonction, depart, gsm, email, login, passwd) values"+
  " (@nom,@prenom,@fonction,@depart,@gsm,@email,@login,@passwd)";

            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@nom", (object)x1));
            cmd.Parameters.Add(new SqlParameter("@prenom", (object)x2));
            cmd.Parameters.Add(new SqlParameter("@fonction", (object)x3));
            cmd.Parameters.Add(new SqlParameter("@depart", (object)x4));
            cmd.Parameters.Add(new SqlParameter("@gsm", (object)x5));
            cmd.Parameters.Add(new SqlParameter("@email", (object)x6));
            cmd.Parameters.Add(new SqlParameter("@login", (object)x7));
            cmd.Parameters.Add(new SqlParameter("@passwd", (object)x8));
            cmd.ExecuteNonQuery();
            suivi_actions("Création d'un Administrateur  : " + x1 + " " + x2);

            return "";
        }
        public string set_notification(string titre, string des, string code_relative)
        {
            function_test();
            DateTime notifDate = DateTime.Now;
            string req = "INSERT INTO notification (titre_notif, des_notif, code_relative_notif,date_notif,depar_notif) VALUES ('" + titre + "','" + des + "','" + code_relative + "','" + notifDate + "','None')";
            return DataExcute(req);
        }
        public string set_administrateur(string nom, string poste, string dep, string gsm, string email, string login, string pwd)
        {
            function_test();
            string req = "INSERT INTO droit (n_p,fonction,depart,gsm,email,login,passwd) VALUES ('" + nom + "','" + poste + "','" + dep + "','" + gsm + "','" + email + "','" + login + "','" + pwd + "')";
            suivi_actions("Ajout administrateur : " + nom);
            return DataExcute(req);
        }
        public string insert_into_piece(string piece, int quantite, int code)
        {
            function_test();
            string req = "INSERT INTO piece (libelle_piece_u, quantite_piece_u, UniqueID) VALUES ('" + piece + "','" + quantite + "','" + code + "')";
            return DataExcute(req);
        }
        public string insert_into_commande1(string req_feur, string etat, string d)
        {
            function_test();
            string req = "INSERT INTO commande (id_feur, date_cre, etat, fournisseur) VALUES ('" + req_feur + "','" + System.DateTime.Now.ToString() + "', '" + etat + "', '" + d + "')";
            suivi_actions("Ajout d’une commande fournisseur.");
            return DataExcute(req);
        }

        public string insert_into_fact(string id_clt, string etat, string client, string etat_fac_bl, string nbcommande, string montantttc, string remise, string montanthtc, string timbre, string tva)
        {
            function_test();
            string req = "INSERT INTO facture (id_clt, date_ajout, etat, client, etat_fact, etat_bon_liv, etat_bon_sortie,id_fact,montant_ttc,remise,montant_ht,timbre,tva) VALUES ('" + id_clt + "','" + System.DateTime.Now.ToString() + "', '" + etat + "', '" + client + "', '" + etat_fac_bl + "', '" + etat_fac_bl + "',  '" + etat_fac_bl + "','" + nbcommande + "', '" + montantttc + "',  '" + remise + "','" + montanthtc + "','" + timbre + "','" + tva + "')";
            suivi_actions("Ajout d’une commande pour le client " + client + ".");
            return DataExcute(req);
        }
        public string insertintofacture(string id_clt, string date, string etat, string client, string nbcommande, string montantttc, string remise, string montanthtc, string timbre, string tva)
        {
            function_test();
            string req = "INSERT INTO fact (id_clt, date_ajout, etat, client,id_fact,montant_ttc,remise,montant_ht,timbre,tva) VALUES ('" + id_clt + "','" + date + "', '" + etat + "', '" + client + "', '" + nbcommande + "', '" + montantttc + "',  '" + remise + "','" + montanthtc + "','" + timbre + "','" + tva + "')";
            suivi_actions("Ajout d’une facture pour le client " + client + ".");
            return DataExcute(req);
        }

        public string insert_into_devis(string etat)
        {
            function_test();
            string req = "INSERT INTO devis (date_ajout, etat) VALUES ('" + System.DateTime.Now.ToString() + "', '" + etat + "')";
            suivi_actions("Création d'un devis.");
            return DataExcute(req);
        }


        public string set_SocieteWithPicture(byte[] imgdata, string nom_soc, string res_soc, string adr_soc, string tel_soc, string fax_soc, string gsm_soc, string email_soc, string site_soc, string mat_soc, string pic_soc, string banque, string compte, string pic_pied)
        {
            function_test();
            string req = " INSERT INTO societe(nom_societe,responseble_societe,adresse_societe,tel_societe,fax_societe,gsm_societe,email_societe,site_societe,matricule_societe,logo_societe,pic_societe, banque, compte, pic_pied)VALUES(@nom_societe,@responseble_societe,@adresse_societe,@tel_societe,@fax_societe,@gsm_societe,@email_societe,@site_societe,@matricule_societe,@logo_societe,@pic_societe, @banque, @compte, @pic_pied)";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@nom_societe", (object)nom_soc));
            cmd.Parameters.Add(new SqlParameter("@responseble_societe", (object)res_soc));
            cmd.Parameters.Add(new SqlParameter("@adresse_societe", (object)adr_soc));
            cmd.Parameters.Add(new SqlParameter("@tel_societe", (object)tel_soc));
            cmd.Parameters.Add(new SqlParameter("@fax_societe", (object)fax_soc));
            cmd.Parameters.Add(new SqlParameter("@gsm_societe", (object)gsm_soc));
            cmd.Parameters.Add(new SqlParameter("@email_societe", (object)email_soc));
            cmd.Parameters.Add(new SqlParameter("@site_societe", (object)site_soc));
            cmd.Parameters.Add(new SqlParameter("@matricule_societe", (object)mat_soc));
            cmd.Parameters.Add(new SqlParameter("@logo_societe", (object)imgdata));
            cmd.Parameters.Add(new SqlParameter("@banque", (object)banque));
            cmd.Parameters.Add(new SqlParameter("@compte", (object)compte));
            cmd.Parameters.Add(new SqlParameter("@pic_pied", (object)pic_pied));
            cmd.Parameters.Add(new SqlParameter("@pic_societe", (object)pic_soc));
            suivi_actions("Modification des information de la société");
            cmd.ExecuteNonQuery();
            return "true";
        }
        public string set_SocieteWithoutPicture(string nom_soc, string res_soc, string adr_soc, string tel_soc, string fax_soc, string gsm_soc, string email_soc, string site_soc, string mat_soc, string banque, string compte, string pic_pied)
        {
            function_test();
            string req = " INSERT INTO societe(nom_societe,responseble_societe,adresse_societe,tel_societe,fax_societe,gsm_societe,email_societe,site_societe,matricule_societe, banque, compte, pic_pied)VALUES(@nom_societe,@responseble_societe,@adresse_societe,@tel_societe,@fax_societe,@gsm_societe,@email_societe,@site_societe,@matricule_societe, @banque, @compte, @pic_pied)";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@nom_societe", (object)nom_soc));
            cmd.Parameters.Add(new SqlParameter("@responseble_societe", (object)res_soc));
            cmd.Parameters.Add(new SqlParameter("@adresse_societe", (object)adr_soc));
            cmd.Parameters.Add(new SqlParameter("@tel_societe", (object)tel_soc));
            cmd.Parameters.Add(new SqlParameter("@fax_societe", (object)fax_soc));
            cmd.Parameters.Add(new SqlParameter("@gsm_societe", (object)gsm_soc));
            cmd.Parameters.Add(new SqlParameter("@email_societe", (object)email_soc));
            cmd.Parameters.Add(new SqlParameter("@site_societe", (object)site_soc));
            cmd.Parameters.Add(new SqlParameter("@matricule_societe", (object)mat_soc));
            cmd.Parameters.Add(new SqlParameter("@banque", (object)banque));
            cmd.Parameters.Add(new SqlParameter("@compte", (object)compte));
            cmd.Parameters.Add(new SqlParameter("@pic_pied", (object)pic_pied));
            cmd.ExecuteNonQuery();
            suivi_actions("Modification des information de la société");
            return "true";
        }
        public string insert_pieceFromStock(string l, int q, string c, string d, int e, string f, int pua, int pv)
        {
            function_test();
            string req = "INSERT INTO piece (libelle_piece_u, quantite_piece_u, id_feur, etat, id_cde, req_piece, pua, pv) VALUES ('" + l + "','" + q + "','" + c + "', '" + d + "', '" + e + "', '" + f + "', '" + pua + "', '" + pv + "')";
            suivi_actions("ajout d'une piéce à la commande : (" + e + ")");
            return DataExcute(req);
        }
        //*********************************** UPDATE ***********************************

        // modification d'une unité
        public string update_Unite(string des, int code_unite)
        {
            function_test();
            string req = "UPDATE unite SET designation_unite =@designation_unite WHERE id_unite =@id_unite";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@designation_unite", (object)des));
            cmd.Parameters.Add(new SqlParameter("@id_unite", (object)code_unite));
            cmd.ExecuteNonQuery();
            suivi_actions("Modification d’une unité: (" + des + ")");
            return "";
        }

        public string update_etat_cde_clt(string etat2, int id_fact)
        {
            function_test();
            string req = "UPDATE facture SET etat ='" + etat2 + "' WHERE id ='" + id_fact + "'";
            suivi_actions("Validation de la commande client n°: (" + id_fact + ")");
            return DataExcute(req);
        }
        public string update_etat_cde(string etat, int id_fact)
        {
            function_test();
            string req = "UPDATE CommandeClient SET etatbl ='" + etat + "' WHERE id_commande ='" + id_fact + "'";
            suivi_actions("Validation de la commande client n°: (" + id_fact + ")");
            return DataExcute(req);
        }
        public string update__devis(string client, string id_clt, int id_devis)
        {
            function_test();
            string req = "UPDATE devis SET client ='" + client + "', id_clt ='" + id_clt + "' WHERE id ='" + id_devis + "'";

            return DataExcute(req);
        }


        // modification d'une Magasin
        public string update_Magasin(string des, int code_Magasin)
        {
            function_test();
            string req = "UPDATE empalcement SET des_emp ='" + des + "' WHERE id_emp ='" + code_Magasin + "'";
            suivi_actions("Modification d’un Magasin\\Emplacement de stock: (" + des + ")");
            return DataExcute(req);
        }


        public string update_categ_encaiss(string des, int id)
        {
            function_test();
            string req = "UPDATE categ SET des ='" + des + "' WHERE id ='" + id + "'";
            suivi_actions("Modification d'une catégorie: (" + des + ")");
            return DataExcute(req);
        }
        public string valider_cde_clt(int code, string valide)
        {
            function_test();
            string req = "UPDATE facture SET etat ='" + valide + "' WHERE id ='" + code + "'";
            suivi_actions("Validation de la commande client n°: (" + code + ")");
            return DataExcute(req);
        }
        public string valider_cde_cltPasser(int code, string valide)
        {
            function_test();
            string req = "UPDATE CommandeClient SET etatcmd =@etatcmd WHERE id_commande =@id_commande";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@id_commande", (object)code));
            cmd.Parameters.Add(new SqlParameter("@etatcmd", (object)valide));
            cmd.ExecuteNonQuery();
           
            suivi_actions("Validation de la commande client n°: (" + code + ")");
            return "";
        }
        public string Annuler_cde_clt(int code, string annul)
        {
            function_test();
            string req = "UPDATE facture SET etat =@etat WHERE id=@id";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@id", (object)code));
            cmd.Parameters.Add(new SqlParameter("@etat", (object)annul));
            cmd.ExecuteNonQuery();
            
            suivi_actions("Annulation de la commande client n°: (" + code + ")");
            return "";
        }

        public string update_info_supp(int code, string a, string b)
        {
            function_test();
            string req = "UPDATE info_supp_eq SET designation_info_supp ='" + a + "' ,contenu_info_supp ='" + b + "' WHERE id_info_supp='" + code + "'";
            suivi_actions("Modification d’une information supplimentaire : ( " + a + " ) avec le code : ( " + code + " ).");
            return DataExcute(req);
        }
        public string update_commentaire(string code, string a)
        {
            function_test();
            string req = "UPDATE equipement SET commentaire_eq ='" + a + "' WHERE Code_eq='" + code + "'";
            suivi_actions("Ajout d’une commentaire pour l’équipement : ( " + code + " ).");
            return DataExcute(req);
        }
        public string update_equipement(int code, string serie, string m, string t, string c, DateTime af, string n, string comp, DateTime mser, string prix, int mc, string cout, string stat, int famil, int SousFamil, string code_contrat)
        {
            function_test();
            string req = "UPDATE equipement SET Code_eq ='" + serie + "', Marque_eq='" + m + "', Type_eq='" + t + "',Construction_eq='" + c + "',Année_fabrication_eq='" + af + "',Numéro_immob_eq='" + n + "', Information_comp_eq='" + comp + "',Date_mise_service_eq='" + mser + "', Prix_achat_eq='" + prix + "', machine_critique_eq='" + mc + "',cout_non_production_eq='" + cout + "', Statut_eq='" + stat + "', famille_eq='" + famil + "', sfamille_eq='" + SousFamil + "', code_contrat_g='" + code_contrat + "' WHERE uid='" + code + "'";
            suivi_actions("Modification des informations pour l’équipement : ( " + serie + " ).");
            return DataExcute(req);
        }
        public string update_fin_contrat(string code, string date_deb, string date_fin)
        {
            function_test();
            string req = "UPDATE contrat_garantie SET date_debut_contrat_g ='" + date_deb + "', date_fin_contrat_g='" + date_fin + "' WHERE code_contrat_g='" + code + "'";
            suivi_actions("Modification des données concernant un contrat d’equipement. Code de contrat: ( " + code + " ).");
            return DataExcute(req);
        }
        public string nombre_intervention(string codeEq)
        {
            function_test();
            string req = "UPDATE equipement SET nbr =nbr+1 WHERE Code_eq='" + codeEq + "'";
            return DataExcute(req);
        }
        //mise à jour de fichier de contrat equipement
        public string update_contrat_fich(int code, string a, string b, byte[] c, string d, string e, string f, byte[] g)
        {
            function_test();
            try
            {
                string req = " UPDATE fichier_garentie set description= @description , image= @image , nom = @nom , type = @type, taille= @taille, extension = @extension, code_contrat_g= @code_contrat_g where id_fichier_g = @id";
                SqlCommand cmd = new SqlCommand(req, sql_gmao.conn);
                cmd.Parameters.Add(new SqlParameter("@description", (object)d));
                cmd.Parameters.Add(new SqlParameter("@image", (object)c));
                cmd.Parameters.Add(new SqlParameter("@nom", (object)b));
                cmd.Parameters.Add(new SqlParameter("@code_contrat_g", (object)a));
                cmd.Parameters.Add(new SqlParameter("@id", (object)code));
                cmd.Parameters.Add(new SqlParameter("@type", (object)e));
                cmd.Parameters.Add(new SqlParameter("@taille", (object)f));
                cmd.Parameters.Add(new SqlParameter("@extension", (object)g));
                cmd.ExecuteNonQuery();
                return "true";
            }
            catch (Exception ss)
            {
                return (ss.Message);
            }
        }
        //mise a jour contact si pas d'image
        public string update_contrat_fich2(int code, string a, string d)
        {
            function_test();
            try
            {
                string req = " UPDATE fichier_garentie set description= @description, code_contrat_g= @code_contrat_g where id_fichier_g = @id";
                SqlCommand cmd = new SqlCommand(req, sql_gmao.conn);

                cmd.Parameters.Add(new SqlParameter("@description", (object)d));
                cmd.Parameters.Add(new SqlParameter("@code_contrat_g", (object)a));
                cmd.Parameters.Add(new SqlParameter("@id", (object)code));
                cmd.ExecuteNonQuery();

                return "true";
            }
            catch (Exception ss)
            {
                return (ss.Message);
            }
        }




        public string update_contrat_fich3(string a, string b, string c, string d, Byte[] e, string f, Byte[] g, int code)
        {
            function_test();
            try
            {
                string req = " UPDATE retour set descri= @descri, client= @client, datee= @datee, commentaire= @commentaire , extension= @extension, type= @type, imagee= @imagee where id = @id";
                SqlCommand cmd = new SqlCommand(req, sql_gmao.conn);

                cmd.Parameters.Add(new SqlParameter("@id", (object)code));
                cmd.Parameters.Add(new SqlParameter("@descri", (object)a));
                cmd.Parameters.Add(new SqlParameter("@client", (object)b));
                cmd.Parameters.Add(new SqlParameter("@datee", (object)c));
                cmd.Parameters.Add(new SqlParameter("@commentaire", (object)d));
                cmd.Parameters.Add(new SqlParameter("@extension", (object)e));
                cmd.Parameters.Add(new SqlParameter("@type", (object)f));
                cmd.Parameters.Add(new SqlParameter("@imagee", (object)g));
                cmd.ExecuteNonQuery();
                suivi_actions("Mise à jour de retour client n° " + code);
                return "true";


            }
            catch (Exception ss)
            {
                return (ss.Message);
            }
        }



        public string update_contrat_fich32(string a, string b, string c, string d, Byte[] e, string f, Byte[] g, int code)
        {
            function_test();
            try
            {
                string req = " UPDATE retour2 set descri= @descri, client= @client, datee= @datee, commentaire= @commentaire , extension= @extension, type= @type, imagee= @imagee where id = @id";
                SqlCommand cmd = new SqlCommand(req, sql_gmao.conn);

                cmd.Parameters.Add(new SqlParameter("@id", (object)code));
                cmd.Parameters.Add(new SqlParameter("@descri", (object)a));
                cmd.Parameters.Add(new SqlParameter("@client", (object)b));
                cmd.Parameters.Add(new SqlParameter("@datee", (object)c));
                cmd.Parameters.Add(new SqlParameter("@commentaire", (object)d));
                cmd.Parameters.Add(new SqlParameter("@extension", (object)e));
                cmd.Parameters.Add(new SqlParameter("@type", (object)f));
                cmd.Parameters.Add(new SqlParameter("@imagee", (object)g));
                cmd.ExecuteNonQuery();
                suivi_actions("Mise à jour de Commande client reçu n° " + code);
                return "true";


            }
            catch (Exception ss)
            {
                return (ss.Message);
            }
        }


        public string update_contrat_fich33(string a, string b, string c, string d, Byte[] e, string f, Byte[] g, int code)
        {
            function_test();
            try
            {
                string req = " UPDATE retour3 set descri= @descri, client= @client, datee= @datee, commentaire= @commentaire , extension= @extension, type= @type, imagee= @imagee where id = @id";
                SqlCommand cmd = new SqlCommand(req, sql_gmao.conn);

                cmd.Parameters.Add(new SqlParameter("@id", (object)code));
                cmd.Parameters.Add(new SqlParameter("@descri", (object)a));
                cmd.Parameters.Add(new SqlParameter("@client", (object)b));
                cmd.Parameters.Add(new SqlParameter("@datee", (object)c));
                cmd.Parameters.Add(new SqlParameter("@commentaire", (object)d));
                cmd.Parameters.Add(new SqlParameter("@extension", (object)e));
                cmd.Parameters.Add(new SqlParameter("@type", (object)f));
                cmd.Parameters.Add(new SqlParameter("@imagee", (object)g));
                cmd.ExecuteNonQuery();
                suivi_actions("Mise à jour de Commande Fournisseur reçu n° " + code);
                return "true";


            }
            catch (Exception ss)
            {
                return (ss.Message);
            }
        }


        public string update_encaiss2(DateTime datee, string design, string categ, string montant, string comment, string clt, Byte[] ext, string chemin, Byte[] image, int code)
        {
            function_test();
            try
            {
                string req = " UPDATE conta set datee= @datee, design= @design, categ= @categ, montant= @montant, comment= @comment, clt= @clt, extension= @extension, type= @type, imagee= @imagee where id = @code";
                SqlCommand cmd = new SqlCommand(req, sql_gmao.conn);

                cmd.Parameters.Add(new SqlParameter("@code", (object)code));
                cmd.Parameters.Add(new SqlParameter("@datee", (object)datee));
                cmd.Parameters.Add(new SqlParameter("@design", (object)design));
                cmd.Parameters.Add(new SqlParameter("@categ", (object)categ));
                cmd.Parameters.Add(new SqlParameter("@montant", (object)montant));
                cmd.Parameters.Add(new SqlParameter("@comment", (object)comment));
                cmd.Parameters.Add(new SqlParameter("@clt", (object)clt));
                cmd.Parameters.Add(new SqlParameter("@extension", (object)ext));
                cmd.Parameters.Add(new SqlParameter("@type", (object)chemin));
                cmd.Parameters.Add(new SqlParameter("@imagee", (object)image));
                cmd.ExecuteNonQuery();
                suivi_actions("Mise à jour de l'encaissement n° " + code);
                return "true";


            }
            catch (Exception ss)
            {
                return (ss.Message);
            }
        }





        public string update_decaiss2(DateTime datee, string design, string categ, string montant, string comment, string feur, Byte[] ext, string chemin, Byte[] image, int code)
        {
            function_test();
            try
            {
                string req = " UPDATE conta set datee= @datee, design= @design, categ= @categ, montant= @montant, comment= @comment, feur= @feur, extension= @extension, type= @type, imagee= @imagee where id = @code";
                SqlCommand cmd = new SqlCommand(req, sql_gmao.conn);

                cmd.Parameters.Add(new SqlParameter("@code", (object)code));
                cmd.Parameters.Add(new SqlParameter("@datee", (object)datee));
                cmd.Parameters.Add(new SqlParameter("@design", (object)design));
                cmd.Parameters.Add(new SqlParameter("@categ", (object)categ));
                cmd.Parameters.Add(new SqlParameter("@montant", (object)montant));
                cmd.Parameters.Add(new SqlParameter("@comment", (object)comment));
                cmd.Parameters.Add(new SqlParameter("@feur", (object)feur));
                cmd.Parameters.Add(new SqlParameter("@extension", (object)ext));
                cmd.Parameters.Add(new SqlParameter("@type", (object)chemin));
                cmd.Parameters.Add(new SqlParameter("@imagee", (object)image));
                cmd.ExecuteNonQuery();
                suivi_actions("Mise à jour de décaissement n° " + code);
                return "true";


            }
            catch (Exception ss)
            {
                return (ss.Message);
            }
        }

        public string update_contrat_ficheeeee(string a, string b, string c, string d, int code)
        {
            function_test();
            try
            {
                string req = " UPDATE retour set descri= @descri, client= @client, datee= @datee, commentaire= @commentaire where id = @id";
                SqlCommand cmd = new SqlCommand(req, sql_gmao.conn);

                cmd.Parameters.Add(new SqlParameter("@id", (object)code));
                cmd.Parameters.Add(new SqlParameter("@descri", (object)a));
                cmd.Parameters.Add(new SqlParameter("@client", (object)b));
                cmd.Parameters.Add(new SqlParameter("@datee", (object)c));
                cmd.Parameters.Add(new SqlParameter("@commentaire", (object)d));

                cmd.ExecuteNonQuery();
                suivi_actions("Mise à jour de retour client n° " + code);


                return "true";
            }
            catch (Exception ss)
            {
                return (ss.Message);
            }
        }



        public string update_contrat_ficheeeee2(string a, string b, string c, string d, int code)
        {
            function_test();
            try
            {
                string req = " UPDATE retour2 set descri= @descri, client= @client, datee= @datee, commentaire= @commentaire where id = @id";
                SqlCommand cmd = new SqlCommand(req, sql_gmao.conn);

                cmd.Parameters.Add(new SqlParameter("@id", (object)code));
                cmd.Parameters.Add(new SqlParameter("@descri", (object)a));
                cmd.Parameters.Add(new SqlParameter("@client", (object)b));
                cmd.Parameters.Add(new SqlParameter("@datee", (object)c));
                cmd.Parameters.Add(new SqlParameter("@commentaire", (object)d));

                cmd.ExecuteNonQuery();
                suivi_actions("Mise à jour de Commande client reçu n° " + code);


                return "true";
            }
            catch (Exception ss)
            {
                return (ss.Message);
            }
        }

        public string update_contrat_ficheeeee3(string a, string b, string c, string d, int code)
        {
            function_test();
            try
            {
                string req = " UPDATE retour3 set descri= @descri, client= @client, datee= @datee, commentaire= @commentaire where id = @id";
                SqlCommand cmd = new SqlCommand(req, sql_gmao.conn);

                cmd.Parameters.Add(new SqlParameter("@id", (object)code));
                cmd.Parameters.Add(new SqlParameter("@descri", (object)a));
                cmd.Parameters.Add(new SqlParameter("@client", (object)b));
                cmd.Parameters.Add(new SqlParameter("@datee", (object)c));
                cmd.Parameters.Add(new SqlParameter("@commentaire", (object)d));

                cmd.ExecuteNonQuery();
                suivi_actions("Mise à jour de Commande Fournisseur reçu n° " + code);


                return "true";
            }
            catch (Exception ss)
            {
                return (ss.Message);
            }
        }

        public string update_encaiss(DateTime datee, string design, string categ, string montant, string comment, string clt, int code)
        {
            function_test();
            try
            {
                string req = " UPDATE conta set datee= @datee, design= @design, categ= @categ, montant= @montant, comment= @comment, clt= @clt where id = @code";
                SqlCommand cmd = new SqlCommand(req, sql_gmao.conn);

                cmd.Parameters.Add(new SqlParameter("@code", (object)code));
                cmd.Parameters.Add(new SqlParameter("@datee", (object)datee));
                cmd.Parameters.Add(new SqlParameter("@design", (object)design));
                cmd.Parameters.Add(new SqlParameter("@categ", (object)categ));
                cmd.Parameters.Add(new SqlParameter("@montant", (object)montant));
                cmd.Parameters.Add(new SqlParameter("@comment", (object)comment));
                cmd.Parameters.Add(new SqlParameter("@clt", (object)clt));


                cmd.ExecuteNonQuery();
                suivi_actions("Mise à jour de l'encaissement n°" + code);


                return "true";
            }
            catch (Exception ss)
            {
                return (ss.Message);
            }
        }



        public string update_decaiss(DateTime datee, string design, string categ, string montant, string comment, string feur, int code)
        {
            function_test();
            try
            {
                string req = " UPDATE conta set datee= @datee, design= @design, categ= @categ, montant= @montant, comment= @comment, feur= @feur where id = @code";
                SqlCommand cmd = new SqlCommand(req, sql_gmao.conn);

                cmd.Parameters.Add(new SqlParameter("@code", (object)code));
                cmd.Parameters.Add(new SqlParameter("@datee", (object)datee));
                cmd.Parameters.Add(new SqlParameter("@design", (object)design));
                cmd.Parameters.Add(new SqlParameter("@categ", (object)categ));
                cmd.Parameters.Add(new SqlParameter("@montant", (object)montant));
                cmd.Parameters.Add(new SqlParameter("@comment", (object)comment));
                cmd.Parameters.Add(new SqlParameter("@feur", (object)feur));


                cmd.ExecuteNonQuery();
                suivi_actions("Mise à jour de décaissement n°" + code);


                return "true";
            }
            catch (Exception ss)
            {
                return (ss.Message);
            }
        }



        public string update_piece_utilisee(int id_p, string code_p, string des_p, string qu_p)
        {
            function_test();
            string req = "UPDATE piece SET code_piece_u ='" + code_p + "', libelle_piece_u = '" + des_p + "', quantite_piece_u = '" + qu_p + "' WHERE id_piece='" + id_p + "'";
            suivi_actions("Modification de piece : ( " + des_p + " ) code : ( " + code_p + " ).");
            return DataExcute(req);
        }
        public string update_piece_etat(int code, string a, int b)
        {
            function_test();
            string req = "UPDATE piece SET etat2 ='" + a + "', id_cde='" + b + "' WHERE id_piece ='" + code + "'";
            suivi_actions("Modification de l’etat de commande interne");
            return DataExcute(req);
        }
        public string update_piece_etat2(int code, int a)
        {

            if (sql_gmao.conn.State.ToString().Equals("Closed"))
            {
                sql_gmao.conn.Open();
            }
            function_test();
            string req = "UPDATE piece SET quantite_piece_u ='" + a + "' WHERE id_piece ='" + code + "'";
            suivi_actions("Modification de la quantité de piéce commandée.");
            return DataExcute(req);
        }
        //suppression d'une unité
        public string delete_Unite(int code)
        {
            function_test();
            string req = "DELETE FROM unite WHERE id_unite=@id_unite";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@id_unite", (object)code));
            cmd.ExecuteNonQuery();
            suivi_actions("Suppression d’une unité n°" + code);
            return "";
        }
        public string delete_aliment(int code)
        {
            function_test();
            string req = "DELETE FROM alimenter WHERE id='" + code + "'";
            suivi_actions("Suppression de l'historique d'alimentation de stock: ligne " + code);
            return DataExcute(req);
        }
        public string delete_sortie(int code)
        {
            function_test();
            string req = "DELETE FROM delimenter WHERE id='" + code + "'";
            suivi_actions("Suppression de l'historique de sortie de stock: ligne " + code);
            return DataExcute(req);
        }
        public string delete_piececmd(int code)
        {
            function_test();
            string req = "DELETE FROM piece_commande WHERE id_piece='" + code + "'";

            return DataExcute(req);
        }

        public string delete_piecebl(int code)
        {
            function_test();
            string req = "DELETE FROM piece_bl WHERE id_piece='" + code + "'";

            return DataExcute(req);
        }
        public string delete_piecefacture(int code)
        {
            function_test();
            string req = "DELETE FROM piece_fact WHERE id_fact='" + code + "'";

            return DataExcute(req);
        }
        public string annulerbl(string codebl)
        {
            function_test();
            string req = "DELETE FROM piece_bl WHERE id_commande='" + codebl + "'";

            return DataExcute(req);
        }

        //suppression d'une Magasin
        public string delete_Magasin(int code)
        {
            function_test();
            string req = "DELETE FROM empalcement WHERE id_emp='" + code + "'";
            suivi_actions("Suppression d’un magasin\\emplacement de stock");
            return DataExcute(req);
        }


        public string delete_categ_encaiss(int code)
        {
            function_test();
            string req = "DELETE FROM categ WHERE id='" + code + "'";
            suivi_actions("Suppression d’une catégorie d'encaissement");
            return DataExcute(req);
        }
        public string update_piece_after_delete(int code, string a)
        {
            function_test();
            string req = "UPDATE piece SET etat2 ='" + a + "' WHERE id_piece ='" + code + "'";
            return DataExcute(req);
        }
        public string update_piece_after_delete_cde(int code, string a)
        {
            function_test();
            string req = "UPDATE piece SET etat2 ='" + a + "' WHERE id_cde ='" + code + "'";
            suivi_actions("Modification de la quantité de piéce suite à la suppression d’une commande.");
            return DataExcute(req);
        }
        public string update_piece(int id, int q, string l)
        {
            function_test();
            string req = "UPDATE piece SET quantite_piece_u ='" + q + "' WHERE UniqueID='" + id + "' AND libelle_piece_u = '" + l + "'";
            suivi_actions("Modification de la quantité de piéce ( " + l + " ).");
            return DataExcute(req);
        }
        public string update_piece_etat_FromStock(string code, string a, int b)
        {
            function_test();
            string req = "UPDATE piece SET etat2 ='" + a + "', id_cde='" + b + "' WHERE id_piece ='" + code + "'";
            return DataExcute(req);
        }
        public string update_piece_qte(string codep, int code, int a, double montant)
        {
            function_test();
            string req = "UPDATE piece_fact SET quantite_piece_u =" + a + ", pv=" + montant + " WHERE id_fact =" + code + " and code_piece_u='" + codep + "'";
            return DataExcute(req);
        }
        public string update_qte_piece(int code, int quantite, string piece_up)
        {
            function_test();
            string req = "UPDATE piece SET quantite_piece_u ='" + quantite + "' WHERE UniqueID='" + code + "' AND libelle_piece_u = '" + piece_up + "'";
            return DataExcute(req);
        }
        public string update_qte_piece2(int a, string feur, string lib)
        {
            function_test();
            string req = "UPDATE piece SET quantite_piece_u +='" + a + "' WHERE id_feur= '" + feur + "' AND libelle_piece_u = '" + lib + "'";
            return DataExcute(req);
        }

        public string update_puv_piece2(int a, string feur, string lib)
        {
            function_test();
            string req = "UPDATE piece SET pv +='" + a + "' WHERE id_feur= '" + feur + "' AND libelle_piece_u = '" + lib + "'";
            return DataExcute(req);
        }

        public string update_qte_piece_commande(string lib, string quantit, int idpiece, string remise, string pv, string tva, string puv)
        {
            function_test();
            string req = "UPDATE piece_commande SET libelle_piece ='" + lib + "',quantite_piece='" + quantit + "', totvente ='" + pv + "',remise='" + remise + "',ttva='" + tva + "',puv='" + puv + "' WHERE id_piece= '" + idpiece + "'";
            suivi_actions("Ajout piéce : ( " + lib + " ) à la  commande client n° " + idpiece);

            return DataExcute(req);
        }
        public string update_qterestcommande(string qteres, string codep)
        {
            function_test();
            string req = "UPDATE piece_commande SET qterest =@qterest where id_piece=@id_piece";

            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@qterest", (object)qteres));
            cmd.Parameters.Add(new SqlParameter("@id_piece", (object)codep));
            cmd.ExecuteNonQuery();
            return "";
        }
        public string update_qterestcommande2(string qteres, string codep,int id_cmd)
        {
            function_test();
            string req = "UPDATE piece_commande SET qterest =@qterest where code_art=@code_art and id_commande=@id_commande";

            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@qterest", (object)qteres));
            cmd.Parameters.Add(new SqlParameter("@code_art", (object)codep));
            cmd.Parameters.Add(new SqlParameter("@id_commande", (object)id_cmd));
            cmd.ExecuteNonQuery();
            return "";
        }
        public string update_qte_piece_bl(string lib, string quantit, int idpiece, string remise, string pv, string tva, string puv)
        {
            function_test();
            string req = "UPDATE piece_bl SET libelle_piece ='" + lib + "',quantite_piece='" + quantit + "', totvente ='" + pv + "',remise='" + remise + "',ttva='" + tva + "',puv='" + puv + "' WHERE id_piece= '" + idpiece + "'";
            suivi_actions("Ajout piéce : ( " + lib + " ) à la  commande client n° " + idpiece);

            return DataExcute(req);
        }

        public string update_piece_bl2(string lib, string quantit, string code_art, string remise, string pv, string tva, string puv,int id_bl)
        {
            function_test();
            string req = "UPDATE piece_bl SET libelle_piece ='" + lib + "',quantite_piece='" + quantit + "', totvente ='" + pv + "',remise='" + remise + "',ttva='" + tva + "',puv='" + puv + "' WHERE code_art= '" + code_art + "' and id_commande='"+id_bl+"'";
            suivi_actions("update piéce : ( " + lib + " ) à la  commande client n° " + code_art);

            return DataExcute(req);
        }
        public string update_qte_piece_fact(Double a, string client, string lib, Double pv)
        {
            function_test();
            string req = "UPDATE piece_fact SET quantite_piece_u +='" + a + "', pv ='" + pv + "' WHERE id_clt= '" + client + "' AND libelle_piece_u = '" + lib + "'";
            suivi_actions("Ajout piéce : ( " + lib + " ) à la  commande client n° " + a);

            return DataExcute(req);
        }

        public string update_qte_piece_fact2(int a, int client, string code)
        {
            function_test();
            string req = "UPDATE piece_fact SET quantite_piece_u +='" + a + "' WHERE id_clt= '" + client + "' AND code_piece_u = '" + code + "'";
            return DataExcute(req);
        }
        public string update_piece_etatFromStock(string code, string a, int b)
        {
            function_test();
            string req = "UPDATE piece SET etat2 ='" + a + "', id_cde='" + b + "' WHERE id_piece ='" + code + "'";
            return DataExcute(req);
        }
        public string update_intervention(string titre, string des, string eta, string cat, string datint, string hd, string hf, string tt, string cint, string cext, string code_inter)
        {
            function_test();
            string req = "UPDATE intervention SET titre_intervention ='" + titre + "' ,description_intervention ='" + des + "',etat_intervention ='" + eta + "',categorie_intervention ='" + cat + "',date_intervention ='" + datint + "',heure_debut_intervention ='" + hd + "' ,heure_fin_intervention ='" + hf + "',temps_total_intervention ='" + tt + "',code_intervenant ='" + cint + "',code_soustraitant ='" + cext + "' WHERE code_intervention ='" + code_inter + "'";
            suivi_actions("Modification de l’intervention : ( " + titre + " ). Code intervention : ( " + code_inter + " ).");
            return DataExcute(req);
        }
        public string resetcmdclt()
        {
            function_test();
            string req = "ALTER TABLE CommandeClient AUTO_INCREMENT = 1 ";

            return DataExcute(req);
        }

        public string update_etat_fact(string etat, int code, string date_env, string ht, string ttc, string tva, string remise, string timbre, string dev_fac, string fretcab, string instmisajour)
        {
            function_test();
            string req = "UPDATE facture SET etat_fact ='" + etat + "', date_envoie='" + date_env + "', montant_ht ='" + ht + "', montant_ttc='" + ttc + "',tva ='" + tva + "', remise='" + remise + "' , timbre='" + timbre + "' , dev_fac='" + dev_fac + "', fournitureetcable ='" + fretcab + "',installetmiseajour ='" + instmisajour + "' WHERE id ='" + code + "'";
            suivi_actions("Facture n°: ( " + code + " )  marquée comme envoyée.");
            return DataExcute(req);
        }
        public string update_etat_commande(string etat, int code, string date_env, string ht, string ttc, string tva, string remise, string timbre, string dev_fac)
        {
            function_test();
            string req = "UPDATE facture SET etat_fact ='" + etat + "', date_envoie='" + date_env + "', montant_ht ='" + ht + "', montant_ttc='" + ttc + "',tva ='" + tva + "', remise='" + remise + "' , timbre='" + timbre + "' , dev_fac='" + dev_fac + "', WHERE id ='" + code + "'";
            suivi_actions("Facture n°: ( " + code + " )  marquée comme envoyée.");
            return DataExcute(req);
        }
        public string update_etat_commandeclt1(string etat, int code, string date_env, string ht, string ttc, string tva, string remise, string timbre)
        {
            function_test();

            string req = "UPDATE CommandeClient SET etatbl ='" + etat + "', montant_ht ='" + ht + "', montant_ttc='" + ttc + "',tva ='" + tva + "', remise='" + remise + "' , timbre='" + timbre + "' WHERE id_commande ='" + code + "'";
            suivi_actions("commande n°: ( " + code + " )  modifié.");
            return DataExcute(req);

        }
        public string update_comandeclient(string ttc, string code)
        {
            function_test();

            string req = "UPDATE CommandeClient SET  montant_ttc=@montant_ttc WHERE id_commande =@id_commande";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@montant_ttc", (object)ttc));
            cmd.Parameters.Add(new SqlParameter("@id_commande", (object)code));
            cmd.ExecuteNonQuery();
            
            suivi_actions("commande n°: ( " + code + " )  modifié.");
            return "";

        }
        public string update_etat_bonlivraison(string etat, int code)
        {
            function_test();

            string req = "UPDATE bon_livraison SET etat =@etat WHERE id =@id";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@etat", (object)etat));
            cmd.Parameters.Add(new SqlParameter("@id", (object)code));
            cmd.ExecuteNonQuery();
            suivi_actions("Bl n°: ( " + code + " )  marquée comme envoyée.");
            return "";
        }
        public string update_bonlivraison(string etat, int code, string date_env, string ht, string ttc, string tva, string remise, string timbre)
        {
            function_test();

            string req = "UPDATE bon_livraison SET etat ='" + etat + "', montant_ht ='" + ht + "', montant_ttc='" + ttc + "',tva ='" + tva + "', remise='" + remise + "' , timbre='" + timbre + "' WHERE id ='" + code + "'";
            suivi_actions("Bl n°: ( " + code + " )  marquée comme envoyée.");
            return DataExcute(req);

        }
        public string update_bonlivraison2(int code, string ht, string ttc, string timbre, string mode_livraison, string moyen_livraison, string lieu_livraison, string nbcmd, string id_clt, string client, string date_ajout)
        {
            function_test();

            string req = "UPDATE bon_livraison SET date_ajout=@date_ajout, client=@client, id_clt=@id_clt, nbcmd=@nbcmd, mode_livraison=@mode_livraison,moyen_livraison=@moyen_livraison" +
                ",lieu_livraison=@lieu_livraison,  montant_ht =@montant_ht, montant_ttc=@montant_ttc , timbre=@timbre WHERE id =@id";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@client", (object)client));
            cmd.Parameters.Add(new SqlParameter("@id_clt", (object)id_clt));
            cmd.Parameters.Add(new SqlParameter("@nbcmd", (object)nbcmd));
            cmd.Parameters.Add(new SqlParameter("@mode_livraison", (object)mode_livraison));
            cmd.Parameters.Add(new SqlParameter("@moyen_livraison", (object)moyen_livraison));
            cmd.Parameters.Add(new SqlParameter("@lieu_livraison", (object)lieu_livraison));
            cmd.Parameters.Add(new SqlParameter("@montant_ht", (object)ht));
            cmd.Parameters.Add(new SqlParameter("@montant_ttc", (object)ttc));
            cmd.Parameters.Add(new SqlParameter("@timbre", (object)timbre));
            cmd.Parameters.Add(new SqlParameter("@date_ajout", (object)date_ajout));
            cmd.Parameters.Add(new SqlParameter("@id", (object)code));
            

            cmd.ExecuteNonQuery();
            suivi_actions("Bl n°: ( " + code + " )  est modifier");
            return "";

        }
        public string update_bonlivraison3(int code, string ht, string ttc, string timbre, string mode_livraison, string moyen_livraison, string lieu_livraison, string date_ajout)
        {
            function_test();

            string req = "UPDATE bon_livraison SET date_ajout=@date_ajout, mode_livraison=@mode_livraison,moyen_livraison=@moyen_livraison,lieu_livraison=@lieu_livraison,  montant_ht =@montant_ht, montant_ttc=@montant_ttc , timbre=@timbre WHERE id =@id";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@mode_livraison", (object)mode_livraison));
            cmd.Parameters.Add(new SqlParameter("@moyen_livraison", (object)moyen_livraison));
            cmd.Parameters.Add(new SqlParameter("@lieu_livraison", (object)lieu_livraison));
            cmd.Parameters.Add(new SqlParameter("@montant_ht", (object)ht));
            cmd.Parameters.Add(new SqlParameter("@montant_ttc", (object)ttc));
            cmd.Parameters.Add(new SqlParameter("@timbre", (object)timbre));
            cmd.Parameters.Add(new SqlParameter("@date_ajout", (object)date_ajout));
            cmd.Parameters.Add(new SqlParameter("@id", (object)code));
            cmd.ExecuteNonQuery();
            suivi_actions("Bl n°: ( " + code + " )  est modifier.");
            return "";

        }
        public string update_etat(string etat, string code)
        {
            function_test();

            string req = "UPDATE CommandeClient SET etatbl =@etatbl where id_commande =@id_commande";

            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@etatbl", (object)etat));
            cmd.Parameters.Add(new SqlParameter("@id_commande", (object)code));
            cmd.ExecuteNonQuery();
            
            suivi_actions("Bl n°: ( " + code + " )  marquée comme "+etat+".");
            return "";

        }
        public string update_etatcmd(string etat, string code)
        {
            function_test();

            string req = "UPDATE CommandeClient SET etatcmd =@etatcmd where id_commande =@id_commande";

            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@etatcmd", (object)etat));
            cmd.Parameters.Add(new SqlParameter("@id_commande", (object)code));
            cmd.ExecuteNonQuery();
           
            suivi_actions("commande client n°: ( " + code + " )  marquée comme "+etat+".");
            return "";

        }
        public string update_etatcmdfr(string etat, string code)
        {
            function_test();

            string req = "UPDATE Commandefr SET etatcmd =@etatcmd where id_commande =@id_commande";

            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@etatcmd", (object)etat));
            cmd.Parameters.Add(new SqlParameter("@id_commande", (object)code));
            cmd.ExecuteNonQuery();
           
            suivi_actions("Commande fr n°: ( " + code + " )  recu.");
            return "";

        }
        public string update_clt_cmd_fr(int id_cmd, string id_clt, string ref_cmd, string ref_pf, string mode_payement, string contact, string mode_liv, string delai_livraison, string delai_payement)
        {
            function_test();

            string req = "UPDATE Commandefr SET id_clt ='" + id_clt + "',ref_cmd='" + ref_cmd + "',ref_pf='" + ref_pf + "',mode_payement='" + mode_payement + "',contact='" + contact + "',mode_liv='" + mode_liv + "',delai_livraison='" + delai_livraison + "',delai_payement='" + delai_payement + "' where id_commande ='" + id_cmd + "'";
            suivi_actions("Commande fr n°: ( " + id_cmd + " )  recu.");
            return DataExcute(req);

        }
        //public string update_etat_devis(string etat, int code, string date_env, string ht, string ttc, string tva, string remise, string timbre, string dev)
        //{
        //    function_test();
        //    string req = "UPDATE devis SET etat ='" + etat + "', date_envoie='" + date_env + "', montant_ht ='" + ht + "', montant_ttc='" + ttc + "',tva ='" + tva + "', remise='" + remise + "', timbre='" + timbre + "',  dev='" + dev + "' WHERE id ='" + code + "'";
        //    suivi_actions("Devis n°: ( " + code + " )  marqué comme envoyé.");
        //    return DataExcute(req);
        //}

        public string update_etat_liv(string etat, int code, string date_env, string ht, string ttc, string tva, string remise, string timbre, string dev_liv)
        {
            function_test();
            string req = "UPDATE facture SET etat_bon_liv ='" + etat + "', date_envoie_bl='" + date_env + "' , montant_ht ='" + ht + "', montant_ttc='" + ttc + "',tva ='" + tva + "', remise='" + remise + "', timbre='" + timbre + "', dev_liv='" + dev_liv + "' WHERE id ='" + code + "'";
            suivi_actions("Bon de livraison n°: ( " + code + " )  marqué comme envoyé.");
            return DataExcute(req);
        }

        public string update_etat_bon_sortie(string etat, int code, string date_env)
        {
            function_test();
            string req = "UPDATE facture SET etat_bon_sortie ='" + etat + "', date_val_bon_sortie='" + date_env + "' WHERE id ='" + code + "'";
            suivi_actions("Bon de Sortie n°: ( " + code + " )  marqué comme Validé.");
            return DataExcute(req);
        }

        public string update_comm_bon_sortie(string comm, int code)
        {
            function_test();
            string req = "UPDATE facture SET comm_bon_sortie ='" + comm + "' WHERE id ='" + code + "'";
            suivi_actions("Bon de Sortie n°: ( " + code + " )  Commenté.");
            return DataExcute(req);
        }

        public string update_interventioncloture(string titre, string des, string eta, string cat, string datint, string hd, string hf, string tt, string cint, string cext, string code_inter, string datefincloture)
        {
            function_test();
            string req = "UPDATE intervention SET titre_intervention ='" + titre + "' ,description_intervention ='" + des + "',etat_intervention ='" + eta + "',categorie_intervention ='" + cat + "',date_intervention ='" + datint + "',heure_debut_intervention ='" + hd + "' ,heure_fin_intervention ='" + hf + "',temps_total_intervention ='" + tt + "',code_intervenant ='" + cint + "',code_soustraitant ='" + cext + "',date_fin_intervention ='" + datefincloture + "' WHERE code_intervention ='" + code_inter + "'";
            suivi_actions("Modification de l’intervention : ( " + titre + " ). Code intervention : ( " + code_inter + " ).");
            return DataExcute(req);
        }
        public string update_compteur(string des, string unite, string lisup, string valact, string datRe, int code_com)
        {
            function_test();
            string req = "UPDATE compteur SET designation_compteur ='" + des + "' ,unite_compteur ='" + unite + "',limite_supp_compteur ='" + lisup + "',valeur_compteur ='" + valact + "',date_releve ='" + datRe + "' WHERE id_compteur ='" + code_com + "'";
            suivi_actions("Modification de la compteur : ( " + des + " ).");
            return DataExcute(req);
        }
        public string update_FamilleEquipement(string des, int code_fe)
        {
            function_test();
            string req = "UPDATE Famille_equipement SET designation_fe =@designation_fe WHERE code_fe =@code_fe";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@designation_fe", (object)des));
            cmd.Parameters.Add(new SqlParameter("@code_fe", (object)code_fe));
            cmd.ExecuteNonQuery();
            suivi_actions("Modification de famille : ( " + des + " ).");
            return "";
        }
        public string update_SousFamilleEquipement(string des, int code_sfe)
        {
            function_test();
            string req = "UPDATE SousFamille_eq SET designation_sfe ='" + des + "' WHERE code_sfe ='" + code_sfe + "'";
            suivi_actions("Modification de sous famille : ( " + des + " ).");
            return DataExcute(req);
        }
        public string update_Feur(string raison, string respon, string gsm, string tel, string fax, string adr, string cp, string ville, string email, string site, string tva, string matrFisc, string modPay, string code,int id)
        {
            function_test();
            string req = "UPDATE fournisseur SET code_feur=@code_feur, raison_soc =@raison_soc ,responsbale =@responsbale ,gsm_feur =@gsm_feur ,tel_feur =@tel_feur ,"+
                "fax_feur =@fax_feur ,adresse_feur =@adresse_feur ,cp_feur =@cp_feur ,ville_feur =@ville_feur ,email_feur =@email_feur ,site_feur =@site_feur ,tva_feur =@tva_feur"+
                ",forme_juriduque =@forme_juriduque,mode_pay =@mode_pay WHERE id =@id";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@code_feur", (object)code));
            cmd.Parameters.Add(new SqlParameter("@raison_soc", (object)raison));
            cmd.Parameters.Add(new SqlParameter("@responsbale", (object)respon));
            cmd.Parameters.Add(new SqlParameter("@gsm_feur", (object)gsm));
            cmd.Parameters.Add(new SqlParameter("@tel_feur", (object)tel));
            cmd.Parameters.Add(new SqlParameter("@fax_feur", (object)fax));
            cmd.Parameters.Add(new SqlParameter("@adresse_feur", (object)adr));
            cmd.Parameters.Add(new SqlParameter("@cp_feur", (object)cp));
            cmd.Parameters.Add(new SqlParameter("@ville_feur", (object)ville));
            cmd.Parameters.Add(new SqlParameter("@email_feur", (object)email));
            cmd.Parameters.Add(new SqlParameter("@site_feur", (object)site));
            cmd.Parameters.Add(new SqlParameter("@tva_feur", (object)tva));
            cmd.Parameters.Add(new SqlParameter("@forme_juriduque", (object)matrFisc));
            cmd.Parameters.Add(new SqlParameter("@mode_pay", (object)modPay));
            cmd.Parameters.Add(new SqlParameter("@id", (object)id));
            cmd.ExecuteNonQuery();
            suivi_actions("Modification des infos le fournisseur : ( " + raison + " ).");
            return "";
        }


        public string update_clt(string raison, string respon, string gsm, string tel, string fax, string adr, string cp, string ville, string email, string site, string tva, string matrFisc, string modPay, string code,string id)
        {
            function_test();
            string req = "UPDATE client SET code=@code, raison_soc =@raison_soc,responsbale =@responsbale,gsm_clt =@gsm_clt,tel_clt =@tel_clt"+
                ",fax_clt =@fax_clt,adresse_clt =@adresse_clt,cp_clt =@cp_clt,ville_clt =@ville_clt,email_clt =@email_clt,site_clt =@site_clt,tva_clt =@tva_clt,forme_juriduque =@forme_juriduque,mode_pay =@mode_pay WHERE code_clt =@code_clt";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@code", (object)code));
            cmd.Parameters.Add(new SqlParameter("@raison_soc", (object)raison));
            cmd.Parameters.Add(new SqlParameter("@responsbale", (object)respon));
            cmd.Parameters.Add(new SqlParameter("@gsm_clt", (object)gsm));
            cmd.Parameters.Add(new SqlParameter("@tel_clt", (object)tel));
            cmd.Parameters.Add(new SqlParameter("@fax_clt", (object)fax));
            cmd.Parameters.Add(new SqlParameter("@adresse_clt", (object)adr));
            cmd.Parameters.Add(new SqlParameter("@cp_clt", (object)cp));
            cmd.Parameters.Add(new SqlParameter("@ville_clt", (object)ville));
            cmd.Parameters.Add(new SqlParameter("@email_clt", (object)email));
            cmd.Parameters.Add(new SqlParameter("@site_clt", (object)site));
            cmd.Parameters.Add(new SqlParameter("@tva_clt", (object)tva));
            cmd.Parameters.Add(new SqlParameter("@forme_juriduque", (object)matrFisc));
            cmd.Parameters.Add(new SqlParameter("@mode_pay", (object)modPay));
            cmd.Parameters.Add(new SqlParameter("@code_clt", (object)id));
            cmd.ExecuteNonQuery();
            suivi_actions("Modification des information pour le client: ( " + raison + " ).");
            return DataExcute(req);
        }

        public string update_Stock(string code, string des, string unite, string qte, string qte_reelle, string seuil, string nature, string pua, string puv, string code_fr, string puv_rev)
        {
            function_test();
            DateTime now = DateTime.Now;
            string req = "UPDATE stock SET libelle_piece ='" + des + "' ,unite_piece ='" + unite + "' ,quantite_piece ='" + qte + "' ,quantite_reelle ='" + qte_reelle + "' ,seuil_piece ='" + seuil + "' ,nature ='" + nature + "',pua ='" + pua + "' ,puv ='" + puv + "' ,code_feur ='" + code_fr + "' ,puv_rev ='" + puv_rev + "' WHERE code_piece= '" + code + "'";
            suivi_actions("Modification des données de la piéce : ( " + des + " )");
            return DataExcute(req);
        }
        public string update_Stock2(string code, string des, string unite, string qte, string qte_reelle, string seuil, string nature, string pua, string puv, string code_fr, string puv_rev,int id)
        {
            function_test();
            DateTime now = DateTime.Now;
            string req = "UPDATE stock SET code_piece=@code_piece, libelle_piece =@libelle_piece,unite_piece =@unite_piece,quantite_piece =@quantite_piece,quantite_reelle =@quantite_reelle,seuil_piece =@seuil_piece,nature =@nature,pua =@pua,puv =@puv,code_feur =@code_feur,puv_rev =@puv_rev WHERE id=@id";

            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@code_piece", (object)code));
            cmd.Parameters.Add(new SqlParameter("@libelle_piece", (object)des));
            cmd.Parameters.Add(new SqlParameter("@unite_piece", (object)unite));
            cmd.Parameters.Add(new SqlParameter("@quantite_piece", (object)qte));
            cmd.Parameters.Add(new SqlParameter("@quantite_reelle", (object)qte_reelle));
            cmd.Parameters.Add(new SqlParameter("@seuil_piece", (object)seuil));
            cmd.Parameters.Add(new SqlParameter("@nature", (object)nature));
            cmd.Parameters.Add(new SqlParameter("@pua", (object)pua));
            cmd.Parameters.Add(new SqlParameter("@puv", (object)puv));
            cmd.Parameters.Add(new SqlParameter("@code_feur", (object)code_fr));
            cmd.Parameters.Add(new SqlParameter("@puv_rev", (object)puv_rev));
            cmd.Parameters.Add(new SqlParameter("@id", (object)id));


            cmd.ExecuteNonQuery();
            suivi_actions("Modification des données de la piéce : ( " + des + " )");
            return "";
        }
        public string update_stock_aftercmdreceiving(string a, string b)
        {
            function_test();
            string req = "UPDATE stock SET quantite_piece =@quantite_piece WHERE code_piece=@code_piece";

            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@quantite_piece", (object)a));
            cmd.Parameters.Add(new SqlParameter("@code_piece", (object)b));
            cmd.ExecuteNonQuery();
           
            suivi_actions("Ajout du produit ( " + b + " ) Quantité:( " + a + " ) pour alimenter le stock");
            return "";
        }
        public string update_stock_after_accept(int a, string b)
        {
            function_test();
            string req = "UPDATE stock SET quantite_piece -='" + a + "' WHERE libelle_piece= '" + b + "'";
            suivi_actions("Réservation de piéces ( " + b + " ) Quantité:( " + a + " ) pour une commande client");
            return DataExcute(req);
        }

        public string update_stock_after_accept1(int a, string b)
        {
            function_test();
            string req = "UPDATE stock SET quantite_piece +='" + a + "' WHERE libelle_piece= '" + b + "'";
            suivi_actions("Modification la quantité :( " + a + " ) de piéce :( " + b + " ).");
            return DataExcute(req);
        }
        public string update_sousstock_after_accept(Double qt, string code)
        {
            function_test();
            string req = "UPDATE stock SET quantite_piece -='" + qt + "' WHERE code_piece= '" + code + "'";
            suivi_actions("Réservation de piéces ( " + code + " ) Quantité:( " + qt + " ) pour une commande client");
            return DataExcute(req);
        }
        public string update_sousstock_after_accept2(Double qt, string code)
        {
            function_test();
            string req = "UPDATE stock SET quantite_piece =cast(quantite_piece as real)-@quantite_piece WHERE code_piece= @code_piece";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@quantite_piece", (object)qt));
            cmd.Parameters.Add(new SqlParameter("@code_piece", (object)code));
            cmd.ExecuteNonQuery();
            suivi_actions("Réservation de piéces ( " + code + " ) Quantité:( " + qt + " ) pour une commande client");
            return"";
        }
        public string update_piece_factt(Double qt, string code,int id_fact,double prix_vent)
        {
            function_test();
            string req = "UPDATE piece_fact SET quantite_piece_u =@quantite_piece_u, pv =@pv WHERE code_piece_u=@code_piece_u and id_fact=@id_fact";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@quantite_piece_u", (object)qt));
            cmd.Parameters.Add(new SqlParameter("@pv", (object)prix_vent));
            cmd.Parameters.Add(new SqlParameter("@code_piece_u", (object)code));
            cmd.Parameters.Add(new SqlParameter("@id_fact", (object)id_fact));
            cmd.ExecuteNonQuery();
            suivi_actions("ajout de piéces facture vente ( " + code + " ) Quantité:( " + qt + " ) pour une commande client");
            return "";
        }
        public string update_stock_after_annul(int a, string b)
        {
            function_test();
            string req = "UPDATE stock SET quantite_piece +='" + a + "' WHERE libelle_piece= '" + b + "'";
            suivi_actions("Retour de:( " + a + " )articles de:( " + b + " ) au stock suite à annulation de commande client.");
            return DataExcute(req);
        }
        public string update_stock_after_aliment(Double quantite_piece, string code_piece)
        {
            try
            {
                function_test();
                string req = "UPDATE stock SET quantite_piece =@quantite_piece WHERE code_piece =@code_piece";
                SqlCommand cmd = new SqlCommand(req, conn);
                cmd.Parameters.Add(new SqlParameter("@quantite_piece", (object)quantite_piece));
                cmd.Parameters.Add(new SqlParameter("@code_piece", (object)code_piece));
                DataTable dt = new DataTable();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e) { }
            return "";

           // return DataExcute(req);
        }
        public string update_stock_after_aliment2(Double a, string b)
        {
            function_test();
            string req = "UPDATE stock SET quantite_piece -='" + a + "' WHERE code_piece = '" + b + "'";
            return DataExcute(req);
        }

        public string alimenter(DateTime date, Double qte, string commentaire, string timee, string piece, string nature)
        {
            function_test();
            string req = "INSERT INTO alimenter (datee, qte, commentaire, timee, piece, nature)"+
                " VALUES (@datee,@qte,@commentaire,@timee,@piece,@nature)";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@datee", (object)date));
            cmd.Parameters.Add(new SqlParameter("@qte", (object)qte));
            cmd.Parameters.Add(new SqlParameter("@commentaire", (object)commentaire));
            cmd.Parameters.Add(new SqlParameter("@timee", (object)timee));
            cmd.Parameters.Add(new SqlParameter("@piece", (object)piece));
            cmd.Parameters.Add(new SqlParameter("@nature", (object)nature));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            
            suivi_actions("Alimentation de stock >> piéce: (" + piece + ") avec quantité (" + qte + ").");

            return "";
        }

        public string delimenter(DateTime a, int b, string c, string d, string e, string f)
        {
            function_test();
            string req = "INSERT INTO delimenter (datee, qte, commentaire, timee, piece, nature) VALUES ('" + a + "','" + b + "','" + c + "', '" + d + "' , '" + e + "', '" + f + "')";
            suivi_actions("Sortie vers production >> piéce: (" + e + ") avec quantité (" + b + ").");
            return DataExcute(req);
        }
        public string envoie_cde(int code, string a, string b, string c)
        {
            function_test();
            if (sql_gmao.conn.State.ToString().Equals("Closed"))
            {
                sql_gmao.conn.Open();
            }
            string req = "UPDATE Appointments SET etat ='" + b + "', date_envoie= '" + a + "', commentaire ='" + c + "' WHERE UniqueID='" + code + "'";
            suivi_actions("Envoie d’une commande interne.");
            return DataExcute(req);
        }

        public string update_app(int code, string a, string b)
        {
            function_test();
            string req = "UPDATE Appointments SET equipement ='" + a + "', etat ='" + b + "' WHERE UniqueID='" + code + "'";
            suivi_actions("Modification d’une intervention préventitve.");
            return DataExcute(req);
        }
        public string update_cdeeee_sans_comm(int id, string a)
        {
            function_test();
            if (sql_gmao.conn.State.ToString().Equals("Closed"))
            {
                sql_gmao.conn.Open();
            }
            string req = "UPDATE Appointments SET etat ='" + a + "'  WHERE UniqueID= '" + id + "'";
            suivi_actions("Modification l’état :( " + a + " ) de la commande n° :" + id);
            return DataExcute(req);
        }
        public string update_cdeeee(int id, string a, string b)
        {
            function_test();
            if (sql_gmao.conn.State.ToString().Equals("Closed"))
            {
                sql_gmao.conn.Open();
            }
            string req = "UPDATE Appointments SET commentaire +='" + b + "', etat ='" + a + "'  WHERE UniqueID= '" + id + "'";
            suivi_actions("Modification l’état :( " + a + " ) d’une commande.");
            return DataExcute(req);
        }
        //update intervention planifiée
        public string update_appointments(int id, string a, string b, string c, string d)
        {
            function_test();
            string req = "UPDATE Appointments SET equipement='" + a + "', etat='" + b + "', importance= '" + c + "', emetteur= '" + d + "' WHERE UniqueID='" + id + "'";
            suivi_actions("Modification d’une intervention planifiée : " + a);
            return DataExcute(req);
        }
        public string update_commande_after_delete(int code, string a)
        {
            function_test();
            string req = "UPDATE Appointments SET etat ='" + a + "' WHERE UniqueID='" + code + "'";
            return DataExcute(req);
        }
        public string update_piece_fact(int code, string factrass)
        {
            function_test();
            string req = "UPDATE piece_fact SET id_factRass='" + factrass + "' WHERE id_fact='" + code + "'";
            return DataExcute(req);

        }
        // update technicien interne si avec image
        public string update_technicien(byte[] imgdata, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s1, string s10, string c1, string np)
        {
            if (sql_gmao.conn.State.ToString().Equals("Closed"))
            {
                sql_gmao.conn.Open();
            }
            function_test();
            string req = " UPDATE intervenant set nom_intervenant= @nom_intervenant , prenom_intervenant= @prenom_intervenant, email_intervenant= @email_intervenant , tel_intervenant = @tel_intervenant , mobile_intervenant = @mobile_intervenant, poste_intervenant= @poste_intervenant, Image = @imagee, pic= @pic, code_intervenant= @code_intervenant, etat1= @etat1, ResourceName=@np where UniqueID = @id";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@nom_intervenant", (object)s3));
            cmd.Parameters.Add(new SqlParameter("@prenom_intervenant", (object)s2));
            cmd.Parameters.Add(new SqlParameter("@email_intervenant", (object)s6));
            cmd.Parameters.Add(new SqlParameter("@tel_intervenant", (object)s4));
            cmd.Parameters.Add(new SqlParameter("@mobile_intervenant", (object)s5));
            cmd.Parameters.Add(new SqlParameter("@poste_intervenant", (object)s7));
            cmd.Parameters.Add(new SqlParameter("@np", (object)np));
            cmd.Parameters.Add(new SqlParameter("@imagee", (object)imgdata));
            cmd.Parameters.Add(new SqlParameter("@pic", (object)s8));
            cmd.Parameters.Add(new SqlParameter("@code_intervenant", (object)s10));
            cmd.Parameters.Add(new SqlParameter("@id", (object)s1));
            cmd.Parameters.Add(new SqlParameter("@etat1", (object)c1));
            cmd.ExecuteNonQuery();
            return "true";
        }
        // update technicien interne si sans image
        public string update_technicien2(string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s1, string s10, string c1, byte[] Image, string np)
        {
            function_test();
            string req = " UPDATE intervenant set nom_intervenant= @nom_intervenant , prenom_intervenant= @prenom_intervenant, email_intervenant= @email_intervenant , tel_intervenant = @tel_intervenant , mobile_intervenant = @mobile_intervenant, poste_intervenant= @poste_intervenant, pic= @pic, Image= @Image, code_intervenant= @code_intervenant, etat1= @etat1, ResourceName=@np where UniqueID = @id";
            SqlCommand cmd = new SqlCommand(req, conn);

            cmd.Parameters.Add(new SqlParameter("@nom_intervenant", (object)s3));
            cmd.Parameters.Add(new SqlParameter("@prenom_intervenant", (object)s2));
            cmd.Parameters.Add(new SqlParameter("@email_intervenant", (object)s6));
            cmd.Parameters.Add(new SqlParameter("@tel_intervenant", (object)s4));
            cmd.Parameters.Add(new SqlParameter("@mobile_intervenant", (object)s5));
            cmd.Parameters.Add(new SqlParameter("@poste_intervenant", (object)s7));

            cmd.Parameters.AddWithValue("@Image", (Image == null)
   ? (object)DBNull.Value
   : Image).SqlDbType = SqlDbType.Image;
            cmd.Parameters.Add(new SqlParameter("@pic", (object)s8));
            cmd.Parameters.Add(new SqlParameter("@code_intervenant", (object)s10));
            cmd.Parameters.Add(new SqlParameter("@id", (object)s1));
            cmd.Parameters.Add(new SqlParameter("@etat1", (object)c1));
            cmd.Parameters.Add(new SqlParameter("@np", (object)np));
            cmd.ExecuteNonQuery();
            return "true";
        }
        //update sous traitant avec image
        public string update_sous_traitant(byte[] imgdata, string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s9, string s10, string s11, string c1)
        {
            function_test();
            string req = " UPDATE sous_traitant set code_soustraitant= @code_soustraitant , raison_sociale_soustraitant= @raison_sociale_soustraitant, nom_responsable_soustraitant= @nom_responsable_soustraitant , tel_soustraitant = @tel_soustraitant , mobile_soustraitant = @mobile_soustraitant, email_soustraitant= @email_soustraitant, Image = @imagee, pic= @pic, fax= @fax, fonction= @fonction, prenom_responsable_soustraitant= @prenom_responsable_soustraitant, etat= @etat where id = @id";
            SqlCommand cmd = new SqlCommand(req, conn);

            cmd.Parameters.Add(new SqlParameter("@code_soustraitant", (object)s10));
            cmd.Parameters.Add(new SqlParameter("@raison_sociale_soustraitant", (object)s9));
            cmd.Parameters.Add(new SqlParameter("@nom_responsable_soustraitant", (object)s3));
            cmd.Parameters.Add(new SqlParameter("@tel_soustraitant", (object)s4));
            cmd.Parameters.Add(new SqlParameter("@mobile_soustraitant", (object)s5));
            cmd.Parameters.Add(new SqlParameter("@email_soustraitant", (object)s6));
            cmd.Parameters.Add(new SqlParameter("@fonction", (object)s7));
            cmd.Parameters.Add(new SqlParameter("@fax", (object)s11));
            cmd.Parameters.Add(new SqlParameter("@prenom_responsable_soustraitant", (object)s2));
            cmd.Parameters.Add(new SqlParameter("@imagee", (object)imgdata));
            cmd.Parameters.Add(new SqlParameter("@pic", (object)s8));
            cmd.Parameters.Add(new SqlParameter("@etat", (object)c1));
            cmd.Parameters.Add(new SqlParameter("@id", (object)s1));
            cmd.ExecuteNonQuery();
            return "true";
        }
        //update sous traitant sans image
        public string update_sous_traitant2(string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string s9, string s10, string s11, string c1, byte[] Image)
        {
            function_test();
            string req = " UPDATE sous_traitant SET code_soustraitant= @code_soustraitant , raison_sociale_soustraitant= @raison_sociale_soustraitant, nom_responsable_soustraitant= @nom_responsable_soustraitant , tel_soustraitant = @tel_soustraitant , mobile_soustraitant = @mobile_soustraitant, email_soustraitant= @email_soustraitant, pic= @pic,Image= @Image, fax= @fax, fonction= @fonction, prenom_responsable_soustraitant= @prenom_responsable_soustraitant, etat= @etat where id = @id";
            SqlCommand cmd = new SqlCommand(req, conn);

            cmd.Parameters.Add(new SqlParameter("@code_soustraitant", (object)s10));
            cmd.Parameters.Add(new SqlParameter("@raison_sociale_soustraitant", (object)s9));
            cmd.Parameters.Add(new SqlParameter("@nom_responsable_soustraitant", (object)s3));
            cmd.Parameters.Add(new SqlParameter("@tel_soustraitant", (object)s4));
            cmd.Parameters.Add(new SqlParameter("@mobile_soustraitant", (object)s5));
            cmd.Parameters.Add(new SqlParameter("@email_soustraitant", (object)s6));
            cmd.Parameters.Add(new SqlParameter("@fonction", (object)s7));
            cmd.Parameters.Add(new SqlParameter("@fax", (object)s11));
            cmd.Parameters.Add(new SqlParameter("@prenom_responsable_soustraitant", (object)s2));
            cmd.Parameters.AddWithValue("@Image", (Image == null)
    ? (object)DBNull.Value
    : Image).SqlDbType = SqlDbType.Image;
            cmd.Parameters.Add(new SqlParameter("@pic", (object)s8));
            cmd.Parameters.Add(new SqlParameter("@etat", (object)c1));
            cmd.Parameters.Add(new SqlParameter("@id", (object)s1));
            cmd.ExecuteNonQuery();
            return "true";
        }
        //modifier utilisateur de type gestion de stock
        public string update_user_gestion_stock(int id, string x1, string x2, string x3, string x4, string x5, string x6, string x7, string x8, string x9, string x10, string x11, string x12, string x13, string x14, string x15, string x16, string x17, string x18, string x19, string x20, string x21, string x22, string x23, string x24, string x25, string x26, string x27, string x28, string x29, string x30, string x31, string x32, string x33, string x34, string x35, string x36, string x37, string x38, string x39, string x40, string x41, string x42, string x43, string x44)
        {
            function_test();
            string req = "UPDATE droit SET nom =@nom, prenom =@prenom, fonction =@fonction, depart =@depart, gsm =@gsm, email =@email"+
                ", login =@login, passwd =@passwd, visualiser_stock =@visualiser_stock, ajouter_stock =@ajouter_stock, modifier_stock =@modifier_stock"+
                ", supprimer_stock =@supprimer_stock, ges_uni =@ges_uni, ges_mag =@ges_mag, doc_stock =@doc_stock, passer_cde =@passer_cde"+
                ", aliment =@aliment, sortie_prod =@sortie_prod, alim =@alim, sortie_sto =@sortie_sto, vis_clt =@vis_clt, ajout_clt =@ajout_clt"+
                ", mod_clt =@mod_clt, supp_clt=@supp_clt, doc_clt=@doc_clt, supp_cde_clt=@supp_cde_clt, vali_cde_clt=@vali_cde_clt, fact=@fact, bon_liv =@bon_liv, bon_sortie =@bon_sortie, vis_feur =@vis_feur, aj_feur =@aj_feur, mod_feur =@mod_feur, supp_feur =@supp_feur, doc_feur =@doc_feur, supp_cde_feur =@supp_cde_feur, vis_dev =@vis_dev, aj_devis=@aj_devis, supp_dev=@supp_dev, doc_dev=@doc_dev, stat=@stat, noti=@noti, conta=@conta, stat_conta=@stat_conta WHERE id_user =@id_user";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@nom", (object)x1));
            cmd.Parameters.Add(new SqlParameter("@prenom", (object)x2));
            cmd.Parameters.Add(new SqlParameter("@fonction", (object)x3));
            cmd.Parameters.Add(new SqlParameter("@depart", (object)x4));
            cmd.Parameters.Add(new SqlParameter("@gsm", (object)x5));
            cmd.Parameters.Add(new SqlParameter("@email", (object)x6));
            cmd.Parameters.Add(new SqlParameter("@login", (object)x7));
            cmd.Parameters.Add(new SqlParameter("@passwd", (object)x8));
            cmd.Parameters.Add(new SqlParameter("@visualiser_stock", (object)x9));
            cmd.Parameters.Add(new SqlParameter("@ajouter_stock", (object)x10));
            cmd.Parameters.Add(new SqlParameter("@modifier_stock", (object)x11));
            cmd.Parameters.Add(new SqlParameter("@supprimer_stock", (object)x12));
            cmd.Parameters.Add(new SqlParameter("@ges_uni", (object)x13));
            cmd.Parameters.Add(new SqlParameter("@ges_mag", (object)x14));
            cmd.Parameters.Add(new SqlParameter("@doc_stock", (object)x15));
            cmd.Parameters.Add(new SqlParameter("@passer_cde", (object)x16));
            cmd.Parameters.Add(new SqlParameter("@aliment", (object)x17));
            cmd.Parameters.Add(new SqlParameter("@sortie_prod", (object)x18));
            cmd.Parameters.Add(new SqlParameter("@alim", (object)x19));
            cmd.Parameters.Add(new SqlParameter("@sortie_sto", (object)x20));
            cmd.Parameters.Add(new SqlParameter("@vis_clt", (object)x21));
            cmd.Parameters.Add(new SqlParameter("@ajout_clt", (object)x22));
            cmd.Parameters.Add(new SqlParameter("@mod_clt", (object)x23));
            cmd.Parameters.Add(new SqlParameter("@supp_clt", (object)x24));
            cmd.Parameters.Add(new SqlParameter("@doc_clt", (object)x25));
            cmd.Parameters.Add(new SqlParameter("@supp_cde_clt", (object)x26));
            cmd.Parameters.Add(new SqlParameter("@vali_cde_clt", (object)x27));
            cmd.Parameters.Add(new SqlParameter("@fact", (object)x28));
            cmd.Parameters.Add(new SqlParameter("@bon_liv", (object)x29));
            cmd.Parameters.Add(new SqlParameter("@bon_sortie", (object)x30));
            cmd.Parameters.Add(new SqlParameter("@vis_feur", (object)x31));
            cmd.Parameters.Add(new SqlParameter("@aj_feur", (object)x32));
            cmd.Parameters.Add(new SqlParameter("@mod_feur", (object)x33));
            cmd.Parameters.Add(new SqlParameter("@supp_feur", (object)x34));
            cmd.Parameters.Add(new SqlParameter("@doc_feur", (object)x35));
            cmd.Parameters.Add(new SqlParameter("@supp_cde_feur", (object)x36));
            cmd.Parameters.Add(new SqlParameter("@vis_dev", (object)x37));
            cmd.Parameters.Add(new SqlParameter("@aj_devis", (object)x38));
            cmd.Parameters.Add(new SqlParameter("@supp_dev", (object)x39));
            cmd.Parameters.Add(new SqlParameter("@doc_dev", (object)x40));
            cmd.Parameters.Add(new SqlParameter("@stat", (object)x41));
            cmd.Parameters.Add(new SqlParameter("@noti", (object)x42));
            cmd.Parameters.Add(new SqlParameter("@conta", (object)x43));
            cmd.Parameters.Add(new SqlParameter("@stat_conta", (object)x44));
            cmd.Parameters.Add(new SqlParameter("@id_user", (object)id));
            cmd.ExecuteNonQuery();
            
            suivi_actions("Modification des droits de l'utilisateur : " + x1 + " " + x2);
            return "";
        }

        //modifier utilisateur de type admin
        public string update_user_admin(int id, string x1, string x2, string x3, string x4, string x5, string x6, string x7, string x8)
        {
            function_test();
            string req = "UPDATE droit SET nom =@nom, prenom =@prenom, fonction =@fonction, depart =@depart, gsm =@gsm, email =@email, login =@login, passwd =@passwd WHERE id_user =@id_user";

            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@nom", (object)x1));
            cmd.Parameters.Add(new SqlParameter("@prenom", (object)x2));
            cmd.Parameters.Add(new SqlParameter("@fonction", (object)x3));
            cmd.Parameters.Add(new SqlParameter("@depart", (object)x4));
            cmd.Parameters.Add(new SqlParameter("@gsm", (object)x5));
            cmd.Parameters.Add(new SqlParameter("@email", (object)x6));
            cmd.Parameters.Add(new SqlParameter("@login", (object)x7));
            cmd.Parameters.Add(new SqlParameter("@passwd", (object)x8));
            cmd.Parameters.Add(new SqlParameter("@id_user", (object)id));
            cmd.ExecuteNonQuery();
            suivi_actions("Modification des informations de l'Administrateur : " + x1 + " " + x2);

            return "";
        }
        public string update_skin(int id, string a)
        {
            function_test();
            if (sql_gmao.conn.State.ToString().Equals("Closed"))
            {
                sql_gmao.conn.Open();
            }
            string req = "UPDATE droit SET theme ='" + a + "'WHERE id_user= '" + id + "'";
            return DataExcute(req);
        }
        public string update_commande_etat(int id, string a, string b, string ht, string ttc, string tva, string remise, string timbre, string dev)
        {
            if (sql_gmao.conn.State.ToString().Equals("Closed"))
            {
                sql_gmao.conn.Open();
            }
            function_test();

            string req = "UPDATE commande SET etat ='" + a + "', date_env = '" + DateTime.Now.ToShortDateString() + "', emetteur = '" + b + "' , montant_ht ='" + ht + "', montant_ttc='" + ttc + "',tva ='" + tva + "', remise='" + remise + "', timbre='" + timbre + "', dev='" + dev + "' WHERE id= '" + id + "'";
            suivi_actions("Modification de la commande fournisseur n° " + id + ".");
            return DataExcute(req);
        }
        public string update_Mesures(string des, string unite, int valSup, int valAct, string dateMes, string codeInt, int idMesures)
        {
            string req = "UPDATE mesures SET designation_mesures ='" + des + "',unite_mesures ='" + unite + "',limite_supp_mesures ='" + valSup + "',valeur_actuelle_mesures ='" + valAct + "',code_intervenant ='" + codeInt + "',date_mesure ='" + dateMes + "' WHERE id_mesure='" + idMesures + "' ";
            suivi_actions("Modification la mesure :( " + des + " ).");
            return DataExcute(req);
        }
        public string update_notification(int id_notif, string user)
        {
            function_test();
            string req = "UPDATE notification SET vu_notif ='" + user + "' WHERE id_notif='" + id_notif + "' ";
            suivi_actions("Marquée notification :( " + id_notif + " ) comme vu.");
            return DataExcute(req);
        }
        public string update_Administrateur(string nom, string poste, string dep, string gsm, string email, string pwd, string login)
        {
            function_test();
            string req = "UPDATE droit SET n_p ='" + nom + "';fonction ='" + poste + "',depart ='" + dep + "',gsm ='" + gsm + "',email ='" + email + "',passwd ='" + pwd + "' WHERE login='" + login + "' ";
            suivi_actions("Modification de l’administrateur : " + nom);
            return DataExcute(req);
        }
        public string update_commentaire_and_etat_commande(int id, string a, string b)
        {
            function_test();
            string req = "UPDATE commande SET etat_chez_feur='" + a + "', commentaire='" + b + "' WHERE id='" + id + "'";
            suivi_actions("Modification l’état d’une commande : " + a);
            return DataExcute(req);
        }

        public string update_commentaire_bon_liv(int id, string a)
        {
            function_test();
            string req = "UPDATE facture SET comm_bon_liv='" + a + "' WHERE id='" + id + "'";
            suivi_actions("ajout d'un commentaire: bon de livraison n° " + id);
            return DataExcute(req);
        }
        public string update_commentaire_fact(int id, string a)
        {
            function_test();
            string req = "UPDATE facture SET com2='" + a + "' WHERE id='" + id + "'";
            suivi_actions("ajout d'un commentaire pour annulation de la commande client n° " + id + "");
            return DataExcute(req);
        }

        public string update_commentaire_facture(int id, string a)
        {
            function_test();
            string req = "UPDATE facture SET comm_fact='" + a + "' WHERE id='" + id + "'";
            suivi_actions("ajout d'un commentaire: Facture n° " + id);
            return DataExcute(req);
        }

        public string update_id_facture(int id, string a)
        {
            function_test();
            string req = "UPDATE facture SET id_fact='" + a + "' WHERE id='" + id + "'";
            suivi_actions("Changement de l'identifiant de la Facture de la commande n° " + id);
            return DataExcute(req);
        }
        public string update_id_liv(int id, string a)
        {
            function_test();
            string req = "UPDATE facture SET id_liv='" + a + "' WHERE id='" + id + "'";
            suivi_actions("Changement de l'identifiant de bon de livraison de la commande n° " + id);
            return DataExcute(req);
        }

        public string update_commentaire_devis(int id, string a)
        {
            function_test();
            string req = "UPDATE devis SET comm_fact='" + a + "' WHERE id='" + id + "'";
            suivi_actions("ajout d'un commentaire: Devis n° " + id);
            return DataExcute(req);
        }


        public string update_user(int id, string a, string b, string c)
        {
            function_test();
            string req = "UPDATE droit SET gsm='" + a + "', email='" + b + "', passwd='" + c + "' WHERE id_user='" + id + "'";
            suivi_actions("Modification de ses droits d'accès ou informations: Utilisateur n° : " + id);
            return DataExcute(req);
        }
        public string update_SocieteWithoutPic(byte[] Image, string nom_soc, string res_soc, string adr_soc, string tel_soc, string fax_soc, string gsm_soc, string email_soc, string site_soc, string mat_soc, string pic_soc, int id_soc, string banque, string compte)
        {
            function_test();
            string req = " UPDATE societe set nom_societe= @nom_societe , responseble_societe= @responseble_societe, adresse_societe= @adresse_societe , tel_societe = @tel_societe , fax_societe = @fax_societe, gsm_societe= @gsm_societe, email_societe= @email_societe,site_societe= @site_societe, matricule_societe= @matricule_societe, logo_societe= @logo_societe, pic_societe= @pic_societe, banque= @banque, compte= @compte where id_societe = @id_societe";
            SqlCommand cmd = new SqlCommand(req, conn);

            cmd.Parameters.Add(new SqlParameter("@nom_societe", (object)nom_soc));
            cmd.Parameters.Add(new SqlParameter("@responseble_societe", (object)res_soc));
            cmd.Parameters.Add(new SqlParameter("@adresse_societe", (object)adr_soc));
            cmd.Parameters.Add(new SqlParameter("@tel_societe", (object)tel_soc));
            cmd.Parameters.Add(new SqlParameter("@fax_societe", (object)fax_soc));
            cmd.Parameters.Add(new SqlParameter("@gsm_societe", (object)gsm_soc));
            cmd.Parameters.Add(new SqlParameter("@email_societe", (object)email_soc));
            cmd.Parameters.Add(new SqlParameter("@site_societe", (object)site_soc));
            cmd.Parameters.Add(new SqlParameter("@matricule_societe", (object)mat_soc));
            cmd.Parameters.AddWithValue("@logo_societe", (Image == null) ? (object)DBNull.Value : Image).SqlDbType = SqlDbType.Image;
            cmd.Parameters.Add(new SqlParameter("@pic_societe", (object)pic_soc));
            cmd.Parameters.Add(new SqlParameter("@id_societe", (object)id_soc));
            cmd.Parameters.Add(new SqlParameter("@banque", (object)banque));
            cmd.Parameters.Add(new SqlParameter("@compte", (object)compte));
            cmd.ExecuteNonQuery();
            suivi_actions("Modification des information de la société");
            return "true";
        }
        public string update_SocieteWithPicture(string nom_soc, string res_soc, string adr_soc, string tel_soc, string fax_soc, string gsm_soc, string email_soc, string site_soc, string mat_soc, byte[] imgdata, string pic_soc, int id_soc, string banque, string compte)
        {
            function_test();
            string req = " UPDATE societe set nom_societe= @nom_societe , responseble_societe= @responseble_societe, adresse_societe= @adresse_societe , tel_societe = @tel_societe , fax_societe = @fax_societe, gsm_societe= @gsm_societe, email_societe= @email_societe,site_societe= @site_societe, matricule_societe= @matricule_societe, logo_societe= @logo_societe, pic_societe= @pic_societe, banque= @banque, compte= @compte where id_societe = @id_societe";
            SqlCommand cmd = new SqlCommand(req, conn);

            cmd.Parameters.Add(new SqlParameter("@nom_societe", (object)nom_soc));
            cmd.Parameters.Add(new SqlParameter("@responseble_societe", (object)res_soc));
            cmd.Parameters.Add(new SqlParameter("@adresse_societe", (object)adr_soc));
            cmd.Parameters.Add(new SqlParameter("@tel_societe", (object)tel_soc));
            cmd.Parameters.Add(new SqlParameter("@fax_societe", (object)fax_soc));
            cmd.Parameters.Add(new SqlParameter("@gsm_societe", (object)gsm_soc));
            cmd.Parameters.Add(new SqlParameter("@email_societe", (object)email_soc));
            cmd.Parameters.Add(new SqlParameter("@site_societe", (object)site_soc));
            cmd.Parameters.Add(new SqlParameter("@matricule_societe", (object)mat_soc));
            cmd.Parameters.Add(new SqlParameter("@logo_societe", (object)imgdata));
            cmd.Parameters.Add(new SqlParameter("@pic_societe", (object)pic_soc));
            cmd.Parameters.Add(new SqlParameter("@id_societe", (object)id_soc));
            cmd.Parameters.Add(new SqlParameter("@banque", (object)banque));
            cmd.Parameters.Add(new SqlParameter("@compte", (object)compte));
            cmd.ExecuteNonQuery();
            suivi_actions("Modification des information de la société");
            return "true";
        }




        public string update_pied(string pied, byte[] imm_pied)
        {
            function_test();
            string req = " UPDATE societe set pic_pied= @pied , imm= @imm";
            SqlCommand cmd = new SqlCommand(req, conn);

            cmd.Parameters.Add(new SqlParameter("@pied", (object)pied));
            cmd.Parameters.Add(new SqlParameter("@imm", (object)imm_pied));

            cmd.ExecuteNonQuery();
            return "true";
        }

        public string update_piedbbb(string pied, byte[] imm_pied)
        {
            function_test();
            string req = " UPDATE societe set pic_pied= @pied , imm= @imm";
            SqlCommand cmd = new SqlCommand(req, conn);

            cmd.Parameters.Add(new SqlParameter("@pied", (object)pied));

            cmd.Parameters.AddWithValue("@imm", (imm_pied == null)
   ? (object)DBNull.Value
   : imm_pied).SqlDbType = SqlDbType.Image;

            cmd.ExecuteNonQuery();
            return "true";
        }

        //*********************************** DELETE ***********************************
        public string delete_info_supp(int code)
        {
            function_test();
            string req = "DELETE FROM info_supp_eq WHERE id_info_supp='" + code + "'";
            suivi_actions("Suppression d’une information supplimentaire.");
            return DataExcute(req);
        }
        public string delete_contactGarentie(string code)
        {
            function_test();
            string req = "DELETE FROM contrat_garantie WHERE code_contrat_g='" + code + "'";
            suivi_actions("Suppression d’un contrat de garantie.");
            return DataExcute(req);
        }
        //supprimer fichier contrat
        public string delete_contact_pict(int code)
        {
            function_test();
            string req = "DELETE FROM fichier_garentie WHERE id_fichier_g='" + code + "'";
            suivi_actions("Suppression d’une fiche de contrat de garantie.");
            return DataExcute(req);
        }

        public string delete__pict(int code)
        {
            function_test();
            string req = "DELETE FROM retour WHERE id='" + code + "'";
            suivi_actions("Suppression d’un retour client n° " + code);
            return DataExcute(req);
        }

        public string delete__pict2(int code)
        {
            function_test();
            string req = "DELETE FROM retour2 WHERE id=@id";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@id", (object)code));
            cmd.ExecuteNonQuery();
            suivi_actions("Suppression d’une Commande client reçu n° " + code);
            return "";
        }

        public string delete__pict3(int code)
        {
            function_test();
            string req = "DELETE FROM retour3 WHERE id='" + code + "'";
            suivi_actions("Suppression d’une Commande Fournisseur reçu n° " + code);
            return DataExcute(req);
        }

        public string delete_encaiss(int code)
        {
            function_test();
            string req = "DELETE FROM conta WHERE id=@id";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@id", (object)code));
            cmd.ExecuteNonQuery();
            
            suivi_actions("Suppression d’un encaissement n° " + code);
            return "";
        }
        public string delete_piece2(int code)
        {
            function_test();

            if (sql_gmao.conn.State.ToString().Equals("Closed"))
            {
                sql_gmao.conn.Open();
            }
            string req = "DELETE FROM piece WHERE UniqueID='" + code + "'";
            suivi_actions("Suppression d’une piéce.");
            return DataExcute(req);
        }
        public string delete_piece_from_cde(int code, string a)
        {
            function_test();
            string req = "DELETE FROM piece WHERE id_cde='" + code + "' And etat= '" + a + "'";

            return DataExcute(req);
        }

        public string delete_piece_from_cde_clt(int code)
        {
            function_test();
            string req = "DELETE FROM piece_commande WHERE id_commande=@id_commande";

            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@id_commande", (object)code));
            cmd.ExecuteNonQuery();
            return "";
        }
        public string delete_piece_from_cde_fr(int code)
        {
            function_test();
            string req = "DELETE FROM piece_commandefr WHERE id_commande=@id_commande";

            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@id_commande", (object)code));
            cmd.ExecuteNonQuery();
            return "";
        }
        public string delete_piece_bl(int code)
        {
            function_test();
            string req = "DELETE FROM piece_bl WHERE id_commande=@id_commande";

            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@id_commande", (object)code));
            cmd.ExecuteNonQuery();
            return "";
        }

        public string delete_piece_fact(int code)
        {
            function_test();
            string req = "DELETE FROM piece_fact WHERE id_fact='" + code + "'";

            return DataExcute(req);
        }
       
        public string delete_piece_factpf(int code)
        {
            function_test();
            string req = "DELETE FROM piece_factpf WHERE id_factpf=@id_factpf";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@id_factpf", (object)code));
            cmd.ExecuteNonQuery();
            return "";
            
        }
        public string delete_BL(int code)
        {
            function_test();
            string req = "DELETE FROM bon_livraison WHERE id='" + code + "'";

            return DataExcute(req);
        }
        public string delete_facture(int code)
        {
            function_test();
            string req = "DELETE FROM facture WHERE id='" + code + "'";

            return DataExcute(req);
        }
        public string delete_facturepf(int code)
        {
            function_test();
            string req = "DELETE FROM facturePerformat WHERE id=@id";

            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@id", (object)code));
            cmd.ExecuteNonQuery();
            return "";
        }
        public string delete_piece_from_cdepiece(int code)
        {
            function_test();
            string req = "DELETE FROM piece_commande WHERE id_piece='" + code + "'";

            return DataExcute(req);
        }
        public string delete_piece_from_devis(int code)
        {
            function_test();
            string req = "DELETE FROM piece_devis WHERE id_fact=@id_fact";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@id_fact", (object)code));
            cmd.ExecuteNonQuery();
            return "";
        }

        public string delete_piece(int code)
        {
            function_test();
            string req = "DELETE FROM piece WHERE id_piece='" + code + "'";
            suivi_actions("Suppression d’une piéce n° " + code);
            return DataExcute(req);
        }
        public string delete_pieceOfIntervention(string code)
        {
            function_test();
            string req = "DELETE FROM piece WHERE code_intervention='" + code + "'";
            return DataExcute(req);
        }
        public string delete_compteur(int code)
        {
            function_test();
            string req = "DELETE FROM compteur WHERE id_compteur='" + code + "'";
            suivi_actions("Suppression d’un compteur.");
            return DataExcute(req);
        }
        public string delete_FamilleEquipement(int code)
        {
            function_test();
            string req = "DELETE FROM Famille_equipement WHERE code_fe=@code_fe";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@code_fe", (object)code));
            cmd.ExecuteNonQuery();
            suivi_actions("Suppression d’une Famille d’équipement(Ceci enjoindre la suppression de tous les équipements sous la famille supprimée).");
            return "";
        }
        public string delete_SousFamilleEquipement(int code)
        {
            function_test();
            string req = "DELETE FROM SousFamille_eq WHERE code_sfe='" + code + "'";
            suivi_actions("Suppression d’une Sous Famille d’équipement(Ceci enjoindre la suppression de tous les équipements sous la sous famille supprimée).");
            return DataExcute(req);
        }
        public string delete_EquipementByFamille(int code)
        {
            function_test();
            string req = "DELETE FROM equipement WHERE famille_eq='" + code + "'";
            suivi_actions("Suppression d’une équipement suit la suppression de sa Famille d’équipement.");
            return DataExcute(req);
        }
        public string delete_EquipementBySousFamille(int code)
        {
            function_test();
            string req = "DELETE FROM equipement WHERE sfamille_eq='" + code + "'";
            suivi_actions("Suppression d’une équipement suit la suppression de sa Sous Famille d’équipement.");
            return DataExcute(req);
        }
        public string delete_MesureByEq(int code)
        {
            function_test();
            string req = "DELETE FROM mesures WHERE id_mesure='" + code + "'";
            suivi_actions("Suppression d’une Mesures.");
            return DataExcute(req);
        }
        public string delete_intervention(string code)
        {
            function_test();
            string req = "DELETE FROM intervention WHERE code_intervention='" + code + "'";
            suivi_actions("Suppression d’une intervention.");
            return DataExcute(req);
        }
        public string delete_feur(string code)
        {
            function_test();
            string req = "DELETE FROM fournisseur WHERE code_feur=@code_feur";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@code_feur", (object)code));
            cmd.ExecuteNonQuery();
            suivi_actions("Suppression d’un fournisseur ayant le code " + code);
            return "";
        }


        public string delete_clt(string code)
        {
            function_test();
            string req = "DELETE FROM client WHERE code_clt='" + code + "'";
            suivi_actions("Suppression d’un client ayant le code " + code + ".");
            return DataExcute(req);
        }

        public string delete_cde(int code)
        {
            function_test();
            string req = "DELETE FROM commande WHERE id='" + code + "'";
            suivi_actions("Suppression d’une commande.");
            return DataExcute(req);
        }

        public string delete_cde_clt(int code)
        {
            function_test();
            string req = "DELETE FROM facture WHERE id='" + code + "'";
            suivi_actions("Suppression de la commande client n° " + code + ".");
            return DataExcute(req);
        }
        public string deletecmdClt(int code)
        {
            function_test();
            string req = "DELETE FROM CommandeClient WHERE id_commande=@id_commande";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@id_commande", (object)code));
            cmd.ExecuteNonQuery();
           
            suivi_actions("Suppression de la commande client n° " + code + ".");
            return "";
        }
        public string deletecmdfr(int code)
        {
            function_test();
            string req = "DELETE FROM Commandefr WHERE id_commande=@id_commande";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@id_commande", (object)code));
            cmd.ExecuteNonQuery();
            
            suivi_actions("Suppression de la commande client n° " + code + ".");
            return "";
        }
        public string delete_devis1(int code)
        {
            function_test();
            string req = "DELETE FROM devis WHERE id=@id";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@id", (object)code));
            cmd.ExecuteNonQuery();
            suivi_actions("Suppression de Devis n° " + code + ".");
            return "";
        }
        // supprimer technicien
        public string delete_tech(int code)
        {
            function_test();
            string req = "DELETE FROM intervenant WHERE UniqueID='" + code + "'";
            suivi_actions("Suppression d’un technicien. Code technicien : ( " + code + " ).");
            return DataExcute(req);
        }

        // supprimer sous traitant
        public string delete_sous_trait(int code)
        {
            function_test();
            string req = "DELETE FROM sous_traitant WHERE id='" + code + "'";
            suivi_actions("Suppression d’un Sous Traitant. Code sous Traitant : ( " + code + " ).");
            return DataExcute(req);
        }
        // supprimer user
        public string delete_user(int code)
        {
            function_test();
            string req = "DELETE FROM droit WHERE id_user=@id_user";
            suivi_actions("Supression d'un utilisateur n°: " + code);

            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@id_user", (object)code));
            cmd.ExecuteNonQuery();
            return "";
        }
        public string delete_cde_inter(int code)
        {
            function_test();
            string req = "DELETE FROM Appointments WHERE UniqueID='" + code + "'";
            suivi_actions("Supression une commande interne rélative à une intervention préventive.");
            return DataExcute(req);
        }
        public string delete_Notification(int code)
        {
            function_test();
            string req = "DELETE FROM notification WHERE id_notif='" + code + "'";
            suivi_actions("Supression d’une notification.");
            return DataExcute(req);
        }
        public string vider_Notification()
        {
            function_test();
            string req = "TRUNCATE TABLE notification";
            suivi_actions("Suppression de toutes les notifications");
            return DataExcute(req);
        }
        public string delete_Admin(int code)
        {
            function_test();
            string req = "DELETE FROM droit WHERE id_user='" + code + "'";
            suivi_actions("Suppression de l’utilisateur : " + code);
            return DataExcute(req);
        }
        public string delete_pieceFromStock(string code)
        {
            function_test();
            string req = "DELETE FROM stock WHERE code_piece=@code_piece";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@code_piece", (object)code));
            cmd.ExecuteNonQuery();

            suivi_actions("Suppression de piéce n°: " + code + " de stock");
            return DataExcute(req);
        }
        //*********************************** SLECTION ***********************************
        public DataTable select_Mat_Prem()
        {
            function_test();
            req_select = "SELECT * FROM stock e WHERE e.nature not like 'Produit fini'";
            DataSet ds = new DataSet();
            da = new SqlDataAdapter(req_select, conn);
            da.Fill(ds, "table");
            DataTable data = new DataTable();
            data = ds.Tables["table"];
            return data;
        }
        public DataTable select_compte_cl11(string code)
        {

            function_test();
            req_select = "SELECT * FROM compte_cl where code_cl= '" + code + "'";
            return DataReturn(req_select);
            //function_test();
            //string req = "UPDATE compte_fr SET solde +='" +  montant + "',debit +='" +montant+ "'  WHERE code_fr ='" + code + "'";
            //suivi_actions("Validation de la compte fournisseur : (" + code + ")");
            //return DataExcute(req);
        }
        public DataTable get_piece_fact(int id_fact, string code)
        {
            function_test();
            string req = "select * FROM piece_fact WHERE id_fact=@id_fact and code_piece_u=@code_piece_u";

            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@id_fact", (object)id_fact));
            cmd.Parameters.Add(new SqlParameter("@code_piece_u", (object)code));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public string update_compte_cl11(string code_cl, string solde, string debit)
        {


            function_test();
            string req = "UPDATE compte_cl SET solde =@solde, credit= @credit  WHERE code_cl =@code_cl ";
            SqlCommand cmd = new SqlCommand(req, sql_gmao.conn);
            cmd.Parameters.Add(new SqlParameter("@code_cl", (object)code_cl));
            cmd.Parameters.Add(new SqlParameter("@solde", (object)solde));
            cmd.Parameters.Add(new SqlParameter("@debit", (object)debit));
            suivi_actions("Validation de la compte fournisseur : (" + code_cl + ")");
            cmd.ExecuteNonQuery();
            return "true";
        }
        public DataTable select_Prod_Fini()
        {
            function_test();
            req_select = "SELECT * FROM stock e WHERE e.nature like 'Produit fini'";
            DataSet ds = new DataSet();
            da = new SqlDataAdapter(req_select, conn);
            da.Fill(ds, "table");
            DataTable data = new DataTable();
            data = ds.Tables["table"];
            return data;
        }
        public DataTable get_CltSuivi(string idclt, DateTime date1, DateTime date2)
        {
            function_test();
            //req_select = "SELECT libelle_piece_u,quantite_piece_u,id_fact,id_clt FROM piece_fact WHERE code_piece_u=" + codeP ;
            req_select = "SELECT * FROM piece_fact WHERE id_clt='" + idclt + "' and id_fact IN ( SELECT id FROM facturevente WHERE cast(substring(date_ajout,7,4)+'/'+substring(date_ajout,4,2)+'/'+substring(date_ajout,1,2) as datetime) between cast('" + date1.Year.ToString() + "/" + date1.Month.ToString() + "/" + date1.Day.ToString() + "' as datetime) and cast('" + date2.Year.ToString() + "/" + date2.Month.ToString() + "/" + date2.Day.ToString() + "' as datetime) and avoir='0')";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }

        public DataTable selectMatPremByCodeP(string x)
        {
            function_test();
            req_select = "SELECT * FROM Art_SousArt e WHERE e.codeArt = '" + x + "'";
            DataSet ds = new DataSet();
            da = new SqlDataAdapter(req_select, conn);
            da.Fill(ds, "table");
            DataTable data = new DataTable();
            data = ds.Tables["table"];
            return data;
        }
        public string req_select;
        public DataTable get_equipementById(int code)
        {
            function_test();
            req_select = "SELECT e.Code_eq FROM equipement e WHERE e.uid = '" + code + "'";
            return DataReturn(req_select);
        }
        public DataTable get_equipementByIdtree(int code)
        {
            function_test();
            req_select = "SELECT * FROM equipement e WHERE e.uid = '" + code + "'";
            return DataReturn(req_select);
        }

        public DataTable get_etat_fact(int code)
        {
            function_test();
            req_select = "SELECT * FROM facture  WHERE id = '" + code + "'";
            return DataReturn(req_select);
        }
        public DataTable get_etat_factpf(int code)
        {
            function_test();
            req_select = "SELECT * FROM facturePerformat  WHERE id = '" + code + "'";
            return DataReturn(req_select);
        }
        public DataTable get_etat_factpfBynum(int numero)
        {
            function_test();
            req_select = "SELECT * FROM facturePerformat  WHERE numero =@numero";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@numero", (object)numero));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public DataTable get_etat_commande(int code)
        {
            function_test();
            req_select = "SELECT * FROM CommandeClient  WHERE id_commande = '" + code + "'";
            return DataReturn(req_select);
        }
        public DataTable get_etat_cmdClt(int code)
        {
            function_test();
            req_select = "SELECT * FROM CommandeClient  WHERE id_commande = '" + code + "'";
            return DataReturn(req_select);
        }
        public DataTable get_etat_cmdfr(int code)
        {
            function_test();
            req_select = "SELECT * FROM Commandefr  WHERE id_commande = '" + code + "'";
            return DataReturn(req_select);
        }
        public DataTable get_etat_devv(int code)
        {
            function_test();
            req_select = "SELECT * FROM devis  WHERE id =@id";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@id", (object)code));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }

        public DataTable select_equipement(int code)
        {
            function_test();
            req_select = "SELECT * FROM equipement e WHERE e.uid = '" + code + "'";
            return DataReturn(req_select);
        }
        public DataTable get_equipementByFamille(int code)
        {
            function_test();
            req_select = "SELECT * FROM equipement WHERE famille_eq = '" + code + "'";
            return DataReturn(req_select);
        }
        public DataTable select_contratgarentie(int code)
        {
            function_test();
            req_select = "SELECT * FROM equipement e, contrat_garantie cg WHERE e.uid = '" + code + "' AND e.code_contrat_g = cg.code_contrat_g";
            return DataReturn(req_select);
        }
        public DataTable get_contratByCodeEq(string code)
        {
            function_test();
            req_select = "SELECT * FROM contrat_garantie WHERE code_contrat_g = '" + code + "'";
            return DataReturn(req_select);
        }
        public DataTable get_ficheByCodeContrat(string code)
        {
            function_test();
            req_select = "SELECT * FROM fichier_garentie WHERE code_contrat_g = '" + code + "'";
            return DataReturn(req_select);
        }
        public DataTable info_supp(string code)
        {
            function_test();
            req_select = "SELECT inf.id_info_supp, inf.designation_info_supp, inf.contenu_info_supp FROM info_supp_eq inf, equipement e WHERE e.Code_eq = '" + code + "' AND e.Code_eq = inf.code_eq ";
            return DataReturn(req_select);
        }

        public DataTable get_infoSupByEqu(string code)
        {
            function_test();
            req_select = "SELECT * FROM info_supp_eq WHERE code_eq = '" + code + "'";
            return DataReturn(req_select);
        }
        public DataTable mesures_equipement(int code)
        {
            function_test();
            req_select = "SELECT m.id_mesure, m.designation_mesures, m.unite_mesures, m.limite_inf_mesures, m.limite_supp_mesures, m.valeur_actuelle_mesures, p.nom_intervenant, p.prenom_intervenant, p.email_intervenant, p.mobile_intervenant FROM mesures m, equipement e, intervenant p WHERE e.uid = '" + code + "' AND e.Code_eq = m.code_eq AND m.code_intervenant = p.code_intervenant";
            return DataReturn(req_select);
        }
        public DataTable get_mesuresByEqu(string code)
        {
            function_test();
            req_select = "SELECT * FROM mesures WHERE code_eq = '" + code + "'";
            return DataReturn(req_select);
        }
        public DataTable compteur_equipement(string code)
        {
            function_test();
            req_select = "SELECT * FROM compteur WHERE code_eq = '" + code + "'";
            return DataReturn(req_select);
        }
        public DataTable get_intervention_equipement(int code)
        {
            function_test();
            req_select = "SELECT a.code_intervention, a.titre_intervention, a.description_intervention, a.etat_intervention, a.date_intervention, a.heure_debut_intervention,a.date_fin_intervention, a.heure_fin_intervention, a.code_eq, a.code_intervenant, a.code_soustraitant, a.categorie_intervention FROM equipement e, intervention a, intervenant p WHERE e.uid = '" + code + "' AND e.Code_eq = a.code_eq AND a.code_intervenant = p.code_intervenant ORDER BY a.date_intervention DESC";
            return DataReturn(req_select);
        }
        public DataTable get_intervention(string code)
        {
            function_test();
            req_select = "SELECT * FROM intervention WHERE code_intervention = '" + code + "'";
            return DataReturn(req_select);
        }
        public DataTable get_interventionByCodeEq(string code)
        {
            function_test();
            req_select = "SELECT * FROM intervention WHERE code_eq = '" + code + "'";
            return DataReturn(req_select);
        }
        public DataTable get_intervenant_interne(string code)
        {
            function_test();
            req_select = "SELECT * FROM intervenant WHERE code_intervenant = '" + code + "'";
            return DataReturn(req_select);
        }
        public DataTable get_Allintervenant_interne()
        {
            function_test();
            req_select = "SELECT code_intervenant,nom_intervenant,prenom_intervenant,poste_intervenant,email_intervenant,tel_intervenant,mobile_intervenant,pwd_intervenant FROM intervenant ORDER BY prenom_intervenant ASC";
            return DataReturn(req_select);
        }
        public DataTable get_intervenant_externe(string code)
        {
            function_test();
            req_select = "SELECT * FROM sous_traitant WHERE code_soustraitant = '" + code + "'";
            return DataReturn(req_select);
        }
        public DataTable get_Allintervenant_externe()
        {
            function_test();
            req_select = "SELECT * FROM sous_traitant ORDER BY nom_responsable_soustraitant ASC";
            return DataReturn(req_select);
        }
        public DataTable get_intervenantByCode(String code)
        {
            function_test();
            req_select = "SELECT nom_intervenant, prenom_intervenant FROM intervenant WHERE code_intervenant = '" + code + "'";
            return DataReturn(req_select);
        }
        public DataTable get_extervenantByCode(String code)
        {
            function_test();
            req_select = "SELECT * FROM sous_traitant WHERE code_soustraitant = '" + code + "'";
            return DataReturn(req_select);
        }
        public DataTable get_last_intervention(int code)
        {
            function_test();
            req_select = "SELECT MAX(a.date_intervention) AS LastDate FROM equipement e, intervention a WHERE e.uid = '" + code + "' AND e.Code_eq = a.code_eq";
            return DataReturn(req_select);
        }
        public DataTable get_famille()
        {
            function_test();
            req_select = "SELECT * FROM Famille_equipement ORDER BY designation_fe ASC";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }

        public DataTable get_AllSousfamille()
        {
            function_test();
            req_select = "SELECT * FROM SousFamille_eq ORDER BY designation_sfe ASC";
            return DataReturn(req_select);
        }
        public DataTable get_familleByDes(string des)
        {
            function_test();
            req_select = "SELECT * FROM Famille_equipement WHERE designation_fe=@designation_fe";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@designation_fe", (object)des));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
            
        }
        public DataTable get_SousFamille(int code)
        {
            function_test();
            req_select = "SELECT * FROM SousFamille_eq WHERE code_fe ='" + code + "' ORDER BY designation_sfe ASC";
            return DataReturn(req_select);
        }
        public DataTable get_SousfamilleByDes(string des)
        {
            function_test();
            req_select = "SELECT * FROM SousFamille_eq WHERE designation_sfe= '" + des + "'";
            return DataReturn(req_select);
        }
        public DataTable get_equipementBySousFamille(int des)
        {
            function_test();
            req_select = "SELECT * FROM equipement WHERE sfamille_eq= '" + des + "'";
            return DataReturn(req_select);
        }
        public DataTable get_equipement(int code)
        {
            function_test();
            req_select = "SELECT * FROM Appointments WHERE UniqueID = '" + code + "' ";
            return DataReturn(req_select);
        }
        public DataTable get_familleById(int code)
        {
            function_test();
            req_select = "SELECT * FROM Famille_equipement WHERE code_fe ='" + code + "'";
            return DataReturn(req_select);
        }
        public DataTable get_SousfamilleById(int code)
        {
            function_test();
            req_select = "SELECT * FROM SousFamille_eq WHERE code_sfe ='" + code + "'";
            return DataReturn(req_select);
        }
        public DataTable get_unite()
        {
            function_test();
            req_select = "SELECT id_unite,designation_unite FROM unite ORDER BY designation_unite ASC";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
           
        }
        public DataTable get_contrat_fichier(string code)
        {
            function_test();
            req_select = "SELECT * FROM contrat_garantie cg, fichier_garentie fg WHERE cg.code_contrat_g = '" + code + "' AND cg.code_contrat_g = fg.code_contrat_g";
            return DataReturn(req_select);
        }

        public DataTable get_retour1()
        {
            function_test();
            req_select = "SELECT * FROM retour";
            return DataReturn(req_select);
        }

        public DataTable get_retour2()
        {
            function_test();
            req_select = "SELECT * FROM retour2";
            SqlCommand cmd = new SqlCommand(req_select, conn);
      
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
            
        }

        public DataTable get_retour3()
        {
            function_test();
            req_select = "SELECT * FROM retour3";
            return DataReturn(req_select);
        }

        public DataTable get_conta(string a)
        {
            function_test();
            req_select = "SELECT * FROM conta where type1=@type1";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@type1", (object)a));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }

        public DataTable get_stock()
        {
            function_test();
            req_select = "SELECT * FROM stock ORDER BY libelle_piece ASC";
            return DataReturn(req_select);
        }
        public DataTable get_stockBycode(string code)
        {
            function_test();
            req_select = "SELECT * FROM stock where code_piece=@code";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@code", (object)code));
           // return DataReturn(req_select);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
            
        }
        public DataTable get_stocknotprem()
        {
            function_test();
            req_select = "SELECT * FROM stock e where e.nature not like 'Matiére premiére' ORDER BY libelle_piece ASC";
            return DataReturn(req_select);
        }

        public DataTable get_piece44(int code)
        {
            function_test();
            req_select = "SELECT * FROM piece_fact where id_fact=@id_fact";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@id_fact", (object)code));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public DataTable get_facture_joint(string code)
        {
            function_test();
            req_select = "SELECT * FROM piece_fact where id_factRass='" + code + "' ";
            return DataReturn(req_select);
        }
        public DataTable get_pieceupdated(string code)
        {
            function_test();
            req_select = "SELECT * FROM piece_fact where code_piece_u='" + code + "' ";
            return DataReturn(req_select);
        }

        public DataTable get_piece_from_devis(int code)
        {
            function_test();
            req_select = "SELECT * FROM piece_devis where id_fact=@id_fact";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@id_fact", (object)code));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }

        public DataTable get_piece_fact(string etat, string id_clt, int id_fact)
        {
            function_test();
            req_select = "SELECT * FROM piece_fact where etat!= '" + etat + "' and id_clt='" + id_clt + "' and id_fact='" + id_fact + "' ";
            return DataReturn(req_select);

        }
        public DataTable get_piece_byCode(string code)
        {
            function_test();
            req_select = "SELECT * FROM stock where code_piece=@code_piece";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@code_piece", (object)code));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
            //return DataReturn(req_select);

        }
        public DataTable getartbyCode(string code)
        {
            function_test();
            req_select = "SELECT * FROM stock where code_piece like'%" + code + "%'";
            return DataReturn(req_select);

        }
        public DataTable get_piece_bydesignation(string design)
        {
            function_test();
            req_select = "SELECT * FROM stock where libelle_piece'" + design + "'";
            return DataReturn(req_select);

        }
        public DataTable get_clients()
        {
            function_test();
            req_select = "SELECT * FROM client ORDER BY raison_soc ASC";
            return DataReturn(req_select);
        }
        public DataTable get_clientsByCode(string code)
        {
            function_test();
            req_select = "SELECT * FROM client where code=@code";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@code", (object)code));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
           // return DataReturn(req_select);
        }
        public DataTable test_fact(string client, string etat)
        {
            function_test();
            req_select = "SELECT * FROM facture where id_clt='" + client + "' and etat!='" + etat + "' ";
            return DataReturn(req_select);
        }

        public DataTable test_fact2(string client, string etat_fact, string etat_liv)
        {
            function_test();
            req_select = "SELECT * FROM facture where id_clt='" + client + "' and (etat_fact ='" + etat_fact + "' OR etat_bon_liv ='" + etat_liv + "') ";
            return DataReturn(req_select);
        }
        public DataTable get_his_sortie_stock()
        {
            function_test();
            req_select = "SELECT * FROM delimenter";
            return DataReturn(req_select);
        }
        public DataTable addcateg(string a)
        {
            function_test();
            req_select = "SELECT * FROM categ where type = '" + a + "'";
            return DataReturn(req_select);
        }


        public DataTable get_his_alimentations()
        {
            function_test();
            req_select = "SELECT * FROM alimenter";
            return DataReturn(req_select);
        }


        public DataTable get_pieceOfintervention(string code)
        {
            function_test();
            req_select = "SELECT p.id_piece, p.code_piece_u, p.libelle_piece_u, p.quantite_piece_u FROM intervention a, piece p WHERE p.code_intervention=a.code_intervention AND a.code_intervention = '" + code + "' ORDER BY id_piece ASC";
            return DataReturn(req_select);
        }
        public DataTable get_pieceOfIntervention1(string code)
        {
            function_test();
            req_select = "SELECT * FROM piece WHERE code_intervention = '" + code + "' ";
            return DataReturn(req_select);
        }
        public DataTable get_pieceByintervention(string code)
        {
            function_test();
            req_select = "SELECT p.id_piece, p.code_piece_u, p.libelle_piece_u, p.quantite_piece_u FROM piece p WHERE p.code_intervention = '" + code + "' ORDER BY id_piece ASC";
            return DataReturn(req_select);
        }
        public DataTable get_AllFeur()
        {
            function_test();
            req_select = "SELECT * FROM fournisseur ORDER BY responsbale ASC";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
            //return DataReturn(req_select);
        }
        public DataTable get_frs()
        {
            function_test();
            req_select = "SELECT * FROM fournisseur ORDER BY raison_soc";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;

        }
        public DataTable get_Allclt()
        {
            function_test();
            req_select = "SELECT * FROM client ORDER BY responsbale ASC";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
          //  return DataReturn(req_select);
        }
        public DataTable get_FeurByCode(string Code)
        {
            function_test();
            req_select = "SELECT * FROM fournisseur WHERE code_feur =@code_feur";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@code_feur", (object)Code));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
            

        }



        public DataTable get_cltByCode(string Code)
        {
            function_test();
            req_select = "SELECT * FROM client WHERE code_clt = @code_clt";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@code_clt", (object)Code));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;

        }
        public DataTable get_cltByDesign(string design)
        {
            function_test();
            req_select = "SELECT * FROM client WHERE raison_soc =@raison_soc";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@raison_soc", (object)design));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;

        }
        public DataTable get_frByDesign(string design)
        {
            function_test();
            req_select = "SELECT * FROM fournisseur WHERE raison_soc = '" + design + "'";
            //return DataReturn(req_select);
            DataSet ds = new DataSet();
            da = new SqlDataAdapter(req_select, conn);
            da.Fill(ds, "table");
            DataTable data = new DataTable();
            data = ds.Tables["table"];
            return data;

        }
        public DataTable get_frBycode(string code)
        {
            function_test();
            req_select = "SELECT * FROM fournisseur WHERE code_feur =@code_feur";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@code_feur", (object)code));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;

        }


        //get libellé pieces 
        public DataTable get_piece()
        {
            function_test();
            req_select = "SELECT libelle_piece FROM stock ORDER BY libelle_piece ASC";
            return DataReturn(req_select);
        }
        public DataTable get_facture()
        {
            function_test();
            req_select = "SELECT * FROM facture";
            return DataReturn(req_select);
        }
        public DataTable get_facturepf()
        {
            function_test();
            req_select = "SELECT * FROM facturePerformat";
            SqlCommand cmd = new SqlCommand(req_select, conn);
           
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
          
        }
        public DataTable get_MaxNumFpfm()
        {
            function_test();
            req_select = "SELECT MAX(cast(numero as int)) as max FROM facturePerformat";
            return DataReturn(req_select);
        }
        public DataTable get_CmdClient()
        {
            function_test();
            req_select = "SELECT * FROM CommandeClient";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }

        public DataTable get_BLS()
        {
            function_test();
            req_select = "SELECT * FROM bon_livraison";
            SqlCommand cmd = new SqlCommand(req_select, conn);
         
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public DataTable get_Facts()
        {
            function_test();
            req_select = "SELECT * FROM facture";
            return DataReturn(req_select);
        }
        public DataTable get_devis()
        {
            function_test();
            req_select = "SELECT * FROM devis";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }


        //grid piece selectionnés
        public DataTable grid_piece(int code)
        {
            function_test();
            req_select = "SELECT * FROM piece WHERE UniqueID = '" + code + "' ";
            return DataReturn(req_select);
        }
        public DataTable get_piecebycode(string code)
        {
            function_test();
            req_select = "SELECT * FROM stock WHERE code_piece =@code_piece";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@code_piece", (object)code));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
            //return DataReturn(req_select);
        }
        public DataTable get_Magasin()
        {
            function_test();
            req_select = "SELECT * FROM empalcement";
            return DataReturn(req_select);
        }

        public DataTable get_categ(string a)
        {
            function_test();
            req_select = "SELECT * FROM categ where type ='" + a + "'";
            return DataReturn(req_select);
        }


        public DataTable get_MagasinByDes(string des)
        {
            function_test();
            req_select = "SELECT * FROM empalcement WHERE des_emp= '" + des + "'";
            return DataReturn(req_select);
        }

        public DataTable get_categ2(string des)
        {
            function_test();
            req_select = "SELECT * FROM categ WHERE des= '" + des + "'";
            return DataReturn(req_select);
        }
        public DataTable combologin()
        {
            function_test();
            req_select = "SELECT * FROM droit";
            return DataReturn(req_select);
        }

        public DataTable log_login(string a, string b)
        {
            function_test();
            suivi_actions("Authentification de l'utilisateur " + a);
            req_select = "SELECT * FROM droit WHERE login=@login AND passwd=@passwd";

            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@login", (object)a));
            cmd.Parameters.Add(new SqlParameter("@passwd", (object)b));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public DataTable get_id_piece(int b)
        {
            function_test();
            req_select = "SELECT * FROM piece WHERE id_piece = '" + b + "'";
            return DataReturn(req_select);
        }

        public DataTable list_cde()
        {
            function_test();
            req_select = "SELECT * FROM commande";
            return DataReturn(req_select);
        }
        public DataTable list_cdefr()
        {
            function_test();
            req_select = "SELECT * FROM Commandefr where etatcmd<>'reçu'";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;

        }
        public DataTable list_cdefrrecu()
        {
            function_test();
            req_select = "SELECT * FROM Commandefr where etatcmd='reçu'";
          
            SqlCommand cmd = new SqlCommand(req_select, conn);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;

          
        }
        public DataTable list_cde_interne(string e, string f, string g)
        {
            function_test();
            req_select = "SELECT * FROM Appointments where etat!= '" + e + "' AND etat!= '" + f + "' AND etat!= '" + g + "'";
            return DataReturn(req_select);
        }
        public DataTable affiche_cde_infos(int a)
        {
            function_test();
            req_select = "SELECT * FROM Appointments where UniqueID='" + a + "'";
            return DataReturn(req_select);
        }

        public DataTable affiche_cde_clt_infos(int a)
        {
            function_test();
            req_select = "SELECT * FROM facture where id='" + a + "'";
            return DataReturn(req_select);
        }
        public DataTable affichefacturepfinfos(int a)
        {
            function_test();
            req_select = "SELECT * FROM facturePerformat where id='" + a + "'";
            return DataReturn(req_select);
        }
        public DataTable affiche_cmdclt_infos(int a)
        {
            function_test();
            req_select = "SELECT * FROM CommandeClient where id_commande=@id_commande";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@id_commande", (object)a));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public DataTable affiche_BL_infos(int a)
        {
            function_test();
            req_select = "SELECT * FROM bon_livraison where id='" + a + "'";
            return DataReturn(req_select);
        }
        public DataTable affiche_cmdFR_infos(int a)
        {
            function_test();
            req_select = "SELECT * FROM Commandefr where id_commande=@id_commande";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@id_commande", (object)a));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }

        public DataTable affiche_infos_societe()
        {
            function_test();
            req_select = "SELECT * FROM societe ";
            return DataReturn(req_select);
        }

        public DataTable affiche_infos_feur(int id)
        {
            function_test();
            req_select = "SELECT * FROM commande where id=@id";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@id", (object)id));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
           // return DataReturn(req_select);
        }

        public DataTable affiche_infos_feur2(string id)
        {
            function_test();
            req_select = "SELECT * FROM fournisseur where code_feur= '" + id + "' ";
            return DataReturn(req_select);
        }

        public DataTable affiche_infos_clt(string clt)
        {
            function_test();
            req_select = "SELECT * FROM client where code_clt='" + clt + "'";
            return DataReturn(req_select);
        }
        // get informations sur la commande fournisseur
        public DataTable get_list_cde(int a)
        {
            function_test();
            req_select = "SELECT * FROM commande where id= '" + a + "'";
            return DataReturn(req_select);
        }
        public DataTable get_list_cdefr(int a)
        {
            function_test();
            req_select = "SELECT * FROM commandefr where id_commande=@id_commande";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@id_commande", (object)a));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;

           
        }
        public DataTable get_list_piece_from_cde(int a, string b)
        {
            function_test();
            req_select = "SELECT * FROM piece WHERE id_cde= '" + a + "' AND etat= '" + b + "' ";
            return DataReturn(req_select);

        }

        public DataTable get_list_piece_from_cdefr(int a)
        {
            function_test();
            req_select = "SELECT * FROM piece_commandefr WHERE id_commande=@id_commande";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@id_commande", (object)a));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;

        }
        public DataTable get_id_cde(string code, string b)
        {
            function_test();
            req_select = "SELECT * FROM commande WHERE etat = '" + code + "' AND id_feur= '" + b + "'  ";
            //return DataReturn(req_select);
            DataSet ds = new DataSet();
            da = new SqlDataAdapter(req_select, conn);
            da.Fill(ds, "table");
            DataTable data = new DataTable();
            data = ds.Tables["table"];
            return data;
        }
        public DataTable test_dispo_piece(string lib)
        {
            function_test();
            req_select = "SELECT * FROM stock WHERE libelle_piece = '" + lib + "' ";
            //return DataReturn(req_select);
            DataSet ds = new DataSet();
            da = new SqlDataAdapter(req_select, conn);
            da.Fill(ds, "table");
            DataTable data = new DataTable();
            data = ds.Tables["table"];
            return data;
        }
        public DataTable test_dispo_piececode(string lib)
        {
            function_test();
            req_select = "SELECT * FROM stock WHERE code_piece = '" + lib + "' ";
            //return DataReturn(req_select);
            DataSet ds = new DataSet();
            da = new SqlDataAdapter(req_select, conn);
            da.Fill(ds, "table");
            DataTable data = new DataTable();
            data = ds.Tables["table"];
            return data;
        }
        public DataTable test_ajout_piece_cde(int id)
        {
            function_test();
            req_select = "SELECT * FROM piece WHERE id_piece = '" + id + "' ";
            return DataReturn(req_select);
        }
        public DataTable get_etat_envoie(int code)
        {
            function_test();
            req_select = "SELECT * FROM Appointments WHERE UniqueID = '" + code + "' ";
            return DataReturn(req_select);
        }

        // afficher technicien dans grid
        public DataTable affiche_tech()
        {
            function_test();
            req_select = "SELECT * FROM intervenant";
            return DataReturn(req_select);
        }
        // afficher sous traitant dans grid
        public DataTable affiche_sous_trait()
        {
            function_test();
            req_select = "SELECT * FROM sous_traitant";
            return DataReturn(req_select);
        }

        //test si utilisateur existe ou pas
        public DataTable test_user(string pseudo)
        {
            function_test();
            req_select = "SELECT * FROM droit WHERE login =@login";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@login", (object)pseudo));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
            //return DataReturn(req_select);
        }
        //afficher liste des utilisateurs dans grid
        public DataTable grid_user()
        {
            function_test();
            req_select = "SELECT * FROM droit";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }

        public DataTable select_info_from_cde(int id)
        {
            function_test();
            req_select = "SELECT * FROM Appointments WHERE UniqueID = '" + id + "'";
            return DataReturn(req_select);
        }

        public DataTable get_skin(int id)
        {
            function_test();
            req_select = "SELECT skin FROM droit WHERE id_user = '" + id + "' ";
            return DataReturn(req_select);
        }
        public DataTable get_etat_comde(int id)
        {
            if (sql_gmao.conn.State.ToString().Equals("Closed"))
            {
                sql_gmao.conn.Open();
            }
            function_test();
            req_select = "SELECT * FROM Appointments WHERE UniqueID = '" + id + "'";
            return DataReturn(req_select);
        }
        public DataTable get_mesuresById(int code)
        {
            function_test();
            req_select = "SELECT * FROM mesures WHERE id_mesure = '" + code + "'";
            return DataReturn(req_select);
        }
        public DataTable get_NomIntervenantInterneByCode(string code)
        {
            function_test();
            req_select = "SELECT nom_intervenant FROM intervenant WHERE code_intervenant = '" + code + "'";
            return DataReturn(req_select);
        }
        public DataTable get_AdminById(int idAdm)
        {
            function_test();
            req_select = "SELECT * FROM droit WHERE id_user = '" + idAdm + "'";
            return DataReturn(req_select);
        }
        public DataTable get_AllActions()
        {
            function_test();
            req_select = "SELECT * FROM historique ORDER BY date_hist DESC";
            return DataReturn(req_select);
        }
        public DataTable LoginExist(string login)
        {
            function_test();
            req_select = "SELECT * FROM droit WHERE login = '" + login + "'";
            return DataReturn(req_select);
        }
        public DataTable get_AllAlert()
        {
            function_test();
            req_select = "SELECT * FROM notification ORDER BY date_notif DESC";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            
            // return DataReturn(req_select);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public DataTable get_PSales(string codeP, DateTime date1, DateTime date2)
        {
            function_test();
            //req_select = "SELECT libelle_piece_u,quantite_piece_u,id_fact,id_clt FROM piece_fact WHERE code_piece_u=" + codeP ;
            req_select = "SELECT * FROM piece_fact WHERE code_piece_u=@code_piece_u and id_fact IN ( SELECT id FROM facturevente WHERE cast(substring(date_ajout,7,4)+'/'+substring(date_ajout,4,2)+'/'+substring(date_ajout,1,2) as datetime) between cast('" + date1.Year.ToString() + "/" + date1.Month.ToString() + "/" + date1.Day.ToString() + "' as datetime) and cast('" + date2.Year.ToString() + "/" + date2.Month.ToString() + "/" + date2.Day.ToString() + "' as datetime) and avoir='0')";
            // id_piece=" + codeP+";  
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@code_piece_u", (object)codeP));
            
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }

        public DataTable get_AllAlertForAdmin()
        {
            function_test();
            req_select = "SELECT * FROM notification WHERE depar_notif='None' OR depar_notif='Utilisateur, Gestion de Projet' ORDER BY date_notif DESC";
            return DataReturn(req_select);
        }
        public DataTable get_AllAlertForStock()
        {
            function_test();
            req_select = "SELECT * FROM notification WHERE depar_notif='None' OR depar_notif='Administrateur' OR depar_notif='Gestion de Projet, Administrateur' ORDER BY date_notif DESC";
            return DataReturn(req_select);
        }
        public DataTable get_AllAlertForProjet()
        {
            function_test();
            req_select = "SELECT * FROM notification WHERE depar_notif='Utilisateur, Administrateur' OR depar_notif='Administrateur' ORDER BY date_notif DESC";
            return DataReturn(req_select);
        }
        public DataTable get_NotifById(int code)
        {
            function_test();
            req_select = "SELECT * FROM notification WHERE id_notif ='" + code + "'";
            return DataReturn(req_select);
        }
        public string vider_Actions()
        {
            function_test();
            string req = "TRUNCATE TABLE historique";
            //suivi_actions("Suppression l’historique de toutes les actions");
            return DataExcute(req);
        }
        public DataTable get_AlertSeuil()
        {
            function_test();
            req_select = "SELECT * FROM stock WHERE quantite_piece <= seuil_piece";
            SqlCommand cmd = new SqlCommand(req_select, conn);
           
            // return DataReturn(req_select);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public DataTable get_AlertCompteur()
        {
            function_test();
            req_select = "SELECT * FROM compteur WHERE valeur_compteur >= limite_supp_compteur";
            return DataReturn(req_select);
        }
        public DataTable Notif_exist(string typeN, string code)
        {
            function_test();
            req_select = "SELECT * FROM notification WHERE titre_notif =@titre_notif AND code_relative_notif =@code_relative_notif";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@titre_notif", (object)typeN));
            cmd.Parameters.Add(new SqlParameter("@code_relative_notif", (object)code));
            // return DataReturn(req_select);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
           
        }
        public DataTable get_AlertCommandes()
        {
            function_test();
            req_select = "SELECT * FROM commande WHERE etat ='Envoyée'";
            return DataReturn(req_select);
        }
        public DataTable get_max_fac2(int code_clt, string etat_fact, string etat_liv)
        {
            function_test();
            req_select = "SELECT id FROM facture WHERE id_clt= '" + code_clt + "' AND (etat_fact ='" + etat_fact + "' OR etat_bon_liv ='" + etat_liv + "') ";
            return DataReturn(req_select);
        }

        public DataTable get_max_fac(string code_clt, string etat)
        {
            function_test();
            req_select = "SELECT id FROM facture WHERE id_clt= '" + code_clt + "' AND etat ='" + etat + "' ";
            return DataReturn(req_select);
        }

        public DataTable get_max_Commande()
        {
            function_test();
            req_select = "SELECT MAX(id_commande) FROM CommandeClient ";

            return DataReturn(req_select);
        }
        public DataTable get_max_Commandefr()
        {
            function_test();
            req_select = "SELECT MAX(id_commande) FROM Commandefr ";

            return DataReturn(req_select);
        }
        public DataTable get_max_BL()
        {
            function_test();
            req_select = "SELECT MAX(id) FROM bon_livraison ";

            return DataReturn(req_select);
        }
        public DataTable get_max_FactPf()
        {
            function_test();
            req_select = "SELECT MAX(id) FROM facturePerformat ";

            return DataReturn(req_select);
        }
        public DataTable get_max_devis()
        {
            function_test();
            req_select = "SELECT MAX(id) FROM devis ";

            return DataReturn(req_select);
        }
        public string resetautoincrement(string tablename, int x)
        {
            function_test();
            string req = "DBCC CHECKIDENT (" + tablename + ",RESEED," + x + ")";

            return DataExcute(req);
        }
        public DataTable getcountcmd(string tablename)
        {
            function_test();
            req_select = "SELECT * from " + tablename;

            return DataReturn(req_select);
        }
        public DataTable getcurrentvalue(string tablename)
        {
            function_test();
            req_select = "SELECT IDENT_CURRENT('" + tablename + "')";

            return DataReturn(req_select);
        }
        public DataTable get_max_devis(string etat)
        {
            function_test();
            req_select = "SELECT MAX(id) FROM devis WHERE etat= '" + etat + "'";
            return DataReturn(req_select);
        }
        public DataTable get_max_fact(string etat)
        {
            function_test();
            req_select = "SELECT MAX(id) FROM facture WHERE etat= '" + etat + "'";
            return DataReturn(req_select);
        }
        public DataTable get_maxfactt()
        {
            function_test();
            req_select = "SELECT MAX(id) FROM facture ";
            return DataReturn(req_select);
        }

        public DataTable get_infos_piece(string code)
        {
            function_test();
            req_select = "SELECT * FROM stock WHERE code_piece=@code_piece";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@code_piece", (object)code));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }

        public DataTable cheak_etat2(int id_fact)
        {
            function_test();
            req_select = "SELECT etat FROM facture WHERE id= '" + id_fact + "' ";
            return DataReturn(req_select);
        }

        public DataTable get_AlertMesures()
        {
            function_test();
            req_select = "SELECT * FROM mesures WHERE valeur_actuelle_mesures <= limite_supp_mesures";
            return DataReturn(req_select);
        }
        public DataTable get_equip(int c)
        {
            function_test();
            req_select = "SELECT * from equipement WHERE iImageIndex= '" + c + "'";
            return DataReturn(req_select);
        }
        public DataTable get_niveau(int a, int b)
        {
            function_test();
            req_select = "SELECT * from equipement WHERE iImageIndex= '" + a + "' OR iImageIndex= '" + b + "' ";
            return DataReturn(req_select);
        }
        public DataTable get_piece4(string piece, int code)
        {
            function_test();
            req_select = "SELECT * FROM piece WHERE libelle_piece_u= '" + piece + "' AND UniqueID = '" + code + "'";
            return DataReturn(req_select);
        }
        public DataTable get_allstock()
        {
            function_test();
            req_select = "SELECT * FROM stock ";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
           // return DataReturn(req_select);
        }
        public DataTable get_allstock2(string famill)
        {
            function_test();
            req_select = "SELECT * FROM stock where nature=@nature";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@nature", (object)famill));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
            
        }
        public DataTable select_from_cde(string etat, string req_feur)
        {
            function_test();
            string req = "SELECT * FROM commande WHERE etat= '" + etat + "' AND id_feur = '" + req_feur + "'";
            //return DataReturn(req_select);
            DataSet ds = new DataSet();
            da = new SqlDataAdapter(req, conn);
            da.Fill(ds, "table");
            DataTable data = new DataTable();
            data = ds.Tables["table"];
            return data;

        }
        public DataTable select_etoile_from_piece(string req_feur, string c, string f, int id_cde)
        {
            function_test();
            string req = "SELECT * FROM piece WHERE id_feur= '" + req_feur + "' AND libelle_piece_u = '" + c + "' AND etat= '" + f + "' AND id_cde= '" + id_cde + "'";
            //return DataReturn(req_select);
            DataSet ds = new DataSet();
            da = new SqlDataAdapter(req, conn);
            da.Fill(ds, "table");
            DataTable data = new DataTable();
            data = ds.Tables["table"];
            return data;
        }

        public DataTable select_etoile_from_piece_fact(string req_clt, string code_piece, string etat, int id_fac)
        {
            function_test();
            string req = "SELECT * FROM piece_fact WHERE id_clt= '" + req_clt + "' AND code_piece_u = '" + code_piece + "' AND etat= '" + etat + "' AND id_fact= '" + id_fac + "'";
            //return DataReturn(req_select);
            DataSet ds = new DataSet();
            da = new SqlDataAdapter(req, conn);
            da.Fill(ds, "table");
            DataTable data = new DataTable();
            data = ds.Tables["table"];
            return data;
        }
        public DataTable selectpiece_factbycodefact(int idfact)
        {
            function_test();
            string req = "SELECT * FROM piece_fact WHERE id_fact= '" + idfact + "'";
            DataSet ds = new DataSet();
            da = new SqlDataAdapter(req, conn);
            da.Fill(ds, "table");
            DataTable data = new DataTable();
            data = ds.Tables["table"];
            return data;

        }
        public DataTable selectpiece_factprofbycodefact(int idfact)
        {
            function_test();
            string req = "SELECT * FROM piece_factpf WHERE id_factpf= '" + idfact + "'";
            DataSet ds = new DataSet();
            da = new SqlDataAdapter(req, conn);
            da.Fill(ds, "table");
            DataTable data = new DataTable();
            data = ds.Tables["table"];
            return data;

        }

        public DataTable select_max_fact()
        {
            function_test();
            string req = "SELECT MAX(id) FROM facture";
            DataSet ds = new DataSet();
            da = new SqlDataAdapter(req, conn);
            da.Fill(ds, "table");
            DataTable data = new DataTable();
            data = ds.Tables["table"];
            return data;
        }

        public DataTable select_max_id_cde(string req_feur, string etat)
        {
            function_test();
            string req = "SELECT MAX(id) FROM commande WHERE id_feur= '" + req_feur + "' AND etat= '" + etat + "'";
            //return DataReturn(req_select);
            DataSet ds = new DataSet();
            da = new SqlDataAdapter(req, conn);
            da.Fill(ds, "table");
            DataTable data = new DataTable();
            data = ds.Tables["table"];
            return data;
        }

        public DataTable test_dispo_tech(string code)
        {
            function_test();
            req_select = "SELECT * FROM intervenant WHERE code_intervenant= '" + code + "'";
            return DataReturn(req_select);
        }
        public DataTable test_dispo_sous_taritant(string code)
        {
            function_test();
            req_select = "SELECT * FROM sous_traitant WHERE code_soustraitant= '" + code + "'";
            return DataReturn(req_select);
        }
        public DataTable get_Societe()
        {
            function_test();
            string req_select = "SELECT * FROM societe";
            return DataReturn(req_select);
        }
        public DataTable select_from_société()
        {
            function_test();
            req_select = "SELECT * FROM societe";
            return DataReturn(req_select);
        }
        public DataTable get_mesure_tableau_bord()
        {
            function_test();
            req_select = "SELECT m.id_mesure, m.designation_mesures, m.unite_mesures, m.limite_inf_mesures, m.limite_supp_mesures, m.valeur_actuelle_mesures, p.nom_intervenant, p.prenom_intervenant, p.email_intervenant, p.mobile_intervenant FROM mesures m, intervenant p WHERE  m.code_intervenant = p.code_intervenant AND m.valeur_actuelle_mesures >= m.limite_supp_mesures";
            return DataReturn(req_select);
        }
        public DataTable get_3days_appointments()
        {
            function_test();
            req_select = "SELECT UniqueID, StartDate, EndDate, Subject, Location, Description, equipement, importance FROM Appointments WHERE Appointments.StartDate BETWEEN (GETDATE()-1)AND (GETDATE()+3)";
            return DataReturn(req_select);
        }
        public DataTable get_UniteByDes(string des)
        {
            function_test();
            req_select = "SELECT * FROM unite WHERE designation_unite=@designation_unite";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@designation_unite", (object)des));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public DataTable check_contrat(string code)
        {
            function_test();
            req_select = "SELECT * FROM contrat_garantie WHERE code_contrat_g= '" + code + "'";
            return DataReturn(req_select);
        }
        public string set_contrat(string code, string date_deb, string date_fin)
        {
            function_test();
            string req = "INSERT INTO contrat_garantie (code_contrat_g, date_debut_contrat_g, date_fin_contrat_g) VALUES ('" + code + "','" + date_deb + "','" + date_fin + "')";
            suivi_actions("Modification des données concernant un contrat d’equipement. Code de contrat: ( " + code + " ).");
            return DataExcute(req);
        }


        public string set_bon_sortie(string libelle, int code_fact, string etat, string code_clt, string client)
        {
            function_test();
            string req = "INSERT INTO bon_sortie (libelle, code_fact, date, etat, code_clt, client) VALUES ('" + libelle + "','" + code_fact + "','" + System.DateTime.Now.ToLongDateString() + "', '" + etat + "', '" + code_clt + "', '" + client + "')";
            suivi_actions("Création d'un bon de sortie: commande n°( " + code_fact + " ).");
            return DataExcute(req);
        }

        public DataTable get_AllCMDAlert()
        {
            function_test();
            req_select = "SELECT * FROM notification WHERE depar_notif !='None' ORDER BY date_notif DESC";
            return DataReturn(req_select);
        }
        public DataTable get_AllprodbyCMD(string nbcmd)
        {
            function_test();
            req_select = "SELECT * FROM piece_commande WHERE id_commande = @id_commande";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@id_commande", (object)nbcmd));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public DataTable get_Allprodbycmdnonull(string nbcmd)
        {
            function_test();
            string req = "SELECT * FROM piece_commande WHERE id_commande =@id_commande and qterest != " + 0;
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@id_commande", (object)nbcmd));
            
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public DataTable get_Allprodbybl(string nbcmd)
        {
            function_test();
            req_select = "SELECT * FROM piece_bl WHERE id_commande =@id_commande";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@id_commande", (object)nbcmd));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
       
        public DataTable get_Allprodbynumbl(string nbl)
        {
            function_test();
            req_select = "SELECT * FROM piece_bl WHERE id_piece like'" + nbl + "'";
            return DataReturn(req_select);
        }
        public DataTable getbl(int nbl)
        {
            function_test();
            req_select = "SELECT * FROM bon_livraison WHERE id =@id";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@id", (object)nbl));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public DataTable get_piececmd(string nbcmd, string codep)
        {
            function_test();
            req_select = "SELECT * FROM piece_commande WHERE id_commande like'" + nbcmd + "' and code_art like '" + codep + "'";
            return DataReturn(req_select);
        }
        public DataTable get_piececmdbynump(int codep)
        {
            function_test();
            req_select = "SELECT * FROM piece_commande WHERE id_piece =@id_piece";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@id_piece", (object)codep));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public DataTable get_piececmdbycode(string codep)
        {
            function_test();
            req_select = "SELECT * FROM piece_commande WHERE code_art ='" + codep + "'";
            return DataReturn(req_select);
        }
        public DataTable get_piececmdbycode2(string codep,int id_cmd)
        {
            function_test();
            req_select = "SELECT * FROM piece_commande WHERE code_art =@code_art and id_commande=@id_commande";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@code_art", (object)codep));
            cmd.Parameters.Add(new SqlParameter("@id_commande", (object)id_cmd));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
            
        }
        public DataTable get_cmdcltbyid(int codep)
        {
            function_test();
            req_select = "SELECT * FROM CommandeClient WHERE id_commande =@id_commande";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@id_commande", (object)codep));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public string set_notificationCde(string titre, string des, string code_relative, string depar)
        {
            function_test();
            DateTime notifDate = DateTime.Now;
            string req = "INSERT INTO notification (titre_notif, des_notif, code_relative_notif,date_notif,vu_notif,depar_notif) VALUES ('" + titre + "','" + des + "','" + code_relative + "','" + notifDate + "','None','" + depar + "')";
            return DataExcute(req);
        }

        public DataTable get_listetousinterventionINT()
        {
            function_test();
            req_select = "SELECT * FROM intervention ORDER BY date_intervention DESC";
            return DataReturn(req_select);
        }

        public DataTable getallclientbydesign(string design)
        {

            function_test();
            req_select = "SELECT * FROM client where raison_soc like '%" + design + "%'";
            return DataReturn(req_select);
        }

        public DataTable get_listetousinterventionodDay()
        {
            function_test();
            req_select = "SELECT * FROM intervention WHERE date_intervention = '" + DateTime.Now.ToShortDateString() + "' ORDER BY date_intervention DESC";
            return DataReturn(req_select);
        }
        public DataTable get_listetousinterventionEXT()
        {
            function_test();
            req_select = "SELECT * FROM intervention a, equipement eq, intervenant ii, sous_traitant iex WHERE a.code_eq=eq.Code_eq AND a.code_intervenant = ii.code_intervenant AND a.code_soustraitant iex.code_soustraitant ORDER BY a.date_intervention ASC";
            return DataReturn(req_select);
        }
        //avoir
        public string insert_piecee_avoir(string codep, string lib, string quantit, string unite, string puv, string remise, string ttva, string idclt, int id_cmd, string pv, string id_prod)
        {

            function_test();
            string req = "INSERT INTO piece_avoir (code_art, libelle_piece, quantite_piece,unit, id_clt,  puv,remise,ttva,id_commande,totvente,id_prod) VALUES" +
" (@code_art,@libelle_piece,@quantite_piece,@unit,@id_clt,@puv,@remise,@ttva,@id_commande,@totvente,@id_prod)";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@code_art", (object)codep));
            cmd.Parameters.Add(new SqlParameter("@libelle_piece", (object)lib));
            cmd.Parameters.Add(new SqlParameter("@quantite_piece", (object)quantit));
            cmd.Parameters.Add(new SqlParameter("@unit", (object)unite));
            cmd.Parameters.Add(new SqlParameter("@id_clt", (object)idclt));
            cmd.Parameters.Add(new SqlParameter("@puv", (object)puv));
            cmd.Parameters.Add(new SqlParameter("@remise", (object)remise));
            cmd.Parameters.Add(new SqlParameter("@ttva", (object)ttva));
            cmd.Parameters.Add(new SqlParameter("@id_commande", (object)id_cmd));
            cmd.Parameters.Add(new SqlParameter("@totvente", (object)pv));
            cmd.Parameters.Add(new SqlParameter("@id_prod", (object)id_prod));
            cmd.ExecuteNonQuery();
            suivi_actions("Ajout cmd : ( " + lib + " ) avec la quantité : ( " + quantit + " ) au commande client n° ( " + id_cmd + " ).");
            return "";
        }
        public string update_sousstock_avoir2(Double qt, string id_prod)
        {
            function_test();
            string req = "UPDATE stock SET quantite_piece =cast(quantite_piece as real)+@quantite_piece WHERE id= @id_prod";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@quantite_piece", (object)qt));
            cmd.Parameters.Add(new SqlParameter("@id_prod", (object)id_prod));
            cmd.ExecuteNonQuery();
            suivi_actions("Retour de piéces ID ( " + id_prod + " ) Quantité:( " + qt + " ) pour une commande client");
            return "";
        }
        public string insert_into_avoir(string numero_fact, string etat, string date, string id_clt, string client, string montantht, string prixtotc, string timbre, string num_cmd, string tva, string remise)
        {
            function_test();
            string req = "INSERT INTO Avoir (date_ajout, client,id_clt,montant_ht,montant_ttc,timbre,numero,tva,remise,etat,numero_fact) VALUES" +
" (@date_ajout, @client,@id_clt,@montant_ht,@montant_ttc,@timbre,@numero,@tva,@remise,@etat,@numero_fact)";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@date_ajout", (object)date));
            cmd.Parameters.Add(new SqlParameter("@client", (object)client));
            cmd.Parameters.Add(new SqlParameter("@id_clt", (object)id_clt));
            cmd.Parameters.Add(new SqlParameter("@montant_ht", (object)montantht));
            cmd.Parameters.Add(new SqlParameter("@montant_ttc", (object)prixtotc));
            cmd.Parameters.Add(new SqlParameter("@timbre", (object)timbre));
            cmd.Parameters.Add(new SqlParameter("@etat", (object)etat));
            cmd.Parameters.Add(new SqlParameter("@numero", (object)num_cmd.Trim()));
            cmd.Parameters.Add(new SqlParameter("@tva", (object)tva));
            cmd.Parameters.Add(new SqlParameter("@remise", (object)remise));
            cmd.Parameters.Add(new SqlParameter("@numero_fact", (object)numero_fact));
            cmd.ExecuteNonQuery();
            suivi_actions("Ajout d’une commandeclient pour le client " + client + ".");
            return "";
        }
        public string update_avoir(string numero_fact, int id,string etat, string date, string id_clt, string client, string montantht, string prixtotc, string timbre, string num_cmd, string tva, string remise)
        {
            function_test();
            string req = "update  Avoir set numero_fact=@numero_fact, etat=@etat, date_ajout=@date_ajout, client=@client,id_clt=@id_clt,montant_ht=@montant_ht,montant_ttc=@montant_ttc,timbre=@timbre,numero=@numero,tva=@tva,remise=@remise where id=@id";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@date_ajout", (object)date));
            cmd.Parameters.Add(new SqlParameter("@client", (object)client));
            cmd.Parameters.Add(new SqlParameter("@id_clt", (object)id_clt));
            cmd.Parameters.Add(new SqlParameter("@montant_ht", (object)montantht));
            cmd.Parameters.Add(new SqlParameter("@montant_ttc", (object)prixtotc));
            cmd.Parameters.Add(new SqlParameter("@timbre", (object)timbre));
            cmd.Parameters.Add(new SqlParameter("@etat", (object)etat));
            cmd.Parameters.Add(new SqlParameter("@numero", (object)num_cmd.Trim()));
            cmd.Parameters.Add(new SqlParameter("@tva", (object)tva));
            cmd.Parameters.Add(new SqlParameter("@remise", (object)remise));
            cmd.Parameters.Add(new SqlParameter("@id", (object)id));
            cmd.Parameters.Add(new SqlParameter("@numero_fact", (object)numero_fact));
            cmd.ExecuteNonQuery();
            suivi_actions("Modification d'avoir n° " + num_cmd + ".");
            return "";
        }
        public void updateEtatAvoir(int id, string etat)
        {
            function_test();
            string req = "update  Avoir set etat=@etat where id=@id";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@etat", (object)etat));
            cmd.Parameters.Add(new SqlParameter("@id", (object)id));
            cmd.ExecuteNonQuery();
            suivi_actions("Modification d'avoir ID" + id + ".");
            
        }
        public void updateEtatAvoirFactureVente(string numero_fact, Boolean avoir)
        {
            function_test();
            string req = "update  facturevente set avoir=@avoir where numero_fact=@numero_fact";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@avoir", (object)avoir));
            cmd.Parameters.Add(new SqlParameter("@numero_fact", (object)numero_fact));
            cmd.ExecuteNonQuery();
            suivi_actions("Modification facture vente avoir N" + numero_fact + ".");

        }
        public DataTable Listavoir()
        {

            function_test();
            req_select = "SELECT *  FROM Avoir ";
            SqlCommand cmd = new SqlCommand(req_select, conn);
           
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;

        }
        public DataTable max_num_avoir()
        {

            function_test();
            req_select = "SELECT MAX(cast(numero as int)) as max FROM Avoir ";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;

        }
        public DataTable selectfromavoirs()
        {
            function_test();
            string req_select = "SELECT * FROM Avoir ";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public DataTable selectfromavoirsByNum(string numero)
        {
            function_test();
            string req_select = "SELECT * FROM Avoir where numero=@numero ";
            SqlCommand cmd = new SqlCommand(req_select, conn);
            cmd.Parameters.Add(new SqlParameter("@numero", (object)numero));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public string delete_piece_avoir(int code)
        {
            function_test();
            string req = "DELETE FROM piece_avoir WHERE id_commande=@id_commande";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@id_commande", (object)code));
            
            cmd.ExecuteNonQuery();
            return "";
        }
        public string delete_avoir(int code)
        {
            function_test();
            string req = "DELETE FROM Avoir WHERE numero=@numero";
            SqlCommand cmd = new SqlCommand(req, conn);
            cmd.Parameters.Add(new SqlParameter("@numero", (object)code));
            
            cmd.ExecuteNonQuery();
            suivi_actions("Suppression de la facture client  n° " + code + ".");
            return DataExcute(req);
        }
        public DataTable selectfromavoirsByNum(int num)
        {
            function_test();
            string req_select = "SELECT * FROM Avoir where numero=@numero";
            SqlCommand cmd = new SqlCommand(req_select, conn);
           
            cmd.Parameters.Add(new SqlParameter("@numero", (object)num));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
        public DataTable selectPieceAvoirsByNum(int num)
        {
            function_test();
            string req_select = "SELECT * FROM piece_avoir where id_commande=@id_commande";
            SqlCommand cmd = new SqlCommand(req_select, conn);

            cmd.Parameters.Add(new SqlParameter("@id_commande", (object)num));
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
    }
}