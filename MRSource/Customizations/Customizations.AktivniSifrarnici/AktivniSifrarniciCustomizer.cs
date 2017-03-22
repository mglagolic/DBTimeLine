using Customizations.Core.Attributes;
using Framework.DBTimeLine;
using Framework.DBTimeLine.DBObjects;
using System;
using System.Collections.Generic;

namespace AktivniSifrarnici
{
    [Customization(CustomizationKey = "AktivniSifrarnici")]
    public class AktivniSifrarniciCustomizer : Customizations.Core.Customizer
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

            Drzava(sch);
            Grad(sch);
            Mjesto(sch);
        }

        private IDBTable Mjesto(IDBSchema sch)
        {
            var rev = new DBRevision(new DateTime(2016, 7, 8), 1, eDBRevisionType.Create);
            var ret = DBMacros.AddDBTableIDNaziv("Mjesto", sch, rev);

            return ret;
        }


        private IDBTable Drzava(IDBSchema sch)
        {
            var ret = sch.AddTable("Drzava", new DBTableDescriptor());

            ret.AddField("Active", DBMacros.DBFieldActiveDescriptor(),
                new DBRevision(new DateTime(2016, 6, 10), 2, eDBRevisionType.Create));

            return ret;
        }

        private IDBTable Grad(IDBSchema sch)
        {
            var ret = sch.AddTable("Grad", new DBTableDescriptor());

            ret.AddField("Active", DBMacros.DBFieldActiveDescriptor(),
                new DBRevision(new DateTime(2016, 6, 10), 2, eDBRevisionType.Create));

            return ret;
        }
    }
}
