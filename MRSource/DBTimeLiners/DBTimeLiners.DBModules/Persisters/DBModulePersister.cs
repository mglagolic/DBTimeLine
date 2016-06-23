namespace DBTimeLiners.DBModules.Persisters
{
    class DBModulePersister : Framework.Persisting.Persister
    {
        protected override string DataBaseTableName
        {
            get
            {
                return "DBTimeLine.Module";
            }
        }

        protected override string Sql
        {
            get
            {
                return "SELECT * FROM " + DataBaseTableName;
            }
        }
    }
}
