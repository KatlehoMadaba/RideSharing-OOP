using System;
using System.Collections.Generic;
using rideSharing.Menus;
using rideSharing.RideRequestSystem;

namespace RideSharing
{
    public class Passenger : User, IRideable
    {
        public static double WalletBalance { get; set; }

        static List<string> Locations = new List<string>()
            {
              "CENTURION",
              "PRETORIA",
              "JHB",
              "HATFIELD",
              "MIDRAND",
            };

        public Passenger(string username, string email, string password) : base(username, password, email)
        {
            Role = "Passenger";
            WalletBalance = 0.00;
        }

        //Checking for valid location input
        private static bool IsValidLocation(string locationInput, List<string> locations)
        {
            if (!locations.Contains(locationInput))
            {
                Console.WriteLine($"Error:{locationInput} is a invalid location please try again");
                return false;

            }
            return true;
        }
        public void requestRide()
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
                Console.WriteLine($"Ride request was succefull from {pickUp} to {dropOff} this is the available driver that will pick you up:");
                validRideRequest = true;
                //Display list of drivers that are avaliable and use random function to pickone to come pick them up
            }
        }
    

    public override void DisplayMenu()
        {
            PassengerMenu.PassengerMainMenu(this);
        }

    }
}