using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace RideSharing
{
    //Management of the users onboarding operations 
    class UserManger
    {
        //private List<User>userList= new List<User>();
        public List<User> userList = new List<User>();
        private const string FilePath = "users.json";


        public UserManger() 
        {
            //Loading users from json file when program starts
            loadUsers();
        }
        public void saveUsers()
        {
            var jsonData = JsonConvert.SerializeObject(userList);
            File.WriteAllText(FilePath, jsonData);
        }

        private void loadUsers()
        {
            if (File.Exists(FilePath)) 
            { 
                var jsonData= File.ReadAllText(FilePath);
                //loading the userList with data from the file 
                userList=JsonConvert.DeserializeObject<List<User>>(jsonData);
            }
        }

        public void registerPassenger(string username, string email, string password)
        {

            //validating if user already exists in the list
            if (userList.Any(user => user.Username == username))
            {
                Console.WriteLine("Username already exists");
                return;
            }
            else
            {
                //adds user to the list 
                var passenger = new Passenger(username, email, password);
                userList.Add(passenger);
                //saves to the json
                saveUsers();
                Console.WriteLine("You have been registered!");
            }
            
        }
        public void registerDriver(string username, string email, string password, string car, string noPlate)
        {
            //validating if user already exists in the list
            if (userList.Any(user => user.Username == username))
            {
                Console.WriteLine("Username already exists");
                return;
            }
            else
            {
                //adds user as an object to the user list 
                var driver = new Driver(username, email, password, car, noPlate);
                userList.Add(driver);
                //saves to the json
                saveUsers() ;
                Console.WriteLine("You have been registered!");
            }
        }

        public User Login(string username, string password)
        {
            //u.Login is used as a condition for a matching user 
            //You are iterating through a list of type User
            //And the function Login returns an object of type User 
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
