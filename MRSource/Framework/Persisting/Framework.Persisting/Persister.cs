using MRFramework.MRPersisting.Factory;
using Framework.Persisting.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System;
using System.Text;
using Framework.Persisting.Implementation;
using System.Linq;

namespace Framework.Persisting
{
    // TODO  - iskoristiti entity framework da se ne gubi vrijeme i malo za skolu
    public abstract class Persister : IPersister
    {
        public Persister()
        {
            SqlGenerator = PersistingSettings.Instance.SqlGenerator;
        }
        private ISqlGenerator SqlGenerator { get; set; }

        public abstract string DataBaseTableName { get; }

        protected string SqlBase
        {
            get
            {
                return "SELECT * FROM " + DataBaseTableName;
            }
        }

        public virtual string Sql
        {
            get
            {
                return SqlBase;
            }
        }

        public DbConnection CNN { get; set; }

        
        private List<IOrderItem> _OrderItems = new List<IOrderItem>();
        public List<IOrderItem> OrderItems
        {
            get
            {
                return _OrderItems;
            }
        }

        public string Where { get; set; }

        public HashSet<IDlo> GetData(DbTransaction transaction = null)
        {
            var ret = new HashSet<IDlo>();
            SetPrimaryKey();
            using (DbCommand cmd = MRC.GetCommand(CNN))
            {
                //string sql = SqlGenerator.GetSql(Sql, Where, GetOrderByClause(), 1, 10);
                cmd.CommandText = SqlGenerator.GetSql(Sql, Where, GetOrderByClause());
                cmd.Transaction = transaction;
                
                //var lsDataColumn = new List<DataColumn>();
                //lsDataColumn.AddRange(PrimaryKey);

                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var dlo = new Dlo();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var columPK = PrimaryKey.Where(dc => dc.ColumnName == reader.GetName(i)).ToList();
                            string columnName = reader.GetName(i);
                            object value = reader.GetValue(i);

                            if (columPK != null && columPK.Count > 0)
                            {
                                dlo.PrimaryKeyValues.Add(columnName, value);
                            }
                            dlo.ColumnValues.Add(columnName, value);
                        }
                        ret.Add(dlo);
                    }
                }

            }
             


            

            var dyn = new { P1 = "Pero", P2 = 2};
            
            //SetPrimaryKey();

            return ret;
            
        }

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

        private void SetPrimaryKey()
        {
            if (PrimaryKey.Count == 0)
            {
                PrimaryKey.AddRange(GetPrimaryKeyFromDB());
            }
        }

        private DataTable _SchemaTable = null;
        private DataTable SchemaTable
        {
            get
            {
                if (_SchemaTable == null)
                {
                    _SchemaTable = PersistingFactoryHelpers.GetSchema(Sql);
                }
                return _SchemaTable;
            }
        }

        private List<DataColumn> _PrimaryKey = new List<DataColumn>();
        private List<DataColumn> PrimaryKey
        {
            get
            {
                return _PrimaryKey;
            }
        }
        
        private List<DataColumn> GetPrimaryKeyFromDB()
        {
            List<DataColumn> ret = null;
            DataRow[] drows = SchemaTable.Select("ISKEY = 1 AND ISHIDDEN = 0");
            if (drows != null && drows.Length > 0)
            {
                ret = new List<DataColumn>();
                for (int i = 0; i < drows.Length; i++)
                {
                    ret.Add(new DataColumn(drows[i]["ColumnName"].ToString()));
                }
            }
            return ret;
        }
        
        #endregion
    }
}
