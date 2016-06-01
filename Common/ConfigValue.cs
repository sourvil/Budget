using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class ConfigValue
    {
        private static string _resourceServerUrl = ReadConfigValue("resourceServerUrl");

        public static string resourceServerUrl { get { return _resourceServerUrl; } }

        private static string ReadConfigValue(string key)
        {
            try
            {
                return System.Configuration.ConfigurationSettings.AppSettings[key].ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
