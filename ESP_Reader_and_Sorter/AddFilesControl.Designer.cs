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
        // <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            btn_browseDatabase = new System.Windows.Forms.Button();
            btn_Toggle = new System.Windows.Forms.Button();
            btn_SubmitDBChanges = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // btn_browseFiles
            // 
            btn_browseFiles.Location = new System.Drawing.Point(3, 3);
            btn_browseFiles.Name = "btn_browseFiles";
            btn_browseFiles.Size = new System.Drawing.Size(152, 72);
            btn_browseFiles.TabIndex = 0;
            btn_browseFiles.Text = "Browse Files";
            btn_browseFiles.UseVisualStyleBackColor = true;
            btn_browseFiles.Click += btn_browseFiles_Click;
            // 
            // folderBrowser_fileSearch
            // 
            folderBrowser_fileSearch.FileName = "openFileDialog1";
            // 
            // btn_browseDatabase
            // 
            btn_browseDatabase.Location = new System.Drawing.Point(3, 3);
            btn_browseDatabase.Name = "btn_browseDatabase";
            btn_browseDatabase.Size = new System.Drawing.Size(152, 72);
            btn_browseDatabase.TabIndex = 2;
            btn_browseDatabase.Text = "Browse Database";
            btn_browseDatabase.UseVisualStyleBackColor = true;
            btn_browseDatabase.Click += btn_browseDatabase_Click;
            // 
            // btn_Toggle
            // 
            btn_Toggle.Location = new System.Drawing.Point(299, 3);
            btn_Toggle.Name = "btn_Toggle";
            btn_Toggle.Size = new System.Drawing.Size(152, 72);
            btn_Toggle.TabIndex = 3;
            btn_Toggle.Text = "Toggle Version";
            btn_Toggle.UseVisualStyleBackColor = true;
            btn_Toggle.Click += btn_Toggle_Click;
            // 
            // btn_SubmitDBChanges
            // 
            btn_SubmitDBChanges.Location = new System.Drawing.Point(161, 3);
            btn_SubmitDBChanges.Name = "btn_SubmitDBChanges";
            btn_SubmitDBChanges.Size = new System.Drawing.Size(132, 72);
            btn_SubmitDBChanges.TabIndex = 4;
            btn_SubmitDBChanges.Text = "Submit additions";
            btn_SubmitDBChanges.UseVisualStyleBackColor = true;
            btn_SubmitDBChanges.Click += btn_SubmitDBChanges_Click;
            // 
            // AddFilesControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(btn_SubmitDBChanges);
            Controls.Add(btn_Toggle);
            Controls.Add(btn_browseDatabase);
            Controls.Add(btn_browseFiles);
            Name = "AddFilesControl";
            Size = new System.Drawing.Size(454, 289);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btn_browseFiles;
        private System.Windows.Forms.OpenFileDialog folderBrowser_fileSearch;
        private System.Windows.Forms.Button btn_browseDatabase;
        private System.Windows.Forms.Button btn_Toggle;
        private System.Windows.Forms.Button btn_SubmitDBChanges;
    }
}
