using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLite.Net.Attributes;

namespace Location.Models
{
    [Table("deviceLocation")]
    public class DeviceLocation : Entity
    {
        public double Altitude { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Course { get; set; }
        public double Speed { get; set; }
    }
}
