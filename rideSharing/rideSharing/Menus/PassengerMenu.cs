using System;
using System.Collections.Generic;
using rideSharing.RideRequestSystem;
using RideSharing;
namespace rideSharing.Menus
{
    //Displays thh passanger menu
    public class PassengerMenu
    {
        public static void PassengerMainMenu(Passenger passenger)
        {

            string option;

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
                        List<string> locations = new List<string> { "CENTURION", "PRETORIA", "JHB", "HATFIELD", "MIDRAND" };
                        passenger.requestRide(locations);
                        break;
                    case "2":
                        Console.WriteLine($"This is your Balance:{passenger.WalletBalance}");
                        break;
                    case "3":
                        Console.WriteLine("Please enter how much you want to add:");
                        double amount = Convert.ToDouble(Console.ReadLine());
                        passenger.AddFunds(amount);
                        break;
                    case "4":
                        passenger.DisplayTripHistory(passenger);
                        break;
                    case "5":
                        //Console.WriteLine("Please select the driver you want to rate:");
                        Driver selectedDriver =new Driver("John","John@example.com","password123","Toyta","hefu");
                        Console.WriteLine("Please enter start between 1-5");
                        int stars = Convert.ToInt32(Console.ReadLine());

                        RideSystem.rateDriver( passenger ,selectedDriver,stars);
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
