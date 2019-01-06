using Microsoft.Extensions.Configuration;
using System.IO;

namespace SisPmsCore4.Resources
{
    public class SystemResources
    {
        public static string stringConnection;
        public static string pPath;

        public static void SetSystemJsonVariables()
        {
            pPath = Path.GetFullPath(Directory.GetCurrentDirectory());
            var config = new ConfigurationBuilder().SetBasePath(pPath).AddJsonFile("appsettings.json").Build();
            stringConnection = config.GetSection("ConnectionString").Value;
        }
    }
}
