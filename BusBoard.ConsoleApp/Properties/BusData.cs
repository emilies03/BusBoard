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
        public decimal TimeToStationInMin { get; set; }
        public DateTime ExpectedArrivalTime { get; set; }

        [JsonConstructor]
        public BusData(string vehicleId, string lineId,string stationName, string destinationName, string timeToStation,
            string expectedArrival)
        {
            VehicleID = vehicleId;
            LineID = lineId;
            StationName = stationName;
            DestinationName = destinationName;
            TimeToStationInMin = (int.Parse(timeToStation))/60;
            ExpectedArrivalTime = Convert.ToDateTime(expectedArrival);

        }
    }
}