using System;
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
    public partial class add_modif_conta : DevExpress.XtraEditors.XtraForm
    {
        public add_modif_conta()
        {
            InitializeComponent();
        }
        sql_gmao fun = new sql_gmao();
        public static string filename, ext, bb;
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
        private void add_modif_conta_Load(object sender, EventArgs e)
        {
            
           
            
            if (compta.etat == "ajouter")
            { labelControl1.Visible = false;
              textEdit1.Visible = false;
            }
            if (compta.etat == "modifier")
            {
                textEdit1.Text = compta.id.ToString();
                dateEdit1.DateTime = compta.datee;
                textEdit3.Text = compta.design;
                comboBoxEdit2.Text= compta.categ;
                textEdit4.Text = compta.montant;
                memoEdit1.Text = compta.comment;
                textEdit5.Text = compta.clt;


            }


            if (compta.etat1 == "ajouter")
            {
                labelControl1.Visible = false;
                textEdit1.Visible = false;
                labelControl6.Text = "Fournisseur";
            }
            if (compta.etat1 == "modifier")
            {
                textEdit1.Text = compta.id1.ToString();
                dateEdit1.DateTime = compta.datee1;
                textEdit3.Text = compta.design1;
                comboBoxEdit2.Text = compta.categ1;
                textEdit4.Text = compta.montant1;
                memoEdit1.Text = compta.comment1;
                textEdit5.Text = compta.feur;
                labelControl6.Text = "Fournisseur";

            }



           
                comboBoxEdit2.Properties.Items.Clear();
                DataTable dd = new DataTable();
            
                 if (compta.categggg == "encaissement")
                 {
                bb = "encaissement";
               
                 }

                 if (compta.categggg == "decaissement")
                 {
                     bb = "decaissement";

                 }
                dd = fun.addcateg(bb);
                foreach (DataRow row in dd.Rows)
                {
                    comboBoxEdit2.Properties.Items.Add(row["des"]);
                }
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
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
               
            
            //encaissement


                if (dateEdit1.Text == "")
                {
                    dxErrorProvider1.Dispose();
                    dxErrorProvider1.SetError(dateEdit1, "Champ obligatoire");
                }
                else if (textEdit3.Text == "")
                {
                    dxErrorProvider1.Dispose();
                    dxErrorProvider1.SetError(textEdit3, "Champ obligatoire");
                }
                else if (comboBoxEdit2.Text == "")
                {
                    dxErrorProvider1.Dispose();
                    dxErrorProvider1.SetError(comboBoxEdit2, "Champ obligatoire");
                }
                else if (textEdit4.Text == "")
                {
                    dxErrorProvider1.Dispose();
                    dxErrorProvider1.SetError(textEdit4, "Champ obligatoire");
                }

                else
                {
                    dxErrorProvider1.Dispose();
                    if (compta.etat == "modifier")
                    {

                        if (textEdit2.Text == "")
                        {

                            DateTime dd;
                            dd = Convert.ToDateTime(dateEdit1.Text);

                            fun.update_encaiss(dd, textEdit3.Text, comboBoxEdit2.Text, textEdit4.Text, memoEdit1.Text, textEdit5.Text, compta.id);

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

                            fun.update_encaiss2(Convert.ToDateTime(dateEdit1.Text), textEdit3.Text, comboBoxEdit2.Text, textEdit4.Text, memoEdit1.Text, textEdit5.Text, extention, ext, imgdata, compta.id);


                            //fun.update_contrat_fich3(retour_client.id_fich, retour_client.cd_cont, filename, imgdata, textEdit1.Text, ext, numBytes.ToString(), extention);
                            this.Close();
                        }
                    }
                    if (compta.etat == "ajouter")
                    {

                        if (textEdit2.Visible == false)
                        {
                            labelControl8.Text = "un justificatif est obligatoire";

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


                            string type = "encaissement";
                            fun.insert_encaiss(dateEdit1.DateTime, textEdit3.Text, comboBoxEdit2.Text, textEdit4.Text, memoEdit1.Text, textEdit5.Text, type, extention, ext, imgdata);

                            this.Close();
                        }
                    }








                    // decaissements



                    if (compta.etat1 == "modifier")
                    {

                        if (textEdit2.Text == "")
                        {

                            DateTime dd;
                            dd = Convert.ToDateTime(dateEdit1.Text);

                            
                            fun.update_decaiss(dd, textEdit3.Text, comboBoxEdit2.Text, textEdit4.Text, memoEdit1.Text, textEdit5.Text, compta.id1);
                           
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
                            
                            fun.update_decaiss2(Convert.ToDateTime(dateEdit1.Text), textEdit3.Text, comboBoxEdit2.Text, textEdit4.Text, memoEdit1.Text, textEdit5.Text, extention, ext, imgdata, compta.id1);


                            //fun.update_contrat_fich3(retour_client.id_fich, retour_client.cd_cont, filename, imgdata, textEdit1.Text, ext, numBytes.ToString(), extention);
                            this.Close();
                        }
                    }
                    if (compta.etat1 == "ajouter")
                    {

                        if (textEdit2.Visible == false)
                        {
                            labelControl8.Text = "un justificatif est obligatoire";

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


                            string type = "decaissement";
                            fun.insert_decaiss(dateEdit1.DateTime, textEdit3.Text, comboBoxEdit2.Text, textEdit4.Text, memoEdit1.Text, textEdit5.Text, type, extention, ext, imgdata);

                            this.Close();
                        }
                    }

                }
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
                        XtraMessageBox.Show("Le fichier sélectionné dépasse la taille autorisée(5 MO)", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}