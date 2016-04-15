using SQLite.Net.Interop;

namespace Location
{
    public class Configuration : IConfiguration
    {
        public string DbPath { get ; set; }
        public ISQLitePlatform SQLitePlatform { get; set; }
    }
}