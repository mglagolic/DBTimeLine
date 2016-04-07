using MRFramework.MRPersisting.Factory;
using Framework.Persisting.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System;
using System.Text;

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

        public Dictionary<object, IDlo> GetData(DbTransaction transaction = null)
        {
            var ret = new Dictionary<object, IDlo>();

            string sql = SqlGenerator.GetSql(Sql, Where, GetOrderByClause());

            SetPrimaryKey();

            return ret;
            
        }

        #region private
        private string GetOrderByClause()
        {
            var sb = new StringBuilder();
            foreach (var oi in OrderItems)
            {
                sb.Append(oi.Name + " ");
                if (oi.Direction == Enums.eOrderDirection.Descending)
                {
                    sb.Append("DESC ");
                }
                sb.Append(",");
                
            }
            sb.Remove(sb.Length - 1, 1);

            return sb.ToString().Trim();
        }

        private void SetPrimaryKey()
        {
                if (PrimaryKey == null)
            {
                _PrimaryKey = GetPrimaryKeyFromDB();
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

        private DataColumn[] _PrimaryKey = null;
        private DataColumn[] PrimaryKey
        {
            get
            {
                return _PrimaryKey;
            }
        }

        private DataColumn[] GetPrimaryKeyFromDB()
        {
            DataColumn[] ret = null;
            DataRow[] drows = SchemaTable.Select("ISKEY = 1 AND ISHIDDEN = 0");
            if (drows != null && drows.Length > 0)
            {
                ret = new DataColumn[drows.Length];
                for (int i = 0; i < drows.Length; i++)
                {
                    ret[i] = new DataColumn(drows[i]["ColumnName"].ToString());
                }
            }
            return ret;
        }


        #endregion
    }
}
