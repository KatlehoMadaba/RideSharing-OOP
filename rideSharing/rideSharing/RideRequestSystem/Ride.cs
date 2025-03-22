using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RideSharing;

namespace rideSharing.RideRequestSystem
{
    public class Ride
    {

        public Passenger Passenger { get; set; }

        public Driver Driver { get; set; }

        public List<string> Locations = new List<string>()
            {
              "CENTURION",
              "PRETORIA",
              "JHB",
              "HATFIELD",
              "MIDRAND",

        };


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
