using System.Collections.Generic;
using System.Linq;
using BusBoard.ConsoleApp;
using BusBoard.ConsoleApp.Properties;

namespace BusBoard.Web.ViewModels
{
  public class BusInfo
  {
    public BusInfo(string postCode)
    {
      PostCode = postCode;
      
      TflAPI TFLServerAPI = new TflAPI();
      PostcodeAPI postcodeApi = new PostcodeAPI();
      PostcodeObject postcode = postcodeApi.GetPostcodeLongitueAndLatitude(postCode);
      if (postcode != null)
      {
        nearestTwoBusStops = TFLServerAPI.GetBusStopsFromCoordinates(postcode.Longitude, postcode.Latitude);
      }
      
      listArrivingBusesAtStop1 =
        TFLServerAPI.GetArrivingBusesListFromServer(nearestTwoBusStops.First().NaptanId);
      listArrivingBusesAtStop2 =
        TFLServerAPI.GetArrivingBusesListFromServer(nearestTwoBusStops.ElementAt(1).NaptanId);
      
    }

    public string PostCode { get; set; }
    public IEnumerable<BusStop> nearestTwoBusStops { get; set; }
    public IEnumerable<BusData> listArrivingBusesAtStop1 { get; set; }
    public IEnumerable<BusData> listArrivingBusesAtStop2 { get; set; }

  }
}