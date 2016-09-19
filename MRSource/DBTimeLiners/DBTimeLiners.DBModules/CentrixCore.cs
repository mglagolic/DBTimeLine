using System;
using Framework.DBTimeLine;
using Framework.DBTimeLine.DBObjects;

namespace DBTimeLiners.DBModules
{
    public class CentrixCore : DBModule
    {
        public override string ModuleKey
        {
            get
            {
                return "CentrixCore";
            }
        }

        public override void CreateTimeLine()
        {
            var rev = new DBRevision(new DateTime(2016, 6, 10), 0, eDBRevisionType.Create);

            IDBSchema sch = AddSchema(DefaultSchemaName, new DBSchemaDescriptor());
            if (DefaultSchemaName != "dbo") sch.AddRevision(new DBRevision(rev));

            tblCases(sch);
        }

                
        private IDBTable tblCases(IDBSchema sch)
        {
            var rev = new DBRevision(new DateTime(2016, 9, 19), 0, eDBRevisionType.Create);
            var ret = DBMacros.AddTableIDNaziv("tblCases", sch, rev);
                    
            return ret;
        }

    }
}
