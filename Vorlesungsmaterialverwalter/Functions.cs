using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vorlesungsmaterialverwalter
{
    public class Functions
    {
        private Database db = new Database(); // calling our DataBase Class
        public void Insertnewsubject(string subname)
        {
            if (db.connection.State != ConnectionState.Open) { db.Connect(); } // testing if the database not connected is , then we connect to it
            string query = "INSERT INTO Subject(Sub_Name) Values(@name) "; // Inserting a new line in the table "Subject" as a new subjectname
            SqlCommand cmd_2 = new SqlCommand(query, db.connection); //Defining new sql command
            cmd_2.Parameters.AddWithValue("@name", subname); // Defining unknown variable and refering known values to it
            cmd_2.ExecuteNonQuery(); // executing the sql command
        }
    }
}
