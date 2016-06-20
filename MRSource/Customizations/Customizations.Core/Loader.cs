using System.Collections.Generic;
using System.Reflection;
using System.IO;
using Framework.Persisting.Interfaces;
using MRFramework.MRPersisting.Factory;
using System.Data.Common;
using Customizations.Core.EventArgs;
using System.Linq;

namespace Customizations.Core
{
    public class Loader
    {
        public List<Customizer> Customizers { get; set; } = new List<Customizer>();

        //# Region "Events and event raisers"

        #region Events and event raisers
        public delegate void CustomizationLoadedEventHandler(object sender, CustomizationLoadedEventArgs e);
        public event CustomizationLoadedEventHandler CustomizationLoaded;
        public void OnCustomizationLoaded(object sender, CustomizationLoadedEventArgs e)
        {
            CustomizationLoaded(sender, e);
        }
        
        #endregion

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
                        //TODO - registrirati ga u neki Customization property i napisati  customization invoker za određeni key i taj invoker ugraditi u DBModule
                        //Customizer customizer = (Customizer)System.Activator.CreateInstance(typ);
                        //Customizers.Add(customizer);
                        //var o = customizations.Contains(customizationAttrib.CustomizationKey);

                        OnCustomizationLoaded(this, new CustomizationLoadedEventArgs() { Message = "Customizer loaded (CustomizationKey: " + customizationAttrib.CustomizationKey + ")."});
                    }
                }
            }
        }
    }
}
