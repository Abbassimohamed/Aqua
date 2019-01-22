using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.IO;
using System.IO.IsolatedStorage;

namespace RibbonSimplePad.Report
{
    public partial class BLReport : DevExpress.XtraReports.UI.XtraReport
    {
        sql_gmao fun = new sql_gmao();
        int id_bl;
        DataTable dt_Piece_bl, dt_bl;
        double tva = 0, tt_htva = 0, tt_net_htva = 0, tt_tva = 0;
        public BLReport(int idbl)
        {
            InitializeComponent();
            id_bl = idbl;
            dt_bl = fun.affiche_BL_infos(id_bl);
            dt_Piece_bl = fun.get_Allprodbybl(id_bl.ToString());
        }
        public void GetHead()
        {
            DataTable vv = new DataTable();
            vv = fun.affiche_infos_societe();
            byte[] IMG = (Byte[])(vv.Rows[0]["logo_societe"]);
            MemoryStream mem = new MemoryStream(IMG);
            //xrPictureBox1.Image = Image.FromStream(mem);

            //info client
            if (dt_bl.Rows.Count > 0)
            {
                DataTable dt_client = fun.get_cltByCode(dt_bl.Rows[0]["id_clt"].ToString());

                xrLabel1.Text = "Client : " + dt_client.Rows[0]["raison_soc"].ToString();
                xrLabel2.Text = "Adresse : " + dt_client.Rows[0]["adresse_clt"].ToString();
                string cp =dt_client.Rows[0]["cp_clt"].ToString();
                if (cp == "0")
                {
                    cp = "";
                }
                xrLabel3.Text = "Code postal : " + cp;
                xrLabel49.Text = "Ville : " + dt_client.Rows[0]["ville_clt"].ToString();
                xrLabel4.Text = "Tél : " + dt_client.Rows[0]["tel_clt"].ToString();
                xrLabel50.Text = "Fax : " + dt_client.Rows[0]["fax_clt"].ToString();
                xrLabel17.Text = "MF : " + dt_client.Rows[0]["forme_juriduque"].ToString();
                xrLabel24.Text = "Code Client : " + dt_client.Rows[0]["code"].ToString();
                double num = double.Parse(dt_bl.Rows[0]["numero_bl"].ToString());
                LabNumero.Text = "" + num.ToString("00000");
                LabDate.Text = "" + dt_bl.Rows[0]["date_ajout"].ToString().Substring(0, 10);
                
                xrLabel6.Text = dt_bl.Rows[0]["nbcmd"].ToString();
                xrLabel9.Text = dt_bl.Rows[0]["mode_livraison"].ToString();
                xrLabel27.Text = dt_bl.Rows[0]["moyen_livraison"].ToString();
                xrLabel29.Text = dt_bl.Rows[0]["lieu_livraison"].ToString(); 
            }
        }

