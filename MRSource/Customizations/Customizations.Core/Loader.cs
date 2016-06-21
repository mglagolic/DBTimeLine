using System.Collections.Generic;
using System.Reflection;
using System.IO;
using Framework.Persisting.Interfaces;
using MRFramework.MRPersisting.Factory;
using System.Data.Common;
using Customizations.Core.EventArgs;

namespace Customizations.Core
{
    public class Loader
    {
        public List<Customizer> Customizers { get; set; } = new List<Customizer>();

        #region Events and event raisers

        public delegate void CustomizationLoadedEventHandler(object sender, CustomizationLoadedEventArgs e);
        public event CustomizationLoadedEventHandler CustomizationLoaded;
        public void OnCustomizationLoaded(object sender, CustomizationLoadedEventArgs e)
        {
            CustomizationLoaded(sender, e);
        }
        
        #endregion

        public void LoadCustomizers()
        {
            Customizers.Clear();

            List<IDlo> customizations = null;
            using (DbConnection cnn = MRC.GetConnection())
            {
                var p = new Persisters.DBCustomizationPersister();
                p.Where = "Active = 1";
                p.CNN = MRC.GetConnection();
                p.CNN.Open();

                customizations = p.GetData(null);
            }
            var files = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "*.dll");
            foreach (string fileName in files)
            {
                var ass = Assembly.LoadFile(fileName);
                List<System.Type> types = new List<System.Type>();
                types.AddRange(ass.GetTypes());
                foreach (System.Type typ in types)
                {
                    var customizationAttrib = typ.GetCustomAttribute<Attributes.CustomizationAttribute>();
                    if (customizationAttrib != null && (customizations.Find(dlo => (string)dlo.ColumnValues["CustomizationKey"] == customizationAttrib.CustomizationKey)) != null)
                    {
                        Customizer customizer = (Customizer)System.Activator.CreateInstance(typ);
                        customizer.CustomizationKey = customizationAttrib.CustomizationKey;
                        Customizers.Add(customizer);
                                            
                        OnCustomizationLoaded(this, new CustomizationLoadedEventArgs() { Message = "Customizer loaded (CustomizationKey: " + customizationAttrib.CustomizationKey + ").", Customizer = customizer});
                    }
                }
            }
        }

        public void CallMethods(string methodActivationKey, Dictionary<string, object> inputs)
        {
            //var customizers = Customizers.FindAll(c => c.CustomizationKey == customizationKey);
            foreach (Customizer c in Customizers)
            {
                // CONSIDER - cacheirati
                var publicMethods = c.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);
                foreach (MethodInfo mi in publicMethods)
                {
                    var methodActivationAttrib = mi.GetCustomAttribute<Attributes.MethodActivationCustomizationAttribute>();
                    if (methodActivationAttrib != null && methodActivationAttrib.ActivationKey != null && methodActivationAttrib.ActivationKey == methodActivationKey)
                    {
                        if (inputs != null)
                        {
                            mi.Invoke(c, new object[] { inputs });
                        }
                        else
                        {
                            mi.Invoke(c, new object[] { });
                        }
                        
                    }
                }
            }
        }
    }
}
