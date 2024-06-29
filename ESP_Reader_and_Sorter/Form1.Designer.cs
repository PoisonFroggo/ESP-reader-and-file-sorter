namespace ESP_Reader_and_Sorter
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_OpenFile = new System.Windows.Forms.Button();
            this.listBx_files = new System.Windows.Forms.ListBox();
            this.btn_openDatabase = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_OpenFile
            // 
            this.btn_OpenFile.Location = new System.Drawing.Point(74, 101);
            this.btn_OpenFile.Name = "btn_OpenFile";
            this.btn_OpenFile.Size = new System.Drawing.Size(75, 23);
            this.btn_OpenFile.TabIndex = 0;
            this.btn_OpenFile.Text = "button1";
            this.btn_OpenFile.UseVisualStyleBackColor = true;
            this.btn_OpenFile.Click += new System.EventHandler(this.btn_OpenFile_Click);
            // 
            // listBx_files
            // 
            this.listBx_files.FormattingEnabled = true;
            this.listBx_files.Location = new System.Drawing.Point(62, 144);
            this.listBx_files.Name = "listBx_files";
            this.listBx_files.Size = new System.Drawing.Size(676, 251);
            this.listBx_files.TabIndex = 1;
            // 
            // btn_openDatabase
            // 
            this.btn_openDatabase.Location = new System.Drawing.Point(305, 101);
            this.btn_openDatabase.Name = "btn_openDatabase";
            this.btn_openDatabase.Size = new System.Drawing.Size(75, 23);
            this.btn_openDatabase.TabIndex = 2;
            this.btn_openDatabase.Text = "Open Database";
            this.btn_openDatabase.UseVisualStyleBackColor = true;
            this.btn_openDatabase.Click += new System.EventHandler(this.btn_openDatabase_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_openDatabase);
            this.Controls.Add(this.listBx_files);
            this.Controls.Add(this.btn_OpenFile);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_OpenFile;
        private System.Windows.Forms.ListBox listBx_files;
        private System.Windows.Forms.Button btn_openDatabase;
    }
}

