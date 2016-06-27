using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Framework.GUI.Controls.GUIEventArgs;

namespace Framework.GUI.Controls
{
    public partial class StepProgressBar : UserControl
    {
        public StepProgressBar()
        {
            InitializeComponent();
        }

        public StepProgressBar(WorkEventHandler worker, WorkEventArgs args): this()
        {
            Worker = worker;
            Args = args;
        }

        public delegate void WorkEventHandler(object sender, WorkEventArgs e);
        public WorkEventHandler Worker { get; set; }
        public WorkEventArgs Args { get; set; }

        System.Threading.ManualResetEvent _busy = null;

        public void StartWork()
        {
            if (backWorker.IsBusy)
            {
                backWorker.CancelAsync();
            }
            else
            {
                backWorker.RunWorkerAsync(Args);
            }
            
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            backWorker.CancelAsync();
            ((Form)Parent).DialogResult = DialogResult.Abort;
        }

        private void backWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _busy = new System.Threading.ManualResetEvent(false);
            if (Worker != null)
            {
                Worker.Invoke(this, Args);
            }
        }

        public event RunWorkerCompletedEventHandler RunWorkerCompleted;
        public void OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ProgressBar1.Value = 100;
            if (RunWorkerCompleted != null)
            {
                RunWorkerCompleted(sender, e);
            }
        }

        private void backWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnRunWorkerCompleted(sender, e);
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
        private void backWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            OnProgressChanged(sender, e);
        }

        public void InitializeSteps(List<StepInfo> steps, List<StepGroupInfo> groups)
        {
            ListView1.Items.Clear();
            ListView1.Groups.Clear();
            
            foreach (var group in groups)
            {
                ListView1.Groups.Add(new ListViewGroup(group.Key, group.Title));
            }

            foreach (var step in steps)
            {
                var item = new ListViewItem()
                {
                    Group = ListView1.Groups[step.Group.Key],
                    Text = step.Title
                };
                ListView1.Items.Add(item);
            }
        }

        private int _CurrentStepIndex = -1;
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


        public void NextStep()
        {
            CurrentStepIndex++;
        }
        public void NextStep(int waitMiliseconds)
        {
            NextStep();
            //_busy.Reset();
            //var ts1 = new TimeSpan();
            System.Threading.Thread.Sleep(waitMiliseconds);
            //var ts2 = new TimeSpan();
        }
    }
}

