using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESP_Reader_and_Sorter
{
    public partial class LoginControl : UserControl
    {
        public LoginControl()
        {
            InitializeComponent();
        }
            //event and bool for handling the login process

        String exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        string path = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName;


        private bool isTrue = false;
        public event EventHandler<EventArgs>? LoginPerformed;
        public void OnLoginPerformed()
        {
            LoginPerformed?.Invoke(this, EventArgs.Empty);
        }
        private void btn_SubmitLogin_Click(object sender, EventArgs e)
        {
            // Example logic to handle login submission
            string password = txtbx_Password.Text;
            string username = txtbx_Username.Text;

            // Perform your authentication logic here
            bool loginSuccessful = AuthenticateUser(username, password);

            if (loginSuccessful)
            {
                MessageBox.Show("Login successful!");
                OnLoginPerformed();
            }
            else
            {
                MessageBox.Show("Login failed. Please try again.");
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            // Example authentication logic (replace with your actual logic)
            // Here, we are just checking if username and password are not empty
            return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);
        }

        private void btn_RegisterLogin_Click(object sender, EventArgs e)
        {
            String password = txtbx_Password.Text;
            String username = txtbx_Username.Text;
            PrintSQLiteDatabases(exePath);
            InsertCredentials(username, password);
            //PrintSQLiteDatabases(path);
            if (password != null && username != null)
            {
                InsertCredentials(username, password);
                PrintTableNames();
            }

        }




        public void InsertCredentials(string username, string password)
        {
            MessageBox.Show("Function InsertCredentials called");
            SqliteConnection myConn = new SqliteConnection("DataSource=database.db;");
            myConn.Open();
            MessageBox.Show("Connection Opened");

            //Use to create tables if needed
            string createTableQuery = @"
            CREATE TABLE IF NOT EXISTS ""Accounts"" (
            	""ID""	INTEGER NOT NULL UNIQUE,
            	""username""	TEXT NOT NULL UNIQUE,
            	""pw""	INTEGER NOT NULL UNIQUE,
            	""gameName""	TEXT NOT NULL,
            	PRIMARY KEY(""ID"" AUTOINCREMENT)
                        );";

            // Use just in case the tables were done incorrectly 
            string dropTables = @"DROP TABLE IF EXISTS ""Files""; 
                DROP TABLE IF EXISTS ""ESP"";
                DROP TABLE IF EXISTS ""Accounts"";";

            string sql = "CREATE TABLE IF NOT EXISTS highscores (name VARCHAR(20), score INT)";
            SqliteCommand command = new SqliteCommand(createTableQuery, myConn);
            command.ExecuteNonQuery();

            myConn.Close();
            MessageBox.Show("Connection Closed");
        }

        // Function to print all SQLite databases in a directory
        public void PrintSQLiteDatabases(string directoryPath)
        {
            MessageBox.Show("Calling function PrintSQLiteDatabases");
            // Check if the directory exists
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine("Directory does not exist.");
                return;
            }

            // Get all files in the directory with .sqlite or .db extension
            string[] files = Directory.GetFiles(directoryPath, "*.sqlite");
            files = files.Concat(Directory.GetFiles(directoryPath, "*.db")).ToArray();

            // Print the list of databases
            MessageBox.Show("SQLite Databases in " + Assembly.GetExecutingAssembly().Location).ToString();
            foreach (string file in files)
            {
                MessageBox.Show(Path.GetFileName(file));
            }
        }


        public void PrintTableNames()
        {
            MessageBox.Show("Calling function PrintTableNames");
            // Establish connection to SQLite database

            SqliteConnection myConn = new SqliteConnection("DataSource=database.db;");
            myConn.Open();
            MessageBox.Show("Connection Opened");


            string query = "SELECT name FROM sqlite_schema WHERE type='table';";
            SqliteCommand cmd = new SqliteCommand(query, myConn);
            SqliteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string tableName = reader.GetString(0);
                MessageBox.Show(tableName);
            }

            myConn.Close();
            MessageBox.Show("Connection Closed");

        }

        public void PrintTableContents()
        {
            MessageBox.Show("Calling function PrintTableContents");
            // Establish connection to SQLite database

            SqliteConnection myConn = new SqliteConnection("DataSource=database.db;");
            myConn.Open();
            MessageBox.Show("Connection Opened");


            string query = "SELECT name FROM sqlite_schema WHERE type='table';";
            SqliteCommand cmd = new SqliteCommand(query, myConn);
            SqliteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string tableName = reader.GetString(0);
                MessageBox.Show(tableName);
            }

            myConn.Close();
            MessageBox.Show("Connection Closed");

        }
    }
}
