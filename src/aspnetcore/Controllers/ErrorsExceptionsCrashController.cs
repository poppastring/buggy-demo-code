using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BuggyDemoCode.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuggyDemoCode.Controllers
{
    public class ErrorsExceptionsCrashController : BaseController
    {
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpGet("exception/out-of-range")]
        public IActionResult OutOfRange()
        {
            var sb = new StringBuilder();
            var list = new DataRecord();

            for (int ctr = 0; ctr <= list.TotalCount; ctr++)
            {
                sb.Append(string.Format("Index {0}: {1}\r\n", ctr, list.MyList[ctr].LastName));

                if (list.MyList[ctr].LastName == "test")
                    break;
            }

            return Ok(sb.ToString());
        }

        [HttpGet("crash/async-void")]
        public IActionResult AsyncVoidCrash()
        {
            WriteToFileBackgroundOperationAsync("", "Hello World\r\n");

            return Ok();
        }

        /// <summary>
        /// Crashes after you create some load
        /// e.g. wrk -c 256 -t 10 -d 20 https://localhost:5001/crash/parallel-list-async
        /// </summary>
        /// <returns></returns>
        [HttpGet("crash/parallel-list-async")]
        public Task ParallelAsyncCrash()
        {
            var list = new List<int>();
            var tasks = new Task[10];

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = GetPageDataAsync(list, i);
            }

            return Task.WhenAll(tasks);
        }

        private async Task GetPageDataAsync(List<int> results, int number)
        {
            await Task.Delay(300); // Exchange with an IO bound call that will take some indeterminate time 100-300ms

            using (var client = new HttpClient())
            {    
                // await client.GetStringAsync(string.Format("https://www.poppastring.com/blog/page/{0}", number));
            }

            // Tracking that the page retrieval occurred...
            results.Add(number);
        }
    }
}