        public void Listpiece()
        {
            //
            xrTable1.BeginInit();
            int inde = xrTable1.Rows.Count - 1;

            xrTable1.InsertRowBelow(xrTable1.Rows[inde]);

            //int gd = xrTable1.Rows.Count;
            xrTable1.Rows[0].Cells[0].Text = "";
            xrTable1.Rows[0].Cells[1].Text = "";
            xrTable1.Rows[0].Cells[2].Text = "";
            xrTable1.Rows[0].Cells[3].Text = "";

            xrTable1.Rows[0].Cells[4].Text = "";

            xrTable1.Rows[0].Cells[5].Text = "";
            xrTable1.Rows[0].Cells[6].Text = "";
            xrTable1.Rows[0].Cells[7].Text = "";
            xrTable1.Rows[0].Cells[8].Text = "";
            xrTable1.EndInit();
            //
            for (int i = 0; i < dt_Piece_bl.Rows.Count; i++)
            {

                xrTable1.BeginInit();
                int indx = xrTable1.Rows.Count - 1;
                xrTable1.InsertRowBelow(xrTable1.Rows[i]);
                int a = i+1;
                //code
                xrTable1.Rows[a].Cells[0].Text = dt_Piece_bl.Rows[i]["code_art"].ToString();
                xrTable1.Rows[a].Cells[0].TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrTable1.Rows[a].Cells[0].Multiline = true;
                //description
                xrTable1.Rows[a].Cells[1].Text = dt_Piece_bl.Rows[i]["libelle_piece"].ToString();
                xrTable1.Rows[a].Cells[1].TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                xrTable1.Rows[a].Cells[1].Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0);
                xrTable1.Rows[a].Cells[1].Multiline = true;
                //unite
                xrTable1.Rows[a].Cells[2].Text = dt_Piece_bl.Rows[i]["unit"].ToString();
                xrTable1.Rows[a].Cells[2].TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrTable1.Rows[a].Cells[2].Multiline = true;
                //quantite
                string qt1 = dt_Piece_bl.Rows[i]["quantite_piece"].ToString();
                xrTable1.Rows[a].Cells[3].Text = qt1;
                xrTable1.Rows[a].Cells[3].TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrTable1.Rows[a].Cells[3].Multiline = true;
                //puv
                //string puv1 = dt_piece.Rows[i]["puv"].ToString();
                ////tt htv et remise
                //tt_htva += double.Parse(qt1.Replace('.', ',')) * double.Parse(puv1.Replace('.', ','));

                xrTable1.Rows[a].Cells[4].Text = "";// puv1;
                xrTable1.Rows[a].Cells[4].TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrTable1.Rows[a].Cells[4].Multiline = true;

                //string remise1 = dt_piece.Rows[i]["remise"].ToString();
                xrTable1.Rows[a].Cells[5].Text = "";// remise1;
                xrTable1.Rows[a].Cells[5].TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrTable1.Rows[a].Cells[5].Multiline = true;

                //string pu_net1 = (double.Parse(puv1.Replace('.', ',')) - ((double.Parse(puv1.Replace('.', ',')) * double.Parse(remise1.Replace('.', ','))) / 100)).ToString("0.000");
                xrTable1.Rows[a].Cells[6].Text = "";// pu_net1;

                //tt_net_htva += double.Parse(pu_net1.Replace('.', ',')) * double.Parse(qt1.Replace('.', ','));

                xrTable1.Rows[a].Cells[6].TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrTable1.Rows[a].Cells[6].Multiline = true;

                xrTable1.Rows[a].Cells[7].Text = "";// (double.Parse(pu_net1.Replace('.', ',')) * double.Parse(qt1.Replace('.', ','))).ToString("0.000"); ;
                xrTable1.Rows[a].Cells[7].TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrTable1.Rows[a].Cells[7].Multiline = true;

                xrTable1.Rows[a].Cells[8].Text = dt_Piece_bl.Rows[i]["ttva"].ToString();
                xrTable1.Rows[a].Cells[8].TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrTable1.Rows[a].Cells[8].Multiline = true;
                xrTable1.EndInit();
                //tva = double.Parse(dt_piece.Rows[i]["tva"].ToString());
                //tt_tva += (double.Parse(pu_net1.Replace('.', ',')) * double.Parse(dt_piece.Rows[i]["tva"].ToString().Replace('.', ','))) / 100;
            }
            if (xrTable1.Rows.Count < 13)
            {
                int g = 13 - xrTable1.Rows.Count;
                int index1 = xrTable1.Rows.Count + g;
                for (int k = xrTable1.Rows.Count; k < index1; k++)
                {
                    xrTable1.BeginInit();
                    int index2 = xrTable1.Rows.Count - 1;

                    xrTable1.InsertRowBelow(xrTable1.Rows[index2]);

                    int gd = xrTable1.Rows.Count;
                    xrTable1.Rows[k].Cells[0].Text = "";
                    xrTable1.Rows[k].Cells[1].Text = "";
                    xrTable1.Rows[k].Cells[2].Text = "";
                    xrTable1.Rows[k].Cells[3].Text = "";

                    xrTable1.Rows[k].Cells[4].Text = "";

                    xrTable1.Rows[k].Cells[5].Text = "";
                    xrTable1.Rows[k].Cells[6].Text = "";
                    xrTable1.Rows[k].Cells[7].Text = "";
                    xrTable1.Rows[k].Cells[8].Text = "";
                    xrTable1.EndInit();
                }
            }
        }
        private void BLReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            GetHead();
            Listpiece();
        }

    }
}
