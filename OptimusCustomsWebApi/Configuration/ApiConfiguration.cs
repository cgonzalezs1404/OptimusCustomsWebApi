using Microsoft.Extensions.Configuration;
using System.IO;

namespace Api.Configuration
{
    public class ApiConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        private static object lockObject = new object();
        /// <summary>
        /// 
        /// </summary>
        private static ApiConfiguration instance;
        /// <summary>
        /// 
        /// </summary>
        private IConfiguration configuration;

        /// <summary>
        /// 
        /// </summary>
        public static ApiConfiguration Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new ApiConfiguration();
                    }
                    return instance;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private ApiConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json",
                optional: true,
                reloadOnChange: true);
            configuration = builder.Build();
        }

        /// <summary>
        /// 
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            }
        }
    }
}
