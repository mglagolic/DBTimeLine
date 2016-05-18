using Framework.DBTimeLine;

namespace DBModules
{
    public class DBStoredProcedureDescriptor : IDBObjectDescriptor
    {
        public IDBObject GetDBObjectInstance(IDBChained parent = null)
        {
            return new DBStoredProcedure() { Parent = parent, Descriptor = this};
        }

        public string Parameters { get; set; }
        public string Body { get; set; }
}
}
