using System;
using Location.Models;

namespace Location
{
    public class LocationUpdatedEventArgs : EventArgs
    {
        DeviceLocation location;

        public LocationUpdatedEventArgs(DeviceLocation location)
        {
            this.location = location;
        }

        public DeviceLocation Location
        {
            get { return location; }
        }
    }
}