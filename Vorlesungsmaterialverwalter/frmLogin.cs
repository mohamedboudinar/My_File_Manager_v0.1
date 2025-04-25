using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Vorlesungsmaterialverwalter
{
    public partial class frmLogin : Form
    {
        private Database database;
        public static string username;
        public frmLogin()
        {
            InitializeComponent();
            database = new Database();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            //Testing if all the fields are filled out and not empty
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Username and Password fields are empty", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //Selecting the username and the password from the users table where there value matches with the given username and password in the forms

                bool isAuthenticated = database.loginAuthentication(txtUsername.Text, txtPassword.Text);

                //Testing if this user exists or there's something not correct 
                if (isAuthenticated)
                {
                    username = txtUsername.Text;
                    new frmDashboard().Show();
                    this.Hide();
                }
                else
                {
                    //Error message and reseting the form
                    MessageBox.Show("Invalid Username or Password, Please Try again", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                    txtUsername.Focus();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //Reseting all the form
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtUsername.Focus();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            //Opening the Register Form and closing the current one
            new frmRegister().Show();
            this.Hide();
        }

        private void checkboxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            //If the checkbox is checked then the user will be able to see the written content otherwise not
            if (checkboxShowPassword.Checked == true)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '•';
            }
        }

    }
}