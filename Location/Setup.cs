using System;
using System.IO;
using Location.Core;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Platform;

namespace Location
{
    public class Setup : MvxIosSetup
    {
        public Setup(MvxApplicationDelegate appDelegate, IMvxIosViewPresenter presenter)
            : base(appDelegate, presenter)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            InitializeContainers();

            SetupConfiguration();

            return new App();
        }

        private void SetupConfiguration()
        {
            var configuration = Mvx.Resolve<IConfiguration>();

            // we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
            // (they don't want non-user-generated data in Documents)
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            configuration.DbPath = Path.Combine(documentsPath, "../Library/");

            configuration.SQLitePlatform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
        }

        private void InitializeContainers()
        {
            Mvx.LazyConstructAndRegisterSingleton<IConfiguration, Configuration>();
            Mvx.RegisterType<ILocationManager, LocationManager>();
        }
    }
}