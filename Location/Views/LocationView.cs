using System;
using CoreLocation;
using Location.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
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
		#endregion

		#region Constructors
		public LocationView () : base ("LocationView", null)
		{
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

            // TODO: do bindings

   //         this.CreateBinding(TipLabel).To((TipViewModel vm) => vm.Tip).Apply();
   //         this.CreateBinding(lblAltitude).To((LocationViewModel vm) => vm.Tip).Apply();

   //         // Screen subscribes to the location changed event
   //         UIApplication.Notifications.ObserveDidBecomeActive ((sender, args) => {
			//	Manager.LocationUpdated += HandleLocationChanged;
			//});

			//// Whenever the app enters the background state, we unsubscribe from the event 
			//// so we no longer perform foreground updates
			//UIApplication.Notifications.ObserveDidEnterBackground ((sender, args) => {
			//	Manager.LocationUpdated -= HandleLocationChanged;
			//});
		}
		#endregion

	}
}

