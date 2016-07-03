using Framework.GUI.Helpers;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Framework.GUI.Controls
{
    public partial class StepProgressBar : UserControl
    {
        public StepProgressBar()
        {
            InitializeComponent();
        }
     
        public void StartWork(object inputs)
        {
            if (backWorker.IsBusy)
            {
                backWorker.CancelAsync();
            }
            else
            {
                ProgressBar1.Enabled = true;
                backWorker.RunWorkerAsync(inputs);
            }
        }
        
        #region Events and Event raisers

        public delegate void ButtonClickEventHandler(object sender, EventArgs e);
        public event ButtonClickEventHandler Aborted;
        public void OnAborted(object sender, EventArgs e)
        {
            if (backWorker.IsBusy && !backWorker.CancellationPending) 
            {
                backWorker.CancelAsync();
                ProgressBar1.Enabled = false;
                Aborted?.Invoke(sender, e);
            }
        }

        public event RunWorkerCompletedEventHandler RunWorkerCompleted;
        public void OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ProgressBar1.Style = ProgressBarStyle.Blocks;
            ProgressBar1.Value = 100;
            if (RunWorkerCompleted != null)
            {
                RunWorkerCompleted(sender, e);
            }
        }

        public event DoWorkEventHandler DoWork;
        public void OnDoWork(object sender, DoWorkEventArgs e)
        {
            if (DoWork != null)
            {
                DoWork(sender, e);
            }
        }

        public event ProgressChangedEventHandler ProgressChanged;
        public void OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressBar1.Value = e.ProgressPercentage;
            if (ProgressChanged != null)
            {
                ProgressChanged(sender, e);
            }
        }

        #endregion

        #region Event pipes
        private void btnAbort_Click(object sender, EventArgs e)
        {
            OnAborted(sender, e);
        }

        private void backWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnRunWorkerCompleted(sender, e);
        }


        private void backWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            OnProgressChanged(sender, e);
        }

        private void backWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            OnDoWork(sender, e);
        }

        #endregion

        private int _CurrentStepIndex = -1;
        //[System.ComponentModel.DesignerSerializationVisibility (DesignerSerializationVisibility.Hidden)]
        public int CurrentStepIndex
        {
            get
            {
                return _CurrentStepIndex;
            }
            set
            {
                _CurrentStepIndex = value;
                if (value > 0)
                {
                    DeselectStep(ListView1, value - 1);
                }
                if (value > -1)
                {
                    SelectStep(ListView1, value);
                }
            }
        }

        public delegate void SelectStepCallback(ListView lv, int index);
        private void SelectStep(ListView lv, int index)
        {
            if (lv.InvokeRequired)
            {
                SelectStepCallback d = new SelectStepCallback(SelectStep);
                lv.Invoke(d, lv, index);
                return;
            }
            lv.Items[index].Font = new Font(lv.Items[index].Font, FontStyle.Bold);
        }

        private void DeselectStep(ListView lv, int index)
        {
            if (lv.InvokeRequired)
            {
                SelectStepCallback d = new SelectStepCallback(DeselectStep);
                lv.Invoke(d, lv, index);
                return;
            }
            lv.Items[index].Font = new Font(lv.Items[index].Font, FontStyle.Regular);
        }


        public void NextStep(bool marquee)
        {
            if (ListView1.Items.Count > CurrentStepIndex + 1)
            {
                CurrentStepIndex++;
                if (marquee)
                {
                    CrossThreadingHelpers.InvokeControl(ProgressBar1, null, (x) => 
                        {
                            ProgressBar1.Style = ProgressBarStyle.Marquee;
                        });
                }
                else
                {
                    ReportProgress(0);
                    CrossThreadingHelpers.InvokeControl(ProgressBar1, null, (x) => { ProgressBar1.Style = ProgressBarStyle.Blocks; });
                }
            }

        }
        public void NextStep(bool continuous, int waitMiliseconds)
        {
            NextStep(continuous);
            System.Threading.Thread.Sleep(waitMiliseconds);
        }
        public void ReportProgress(int percentProgress)
        {
            backWorker.ReportProgress(percentProgress);
        }

        public ListView Grid
        {
            get
            {
                return ListView1;
            }
        }
        public ProgressBar Progress
        {
            get
            {
                return ProgressBar1;
            }
        }
                
    }
}

