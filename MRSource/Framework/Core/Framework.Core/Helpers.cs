using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    public static class Helpers
    {
        public static Stream GetEmbeddedResourceFromResourcePath(Assembly ass, string resourcePath)
        {
            Stream ret = null;

            string resourceFullName = ass.GetManifestResourceNames()
                                .FirstOrDefault(x => x.EndsWith("." + resourcePath));
            ret = ass.GetManifestResourceStream(resourceFullName);

            return ret;
        }

        public static string ReadEmbeddedTextResource(Assembly ass, string resourcePath)
        {
            string ret = null;
            using (var stream = GetEmbeddedResourceFromResourcePath(ass, resourcePath))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    ret = reader.ReadToEnd();
                }
            }

            return ret;
        }
    }
}
