using System;
using Framework.DBTimeLine;
using Framework.DBTimeLine.DBObjects;

namespace DBTimeLiners.DBModules
{
    public class dbo : DBModule
    {
        public override string ModuleKey
        {
            get
            {
                return "dbo";
            }
        }

        public override void CreateTimeLine()
        {
            var rev = new DBRevision(new DateTime(2016, 6, 10), 0, eDBRevisionType.Create);

            IDBSchema sch = AddSchema(DefaultSchemaName, new DBSchemaDescriptor());
            if (DefaultSchemaName != "dbo") sch.AddRevision(new DBRevision(rev));

            Drzava(sch);
            Grad(sch);
            Always(sch);
        }

        private IDBTable Drzava(IDBSchema sch)
        {
            var rev = new DBRevision(new DateTime(2016, 6, 10), 0, eDBRevisionType.Create);
            var ret = DBMacros.AddTableIDNaziv("Drzava", sch, rev);

            return ret;
        }
        private IDBTable Grad(IDBSchema sch)
        {
            var rev = new DBRevision(new DateTime(2016, 6, 10), 0, eDBRevisionType.Create);

            var ret = DBMacros.AddDBTableID("Grad", sch, rev);

            ret.AddField("Naziv", new DBFieldDescriptor() { Nullable = false, FieldType = new DBFieldTypeNvarchar(), Size = 512 }, 
                new DBRevision(rev));

            DBMacros.AddForeignKeyFieldID("DrzavaID", true, ret, sch.Name + ".Drzava",
                new DBRevision(new DateTime(2016, 6, 10), 1, eDBRevisionType.Create));

            return ret;
        }
        
        private void Always(IDBSchema sch)
        {
            sch.AddRevision(new DBRevision(new DateTime(2016, 6, 10), 0, eDBRevisionType.AlwaysExecuteTask, UpdateStatistics));
        }

        private string UpdateStatistics(IDBRevision sender, eDBType dBType)
        {
            return 
@"WAITFOR DELAY '00:00:05'
exec sp_updatestats";
        }
    }
}
