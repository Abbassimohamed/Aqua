﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.IO;
using System.IO.IsolatedStorage;

namespace RibbonSimplePad.Report
{
    public partial class FacturePfReport : DevExpress.XtraReports.UI.XtraReport
    {
        sql_gmao fun = new sql_gmao();
        int id_pf;
        DataTable dt_Piece_fpf,dt_factPF;
        double tva = 0, tt_htva = 0, tt_net_htva = 0, tt_tva = 0;
        public FacturePfReport(int idPf)
        {
            InitializeComponent();
            id_pf = idPf;
            dt_Piece_fpf = fun.selectpiece_factprofbycodefact(id_pf);
            dt_factPF = fun.affichefacturepfinfos(id_pf);
        }
        public void GetHead()
        {
            DataTable vv = new DataTable();
            vv = fun.affiche_infos_societe();
            byte[] IMG = (Byte[])(vv.Rows[0]["logo_societe"]);
            MemoryStream mem = new MemoryStream(IMG);
           // xrPictureBox1.Image = Image.FromStream(mem);

            //info client
            if (dt_factPF.Rows.Count > 0)
            {
                DataTable dt_client = fun.get_cltByCode(dt_factPF.Rows[0]["id_clt"].ToString());

                xrLabel1.Text = "Client : " + dt_client.Rows[0]["raison_soc"].ToString();
                xrLabel2.Text = "Adresse : " + dt_client.Rows[0]["adresse_clt"].ToString();
                string cp = dt_client.Rows[0]["cp_clt"].ToString();
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
                double num = double.Parse(dt_factPF.Rows[0]["numero"].ToString());
                LabNumero.Text = "" +num.ToString("00000");
                LabDate.Text = "" + dt_factPF.Rows[0]["date_ajout"].ToString().Substring(0, 10);
                xrLabel11.Text =""+ dt_factPF.Rows[0]["id_cmd"].ToString();
                xrLabel47.Text =""+ dt_client.Rows[0]["mode_pay"].ToString();
                xrLabel23.Text = "" + dt_factPF.Rows[0]["n_bl"].ToString();
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
            for (int i = 0; i < dt_Piece_fpf.Rows.Count; i++)
            {

                xrTable1.BeginInit();
                int indx = xrTable1.Rows.Count - 1;
                xrTable1.InsertRowBelow(xrTable1.Rows[i]);
                int a = i+1;
                //code
                xrTable1.Rows[a].Cells[0].Text = dt_Piece_fpf.Rows[i]["code_art"].ToString();
                xrTable1.Rows[a].Cells[0].TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrTable1.Rows[a].Cells[0].Multiline = true;
                //description
                xrTable1.Rows[a].Cells[1].Text = dt_Piece_fpf.Rows[i]["libelle_piece"].ToString();
                xrTable1.Rows[a].Cells[1].TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                xrTable1.Rows[a].Cells[1].Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0);
                xrTable1.Rows[a].Cells[1].Multiline = true;
                //unite
                xrTable1.Rows[a].Cells[2].Text = dt_Piece_fpf.Rows[i]["unit"].ToString();
                xrTable1.Rows[a].Cells[2].TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrTable1.Rows[a].Cells[2].Multiline = true;
                //quantite
                string qt1 = dt_Piece_fpf.Rows[i]["quantite_piece"].ToString();
                xrTable1.Rows[a].Cells[3].Text = qt1;
                xrTable1.Rows[a].Cells[3].TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrTable1.Rows[a].Cells[3].Multiline = true;
                //puv
                string puv1 = dt_Piece_fpf.Rows[i]["puv"].ToString();
                //tt htv et remise
                tt_htva += double.Parse(qt1.Replace('.', ',')) * double.Parse(puv1.Replace('.', ','));
                double puv = double.Parse(puv1.Replace('.', ','));
                xrTable1.Rows[a].Cells[4].Text = puv.ToString("0.000") ;
                xrTable1.Rows[a].Cells[4].TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrTable1.Rows[a].Cells[4].Multiline = true;

                string remise1 = dt_Piece_fpf.Rows[i]["remise"].ToString();
                double remise = double.Parse(dt_Piece_fpf.Rows[i]["remise"].ToString().Replace('.', ','));
                xrTable1.Rows[a].Cells[5].Text = remise1;
                xrTable1.Rows[a].Cells[5].TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrTable1.Rows[a].Cells[5].Multiline = true;

                string pu_net1 = (double.Parse(puv1.Replace('.', ',')) - ((double.Parse(puv1.Replace('.', ',')) * double.Parse(remise1.Replace('.', ','))) / 100)).ToString("0.000");
               // double pu_net = double.Parse(pu_net1.Replace('.', ','));
                xrTable1.Rows[a].Cells[6].Text = pu_net1;//.ToString("0.000") ;

                tt_net_htva += double.Parse(pu_net1.Replace('.', ',')) * double.Parse(qt1.Replace('.', ','));

                xrTable1.Rows[a].Cells[6].TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrTable1.Rows[a].Cells[6].Multiline = true;

                xrTable1.Rows[a].Cells[7].Text = (double.Parse(pu_net1.Replace('.', ',')) * double.Parse(qt1.Replace('.', ','))).ToString("0.000"); ;
                xrTable1.Rows[a].Cells[7].TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrTable1.Rows[a].Cells[7].Multiline = true;

                xrTable1.Rows[a].Cells[8].Text = dt_Piece_fpf.Rows[i]["ttva"].ToString();
                xrTable1.Rows[a].Cells[8].TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrTable1.Rows[a].Cells[8].Multiline = true;
                xrTable1.EndInit();
                tva = double.Parse(dt_Piece_fpf.Rows[i]["ttva"].ToString());
                tt_tva += ((double.Parse(pu_net1.Replace('.', ','))* double.Parse(qt1.Replace('.', ','))) * double.Parse(dt_Piece_fpf.Rows[i]["ttva"].ToString().Replace('.', ','))) / 100;
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
        public void GetFooter()
        {
            xrLabel37.Text = tt_net_htva.ToString("0.000");// tt_net_htva.ToString();
            //xrLabel38.Text = tva.ToString();
            xrLabel39.Text = tva.ToString("0.00");
            xrLabel40.Text = tt_tva.ToString("0.000");
            xrLabel41.Text = tt_net_htva.ToString("0.000");
            xrLabel42.Text = tt_net_htva.ToString("0.000");
            xrLabel43.Text = tt_tva.ToString("0.000");
            double timbre = 0;
            double.TryParse(dt_factPF.Rows[0]["timbre"].ToString().Replace('.', ','), out timbre);
            xrLabel9.Text = (tt_tva + tt_net_htva + timbre).ToString("0.000");
            xrLabel44.Text = timbre.ToString("0.000");
           
            xrLabel51.Text = NumberToWords1((tt_tva + tt_net_htva + timbre).ToString("0.000"));
        }
        private void TopMargin_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }
        public static String NumberToWords1(String T)
        {
            String s1 = "";
            String s2 = "";
            String s = "";
            int i = 0;
            //String T= number.ToString();

            while (T[i] != ',' && i < T.Length)
            {
                s1 += T[i];
                i++;
            }
            int j = i + 1;
            while (j < T.Length)
            {


                s2 += T[j];
                j++;

            }
            //  MessageBox.Show(s2);
            s = NumberToWords(Convert.ToInt32(s1)) + " DINARS, " + NumberToWords(Convert.ToInt32(s2)) + " millimes";

            return (s);
        }
        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 1)
            {
                words += NumberToWords(number / 1000) + " mille ";
                number %= 1000;
            }
            if ((number / 1000) == 1)
            {
                words += " mille ";
                number %= 1000;
            }

