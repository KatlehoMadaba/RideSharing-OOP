using System;
using System.Collections.Generic;
using RideSharing;

namespace rideSharing.RideRequestSystem
{
    public class RideSystem
    {
        public List<string> Locations = new List<string>()
            {
              "CENTURION",
              "PRETORIA",
              "JHB",
              "HATFIELD",
              "MIDRAND",
            };

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
        public void RequestRide()
        {
            bool validRideRequest = false;
            while (!validRideRequest)
            {
                string pickUp = "";
                string dropOff = "";

                //Entering pickup location
                bool validPickUp = false;
                while (!validPickUp)
                {
                    Console.WriteLine("\nPlease choose your pick up location");
                    Console.WriteLine("===================================");
                    foreach (var location in Locations)
                    {
                        Console.WriteLine(location);
                    }
                    pickUp = Console.ReadLine().ToUpper();

                    if (IsValidLocation(pickUp, Locations))
                    {
                        validPickUp = true;

                    }
                }

                //Entering drop off location
                bool validDropOff = false;
                while (!validDropOff)
                {
                    Console.WriteLine("Please choose your dropoff location");
                    Console.WriteLine("====================================");
                    foreach (var location in Locations)
                    {

                        Console.WriteLine(location);

                    }
                    dropOff = Console.ReadLine().ToUpper();

                    if (!IsValidLocation(dropOff, Locations))
                    {
                        continue; //if input is invalid return the main menu
                    }
                    if (pickUp == dropOff)
                    {
                        Console.WriteLine("Sorry but your pickup and drop of location cannot be the same");
                        continue;//return the drop off menu again
                    }
                    validDropOff = true;//if input is valid and different 
                }

                //Successuful
                Console.WriteLine($"Ride request was successful from {pickUp} to {dropOff} this is the available driver that will pick you up:");
                validRideRequest = true;
                //Display list of drivers that are avaliable and use random function to pickone to come pick them up
            }
        }

    }
}
