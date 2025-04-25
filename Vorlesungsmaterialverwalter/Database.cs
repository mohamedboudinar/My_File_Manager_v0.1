using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Vorlesungsmaterialverwalter
{

    public class Database
    {
        //Connectionstring of the database 
        public SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Mohamed Boudinar\OneDrive\Bureau\Testing\Vorlesungsmaterialverwalter\myDatabase.mdf"";Integrated Security=True;Connect Timeout=30");

        public void Connect()
        {
            try
            {
                connection.Open(); //Connecting to database

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while connecting to the database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Disconnect()
        {
            try
            {
                connection.Close(); //Disconnecting from database

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while disconnecting from the database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool ExecuteNonQuery(string query)
        {
            bool isAuthenticated = false;

            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection); //creating a new sql command using the given query in parameter and the database connection
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())            //if there's a data that matchup with a line in the user table then the value of our boolean variable will be true
                {
                    isAuthenticated = true;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while executing the query: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }

            return isAuthenticated;
        }

        public void Createtbl(string user)
        {
            if (connection.State != ConnectionState.Open) { Connect(); }

            //Creating a personnalized table in the database for each new user
            string query = "Create Table Documents_" + user + "(File_name varchar(500) not null, Data varbinary(max) not null, Extension char(4) not null, Professor_Name nchar(30) null, Subject_Name varchar(50) not null, Upload_Date date not null, primary key clustered(File_name), foreign key(Subject_Name) references Subject(Sub_Name))";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();

        }


        //Testing the LogIn
        public bool loginAuthentication(string username, string password)
        {
            //Selecting the username and the password from the users table where there value matches with the given username and password in the forms
            string loginQuery = "SELECT * FROM Users WHERE Username='" + username + "' and Password='" + password + "'";
            bool isAuthenticated = ExecuteNonQuery(loginQuery);

            return isAuthenticated;
        }

        //Adding a new User to the database
        public void addNewUser(string username, string password)
        {
            //Making sure the database is connected
            if (connection.State != ConnectionState.Open) { Connect(); }
            //Creating a new table in the database for our new user
            Createtbl(username);
            // Of course we should add his data also the our database
            string register = "INSERT INTO Users VALUES(@username,@pwd)";
            SqlCommand cmd = new SqlCommand(register, connection);

            //Defining the unknow variables in the sql query
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@pwd", password);
            cmd.ExecuteNonQuery();
            Disconnect();
        }

    }
}
