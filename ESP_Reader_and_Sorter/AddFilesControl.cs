using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESP_Reader_and_Sorter
{
    public partial class AddFilesControl : UserControl
    {
        private enum Version
        {
            Browse,
            Sort
        }
        public AddFilesControl()
        {
            InitializeComponent();
        }

        // Set a global variable to hold all the selected files result
        List<String> fullFileName;
        private void btn_browseFiles_Click(object sender, EventArgs e)
        {

            folderBrowser_fileSearch.ShowDialog();

            OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
            OpenFileDialog1.Multiselect = true;
            OpenFileDialog1.Title = "Select a File";
            if (OpenFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // put the selected result in the global variable
                fullFileName = new List<String>(OpenFileDialog1.FileNames);

                // add just the names to the listbox
                foreach (string fileName in fullFileName)
                {
                    listBx_files.Items.Add(fileName.Substring(fileName.LastIndexOf(@"\") + 1));
                }

                /*FileStream fs = new FileStream(fullFileName, FileMode.Open);
                int hexIn;
                String hex;

                for (int i = 0; (hexIn = fs.ReadByte()) != -1; i++)
                {
                    hex = string.Format("{0:X2}", hexIn);

                }*/

            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void btn_browseDatabase_Click(object sender, EventArgs e)
        {
            MatchMaking();
        }

        private void MatchMaking()
        {
            string inputData = @"Some text with file names like file1.txt, file2.docx, and another_file.pdf embedded within.";

            // Define a regular expression pattern to match file names
            string pattern = @"[a-zA-Z0-9_]+\.[a-zA-Z0-9]{2,4}";

            // Find all matches using Regex
            MatchCollection matches = Regex.Matches(inputData, pattern);

            // Extract file names from matches and add them to an array
            string[] fileNames = new string[matches.Count];
            for (int i = 0; i < matches.Count; i++)
            {
                fileNames[i] = matches[i].Value;
            }

            // Print the file names found
            MessageBox.Show("File names found in the input:");
            foreach (string fileName in fileNames)
            {
                MessageBox.Show(fileName);
            }
        }
    }
}
