using Framework.GUI.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.GUI.Controls
{
    public partial class MRRichTextBox : System.Windows.Forms.RichTextBox
    {
        public MRRichTextBox()
        {
            InitializeComponent();
        }

        public MRRichTextBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public void WriteText(string text)
        {
            var pars = new ArrayList();
            pars.Add(text);
            CrossThreadingHelpers.InvokeControl(this, pars, (x) => 
                            {
                                ArrayList input = (ArrayList)x;
                                string str = (string)input[0];
                                AppendText(str);
                                ScrollToCaret();
                            });
                                                  
        }
    }
}
