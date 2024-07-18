namespace ESP_Reader_and_Sorter
{
    partial class AddFilesControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn_browseFiles = new System.Windows.Forms.Button();
            folderBrowser_fileSearch = new System.Windows.Forms.OpenFileDialog();
            listBx_files = new System.Windows.Forms.ListBox();
            btn_browseDatabase = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // btn_browseFiles
            // 
            btn_browseFiles.Location = new System.Drawing.Point(3, 3);
            btn_browseFiles.Name = "btn_browseFiles";
            btn_browseFiles.Size = new System.Drawing.Size(178, 72);
            btn_browseFiles.TabIndex = 0;
            btn_browseFiles.Text = "Browse Files";
            btn_browseFiles.UseVisualStyleBackColor = true;
            btn_browseFiles.Click += btn_browseFiles_Click;
            // 
            // folderBrowser_fileSearch
            // 
            folderBrowser_fileSearch.FileName = "openFileDialog1";
            folderBrowser_fileSearch.FileOk += openFileDialog1_FileOk;
            // 
            // listBx_files
            // 
            listBx_files.FormattingEnabled = true;
            listBx_files.ItemHeight = 15;
            listBx_files.Location = new System.Drawing.Point(3, 81);
            listBx_files.Name = "listBx_files";
            listBx_files.Size = new System.Drawing.Size(368, 139);
            listBx_files.TabIndex = 1;
            // 
            // btn_browseDatabase
            // 
            btn_browseDatabase.Location = new System.Drawing.Point(187, 3);
            btn_browseDatabase.Name = "btn_browseDatabase";
            btn_browseDatabase.Size = new System.Drawing.Size(184, 72);
            btn_browseDatabase.TabIndex = 2;
            btn_browseDatabase.Text = "Browse Database";
            btn_browseDatabase.UseVisualStyleBackColor = true;
            btn_browseDatabase.Click += btn_browseDatabase_Click;
            // 
            // AddFilesControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(btn_browseDatabase);
            Controls.Add(listBx_files);
            Controls.Add(btn_browseFiles);
            Name = "AddFilesControl";
            Size = new System.Drawing.Size(374, 243);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btn_browseFiles;
        private System.Windows.Forms.OpenFileDialog folderBrowser_fileSearch;
        private System.Windows.Forms.ListBox listBx_files;
        private System.Windows.Forms.Button btn_browseDatabase;
    }
}
