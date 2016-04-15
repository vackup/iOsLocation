﻿using System;
using CoreLocation;
using MvvmCross.iOS.Views;
using UIKit;

namespace Location.Views
{
	public partial class LocationView : MvxViewController
    {
		#region Computed Properties
		public static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public static ILocationManager Manager { get; set;}
		#endregion

		#region Constructors
		public LocationView (ILocationManager locationManager) : base ("LocationView", null)
		{
			// As soon as the app is done launching, begin generating location updates in the location manager
			Manager = locationManager;
			Manager.StartLocationUpdates();
		}
		#endregion

		#region Override Methods
		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
        
		#endregion

		#region Public Methods
		public void HandleLocationChanged (object sender, LocationUpdatedEventArgs e)
		{
			// Handle foreground updates
			var location = e.Location;

			lblAltitude.Text = location.Altitude + " meters";
			lblLongitude.Text = location.Longitude.ToString ();
			lblLAtitude.Text = location.Latitude.ToString ();
			lblCourse.Text = location.Course.ToString ();
			lblSpeed.Text = location.Speed.ToString ();

			Console.WriteLine ("foreground updated");
		}

		#endregion

		#region View Lifecycle
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.

			// It is better to handle this with notifications, so that the UI updates
			// resume when the application re-enters the foreground!
			//Manager.LocationUpdated += HandleLocationChanged;

			// Screen subscribes to the location changed event
			UIApplication.Notifications.ObserveDidBecomeActive ((sender, args) => {
				Manager.LocationUpdated += HandleLocationChanged;
			});

			// Whenever the app enters the background state, we unsubscribe from the event 
			// so we no longer perform foreground updates
			UIApplication.Notifications.ObserveDidEnterBackground ((sender, args) => {
				Manager.LocationUpdated -= HandleLocationChanged;
			});
		}
		#endregion

	}
}
