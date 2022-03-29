using ConfigurationDemo.BaseConfig;
using System;

namespace DotNETCodeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //BaseConfigTest.ReadTest();
            //BaseConfigTest.OptionTest();
            //BaseConfigTest.OptionTest_HotChange();
            //BaseConfigTest.OptionDITest();
            //BaseConfigTest.DITest_HotChange();
            BaseConfigTest.ReadXmlTest();
            SectionTest.GetTest();
            //BaseConfigTest.OptionDITest();
            Console.ReadLine();
        }
    }
}
