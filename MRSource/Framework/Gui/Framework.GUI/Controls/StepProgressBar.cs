using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Framework.GUI.Controls
{
    public partial class StepProgressBar : UserControl
    {
        public StepProgressBar()
        {
            InitializeComponent();
        }

        public StepProgressBar(string title, WorkEventHandler worker, Dictionary<string, object> inputs): this()
        {
            Worker = worker;
            Inputs = inputs;
        }

        public delegate void WorkEventHandler(object sender, EventArgs.WorkEventArgs e);
        private WorkEventHandler Worker { get; set; }
        private Dictionary<string, object> Inputs { get; set; }

        public void StartWork()
        {
            if (Worker != null)
            {
                Worker.Invoke(this, new EventArgs.WorkEventArgs() { Inputs = Inputs });
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {

        }
    }
}
