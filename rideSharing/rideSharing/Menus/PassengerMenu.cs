using System;
using System.Collections.Generic;

namespace rideSharing.Menus
{
    //Displays thh passanger menu
    public static class PassengerMenu
    {
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
        public static void PassengerMainMenu()
        {

            string option;
            List<string> Locations = new List<string>()
            {
              "CENTURION",
              "PRETORIA",
              "JHB",
              "HATFIELD",
              "MIDRAND",
            };

            do
            {
                Console.WriteLine("Please pick an option:");
                Console.WriteLine("1.Request a Ride (Enter pickup & drop-off location");
                Console.WriteLine("2.View Wallet Balance?");
                Console.WriteLine("3.Add Funds to your wallet?");
                Console.WriteLine("4.View your ride history");
                Console.WriteLine("5.Rate a Driver");
                Console.WriteLine("0.Exit");

                option = Console.ReadLine();
                Console.WriteLine("===========================");
                Console.WriteLine($"You picked option {option}");
                switch (option)
                {
                    case "1":
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
                        break;
                    case "2":
                        string walletBalance = Console.ReadLine();

                        break;
                    case "3":
                        int addFund = Convert.ToInt32(Console.ReadLine());
                        break;
                    case "4":
                        string viewHistory = Console.ReadLine();
                        break;
                    case "5":
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Please enter valid option");
                        break;
                }
            } while (option != "0");
        }

    }
}
