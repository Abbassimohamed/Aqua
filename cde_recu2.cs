using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Repository;
using System.Diagnostics;
using System.IO;
namespace RibbonSimplePad
{
    public partial class cde_recu2 : DevExpress.XtraEditors.XtraForm
    {
        public cde_recu2()
        {
            InitializeComponent();
        }
        sql_gmao fun = new sql_gmao();
        public static string key, designation_is, contenue_is, etat_form = "", ActeurInterne, ActeurExterne, famille_eq, codeintervention;
        public static int id_is, idequ, idCompteur, idMesureInitiale;
        public static int? SfamillInitiale, famillInitiale;
        public static string oper, des, id_contrat_g, desc, cd_cont, GlobalCodeEq, look3, occupedMesure, CodeinterSurMesureInitiale, NominterSurMesureInitiale, NewMesureInter;

        public static int id_fich;
        public static string descri, clt, date, commentaire;
        public static byte[] imm;
     

        private void simpleButton26_Click(object sender, EventArgs e)
        {
            oper = "ajouter";
            retour_jointure3 co = new retour_jointure3();
            co.ShowDialog();
        }

        private void simpleButton25_Click(object sender, EventArgs e)
        {
            oper = "modifier";
            System.Data.DataRow row = gridView5.GetDataRow(gridView5.FocusedRowHandle);
            id_fich = Convert.ToInt32(row[0]);
            descri = row[1].ToString();
            clt = row[2].ToString();
            date = row[3].ToString();
            commentaire = row[4].ToString();

            cd_cont = row[7].ToString();
            retour_jointure3 co = new retour_jointure3();
            co.ShowDialog();
        }

        private void simpleButton24_Click(object sender, EventArgs e)
        {
            System.Data.DataRow row = gridView5.GetDataRow(gridView5.FocusedRowHandle);
            id_fich = Convert.ToInt32(row[0]);
            fun.delete__pict3(id_fich);
            getAllFichierContrat();
        }
        private void getAllFichierContrat()
        {
            gridControl5.DataSource = null;
            gridView5.Columns.Clear();
            gridControl5.DataSource = fun.get_retour3();
            RepositoryItemPictureEdit pictureEdit = gridControl5.RepositoryItems.Add("PictureEdit") as RepositoryItemPictureEdit;
            pictureEdit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;

            gridView5.Columns["extension"].ColumnEdit = pictureEdit;
            this.gridView5.Columns[0].Visible = false;
            this.gridView5.Columns[1].Caption = "Description";
            this.gridView5.Columns[2].Caption = "Fournisseur";
            this.gridView5.Columns[3].Caption = "Date de retour";
            this.gridView5.Columns[4].Caption = "Commentaire";
            this.gridView5.Columns[6].Caption = "Type de fichier";
            this.gridView5.Columns[7].Visible = false;

            gridView5.Columns[4].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView5.Columns[6].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

        }

        private void simpleButton23_Click(object sender, EventArgs e)
        {
            getAllFichierContrat();
        }

        private void simpleButton22_Click(object sender, EventArgs e)
        {
            gridControl5.ShowRibbonPrintPreview();
        }

        private void cde_recu_Activated(object sender, EventArgs e)
        {
           
        }

        private void gridView5_DoubleClick(object sender, EventArgs e)
        {
            GridHitInfo celclick = gridView5.CalcHitInfo(gridControl5.PointToClient(Control.MousePosition));
            if (celclick.InRow)
            {
                int count = gridView5.DataRowCount;
                if (count != 0 && gridView5.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    System.Data.DataRow row = gridView5.GetDataRow(gridView5.FocusedRowHandle);
                    imm = DevExpress.XtraEditors.Controls.ByteImageConverter.ToByteArray(row[7]);
                    //des = row[4].ToString();
                    //id_fich = Convert.ToInt32(row[3]);
                    //System.Diagnostics.Process.Start(row[6].ToString());
                    //zz.ShowDialog();
                    byte[] bytes = imm;
                    string nom = row[1].ToString();
                    string extention = row[6].ToString();
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

        private void cde_recu_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void cde_recu2_Load(object sender, EventArgs e)
        {

        }

        private void cde_recu2_Activated(object sender, EventArgs e)
        {
            getAllFichierContrat();
            gridView5.OptionsView.ShowAutoFilterRow = true;
            gridView5.BestFitColumns();
            gridView5.OptionsBehavior.Editable = false;
            gridView5.OptionsView.EnableAppearanceEvenRow = true;
            Form1.load = 1;

            Form1.wait = 1;
        }

        private void cde_recu2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }
    }
}