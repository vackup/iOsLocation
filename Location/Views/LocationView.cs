using System;

using UIKit;
using Location.Core.ViewModels;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;

namespace Location
{
	public partial class LocationView : MvxViewController<LocationViewModel>
	{
		public LocationView () : base ("LocationView", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.

			this.CreateBinding(lblAltitude).For(label => label.Text).To((LocationViewModel vm) => vm.Altitude).Apply();
			this.CreateBinding(lblLongitude).To((LocationViewModel vm) => vm.Longitude).Apply();
			this.CreateBinding(lblLatitud).To((LocationViewModel vm) => vm.Latitude).Apply();
			this.CreateBinding(lblCourse).To((LocationViewModel vm) => vm.Course).Apply();
			this.CreateBinding(lblSpeed).To((LocationViewModel vm) => vm.Speed).Apply();
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


