namespace Customizations.Core.Persisters
{
    class DBCustomizationPersister : Framework.Persisting.Persister
    {
        protected override string DataBaseTableName
        {
            get
            {
                return "Config.Customization";
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
