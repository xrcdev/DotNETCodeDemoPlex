using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationDemo.Model
{
    internal class WeatherOption
    {

        public string City { get; set; }

        /// <summary>
        /// ℃ or ℉
        /// </summary>
        public string UnitKind { get; set; } = "℃";
    }
}
