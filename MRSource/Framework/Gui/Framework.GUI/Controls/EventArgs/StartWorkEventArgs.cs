using System.Collections.Generic;

namespace Framework.GUI.Controls.EventArgs
{
    public class WorkEventArgs: System.EventArgs
    {
        public Dictionary<string, object> Inputs { get; set; } 
    }
}
