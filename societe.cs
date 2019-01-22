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
using System.Text.RegularExpressions;

namespace RibbonSimplePad
{
    public partial class societe : DevExpress.XtraEditors.XtraForm
    {
        public societe()
        {
            InitializeComponent();
        }
        sql_gmao fun = new sql_gmao();
        public static int id_soc;
        OpenFileDialog ofd = new OpenFileDialog();
        public static byte[] imgdata, ima2;
        public static byte[] Imm = null;
        public static string vue;
        byte[] ReadFile(string sPath)  

        {
            byte[] data = null;
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fStream);
            data = br.ReadBytes((int)numBytes);
            return data;
        }

        byte[] ReadFile2(string sPath)
        {
            byte[] data2 = null;
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fStream);
            data2 = br.ReadBytes((int)numBytes);
            return data2;
        }

        byte[] ReadFile3(string sPath)
        {
            byte[] data2 = null;
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fStream);
            data2 = br.ReadBytes((int)numBytes);
            return data2;
        }
        private void societe_Load(object sender, EventArgs e)
        {
            getSociete();
        }
        private void getSociete()
        {
            DataTable soc = new DataTable();
            soc = fun.get_Societe();
            if (soc.Rows.Count != 0)
            {
                id_soc = Convert.ToInt32(soc.Rows[0]["id_societe"]);
                textEdit1.Text = soc.Rows[0]["nom_societe"].ToString();
                textEdit2.Text = soc.Rows[0]["responseble_societe"].ToString();
                textEdit3.Text = soc.Rows[0]["adresse_societe"].ToString();
                textEdit4.Text = soc.Rows[0]["tel_societe"].ToString();
                textEdit5.Text = soc.Rows[0]["gsm_societe"].ToString();
                textEdit6.Text = soc.Rows[0]["fax_societe"].ToString();
                textEdit7.Text = soc.Rows[0]["email_societe"].ToString();
                textEdit9.Text = soc.Rows[0]["site_societe"].ToString();
                textEdit10.Text = soc.Rows[0]["matricule_societe"].ToString();
                textEdit11.Text = soc.Rows[0]["banque"].ToString();
                textEdit12.Text = soc.Rows[0]["compte"].ToString();


                string test_image = soc.Rows[0]["pic_societe"].ToString();
                if (test_image != "")
                {
                    byte[] IMG = (Byte[])(soc.Rows[0]["logo_societe"]);
                    MemoryStream mem = new MemoryStream(IMG);
                  
                    textEdit8.Text = test_image;
                }

                else {  textEdit8.Text = ""; }


                string test_image2 = soc.Rows[0]["pic_pied"].ToString();
                if (test_image != "")
                {
                    
                    textEdit13.Text = test_image2;
                }

                else {  textEdit13.Text = ""; }
             


                simpleButton1.Visible = false;
                simpleButton2.Visible = true;

            }
            else
            {
                simpleButton1.Visible = true;
                simpleButton2.Visible = false;
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ajouterSociete();
            getSociete();
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {

        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text += "a";
            if (label1.Text.ToString() == "10aaa")
            {
                labelControl10.Visible = false;
                labelControl11.Visible = false;
                label1.Text = "10";
                timer1.Stop();
            }
        }
        private void ajouterSociete()
        {
            if (textEdit1.Text == "")
            {
                dxErrorProvider1.Dispose();
                dxErrorProvider1.SetError(textEdit1, "Champ Obligatoire");
            }
            else if (textEdit2.Text == "")
            {
                dxErrorProvider1.Dispose();
                dxErrorProvider1.SetError(textEdit2, "Champ Obligatoire");
            }
            else if (textEdit3.Text == "")
            {
                dxErrorProvider1.Dispose();
                dxErrorProvider1.SetError(textEdit3, "Champ Obligatoire");
            }
            else if (textEdit4.Text == "")
            {
                dxErrorProvider1.Dispose();
                dxErrorProvider1.SetError(textEdit4, "Champ Obligatoire");
            }
            else if (textEdit7.Text == "")
            {
                dxErrorProvider1.Dispose();
                dxErrorProvider1.SetError(textEdit7, "Champ Obligatoire");
            }
            else if (textEdit10.Text == "")
            {
                dxErrorProvider1.Dispose();
                dxErrorProvider1.SetError(textEdit10, "Champ Obligatoire");
            }
            else if (!Regex.IsMatch(textEdit7.Text, @"^[a-z,A-Z]{1,10}((-|.)\w+)*@\w+.\w{2,3}$"))
            {
                dxErrorProvider1.Dispose();
                dxErrorProvider1.SetError(textEdit7, "Email invalide!!!");
            }

            else if (textEdit11.Text == "")
            {
                dxErrorProvider1.Dispose();
                dxErrorProvider1.SetError(textEdit11, "Champ Obligatoire");
            }

            else if (textEdit12.Text == "")
            {
                dxErrorProvider1.Dispose();
                dxErrorProvider1.SetError(textEdit12, "Champ Obligatoire");
            }

            else
            {
                if (textEdit8.Text != "")
                {
                    dxErrorProvider1.Dispose();
                    imgdata = ReadFile(textEdit8.Text);
                    //immmm = ReadFile(textEdit13.Text);
                    string a = "";
                    fun.set_SocieteWithPicture(imgdata, textEdit1.Text, textEdit2.Text, textEdit3.Text, textEdit4.Text, textEdit6.Text, textEdit5.Text, textEdit7.Text, textEdit9.Text, textEdit10.Text, textEdit8.Text, textEdit11.Text, textEdit12.Text, a);
                }
                else
                {
                    string b = "";
                    dxErrorProvider1.Dispose();
                    fun.set_SocieteWithoutPicture(textEdit1.Text, textEdit2.Text, textEdit3.Text, textEdit4.Text, textEdit6.Text, textEdit5.Text, textEdit7.Text, textEdit9.Text, textEdit10.Text, textEdit11.Text, textEdit12.Text, b);
                }


                if (textEdit13.Text != "")
                {
                    dxErrorProvider1.Dispose();
                    ima2 = null;
                    ima2 = ReadFile2(textEdit13.Text);
                    //immmm = ReadFile(textEdit13.Text);
                    fun.update_pied(textEdit13.Text, ima2);

                

                }

                labelControl11.Visible = true;
                timer1.Start();
            }

        }
        private void ModifierSociete()
        {
            if (textEdit1.Text == "")
            {
                dxErrorProvider1.Dispose();
                dxErrorProvider1.SetError(textEdit1, "Champ Obligatoire");
            }
            else if (textEdit2.Text == "")
            {
                dxErrorProvider1.Dispose();
                dxErrorProvider1.SetError(textEdit2, "Champ Obligatoire");
            }
            else if (textEdit3.Text == "")
            {
                dxErrorProvider1.Dispose();
                dxErrorProvider1.SetError(textEdit3, "Champ Obligatoire");
            }
            else if (textEdit4.Text == "")
            {
                dxErrorProvider1.Dispose();
                dxErrorProvider1.SetError(textEdit4, "Champ Obligatoire");
            }
            else if (textEdit7.Text == "")
            {
                dxErrorProvider1.Dispose();
                dxErrorProvider1.SetError(textEdit7, "Champ Obligatoire");
            }
            else if (textEdit10.Text == "")
            {
                dxErrorProvider1.Dispose();
                dxErrorProvider1.SetError(textEdit10, "Champ Obligatoire");
            }
            else if (!Regex.IsMatch(textEdit7.Text, @"^[a-z,A-Z]{1,10}((-|.)\w+)*@\w+.\w{2,3}$"))
            {
                dxErrorProvider1.Dispose();
                dxErrorProvider1.SetError(textEdit7, "Email invalide!!!");
            }
            else
            {
                if (textEdit8.Text != "")
                {
                    dxErrorProvider1.Dispose();
                    imgdata = ReadFile(textEdit8.Text);
                    fun.update_SocieteWithPicture(textEdit1.Text, textEdit2.Text, textEdit3.Text, textEdit4.Text, textEdit6.Text, textEdit5.Text, textEdit7.Text, textEdit9.Text, textEdit10.Text, imgdata, textEdit8.Text, id_soc, textEdit11.Text, textEdit12.Text);
                }
                else
                {
                    dxErrorProvider1.Dispose();
                    fun.update_SocieteWithoutPic(Imm, textEdit1.Text, textEdit2.Text, textEdit3.Text, textEdit4.Text, textEdit6.Text, textEdit5.Text, textEdit7.Text, textEdit9.Text, textEdit10.Text, textEdit8.Text, id_soc, textEdit11.Text, textEdit12.Text);
                }


                if (textEdit13.Text != "")
                {
                    dxErrorProvider1.Dispose();
                    ima2 = ReadFile2(textEdit13.Text);
                    //immmm = ReadFile(textEdit13.Text);
                    fun.update_pied(textEdit13.Text, ima2);

                }
                else 
                {
                    string acc="";
                    ima2=null;
                    fun.update_piedbbb(acc,ima2);
                }


                labelControl10.Visible = true;
                timer1.Start();



            }

        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            ModifierSociete();
            getSociete();
        }
        private void societe_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }

        private void societe_Activated(object sender, EventArgs e)
        {
            Form1.wait = 1;
            Form1.load = 1;
        }

        private void checkEdit1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkEdit1.Checked == true)
            {
                simpleButton6.Visible = true;
                simpleButton7.Visible = true;
                textEdit8.Visible = true;
            }
            if (checkEdit1.Checked == false)
            {
                simpleButton6.Visible = false;
                simpleButton7.Visible = false;
                textEdit8.Visible = false;
            }
        }

        private void simpleButton6_Click_1(object sender, EventArgs e)
        {
            textEdit8.Text = "";
           
        }

        private void simpleButton7_Click_1(object sender, EventArgs e)
        {
            ofd.Title = "joindre des fichiers";
            if (ofd.CheckFileExists == true)
            {
                ofd.Filter = "";
                ofd.ShowDialog();
                if (ofd.FileName != "")
                {
                    FileInfo fi = new FileInfo(ofd.FileName);
                    textEdit8.Text = ofd.FileName;
                    
                }
            }
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit2.Checked == true)
            {
                simpleButton3.Visible = true;
                simpleButton4.Visible = true;
                textEdit13.Visible = true;
            }
            if (checkEdit2.Checked == false)
            {
                simpleButton3.Visible = false;
                simpleButton4.Visible = false;
                textEdit13.Visible = false;
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            textEdit13.Text = "";
           
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            ofd.Title = "joindre des fichiers";
            if (ofd.CheckFileExists == true)
            {
                ofd.Filter = "";
                ofd.ShowDialog();
                if (ofd.FileName != "")
                {
                    FileInfo fi = new FileInfo(ofd.FileName);
                    textEdit13.Text = ofd.FileName;
                   
                }
            }
        }
    }
}