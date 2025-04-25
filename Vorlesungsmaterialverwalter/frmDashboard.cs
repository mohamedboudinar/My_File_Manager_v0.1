using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vorlesungsmaterialverwalter
{
    public partial class frmDashboard : Form
    {
        public frmDashboard()
        {
            InitializeComponent();
            //As soon as the programm opens the Dashboard form will appear first as default
            lbTitel.Text = "Dashboard"; //The title of the form
            this.pnlFormLoader.Controls.Clear();  //Making sure that the panel clear and ready to load new form
            Dashboard dashboard = new Dashboard() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            dashboard.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(dashboard); //Adding the Dashboard form in the pannel
            dashboard.Show(); //Displaying it
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            //As soon as the programm opens the Dashboard form will appear first as default
            lbTitel.Text = "Dashboard"; //The title of the form
            this.pnlFormLoader.Controls.Clear();  //Making sure that the panel clear and ready to load new form
            Dashboard dashboard = new Dashboard() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            dashboard.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(dashboard); //Adding the Dashboard form in the pannel
            dashboard.Show(); //Displaying it

        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            //Same thing we did for the Dashboard form but this time with a different form
            lbTitel.Text = "Upload your Files";
            this.pnlFormLoader.Controls.Clear();
            frmUpload upload = new frmUpload() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            upload.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(upload);
            upload.Show();
        }

        private void btnAboutUs_Click(object sender, EventArgs e)
        {
            //Same thing we did for the Dashboard form but this time with a different form

            lbTitel.Text = "About Us";
            this.pnlFormLoader.Controls.Clear();
            frmAboutUs aboutUs = new frmAboutUs() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            aboutUs.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(aboutUs);
            aboutUs.Show();
        }

        private void btnContact_Click(object sender, EventArgs e)
        {
            //Same thing we did for the Dashboard form but this time with a different form

            lbTitel.Text = "Contact Us";
            this.pnlFormLoader.Controls.Clear();
            frmContactUs contact = new frmContactUs() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            contact.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(contact);
            contact.Show();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            //Opening the Log In form and closing the current form
            new frmLogin().Show();
            this.Hide();
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            //Displaying the username of the current connected User 
            if (frmLogin.username != "") { username_id.Text = frmLogin.username; }
        }
    }
}
