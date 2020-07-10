using System;
using BusBoard.Api.Exceptions;
using RestSharp;

namespace BusBoard.ConsoleApp
{
    public class PostcodeAPI
    {

        public PostcodeObject GetPostcodeLongitueAndLatitude(string postcodeSting)
        {
            var client = new RestClient("https://api.postcodes.io/postcodes/");
            var request = new RestRequest($"{postcodeSting}");
            
            PostcodeResponse postcodeResponse = client.Execute<PostcodeResponse>(request).Data;
            if (postcodeResponse.result == null)
            {
                throw new InvalidPostcodeException();
            }
            
            return postcodeResponse.result;
        }


    }
}