using System.Collections.Generic;
using System.Reflection;
using System.IO;
using Framework.Persisting.Interfaces;
using MRFramework.MRPersisting.Factory;
using System.Data.Common;

namespace Customizations.Core
{
    public class Loader
    {
        public List<Customizer> Customizers { get; set; } = new List<Customizer>();

        public void Load()
        {
            Customizers.Clear();

            HashSet<IDlo> customizations = null;
            using (DbConnection cnn = MRC.GetConnection())
            {
                var p = new Persisters.DBCustomizationPersister();
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
                    if (customizationAttrib != null)
                    {
                        Customizers.Add((Customizer)System.Activator.CreateInstance(typ));
                    }
                }
            }
        }
    }
}
