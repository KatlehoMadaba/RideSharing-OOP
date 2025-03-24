using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RideSharing.Menus;
namespace RideSharing
{
    public static class  Program
    {
        static void Main(string[] args)
        {
            UserManger userManger = new UserManger();

            string option;
            do
            {
                Console.WriteLine("====Welcome to ride sharing===");
                MainMenu.DisplayMainMenu();
                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        MainMenu.DisplayRegPassengerMenu(userManger);
                        break;
                    case "2":
                        MainMenu.DisplayRegDriverMenu(userManger);
                        break;
                    case "3":
                        MainMenu.DisplayLoginMenu(userManger);
                        break;
                    case "0":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Please enter a valid option");
                        break;
                }
            }
            while (option != "0");


        }
    }
}
