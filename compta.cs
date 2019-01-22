using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.Repository;

namespace RibbonSimplePad
{
    public partial class compta : DevExpress.XtraEditors.XtraForm
    {
        public compta()
        {
            InitializeComponent();
        }
        sql_gmao fun = new sql_gmao();
        public static string etat, design, categ, montant, comment, clt, categggg;
        public static int id;
       public static DateTime datee;
        public static byte[] imm;


        public static string etat1, design1, categ1, montant1, comment1, feur;
        public static int id1;
        public static DateTime datee1;
        public static byte[] imm1;
        private void compta_Load(object sender, EventArgs e)
        {

        }

        private void getAllFichierContrat()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            string a = "encaissement";
            gridControl1.DataSource = fun.get_conta(a);
            RepositoryItemPictureEdit pictureEdit = gridControl1.RepositoryItems.Add("PictureEdit") as RepositoryItemPictureEdit;
            pictureEdit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;

            gridView1.Columns["extension"].ColumnEdit = pictureEdit;
            this.gridView1.Columns[0].Visible = false;
            this.gridView1.Columns[1].Caption = "Date";
            this.gridView1.Columns[2].Caption = "Designation";
            this.gridView1.Columns[3].Caption = "Catégorie";
            this.gridView1.Columns[4].Caption = "Montant HT";
            this.gridView1.Columns[5].Caption = "Client";
            this.gridView1.Columns[6].Visible = false;
            this.gridView1.Columns[7].Caption = "Commentaire";
            this.gridView1.Columns[8].Caption = "Justificatif";
            this.gridView1.Columns[9].Caption = "Type";
            this.gridView1.Columns[10].Visible = false;
            this.gridView1.Columns[11].Visible = false;


           

            //gridView1.Columns[4].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //gridView1.Columns[6].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

        }




        private void getAllFichierContrat1()
        {
            gridControl2.DataSource = null;
            gridView2.Columns.Clear();
            string a1 = "decaissement";
            gridControl2.DataSource = fun.get_conta(a1);
            RepositoryItemPictureEdit pictureEdit = gridControl2.RepositoryItems.Add("PictureEdit") as RepositoryItemPictureEdit;
            pictureEdit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;

            gridView2.Columns["extension"].ColumnEdit = pictureEdit;
            this.gridView2.Columns[0].Visible = false;
            this.gridView2.Columns[1].Caption = "Date";
            this.gridView2.Columns[2].Caption = "Designation";
            this.gridView2.Columns[3].Caption = "Catégorie";
            this.gridView2.Columns[4].Caption = "Montant HT";
            this.gridView2.Columns[6].Caption = "Fournisseur";
            this.gridView2.Columns[5].Visible = false;
            this.gridView2.Columns[7].Caption = "Commentaire";
            this.gridView2.Columns[8].Caption = "Justificatif";
            this.gridView2.Columns[9].Caption = "Type";
            this.gridView2.Columns[10].Visible = false;
            this.gridView2.Columns[11].Visible = false;




            //gridView1.Columns[4].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //gridView1.Columns[6].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            add_modif_conta add = new add_modif_conta();
            etat = "ajouter";
            categggg = "encaissement";
            add.ShowDialog();
            
            
        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            categggg = "encaissement";
            etat = "modifier";
            System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            id = Convert.ToInt32(row[0]);
            datee = Convert.ToDateTime(row[1]);
            design = row[2].ToString();
            categ = row[3].ToString();
            montant = row[4].ToString();
            clt = row[5].ToString();
            comment = row[7].ToString();

          
            add_modif_conta cf = new add_modif_conta();
            cf.ShowDialog();
        }

        private void compta_Activated(object sender, EventArgs e)
        {
            getAllFichierContrat();
            getAllFichierContrat1();
            Form1.load = 1;

            Form1.wait = 1;
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
          
        }

