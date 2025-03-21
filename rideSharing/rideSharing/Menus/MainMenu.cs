using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RideSharing;

namespace RideSharing.Menus
{

    public static class MainMenu
    {

        public static void DisplayMainMenu()
        {
            Console.WriteLine("1.Enter number 1 to register as Passanger:");
            Console.WriteLine("2.Enter number 2 to register as Driver:");
            Console.WriteLine("3.Login");
            Console.WriteLine("0.Exit");
        }
        public static void DisplayRegPassengerMenu(UserManger userManger)
        {
            Console.WriteLine("Please enter your username:");
            string username = Console.ReadLine();
            Console.WriteLine("Please your email:");
            string email = Console.ReadLine();
            Console.WriteLine("Please your password:");
            string password = Console.ReadLine();
            userManger.registerPassenger(username, email, password);
        }

        public static void DisplayRegDriverMenu(UserManger userManger)
        {
            Console.WriteLine("Please enter your username");
            string username = Console.ReadLine();
            Console.WriteLine("Please your email:");
            string email = Console.ReadLine();
            Console.WriteLine("Please your password:");
            string password = Console.ReadLine();
            Console.WriteLine("Please enter name of car");
            string car = Console.ReadLine();
            Console.WriteLine("Please enter number plate");
            string noPlate = Console.ReadLine();
            userManger.registerDriver(username, email, password, car, noPlate);
        }

        public static void DisplayLoginMenu(UserManger userManger)
        {
            Console.WriteLine("Enter username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter password:");
            string email = Console.ReadLine();
            var user = userManger.Login(username, email);
            if (user != null)
            {
                if (user is Passenger)
                {
                    Console.WriteLine("Welcome Passanger.");
                    //Passenger menu

                }
                else if (user is Driver)
                {
                    Console.WriteLine("Welcome Driver");
                    //Driver menu
                }
            }
        }

    }
}
