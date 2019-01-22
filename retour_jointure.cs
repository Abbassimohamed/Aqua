﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.IO;
using System.Timers;

namespace RibbonSimplePad
{
    public partial class retour_jointure : DevExpress.XtraEditors.XtraForm
    {
        public retour_jointure()
        {
            InitializeComponent();
        }

        sql_gmao fun = new sql_gmao();
        public static string filename, ext;
        public static long numBytes;
        byte[] imgdata;
        byte[] ReadFile(string sPath)
        {
            //Initialize byte array with a null value initially.
            byte[] data = null;
            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(sPath);
            numBytes = fInfo.Length;
            filename = Path.GetFileName(sPath);
            ext = Path.GetExtension(filename);
            //Open FileStream to read file
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);
            //Use BinaryReader to read file stream into byte array.
            BinaryReader br = new BinaryReader(fStream);
            data = br.ReadBytes((int)numBytes);
            return data;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Joindre un fichier";
            if (ofd.CheckFileExists == true)
            {
                ofd.Filter = "";
                ofd.ShowDialog();
                if (ofd.FileName != "")
                {
                    // 5MO max pour un fichier
                    FileInfo fi = new FileInfo(ofd.FileName);
                    if (fi.Length < 5000000)
                    {
                        textEdit2.Text = ofd.FileName;
                    }
                    else
                    {
                        XtraMessageBox.Show("Le fichier selectionné dépasse la taille autorisée(5 MO)", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            // les icones de fichiers
            if (textEdit1.Text != "")
            {
                labelControl4.Text = "";
                byte[] extention;
                Image word, excel, powerpoint, access, pdf, txt, pic, other;
                word = (System.Drawing.Image)Properties.Resources.word;
                excel = (System.Drawing.Image)Properties.Resources.excel;
                powerpoint = (System.Drawing.Image)Properties.Resources.powerpoint;
                access = (System.Drawing.Image)Properties.Resources.access;
                pdf = (System.Drawing.Image)Properties.Resources.pdf;
                txt = (System.Drawing.Image)Properties.Resources.txt;
                pic = (System.Drawing.Image)Properties.Resources.pic;
                other = (System.Drawing.Image)Properties.Resources.format_inconnu;
                if (retour_client.oper == "modifier")
                    
                {
                  
                    if (textEdit2.Text == "")
                    {

                        fun.update_contrat_ficheeeee(textEdit1.Text, lookUpEdit1.Text, dateEdit1.Text, memoEdit1.Text, retour_client.id_fich);
                        this.Close();
                    }
                    else
                    {
                        imgdata = ReadFile(textEdit2.Text);
                        ImageConverter converter = new ImageConverter();
                        if (ext == ".pdf")
                        {
                            extention = (byte[])converter.ConvertTo(pdf, typeof(byte[]));
                        }
                        else if (ext == ".doc" || ext == ".docx")
                        {
                            extention = (byte[])converter.ConvertTo(word, typeof(byte[]));
                        }
                        else if (ext == ".xls" || ext == ".xlsx")
                        {
                            extention = (byte[])converter.ConvertTo(excel, typeof(byte[]));
                        }
                        else if (ext == ".ppt" || ext == ".pptx")
                        {
                            extention = (byte[])converter.ConvertTo(powerpoint, typeof(byte[]));
                        }
                        else if (ext == ".mdb" || ext == ".accdb")
                        {
                            extention = (byte[])converter.ConvertTo(access, typeof(byte[]));
                        }
                        else if (ext == ".txt")
                        {
                            extention = (byte[])converter.ConvertTo(txt, typeof(byte[]));
                        }
                        else if (ext == ".gif" || ext == ".jpg" || ext == ".JPEG" || ext == ".png" || ext == ".bmp")
                        {
                            extention = (byte[])converter.ConvertTo(pic, typeof(byte[]));
                        }
                        else
                        {
                            extention = (byte[])converter.ConvertTo(other, typeof(byte[]));
                        }
                        fun.update_contrat_fich3(textEdit1.Text, lookUpEdit1.Text, dateEdit1.Text, memoEdit1.Text, extention, ext, imgdata, retour_client.id_fich);
                       
                        
                        //fun.update_contrat_fich3(retour_client.id_fich, retour_client.cd_cont, filename, imgdata, textEdit1.Text, ext, numBytes.ToString(), extention);
                        this.Close();
                    }
                }
                if (retour_client.oper == "ajouter")
                {
                  
                    if (textEdit2.Text == "")
                    {
                        labelControl4.Text = "Vous n'avez pas encore ajouté un fichier!";
                    }
                    else
                    {
                       
                        imgdata = ReadFile(textEdit2.Text);
                        ImageConverter converter = new ImageConverter();

                        if (ext == ".pdf")
                        {

                            extention = (byte[])converter.ConvertTo(pdf, typeof(byte[]));
                        }
                        else if (ext == ".doc" || ext == ".docx")
                        {
                            extention = (byte[])converter.ConvertTo(word, typeof(byte[]));
                        }
                        else if (ext == ".xls" || ext == ".xlsx")
                        {
                            extention = (byte[])converter.ConvertTo(excel, typeof(byte[]));
                        }
                        else if (ext == ".ppt" || ext == ".pptx")
                        {
                            extention = (byte[])converter.ConvertTo(powerpoint, typeof(byte[]));
                        }
                        else if (ext == ".mdb" || ext == ".accdb")
                        {
                            extention = (byte[])converter.ConvertTo(access, typeof(byte[]));
                        }
                        else if (ext == ".txt")
                        {
                            extention = (byte[])converter.ConvertTo(txt, typeof(byte[]));
                        }
                        else if (ext == ".gif" || ext == ".jpg" || ext == ".JPEG" || ext == ".png" || ext == ".bmp")
                        {
                            extention = (byte[])converter.ConvertTo(pic, typeof(byte[]));
                        }

                        else
                        {
                            extention = (byte[])converter.ConvertTo(other, typeof(byte[]));
                        }
                       
                        fun.insert_retour(textEdit1.Text, lookUpEdit1.Text, dateEdit1.Text, memoEdit1.Text, extention, ext, imgdata);
                       
                            this.Close();
                    }
                }
            }
            else
            {
                labelControl4.Text = "La description ne peut être vide";
            }
        }
       

        private void retour_jointure_Activated(object sender, EventArgs e)
        {
            clients();
        }
        private void clients()
        {
            //get All stock
            lookUpEdit1.Properties.DataSource = null;
            DataTable Allclients = new DataTable();
            Allclients = fun.get_Allclt();
            
                lookUpEdit1.Properties.ValueMember = "code";
                lookUpEdit1.Properties.DisplayMember = "raison_soc";
                lookUpEdit1.Properties.DataSource = Allclients;
                lookUpEdit1.Properties.PopulateColumns();
                lookUpEdit1.Properties.Columns["code_clt"].Caption = "Code client";
                lookUpEdit1.Properties.Columns["raison_soc"].Caption = "Raison sociale";
                lookUpEdit1.Properties.Columns["responsbale"].Caption = "Responsable";
                lookUpEdit1.Properties.Columns["gsm_clt"].Visible = false;
                lookUpEdit1.Properties.Columns["gsm_clt"].Visible = false;
                lookUpEdit1.Properties.Columns["tel_clt"].Visible = false;
                lookUpEdit1.Properties.Columns["fax_clt"].Visible = false;
                lookUpEdit1.Properties.Columns["adresse_clt"].Visible = false;
                lookUpEdit1.Properties.Columns["cp_clt"].Visible = false;
                lookUpEdit1.Properties.Columns["ville_clt"].Visible = false;
                lookUpEdit1.Properties.Columns["email_clt"].Visible = false;
                lookUpEdit1.Properties.Columns["site_clt"].Visible = false;
                lookUpEdit1.Properties.Columns["tva_clt"].Visible = false;
                lookUpEdit1.Properties.Columns["forme_juriduque"].Visible = false;
                lookUpEdit1.Properties.Columns["mode_pay"].Visible = false;
            
        }

        private void retour_jointure_Load(object sender, EventArgs e)
        {
            if (retour_client.oper == "modifier")
            {
                DataTable dt = fun.get_cltByDesign(retour_client.clt);
                if (dt.Rows.Count > 0)
                {
                    lookUpEdit1.EditValue = dt.Rows[0]["code"].ToString();
                }
                textEdit1.Text = retour_client.descri;
               
                dateEdit1.Text = retour_client.date;
                memoEdit1.Text = retour_client.commentaire;

                
            }
        }
       

    }
}