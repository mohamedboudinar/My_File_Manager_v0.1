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
    public partial class Dashboard : Form
    {
        private DataTable dt = new DataTable();
        private Database db;
        private Filemanager _manager;
        public Dashboard()
        {
            InitializeComponent();
            db = new Database();
            _manager = new Filemanager();
        }
        private void DataLoad()
        {
            //Selecting all the contents (if there's content) from the documents table of the current user
            if (db.connection.State != ConnectionState.Open) { db.Connect(); }
            string query = "Select File_Name,Extension,Professor_Name,Subject_Name,Upload_Date FROM Documents_" + frmLogin.username + " Order By Subject_Name ";

            //Creating a new adapter using the Query and databaseconnection
            SqlDataAdapter adapter = new SqlDataAdapter(query, db.connection);


            //Clearing our old data table
            dt.Clear();

            //Filling it out again with fresh data
            adapter.Fill(dt);

            //testing if the data table has already at least one line or data in it or it's empty
            if (dt.Rows.Count > 0)
            {
                dgvDocuments_2.DataSource = dt;    //the Datagridview will project the content of the data table
            }
            else { label5.Text = "You didn't upload any documents yet..."; }

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            var selectedRow = dgvDocuments_2.SelectedRows;  //defining the selected row in the datagridview
            foreach (var row in selectedRow)
            {
                string fname = (string)((DataGridViewRow)row).Cells[0].Value;//getting the Filename of the selected row
                string username = frmLogin.username;       //getting the username of the current user
                _manager.Openfile(fname, username);   //executing the function Openfile from Filemanager class

            }
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var selectedRow = dgvDocuments_2.SelectedRows;  //defining the selected row in the datagridview
            foreach (var row in selectedRow)
            {
                string fname = (string)((DataGridViewRow)row).Cells[0].Value; //getting the Filename of the selected row
                string tablename = "Documents_" + frmLogin.username;      //trying to define the correct table in database related to the current user
                _manager.DeleteFile(tablename, fname); //executing the delete file methode of Filemanager class 
                DataLoad();
                MessageBox.Show("Your File is successfully deleted", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                db.Disconnect();
            }
        }

        private void searchBar_TextChanged(object sender, EventArgs e)
        {
            DataView dvFiles = dt.DefaultView;   //intialising the Dataview of our data table

            //Filtering the shown files and showing only the ones that has in their name the string written in  the search bar
            dvFiles.RowFilter = "File_Name like '%" + searchBar.Text + "%'";
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            //Selecting the given Columns where the upload date matches with the given range of date
            string query = "SELECT File_Name,Extension,Professor_Name,Subject_Name,Upload_Date FROM Documents_" + frmLogin.username + " WHERE Upload_Date Between @date1 and @date2 Order By Upload_Date asc";
            SqlDataAdapter ada = new SqlDataAdapter(query, db.connection);
            ada.SelectCommand.Parameters.AddWithValue("@date1", calendarFrom.Value);  //Defining the unknow variables in the query 
            ada.SelectCommand.Parameters.AddWithValue("@date2", calendarUntil.Value);

            dt.Clear(); //clearing the old datatable
            ada.Fill(dt); //filling it out again with the new data 
            if (dt.Rows.Count > 0)
            {
                dgvDocuments_2.DataSource = dt;  // showing the content of the data table in the datagridview (if there's content)
            }
        }
    }
}
