using Framework.Persisting;
using MRFramework.MRPersisting.Factory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Centrix1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            customizationLoader.LoadCustomizers();
        }

        Customizations.Core.Loader customizationLoader = new Customizations.Core.Loader();

        private void Form1_Load(object sender, EventArgs e)
        {
            var callMethodsInputs = new Dictionary<string, object>();
            callMethodsInputs.Add("Form", this);
            customizationLoader.CallMethods("FormLoaded", callMethodsInputs);
        }

        
    }
}
