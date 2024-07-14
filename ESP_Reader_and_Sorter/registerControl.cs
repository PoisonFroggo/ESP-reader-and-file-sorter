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

        String exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        string path = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName;

        private void btn_SubmitLogin_Click(object sender, EventArgs e)
        {
            String password = txtbx_Password.Text;
            String username = txtbx_Username.Text;

            if (password != null && username != null)
            {
                var Sql = @"CREATE TABLE IF NOT EXISTS database.authors(
                                    id INTEGER AUTOINCREMENT PRIMARY KEY,
                                    first_name TEXT NOT NULL,
                                    last_name TEXT NOT NULL
                                    )";
            }
        }

        private void btn_RegisterLogin_Click(object sender, EventArgs e)
        {
            String password = txtbx_Password.Text;
            String username = txtbx_Username.Text;
            PrintSQLiteDatabases(exePath);
            //PrintSQLiteDatabases(path);
            if (password != null && username != null)
            {
                InsertCredentials(username, password);
                PrintTableNames();
            }

        }


        public void InsertCredentials(string username, string password)
        {
            SqliteConnection myConn = new SqliteConnection("DataSource=database.db;");
            myConn.Open();
            MessageBox.Show("Connection Opened");

            string createTableQuery = @"
            CREATE TABLE IF NOT EXISTS Directories (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                Username TEXT NOT NULL,
                Password TEXT NOT NULL
            );";

            // SQL query to insert username and password into 'Directories' table
            string insertQuery = @"
            INSERT INTO Directories (Username, Password)
            VALUES (@Username, @Password);";

            string sql = "CREATE TABLE highscores (name VARCHAR(20), score INT)";
            SqliteCommand command = new SqliteCommand(sql, myConn);
            command.ExecuteNonQuery();

            myConn.Close();
            MessageBox.Show("Connection Closed");
        }
            /*MessageBox.Show("Calling function InsertCredentials");
            // SQL query to create 'Directories' table if not exists
            string createTableQuery = @"
            CREATE TABLE IF NOT EXISTS Directories (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                Username TEXT NOT NULL,
                Password TEXT NOT NULL
            );";

            // SQL query to insert username and password into 'Directories' table
            string insertQuery = @"
            INSERT INTO Directories (Username, Password)
            VALUES (@Username, @Password);";

            try
            {
                // Open connection to SQLite database
                using (SqliteConnection connection = new SqliteConnection(exePath))
                {
                    connection.Open();
                    MessageBox.Show("Connection Opened");

                    // Create 'Directories' table if it doesn't exist
                    using (SqliteCommand createTableCommand = new SqliteCommand(createTableQuery, connection))
                    {
                        createTableCommand.ExecuteNonQuery();
                    }
                    MessageBox.Show("Table Created");

                    // Insert username and password into 'Directories' table
                    using (SqliteCommand insertCommand = new SqliteCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@Username", username);
                        insertCommand.Parameters.AddWithValue("@Password", password);
                        insertCommand.ExecuteNonQuery();
                    }
                    MessageBox.Show("Values injected");

                    // Close the connection
                    connection.Close();
                    MessageBox.Show("Connection Closed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error 1: {ex.Message}");
            }
        }*/

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
            using (SqliteConnection connection = new SqliteConnection($"Data Source={exePath};"))
            {
                try
                {
                    connection.Open();

                    // Retrieve the names of all tables in the database
                    DataTable tableSchema = connection.GetSchema("Tables");
                    MessageBox.Show($"Tables in {exePath}:");
                    foreach (DataRow row in tableSchema.Rows)
                    {
                        string tableName = (string)row["TABLE_NAME"];
                        MessageBox.Show(tableName);
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error 2: {ex.Message}");
                }
            }
        }
    }
}
