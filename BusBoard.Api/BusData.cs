using System;
using Newtonsoft.Json;

namespace BusBoard.ConsoleApp.Properties
{
    public class BusData
    {
        public string VehicleID { get; set; }
        public string LineID { get; set; }
        public string StationName { get; set; }
        public string DestinationName { get; set; }
        public int TimeToStation { get; set; }
        public DateTime ExpectedArrival { get; set; }
        
            
        public int getTimeInMins()
        {
            return TimeToStation / 60;
        }
        
    }

}