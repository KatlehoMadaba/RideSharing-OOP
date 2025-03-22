using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rideSharing.RideRequestSystem;

namespace RideSharing
{
    public class Passenger:User , IRideable
    {
       

        public Passenger(string username, string email, string password) : base(username, password, email)
        {
            Role = "Passanger";
        }
        public void requestRide(string pick, string dropoff)
        {

        }
    }
}
