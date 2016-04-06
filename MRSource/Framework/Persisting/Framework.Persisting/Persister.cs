using MRFramework.MRPersisting.Factory;
using Framework.Persisting.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Framework.Persisting
{
    // TODO  - iskoristiti entity framework da se ne gubi vrijeme i malo za skolu
    public abstract class Persister
    {
        public Persister()
        {

        }

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

        // TODO_OLD - ovo preseliti u persisting factory kao static metodu

        //protected internal DataTable GetSchema()
        public DataTable GetSchema()
        {
            DataTable ret = null;

            using (DbConnection lookupCnn = MRC.GetConnection())
            {
                using (DbCommand cmd = MRC.GetCommand(lookupCnn))
                {
                    cmd.CommandText = Sql;
                    try
                    {
                        lookupCnn.Open();
                        using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.Default | CommandBehavior.KeyInfo | CommandBehavior.SchemaOnly))
                        {
                            ret = reader.GetSchemaTable();
                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }
                    catch (System.Exception ex)
                    {
                        throw new System.Exception("Error connecting to database.", ex);
                    }
                }
            }

            return ret;
        }
    }
}
