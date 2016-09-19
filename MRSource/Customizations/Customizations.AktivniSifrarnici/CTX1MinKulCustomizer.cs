using Customizations.Core.Attributes;
using Framework.DBTimeLine;
using Framework.DBTimeLine.DBObjects;
using System;
using System.Collections.Generic;

namespace AktivniSifrarnici
{
    [Customization(CustomizationKey = "Ctx1MinKul")]
    public class Ctx1MinKulCustomizer : Customizations.Core.Customizer
    {
        [MethodActivationCustomization(ActivationKey = "CreateTimeLine")]
        public void CreateTimeLine(Dictionary<string, object> inputs)
        {
            DBTimeLiner dbTimeLiner = (DBTimeLiner)inputs["DBTimeLiner"];

            var modules = dbTimeLiner.DBModules.FindAll(m => m.ModuleKey == "dbo");
            foreach (var module in modules)
            {
                CreateTimeLine(module);
            }
        }

        private void CreateTimeLine(IDBModule module)
        {
            IDBSchema sch = module.AddSchema(module.DefaultSchemaName, new DBSchemaDescriptor());


            var view = sch.AddView("v_cus_cus_CustomView", new DBViewDescriptor() { Body = 
@"SELECT 
    Broj = 1
"
, WithSchemaBinding = true },
                    new DBRevision(new DateTime(2016, 9, 14), 0, eDBRevisionType.Create));

            view.AddRevision(new DBRevision(new DateTime(2016, 9, 19), 0, eDBRevisionType.Modify),
                new DBViewDescriptor() { Body = 

@"SELECT 
Broj = 2

", WithSchemaBinding = false });
        }
        
    }
}
