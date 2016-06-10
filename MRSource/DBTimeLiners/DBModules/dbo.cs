using System;
using System.Collections.Generic;
using Framework.DBTimeLine;
using Framework.DBTimeLine.DBObjects;

namespace DBModules
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
            var rev = new DBRevision(System.DateTime.Now.Date, 0, eDBRevisionType.Create);

            IDBSchema sch = AddSchema(DefaultSchemaName, new DBSchemaDescriptor());
            Drzava(sch);
            Grad(sch);
            Always(sch);
        }

        private IDBTable Drzava(IDBSchema sch)
        {
            var rev = new DBRevision(System.DateTime.Now.Date, 0, eDBRevisionType.Create);
            var ret = DBMacros.AddTableIDNaziv("Drzava", sch, rev);

            return ret;
        }
        private IDBTable Grad(IDBSchema sch)
        {
            var rev = new DBRevision(System.DateTime.Now.Date, 0, eDBRevisionType.Create);
            var rev1 = new DBRevision(System.DateTime.Now.Date, 1, eDBRevisionType.Create);
            var rev2 = new DBRevision(System.DateTime.Now.Date, 2, eDBRevisionType.Create);

            var ret = DBMacros.AddDBTableID("Grad", sch, rev);

            ret.AddField("Naziv", new DBFieldDescriptor() { Nullable = false, FieldType = eDBFieldType.Nvarchar, Size = 512 }, 
                new DBRevision(rev));

            DBMacros.AddForeignKeyFieldID("DrzavaID", true, ret, sch.Name + ".Drzava",
                new DBRevision(rev2));

            return ret;
        }
        
        private void Always(IDBSchema sch)
        {
            sch.AddRevision(new DBRevision(System.DateTime.Now.Date, 0, eDBRevisionType.AlwaysExecuteTask, AlwaysExececuteTask1));
        }

        private string AlwaysExececuteTask1(IDBRevision sender, eDBType dBType)
        {
            return "SELECT AlwaysExececuteTask = 1";
        }
    }
}
