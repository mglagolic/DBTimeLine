using System.Collections.Generic;
using System.Data.Common;
using Framework.DBTimeLine;
using MRFramework.MRPersisting.Factory;
using System.Data;
using Framework.Persisting.Interfaces;
using System;
using DBTimeLiners.DBModules.EventArgs;

namespace DBTimeLiners.DBModules
{
    public class Loader
    {
        public List<IDBModule> DBModules { get; set; } = new List<IDBModule>();

        #region Events and event raisers

        public delegate void ModuleLoadedEventHandler(object sender, ModuleLoadedEventArgs e);
        public event ModuleLoadedEventHandler ModuleLoaded;
        public void OnModuleLoaded(object sender, ModuleLoadedEventArgs e)
        {
            ModuleLoaded(sender, e);
        }

        #endregion

        public List<IDBModule> LoadModulesFromDB(DBTimeLiner dBTimeLiner)
        {
            //var ret = New List(Of IDBModule);
            var ret = new List<IDBModule>();

            using (DbConnection cnn = MRC.GetConnection())
            {
                try
                {
                    cnn.Open();

                    var per = new Persisters.DBModulePersister();
                    per.Where = "Active = 1";
                    per.CNN = cnn;
                    List<IDlo> res = per.GetData(null);

                    foreach (IDlo module in res)
                    {
                        string errorMessage = "";
                        string message = "";
                        string className = "";
                        string assemblyName = "DBTimeLiners.DBModules";
                        string defaultSchemaName = "";
                        try
                        {
                            className = (string)module.ColumnValues["ClassName"];
                            defaultSchemaName = (string)module.ColumnValues["DefaultSchemaName"];

                            IDBModule m = (IDBModule)Activator.CreateInstance(assemblyName, assemblyName + "." + className).Unwrap();
                            m.DefaultSchemaName = defaultSchemaName;
                            m.Parent = dBTimeLiner;

                            message = string.Format(
@"Successfully instanced module (ClassName: {0}, AssemblyName: {1}, DefaultSchemaName: {2}).", className, assemblyName, defaultSchemaName);

                            ret.Add(m);
                        }
                        catch (Exception ex)
                        {
                            errorMessage = string.Format(
@"Error instancing module from database config (ClassName: {0}, AssemblyName: {1}, DefaultSchemaName: {2}),
ErrorMessage: 
{3}", className, assemblyName, defaultSchemaName, ex.Message);

                            ret.Clear();
                            break;
                        }
                        finally
                        {
                            OnModuleLoaded(this, new ModuleLoadedEventArgs() { ErrorMessage = errorMessage, Message = message });
                        }
                    }
                }
                catch (Exception)
                {
                    // CONSIDER - do some logging
                    throw;
                }
                foreach (IDBModule m in ret)
                {
                    DBModules.Add(m);
                    dBTimeLiner.DBModules.Add(m);
                }
            }

            return ret;
        }
    }
}

