using Customizations.Core.Attributes;
using Framework.DBTimeLine;
using Framework.DBTimeLine.DBObjects;
using System;
using System.Collections.Generic;

namespace Customizations.MusicPublisher
{
    [Customization(CustomizationKey = "MusicPublisher")]
    public class MusicPublisherCustomizer : Customizations.Core.Customizer
    {
        [MethodActivationCustomization(ActivationKey = "CreateTimeLine")]
        public void CreateTimeLine(Dictionary<string, object> inputs)
        {
            DBTimeLiner dbTimeLiner = (DBTimeLiner)inputs["DBTimeLiner"];

            var module = new MusicPublisherModule();
            module.Parent = dbTimeLiner;
            dbTimeLiner.DBModules.Add(module);

            module.CreateTimeLine();
        }
    }
}
