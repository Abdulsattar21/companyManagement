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
    public partial class FRM_MAIN : Form
    {
        private static FRM_MAIN frm; 
        // Those stuff is explianed in Class no: 11 in السعداني Course; 
        // you can check but it is almost explained better then here because I barely understanded it;
        static void frm_FormClosed(object sender,FormClosedEventArgs e)
        {
            frm = null;

        }
        public static FRM_MAIN getMainForm
        {
            get
            {
                if(frm == null)
                {
                    frm = new FRM_MAIN();
                    frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
                }
                return frm;
            }
        }
        public FRM_MAIN()
        {
            InitializeComponent();
            if (frm == null)
                frm = this;
            this.usersToolStripMenuItem.Enabled = false;
            this.productsToolStripMenuItem.Enabled = false;
            this.customersToolStripMenuItem.Enabled = false;
            this.exportBackupToolStripMenuItem.Enabled = false;
            this.restoreBackupToolStripMenuItem.Enabled = false;

        }

        private void FRM_MAIN_Load(object sender, EventArgs e)
        {

        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRM_LOGIN frm = new FRM_LOGIN();
            frm.ShowDialog();
        }

        private void addNewProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRM_ADD_PRODUCT frm = new FRM_ADD_PRODUCT();
            frm.ShowDialog();
        }

        private void productsManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRM_PRODUCTS frm = new FRM_PRODUCTS();
            frm.ShowDialog();
        }

        private void typesManagementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRM_CATEGORIES frm = new FRM_CATEGORIES();
            frm.ShowDialog();
            // there is so many different ways to get the data, so here in this program we are 
            // using this method. in cate. page we use another method to get data from sql | 
        }
    }
}
