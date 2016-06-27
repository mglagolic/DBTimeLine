namespace Framework.GUI.Controls
{
    partial class StepProgressBar
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Initializing", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Creating timeline", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Applying timeline", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Finishing tasks", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Checking schema DBTimeLine");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Checking schema Common");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Loading modules");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Loading customizations");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Applying changes");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("Running always execute tasks");
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("Work completed");
            this.backWorker = new System.ComponentModel.BackgroundWorker();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ListView1 = new System.Windows.Forms.ListView();
            this.colStep = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDuration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ProgressBar1 = new System.Windows.Forms.ProgressBar();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // backWorker
            // 
            this.backWorker.WorkerReportsProgress = true;
            this.backWorker.WorkerSupportsCancellation = true;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 382);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(5);
            this.pnlBottom.Size = new System.Drawing.Size(385, 29);
            this.pnlBottom.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(156, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ListView1
            // 
            this.ListView1.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.ListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colStep,
            this.colDuration});
            this.ListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListView1.FullRowSelect = true;
            this.ListView1.GridLines = true;
            listViewGroup1.Header = "Initializing";
            listViewGroup1.Name = "grpInitializing";
            listViewGroup2.Header = "Creating timeline";
            listViewGroup2.Name = "grpCreatingTimeline";
            listViewGroup3.Header = "Applying timeline";
            listViewGroup3.Name = "grpApplyingTimeline";
            listViewGroup4.Header = "Finishing tasks";
            listViewGroup4.Name = "grpFinishing";
            this.ListView1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3,
            listViewGroup4});
            this.ListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            listViewItem1.Group = listViewGroup1;
            listViewItem2.Group = listViewGroup1;
            listViewItem3.Group = listViewGroup2;
            listViewItem4.Group = listViewGroup2;
            listViewItem5.Group = listViewGroup3;
            listViewItem6.Group = listViewGroup3;
            listViewItem7.Group = listViewGroup4;
            this.ListView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7});
            this.ListView1.Location = new System.Drawing.Point(0, 36);
            this.ListView1.Name = "ListView1";
            this.ListView1.Size = new System.Drawing.Size(385, 346);
            this.ListView1.TabIndex = 5;
            this.ListView1.UseCompatibleStateImageBehavior = false;
            this.ListView1.View = System.Windows.Forms.View.Details;
            // 
            // colStep
            // 
            this.colStep.Text = "Step";
            this.colStep.Width = 317;
            // 
            // colDuration
            // 
            this.colDuration.Text = "Duration";
            // 
            // ProgressBar1
            // 
            this.ProgressBar1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ProgressBar1.ForeColor = System.Drawing.Color.SteelBlue;
            this.ProgressBar1.Location = new System.Drawing.Point(0, 0);
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new System.Drawing.Size(385, 36);
            this.ProgressBar1.Step = 1;
            this.ProgressBar1.TabIndex = 6;
            // 
            // StepProgressBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ListView1);
            this.Controls.Add(this.ProgressBar1);
            this.Controls.Add(this.pnlBottom);
            this.Name = "StepProgressBar";
            this.Size = new System.Drawing.Size(385, 411);
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.ComponentModel.BackgroundWorker backWorker;
        internal System.Windows.Forms.Panel pnlBottom;
        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.ColumnHeader colStep;
        internal System.Windows.Forms.ColumnHeader colDuration;
        internal System.Windows.Forms.ProgressBar ProgressBar1;
        private System.Windows.Forms.ListView ListView1;
    }
}
