using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyConfigurationDemo.BaseConfig
{
    public class BaseConfigTest
    {
        public static void TestGetSet()
        {

            var dicData = new Dictionary<string, string>() { { "Key1", "Value1" }, { "Key2", "Value2" } };

            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                             .Add(new MemoryConfigurationSource() { InitialData = dicData }) // 添加配置源   
                           .Build(); // 构建配置
            Console.WriteLine("Key1=" + configurationRoot["Key1"]);//输出内容 Key1=Value1 

        }
    }
}
