using System;
using RestSharp;

namespace BusBoard.ConsoleApp
{
    public class PostcodeAPI
    {

        public PostcodeObject GetPostcodeLongitudeAndLatitude(string postcodeSting)
        {
            var client = new RestClient("https://api.postcodes.io/postcodes/");
            var request = new RestRequest($"{postcodeSting}");
            
            PostcodeResponse postcodeResponse = client.Execute<PostcodeResponse>(request).Data;
            return postcodeResponse.result;
        }
    }
}