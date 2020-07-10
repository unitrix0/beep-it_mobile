using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Mobile
{
    public class AppSettingsManager
    {
        private static AppSettingsManager _instance;
        private readonly JObject _secrets;

        private const string Namespace = "Mobile";
        private const string Filename = "appsettings.json";

        public static AppSettingsManager Settings => _instance ?? (_instance = new AppSettingsManager());

        public AppSettingsManager()
        {
            var assembly = typeof(AppSettingsManager).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream($"{Namespace}.{Filename}");
            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                _secrets = JObject.Parse(json);
            }
        }

        public string this[string name] => GetSetting(name);

        private string GetSetting(string name)
        {
            try
            {
                var path = name.Split(':');

                JToken node = _secrets[path[0]];
                if (node == null) return string.Empty;
                for (int index = 1; index < path.Length; index++)
                {
                    node = node[path[index]];
                    if (node == null) break;
                }

                return node?.ToString() ?? string.Empty;
            }
            catch (Exception)
            {
                Debug.WriteLine($"Unable to retrieve secret '{name}'");
                return string.Empty;
            }
        }
    }
}
