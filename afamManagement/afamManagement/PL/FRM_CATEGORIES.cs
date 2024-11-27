using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.Shared;


namespace afamManagement.PL
{
    public partial class FRM_CATEGORIES : Form
    {
        SqlConnection sqlcon = new SqlConnection(@"Server= .\SQLEXPRESS; Database=Product_DB; Integrated Security=True");
        SqlDataAdapter da;
        DataTable dt = new DataTable();
        BindingManagerBase bmb;
        SqlCommandBuilder cmdb;
        public FRM_CATEGORIES()
        {
            InitializeComponent();
            da = new SqlDataAdapter("select id_cat as 'ID', DESCRIPTION_CAT as 'Description'  from categories", sqlcon);
            da.Fill(dt);
            dgList.DataSource = dt;
            textID.DataBindings.Add("text", dt, "ID");
            textDes.DataBindings.Add("text", dt, "Description");
            // course number 88 in C# course "Khalid Alsadani"
            bmb = this.BindingContext[dt];
            lblPosition.Text = (bmb.Position + 1) + " / " + bmb.Count;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            bmb.Position = 0;
            lblPosition.Text = (bmb.Position + 1) + " / " + bmb.Count;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            bmb.Position = bmb.Count;
            lblPosition.Text = (bmb.Position + 1) + " / " + bmb.Count;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            bmb.Position -= 1;
            lblPosition.Text = (bmb.Position + 1) + " / " + bmb.Count;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            bmb.Position += 1;
            lblPosition.Text = (bmb.Position + 1) + " / " + bmb.Count;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            bmb.AddNew();
            btnNew.Enabled = false;
            btnAdd.Enabled = true;
            int id = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][0]) + 1;
            textID.Text = Convert.ToString(id);
            textDes.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bmb.EndCurrentEdit();
            cmdb = new SqlCommandBuilder(da);
            da.Update(dt);
            MessageBox.Show("Added Successfully", "ADD", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnAdd.Enabled = false;
            btnNew.Enabled = true;
            lblPosition.Text = (bmb.Position + 1) + " / " + bmb.Count;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bmb.RemoveAt(bmb.Position);
            bmb.EndCurrentEdit();
            cmdb = new SqlCommandBuilder(da);
            da.Update(dt);
            MessageBox.Show("Deleted Successfully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblPosition.Text = (bmb.Position + 1) + " / " + bmb.Count;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            bmb.EndCurrentEdit();
            cmdb = new SqlCommandBuilder(da);
            da.Update(dt);
            MessageBox.Show("Edited Successfully", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblPosition.Text = (bmb.Position + 1) + " / " + bmb.Count;
        }

        private void btnPrintCurrent_Click(object sender, EventArgs e)
        {
            RPT2.rpt_single_category rpt = new RPT2.rpt_single_category();
            RPT2.FRM_RPT_PRODUCT frm = new RPT2.FRM_RPT_PRODUCT();
            rpt.SetParameterValue("@id", Convert.ToInt32(textID.Text));
            frm.crystalReportViewer2.ReportSource = rpt;
            frm.ShowDialog();
        }

        private void btnPrintAll_Click(object sender, EventArgs e)
        {
            RPT2.rpt_all_categories rpt = new RPT2.rpt_all_categories();
            RPT2.FRM_RPT_PRODUCT frm = new RPT2.FRM_RPT_PRODUCT();
            rpt.Refresh();
            frm.crystalReportViewer2.ReportSource = rpt;
            frm.ShowDialog(); 
        }

        private void exportToPdfALl_Click(object sender, EventArgs e)
        {
            RPT2.rpt_all_categories myReport = new RPT2.rpt_all_categories();
            // Create export Options
            ExportOptions export = new ExportOptions();
            // create object of destination 
            DiskFileDestinationOptions dfoption = new DiskFileDestinationOptions();
            PdfFormatOptions pdfformat = new PdfFormatOptions();
            // Set the path of the destination 
            // if you want just replace "xls" with "docx" or "pdf"
            dfoption.DiskFileName = @"D:\CategoriesList.pdf";
            export = myReport.ExportOptions;
            // 
            export.ExportDestinationType = ExportDestinationType.DiskFile;
            // set the file Type, there is PDF, Word, Excel and other cool stuff that you need to check it out
            export.ExportFormatType = ExportFormatType.PortableDocFormat;
            export.ExportFormatOptions = pdfformat;
            export.ExportDestinationOptions = dfoption;
            myReport.Refresh();
            myReport.Export();
            MessageBox.Show("List Exported Successfully!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void exportToPdfCurrent_Click(object sender, EventArgs e)
        {
            RPT2.rpt_single_category myReport = new RPT2.rpt_single_category();
            // Create export Options
            ExportOptions export = new ExportOptions();
            // create object of destination 
            DiskFileDestinationOptions dfoption = new DiskFileDestinationOptions();
            PdfFormatOptions pdfformat = new PdfFormatOptions();
            // Set the path of the destination 
            // if you want just replace "xls" with "docx" or "pdf"
            dfoption.DiskFileName = @"D:\CategoryDetails.pdf";
            export = myReport.ExportOptions;
            // 
            export.ExportDestinationType = ExportDestinationType.DiskFile;
            // set the file Type, there is PDF, Word, Excel and other cool stuff that you need to check it out
            export.ExportFormatType = ExportFormatType.PortableDocFormat;
            export.ExportFormatOptions = pdfformat;
            export.ExportDestinationOptions = dfoption;
            myReport.SetParameterValue("@id", Convert.ToInt32(textID.Text));
            // myReport.Refresh();
            myReport.Export();
            MessageBox.Show("List Exported Successfully!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
