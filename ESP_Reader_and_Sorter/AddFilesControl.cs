﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace ESP_Reader_and_Sorter
{
    public partial class AddFilesControl : UserControl
    {
        private enum Version
        {
            Browse,
            Sort
        }

        //Listbox declarations for the different versions
        private ListBox listbx_Browse;
        private ListBox listbx_Browse2;
        private ListBox listbx_Sort;
        private ListBox listbx_Sort2;
        private Label labl1; //label 1 will be for ESP storing, the selected ESP file's dependencies (related files) will be stored in label 2
        private Label labl2;


        private List<string> espPaths = new List<string>(); // List to store file paths, espPath
        private List<string> espNames = new List<string>(); // List to store file names, espName

        private List<string> fileNames = new List<string>(); //List to store file names, fileName
        
        
        private string espGame; //esp table value gameName
        private string espName; //esp table value fileName
        private string espPath; //esp table value espPath
        private string espID; //esp table value espID
        public string Username { get; set; }//Accounts table value username, esp table value espID
        //espID value is the foreign key value of Account's username

        /***********************Definitions for Files table values*********************************/
        private string fileName; //files table value fileName
        private string prereq; //prereq value is the esp table fileName(espName)

        public AddFilesControl()
        {
            InitializeComponent();
            this.listbx_Browse = new System.Windows.Forms.ListBox();
            this.listbx_Browse2 = new System.Windows.Forms.ListBox();
            this.listbx_Sort = new System.Windows.Forms.ListBox();
            this.listbx_Sort2 = new System.Windows.Forms.ListBox();
            this.labl1 = new System.Windows.Forms.Label();
            this.labl2 = new System.Windows.Forms.Label();
            this.SuspendLayout();

            //setup for Browsing list boxes
            this.Controls.Add(this.listbx_Browse);
            listbx_Browse.Location = new System.Drawing.Point(3, 105);
            listbx_Browse.Size = new System.Drawing.Size(150, 180);
            this.Controls.Add(this.listbx_Browse2);
            listbx_Browse2.Location = new System.Drawing.Point(150, 105);
            listbx_Browse2.Size = new System.Drawing.Size(150, 180);

            //setup for Sorting list boxes
            this.Controls.Add(this.listbx_Sort);
            listbx_Sort.Location = new System.Drawing.Point(3, 105);
            listbx_Sort.Size = new System.Drawing.Size(150, 180);
            this.Controls.Add(this.listbx_Sort2);
            listbx_Sort2.Location = new System.Drawing.Point(150, 105);
            listbx_Sort2.Size = new System.Drawing.Size(150, 180);

            //setup for labels
            this.Controls.Add(this.labl1);
            this.Controls.Add(this.labl2);
            labl1.Location = new System.Drawing.Point(30, 84);
            labl2.Location = new System.Drawing.Point(170, 84);
            labl1.AutoSize = true;
            labl2.AutoSize = true;
            labl1.Text = "ESP file";
            labl2.Text = "Related files";


            SetVersion(Version.Browse.ToString());
            this.ResumeLayout(false);

        }






        private string inputData = "";

        // Set a global variable to hold all the selected files result
        List<String> fullFileName;
        private void btn_browseFiles_Click(object sender, EventArgs e)
        {
            BrowseFiles();
        }

        //function to put everything into the database

        private void BrowseFiles() //Opens new file dialog and gets all files selected in the dialog, adding them to the filePaths list and the names of them to the fileNameOnly list
        {
            OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
            OpenFileDialog1.Multiselect = true;
            OpenFileDialog1.Title = "Seclect a  Fil";
            OpenFileDialog1.Filter = "ESP Files (*.esp)|*.esp|All files (*.*)|*.*";
            OpenFileDialog1.FilterIndex = 1; //default to esp files (*.esp)
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (string fileName in OpenFileDialog1.FileNames)
                {
                    espPaths.Add(fileName);

                    string fileNameOnly = Path.GetFileName(fileName);
                    espNames.Add(fileNameOnly);
                    listbx_Browse.Items.Add(ReadESP(fileName));
                    MatchMaking();
                }
                AddToDB();
            }
        }


        private void btn_browseDatabase_Click(object sender, EventArgs e)
        {
            MatchMaking();
        }

        private void MatchMaking() //This takes the global variable which contains the contents of any one esp file and then retrieves all file names from it
        {

            // Define a regular expression pattern to match file names
            string pattern = @"[a-zA-Z0-9_]+\.[a-zA-Z0-9]{2,4}";

            // Find all matches using Regex
            MatchCollection matches = Regex.Matches(inputData, pattern);

            // Extract file names from matches and add them to an array
            string[] fileNames = new string[matches.Count];
            for (int i = 0; i < matches.Count; i++)
            {
                fileNames[i] = matches[i].Value;
                listbx_Browse2.Items.Add(matches[i].Value);
            }
        }

        public void SetVersion(string action)
        {
            string currentAction = action;
            switch (action)
            {
                case "Browse":
                    listbx_Browse.Visible = true;
                    listbx_Browse2.Visible = true;
                    listbx_Sort.Visible = false;
                    listbx_Sort2.Visible = false;
                    btn_browseDatabase.Visible = false;
                    btn_browseFiles.Visible = true;
                    btn_SubmitDBChanges.Visible = true;
                    labl1.Visible = false;
                    labl2.Visible = false;
                    MessageBox.Show("Browse");
                    break;
                case "Sort":
                    listbx_Browse.Visible = false;
                    listbx_Browse2.Visible = false;
                    listbx_Sort.Visible = true;
                    listbx_Sort2.Visible = true;
                    btn_browseDatabase.Visible = true;
                    btn_browseFiles.Visible = false;
                    btn_SubmitDBChanges.Visible = false;
                    labl1.Visible = true;
                    labl2.Visible = true;
                    MessageBox.Show("Sort");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }
        }

        private void btn_Toggle_Click(object sender, EventArgs e)
        {
            if (listbx_Browse.Visible == true)
            {
                SetVersion("Sort");
            }
            else if (listbx_Sort.Visible == true)
            {
                SetVersion("Browse");
            }
        }



        private void AddToDB()
        {
        }

        public string ReadESP(string filePath) //Reads the ESP file in question, returning its contents as a single long string
        {
            try
            {
                // Check if the file exists
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("File not found.", filePath);
                }

                // Specify the code page for ANSI encoding (Windows-1252)
                int codePage = 1252; // Windows-1252 code page

                // Create Encoding using CodePagesEncodingProvider
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding encoding = Encoding.GetEncoding(codePage);

                // Read all text from the file using StreamReader with specified encoding
                using (StreamReader reader = new StreamReader(filePath, encoding, true))
                {
                    // Read the entire file content
                    StringBuilder sb = new StringBuilder();
                    int charValue;
                    while ((charValue = reader.Read()) != -1)
                    {
                        // Append readable characters to StringBuilder
                        if (char.IsControl((char)charValue) && !char.IsWhiteSpace((char)charValue))
                        {
                            sb.Append(' ');
                        }
                        else
                        {
                            sb.Append((char)charValue);
                        }
                    }

                    // Return the file content as a string and define global variable as file content
                    inputData = sb.ToString();
                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions or rethrow as needed
                MessageBox.Show("Error reading file: " + ex.Message);
                return null; // Return null on error
            }
        }

        public List<string> RetrieveFilesFromString(string inputText) //Retrieves a list of files referenced by the ESP
        {
            List<string> fileNames = new List<string>();

            // Define a regular expression pattern to match file names
            string pattern = @"[\w\-. ]+\.[a-zA-Z0-9]{2,4}";

            // Find all matches using Regex
            MatchCollection matches = Regex.Matches(inputText, pattern);

            // Extract file names from matches and add them to the list
            foreach (Match match in matches)
            {
                string fileName = match.Value.Trim();
                fileNames.Add(fileName);
            }

            return fileNames;
        }

        public int FileAmt(List<string> files)
        {

            return files.Count;
        }




        //Submit changes to the database
        private void btn_SubmitDBChanges_Click(object sender, EventArgs e)
        {
            SqliteConnection myConn = new SqliteConnection("DataSource=database.db;");
            myConn.Open();
            //first run a loop to go through filePaths
            for (int i = 0; i<espPaths.Count(); i++)
            {
                Username = Form1.GetUsername(); //gets username for account validation
                espPath = espPaths[i];//espPath
                espName = espNames[i];//espName
                espID = Username; //espID
                                  //add these to the table esp

                //run a loop for every esp that adds to files table

                string insertEspQuery = "INSERT INTO esp (espID, espPath, fileName) VALUES (@Username, @espPath, @espName)";
                SqliteCommand insertEspCommand = new SqliteCommand(insertEspQuery, myConn);
                insertEspCommand.Parameters.AddWithValue("@Username", espID);
                insertEspCommand.Parameters.AddWithValue("@espPath", espPath);
                insertEspCommand.Parameters.AddWithValue("@espName", espName);
                insertEspCommand.ExecuteNonQuery();
                for (int x = 0; x<fileNames.Count; x++)
                {
                    fileName = fileNames[x];
                    prereq = espName;
                    //Add to the files table

                    string insertFilesQuery = "INSERT INTO files (fileName, prereq) VALUES (@fileName, @prereq)";
                    SqliteCommand insertFilesCommand = new SqliteCommand(insertFilesQuery, myConn);
                    insertFilesCommand.Parameters.AddWithValue("@fileName", fileName);
                    insertFilesCommand.Parameters.AddWithValue("@prereq", prereq);
                    insertFilesCommand.ExecuteNonQuery();
                }

                myConn.Close();

                GetData();
            }
        }

        private void GetGameName()
        {

        }

        private void GetData()
        {
            string connectionString = "Data Source=database.db;Version=3;"; // Connection string for SQLite
            SQLiteConnection connection = new SQLiteConnection(connectionString);

            try
            {
                connection.Open();

                // Retrieve data from 'esp' table
                string selectEspQuery = "SELECT espID, espPath, fileName FROM esp";
                SQLiteCommand selectEspCommand = new SQLiteCommand(selectEspQuery, connection);
                SQLiteDataAdapter espDataAdapter = new SQLiteDataAdapter(selectEspCommand);
                DataTable espDataTable = new DataTable();
                espDataAdapter.Fill(espDataTable);

                // Display data from 'esp' table in a message box
                string espMessage = "Contents of 'esp' table:\n\n";
                foreach (DataRow row in espDataTable.Rows)
                {
                    espMessage += $"espID: {row["espID"]}, espPath: {row["espPath"]}, fileName: {row["fileName"]}\n";
                }
                MessageBox.Show(espMessage, "Data from 'esp' table", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Retrieve data from 'files' table
                string selectFilesQuery = "SELECT fileName, prereq FROM files";
                SQLiteCommand selectFilesCommand = new SQLiteCommand(selectFilesQuery, connection);
                SQLiteDataAdapter filesDataAdapter = new SQLiteDataAdapter(selectFilesCommand);
                DataTable filesDataTable = new DataTable();
                filesDataAdapter.Fill(filesDataTable);

                // Display data from 'files' table in a message box
                string filesMessage = "Contents of 'files' table:\n\n";
                foreach (DataRow row in filesDataTable.Rows)
                {
                    filesMessage += $"fileName: {row["fileName"]}, prereq: {row["prereq"]}\n";
                }
                MessageBox.Show(filesMessage, "Data from 'files' table", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
