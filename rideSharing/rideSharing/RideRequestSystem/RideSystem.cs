using System;
using System.Collections.Generic;
using RideSharing;

namespace rideSharing.RideRequestSystem
{
    public class RideSystem
    {

        internal static void RequestRide(Passenger passenger1, object passenger2, string pickUp, string dropOff)
        {
            throw new NotImplementedException();
        }

        //public static void requestRide(string pickup, string dropoff)
        //{
        //    Console.WriteLine($"request a ride from {pickup} to {dropoff}");
        //}

        private static bool IsValidLocation(string locationInput, List<string> locations)
        {
            if (!locations.Contains(locationInput))
            {
                Console.WriteLine($"Error:{locationInput} is a invalid location please try again");
                return false;

            }
            return true;
        }

        public static void rateDriver(Passenger passenger, Driver driver, int stars)
        {
            if (stars < 1 || stars > 5)
            {
                Console.WriteLine("This is an invalid rating your number must be between 1-5");
                return;
            }
            else
            {
                Console.WriteLine($"Thank you for raring your driver {stars} stars");
                driver.Ratings.Add(stars);
            }

        }

        public void TripHistory(Passenger passenger, Driver driver, string pickUp, string dropOff, string distance, double tripCost)
        {

        }
    }
}
