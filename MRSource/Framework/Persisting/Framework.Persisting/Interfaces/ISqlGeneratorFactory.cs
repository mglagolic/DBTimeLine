using Framework.Persisting.Enums;

namespace Framework.Persisting.Interfaces
{
    public interface ISqlGeneratorFactory
    {
        ISqlGenerator GetSqlGenerator(eDBType dBType);

    }
}
