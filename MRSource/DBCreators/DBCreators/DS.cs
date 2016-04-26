using System.Collections.Generic;
using Framework.DBCreator.DBObjects;
using Framework.DBCreator;
using System;

namespace DBCreators
{
    public class DS : DBModule
    {
        public override string ModuleKey
        {
            get
            {
                return "DS";
            }
        }

        public override void CreateTimeLine()
        {
            DBRevision rev = new DBRevision(new DateTime(2016, 4, 25), 0, eDBRevisionType.Create);

            IDBSchema sch = AddSchema("DS", new DBSchemaDescriptor(), new DBRevision(rev));
            Osoba(sch);
            Grad(sch);
            Drzava(sch);

            OsobaGrad(sch);

            InitialFill(sch);
        }

        private IDBView OsobaGrad(IDBSchema sch)
        {
            IDBView v = null;
            DBRevision rev = new DBRevision(new DateTime(2016, 4, 26), 2, eDBRevisionType.Create);

            v = sch.AddView("OsobaGrad", new DBViewDescriptor()
            {
                WithSchemaBinding = true,
                Body =
@"SELECT
    oso.ID,
    Naziv = oso.Ime + ' ' + oso.Prezime,
    GradNaziv = grad.Naziv,
    DrzavaNaziv = drz.Naziv
FROM
    DS.Osoba oso
    LEFT JOIN DS.Grad grad ON oso.GradID = grad.ID
    LEFT JOIN DS.Drzava drz ON grad.DrzavaID = drz.ID
"
            }, new DBRevision(rev));

            return v;
        }
        private IDBTable Osoba(IDBSchema sch)
        {
            IDBTable t = null;
            DBRevision rev = new DBRevision(new DateTime(2016, 4, 25), 0, eDBRevisionType.Create);

            t = sch.AddTable("Osoba", new DBTableDescriptor() { CreatorFieldName = "ID", CreatorFieldDescriptor = new DBFieldDescriptor() { FieldType = eDBFieldType.Guid, Nullable = false }}, 
                new DBRevision(rev));
            t.AddConstraint(new DBPrimaryKeyConstraintDescriptor("ID"), 
                new DBRevision(rev));
            t.AddField("Ime", new DBFieldDescriptor() { FieldType = eDBFieldType.Nvarchar, Size = 256 }, 
                new DBRevision(rev));
            t.AddField("Prezime", new DBFieldDescriptor() { FieldType = eDBFieldType.Nvarchar, Size = 256 }, 
                new DBRevision(rev));
            t.AddField("GradID", new DBFieldDescriptor() { FieldType = eDBFieldType.Guid}, 
                new DBRevision(rev));

            t.AddConstraint(new DBForeignKeyConstraintDescriptor(new List<string>() { "GradID" }, "DS.Grad", new List<string>() { "ID" }),
                new DBRevision(new DateTime(2016, 4, 25), 1, eDBRevisionType.Create));
            return t;
        }

        private IDBTable Grad(IDBSchema sch)
        {
            IDBTable t = null;
            DBRevision rev = new DBRevision(new DateTime(2016, 4, 25), 0, eDBRevisionType.Create);

            t = sch.AddTable("Grad", new DBTableDescriptor() { CreatorFieldName = "ID", CreatorFieldDescriptor = new DBFieldDescriptor() { FieldType = eDBFieldType.Guid, Nullable = false } },
                new DBRevision(rev));
            t.AddConstraint(new DBPrimaryKeyConstraintDescriptor("ID"), 
                new DBRevision(rev));
            t.AddField("Naziv", new DBFieldDescriptor() { FieldType = eDBFieldType.Nvarchar, Size = 256 }, 
                new DBRevision(rev));

            IDBField fld = t.AddField("DrzavaID", new DBFieldDescriptor() { FieldType = eDBFieldType.Guid, Nullable = true },
                new DBRevision(new DateTime(2016, 4, 26), 1, eDBRevisionType.Create, null, UpdateGradDrzavom));

            t.AddConstraint(new DBForeignKeyConstraintDescriptor(new List<string>() { "DrzavaID" }, "DS.Drzava", new List<string>() { "ID" }),
                new DBRevision(new DateTime(2016, 4, 26), 1, eDBRevisionType.Create));

            fld.AddRevision(
                new DBRevision(new DateTime(2016, 4, 26), 2, eDBRevisionType.Modify),
                new DBFieldDescriptor(fld.Descriptor) { Nullable = false }
                );

            return t;
        }

