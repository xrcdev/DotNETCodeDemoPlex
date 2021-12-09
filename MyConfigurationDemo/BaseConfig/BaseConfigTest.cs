using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
//using Microsoft.Extensions.Configuration.Binder;
//using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using ConfigurationDemo.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System.Diagnostics;
using Microsoft.Extensions.Options;
using System.Threading;

namespace ConfigurationDemo.BaseConfig
{
    /// <summary>
    /// basic test
    /// </summary>
    public class BaseConfigTest
    {
        public static void ReadTest()
        {
            var dicData = new Dictionary<string, string>() { { "Key1", "Value1" }, { "Key2", "Value2" } };

            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                             .Add(new MemoryConfigurationSource() { InitialData = dicData }) // 添加配置源   
                           .Build();
            Console.WriteLine("Key1=" + configurationRoot["Key1"]);

        }

        public static void BindTest()
        {

            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json") //
                .Build();
            var option = configurationRoot.GetSection("Weather").Get<WeatherOption>();
            Console.WriteLine("City:" + option.City);

        }

        /// <summary>
        /// 绑定模型,获取更新后的配置
        /// </summary>
        public static void BindTest_HotChange()
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) //
                .Build();
            var option = configurationRoot.GetSection("Weather").Get<WeatherOption>();
            Console.WriteLine("City:" + option.City);
            ChangeToken.OnChange(() => configurationRoot.GetReloadToken(), () =>
            {
                Debugger.Break();
                var option1 = configurationRoot.GetSection("Weather").Get<WeatherOption>();
                Console.WriteLine("City:" + option1.City);
            });
        }


        public static void DITest()
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) //
                .Build();

            var serviceProvider = new ServiceCollection()
              .AddSingleton<WeatherOption>(configurationRoot.GetSection("Weather").Get<WeatherOption>())
              .BuildServiceProvider();

            var option1 = serviceProvider.GetRequiredService<WeatherOption>();

            Console.WriteLine("City:" + option1.City);

        }


        /// <summary>
        /// 依赖注入和动态更新
        /// </summary>
        public static void DITest_HotChange()
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var serviceProvider = new ServiceCollection()
             //.AddSingleton<WeatherOption>(_ => configurationRoot.GetSection("Weather").Get<WeatherOption>())
             .AddTransient<WeatherOption>(_ => configurationRoot.GetSection("Weather").Get<WeatherOption>())
             .BuildServiceProvider();

            var option = serviceProvider.GetRequiredService<WeatherOption>();
            Console.WriteLine("City:" + option.City);

            ChangeToken.OnChange(() => configurationRoot.GetReloadToken(), () =>
               {
                   Debugger.Break();
                   var option1 = serviceProvider.GetRequiredService<WeatherOption>();
                   Console.WriteLine("after changed, City" + option1.City);//如果使用 AddSingleton ,则不会读取到新内容
               });
        }

        public static void DIOption_Test()
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) //
                .Build();

            var serviceProvider = new ServiceCollection()
                .Configure<WeatherOption>(configurationRoot.GetSection("Weather"))
                .BuildServiceProvider();

            //.AddOptions<WeatherOption>()
            //.Configure(options =>
            //{
            //    options.City = "";
            //})
            //.Validate(options =>  false, "Invalid Date or Time pattern.");


            var option = serviceProvider.GetRequiredService<IOptions<WeatherOption>>();
            Console.WriteLine($"City:{option.Value.City} By 'IOptions' ");

            var optionMonitor = serviceProvider.GetRequiredService<IOptionsMonitor<WeatherOption>>();
            Console.WriteLine($"City:{optionMonitor.CurrentValue.City} By 'IOptionsMonitor' ");
            ChangeToken.OnChange(() => configurationRoot.GetReloadToken(), () =>
              {
                  Console.WriteLine("Config Changed");
              });


            ChangeToken.OnChange(() => configurationRoot.GetReloadToken(), () =>
           {
               Debugger.Break();

               Console.WriteLine($"City:{option.Value.City} By 'IOptions' ");
               var option1 = serviceProvider.GetRequiredService<IOptions<WeatherOption>>();
               Console.WriteLine($"City:{option1.Value.City} By 'IOptions' Retrieve");

               Console.WriteLine($"City:{optionMonitor.CurrentValue.City} By 'IOptionsMonitor'  ");
               var optionMonitor1 = serviceProvider.GetRequiredService<IOptionsMonitor<WeatherOption>>();
               Console.WriteLine($"City:{optionMonitor1.CurrentValue.City} By 'IOptionsMonitor'  Retrieve");
               //var option3 = serviceProvider.GetRequiredService<WeatherOption>();
           });

        }


    }
}
