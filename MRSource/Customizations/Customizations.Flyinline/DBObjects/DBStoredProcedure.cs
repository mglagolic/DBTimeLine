using Framework.DBTimeLine;

namespace Customizations.Flyinline.DBObjects
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
            string errorTryBlock = "";
            string errorCatchBlock = "";
            if (((DBStoredProcedureDescriptor)Descriptor).ErrorHandling)
            {
                errorTryBlock = @"BEGIN TRY";
                errorCatchBlock =
@"END TRY  
BEGIN CATCH  
    DECLARE @ErrorMessage NVARCHAR(4000);  
    DECLARE @ErrorSeverity INT;  
    DECLARE @ErrorState INT;  
  
    SELECT   
        @ErrorMessage = ERROR_MESSAGE(),  
        @ErrorSeverity = ERROR_SEVERITY(),  
        @ErrorState = ERROR_STATE();  
	/*

		Do some logging!

	*/
    SET @ErrorMessage = 'Error handled. ' + @ErrorMessage

    RAISERROR (@ErrorMessage, -- Message text.  
               @ErrorSeverity, -- Severity.  
               @ErrorState -- State.  
               );  
END CATCH";
            }

            ret = string.Format(
@"GO
CREATE PROCEDURE {0}.{1}
{2}
AS
{4}
{3}
{5}
", SchemaName, Name, ((DBStoredProcedureDescriptor)Descriptor).Parameters, ((DBStoredProcedureDescriptor)Descriptor).Body, errorTryBlock, errorCatchBlock);
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
