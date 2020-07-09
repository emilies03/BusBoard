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
                          "1.The bus stop code you wish to query (490008660N)\n" +
                          "2 Exit \n");

        userInput = Console.ReadLine();
        if (userInput != "Exit")
        {
          TflAPI TFLServerAPI = new TflAPI();
          IEnumerable<BusData> listArrivingBuses = TFLServerAPI.GetArrivingBusesListFromServer(userInput);
          if (listArrivingBuses.First().DestinationName != null)
          {
            PrintBusesToConsole(listArrivingBuses);
          }
          else
          {
            Console.WriteLine($"Invalid bus stop code: {userInput}");
          }
         
        }
      }
    }

    private static void PrintBusesToConsole(IEnumerable<BusData> listArrivingBuses)
    {
      Console.WriteLine($"\n The next 5 buses arriving at {listArrivingBuses.First().StationName} are: \n");
      foreach (var bus in listArrivingBuses)
      {
        Console.WriteLine($"Bus {bus.VehicleID} on line {bus.LineID} to {bus.DestinationName} will be arriving in {bus.TimeToStation} minutes. Expected: {bus.ExpectedArrival}");
      }
    }


    

  }
}
