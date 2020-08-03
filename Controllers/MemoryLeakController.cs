﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuggyDemoCode.Models;
using Microsoft.AspNetCore.Mvc;

namespace BuggyDemoCode.Controllers
{
    public class MemoryLeakController : Controller
    {
        public IActionResult Index()
        {
            return Ok();
        }

        public IActionResult StaticObjects()
        {
            var val = new DataRecord() { FirstName = "Mark", LastName = "Smith", Address1 = "Wem Street", Address2 = "", City = "Lichfield", State = "Ohio" };

            var val1 = new DataRecord() { FirstName = "Pete", LastName = "Jones", Address1 = "Lawrence Ave", Address2 = "on the corner", City = "", State = "Oregon" };

            var val2 = new DataRecord() { FirstName = "Andrew", LastName = "Jordan", Address1 = "Coleman Ave", Address2 = "", City = "", State = "Michigan" };

            var val3 = new DataRecord() { FirstName = "Marco", LastName = "Stephen", Address1 = "Lichfield Road", Address2 = "", City = "", State = "Indiana" };

            return Ok(val3.TotalCount);
        }
    }
}
