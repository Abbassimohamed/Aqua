using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Drawing.Drawing2D;
using System.IO;
namespace RibbonSimplePad
{
    public partial class bon_achat : DevExpress.XtraEditors.XtraForm
    {
        public bon_achat()
        {
            InitializeComponent();
        }
        public static int req_id, iddd, etat = 0, use_grid;
        sql_gmao fun = new sql_gmao();
        private void bon_achat_Load(object sender, EventArgs e)
        {
            layoutControl1.Location = new Point(
    this.ClientSize.Width / 2 - layoutControl1.Size.Width / 2,
    6);
            Double total_ht = 0;
            labelControl15.Text = login1.raison_sociale;
            labelControl21.Text = login1.adresse;
            act();
            for (int i = 0; i < gridView1.DataRowCount; i++)
            {
                total_ht += Convert.ToDouble(gridView1.GetRowCellValue(i, "Montant HT"));
                labelControl23.Text = total_ht.ToString();
            }
            comboBoxEdit3.Text = "DT";
            comboBoxEdit4.Text = "DT";
         
            comboBoxEdit1.Visible = false;
            DataTable zrr = new DataTable();
            zrr = fun.affiche_infos_feur(Liste_cde.req_code);
            string id_feur;
            id_feur = zrr.Rows[0]["id_feur"].ToString();
            DataTable zcc = new DataTable();
            zcc = fun.affiche_infos_feur2(id_feur);
           labelControl15.Text=zcc.Rows[0]["raison_soc"].ToString();
           labelControl21.Text = zcc.Rows[0]["adresse_feur"].ToString();
            DataTable vv = new DataTable();
            vv = fun.affiche_infos_societe();
            string test_image_1, test_image_2;
            test_image_1 = (vv.Rows[0]["pic_societe"].ToString());
            test_image_2 = (vv.Rows[0]["pic_pied"].ToString());
         
           
            if (test_image_1 != "")
            {
                byte[] IMG = (Byte[])(vv.Rows[0]["logo_societe"]);
                MemoryStream mem = new MemoryStream(IMG);
                pictureEdit2.Image = Image.FromStream(mem);
            }
            else { pictureEdit2.Image = null; }
            if (test_image_2 != "")
            {
                byte[] IMG2 = (Byte[])(vv.Rows[0]["imm"]);
                MemoryStream mem = new MemoryStream(IMG2);
                pictureEdit1.Image = Image.FromStream(mem);
            }
            else { pictureEdit1.Image = null; }
            //information sur la société
            DataTable cc = new DataTable();
            cc = fun.affiche_infos_societe();
            labelControl52.Text = cc.Rows[0]["nom_societe"].ToString();
            labelControl51.Text = cc.Rows[0]["adresse_societe"].ToString();
            labelControl50.Text = cc.Rows[0]["matricule_societe"].ToString();
            labelControl49.Text = cc.Rows[0]["compte"].ToString();
            labelControl43.Text = cc.Rows[0]["banque"].ToString();
            labelControl48.Text = cc.Rows[0]["tel_societe"].ToString();
            labelControl47.Text = cc.Rows[0]["fax_societe"].ToString();
            labelControl46.Text = cc.Rows[0]["email_societe"].ToString();
            labelControl45.Text = cc.Rows[0]["site_societe"].ToString();
           
            use_grid = 0;
          
            labelControl28.Text = Liste_cde.req_non_feur;
            labelControl25.Text = System.DateTime.Now.ToLongDateString();
           
            this.BringToFront();
            DataTable ff = new DataTable();
            // get informations sur la commande fournisseur
            ff = fun.get_list_cdefr(Liste_cde.req_code);
            //si la commande est envoyée au fournisseur ==> commande en mode readonly , ne plus modifier.
            if (ff.Rows[0]["etatcmd"].ToString() == "Envoyée")
            { barButtonItem2.Enabled = false; barButtonItem2.Caption = "Commande Envoyée..."; use_grid = 1; }
            else { use_grid = 0; }
            if (barButtonItem2.Enabled == false) { barEditItem1.Enabled = true; barEditItem3.Enabled = true; }
            else { { barEditItem1.Enabled = false; barEditItem3.Enabled = false; } }
            if (barButtonItem2.Enabled == false)
            {
                DataTable gg = new DataTable();
                gg = fun.affiche_infos_feur(Liste_cde.req_code);
                comboBoxEdit1.Text = gg.Rows[0]["remise"].ToString();
                comboBoxEdit2.Text = gg.Rows[0]["tva"].ToString();
                labelControl23.Text = gg.Rows[0]["montant_ht"].ToString();
                labelControl31.Text = gg.Rows[0]["montant_ttc"].ToString();
                comboBoxEdit5.Text = gg.Rows[0]["timbre"].ToString();
                comboBoxEdit3.Text = gg.Rows[0]["dev"].ToString();
                comboBoxEdit4.Text = gg.Rows[0]["dev"].ToString();
                if (comboBoxEdit1.Text == "")
                {
                    checkEdit1.Checked = false;
                    comboBoxEdit1.Visible = false;
                    labelControl3.Visible = false;
                }
                else
                {

                    comboBoxEdit1.Visible = true;
                    checkEdit1.Checked = true;
                }
                comboBoxEdit1.Enabled = false;
                comboBoxEdit2.Enabled = false;
                comboBoxEdit3.Enabled = false;
                comboBoxEdit4.Enabled = false;
                comboBoxEdit5.Visible = false;
                checkEdit1.Enabled = false;
            }
        }

