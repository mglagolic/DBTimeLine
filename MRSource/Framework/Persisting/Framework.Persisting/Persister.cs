using MRFramework.MRPersisting.Factory;
using Framework.Persisting.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Framework.Persisting.Implementation;
using Framework.Persisting.Enums;

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
        protected abstract string Sql { get; }

        #region private
        
        private void SetSchemaTableIfNull()
        {
            if (SchemaTable == null) SchemaTable = PersistingFactoryHelpers.GetSchema(Sql);
        }

        private void SetPrimaryKeyIfNull()
        {
            if (PrimaryKey.Count == 0)
            {
                PrimaryKey.AddRange(PersistingFactoryHelpers.GetPrimaryKeyFromDB(DataBaseTableName));
            }
        }

        private DataTable SchemaTable { get; set; } = null;

        private List<DataColumn> PrimaryKey { get; } = new List<DataColumn>();

        #endregion

        #region IPersister

        public DbConnection CNN { get; set; }
       
        public string Where { get; set; }

        public List<IOrderItem> OrderItems { get; } = new List<IOrderItem>();

        public int PageSize { get; set; } = PersistingSettings.Instance.DefaultPageSize;
        
        public List<IDlo> GetData(DbTransaction transaction, int pageNumber = -1)
        {
            var ret = new List<IDlo>();

            SetSchemaTableIfNull();
            SetPrimaryKeyIfNull();         

            using (DbCommand cmd = MRC.GetCommand(CNN))
            {
                cmd.CommandText = SqlGenerator.GetSql(Sql, Where, SqlGenerator.GetOrderByClause(OrderItems), pageNumber, PageSize);
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
