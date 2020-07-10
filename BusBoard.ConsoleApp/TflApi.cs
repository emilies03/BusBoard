using System;
using System.Collections.Generic;
using System.Linq;
using BusBoard.ConsoleApp.Properties;
using RestSharp;

namespace BusBoard.ConsoleApp
{
    public class TflApi
    {
        public IEnumerable<BusData> GetArrivingBusesList(string busStop)
        {
            var client = new RestClient("https://api.tfl.gov.uk");
            var request = new RestRequest($"StopPoint/{busStop}/Arrivals");
            List<BusData> listArrivingBusDataFromJSON = client.Execute<List<BusData>>(request).Data;
            var orderedList5ArrivingBusData =
                (listArrivingBusDataFromJSON.OrderBy(o => o.ExpectedArrival).ToList()).Take(5);

            return orderedList5ArrivingBusData;
        }


        public IEnumerable<BusStop> GetBusStopsFromCoordinates(string longitude, string latitude)
        {
            
            
            var client = new RestClient("https://api.tfl.gov.uk/");
            var request = new RestRequest($"Stoppoint?lat={latitude}&lon={longitude}&stoptypes=NaptanBusCoachStation,NaptanPublicBusCoachTram");

            BusStopResponse busStopResponseFromServer = client.Execute<BusStopResponse>(request).Data;

            var closestTwoBusStops = busStopResponseFromServer.StopPoints.Take(2);
            
            return closestTwoBusStops;
        }
        

    }
}