// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Location
{
	[Register ("LocationView")]
	partial class LocationView
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblAltitude { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblCourse { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblLatitud { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblLongitude { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblSpeed { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (lblAltitude != null) {
				lblAltitude.Dispose ();
				lblAltitude = null;
			}
			if (lblCourse != null) {
				lblCourse.Dispose ();
				lblCourse = null;
			}
			if (lblLatitud != null) {
				lblLatitud.Dispose ();
				lblLatitud = null;
			}
			if (lblLongitude != null) {
				lblLongitude.Dispose ();
				lblLongitude = null;
			}
			if (lblSpeed != null) {
				lblSpeed.Dispose ();
				lblSpeed = null;
			}
		}
	}
}
