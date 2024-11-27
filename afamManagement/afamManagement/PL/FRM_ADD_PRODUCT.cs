using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace afamManagement.PL
{
    public partial class FRM_ADD_PRODUCT : Form
    {
        public string state = "add";
        // this for the update part 
        BL.CLS_PRODUCTS prd = new BL.CLS_PRODUCTS();
        public FRM_ADD_PRODUCT()
        {
           // string state = "add";
            InitializeComponent();
            cmbCategories.DataSource = prd.GET_ALL_CATEGORIES();
            cmbCategories.DisplayMember = "DESCRIPTION_CAT";
            cmbCategories.ValueMember = "ID_CAT";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images | *.JPG;*.PNG;*.GIF;*.BMP";
            if(ofd.ShowDialog()== DialogResult.OK)
            {
                pbox.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pbox_Click(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (state == "add")
            {
                MemoryStream ms = new MemoryStream();
                pbox.Image.Save(ms, pbox.Image.RawFormat);
                byte[] byteImage = ms.ToArray();

                prd.ADD_PRODUCT(Convert.ToInt32(cmbCategories.SelectedValue), txtDes.Text, txtRef.Text
                                , Convert.ToInt32(txtQte.Text), txtPrice.Text, byteImage);
                MessageBox.Show("Add Completed successfully", "Add Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MemoryStream ms = new MemoryStream();
                pbox.Image.Save(ms, pbox.Image.RawFormat);
                byte[] byteImage = ms.ToArray();

                prd.UPDATE_PRODUCT(Convert.ToInt32(cmbCategories.SelectedValue), txtDes.Text, txtRef.Text
                                , Convert.ToInt32(txtQte.Text), txtPrice.Text, byteImage);
                MessageBox.Show("Update Completed successfully", "Updeate Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            FRM_PRODUCTS.getMainForm.dataGridView1.DataSource = prd.GET_ALL_PRODUCTS();
        }

        private void txtRef_Validated(object sender, EventArgs e)
        {
            if (state == "add")
            {
                DataTable Dt = new DataTable();
                Dt = prd.verifyProductID(txtRef.Text);
                if (Dt.Rows.Count > 0)
                {
                    MessageBox.Show("This Product is already ADDED", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // This to return the foucse to the txtRef when you enter invaild values 
                    txtRef.Focus();
                    txtRef.SelectionStart = 0;
                    txtRef.SelectionLength = txtRef.TextLength;
                }
            }
        }

        private void txtRef_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
