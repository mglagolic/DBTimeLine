namespace Centrix1
{
    partial class FormMain
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
            this.pnlConnect = new System.Windows.Forms.Panel();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblServer = new System.Windows.Forms.Label();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pnlConnect.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlConnect
            // 
            this.pnlConnect.Controls.Add(this.btnConnect);
            this.pnlConnect.Controls.Add(this.lblServer);
            this.pnlConnect.Controls.Add(this.lblDatabase);
            this.pnlConnect.Controls.Add(this.Label7);
            this.pnlConnect.Controls.Add(this.label2);
            this.pnlConnect.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlConnect.Location = new System.Drawing.Point(0, 0);
            this.pnlConnect.Name = "pnlConnect";
            this.pnlConnect.Size = new System.Drawing.Size(304, 54);
            this.pnlConnect.TabIndex = 25;
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnConnect.ForeColor = System.Drawing.Color.Black;
            this.btnConnect.Location = new System.Drawing.Point(212, 3);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(78, 45);
            this.btnConnect.TabIndex = 20;
            this.btnConnect.Text = "Connect";
            this.btnConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblServer
            // 
            this.lblServer.BackColor = System.Drawing.Color.Transparent;
            this.lblServer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblServer.ForeColor = System.Drawing.Color.Black;
            this.lblServer.Location = new System.Drawing.Point(80, 5);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(126, 21);
            this.lblServer.TabIndex = 14;
            // 
            // lblDatabase
            // 
            this.lblDatabase.BackColor = System.Drawing.Color.Transparent;
            this.lblDatabase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDatabase.ForeColor = System.Drawing.Color.Black;
            this.lblDatabase.Location = new System.Drawing.Point(80, 26);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(126, 21);
            this.lblDatabase.TabIndex = 21;
            // 
            // Label7
            // 
            this.Label7.ForeColor = System.Drawing.Color.Black;
            this.Label7.Location = new System.Drawing.Point(7, 27);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(67, 19);
            this.Label7.TabIndex = 11;
            this.Label7.Text = "Database";
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(7, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 21);
            this.label2.TabIndex = 10;
            this.label2.Text = "Server";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(59, 60);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(182, 51);
            this.button1.TabIndex = 26;
            this.button1.Text = "Open";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 120);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pnlConnect);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormMain";
            this.pnlConnect.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel pnlConnect;
        internal System.Windows.Forms.Button btnConnect;
        internal System.Windows.Forms.Label lblServer;
        internal System.Windows.Forms.Label lblDatabase;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Button button1;
    }
}