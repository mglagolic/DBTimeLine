namespace Framework.GUI.Controls
{
    partial class DatabaseConnect
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.cbServers = new System.Windows.Forms.ComboBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.cbAuthentication = new System.Windows.Forms.ComboBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTestConnect = new System.Windows.Forms.Label();
            this.btnRefreshDB = new System.Windows.Forms.Button();
            this.btnRefreshServer = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.ForeColor = System.Drawing.Color.Black;
            this.btnConnect.Location = new System.Drawing.Point(239, 129);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(58, 20);
            this.btnConnect.TabIndex = 31;
            this.btnConnect.Text = "Connect";
            this.btnConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.ForeColor = System.Drawing.Color.Black;
            this.txtPassword.Location = new System.Drawing.Point(83, 77);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(154, 20);
            this.txtPassword.TabIndex = 30;
            // 
            // Label1
            // 
            this.Label1.ForeColor = System.Drawing.Color.Black;
            this.Label1.Location = new System.Drawing.Point(2, 2);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(67, 21);
            this.Label1.TabIndex = 21;
            this.Label1.Text = "Server";
            // 
            // Label10
            // 
            this.Label10.ForeColor = System.Drawing.Color.Black;
            this.Label10.Location = new System.Drawing.Point(2, 78);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(71, 19);
            this.Label10.TabIndex = 29;
            this.Label10.Text = "Password";
            // 
            // Label7
            // 
            this.Label7.ForeColor = System.Drawing.Color.Black;
            this.Label7.Location = new System.Drawing.Point(1, 103);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(67, 20);
            this.Label7.TabIndex = 22;
            this.Label7.Text = "Database";
            // 
            // txtUsername
            // 
            this.txtUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUsername.ForeColor = System.Drawing.Color.Black;
            this.txtUsername.Location = new System.Drawing.Point(83, 53);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(154, 20);
            this.txtUsername.TabIndex = 28;
            // 
            // cbServers
            // 
            this.cbServers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbServers.ForeColor = System.Drawing.Color.Black;
            this.cbServers.FormattingEnabled = true;
            this.cbServers.Items.AddRange(new object[] {
            "localhost",
            "adsql2008R2",
            "centrix2dev"});
            this.cbServers.Location = new System.Drawing.Point(83, 2);
            this.cbServers.Name = "cbServers";
            this.cbServers.Size = new System.Drawing.Size(154, 21);
            this.cbServers.TabIndex = 23;
            // 
            // Label9
            // 
            this.Label9.ForeColor = System.Drawing.Color.Black;
            this.Label9.Location = new System.Drawing.Point(2, 54);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(67, 19);
            this.Label9.TabIndex = 27;
            this.Label9.Text = "User name";
            // 
            // txtDatabase
            // 
            this.txtDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDatabase.ForeColor = System.Drawing.Color.Black;
            this.txtDatabase.Location = new System.Drawing.Point(83, 103);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(154, 20);
            this.txtDatabase.TabIndex = 24;
            // 
            // cbAuthentication
            // 
            this.cbAuthentication.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAuthentication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAuthentication.ForeColor = System.Drawing.Color.Black;
            this.cbAuthentication.FormattingEnabled = true;
            this.cbAuthentication.Items.AddRange(new object[] {
            "Windows Authentication",
            "SQL Server Authentication"});
            this.cbAuthentication.Location = new System.Drawing.Point(83, 26);
            this.cbAuthentication.Name = "cbAuthentication";
            this.cbAuthentication.Size = new System.Drawing.Size(154, 21);
            this.cbAuthentication.TabIndex = 26;
            this.cbAuthentication.SelectedIndexChanged += new System.EventHandler(this.cbAuthentication_SelectedIndexChanged);
            // 
            // Label8
            // 
            this.Label8.ForeColor = System.Drawing.Color.Black;
            this.Label8.Location = new System.Drawing.Point(2, 26);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(75, 21);
            this.Label8.TabIndex = 25;
            this.Label8.Text = "Authentication";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTestConnect);
            this.panel1.Controls.Add(this.btnRefreshDB);
            this.panel1.Controls.Add(this.btnRefreshServer);
            this.panel1.Controls.Add(this.Label1);
            this.panel1.Controls.Add(this.btnConnect);
            this.panel1.Controls.Add(this.Label8);
            this.panel1.Controls.Add(this.txtPassword);
            this.panel1.Controls.Add(this.cbAuthentication);
            this.panel1.Controls.Add(this.txtDatabase);
            this.panel1.Controls.Add(this.Label10);
            this.panel1.Controls.Add(this.Label9);
            this.panel1.Controls.Add(this.Label7);
            this.panel1.Controls.Add(this.cbServers);
            this.panel1.Controls.Add(this.txtUsername);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 155);
            this.panel1.TabIndex = 32;
            // 
            // lblTestConnect
            // 
            this.lblTestConnect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTestConnect.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTestConnect.ForeColor = System.Drawing.Color.Black;
            this.lblTestConnect.Location = new System.Drawing.Point(83, 129);
            this.lblTestConnect.Name = "lblTestConnect";
            this.lblTestConnect.Size = new System.Drawing.Size(154, 20);
            this.lblTestConnect.TabIndex = 34;
            // 
            // btnRefreshDB
            // 
            this.btnRefreshDB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshDB.ForeColor = System.Drawing.Color.Black;
            this.btnRefreshDB.Location = new System.Drawing.Point(239, 103);
            this.btnRefreshDB.Name = "btnRefreshDB";
            this.btnRefreshDB.Size = new System.Drawing.Size(58, 20);
            this.btnRefreshDB.TabIndex = 33;
            this.btnRefreshDB.Text = "Refresh";
            this.btnRefreshDB.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefreshDB.UseVisualStyleBackColor = false;
            this.btnRefreshDB.Click += new System.EventHandler(this.btnRefreshDB_Click);
            // 
            // btnRefreshServer
            // 
            this.btnRefreshServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshServer.ForeColor = System.Drawing.Color.Black;
            this.btnRefreshServer.Location = new System.Drawing.Point(239, 2);
            this.btnRefreshServer.Name = "btnRefreshServer";
            this.btnRefreshServer.Size = new System.Drawing.Size(58, 21);
            this.btnRefreshServer.TabIndex = 32;
            this.btnRefreshServer.Text = "Refresh";
            this.btnRefreshServer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefreshServer.UseVisualStyleBackColor = false;
            this.btnRefreshServer.Click += new System.EventHandler(this.btnRefreshServer_Click);
            // 
            // DatabaseConnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "DatabaseConnect";
            this.Size = new System.Drawing.Size(300, 155);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button btnConnect;
        internal System.Windows.Forms.TextBox txtPassword;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.TextBox txtUsername;
        public System.Windows.Forms.ComboBox cbServers;
        internal System.Windows.Forms.Label Label9;
        public  System.Windows.Forms.TextBox txtDatabase;
        internal System.Windows.Forms.ComboBox cbAuthentication;
        internal System.Windows.Forms.Label Label8;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Button btnRefreshDB;
        internal System.Windows.Forms.Button btnRefreshServer;
        internal System.Windows.Forms.Label lblTestConnect;
    }
}
