using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BuggyDemoWeb.Models;
using BuggyDemoWeb.Controllers;
using Microsoft.AspNetCore.Mvc;
using BuggyDemoCode.Services;

namespace BuggyDemoWeb.Controllers
{
    public class UnresponsiveHighCPUController : Controller
    {
        private readonly LegacyService legacyService;

        public UnresponsiveHighCPUController(LegacyService legacyService)
        {
            this.legacyService = legacyService;
        }

        public IActionResult Index()
        {
            return Ok();
        }

        [HttpGet("highcpu/poor-regex")]
        public async Task<ActionResult> DataProcessing()
        {
            var tasks = new List<Task>();

            // Process my data original version, big for loop
            tasks.Add(legacyService.ProcessBigDataFile());

            // Process my data version two, big while loop
            tasks.Add(legacyService.ProcessBigDataFile2());

            // Validate a URL, just once
            tasks.Add(legacyService.ValidateUrl(legacyService.EndPointUri));

            await Task.WhenAll(tasks.ToArray());

            return Ok();
        }

        [HttpGet("highcpu/tight-loop/{seconds=30}")]
        public IActionResult NoDataProcessing(int seconds)
        {
            var val = legacyService.ProcessDataHighCPU(seconds);

            return Ok(val);
        }

        [HttpGet("highcpu/large-object-heap/{size=85000}")]
        public int GetLOH1(int size)
        {
            return new byte[size].Length;
        }

    }
}
