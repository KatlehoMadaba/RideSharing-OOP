using System;
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
                        passenger.requestRide();
                        break;
                    case "2":
                        string walletBalance = Console.ReadLine();
                        Console.WriteLine($"This is your Balance:{Passenger.WalletBalance}");
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
