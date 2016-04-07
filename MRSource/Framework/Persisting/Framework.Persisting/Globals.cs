using Framework.Persisting.Enums;
using Framework.Persisting.Interfaces;

namespace Framework.Persisting
{
    public class PersistingSettings
    {
        private static PersistingSettings instance;

        private PersistingSettings() { }
        
        public static PersistingSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PersistingSettings();
                }
                return instance;
            }
        }
        private eDBType dBType = eDBType.TransactSQL;
        public eDBType DBType
        {
            get
            {
                return dBType;
            }
            set
            {
                dBType = value;
            }
        }
        public ISqlGeneratorFactory SqlGeneratorFactory { get; set; }

        private ISqlGenerator sqlGenerator = null;
        public ISqlGenerator SqlGenerator
        {
            get
            {
                if (sqlGenerator == null)
                {
                    sqlGenerator = SqlGeneratorFactory.GetSqlGenerator(DBType);
                }
                return sqlGenerator;
            }
        }

    }
}

