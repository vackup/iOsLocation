using System;
using CoreLocation;
using Location.Dao;
using Location.Models;
using UIKit;

namespace Location
{
    public class LocationManager : ILocationManager
    {
		protected CLLocationManager locMgr;

		public event EventHandler<LocationUpdatedEventArgs> LocationUpdated = delegate { };

		public LocationManager ()
		{
			this.locMgr = new CLLocationManager ();
			this.locMgr.PausesLocationUpdatesAutomatically = false;

			// iOS 8 has additional permissions requirements
			if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
				locMgr.RequestAlwaysAuthorization (); // works in background
				//locMgr.RequestWhenInUseAuthorization (); // only in foreground
			}

			// iOS 9 requires the following for background location updates
			// By default this is set to false and will not allow background updates
			if (UIDevice.CurrentDevice.CheckSystemVersion (9, 0)) {
				locMgr.AllowsBackgroundLocationUpdates = true;
			}

			LocationUpdated += PrintLocation;

		}

		public CLLocationManager LocMgr {
			get { return this.locMgr; }
		}

		public void StartLocationUpdates ()
		{

			// We need the user's permission for our app to use the GPS in iOS. This is done either by the user accepting
			// the popover when the app is first launched, or by changing the permissions for the app in Settings
			if (CLLocationManager.LocationServicesEnabled) {

				//set the desired accuracy, in meters
				LocMgr.DesiredAccuracy = 1;

				LocMgr.LocationsUpdated += (object sender, CLLocationsUpdatedEventArgs e) => {
                    // fire our custom Location Updated event

                    var location  = e.Locations[e.Locations.Length - 1];

                    var deviceLocation = new DeviceLocation
                    {
                        Altitude = location.Altitude,
                        Longitude = location.Coordinate.Longitude,
                        Latitude = location.Coordinate.Latitude,
                        Course = location.Course,
                        Speed = location.Speed
                    };

                    LocationUpdated (this, new LocationUpdatedEventArgs (deviceLocation));
				};

				LocMgr.StartUpdatingLocation ();
			}
		}

		//This will keep going in the background and the foreground
		public void PrintLocation (object sender, LocationUpdatedEventArgs e)
		{
			
			var location = e.Location;
			Console.WriteLine ("Altitude: " + location.Altitude + " meters");
			Console.WriteLine ("Longitude: " + location.Longitude);
			Console.WriteLine ("Latitude: " + location.Latitude);
			Console.WriteLine ("Course: " + location.Course);
			Console.WriteLine ("Speed: " + location.Speed);

		    //Database.SaveItem(deviceLocation);
		}
	}
}