        private IDBTable Drzava(IDBSchema sch)
        {
            IDBTable t = null;
            DBRevision rev = new DBRevision(new DateTime(2016, 4, 26), 0, eDBRevisionType.Create);

            t = sch.AddTable("Drzava", new DBTableDescriptor() { CreatorFieldName = "ID", CreatorFieldDescriptor = new DBFieldDescriptor() { FieldType = eDBFieldType.Guid, Nullable = false } },
                new DBRevision(rev));
            t.AddConstraint(new DBPrimaryKeyConstraintDescriptor("ID"),
                new DBRevision(rev));
            t.AddField("Naziv", new DBFieldDescriptor() { FieldType = eDBFieldType.Nvarchar, Size = 256 },
                new DBRevision(rev));

            return t;
        }

        private string UpdateGradDrzavom(IDBRevision sender)
        {
            return @"GO
UPDATE DS.Grad 
SET DrzavaID = '37D047AF-E2DA-4E08-B25C-5B79EFA94927'
";
        }

        private string InitialFillDrzava(IDBRevision sender)
        {
            
            return @"

INSERT INTO DS.Drzava (ID, Naziv) SELECT '37D047AF-E2DA-4E08-B25C-5B79EFA94927', 'Hrvatska'";
        }

        private string InitialFillGrad(IDBRevision sender)
        {
            return @"
IF NOT EXISTS (SELECT TOP 1 1 FROM DS.Grad WHERE ID = '57568560-B3CB-42B9-A018-45DAD9632519')
BEGIN
    INSERT INTO DS.Grad (ID, Naziv) 
    SELECT '57568560-B3CB-42B9-A018-45DAD9632519', 'Zagreb' 
END
IF NOT EXISTS (SELECT TOP 1 1 FROM DS.Grad WHERE ID = '559B02AB-AB0D-484B-8E7B-224E708F9E38')
BEGIN
    INSERT INTO DS.Grad (ID, Naziv) 
    SELECT '559B02AB-AB0D-484B-8E7B-224E708F9E38', 'Marija Bistrica'
END
";
        }

        private string InitialFillOsoba(IDBRevision sender)
        {
            return @"
INSERT INTO DS.Osoba (ID, Ime, Prezime, GradID) 
SELECT newid(), 'Jelena', 'Stinčić Glagolić', '559B02AB-AB0D-484B-8E7B-224E708F9E38' UNION ALL
SELECT newid(), 'Miro', 'Glagolić', '57568560-B3CB-42B9-A018-45DAD9632519'";
        }

        private void InitialFill(IDBSchema sch)
        {
            IDBTable t = sch.AddTable("Grad", null);
            t.AddRevision(new DBRevision(new DateTime(2016, 4, 25), 2, eDBRevisionType.Task, InitialFillGrad));
            t.AddRevision(new DBRevision(new DateTime(2016, 4, 25), 3, eDBRevisionType.AlwaysExecute, InitialFillGrad));

            t = sch.AddTable("Osoba", null);
            t.AddRevision(new DBRevision(new DateTime(2016, 4, 25), 3, eDBRevisionType.Task, InitialFillOsoba));

            t = sch.AddTable("Drzava", null);
            t.AddRevision(new DBRevision(new DateTime(2016, 4, 26), 1, eDBRevisionType.Task, InitialFillDrzava));
        }
    }
}
