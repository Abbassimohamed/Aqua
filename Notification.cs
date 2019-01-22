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
    public partial class Notification : DevExpress.XtraEditors.XtraForm
    {
        public Notification()
        {
            InitializeComponent();
        }
        sql_gmao fun = new sql_gmao();
        public static string user;
        private void Notification_Load(object sender, EventArgs e)
        {
            get_AllNotification();
            gridView1.BestFitColumns();
        }
        private void get_AllNotification()
        {
            if (login1.depart == "Administrateur")
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
                gridControl1.DataSource = fun.get_AllAlertForAdmin();
                this.gridView1.Columns[0].Visible = false;
                this.gridView1.Columns[1].Caption = "Titre";
                this.gridView1.Columns[2].Caption = "Description";
                this.gridView1.Columns[3].Visible = false;
                this.gridView1.Columns[4].Visible = false;
                this.gridView1.Columns[5].Caption = "Date";
                this.gridView1.Columns[6].Visible = false;
            }
            else if (login1.depart == "Utilisateur")
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
                gridControl1.DataSource = fun.get_AllAlertForStock();
                this.gridView1.Columns[0].Visible = false;
                this.gridView1.Columns[1].Caption = "Titre";
                this.gridView1.Columns[2].Caption = "Description";
                this.gridView1.Columns[3].Visible = false;
                this.gridView1.Columns[4].Visible = false;
                this.gridView1.Columns[5].Caption = "Date";
                this.gridView1.Columns[6].Visible = false;
            }
            else if (login1.depart == "Gestion de Projet")
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
                gridControl1.DataSource = fun.get_AllAlertForProjet();
                this.gridView1.Columns[0].Visible = false;
                this.gridView1.Columns[1].Caption = "Titre";
                this.gridView1.Columns[2].Caption = "Description";
                this.gridView1.Columns[3].Visible = false;
                this.gridView1.Columns[4].Visible = false;
                this.gridView1.Columns[5].Caption = "Date";
                this.gridView1.Columns[6].Visible = false;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                DataTable notifExist = new DataTable();
                notifExist = fun.get_NotifById(Convert.ToInt32(row[0]));
                if (notifExist.Rows.Count != 0)
                {
                    if (notifExist.Rows[0]["vu_notif"] is DBNull)
                    {
                        user = login1.pseudo;
                    }
                    else
                    {
                        user = notifExist.Rows[0]["vu_notif"] + "," + login1.pseudo;
                    }
                    fun.update_notification(Convert.ToInt32(row[0]), user);
                }
            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataTable notifExist = new DataTable();
            notifExist = fun.get_AllAlert();
            if (notifExist.Rows.Count != 0)
            {
                foreach (DataRow rowNot in notifExist.Rows)
                {
                    if (rowNot["vu_notif"] is DBNull)
                    {
                        user = login1.pseudo;
                    }
                    else
                    {
                        user = rowNot["vu_notif"] + "," + login1.pseudo;
                    }
                    fun.update_notification(Convert.ToInt32(rowNot[0]), user);
                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                fun.delete_Notification(Convert.ToInt32(row[0]));
                fun.suivi_actions("Supprimer la notification : " + Convert.ToString(row[2]));
                get_AllNotification();
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            fun.vider_Notification();
            get_AllNotification();
        }

        private void Notification_Activated(object sender, EventArgs e)
        {
            get_AllNotification();
            Form1.wait = 1;
            Form1.load = 1;
            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridView1.BestFitColumns();
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.EnableAppearanceEvenRow = true;
        }

        private void Notification_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }
    }
}