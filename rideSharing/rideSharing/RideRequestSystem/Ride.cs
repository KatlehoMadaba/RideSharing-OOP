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

        public double TripCost { get; set; }

        public double RatePerKm { get; set; }

        public double Distance { get; set; }



        Random random = new Random();
        public Ride(Passenger passenger, Driver driver, string pickupLocation, string dropOffLocation)
        {
            DropOffLocation = dropOffLocation;
            PickupLocation = pickupLocation;
            Passenger = passenger;
            Driver = driver;
            RatePerKm = 10.00;
            Distance = random.Next(5, 101);// Random distance between 5 and 100 km
            TripCost=Distance*RatePerKm;
        }
        
    }
}
