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
            BaseConfigTest.DIOption_Test();
            SectionTest.GetTest();
            //BaseConfigTest.OptionDITest();
            Console.ReadLine();
        }
    }
}
