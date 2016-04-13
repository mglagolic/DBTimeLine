using Framework.Persisting.Enums;
using Framework.Persisting.Interfaces;

namespace Framework.Persisting
{
    public class PersistingSettings
    {
        private static PersistingSettings instance;

        private PersistingSettings()
        {
         
        }
        
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

        private eDBType _DBType = eDBType.TransactSQL;
        public eDBType DBType
        {
            get
            {
                return _DBType;
            }
            set
            {
                if (_DBType != value)
                {
                    _DBType = value;
                    _SqlGenerator = SqlGeneratorFactory.GetSqlGenerator(DBType);
                }
            }
        }

        private ISqlGeneratorFactory _SqlGeneratorFactory = null;
        public ISqlGeneratorFactory SqlGeneratorFactory { get { return _SqlGeneratorFactory; }
            set
            {
                if (_SqlGeneratorFactory != value)
                {
                    _SqlGeneratorFactory = value;
                    _SqlGenerator = SqlGeneratorFactory.GetSqlGenerator(DBType);
                }
            }
        }

        private ISqlGenerator _SqlGenerator = null;
        public ISqlGenerator SqlGenerator
        {
            get
            {
                return _SqlGenerator;
            }
        }

        public int DefaultPageSize { get; set; } = 20;
    }
}
