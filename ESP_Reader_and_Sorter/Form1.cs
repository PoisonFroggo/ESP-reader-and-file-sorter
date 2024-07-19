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
using System.Drawing.Text;
using System.Text.RegularExpressions;


namespace ESP_Reader_and_Sorter
{
    public partial class Form1 : Form
    {
        public string username { get; set; } //Extremely unsafe change later if time allows
        private SqliteConnection _connection;
        private SqliteCompiler Compiler;


        // Set a global variable to hold all the selected files result
        List<String> fullFileName;

        private LoginControl loginControl;
        private AddFilesControl addFilesControl;
        public Form1()
        {
            InitializeComponent();
            loginControl = new LoginControl();
            loginControl.Dock = DockStyle.Fill;
            this.Controls.Add(loginControl);

            // Initialize AddFilesControl
            addFilesControl = new AddFilesControl();
            addFilesControl.Dock = DockStyle.Fill;
            addFilesControl.Visible = false; // Start with AddFilesControl hidden
            this.Controls.Add(addFilesControl);
            LoginControl_LoginSuccessChanged();
        }


        public void LoginControl_LoginSuccessChanged()
        {
            loginControl.LoginPerformed += (s, args) =>
            {
                username = loginControl.Username;
                loginControl.Visible = false;
                addFilesControl.Visible = true;
                MessageBox.Show($"Welcome {username}!");
            };
        }


    }
}