        private void calcule()
        {

            double temp, temp2, final;
            double timbre = 0;
            double value_after_remise = 0;
            double value_before_remise = 0;
            value_before_remise = Convert.ToDouble(labelControl23.Text.Replace('.', ','));
            if (checkEdit1.Checked == true)
            {
                if (comboBoxEdit1.Text == "")
                {
                    XtraMessageBox.Show("Choisir ou saisir un taux de remise ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    value_before_remise = Convert.ToDouble(labelControl23.Text.Replace('.',','));
                    temp = (value_before_remise * Convert.ToDouble(comboBoxEdit1.Text.Replace('.', ','))) / 100;
                    value_after_remise = value_before_remise - temp;
                    if (comboBoxEdit2.Text == "")
                    {
                        XtraMessageBox.Show("Choisir ou saisir un taux de TVA ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    else
                    {
                        temp2 = (value_after_remise * Convert.ToDouble(comboBoxEdit2.Text)) / 100;
                        //timbre = Int32.Parse(textEdit1.Text.ToString());
                        timbre = Convert.ToDouble(comboBoxEdit5.Text.Replace('.', ','));
                        final = value_after_remise + temp2 + timbre;
                        labelControl31.Text = final.ToString();
                    }
                }
            }

            else
            {

                value_after_remise = value_before_remise;
                temp2 = (value_after_remise * Convert.ToDouble(comboBoxEdit2.Text.Replace('.', ','))) / 100;

                timbre = Convert.ToDouble(comboBoxEdit5.Text.Replace('.', ','));
                final = value_after_remise + temp2 + timbre;
                labelControl31.Text = final.ToString();
            }
        }

        private void act()
        {
            //requperer la liste des piece referant à une commande
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            string b = "ajoute";
            gridControl1.DataSource = fun.get_list_piece_from_cdefr(Liste_cde.id_commande);
            this.gridView1.Columns[0].Visible = false;
            this.gridView1.Columns[1].Visible = false;
            this.gridView1.Columns[4].Visible = false;
            this.gridView1.Columns[5].Visible = false;
            this.gridView1.Columns[2].Caption = "Libellé";
            this.gridView1.Columns[3].Caption = "Quantité";
            this.gridView1.Columns[6].Caption = "Prix Unitaire HT";
            this.gridView1.Columns[7].Caption = "Montant HT";
            this.gridView1.Columns[8].Visible = false;
            this.gridView1.Columns[9].Visible = false;
            this.gridView1.Columns[10].Visible = false;
            this.gridView1.Columns[11].Visible = false;
            this.gridView1.Columns[12].Visible = false;
            gridView1.OptionsView.ShowAutoFilterRow = true;
        }
        private void bon_achat_Activated(object sender, EventArgs e)
        {
            act();

            if (login1.depart == "Utilisateur")
            {
                //exporter droit
                if (login1.expor_cde_feur == "OUI") { barButtonItem1.Enabled = true; }
                else { barButtonItem1.Enabled = false; }
                //supprimer produit de commande
                if (login1.supp_pdt_cde == "OUI") { barButtonItem3.Enabled = true; }
                else { barButtonItem3.Enabled = false; }
                //modifier quantité commandé
                if (login1.modif_qté_cde == "OUI") { barButtonItem4.Enabled = true; }
                else { barButtonItem4.Enabled = false; }
            }
            if (login1.depart == "Administrateur")
            {
                barButtonItem1.Enabled = true;
                barButtonItem3.Enabled = true;
                barButtonItem4.Enabled = true;
            }

          
            gridView1.BestFitColumns();
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.EnableAppearanceEvenRow = true;
            
        }
        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (use_grid != 1)
                {
                    Point pt = this.Location;
                    pt.Offset(this.Left + e.X, this.Top + e.Y);
                    popupMenu1.ShowPopup(this.barManager1, Control.MousePosition);
                }
            }
        }
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                //requperer l'id de piece
                req_id = Convert.ToInt32(row[0]);
            }
            etat = 1;
            quantite_piece qq = new quantite_piece();
            qq.ShowDialog();
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            layoutControl1.ShowRibbonPrintPreview();
        }
        private void barButtonItem3_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                //requperer les information sur la piece selection
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                req_id = Convert.ToInt32(row[0]);
                iddd = Convert.ToInt32(row[0]);
                sql_gmao ss = new sql_gmao();
                DataTable nn = new DataTable();
                nn = ss.get_id_piece(req_id);
                int k = Convert.ToInt32(nn.Rows[0]["id_piece"]);
                string w = "";
                //supprimer la piece de commande
                ss.update_piece_after_delete(k, w);
                ss.delete_piece(iddd);
                //actualiser datagrid
                act();
            }
        }
        private void barButtonItem4_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //modifier la quantité de piece commandée 
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                req_id = Convert.ToInt32(row[0]);
                etat = 1;
                quantite_piece qq = new quantite_piece();
                qq.ShowDialog();
            }
        }
        private void bon_achat_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string etat = "envoyée";
            if (checkEdit1.Checked == true)
            {
                if (comboBoxEdit1.Text == "")
                {
                    XtraMessageBox.Show("Choisir ou saisir un taux de remise!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (comboBoxEdit2.Text == "")
            {
                XtraMessageBox.Show("Choisir ou saisir un taux de TVA avant ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }

            //si commnade est vide alerte 
            if (gridView1.RowCount == 0)
            {
                XtraMessageBox.Show("Vous ne pouvez pas envoyer une commande vide!!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //update etat de commande envoyée
                string a = "Envoyée";
                fun.update_commande_etat(Liste_cde.req_code, a, login1.nom + " " + login1.prenom, labelControl23.Text, labelControl31.Text, comboBoxEdit2.Text, comboBoxEdit1.Text, comboBoxEdit5.Text, comboBoxEdit3.Text);
                barButtonItem2.Enabled = false; barButtonItem2.Caption = "Commande Envoyée..."; use_grid = 1;
                barEditItem1.Enabled = true; barEditItem3.Enabled = true;
            }
        }
        private void repositoryItemTextEdit1_Leave(object sender, EventArgs e)
        {
            string a, b;
            if (barEditItem1.EditValue == "")
            { a = ""; }
            else { a = barEditItem1.EditValue.ToString(); }

            if (barEditItem3.EditValue == "")
            { b = ""; }
            else { b = barEditItem3.EditValue.ToString(); }

            fun.update_commentaire_and_etat_commande(Liste_cde.req_code, a, b);

        }
        private void repositoryItemComboBox1_Leave(object sender, EventArgs e)
        {
            string a, b;
            if (barEditItem1.EditValue == "")
            { a = ""; }
            else { a = barEditItem1.EditValue.ToString(); }

            if (barEditItem3.EditValue == "")
            { b = ""; }
            else { b = barEditItem3.EditValue.ToString(); }

            fun.update_commentaire_and_etat_commande(Liste_cde.req_code, a, b);
        }

        private void comboBoxEdit1_TextChanged(object sender, EventArgs e)
        {

            if (barButtonItem2.Enabled == true)
            {
                calcule();
            }
        }

        private void comboBoxEdit2_TextChanged(object sender, EventArgs e)
        {

            if (barButtonItem2.Enabled == true)
            {
                calcule();
            }
        }

        private void comboBoxEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {

            //allow only numeric
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void comboBoxEdit2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //allow only numeric
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked == true)
            {
                comboBoxEdit1.Visible = true;
            }
            else { comboBoxEdit1.Visible = false; }
        }

        private void labelControl34_EditValueChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked == true)
            {
                if (comboBoxEdit1.Text == "")
                {
                    XtraMessageBox.Show("Choisir ou saisir un taux de remise!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }

            if (comboBoxEdit2.Text == "")
            {
                XtraMessageBox.Show("Choisir ou saisir un taux de TVA avant ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }

            if (barButtonItem2.Enabled == true)
            {
                calcule();
            }
        }

        private void comboBoxEdit3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxEdit4.Text = comboBoxEdit3.Text;
        }

        private void comboBoxEdit4_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxEdit3.Text = comboBoxEdit4.Text;
        }

        private void comboBoxEdit5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}