using Framework.DBTimeLine.DBObjects;
using Framework.DBTimeLine;

namespace AktivniSifrarnici
{
    public class AktivniSifrarniciDBModule : DBModule
    {
        public override string ModuleKey
        {
            get
            {
                return "AktivniSifrarnici";
            }
        }

        public override void CreateTimeLine()
        {
            IDBSchema sch = AddSchema("dbo", new DBSchemaDescriptor());

            Drzava(sch);
            Grad(sch);
        }
        private IDBTable Drzava(IDBSchema sch)
        {
            var ret = sch.AddTable("Drzava", new DBTableDescriptor() );

            ret.AddField("Active", DBMacros.DBFieldActiveDescriptor(),
                new DBRevision(new System.DateTime(2016, 6, 10), 2, eDBRevisionType.Create));

            return ret;
        }

        private IDBTable Grad(IDBSchema sch)
        {
            var ret = sch.AddTable("Grad", new DBTableDescriptor());

            ret.AddField("Active", DBMacros.DBFieldActiveDescriptor(),
                new DBRevision(new System.DateTime(2016, 6, 10), 2, eDBRevisionType.Create));

            return ret;
        }
    }
}
