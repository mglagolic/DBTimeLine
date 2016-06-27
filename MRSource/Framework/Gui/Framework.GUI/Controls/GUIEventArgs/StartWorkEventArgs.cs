using System.Collections.Generic;
using System.Windows.Forms;

namespace Framework.GUI.Controls.GUIEventArgs
{
    public class WorkEventArgs: System.EventArgs
    {
        public StepProgressBar Parent { get; set; }
        public Dictionary<string, object> Inputs { get; set; } 
        public List<StepInfo> Steps { get; set; }
        public List<StepGroupInfo> StepGroups { get; set; }

    }
}
