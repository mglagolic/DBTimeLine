using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.DBTimeLine;

namespace DBModules
{

    public class DBStoredProcedure : Framework.DBTimeLine.DBObjects.DBObject
    {
        public override string ObjectTypeName
        {
            get
            {
                return "SP";
            }
        }

        public override int ObjectTypeOrdinal
        {
            get
            {
                return 60;
            }
        }

        public override string GetSqlCreate(eDBType dBType)
        {
            string ret = "";
            ret = string.Format(
@"GO
CREATE PROCEDURE {0}.{1}
{2}
AS
{3}
", SchemaName, Name, ((DBStoredProcedureDescriptor) Descriptor).Parameters, ((DBStoredProcedureDescriptor)Descriptor).Body);
            return ret;
        }

        public override string GetSqlDelete(eDBType dBType)
        {
            string ret = "";
            ret = string.Format(
@"GO
DROP PROCEDURE {0}.{1}", SchemaName, Name);

            return ret;
        }

        public override string GetSqlModify(eDBType dBType)
        {
            string ret = "";
            ret = string.Format(
@"GO
ALTER PROCEDURE {0}.{1}
{2}
AS
{3}
", SchemaName, Name, ((DBStoredProcedureDescriptor)Descriptor).Parameters, ((DBStoredProcedureDescriptor)Descriptor).Body);
            return ret;
        }
    }
}
