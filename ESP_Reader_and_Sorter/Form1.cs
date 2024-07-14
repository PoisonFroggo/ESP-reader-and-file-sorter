using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Data.Sqlite;
using SqlKata;
using SqlKata.Compilers;


namespace ESP_Reader_and_Sorter
{
    public partial class Form1 : Form
    {
        private SqliteConnection _connection;
        private SqliteCompiler Compiler;


        // Set a global variable to hold all the selected files result
        List<String> fullFileName;
        public Form1()
        {
            InitializeComponent();
        }

        /*private void btn_OpenFile_Click(object sender, EventArgs e)
        {

            OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
            OpenFileDialog1.Multiselect = true;
            OpenFileDialog1.Title = "Seclect a  Fil";
            if (OpenFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // put the selected result in the global variable
                fullFileName = new List<String>(OpenFileDialog1.FileNames);

                // add just the names to the listbox
                foreach (string fileName in fullFileName)
                {
                    listBx_files.Items.Add(fileName.Substring(fileName.LastIndexOf(@"\") + 1));
                }


            }
            
            FileStream fs = new FileStream(@"C:\Users\nc471\Downloads\Interplay 10mm SMG\Interplay10mmSMG.esp", FileMode.Open);
            int hexIn;
            String hex;

            for (int i = 0; (hexIn = fs.ReadByte()) != -1; i++)
            {
                hex = string.Format("{0:X2}", hexIn);
                labl_FileRead.Text += hex;
            }
            
        }

        private void btn_openDatabase_Click(object sender, EventArgs e)
        {
            _connection = CreateConnection();
            foreach (var item in listBx_files.Items)
            {
                string fileName = item.ToString();

                // Insert each item from the list box into the database
                using (SqliteConnection connection = new SqliteConnection("Data Source= database.db;"))
                {
                    connection.Open();

                    string sql = "CREATE TABLE IF NOT EXISTS Files (" +
                        "FileID INTEGER PRIMARY KEY AUTOINCREMENT," +
                        "FileName TEXT NOT NULL" +
                        ");" +
                        "INSERT INTO Files (FileName) VALUES (@fileName)";
                    using (SqliteCommand cmd = new SqliteCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@fileName", fileName);
                        cmd.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
        }

        static SqliteConnection CreateConnection()
        {

            SqliteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SqliteConnection("Data Source= " +
                "database.db;");
            // Open the connection:
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {

            }
            return sqlite_conn;
        }*/
    } 
}