        private void gridView1_DoubleClick_1(object sender, EventArgs e)
        {
            GridHitInfo celclick = gridView1.CalcHitInfo(gridControl1.PointToClient(Control.MousePosition));
            if (celclick.InRow)
            {
                int count = gridView1.DataRowCount;
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                    imm = DevExpress.XtraEditors.Controls.ByteImageConverter.ToByteArray(row[10]);
                    //des = row[4].ToString();
                    //id_fich = Convert.ToInt32(row[3]);
                    //System.Diagnostics.Process.Start(row[6].ToString());
                    //zz.ShowDialog();
                    byte[] bytes = imm;
                    string nom = row[2].ToString();
                    string extention = row[9].ToString();
                    string path2 = @"c:\STOCK\DOCS\";
                    string path = @"c:\STOCK\DOCS\" + nom + extention;

                    if (Directory.Exists(path2))
                    {
                        try
                        {
                            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
                            {
                                writer.Write(bytes);
                            }

                            // open it with default application based in the
                            // file extension
                            Process p = System.Diagnostics.Process.Start(path);
                            //p.Wait();
                        }
                        finally
                        {
                            //clean the tmp file
                            //File.Delete(path);
                        }


                    }
                    else
                    {

                        System.IO.Directory.CreateDirectory(path2);
                        try
                        {
                            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
                            {
                                writer.Write(bytes);
                            }

                            // open it with default application based in the
                            // file extension
                            Process p = System.Diagnostics.Process.Start(path);
                            //p.Wait();
                        }
                        finally
                        {
                            //clean the tmp file
                            //File.Delete(path);
                        }
                    }

                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            getAllFichierContrat();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
           id = Convert.ToInt32(row[0]);
           fun.delete_encaiss(id);
            getAllFichierContrat();
        }

        private void simpleButton17_Click(object sender, EventArgs e)
        {
            add_modif_conta add = new add_modif_conta();
            etat1 = "ajouter";
            categggg = "decaissement";
            add.ShowDialog();
        }

        private void simpleButton16_Click(object sender, EventArgs e)
        {
            etat1 = "modifier";
            categggg = "decaissement";
            System.Data.DataRow row = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            id1 = Convert.ToInt32(row[0]);
            datee1 = Convert.ToDateTime(row[1]);
            design1 = row[2].ToString();
            categ1 = row[3].ToString();
            montant1 = row[4].ToString();
            feur = row[6].ToString();
            comment1 = row[7].ToString();


            add_modif_conta cf = new add_modif_conta();
            cf.ShowDialog();
        }

        private void simpleButton15_Click(object sender, EventArgs e)
        {
            System.Data.DataRow row = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            id1 = Convert.ToInt32(row[0]);
            fun.delete_encaiss(id1);
            getAllFichierContrat1();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            categggg = "encaissement";
            categ gg = new categ();
            gg.ShowDialog();
          
        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {
            categggg = "decaissement";
            categ gg = new categ();
            gg.ShowDialog();
        }

        private void simpleButton18_Click(object sender, EventArgs e)
        {
            getAllFichierContrat1();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            gridView1.ShowRibbonPrintPreview();
        }

        private void simpleButton14_Click(object sender, EventArgs e)
        {
            gridView2.ShowRibbonPrintPreview();
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
           
       double total_ht= 0;
            for (int i = 0; i < gridView2.DataRowCount; i++)
            {

                string aaa = gridView2.GetRowCellValue(i, "montant").ToString().Replace(".", ",");


                total_ht += Convert.ToDouble(aaa);
               
            }
            labelControl1.Text = total_ht.ToString() + " HT";
            total_ht = 0;
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
 
            double total_ht= 0;
            for (int i = 0; i < gridView1.DataRowCount; i++)
            {
                string aaa = gridView1.GetRowCellValue(i, "montant").ToString().Replace(".",",");
                
                
                total_ht += Convert.ToDouble(aaa);
               
            }
            labelControl2.Text = total_ht.ToString() + " HT";
            total_ht = 0;
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}