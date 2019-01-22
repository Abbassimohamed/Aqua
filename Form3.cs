using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace Somertex
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            
        }
        private void printReicept()
        {
            PrintDialog printDialog = new PrintDialog();
            PrintDocument printdocument = new PrintDocument();
            printDialog.Document = printdocument;
            printdocument.PrintPage += new PrintPageEventHandler(printdocument_PrintPage);
            DialogResult resultat = printDialog.ShowDialog();
            if (resultat == DialogResult.OK)
            {
                printdocument.Print();
            }


        }
        void printdocument_PrintPage(Object Sender, PrintPageEventArgs e)
        {

            Graphics grafic = e.Graphics;
            Font font = new Font("Courrier New", 12);
            float fontHeight = font.GetHeight();
            Point p1 = new Point(150, 200);
            Point p2 = new Point(150, 230);
            String uneDate = System.DateTime.Now.ToShortDateString();
            Pen p = new Pen(Brushes.Black, 1.2f);
           // Image newImage = Image.FromFile("InsertImageLogo.jpg");
           // grafic.DrawImage(newImage, 60, 60);

            grafic.FillRectangle(Brushes.White, new Rectangle(40, 200, 250, 30));
            grafic.DrawRectangle(p, new Rectangle(40, 200, 260, 30));
            grafic.DrawString("Document", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(60, 204, 250, 30));
            grafic.DrawString("BON DE LIVRAISON", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(160, 204, 250, 30));
            grafic.DrawString("Numéro:", new Font("Courrier New", 10), new SolidBrush(Color.Black), 50, 250);
            grafic.DrawString("Date:", new Font("Courrier New", 10), new SolidBrush(Color.Black), 50, 280);
            grafic.DrawLine(p, p1, p2);
            grafic.FillRectangle(Brushes.White, new Rectangle(400, 200, 400, 150));
            grafic.DrawRectangle(p, new Rectangle(400, 200, 400, 150));
            grafic.DrawString("Client :", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(402, 202, 400, 150));
            grafic.DrawString("Adresse :", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(402, 227, 400, 150));
            grafic.DrawString("Code postal :", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(402, 252, 400, 150));
            grafic.DrawString("Ville :", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(552, 252, 400, 150));
            grafic.DrawString("tél :", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(402, 277, 400, 150));
            grafic.DrawString("FAX :", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(552, 277, 400, 150));
            grafic.DrawString("MF :", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(402, 302, 400, 150));
            grafic.DrawString("Code Client :", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(402, 327, 400, 150));

            grafic.FillRectangle(Brushes.White, new Rectangle(40, 400, 150, 40));
            grafic.DrawRectangle(p, new Rectangle(40, 400, 150, 40));
            grafic.DrawString("Prix en TND", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(70, 408, 250, 30));

            grafic.FillRectangle(Brushes.White, new Rectangle(190, 400, 200, 40));
            grafic.DrawRectangle(p, new Rectangle(190, 400, 200, 40));
            grafic.DrawString("REf.Commande", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(200, 408, 250, 30));
            grafic.DrawString("Dépot", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(330, 408, 250, 30));
            Point p3 = new Point(310,400);
            Point p4 = new Point(310, 440);
            grafic.DrawLine(p, p3, p4);

            grafic.FillRectangle(Brushes.White, new Rectangle(390, 400, 200, 40));
            grafic.DrawRectangle(p, new Rectangle(390, 400, 200, 40));
            grafic.DrawString("Mode de livraison", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(430, 408, 200, 40));

            grafic.FillRectangle(Brushes.White, new Rectangle(590, 400, 210, 40));
            grafic.DrawRectangle(p, new Rectangle(590, 400, 210, 40));
            grafic.DrawString("Moyen de livraison", new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(592, 408, 250, 40));
            grafic.DrawString("Lieu de livraison", new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(702, 408, 250, 40));

            grafic.FillRectangle(Brushes.White, new Rectangle(40, 440, 150, 20));
            grafic.DrawRectangle(p, new Rectangle(40, 440, 150, 20));
            grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20));

            grafic.FillRectangle(Brushes.White, new Rectangle(190, 440, 200, 20));
            grafic.DrawRectangle(p, new Rectangle(190, 440, 200, 20));
            grafic.DrawString("Déscription", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(240, 442, 200, 20));

            grafic.FillRectangle(Brushes.White, new Rectangle(390, 440, 60, 20));
            grafic.DrawRectangle(p, new Rectangle(390, 440, 60, 20));
            grafic.DrawString("UN", new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(400, 442, 60, 20));

            grafic.FillRectangle(Brushes.White, new Rectangle(450, 440, 70, 20));
            grafic.DrawRectangle(p, new Rectangle(450, 440, 70, 20));
            grafic.DrawString("Qté", new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(460, 442, 70, 20));

            grafic.FillRectangle(Brushes.White, new Rectangle(520, 440, 70, 20));
            grafic.DrawRectangle(p, new Rectangle(520, 440, 70, 20));
            grafic.DrawString("PUHT", new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(530, 442, 70, 20));

            grafic.FillRectangle(Brushes.White, new Rectangle(590, 440, 50, 20));
            grafic.DrawRectangle(p, new Rectangle(590, 440, 50, 20));
            grafic.DrawString("%REM", new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(695, 442, 50, 20));

            grafic.FillRectangle(Brushes.White, new Rectangle(640, 440, 60, 20));
            grafic.DrawRectangle(p, new Rectangle(640, 440, 60, 20));
            grafic.DrawString("PU net HT", new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(650, 442, 60, 20));
            Point p5 = new Point(700,400);
            Point p6 = new Point(700, 440);
            grafic.DrawLine(p, p5, p6);

            grafic.FillRectangle(Brushes.White, new Rectangle(700, 440, 60, 20));
            grafic.DrawRectangle(p, new Rectangle(700, 440, 60, 20));
            grafic.DrawString("PTHT", new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(710, 442, 60, 20));

            grafic.FillRectangle(Brushes.White, new Rectangle(760, 440, 40, 20));
            grafic.DrawRectangle(p, new Rectangle(760, 440, 40, 20));
            grafic.DrawString("TVA", new Font("Courrier New", 9), new SolidBrush(Color.Black), new Rectangle(770, 442, 40, 20));

           /* int r = 0;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                DataRow row = gridView1.GetDataRow(i);
                // grafic.DrawRectangle(p, new Rectangle(10, 300+r, 80, 30));

                grafic.DrawString(row[7].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(50, 490 + r, 150, 30));
                grafic.DrawString(row[8].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(200, 490 + r, 200, 30));
                grafic.DrawString(row[2].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(400, 490 + r, 60, 30));
                grafic.DrawString(row[4].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(460, 490 + r, 70, 30));
                grafic.DrawString(row[3].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(530, 490 + r, 70, 30));
                grafic.DrawString(row[9].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(600, 490 + r, 50, 30));
                grafic.DrawString(row[10].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(650, 490 + r, 60, 30));
                grafic.DrawString(row[5].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(710, 490 + r, 60, 30));
                grafic.DrawString(row[6].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(770, 490 + r, 40, 30));
                r = r + 20;
            }*/

            grafic.FillRectangle(Brushes.White, new Rectangle(40, 460, 150, 380));
            grafic.DrawRectangle(p, new Rectangle(40, 460, 150, 380));
            // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

            grafic.FillRectangle(Brushes.White, new Rectangle(190, 460, 200, 380));
            grafic.DrawRectangle(p, new Rectangle(190, 460, 200, 380));
            // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20));

            grafic.FillRectangle(Brushes.White, new Rectangle(390, 460, 60, 380));
            grafic.DrawRectangle(p, new Rectangle(390, 460, 60, 380));
            // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

            grafic.FillRectangle(Brushes.White, new Rectangle(450, 460, 70, 380));
            grafic.DrawRectangle(p, new Rectangle(450, 460, 70, 380));
            // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

            grafic.FillRectangle(Brushes.White, new Rectangle(520, 460, 70, 380));
            grafic.DrawRectangle(p, new Rectangle(520, 460, 70, 380));
            // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

            grafic.FillRectangle(Brushes.White, new Rectangle(590, 460, 50, 380));
            grafic.DrawRectangle(p, new Rectangle(590, 460, 50, 380));
            // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

            grafic.FillRectangle(Brushes.White, new Rectangle(640, 460, 60, 380));
            grafic.DrawRectangle(p, new Rectangle(640, 460, 60, 380));
            // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

            grafic.FillRectangle(Brushes.White, new Rectangle(700, 460, 60, 380));
            grafic.DrawRectangle(p, new Rectangle(700, 460, 60, 380));
            // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

            grafic.FillRectangle(Brushes.White, new Rectangle(760, 460, 40, 380));
            grafic.DrawRectangle(p, new Rectangle(760, 460, 40, 380));
            // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

            grafic.FillRectangle(Brushes.White, new Rectangle(40, 860, 130, 40));
            grafic.DrawRectangle(p, new Rectangle(40, 860, 130, 40));
            grafic.DrawString("Le magasinier", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(50, 862, 130, 40));
            grafic.FillRectangle(Brushes.White, new Rectangle(170, 860, 130, 40));
            grafic.DrawRectangle(p, new Rectangle(170, 860, 130, 40));
            grafic.DrawString("Le controleur", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(180, 862, 130, 40));
            grafic.FillRectangle(Brushes.White, new Rectangle(300, 860, 130, 40));
            grafic.DrawRectangle(p, new Rectangle(300, 860, 130, 40));
            grafic.DrawString("Le transporteur", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(310, 862, 130, 40));



            grafic.FillRectangle(Brushes.White, new Rectangle(40, 900, 130, 60));
            grafic.DrawRectangle(p, new Rectangle(40, 900, 130, 60));
            //grafic.DrawString("TVA", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(750, 442, 60, 20));
            grafic.FillRectangle(Brushes.White, new Rectangle(170, 900, 130, 60));
            grafic.DrawRectangle(p, new Rectangle(170, 900, 130, 60));
            //grafic.DrawString("TVA", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(750, 442, 60, 20));
            grafic.FillRectangle(Brushes.White, new Rectangle(300, 900, 130, 60));
            grafic.DrawRectangle(p, new Rectangle(300, 900, 130, 60));
            //grafic.DrawString("TVA", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(750, 442, 60, 20));
           

            grafic.FillRectangle(Brushes.White, new Rectangle(550, 860, 250, 40));
            grafic.DrawRectangle(p, new Rectangle(550, 860, 250, 40));
            grafic.DrawString("Le client / le réceptionnaire", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(590, 862, 250, 40));

            grafic.FillRectangle(Brushes.White, new Rectangle(550, 900, 250, 60));
            grafic.DrawRectangle(p, new Rectangle(550, 900, 250, 60));
          //  grafic.DrawString("Total TTC", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(460, 942, 350, 20));

            
            grafic.DrawString("Le client avoir pris connaissance et accepté nos conditions générales de vente", new Font("Courrier New", 9), new SolidBrush(Color.Black), 40, 1000);
            grafic.DrawString("Z.L M'ghira 3-Rue Nabeul-2082-Fouchana.Tunisia/Email:info@aquatun.com/website: www.aquatun.com", new Font("Courrier New", 8), new SolidBrush(Color.Black), 40, 1050);
            grafic.DrawString("Tél : (+216)7A 409 215- (+216)71 409 237/ Fax: +216 71 409 223", new Font("Courrier New", 8), new SolidBrush(Color.Black), 40, 1070);
            grafic.DrawString("Sarl.cap.Soc 75 000.000 TND-TVA:1214920 C/A/MOOO-RCN°B24161292011-CD : 1214920C", new Font("Courrier New", 8), new SolidBrush(Color.Black), 40, 1090);
            grafic.DrawString("RIB:BIAT 08047020021000116515/BTK 200060600011211369270", new Font("Courrier New", 8), new SolidBrush(Color.Black), 40, 1110);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            printReicept();
        }
    }
}
