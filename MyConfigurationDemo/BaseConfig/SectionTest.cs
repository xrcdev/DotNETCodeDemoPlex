using ConfigurationDemo.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationDemo.BaseConfig
{
    internal class SectionTest
    {
        public static void GetTest()
        {

            var source = new Dictionary<string, string>
            {
                 {"Microsoft:Extensions:Configuration:Ini","IniConfiguration" },
                 {"Microsoft:Extensions:Configuration:Json","JsonConfiguration" },
            };
            var source1 = new Dictionary<string, string>
            {
                 {"Microsoft:Extensions:Configuration:Xml","XmlConfiguration" },
                 {"Microsoft:Extensions:Configuration:Yml","YmlConfiguration" },
            };
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .Add(new MemoryConfigurationSource() { InitialData = source }) // 添加配置源
                .Add(new MemoryConfigurationSource() { InitialData = source1 }) // 添加配置源  
                 .Build();
            var configPlex = configurationRoot.GetSection("Microsoft:Extensions:Configuration").Get<ConfigurationPlex>();

            /*
             BindInstance
             
            root.Providers .Aggregate(Enumerable.Empty<string>(), (seed, source) => source.GetChildKeys(seed, path))
            BindProperty
             */
            Console.WriteLine("Ini =" + configPlex.Ini);
            Console.WriteLine("Xml =" + configPlex.Xml);

        }
    }
}
