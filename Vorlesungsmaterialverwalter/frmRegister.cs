using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Vorlesungsmaterialverwalter
{
    public partial class frmRegister : Form
    {
        private Database database;

        public frmRegister()
        {
            InitializeComponent();
            database = new Database();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtConPassword.Text;

            //Testing if all the fields are filled out 
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("You have to fill out all the fields . Please try again.", "Signup Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //making sure that the password and password confirmation are matching 
            else if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Text = "";
                txtConPassword.Text = "";
                txtPassword.Focus();
            }
            else
            {
                try
                {
                    database.addNewUser(username, password);


                    //When everything works, the current form will be reseted for the next use
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                    txtConPassword.Text = "";

                    MessageBox.Show("Your account has been successfully created.", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred during registration: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void checkboxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            //If the checkbox is checked then both the content of the Password and passwordconfirmation fields will be displayed otherwise not
            if (checkboxShowPassword.Checked)
            {
                txtPassword.PasswordChar = '\0';
                txtConPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '•';
                txtConPassword.PasswordChar = '•';
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //Reseting the current Form
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtConPassword.Text = "";
            txtUsername.Focus();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            //Closing the current form and oppent the Log In Form
            new frmLogin().Show();
            this.Hide();
        }

    }
}