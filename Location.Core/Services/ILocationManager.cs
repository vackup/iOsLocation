using System;

namespace Location
{
    public interface ILocationManager
    {
        event EventHandler<LocationUpdatedEventArgs> LocationUpdated;
        //CLLocationManager LocMgr { get; }
        void StartLocationUpdates ();
        void PrintLocation (object sender, LocationUpdatedEventArgs e);
    }
}