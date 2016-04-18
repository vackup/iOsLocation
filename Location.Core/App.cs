using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Location.Core.ViewModels;
using Location.Dao;
using Location.Models;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace Location.Core
{
    public class App : MvxApplication
    {
        public App()
        {
        }

        public override async void Initialize()
        {
            InitializeContainers();

            await SetupDataBase();
        }

        private static void InitializeContainers()
        {
            Mvx.LazyConstructAndRegisterSingleton<IRepository<DeviceLocation>, Repository<DeviceLocation>>();
            Mvx.RegisterType<IDatabase, Database>();

            Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<LocationViewModel>());
        }

        private async Task SetupDataBase()
        {
            var cancellationTokenSource = new CancellationTokenSource();

            var context = Mvx.Resolve<IDatabase>();
            await context.InitializeSQLiteAsync(cancellationTokenSource.Token);
        }
    }
}
