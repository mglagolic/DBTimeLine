using System;
using Framework.DBTimeLine.DBObjects;
using Framework.DBTimeLine;
using System.Collections.Generic;
using Customizations.Flyinline.StoredProcedures;

namespace Customizations.Identity
{
    public class IdentityModule : DBModule
    {
        public override string ModuleKey
        {
            get
            {
                return "FlyinlineERM";
            }
        }
        public IdentityModule()
        {
            DefaultSchemaName = "Flyinline";
        }

        public override void CreateTimeLine()
        {
            var rev = new DBRevision(new DateTime(2016, 6, 10), 0, eDBRevisionType.Create);

            IDBSchema sch = AddSchema(DefaultSchemaName, new DBSchemaDescriptor());
            if (DefaultSchemaName != "dbo") sch.AddRevision(new DBRevision(rev));

            UserDetail(sch);

            ClearDbForTesting.Create(sch);

            Tasks(sch);
        }


        private IDBTable UserDetail(IDBSchema sch)
        {
            var rev = new DBRevision(new DateTime(2019, 9, 9), 0, eDBRevisionType.Create);
            var ret = DBMacros.AddDBTableID("UserDetail", sch, rev);

            ret.AddConstraint(new DBForeignKeyConstraintDescriptor(new List<string>() { "ID" }, "Common.Principal", new List<string>() { "ID" }), 
                new DBRevision(rev));

            ret.AddField("Username", new DBFieldDescriptor() { FieldType = new DBFieldTypeNvarchar(), Size = 512, Nullable = false },
                new DBRevision(rev));

            ret.AddConstraint(new DBUniqueConstraintDescriptor("Username"),
                new DBRevision(rev));

            ret.AddField("Email", new DBFieldDescriptor() { FieldType = new DBFieldTypeNvarchar(), Size = 512, Nullable = false },
                new DBRevision(rev));

            ret.AddField("Fullname", new DBFieldDescriptor() { FieldType = new DBFieldTypeNvarchar(), Size = 512, Nullable = false },
                new DBRevision(rev));

            ret.AddField("Nickname", new DBFieldDescriptor() { FieldType = new DBFieldTypeNvarchar(), Size = 512, Nullable = false },
                new DBRevision(rev));

            return ret;
        }

        private void Tasks(IDBSchema sch)
        {
            sch.AddRevision(new DBRevision(new DateTime(2019, 9, 9), 0, eDBRevisionType.AlwaysExecuteTask, FillClaims));
            sch.AddRevision(new DBRevision(new DateTime(2019, 9, 10), 0, eDBRevisionType.AlwaysExecuteTask, FillRoles));
        }

        #region Tasks
        
        private string FillClaims(IDBRevision sender, eDBType dBType)
        {
            return
@"WITH ClaimsCTE AS
(
    SELECT TOP 0 ID = NEWID(), Name = ''
    -- SELECT ID = NEWID(), Name = 'Users.Commands.RegisterUser'
)

INSERT INTO Common.Claim (ID, Name)
SELECT 
	claims.ID, claims.Name 
FROM 
	ClaimsCTE claims
	LEFT JOIN Common.Claim c ON claims.Name = c.Name
WHERE 
	c.ID is null
";
        }


        private string FillRoles(IDBRevision sender, eDBType dBType)
        {
            return
@"WITH RolesCTE AS
(
SELECT ID = NEWID(), Name = 'Client'
UNION ALL SELECT NEWID(), 'BusinessOwner'
)

INSERT INTO Common.Role (ID, Name)
SELECT 
	t.ID, t.Name 
FROM 
	RolesCTE t
	LEFT JOIN Common.Role r ON t.Name = r.Name
WHERE 
	r.ID is null
";
        }


//        private string FillRolePermissions(IDBRevision sender, eDBType dBType)
//        {
//            return
//@"WITH RolesCTE AS
//(
//SELECT ID = NEWID(), Name = 'Client'
//UNION ALL SELECT NEWID(), 'BusinessOwner'
//)

//INSERT INTO Common.Role (ID, Name)
//SELECT 
//	t.ID, t.Name 
//FROM 
//	RolesCTE t
//	LEFT JOIN Common.Role r ON t.Name = r.Name
//WHERE 
//	r.ID is null
//";
//        }
        #endregion

    }
}
