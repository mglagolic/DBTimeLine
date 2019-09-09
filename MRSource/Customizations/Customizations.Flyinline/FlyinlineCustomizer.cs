using Customizations.Core.Attributes;
using Framework.DBTimeLine;
using Framework.DBTimeLine.DBObjects;
using System;
using System.Collections.Generic;

namespace Customizations.Identity
{
    [Customization(CustomizationKey = "FlyinlineERM")]
    public class FlyinlineCustomizer : Customizations.Core.Customizer
    {
        [MethodActivationCustomization(ActivationKey = "CreateTimeLine")]
        public void CreateTimeLine(Dictionary<string, object> inputs)
        {
            DBTimeLiner dbTimeLiner = (DBTimeLiner)inputs["DBTimeLiner"];

            var moduleIdentity = new IdentityModule();
            moduleIdentity.Parent = dbTimeLiner;
            dbTimeLiner.DBModules.Add(moduleIdentity);

            moduleIdentity.CreateTimeLine();          
        }
    }
}
