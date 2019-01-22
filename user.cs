using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Text.RegularExpressions;

namespace RibbonSimplePad
{
    public partial class user : DevExpress.XtraEditors.XtraForm
    {
        public user()
        {
            InitializeComponent();
        }
        sql_gmao fun = new sql_gmao();
        private void user_Load(object sender, EventArgs e)
        {
            this.Opacity = 0;
            this.TransparencyKey = BackColor;
            pictureEdit1.Enabled = false;
            textEdit1.Text = login1.gsm;
            textEdit2.Text = login1.email;
            fadein.Start();
        }
        private void pictureEdit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textEdit2.Text != "" && (!Regex.IsMatch(textEdit2.Text, @"^[a-z,A-Z]{1,10}((-|.)\w+)*@\w+.\w{2,3}$")))
            {

                dxErrorProvider1.Dispose();
                dxErrorProvider1.SetError(textEdit2, "Email invalide!!!");

            }
                
            else if (textEdit3.Text != login1.passwd)
            {
                dxErrorProvider1.Dispose();
                dxErrorProvider1.SetError(textEdit3, "Mot de passe actuel invalide!");
            }
            else if (textEdit4.Text != textEdit5.Text)
            {
                dxErrorProvider1.Dispose();
                dxErrorProvider1.SetError(textEdit5, "Les deux mots de passe sont différents!");
            }
            else
            {
                dxErrorProvider1.Dispose();
                fun.update_user(login1.id_user, textEdit1.Text, textEdit2.Text, textEdit5.Text);
                labelControl6.Visible = true;
                labelControl7.Visible = true;
                timer1.Start();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text += "a";
            if (label1.Text.ToString() == "10aaa")
            {
                labelControl6.Visible = false;
                labelControl7.Visible = false;
                label1.Text = "10";
                this.Close();
            }
        }
        private void fadetin_Tick(object sender, EventArgs e)
        {
            this.Opacity += 0.01;
            if (this.Opacity == 1)
            {
                pictureEdit1.Enabled = true;
                fadein.Stop();
            }
        }
    }
}