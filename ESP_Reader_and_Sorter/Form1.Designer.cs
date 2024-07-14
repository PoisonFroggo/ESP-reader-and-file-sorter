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
            loginControl1 = new LoginControl();
            SuspendLayout();
            // 
            // loginControl1
            // 
            loginControl1.Location = new System.Drawing.Point(12, 12);
            loginControl1.Name = "loginControl1";
            loginControl1.Size = new System.Drawing.Size(403, 249);
            loginControl1.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(457, 287);
            Controls.Add(loginControl1);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "ESP Reader";
            ResumeLayout(false);
        }

        #endregion

        private LoginControl loginControl1;
    }
}

