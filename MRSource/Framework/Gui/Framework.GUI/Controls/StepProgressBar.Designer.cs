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
            this.backWorker = new System.ComponentModel.BackgroundWorker();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnAbort = new System.Windows.Forms.Button();
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
            this.backWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backWorker_DoWork);
            this.backWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backWorker_ProgressChanged);
            this.backWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backWorker_RunWorkerCompleted);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnAbort);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 283);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(5);
            this.pnlBottom.Size = new System.Drawing.Size(273, 27);
            this.pnlBottom.TabIndex = 4;
            // 
            // btnAbort
            // 
            this.btnAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbort.Location = new System.Drawing.Point(158, 1);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(107, 23);
            this.btnAbort.TabIndex = 0;
            this.btnAbort.Text = "Abort";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
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
            this.ListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ListView1.Location = new System.Drawing.Point(0, 36);
            this.ListView1.Name = "ListView1";
            this.ListView1.Size = new System.Drawing.Size(273, 247);
            this.ListView1.TabIndex = 5;
            this.ListView1.UseCompatibleStateImageBehavior = false;
            this.ListView1.View = System.Windows.Forms.View.Details;
            // 
            // colStep
            // 
            this.colStep.Text = "Step";
            this.colStep.Width = 202;
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
            this.ProgressBar1.MarqueeAnimationSpeed = 10;
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new System.Drawing.Size(273, 36);
            this.ProgressBar1.Step = 1;
            this.ProgressBar1.TabIndex = 6;
            // 
            // StepProgressBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ListView1);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.ProgressBar1);
            this.Name = "StepProgressBar";
            this.Size = new System.Drawing.Size(273, 310);
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.Panel pnlBottom;
        internal System.Windows.Forms.ColumnHeader colStep;
        internal System.Windows.Forms.ColumnHeader colDuration;
        internal System.Windows.Forms.ProgressBar ProgressBar1;
        internal System.ComponentModel.BackgroundWorker backWorker;
        public System.Windows.Forms.ListView ListView1;
        internal System.Windows.Forms.Button btnAbort;
    }
}
