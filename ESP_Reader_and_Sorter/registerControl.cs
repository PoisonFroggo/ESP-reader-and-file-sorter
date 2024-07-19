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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ESP_Reader_and_Sorter
{
    public partial class LoginControl : UserControl
    {
        public string Username { get; private set; }
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

            if (CheckCredentials(username, password))
            {
                MessageBox.Show("Login successful!");
                Username = username;
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
            //PrintSQLiteDatabases(exePath); d
            if (UsernameExists(username) || PasswordExists(password))
            {
                MessageBox.Show("Account with this username already exists");
            }
            else
            {
                InsertCredentials(username, password);
                //PrintTableNames();
            }

        }

        private bool UsernameExists(string username)
        {
            string dbFilePath = "database.db";
            string connectionString = $"Data Source={dbFilePath};";
            using (SqliteConnection myConn = new SqliteConnection(connectionString))
            {
                myConn.Open();

                string checkExistingQuery = @"SELECT COUNT(*) FROM ""Accounts"" WHERE ""username"" = $username";
                using (SqliteCommand checkCommand = new SqliteCommand(checkExistingQuery, myConn))
                {
                    checkCommand.Parameters.AddWithValue("$username", username);

                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private bool PasswordExists(string password)
        {
            string dbFilePath = "database.db";
            string connectionString = $"Data Source={dbFilePath};";
            using (SqliteConnection myConn = new SqliteConnection(connectionString))
            {
                myConn.Open();

                string checkExistingQuery = @"SELECT COUNT(*) FROM ""Accounts"" WHERE ""pw"" = $pw";
                using (SqliteCommand checkCommand = new SqliteCommand(checkExistingQuery, myConn))
                {
                    checkCommand.Parameters.AddWithValue("$pw", password);

                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                    return count > 0;
                }
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
CREATE TABLE IF NOT EXISTS ""Files"" (
    ""fileName"" TEXT NOT NULL,
    ""prereq"" TEXT,
    PRIMARY KEY(""fileName""),
    FOREIGN KEY(""prereq"") REFERENCES ESP(""fileName"")
);

CREATE TABLE IF NOT EXISTS ""ESP"" (
    ""gameName"" TEXT,
    ""fileName"" TEXT NOT NULL,
    ""espPath"" TEXT NOT NULL,
    ""espID"" TEXT NOT NULL,
    FOREIGN KEY (""espID"") REFERENCES Accounts(""username"")
);

CREATE TABLE IF NOT EXISTS ""Accounts"" (
    ""ID"" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    ""username"" TEXT NOT NULL UNIQUE,
    ""pw"" TEXT NOT NULL  -- Store passwords securely in production
);
";

            // Use just in case the tables were done incorrectly 
            string dropTables = @"  DROP TABLE IF EXISTS ""Files""; 
                                    DROP TABLE IF EXISTS ""ESP"";
                                    DROP TABLE IF EXISTS ""Accounts"";";

            //SqliteCommand comm1 = new SqliteCommand(dropTables, myConn);
            //comm1.ExecuteNonQuery();
            //MessageBox.Show("Tables dropped");
            //SqliteCommand command = new SqliteCommand(createTableQuery, myConn);
            //command.ExecuteNonQuery();

            String insertQuery = @"INSERT INTO ""Accounts"" (""username"", ""pw"")
                                    VALUES ($username, $password)";
            using (SqliteCommand insertCommand = new SqliteCommand(insertQuery, myConn))
            {
                insertCommand.Parameters.AddWithValue("$username", username);
                insertCommand.Parameters.AddWithValue("$password", password);
                insertCommand.ExecuteNonQuery();
            }
            MessageBox.Show("Credentials inserted successfully!");

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


        public static bool CheckCredentials(string username, string pw)
        {
            string dbFilePath = "database.db";
            string connectionString = $"Data Source={dbFilePath};";
            bool credentialsValid = false;

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT 1 FROM Accounts WHERE username = @username AND pw = @pw LIMIT 1;";
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@pw", pw);

                    object result = command.ExecuteScalar();
                    credentialsValid = (result != null && result != DBNull.Value);
                }

                connection.Close();
            }

            return credentialsValid;
        }








        /********************************************DEBUG FUNCTIONS***********************************************************/
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
