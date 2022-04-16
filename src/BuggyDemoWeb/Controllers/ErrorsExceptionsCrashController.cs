using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BuggyDemoWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuggyDemoWeb.Controllers
{
    public class ErrorsExceptionsCrashController : BaseController
    {
        private static int counter = 0;
        private const int OUTPUT_FREQUENCY = 1000;

        public IActionResult Index()
        {
            return Ok();
        }

        /// <summary>
        /// Port exhaustion, apparently, not sure how to meausure it...
        /// </summary>
        /// <returns></returns>
        [HttpGet("exception/port-exhaustion")]
        public async Task<int> HttpClientPortExhaustion()
        {
            using (var httpClient = new HttpClient())
            {
                var result = await httpClient.GetAsync("https://github.com");
                return (int)result.StatusCode;
            }
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

        [HttpGet("crash/stack-overflow")]
        public IActionResult StackOverflow()
        {
            StackOverflowExample();

            return Ok();
        }

        [HttpGet("crash/stack-overflow2")]
        public IActionResult StackOverflow2()
        {
            var tag = new ValidTag();

            tag.MyTag = "<i>";

            return Ok(tag.MyTag);
        }

        [HttpGet("crash/async-void")]
        public IActionResult AsyncVoidCrash()
        {
            string filename = EndsUpReturningNullInProduction();

            WriteToFileBackgroundOperationAsync(filename, "Hello World\r\n");

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

            //using (var client = new HttpClient())
            //{    
            // await client.GetStringAsync(string.Format("https://www.poppastring.com/blog/page/{0}", number));
            //}

            // Tracking that the page retrieval occurred...
            results.Add(number);
        }


        private void StackOverflowExample()
        {
            counter++;

            if (counter % OUTPUT_FREQUENCY == 0)
            {
                Console.WriteLine($"Current count: {counter}");
            }

            StackOverflowExample();

        }
    }
}
