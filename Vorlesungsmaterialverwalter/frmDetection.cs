using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vorlesungsmaterialverwalter
{
    public partial class frmDetection : Form
    {
        private Database db;
        private Filemanager _manager;
        private DataTable dt = new DataTable();

        public frmDetection()
        {
            InitializeComponent();
            db = new Database();
            _manager = new Filemanager();
        }

        private void frmDetection_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            if (db.connection.State != ConnectionState.Open) { db.Connect(); } //making sure the database is connected

            //Displaying all the content saved on the clipboard
            string query = "SELECT File_,Extension,Professor_Name,Subject_Name,Upload_Date,Status FROM ClipBoardtbl ";
            SqlDataAdapter ada = new SqlDataAdapter(query, db.connection);
            dt.Clear();
            ada.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                dvgDocuments.DataSource = dt;
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Deleting the selected file (already made a clarification in the dashboard form and how this works =)
            var selectedRow = dvgDocuments.SelectedRows;
            foreach (var row in selectedRow)
            {
                if (db.connection.State != ConnectionState.Open) { db.Connect(); }
                string fname = (string)((DataGridViewRow)row).Cells[0].Value;
                string query = "Delete From ClipBoardtbl WHERE file_=@fname";
                SqlCommand cmd = new SqlCommand(query, db.connection);
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Your File is successfully deleted", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                db.Disconnect();
            }
            LoadData();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            //Testing if there's already displayed and saved content on clipboard 
            if (dvgDocuments.Rows.Count > 0)
            {
                if (db.connection.State != ConnectionState.Open) { db.Connect(); }

                //Inserting these files in the personnalized table of the current user but with a constraint .. The status of the Detection must be "OK" otherwise 
                // the file can't be added to the table because there's a relationship between the tables and the subject name column can't be empty (Primary key of the table subject)
                string query = "INSERT INTO Documents_" + frmLogin.username + " (File_Name,Data,Extension,Professor_Name,Subject_Name,Upload_Date)" +
                    " SELECT File_,Data,Extension,Professor_Name,Subject_Name,Upload_Date From ClipBoardtbl WHERE Status='OK'";
                SqlCommand cmd = new SqlCommand(query, db.connection);
                cmd.ExecuteNonQuery();
                // and then deleting all the content of the clipboard so it will be ready for the next use
                string tablename = "ClipBoardtbl";
                _manager.DeleteAllFiles(tablename);


                MessageBox.Show("Done", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Close();
        }

        private void frmDetection_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if the user closes the fomr there won't be any data saved anymore on the clipboard
            string tablename = "ClipBoardtbl";
            _manager.DeleteAllFiles(tablename);

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
