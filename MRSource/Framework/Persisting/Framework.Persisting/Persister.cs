using MRFramework.MRPersisting.Factory;
using Framework.Persisting.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Framework.Persisting.Implementation;

namespace Framework.Persisting
{
    public abstract class Persister : IPersister
    {
        public Persister()
        {
            SqlGenerator = PersistingSettings.Instance.SqlGenerator;
        }

        private ISqlGenerator SqlGenerator { get; set; }

        protected abstract string DataBaseTableName { get; }
        public abstract string Sql { get; }

        #region private
        private string GetOrderByClause()
        {
            var sb = new StringBuilder();
            foreach (var oi in OrderItems)
            {
                sb.Append(oi.SqlName + " ");
                if (oi.Direction == Enums.eOrderDirection.Descending)
                {
                    sb.Append("DESC ");
                }
                sb.Append(",");
                
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }

            return sb.ToString().Trim();
        }

        private void SetSchemaTableIfNull()
        {
            if (SchemaTable == null) SchemaTable = PersistingFactoryHelpers.GetSchema(Sql);
        }

        private void SetPrimaryKeyIfNull()
        {
            if (PrimaryKey.Count == 0)
            {
                PrimaryKey.AddRange(GetPrimaryKeyFromDB());
            }
        }

        private DataTable SchemaTable { get; set; } = null;

        private List<DataColumn> PrimaryKey { get; } = new List<DataColumn>();

        private List<DataColumn> GetPrimaryKeyFromDB()
        {
            List<DataColumn> ret = null;
            // TODO - dohvatiti PK na ispravan nacin, ovo dolje je krivo. uzeti schemu base selecta pa onda naci mozda naci key ili koristiti datatable.fillschema i primary key property

            //DataRow[] drows = SchemaTable.Select("ISKEY = 1 AND ISHIDDEN = 0");
            //if (drows != null && drows.Length > 0)
            //{
            //    ret = new List<DataColumn>();
            //    for (int i = 0; i < drows.Length; i++)
            //    {
            //        ret.Add(new DataColumn(drows[i]["ColumnName"].ToString()));
            //    }
            //}
            return ret;
        }

        #endregion

        #region IPersister

        public DbConnection CNN { get; set; }
       
        public string Where { get; set; }

        public List<IOrderItem> OrderItems { get; } = new List<IOrderItem>();

        public int PageSize { get; set; } = PersistingSettings.Instance.DefaultPageSize;
        
        public HashSet<IDlo> GetData(DbTransaction transaction, int pageNumber = -1)
        {
            var ret = new HashSet<IDlo>();

            SetSchemaTableIfNull();
            SetPrimaryKeyIfNull();            

            using (DbCommand cmd = MRC.GetCommand(CNN))
            {
                cmd.CommandText = SqlGenerator.GetSql(Sql, Where, GetOrderByClause(), pageNumber, PageSize);
                cmd.Transaction = transaction;

                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var dlo = new Dlo();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            // CONSIDER - treba li poslusati best practices dbreadera (Stringove vracati s reader.GetString, decimali reader.GetDecimal, itd.) testirati performance profilerom
                            dlo.ColumnValues.Add(reader.GetName(i), reader.GetValue(i));
                        }
                        ret.Add(dlo);
                    }
                }
            }
            return ret;
        }
        
        #endregion
    }
}
