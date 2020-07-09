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

      userConsoleInteration();
    }

    private static void userConsoleInteration()
    {
      var userInput = "";
      while (userInput != "Exit")
      {
        Console.WriteLine("\n Please enter an instruction: \n" +
                          "1.The bus stop code you wish to query (490008660N)\n" +
                          "2 Exit \n");

        userInput = Console.ReadLine();
        if (userInput != "Exit")
        {
          var clientJSONResponse = getClientJSONResponse(userInput);
          if (clientJSONResponse.StatusCode == HttpStatusCode.OK)
          {
            List<BusData> listArrivingBuses = getArrivingBusesFromJSON(clientJSONResponse);
            printBusesToConsole(listArrivingBuses);
          }
          else
          {
            Console.WriteLine($"Invalid bus stop code: {userInput}");
          }
        }
      }
    }

    private static void printBusesToConsole(List<BusData> listArrivingBuses)
    {
      Console.WriteLine($"The next 5 buses arriving at {listArrivingBuses[0].StationName} are: \n");
      foreach (var bus in listArrivingBuses)
      {
        Console.WriteLine($"Bus {bus.VehicleID} on line {bus.LineID} to {bus.DestinationName} will be arriving in {bus.TimeToStationInMin} minutes. Expected: {bus.ExpectedArrivalTime}");
      }
    }


    private static List<BusData> getArrivingBusesFromJSON(IRestResponse clientJSONResponse)
    {
      List<BusData> listArrivingBusDataFromJSON = JsonConvert.DeserializeObject<List<BusData>>(clientJSONResponse.Content);
      List<BusData> orderedListArrivingBusData = listArrivingBusDataFromJSON.OrderBy(o=>o.ExpectedArrivalTime).ToList();

      return orderedListArrivingBusData;
    }

    private static IRestResponse getClientJSONResponse(string userInput)
    {
      var client = new RestClient("https://api.tfl.gov.uk");
      var request = new RestRequest($"StopPoint/{userInput}/Arrivals", DataFormat.Json);
      var response = client.Get(request);

      return response;
    }

  }
}
