using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Location.Models;
using SQLite.Net;
using SQLite.Net.Async;

namespace Location.Dao
{
    public class Database : IDatabase
    {
        private readonly IConfiguration _configuration;

        #region Declarations
        private const string DatabaseTablesNamespace = "Database.Tables";

        private SQLiteAsyncConnection _connection;
        private static bool _isInitialized;

        #endregion

        #region Properties

        public static bool IsInitialized
        {
            get { return _isInitialized; }
            private set { _isInitialized = value; }
        }

        #endregion

        #region Constructors

        // static constructor
        public Database(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        #region Public Methods

        public SQLiteAsyncConnection GetOrCreateConnection()
        {
            if (!Database.IsInitialized)
            {
                throw new Exception("Database is not initialized.");
            }

            this._connection = _connection ?? CreateSqliteAsyncConnection();
            return this._connection;
        }

        public async Task InitializeSQLiteAsync(CancellationToken cancellationToken)
        {
            if (Database.IsInitialized)
            {
                throw new Exception("Database context has already been initialized");
            }


            var dbConnection = CreateSqliteAsyncConnection();
            var tablesToCreate = GetTableTypes();
            var currentVersion = 1;
            var nextVersion = 1;

            var toCreate = tablesToCreate as Type[] ?? tablesToCreate.ToArray();
            await RunDatabasePreDeploymentAsync(
                dbConnection,
                toCreate,
                currentVersion,
                nextVersion,
                cancellationToken);

            await RunDatabaseDeploymentAsync(
                dbConnection,
                toCreate,
                currentVersion,
                nextVersion,
                cancellationToken);

            await RunDatabasePostDeploymentAsync(
                dbConnection,
                toCreate,
                currentVersion,
                nextVersion,
                cancellationToken);

            Database.IsInitialized = true;
        }

        public async Task DeleteAllDataAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var dbConnection = GetOrCreateConnection();

            const string query = "DELETE FROM {0}";
            var tables = GetTableTypes();

            foreach (var item in tables)
            {
                await dbConnection.ExecuteAsync(string.Format(query, item.Name));
            }

        }



        #endregion

        #region Private Methods

        private SQLiteAsyncConnection CreateSqliteAsyncConnection()
        {
            var sqliteFilename = "MyDatabase.db3";
            var libraryPath = _configuration.DbPath;

//#if __ANDROID__
//				string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); ;
//#else
//            // we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
//            // (they don't want non-user-generated data in Documents)
//            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
//            string libraryPath = Path.Combine(documentsPath, "../Library/");
//#endif
            var sqLitePath = Path.Combine(libraryPath, sqliteFilename);

            var connectionString = new SQLiteConnectionString(sqLitePath, true);
            var sqLiteConnectionPool = new SQLiteConnectionPool(_configuration.SQLitePlatform);

            Debug.WriteLine(string.Format("sqlitePath: {0}", sqLitePath));

            return new SQLiteAsyncConnection(() =>
                sqLiteConnectionPool.GetConnection(connectionString));
        }

        private async Task
            RunDatabaseDeploymentAsync(SQLiteAsyncConnection dbConnection,
                IEnumerable<Type> tablesToCreate,
                int currentVersion,
                int nextVersion,
                CancellationToken cancellationToken)
        {
            var isConstructed = await DatabaseTablesExist(dbConnection);
            if (isConstructed)
            {
                return;
            }

            await dbConnection.CreateTablesAsync(tablesToCreate.ToArray());
        }

        private async Task<bool> RunDatabasePreDeploymentAsync(SQLiteAsyncConnection dbConnection,
            IEnumerable<Type> tablesToCreate,
            int currentVersion,
            int nextVersion,
            CancellationToken cancellationToken)
        {
            return await Task.Factory.StartNew(() => true, cancellationToken);
        }

        private async Task<bool> RunDatabasePostDeploymentAsync(SQLiteAsyncConnection dbConnection,
            IEnumerable<Type> tablesToCreate,
            int currentVersion,
            int nextVersion,
            CancellationToken cancellationToken)
        {
            return await Task.Factory.StartNew(() => true, cancellationToken);
        }

        private static IEnumerable<Type> GetTableTypes()
        {
            yield return typeof(DeviceLocation);
        }

        private async Task<bool> DatabaseTablesExist(SQLiteAsyncConnection dbConnection)
        {
            const string query = "SELECT count(*) FROM sqlite_master WHERE type='table' AND name='scalar'";
            var result = await dbConnection.ExecuteScalarAsync<int>(query);
            return result == 1;
        }

        private IEnumerable<Type> TryGetTableTypes(Assembly assembly)
        {
            try
            {
                return assembly.DefinedTypes
                    .Where(m => m.Namespace == DatabaseTablesNamespace)
                    .Select(m => m.AsType());
            }
            catch (ReflectionTypeLoadException exception)
            {
                return new Type[0];
            }
        }

        private IEnumerable GeneratePreDeploymentObjects(IList<Type> mappedTypes)
        {
            yield break;
        }

        private IEnumerable GeneratePostDeploymentObjects(IList<Type> mappedTypes)
        {
            yield break;
        }

        #endregion

    }
}