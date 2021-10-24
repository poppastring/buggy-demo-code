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
    public class UnresponsiveHighCPUController : BaseController
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

        [HttpGet("highcpu/concatonate-large-strings")]
        public IActionResult ConcatString()
        {
            var html = "<table cellpadding=\"0\" cellspacing=\"0\"><tbody><tr>";
            var newrocord = new DataRecord() { FirstName = "Marco", LastName = "Polo", Address1 = "Lichfield Road", Address2 = "", City = "", State = "Indiana" };

            foreach (var rec in newrocord.MyList)
            {
                html += html + string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td> </tr>", rec.FirstName, rec.LastName, rec.Address1, rec.Address2, rec.City, rec.State);
            }

            html += html + "</table>";

            return Ok(html);
        }

        [HttpGet("highcpu/process-my-data")]
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

        [HttpGet("highcpu/large-object-heap/{size=85000}")]
        public int GetLOH1(int size)
        {
            return new byte[size].Length;
        }

    }
}
