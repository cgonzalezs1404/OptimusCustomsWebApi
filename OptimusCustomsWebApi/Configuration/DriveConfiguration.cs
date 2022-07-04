using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OptimusCustomsWebApi.Configuration
{
    public class DriveConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        private static object lockObject = new object();
        /// <summary>
        /// 
        /// </summary>
        private static DriveConfiguration instance;
        /// <summary>
        /// 
        /// </summary>
        private IConfiguration configuration;

        /// <summary>
        /// 
        /// </summary>
        public static DriveConfiguration Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new DriveConfiguration();
                    }
                    return instance;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private DriveConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("credentials.json",
                optional: true,
                reloadOnChange: true);
            configuration = builder.Build();
        }
    }
}
