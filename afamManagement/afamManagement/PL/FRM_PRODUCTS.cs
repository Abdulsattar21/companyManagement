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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.Shared;
namespace afamManagement.PL
{
    public partial class FRM_PRODUCTS : Form
    {
        private static FRM_PRODUCTS frm;
        // Those stuff is explianed in Class no: 11 in السعداني Course; 
        // you can check but it is almost explained better then here because I barely understanded it;
        static void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm = null;

        }
        public static FRM_PRODUCTS getMainForm
        {
            get
            {
                if (frm == null)
                {
                    frm = new FRM_PRODUCTS();
                    frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
                }
                return frm;
            }
        }
        BL.CLS_PRODUCTS prd = new BL.CLS_PRODUCTS();
        public FRM_PRODUCTS()
        {
            InitializeComponent();
            if (frm == null)
                frm = this;
            this.dataGridView1.DataSource = prd.GET_ALL_PRODUCTS();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FRM_ADD_PRODUCT frm = new FRM_ADD_PRODUCT();
            frm.ShowDialog();
            this.dataGridView1.DataSource = prd.GET_ALL_PRODUCTS();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = prd.SearchProducts(txtSearch.Text);
            this.dataGridView1.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure?","Delete Product",MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)==DialogResult.Yes)
            {
                prd.DeleteProduct(this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
                MessageBox.Show("Deleted Successfully!", "Delete Product", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.dataGridView1.DataSource = prd.GET_ALL_PRODUCTS();
            }
            else
            {
                MessageBox.Show("Delete Canceled!", "Delete Product", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FRM_ADD_PRODUCT frm = new FRM_ADD_PRODUCT();
            frm.txtRef.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            frm.txtDes.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            frm.txtQte.Text = this.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            frm.txtPrice.Text = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
            frm.cmbCategories.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
            frm.Text="Product Data Update: "+ this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            frm.btnOk.Text = "Update";
            frm.state = "update";
            frm.txtRef.ReadOnly = true;
            byte[] image = (byte[])prd.GET_IMAGE_PRRODUCT(this.dataGridView1.CurrentRow.Cells[0].Value.ToString()).Rows[0][0];
            MemoryStream ms = new MemoryStream(image);
            frm.pbox.Image = Image.FromStream(ms);
            frm.ShowDialog();
            this.dataGridView1.DataSource = prd.GET_ALL_PRODUCTS();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FRM_PREVIEW frm = new FRM_PREVIEW();
            byte[] image = (byte[])prd.GET_IMAGE_PRRODUCT(this.dataGridView1.CurrentRow.Cells[0].Value.ToString()).Rows[0][0];
            MemoryStream ms = new MemoryStream(image);
            frm.pictureBox1.Image = Image.FromStream(ms);
            frm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RPT2.rpt_prd_single1 myReport1 = new RPT2.rpt_prd_single1();
            myReport1.SetParameterValue("@ID", this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
            RPT2.FRM_RPT_PRODUCT myForm1 = new RPT2.FRM_RPT_PRODUCT();
            myForm1.crystalReportViewer2.ReportSource = myReport1;
            myForm1.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            RPT2.rpt_all_products myReport = new RPT2.rpt_all_products();
            RPT2.FRM_RPT_PRODUCT myForm = new RPT2.FRM_RPT_PRODUCT();
            myForm.crystalReportViewer2.ReportSource = myReport;
            myForm.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            RPT2.rpt_all_products myReport = new RPT2.rpt_all_products();
            // Create export Options
            ExportOptions export = new ExportOptions();
            // create object of destination 
            DiskFileDestinationOptions dfoption = new DiskFileDestinationOptions();
            ExcelFormatOptions excelFormat = new ExcelFormatOptions();
            // Set the path of the destination 
            // if you want just replace "xls" with "docx" or "pdf"
            dfoption.DiskFileName = @"D:\ProductsList.xls";
            export = myReport.ExportOptions;
            // 
            export.ExportDestinationType = ExportDestinationType.DiskFile;
            // set the file Type, there is PDF, Word, Excel and other cool stuff that you need to check it out
            export.ExportFormatType = ExportFormatType.Excel;
            export.ExportFormatOptions = excelFormat;
            export.ExportDestinationOptions = dfoption;
            myReport.Export();
            MessageBox.Show("List Exported Successfully!", "Export", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
