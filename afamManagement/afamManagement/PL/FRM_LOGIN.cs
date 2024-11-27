using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace afamManagement.PL
{
    public partial class FRM_LOGIN : Form
    {
        AFAM_Management.BL.CLS_LOGIN log = new AFAM_Management.BL.CLS_LOGIN();
        public FRM_LOGIN()
        {
            InitializeComponent();
        }
        
        private void FRM_LOGIN_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            DataTable Dt = log.LOGIN(txtID.Text, txtPWD.Text);
            if (Dt.Rows.Count > 0)
            {
                FRM_MAIN.getMainForm.restoreBackupToolStripMenuItem.Enabled = true;
                FRM_MAIN.getMainForm.usersToolStripMenuItem.Enabled = true;
                FRM_MAIN.getMainForm.productsToolStripMenuItem.Enabled = true;
                FRM_MAIN.getMainForm.customersToolStripMenuItem.Enabled = true;
                FRM_MAIN.getMainForm.exportBackupToolStripMenuItem.Enabled = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Login Failed!");
            }
        }
    }
}
