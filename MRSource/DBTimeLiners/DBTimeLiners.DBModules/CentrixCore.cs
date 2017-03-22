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

            var ass = System.Reflection.Assembly.GetExecutingAssembly();
            sch.AddExecuteOnceTask(new DateTime(2016, 9, 21), 0, Framework.Core.Helpers.ReadEmbeddedTextResource(ass, "Resources.CentrixCore.sp_Zbroji_v1.txt"));
        }

        private IDBTable tblCases(IDBSchema sch)
        {
            var rev = new DBRevision(new DateTime(2016, 9, 19), 0, eDBRevisionType.Create);
            var ret = DBMacros.AddDBTableIDNaziv("tblCases", sch, rev);
                    
            return ret;
        }

    }
}
