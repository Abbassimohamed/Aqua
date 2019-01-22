using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace RibbonSimplePad
{
    public partial class about : DevExpress.XtraEditors.XtraForm
    {
        public about()
        {
            InitializeComponent();
        }

        private void pictureEdit2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void about_Load(object sender, EventArgs e)
        {
            this.TransparencyKey = BackColor;
            this.Opacity = 0;
            pictureEdit2.Enabled = false;
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Opacity += 0.01;
            if (this.Opacity == 1)
            {
                pictureEdit2.Enabled = true; timer1.Stop();
            }
        }
    }
}