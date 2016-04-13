using SQLite;
using SQLite.Net.Attributes;

namespace Location.Models
{
    public abstract class Entity : IEntity
    {
        #region IEntity implementation
        [PrimaryKey, AutoIncrement]
        public int KeyId { get; set; }

        #endregion

        public Entity()
        {
        }
    }
}