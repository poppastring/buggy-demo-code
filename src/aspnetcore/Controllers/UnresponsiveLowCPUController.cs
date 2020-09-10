using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BuggyDemoCode.Controllers
{
    public class UnresponsiveLowCPUController : BaseController
    {
        public IActionResult Index()
        {
            return Ok();
        }

        /// <summary>
        /// e.g. wrk -c 256 -t 10 -d 20 https://localhost:5001/lowcpu/uses-too-many-threadpool-thread
        /// </summary>
        /// <returns></returns>
        [HttpGet("lowcpu/uses-too-many-threadpool-threads")]
        public IActionResult SyncOverAsyncResult()
        {
            var text = ReadTextAsync(string.Format(@"{0}\\test.txt", Environment.CurrentDirectory)).Result;

            return Ok(text?.Remove(30));
        }
    }
}
