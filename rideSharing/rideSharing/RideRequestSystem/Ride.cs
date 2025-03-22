using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RideSharing;

namespace rideSharing.RideRequestSystem
{
    class Ride
    {

        public Passenger Passenger { get; set; }

        public Driver Driver { get; set; }

        public List<string> PickupLocations { get; set; }

        public List<string> DropOffLocatiosns { get; set; }

        public string PickupLocation { get; set; }

        public string DropOffLocation { get; set; }

        public Ride(Passenger passenger,Driver driver ,string pickupLocation, string dropOffLocation)
        {
            DropOffLocation = dropOffLocation;
            PickupLocation = pickupLocation;
            Passenger = passenger;
            Driver = driver;
        }

    }
}
