﻿using System;
using Framework.DBTimeLine.DBObjects;
using Framework.DBTimeLine;
using System.Collections.Generic;

namespace Customizations.Identity
{
    public class IdentityModule : DBModule
    {
        public override string ModuleKey
        {
            get
            {
                return "Identity";
            }
        }
        public IdentityModule()
        {
            DefaultSchemaName = "Common";
        }

        public override void CreateTimeLine()
        {
            var rev = new DBRevision(new DateTime(2016, 6, 10), 0, eDBRevisionType.Create);

            IDBSchema sch = AddSchema(DefaultSchemaName, new DBSchemaDescriptor());
            if (DefaultSchemaName != "dbo") sch.AddRevision(new DBRevision(rev));

            Principal(sch);
            Role(sch);
            Claim(sch);
            PrincipalHasRole(sch);
            RolePermission(sch);
            PrincipalPermission(sch);

            ClaimPermission(sch);

            Tasks(sch);
        }

        private IDBTable Principal(IDBSchema sch)
        {
            var rev = new DBRevision(new DateTime(2016, 6, 10), 0, eDBRevisionType.Create);
            var ret = DBMacros.AddDBTableID("Principal", sch, rev);

            ret.AddField("Username", DBMacros.DBFieldNazivDescriptor(false), rev);

            ret.AddField("SuperAdmin", DBMacros.DBFieldBitDescriptor(false), rev);

            return ret;
        }

        private IDBTable Role(IDBSchema sch)
        {
            var rev = new DBRevision(new DateTime(2016, 6, 10), 0, eDBRevisionType.Create);
            var ret = DBMacros.AddDBTableID("Role", sch, rev);

            ret.AddField("Name", DBMacros.DBFieldNazivDescriptor(false), rev);

            return ret;
        }

        private IDBTable Claim(IDBSchema sch)
        {
            var rev = new DBRevision(new DateTime(2016, 6, 10), 0, eDBRevisionType.Create);
            var ret = DBMacros.AddDBTableID("Claim", sch, rev);

            ret.AddField("Name", DBMacros.DBFieldNazivDescriptor(false), rev);

            return ret;
        }

        private IDBTable PrincipalHasRole(IDBSchema sch)
        {
            var rev = new DBRevision(new DateTime(2016, 6, 10), 0, eDBRevisionType.Create);
            var ret = DBMacros.AddDBTableID("PrincipalHasRole", sch, rev);

            DBMacros.AddForeignKeyFieldID("PrincipalID", true, ret, sch.Name + ".Principal",
                    new DBRevision(new DateTime(2016, 6, 10), 1, eDBRevisionType.Create));
            DBMacros.AddForeignKeyFieldID("RoleID", true, ret, sch.Name + ".Role",
                    new DBRevision(new DateTime(2016, 6, 10), 1, eDBRevisionType.Create));

            return ret;
        }

        private IDBTable RolePermission(IDBSchema sch)
        {
            var rev = new DBRevision(new DateTime(2016, 6, 10), 0, eDBRevisionType.Create);
            var ret = DBMacros.AddDBTableID("RolePermission", sch, rev);

            var fld = DBMacros.AddForeignKeyFieldID("RoleID", true, ret, sch.Name + ".Role",
                new DBRevision(new DateTime(2016, 6, 10), 1, eDBRevisionType.Create));
            fld.AddRevision(new DBRevision(new DateTime(2016, 6, 10), 2, eDBRevisionType.Modify),
                new DBFieldDescriptor(fld.Descriptor) { Nullable = false });
            
            DBMacros.AddForeignKeyFieldID("ClaimID", true, ret, sch.Name + ".Claim",
                new DBRevision(new DateTime(2016, 6, 10), 1, eDBRevisionType.Create));

            ret.AddField("CanExecute", DBMacros.DBFieldBitDescriptor(false),
                new DBRevision(new DateTime(2016, 6, 10), 2, eDBRevisionType.Create));

            ret.AddIndex(new DBIndexDescriptor(new List<string>() { "RoleID", "ClaimID" }, new List<string>() { "CanExecute" }) { Unique = true },
                new DBRevision(new DateTime(2016, 6, 10), 3, eDBRevisionType.Create));

            return ret;
        }

        private IDBTable PrincipalPermission(IDBSchema sch)
        {
            var rev = new DBRevision(new DateTime(2016, 6, 10), 0, eDBRevisionType.Create);
            var ret = DBMacros.AddDBTableID("PrincipalPermission", sch, rev);

            DBMacros.AddForeignKeyFieldID("PrincipalID", true, ret, sch.Name + ".Principal",
                new DBRevision(new DateTime(2016, 6, 10), 1, eDBRevisionType.Create));

            var fld = DBMacros.AddForeignKeyFieldID("ClaimID", true, ret, sch.Name + ".Claim",
                new DBRevision(new DateTime(2016, 6, 10), 1, eDBRevisionType.Create));
            fld.AddRevision(new DBRevision(new DateTime(2016, 6, 10), 2, eDBRevisionType.Modify),
                new DBFieldDescriptor(fld.Descriptor) { Nullable = false });

            ret.AddField("CanExecute", DBMacros.DBFieldBitDescriptor(false), 
                new DBRevision(new DateTime(2016, 6, 10), 2, eDBRevisionType.Create));

            ret.AddIndex(new DBIndexDescriptor(new List<string>() { "PrincipalID", "ClaimID" }, new List<string>() { "CanExecute"}) { Unique = true },
                new DBRevision(new DateTime(2016, 6, 10), 3, eDBRevisionType.Create));

            return ret;
        }
        private IDBView ClaimPermission(IDBSchema sch)
        {
            var ret = sch.AddView("ClaimPermission", new DBViewDescriptor()
            {
                Body =
@"SELECT 
	t.ClaimID, t.PrincipalID, CanExecute = MAX(CAST(t.CanExecute AS INT))
FROM
	(
		SELECT 
			PrincipalID, ClaimID, CanExecute
		FROM
			Common.PrincipalPermission pp

		UNION ALL

		SELECT 
			phr.PrincipalID, rp.ClaimID, rp.CanExecute
		FROM
			Common.RolePermission rp
			INNER JOIN Common.PrincipalHasRole phr ON rp.RoleID = phr.RoleID
	) t
GROUP BY
	t.PrincipalID, t.ClaimID",
                WithSchemaBinding = false
            },
            new DBRevision(new DateTime(2016, 6, 10), 4, eDBRevisionType.Create));

            return ret;
        }
        private void Tasks(IDBSchema sch)
        {
            sch.AddRevision(new DBRevision(new DateTime(2016, 6, 10), 1, eDBRevisionType.Task, FillAdmin));
            sch.AddRevision(new DBRevision(new DateTime(2016, 6, 10), 2, eDBRevisionType.Task, FillAdmin2));
        }

        #region Tasks
        
        private string FillAdmin(IDBRevision sender, eDBType dBType)
        {
            return
@"--WAITFOR DELAY '00:00:01'
--exec sp_updatestats

INSERT INTO Common.Role (ID, Name)
SELECT NEWID(), 'Administrator'";
        }
        private string FillAdmin2(IDBRevision sender, eDBType dBType)
        {
            return
@"--WAITFOR DELAY '00:00:01'
--exec sp_updatestats

INSERT INTO Common.Role (ID, Name)
SELECT TOP 0 NEWID(), 'Administrator'";
        }

        #endregion

    }
}