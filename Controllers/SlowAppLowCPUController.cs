using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BuggyDemoCode.Controllers
{
    public class SlowAppLowCPUController : BaseController
    {
        public IActionResult Index()
        {
            return Ok();
        }

        public IActionResult ReadDataFromFile()
        {
            var text = ReadTextAsync(@"C:\dev\test.txt").Result;

            return Ok(text.Remove(30));
        }
    }
}
