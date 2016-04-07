using System;
using Framework.Persisting.Enums;
using Framework.Persisting.Interfaces;

namespace Framework.Persisting.Implementation
{
    public class SqlGeneratorFactory : ISqlGeneratorFactory
    {
        public ISqlGenerator GetSqlGenerator(eDBType dBType)
        {
            ISqlGenerator ret = null;

            switch (dBType)
            {
                case eDBType.TransactSQL:
                    ret = new SqlGenerator();
                    break;
                case eDBType.SqlServer:
                    break;
                case eDBType.MySql:
                    break;
                default:
                    throw new NotSupportedException();
            }

            return ret;
        }
    }
}
