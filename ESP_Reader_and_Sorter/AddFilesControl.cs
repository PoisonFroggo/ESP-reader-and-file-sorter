using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESP_Reader_and_Sorter
{
    public partial class AddFilesControl : UserControl
    {
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
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
