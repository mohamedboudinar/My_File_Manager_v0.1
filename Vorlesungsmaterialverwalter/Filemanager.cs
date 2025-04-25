using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Vorlesungsmaterialverwalter
{
    public class Filemanager
    {
        Database db = new Database();          // calling our Database Class.
        public void Openfile(string filename, string username)
        {
            if (db.connection.State != ConnectionState.Open) { db.Connect(); }          // Checking if the Database is not connected. If is true then we connect it. 
            string query = "SELECT File_Name,Extension,Data FROM Documents_" + username + "WHERE File_Name=@filename";
            SqlCommand cmd = new SqlCommand(query, db.connection);          // Defining a new SQL command.
            cmd.Parameters.AddWithValue("@filename", filename);         // Definig unknown variable and refering known values to it 

            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string name = reader["File_Name"].ToString();           // Extracting file name.
                byte[] data = (byte[])reader["data"];           // Extracting the data of the file.
                string extn = reader["Extension"].ToString();           // Extracting the extension of the file.

                string newFileName = name.Replace(extn, DateTime.Now.ToString("ddMMyyhhmmss"));         // Creating a new name to the file.
                File.WriteAllBytes(newFileName, data);          // Creating the new file.
                System.Diagnostics.Process.Start(newFileName);          // Executing the new file.
            }
            reader.Close();
        }
        public void DeleteFile(string tbename, string name)
        {
            try
            {
                if (db.connection.State != ConnectionState.Open) { db.Connect(); }           // Checking if the Database is not connected. If is true then we connect it. 
                string query = "DELETE FROM" + tbename + "WHERE File_Name=@name";           // Deleteing the whole line in the given table where the file name matches with the given name.
                SqlCommand cmd = new SqlCommand(query, db.connection);           // Defining a new SQL command.
                cmd.Parameters.AddWithValue("@name", name);         // Definig unknown variable and refering known values to it. 
                cmd.ExecuteNonQuery();          // Executing the SQL command          
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured during file deletion: " + ex.Message);
            }
        }
        public void DeleteAllFiles(string tbename)
        {
            try
            {
                if (db.connection.State != ConnectionState.Open) { db.Connect(); }           // Checking if the Database is not connected. If is true then we connect it. 
                string query = "DELETE FROM" + tbename;           // Deleteing the whole line in the given table where the file name matches with the given name.
                SqlCommand cmd = new SqlCommand(query, db.connection);           // Defining a new SQL command.
                cmd.ExecuteNonQuery();          // Executing the SQL command.      
            }
            catch (Exception ex)
            {
                MessageBox.Show("An erroe occurred during file deletion: " + ex.Message);
            }
        }
        public void AddFile(string filePath, string prof, string subject, string tablename)
        {
            try
            {

                if (db.connection.State != ConnectionState.Open) { db.Connect(); }
                using (Stream stream = File.OpenRead(filePath))      //opening a file to read
                {
                    byte[] data = new byte[stream.Length];           //creating a new array of bytes that has limit the same as the length of the stream
                    stream.Read(data, 0, data.Length);            //reading the stream

                    string filename = new FileInfo(filePath).Name;        //getting filename
                    string profName = prof;                            //proffessorname is already given in the parameter of the funtion
                    string subName = subject;                       // same for the subject name
                    DateTime date = DateTime.Now;
                    string ext = new FileInfo(filePath).Extension; // getting the extension of the file

                    //Inserting the given values into the personnalized user table in the database
                    string query = "INSERT INTO " + tablename + "(File_name,Data,Extension,Professor_Name,Subject_Name,Upload_Date) VALUES(@filename,@data,@ext,@profName,@subName,@date)"; //Inserting these values in the personnalized table in database of the user

                    using (db.connection)
                    {
                        SqlCommand cmd = new SqlCommand(query, db.connection);

                        cmd.Parameters.AddWithValue("@filename", filename); //Defining the unknow Variables in our sql query
                        cmd.Parameters.AddWithValue("@data", data);
                        cmd.Parameters.AddWithValue("@ext", ext);
                        cmd.Parameters.AddWithValue("@profName", profName);
                        cmd.Parameters.AddWithValue("@subName", subName);
                        cmd.Parameters.AddWithValue("@date", date);



                        cmd.ExecuteNonQuery(); //Executing the command

                    }

                }
                db.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred during file upload: " + ex.Message);
            }
        }
    }
}
