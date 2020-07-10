﻿using System.Text;
using System.Web.Mvc;
using BusBoard.Api.Exceptions;
using BusBoard.Web.Models;
using BusBoard.Web.ViewModels;

namespace BusBoard.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult BusInfo(PostcodeSelection selection)
        {
            // Add some properties to the BusInfo view model with the data you want to render on the page.
            // Write code here to populate the view model with info from the APIs.
            // Then modify the view (in Views/Home/BusInfo.cshtml) to render upcoming buses.
            try
            {
                var info = new BusInfo(selection.Postcode);
                return View(info);
            }
            catch (InvalidPostcodeException invalidPostcodeException)
            {
                ViewBag.Message = "Invalid postcode entered";
                return View("Index");
            }
            catch (InvalidBusstopException invalidBusStopException)
            {
                ViewBag.Message = "This postcode has no registered bus stops close to it";
                return View("Index");
            }

        }

        public ActionResult About()
        {
            ViewBag.Message = "Information about this site";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact us!";

            return View();
        }
    }
}