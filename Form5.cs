using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace RibbonSimplePad
{
    public partial class Form5 : Form
    {
        public Form5()
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
            Point p1 = new Point(150, 200 );
            Point p2 = new Point(150, 230);
             String uneDate = System.DateTime.Now.ToShortDateString();
             Pen p = new Pen(Brushes.Black, 1.2f);

             Image newImage = Image.FromFile("InsertImageLogo.jpg");
             grafic.DrawImage(newImage, 60, 60);

             grafic.FillRectangle(Brushes.White, new Rectangle(40 , 200, 250, 30));
             grafic.DrawRectangle(p, new Rectangle(40, 200, 250, 30));
             grafic.DrawString("Document", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(60, 204, 250, 30));
             grafic.DrawString("FACTURE", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(170, 204, 250, 30));
             grafic.DrawString("Numéro:", new Font("Courrier New", 10), new SolidBrush(Color.Black), 50,250);
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
             grafic.DrawString("Bon de livraison N°", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(230, 408, 250, 30));


             grafic.FillRectangle(Brushes.White, new Rectangle(390, 400, 210, 40));
             grafic.DrawRectangle(p, new Rectangle(390, 400, 210, 40));
             grafic.DrawString("Ref.Commande", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(430, 408, 250, 30));

             grafic.FillRectangle(Brushes.White, new Rectangle(600, 400, 200, 40));
             grafic.DrawRectangle(p, new Rectangle(600, 400, 200, 40));
             grafic.DrawString("Mode de paiement", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(640, 408, 250, 30));

             grafic.FillRectangle(Brushes.White, new Rectangle(40, 440, 150, 20));
             grafic.DrawRectangle(p, new Rectangle(40, 440, 150, 20));
             grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20));

             grafic.FillRectangle(Brushes.White, new Rectangle(190, 440, 200, 20));
             grafic.DrawRectangle(p, new Rectangle(190, 440, 200, 20));
             grafic.DrawString("Déscription", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(240, 442, 200, 20));

             grafic.FillRectangle(Brushes.White, new Rectangle(390, 440, 50, 20));
             grafic.DrawRectangle(p, new Rectangle(390, 440, 50, 20));
             grafic.DrawString("UN", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(400, 442, 50, 20));

             grafic.FillRectangle(Brushes.White, new Rectangle(440, 440, 50, 20));
             grafic.DrawRectangle(p, new Rectangle(440, 440, 50, 20));
             grafic.DrawString("Qté", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(450, 442, 50, 20));

             grafic.FillRectangle(Brushes.White, new Rectangle(490, 440, 60, 20));
             grafic.DrawRectangle(p, new Rectangle(490, 440, 60, 20));
             grafic.DrawString("PUHT", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(500, 442, 60, 20));

             grafic.FillRectangle(Brushes.White, new Rectangle(550, 440, 50, 20));
             grafic.DrawRectangle(p, new Rectangle(550, 440, 50, 20));
             grafic.DrawString("%REM", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(555, 442, 50, 20));

             grafic.FillRectangle(Brushes.White, new Rectangle(600, 440, 70, 20));
             grafic.DrawRectangle(p, new Rectangle(600, 440, 70, 20));
             grafic.DrawString("PU net HT", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(610, 442, 70, 20));

             grafic.FillRectangle(Brushes.White, new Rectangle(670, 440, 70, 20));
             grafic.DrawRectangle(p, new Rectangle(670, 440, 70, 20));
             grafic.DrawString("PTHT", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(680, 442, 70, 20));

             grafic.FillRectangle(Brushes.White, new Rectangle(740, 440, 60, 20));
             grafic.DrawRectangle(p, new Rectangle(740, 440, 60, 20));
             grafic.DrawString("TVA", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(750, 442, 60, 20));



             grafic.FillRectangle(Brushes.White, new Rectangle(40, 460, 150, 380));
             grafic.DrawRectangle(p, new Rectangle(40, 460, 150, 380));
            // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

             grafic.FillRectangle(Brushes.White, new Rectangle(190, 460, 200, 380));
             grafic.DrawRectangle(p, new Rectangle(190, 460, 200, 380));
             // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20));

             grafic.FillRectangle(Brushes.White, new Rectangle(390, 460, 50, 380));
             grafic.DrawRectangle(p, new Rectangle(390, 460, 50, 380));
             // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

             grafic.FillRectangle(Brushes.White, new Rectangle(440, 460, 50, 380));
             grafic.DrawRectangle(p, new Rectangle(440, 460, 50, 380));
             // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

             grafic.FillRectangle(Brushes.White, new Rectangle(490, 460, 60, 380));
             grafic.DrawRectangle(p, new Rectangle(490, 460, 60, 380));
             // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

             grafic.FillRectangle(Brushes.White, new Rectangle(550, 460, 50, 380));
             grafic.DrawRectangle(p, new Rectangle(550, 460, 50, 380));
             // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

             grafic.FillRectangle(Brushes.White, new Rectangle(600, 460, 70, 380));
             grafic.DrawRectangle(p, new Rectangle(600, 460, 70, 380));
             // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

             grafic.FillRectangle(Brushes.White, new Rectangle(670, 460, 70, 380));
             grafic.DrawRectangle(p, new Rectangle(670, 460, 70, 380));
             // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))

             grafic.FillRectangle(Brushes.White, new Rectangle(740, 460, 60, 380));
             grafic.DrawRectangle(p, new Rectangle(740, 460, 60, 380));
             // grafic.DrawString("Code", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(80, 442, 150, 20))
           /*  int r=0;
             for (int i = 0; i < gridView1.RowCount; i++)
             {
                 DataRow row = gridView1.GetDataRow(i);
                 // grafic.DrawRectangle(p, new Rectangle(10, 300+r, 80, 30));
                
                 grafic.DrawString(row[7].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(50 , 490 +r, 150, 30));
                 grafic.DrawString(row[8].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(200, 490 + r, 200, 30));
                 grafic.DrawString(row[2].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(400, 490 + r, 50, 30));
                 grafic.DrawString(row[4].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(450, 490 + r, 50, 30));
                 grafic.DrawString(row[3].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(500, 490 + r, 60, 30));
                 grafic.DrawString(row[9].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(560, 490 + r, 50, 30));
                 grafic.DrawString(row[10].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(610, 490 + r, 70, 30));
                 grafic.DrawString(row[5].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(680, 490 + r, 70, 30));
                 grafic.DrawString(row[6].ToString(), new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(750, 490 + r, 60, 30));
                 r = r + 20;
             }
             */
             grafic.FillRectangle(Brushes.White, new Rectangle(40, 860, 70, 20));
             grafic.DrawRectangle(p, new Rectangle(40, 860, 70, 20));
             grafic.DrawString("Base HT", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(50, 862, 70, 20));
             grafic.FillRectangle(Brushes.White, new Rectangle(110, 860, 70, 20));
             grafic.DrawRectangle(p, new Rectangle(110, 860, 70, 20));
             grafic.DrawString("Code TVA", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(120, 862, 70, 20));
             grafic.FillRectangle(Brushes.White, new Rectangle(180, 860, 70, 20));
             grafic.DrawRectangle(p, new Rectangle(180, 860, 70, 20));
             grafic.DrawString("Taux TVA", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(185, 862, 70, 20));
             grafic.FillRectangle(Brushes.White, new Rectangle(250, 860, 70, 20));
             grafic.DrawRectangle(p, new Rectangle(250, 860, 70, 20));
             grafic.DrawString("Montant TVA", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(260, 862, 70, 20));


             grafic.FillRectangle(Brushes.White, new Rectangle(40, 880, 70, 20));
             grafic.DrawRectangle(p, new Rectangle(40, 880, 70, 20));
             //grafic.DrawString("TVA", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(750, 442, 60, 20));
             grafic.FillRectangle(Brushes.White, new Rectangle(110, 880, 70, 20));
             grafic.DrawRectangle(p, new Rectangle(110, 880, 70, 20));
             //grafic.DrawString("TVA", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(750, 442, 60, 20));
             grafic.FillRectangle(Brushes.White, new Rectangle(180, 880, 70, 20));
             grafic.DrawRectangle(p, new Rectangle(180, 880, 70, 20));
             //grafic.DrawString("TVA", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(750, 442, 60, 20));
             grafic.FillRectangle(Brushes.White, new Rectangle(250, 880, 70, 20));
             grafic.DrawRectangle(p, new Rectangle(250, 880, 70, 20));
             //grafic.DrawString("TVA", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(750, 442, 60, 20));

             grafic.FillRectangle(Brushes.White, new Rectangle(450, 860, 350, 20));
             grafic.DrawRectangle(p, new Rectangle(450, 860, 350, 20));
             grafic.DrawString("Total HT", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(460, 862, 350, 20));

             grafic.FillRectangle(Brushes.White, new Rectangle(450, 880, 350, 20));
             grafic.DrawRectangle(p, new Rectangle(450, 880, 350, 20));
             grafic.DrawString("Total net HT", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(460, 882, 350, 20));

             grafic.FillRectangle(Brushes.White, new Rectangle(450, 900, 350, 20));
             grafic.DrawRectangle(p, new Rectangle(450, 900, 350, 20));
             grafic.DrawString("Total TVA", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(460, 902, 350, 20));

             grafic.FillRectangle(Brushes.White, new Rectangle(450, 920, 350, 20));
             grafic.DrawRectangle(p, new Rectangle(450, 920, 350, 20));
             grafic.DrawString("Timbre fiscal", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(460, 922, 350, 20));

             grafic.FillRectangle(Brushes.White, new Rectangle(450, 940, 350, 20));
             grafic.DrawRectangle(p, new Rectangle(450, 940, 350, 20));
             grafic.DrawString("Total TTC", new Font("Courrier New", 10), new SolidBrush(Color.Black), new Rectangle(460, 942, 350, 20));

             grafic.DrawString("Arretée la présente facture en TTC à la somme de :", new Font("Courrier New", 10), new SolidBrush(Color.Black), 40, 950);
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
