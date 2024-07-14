namespace ESP_Reader_and_Sorter
{
    partial class LoginControl
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
            btn_RegisterLogin = new System.Windows.Forms.Button();
            txtbx_Username = new System.Windows.Forms.TextBox();
            txtbx_Password = new System.Windows.Forms.TextBox();
            labl_Username = new System.Windows.Forms.Label();
            labl_Password = new System.Windows.Forms.Label();
            btn_SubmitLogin = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // btn_RegisterLogin
            // 
            btn_RegisterLogin.Location = new System.Drawing.Point(3, 186);
            btn_RegisterLogin.Name = "btn_RegisterLogin";
            btn_RegisterLogin.Size = new System.Drawing.Size(397, 23);
            btn_RegisterLogin.TabIndex = 0;
            btn_RegisterLogin.Text = "Register Account";
            btn_RegisterLogin.UseVisualStyleBackColor = true;
            btn_RegisterLogin.Click += btn_RegisterLogin_Click;
            // 
            // txtbx_Username
            // 
            txtbx_Username.Location = new System.Drawing.Point(200, 96);
            txtbx_Username.Name = "txtbx_Username";
            txtbx_Username.Size = new System.Drawing.Size(100, 23);
            txtbx_Username.TabIndex = 1;
            // 
            // txtbx_Password
            // 
            txtbx_Password.Location = new System.Drawing.Point(200, 125);
            txtbx_Password.Name = "txtbx_Password";
            txtbx_Password.Size = new System.Drawing.Size(100, 23);
            txtbx_Password.TabIndex = 2;
            // 
            // labl_Username
            // 
            labl_Username.AutoSize = true;
            labl_Username.Location = new System.Drawing.Point(114, 99);
            labl_Username.Name = "labl_Username";
            labl_Username.Size = new System.Drawing.Size(63, 15);
            labl_Username.TabIndex = 3;
            labl_Username.Text = "Username:";
            // 
            // labl_Password
            // 
            labl_Password.AutoSize = true;
            labl_Password.Location = new System.Drawing.Point(114, 128);
            labl_Password.Name = "labl_Password";
            labl_Password.Size = new System.Drawing.Size(60, 15);
            labl_Password.TabIndex = 4;
            labl_Password.Text = "Password:";
            // 
            // btn_SubmitLogin
            // 
            btn_SubmitLogin.Location = new System.Drawing.Point(3, 215);
            btn_SubmitLogin.Name = "btn_SubmitLogin";
            btn_SubmitLogin.Size = new System.Drawing.Size(397, 23);
            btn_SubmitLogin.TabIndex = 5;
            btn_SubmitLogin.Text = "Login on Existing Account";
            btn_SubmitLogin.UseVisualStyleBackColor = true;
            btn_SubmitLogin.Click += btn_SubmitLogin_Click;
            // 
            // LoginControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(btn_SubmitLogin);
            Controls.Add(labl_Password);
            Controls.Add(labl_Username);
            Controls.Add(txtbx_Password);
            Controls.Add(txtbx_Username);
            Controls.Add(btn_RegisterLogin);
            Name = "LoginControl";
            Size = new System.Drawing.Size(403, 249);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btn_RegisterLogin;
        private System.Windows.Forms.TextBox txtbx_Username;
        private System.Windows.Forms.TextBox txtbx_Password;
        private System.Windows.Forms.Label labl_Username;
        private System.Windows.Forms.Label labl_Password;
        private System.Windows.Forms.Button btn_SubmitLogin;
    }
}
