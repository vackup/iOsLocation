using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Location.Models;
using SQLite;

namespace Location.Dao
{
    public class Database : SQLiteConnection
    {
        protected static Database me = null;
        protected static string dbLocation;

        static object locker = new object();

        /// <summary>
        /// Initializes a new instance of the MwcDatabase. 
        /// if the database doesn't exist, it will create the database and all the tables.
        /// </summary>
        /// <param name='path'>
        /// Path.
        /// </param>
        public Database(string path) : base(path)
        {
            CreateTable<DeviceLocation>();
        }

        static Database()
        {
            // set the db location
            dbLocation = DatabaseFilePath;

            // instantiate a new db
            me = new Database(dbLocation);
        }

        public static string DatabaseFilePath
        {
            get
            {
                var sqliteFilename = "MyDatabase.db3";

#if __ANDROID__
				string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); ;
#else
                // we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
                // (they don't want non-user-generated data in Documents)
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
                string libraryPath = Path.Combine(documentsPath, "../Library/");
#endif
                var path = Path.Combine(libraryPath, sqliteFilename);

                return path;
            }
        }

        public static IEnumerable<T> GetItems<T>() where T : Models.IEntity, new()
        {
            lock (locker)
            {
                return (from i in me.Table<T>() select i).ToList();
            }
        }

        public static T GetItem<T>(int id) where T : Models.IEntity, new()
        {
            lock (locker)
            {
                return (from i in me.Table<T>()
                        where i.KeyId == id
                        select i).FirstOrDefault();
            }
        }

        public static int SaveItem<T>(T item) where T : Models.IEntity
        {
            lock (locker)
            {
                if (item.KeyId != 0)
                {
                    me.Update(item);
                    return item.KeyId;
                }
                else
                {
                    return me.Insert(item);
                }
            }
        }

        public static void SaveItems<T>(IEnumerable<T> items) where T : IEntity
        {
            lock (locker)
            {
                me.BeginTransaction();

                foreach (T item in items)
                {
                    SaveItem<T>(item);
                }

                me.Commit();
            }
        }

        //public static int DeleteAllItems<T>(int id) where T : IEntity, new()
        //{
        //    lock (locker)
        //    {
        //        return me.DeleteAll<T>();
        //    }
        //}

        public static int DeleteItem<T>(int id) where T : IEntity, new()
        {
            lock (locker)
            {
                return me.Delete<T>(new T() { KeyId = id });
            }
        }

        public static void ClearTable<T>() where T : Models.IEntity, new()
        {
            lock (locker)
            {
                me.Execute(string.Format("delete from \"{0}\"", typeof(T).Name));
            }
        }

        // helper for checking if database has been populated
        public static int CountTable<T>() where T : Models.IEntity, new()
        {
            lock (locker)
            {
                string sql = string.Format("select count (*) from \"{0}\"", typeof(T).Name);
                var c = me.CreateCommand(sql, new object[0]);
                return c.ExecuteScalar<int>();
            }
        }

        //public static IEnumerable<Page> GetPagesByCategory(string category)
        //{
        //    return me.Query<Page>(string.Format("SELECT * FROM {0} WHERE Category = ?", typeof(Page).Name), category);
        //}
    }
}