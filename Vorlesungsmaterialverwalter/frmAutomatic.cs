using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IronPdf;

namespace Vorlesungsmaterialverwalter
{
    public partial class frmAutomatic : Form
    {
        private Database db;
        private Filemanager _manager;
        public frmAutomatic()
        {
            InitializeComponent();
            db = new Database();
            _manager = new Filemanager();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (db.connection.State != ConnectionState.Open) { db.Connect(); } //testing if the database connected is or not


            // Opening PDF / Text File
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();

            // Seeking necessary Infos from it 
            using (Stream stream = File.OpenRead(dlg.FileName))
            {
                byte[] data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);
                fName.Text = new FileInfo(dlg.FileName).Name;
                fType.Text = new FileInfo(dlg.FileName).Extension;
                DateTime upDate = DateTime.Now;
                string subname = "";

                // Extracting PDF content als String
                var pdf = PdfDocument.FromFile(dlg.FileName);
                string text = pdf.ExtractAllText();

                // Adding Keywords for the research
                List<string> mykeywords = new List<string>();
                StreamReader mystream = new StreamReader("Keywords.txt");
                string line;

                while ((line = mystream.ReadLine()) != null)
                {
                    mykeywords.Add(line); //each keyword in the textfile will be added to the keywordslist
                }

                mystream.Close();

                // Testing if the Detection will work or not
                string stats = "Not OK";
                int i = 0;
                string text_1 = text.ToUpper(); //Uppercasing all the extracted text to get higher chances to the detection


                //testing everytime with a newkeyword from the list until the programm find a matchup or the list is over
                while (text_1.Contains(mykeywords[i].ToUpper()) == false && i < mykeywords.Count - 1)
                {

                    i++;

                }


                string keyword = mykeywords[i];

                //Testing again to know why the loop stopped : Is it because the programm found the word or because the list is over?
                if (text_1.Contains(keyword.ToUpper()))
                {
                    stats = "OK";

                    //Being more precised to get the correct subject name in the database
                    if (keyword.ToUpper() == "MATHE" || keyword.ToUpper() == "MATHS" || keyword.ToUpper() == "VECTOR" || keyword.ToUpper() == "MATHEMATIK" || keyword.ToUpper() == "LÖSEN SIE") { subname = "Mathematik"; }
                    else if (keyword.ToUpper() == "PROG" || keyword.ToUpper() == "PROGRAMMIERUNG 1" || keyword.ToUpper() == "PROGRAMMIERUNG 2" || keyword.ToUpper() == "C#" || keyword.ToUpper() == "KLASSE" || keyword.ToUpper() == "PROGRAM") { subname = "Programmierung"; }
                    else if (keyword.ToUpper() == "WELLE" || keyword.ToUpper() == "KONSTRUKTION" || keyword.ToUpper() == "3D" || keyword.ToUpper() == "DIN") { subname = "Konstruktion"; }
                    else if (keyword.ToUpper() == "KRAFT" || keyword.ToUpper() == "MOMENT" || keyword.ToUpper() == "MECHANIK" || keyword.ToUpper() == "STAB" || keyword.ToUpper() == "BALKEN" || keyword.ToUpper() == "SCHNITLASTEN" || keyword.ToUpper() == "STRECKENLAST") { subname = "Teschniche Mechanik"; }
                    else { subname = keyword; }


                }
                // Showing Result in Clipboard

                string query = "INSERT INTO ClipBoardtbl (File_,Data,Extension,Subject_Name,Upload_Date,Status) VALUES(@fname,@data,@extn,@subName,@upDate,@stats)";
                SqlCommand cmd = new SqlCommand(query, db.connection);
                cmd.Parameters.AddWithValue("@stats", stats); //Defining the unknow variables in the query string
                cmd.Parameters.AddWithValue("@fname", fName.Text);
                cmd.Parameters.AddWithValue("@data", data);
                cmd.Parameters.AddWithValue("@extn", fType.Text);
                cmd.Parameters.AddWithValue("@subName", subname);
                cmd.Parameters.AddWithValue("@upDate", upDate);

                cmd.ExecuteNonQuery();


            }
            db.Disconnect();




            new frmDetection().Show();

        }


    }
}
    

