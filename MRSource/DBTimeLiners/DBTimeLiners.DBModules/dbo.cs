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

        private string FillDrzava(IDBRevision sender, eDBType dBType)
        {
            var ret = string.Format(

@"GO
WITH
  Pass0 as (select 1 as C union all select 1), --2 rows
  Pass1 as (select 1 as C from Pass0 as A, Pass0 as B),--4 rows
  Pass2 as (select 1 as C from Pass1 as A, Pass1 as B),--16 rows
  Pass3 as (select 1 as C from Pass2 as A, Pass2 as B),--256 rows
  Pass4 as (select 1 as C from Pass3 as A, Pass3 as B),--65536 rows
  Pass5 as (select 1 as C from Pass4 as A, Pass4 as B),
  --I removed Pass5, since I'm only populating the Numbers table to 10,000
  Tally as (select row_number() over(order by C) as Number from Pass5)

INSERT INTO {0} (ID, Naziv)
SELECT 
    ID = NEWID(), 
    Naziv = CAST(Number as nvarchar(512)) 
FROM Tally 
WHERE 
    Number <= 100000

WAITFOR DELAY '00:00:03'
", sender.Parent.SchemaName + "." + sender.Parent.SchemaObjectName);

            return ret;
        }
        
        private IDBTable Drzava(IDBSchema sch)
        {
            var rev = new DBRevision(new DateTime(2016, 6, 10), 0, eDBRevisionType.Create);
            var ret = DBMacros.AddTableIDNaziv("Drzava", sch, rev);

            ret.AddRevision(new DBRevision(new DateTime(2016, 6, 28), 0, eDBRevisionType.Task, FillDrzava, null, 150));

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
            
            //sch.AddView("testAlways", new DBViewDescriptor() { Body = "SELECT Broj = 1", WithSchemaBinding = false },
            //    new DBRevision(new DateTime(2016, 6, 29), 0, eDBRevisionType.AlwaysExecuteTask));
        }

        private string UpdateStatistics(IDBRevision sender, eDBType dBType)
        {
            return 
@"WAITFOR DELAY '00:00:02'
exec sp_updatestats";
        }
    }
}
