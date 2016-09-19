using System;
using Framework.DBTimeLine;
using Framework.DBTimeLine.DBObjects;

namespace DBTimeLiners.DBModules
{
    public class ImperiosCore : DBModule
    {
        public override string ModuleKey
        {
            get
            {
                return "ImperiosCore";
            }
        }

        public override void CreateTimeLine()
        {
            var rev = new DBRevision(new DateTime(2016, 6, 10), 0, eDBRevisionType.Create);

            IDBSchema sch = AddSchema(DefaultSchemaName, new DBSchemaDescriptor());
            if (DefaultSchemaName != "dbo") sch.AddRevision(new DBRevision(rev));

            MjestoTroska(sch);
        }

                
        private IDBTable MjestoTroska(IDBSchema sch)
        {
            var rev = new DBRevision(new DateTime(2016, 9, 19), 0, eDBRevisionType.Create);
            var ret = DBMacros.AddTableIDNaziv("MjestoTroska", sch, rev);

            //ret.AddRevision(new DBRevision(new DateTime(2016, 6, 28), 0, eDBRevisionType.Task, null, null));

            return ret;
        }

    }
}
