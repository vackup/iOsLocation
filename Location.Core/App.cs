using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Mvx.LazyConstructAndRegisterSingleton<IRepository<DeviceLocation>, Repository<DeviceLocation>>();
            Mvx.RegisterType<IDatabase, Database>();

            Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<LocationViewModel>());
        }
    }
}
