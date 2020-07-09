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
        public DateTime ExpectedArrivalTime { get; set; }

        [JsonConstructor]
        public BusData(string vehicleId, string lineId,string stationName, string destinationName, string timeToStation,
            string expectedArrivalTime)
        {
            VehicleID = vehicleId;
            LineID = lineId;
            StationName = stationName;
            DestinationName = destinationName;
            TimeToStation = int.Parse(timeToStation);
            ExpectedArrivalTime = Convert.ToDateTime(expectedArrivalTime);
        }
    }
}