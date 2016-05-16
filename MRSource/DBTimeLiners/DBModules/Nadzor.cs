using System.Collections.Generic;
using Framework.DBTimeLine;
using System;
using Framework.DBTimeLine.DBObjects;

namespace DBModules
{
    public class Nadzor : DBModule
    {
        public override string ModuleKey
        {
            get
            {
                return "Nadzor";
            }
        }

        public override void CreateTimeLine()
        {
            DBRevision rev = new DBRevision(new DateTime(2016, 4, 25), 0, eDBRevisionType.Create);

            IDBSchema sch = AddSchema("NAD", new DBSchemaDescriptor(), new DBRevision(rev));

            Zona(sch);
            tblNadzor(sch);


            InitialFill(sch);
        }

        private IDBTable Zona(IDBSchema sch)
        {
            IDBTable t = null;
            DBRevision rev = new DBRevision(new DateTime(2016, 4, 25), 0, eDBRevisionType.Create);

            t = sch.AddTable("Zona", new DBTableDescriptor() { CreatorFieldName = "ID", CreatorFieldDescriptor = new DBFieldDescriptor() { FieldType = eDBFieldType.Guid, Nullable = false }}, 
                new DBRevision(rev));
            t.AddConstraint(new DBPrimaryKeyConstraintDescriptor("ID"), 
                new DBRevision(rev));

            t.AddField("Naziv", new DBFieldDescriptor() { FieldType = eDBFieldType.Nvarchar, Size = 256 }, 
                new DBRevision(rev));
                                    
            return t;
        }
        
        private IDBTable tblNadzor(IDBSchema sch)
        {
            IDBTable t = null;
            DBRevision rev = new DBRevision(new DateTime(2016, 4, 26), 0, eDBRevisionType.Create);

            t = sch.AddTable("Nadzor", new DBTableDescriptor() { CreatorFieldName = "ID", CreatorFieldDescriptor = new DBFieldDescriptor() { FieldType = eDBFieldType.Guid, Nullable = false } },
                new DBRevision(rev));
            t.AddConstraint(new DBPrimaryKeyConstraintDescriptor("ID"),
                new DBRevision(rev));
            t.AddField("Datum", new DBFieldDescriptor() { FieldType = eDBFieldType.Datetime},
                new DBRevision(rev));
            t.AddField("ZonaID", new DBFieldDescriptor() { FieldType = eDBFieldType.Guid },
                new DBRevision(rev));

            t.AddConstraint(new DBForeignKeyConstraintDescriptor(new List<string>() { "ZonaID" }, "Nad.Zona", new List<string>() { "ID" }),
                new DBRevision(new DateTime(2016, 4, 26), 1, eDBRevisionType.Create));

            return t;
        }

        private string InitialFillZona(IDBRevision sender, eDBType dBType)
        {
            
            return @"

INSERT INTO NAD.Zona (ID, Naziv) 
SELECT '37D047AF-E2DA-4E08-B25C-5B79EFA94927', 'No man s land' UNION ALL
SELECT '56FAFD8E-A7A3-40C1-B394-694B3162A384', 'Mans land'
";
        }

        private string InitialFillNadzor(IDBRevision sender, eDBType dBType)
        {
            return @"
IF NOT EXISTS (SELECT TOP 1 1 FROM Nad.Nadzor WHERE ID = '57568560-B3CB-42B9-A018-45DAD9632519')
BEGIN
    INSERT INTO NAD.Nadzor (ID, Datum, ZonaID) 
    SELECT '57568560-B3CB-42B9-A018-45DAD9632519', getdate(), '37D047AF-E2DA-4E08-B25C-5B79EFA94927'
END
IF NOT EXISTS (SELECT TOP 1 1 FROM Nad.Nadzor WHERE ID = '559B02AB-AB0D-484B-8E7B-224E708F9E38')
BEGIN
    INSERT INTO NAD.Nadzor (ID, Datum, ZonaID) 
    SELECT '559B02AB-AB0D-484B-8E7B-224E708F9E38', DATEADD(d, -1, getdate()), '37D047AF-E2DA-4E08-B25C-5B79EFA94927'
END
";
        }

        
        private void InitialFill(IDBSchema sch)
        {
            IDBTable t = sch.AddTable("Zona", null);
            t.AddRevision(new DBRevision(new DateTime(2016, 4, 27), 0, eDBRevisionType.Task, InitialFillZona));
            
            t = sch.AddTable("Nadzor", null);
            t.AddRevision(new DBRevision(new DateTime(2016, 4, 27), 1, eDBRevisionType.Task, InitialFillNadzor));

        }
    }
}
