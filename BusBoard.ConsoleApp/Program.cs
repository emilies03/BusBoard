using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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
      
      
      var clientResponse = getClientJSONResponse();
    }

    private static IRestResponse getClientJSONResponse()
    {
      var client = new RestClient("https://api.tfl.gov.uk");
      var request = new RestRequest("StopPoint/490008660N/Arrivals", DataFormat.Json);
      var response = client.Get(request);
      
      Console.WriteLine(response.ResponseUri);

      return response;
    }

  }
}