            if ((number / 100) > 1)
            {
                words += NumberToWords(number / 100) + " cent ";
                number %= 100;
            }
            if ((number / 100) == 1)
            {
                words += " cent ";
                number %= 100;
            }
            if (number > 0)
            {
                // if (words != "")
                //;

                var unitsMap = new[] { "zero", "un", "deux", "trois", "quatre", "cinq", "six", "sept", "huit", "neuf", "dix", "onze", "douze", "treize", "quatorze", "quinze", "seize", "dix-sept", "dix-huit", "dix-neuf" };
                var tensMap = new[] { "zero", "dix", "vingt", "trente", "quarante", "cinquante", "soixante", "soixante-dix", "quatre-vingts", "quatre-vingt-dix" };

                if (number < 20)
                    words += unitsMap[number];

                else
                {
                    if (number > 60)
                    {
                        if ((number / 10) <= 7)
                        {
                            if (number % 7 == 0)
                            {
                                words += tensMap[7];
                            }
                            else
                            {
                                words += tensMap[6];
                                words += unitsMap[number - 60];
                            }
                        }
                        if ((number / 10) > 7)
                        {
                            if (number % 8 == 0)
                            {
                                words += tensMap[8];
                            }
                            else
                            {
                                words += tensMap[8];
                                words += unitsMap[number - 80];
                            }
                        }
                        return words;
                    }
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }

        private void FacturePfReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            GetHead();
            Listpiece();
            GetFooter();
        }
    }
}
