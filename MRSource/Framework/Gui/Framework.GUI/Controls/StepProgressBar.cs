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
        protected virtual void OnAborted(object sender, EventArgs e)
        {
            CrossThreadingHelpers.InvokeControl((Control) sender, null, (x) =>
            {
                if (backWorker.IsBusy && !backWorker.CancellationPending)
                {
                    if (CanAbort)
                    {
                        backWorker.CancelAsync();
                        ProgressBar1.Enabled = false;
                        Aborted?.Invoke(sender, e);
                    }
                }
            });
           
        }

        public event RunWorkerCompletedEventHandler RunWorkerCompleted;
        protected virtual void OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ProgressBar1.Style = ProgressBarStyle.Blocks;
            ProgressBar1.Value = 100;
            RunWorkerCompleted?.Invoke(sender, e);
        }

        public event DoWorkEventHandler DoWork;
        protected virtual void OnDoWork(object sender, DoWorkEventArgs e)
        {
            DoWork?.Invoke(sender, e);
        }

        public event ProgressChangedEventHandler ProgressChanged;
        protected virtual void OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CrossThreadingHelpers.InvokeControl(ProgressBar1, e.ProgressPercentage, (x) =>
            {
                ProgressBar1.Value = (int) x;
                ProgressChanged?.Invoke(sender, e);
            });
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
        //[DesignerSerializationVisibility (DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [DefaultValue(-1)]
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

        #region Public methods and properties
        public void Abort()
        {
            OnAborted(null, EventArgs.Empty);
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

        [Browsable(false)]
        public ListView Grid
        {
            get
            {
                return ListView1;
            }
        }
        [Browsable(false)]
        public ProgressBar Progress
        {
            get
            {
                return ProgressBar1;
            }
        }

        private bool _CanAbort = true;
        [DefaultValue(true)]
        public bool CanAbort
        {
            get
            {
                return _CanAbort;
            }
            set
            {
                if (value != _CanAbort)
                {
                    _CanAbort = value;

                    CrossThreadingHelpers.InvokeControl(btnAbort, _CanAbort, (x) =>
                    {
                        btnAbort.Visible = (bool) x;
                    });
                }                
            }
        }

        #endregion
    }
}

