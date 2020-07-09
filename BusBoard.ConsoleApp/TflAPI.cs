using System.Collections.Generic;
using System.Linq;
using BusBoard.ConsoleApp.Properties;
using RestSharp;

namespace BusBoard.ConsoleApp
{
    public class TflAPI
    {
        public IEnumerable<BusData> GetArrivingBusesListFromServer(string userInput)
        {
            var client = new RestClient("https://api.tfl.gov.uk");
            var request = new RestRequest($"StopPoint/{userInput}/Arrivals");
            List<BusData> listArrivingBusDataFromJSON = client.Execute<List<BusData>>(request).Data;
            var orderedList5ArrivingBusData =
                (listArrivingBusDataFromJSON.OrderBy(o => o.ExpectedArrival).ToList()).Take(5);

            return orderedList5ArrivingBusData;
        }

    }
}