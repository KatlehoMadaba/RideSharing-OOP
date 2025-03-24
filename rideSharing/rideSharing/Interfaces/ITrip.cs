using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RideSharing;

namespace rideSharing.RideRequestSystem
{
    public interface ITrip
    {
        Driver Driver { get; set; }
        Passenger Passenger { get; set; }
        string PickUp { get; set; }
        string DropOff { get; set; }
        double Distance { get; set; }
        double Cost { get; set; }
        string Status {  get; set; }
    }
}
