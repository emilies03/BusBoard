using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using BusBoard.ConsoleApp.Properties;


namespace BusBoard.ConsoleApp
{
  class Program
  {
    static void Main(string[] args)
    {
      ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
      
      
      UserConsoleInteration();
    }

    private static void UserConsoleInteration()
    {
      var userInput = "";
      while (userInput != "Exit")
      {
        Console.WriteLine("\nPlease enter an instruction: \n" +
                          "1.The post code to find the nearest bus station (e.g NW51TL)\n" +
                          "2 Exit \n");

        userInput = Console.ReadLine();
        if (userInput != "Exit")
        {
          TflAPI TFLServerAPI = new TflAPI();
          PostcodeAPI postcodeApi = new PostcodeAPI();
          PostcodeObject postcode = postcodeApi.GetPostcodeLongitueAndLatitude(userInput);
          
          if (postcode != null)
          {
            var TwoClosestBusStops = TFLServerAPI.GetBusStopsFromCoordinates(postcode.Longitude, postcode.Latitude);
            try
            {
              IEnumerable<BusData> listArrivingBusesAtStop1 =
                TFLServerAPI.GetArrivingBusesListFromServer(TwoClosestBusStops.First().NaptanId);
              IEnumerable<BusData> listArrivingBusesAtStop2 =
                TFLServerAPI.GetArrivingBusesListFromServer(TwoClosestBusStops.ElementAt(1).NaptanId);
              PrintBusesToConsole(listArrivingBusesAtStop1);
              PrintBusesToConsole(listArrivingBusesAtStop2);
            }
            catch (InvalidOperationException exception)
            {
              Console.WriteLine($"There are no bus stops registered near {userInput} \n");
            }
          }
          else
          {
            Console.WriteLine($"Invalid postcode: {userInput}");
          }
        }
      }
    }

    private static void PrintBusesToConsole(IEnumerable<BusData> listArrivingBuses)
    {
      Console.WriteLine($"\nThe next 5 buses arriving at {listArrivingBuses.First().StationName} are: \n");
      foreach (var bus in listArrivingBuses)
      {
        Console.WriteLine($"Bus {bus.VehicleID} on line {bus.LineID} to {bus.DestinationName} will be arriving in {bus.TimeToStation/60} minutes. Expected: {bus.ExpectedArrival}");
      }
    }
    
  }
}
