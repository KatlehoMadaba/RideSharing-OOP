using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RideSharing;

namespace rideSharing.RideRequestSystem
{
    public class Ride : ITrip
    {

        public Passenger Passenger { get; set; }

        public Driver Driver { get; set; }

        public string PickUp { get; set; }

        public string DropOff { get; set; }

        public double TripCost { get; set; }

        public double RatePerKm { get; set; }

        public double Cost { get; set; }

        public double Distance { get; set; }

        private static readonly Random random = new Random();


        public override string ToString()
        {
            return $"Driver: {Driver.Username}, Passenger: {Passenger.Username}, From {PickUp} to {DropOff}, Distance: {Distance} km, Cost: {Cost:C}";
        }

        public Ride(Passenger passenger, Driver driver, string pickUp, string dropOff)
        {
            DropOff = dropOff;
            Driver = driver;
            PickUp = pickUp;
            Passenger = passenger;
            RatePerKm = 10.00;
            Distance = random.Next(5, 101);// Random distance between 5 and 100 km
            Cost = CalculateTripCost();
        }
        public double CalculateTripCost()
        {
            try
            {

            return Distance * RatePerKm;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred while calculating trip amount: + {ex.Message}");
                return 0;
            }
        }
        //have the locations be publicly accessiably to every member

        public static readonly List<string> ValidLocations = new List<string>
        {
            "CENTURION", "PRETORIA", "JHB", "HATFIELD", "MIDRAND"
        };


    }
}
