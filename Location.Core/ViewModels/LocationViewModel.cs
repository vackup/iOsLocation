using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace Location.Core.ViewModels
{
    public class LocationViewModel : MvxViewModel
    {
        private readonly ILocationManager _locationManager;

        private double _speed;
        private double _course;
        private double _altitude;
        private double _longitude;
        private double _latitude;

        public LocationViewModel(ILocationManager locationManager)
        {
            // As soon as the app is done launching, begin generating location updates in the location manager
            _locationManager = locationManager;
            _locationManager.StartLocationUpdates();

            _locationManager.LocationUpdated += HandleLocationChanged;
        }

        private void HandleLocationChanged(object sender, LocationUpdatedEventArgs e)
        {
            // Handle foreground updates
            var location = e.Location;

            Altitude = location.Altitude;
            Longitude = location.Longitude;
            Latitude = location.Latitude;
            Course = location.Course;
            Speed = location.Speed;

			Mvx.TaggedTrace("LocationChanged", "LocationChanged", location);

            //Console.WriteLine("foreground updated");
        }

        public override void Start()
        {
            //Recalcuate();
            base.Start();
        }

        public double Altitude
        {
            get { return _altitude; }
            set
            {
                _altitude = value;
                RaisePropertyChanged(() => Altitude);
                //Recalcuate();
            }
        }

        public double Longitude
        {
            get { return _longitude; }
            set
            {
                _longitude = value;
                RaisePropertyChanged(() => Longitude);
                //Recalcuate();
            }
        }

        public double Latitude
        {
            get { return _latitude; }
            set
            {
                _latitude = value;
                RaisePropertyChanged(() => Latitude);
            }
        }

        public double Course
        {
            get { return _course; }
            set
            {
                _course = value;
                RaisePropertyChanged(() => Course);
            }
        }

        public double Speed
        {
            get { return _speed; }
            set
            {
                _speed = value;
                RaisePropertyChanged(() => Speed);
            }
        }

        //void Recalcuate()
        //{
        //    //Latitude = _locationManager.TipAmount(Altitude, Longitude);
        //}
    }
}