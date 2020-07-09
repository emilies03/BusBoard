using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BusBoard.ConsoleApp.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;

namespace BusBoard.ConsoleApp
{
  class Program
  {
    static void Main(string[] args)
    {
      ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

      var clientJSONResponse = getClientJSONResponse();
      List<BusData> listArrivingBuses = getArrivingBussesFromJSON(clientJSONResponse);
      printBusesToConsole(listArrivingBuses);
    }

    private static void printBusesToConsole(List<BusData> listArrivingBuses)
    {
      Console.WriteLine($"The next 5 buses arriving at {listArrivingBuses[0].StationName} are: \n");
      foreach (var bus in listArrivingBuses)
      {
        Console.WriteLine(bus.VehicleID);
      }
    }


    private static List<BusData> getArrivingBussesFromJSON(IRestResponse clientJSONResponse)
    {
      List<BusData> listArrivingBusDataFromJSON = JsonConvert.DeserializeObject<List<BusData>>(clientJSONResponse.Content);

      return listArrivingBusDataFromJSON;
    }

    private static IRestResponse getClientJSONResponse()
    {
      var client = new RestClient("https://api.tfl.gov.uk");
      var request = new RestRequest("StopPoint/490008660N/Arrivals", DataFormat.Json);
      var response = client.Get(request);

      return response;
    }

  }
}
