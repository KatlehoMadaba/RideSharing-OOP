using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace rideSharing.RideRequestSystem
{
    public static class RideSystem
    {
        public static void requestRide(string pickup, string dropoff)
        {
            Console.WriteLine($"request a ride from {pickup} to {dropoff}");
        }
    }
}
