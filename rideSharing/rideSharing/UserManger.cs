using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RideSharing
{
    //Management of the users onboarding operations 
    class UserManger
    {
        private List<User>userList= new List<User>();
        public void registerPassenger(string username, string email, string password)
        {
            //validating if user already exists in the list
            if (userList.Any(user => user.Username == username))
            {
                Console.WriteLine("Username already exists");
                return;
            }
            //adds user to the list 
            userList.Add(new Passenger(username, email, password));
            Console.WriteLine("You have been registered!");
        }
        public void registerDriver(string username, string email, string password, string car, string noPlate)
        {
            //validating if user already exists in the list
            if (userList.Any(user => user.Username == username))
            {
                Console.WriteLine("Username already exists");
                return;
            }
            //adds user as an object to the user list 
            userList.Add(new Driver(username, email, password, car, noPlate));
            Console.WriteLine("You have been registered!");
        }

        public User Login(string username, string password)
        {
            //u.Login is used as a condition for a matching user 
            //Login in method called on each User object in the Users list and passing the username and password as arguments
            //The login method should return true if the username and password have a match for that user or false
            var user = userList.FirstOrDefault(u => u.Login(username, password));
            if (user != null)
            {
                Console.WriteLine($"Welcome {username}");
                return user;
            }
            else
            {
                Console.WriteLine("This user does not exist or this password is incorrect");
                return null;
            }
        }


    }
}
