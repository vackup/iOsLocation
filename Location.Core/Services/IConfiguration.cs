using SQLite.Net.Interop;

namespace Location
{
    public interface IConfiguration
    {
        string DbPath { get; set; }
        ISQLitePlatform SQLitePlatform { get; set; }
    }
}