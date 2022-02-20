using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using BuggyDemoWeb.Models;

namespace BuggyDemoWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IActionDescriptorCollectionProvider _actionProvider;

        public HomeController(ILogger<HomeController> logger, IActionDescriptorCollectionProvider actionProvider)
        {
            _actionProvider = actionProvider;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var routes = _actionProvider.ActionDescriptors.Items.Where(x => x.AttributeRouteInfo != null);

            var routeurls = routes.Select(x => new Routes() { Url = x.AttributeRouteInfo.Template }).ToList();

            return View(routeurls);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
