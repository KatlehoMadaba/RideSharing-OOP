using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RideSharing;

namespace rideSharing.UserManagement.DriverMangement
{
    public class DriverMangement
    {
        public void registerDriver(string username, string email, string password, string car, string noPlate, string currentLocation)
        {
            //validating if user already exists in the list
            if (User.userList.Any(user => user.Username == username))
            {
                Console.WriteLine("Username already exists");
                return;
            }
            else
            {
                //adds user as an object to the user list 
                var driver = new Driver(username, email, password, car, noPlate, currentLocation);
                User.userList.Add(driver);
                //updates the json
                UserManger.Instance.UpdateUserData();
                Console.WriteLine("You have been registered!");
            }
        }

    }

   
}
