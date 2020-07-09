using System;
using Newtonsoft.Json;

namespace BusBoard.ConsoleApp.Properties
{
    public class BusData
    {
        private string VehicleID { get; set; }
        private string LineID { get; set; }
        private string DestinationName { get; set; }
        private int TimeToStation { get; set; }
        private DateTime ExpectedArrivalTime { get; set; }

        [JsonConstructor]
        public BusData(string vehicleId, string lineId, string destinationName, string timeToStation,
            string expectedArrivalTime)
        {
            VehicleID = vehicleId;
            LineID = lineId;
            DestinationName = destinationName;
            TimeToStation = int.Parse(timeToStation);
            ExpectedArrivalTime = Convert.ToDateTime(expectedArrivalTime);
        }
    }
}