using System;
using Framework.DBTimeLine.DBObjects;
using Framework.DBTimeLine;

namespace Customizations.MusicPublisher
{
    public class MusicPublisherModule : DBModule
    {
        public override string ModuleKey
        {
            get
            {
                return "MusicPublisher";
            }
        }
        public MusicPublisherModule()
        {
            DefaultSchemaName = "DDEX";
        }

        public override void CreateTimeLine()
        {
            var rev = new DBRevision(new DateTime(2017, 3, 22), 0, eDBRevisionType.Create);

            IDBSchema sch = AddSchema(DefaultSchemaName, new DBSchemaDescriptor());
            if (DefaultSchemaName != "dbo") sch.AddRevision(new DBRevision(rev));
            
            Tasks(sch);
        }

        
        private void Tasks(IDBSchema sch)
        {
            sch.AddRevision(new DBRevision(new DateTime(2017, 3, 22), 0, eDBRevisionType.AlwaysExecuteTask, FillClaims));
        }

        #region Tasks
        
        private string FillClaims(IDBRevision sender, eDBType dBType)
        {
            return
@"
INSERT INTO Common.Claim (ID, Name)
SELECT 
	a.ID, a.Name
FROM
	(
		SELECT ID = NEWID(), Name = 'UserManager'
        UNION ALL SELECT ID = NEWID(), Name = 'RoleManager'
	) a
	LEFT JOIN Common.Claim c on a.Name = c.Name
WHERE 
	c.Name IS NULL
";
        }
        

        #endregion

    }
}
