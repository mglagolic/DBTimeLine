namespace Framework.GUI.Forms
{
    partial class MRDatabaseConnectForm
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
            this.databaseConnect1 = new Framework.GUI.Controls.MRDatabaseConnect();
            this.SuspendLayout();
            // 
            // databaseConnect1
            // 
            this.databaseConnect1.Dock = System.Windows.Forms.DockStyle.Top;
            this.databaseConnect1.Location = new System.Drawing.Point(10, 10);
            this.databaseConnect1.Name = "databaseConnect1";
            this.databaseConnect1.Size = new System.Drawing.Size(330, 155);
            this.databaseConnect1.TabIndex = 1;
            this.databaseConnect1.DatabaseConnected += new Framework.GUI.Controls.MRDatabaseConnect.DatabaseConnectedEventHandler(this.databaseConnect1_DatabaseConnected);
            // 
            // DatabaseConnectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(350, 202);
            this.Controls.Add(this.databaseConnect1);
            this.Name = "DatabaseConnectForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "Connect to database";
            this.Controls.SetChildIndex(this.databaseConnect1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.MRDatabaseConnect databaseConnect1;
    }
}
