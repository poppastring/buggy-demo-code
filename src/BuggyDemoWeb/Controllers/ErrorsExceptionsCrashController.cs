using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BuggyDemoWeb.Models;
using BuggyDemoCode.Services;

namespace BuggyDemoWeb.Controllers
{
    public class ErrorsExceptionsCrashController : Controller
    {
        private readonly LegacyService legacyService;

        public ErrorsExceptionsCrashController(LegacyService legacyService)
        {
            this.legacyService = legacyService;
        }

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

        /// <summary>
        /// Port exhaustion, apparently, not sure how to meausure it...
        /// </summary>
        /// <returns></returns>
        [HttpGet("exception/object-dispose")]
        public IActionResult ObjectDisposeException()
        {
            legacyService.CreateStreamReadByte();

            return Ok();
        }

        [HttpGet("exception/out-of-range")]
        public IActionResult OutOfRange()
        {
            var sb = legacyService.ValidateThisCollection();

            return Ok(sb);
        }

        [HttpGet("crash/stack-overflow")]
        public IActionResult StackOverflow()
        {
            legacyService.TypicalRecurrsionExample();

            return Ok();        }

        [HttpGet("crash/stack-overflow2")]
        public IActionResult StackOverflow2()
        {
            legacyService.ATypicalRecurrsionExample();

            return Ok();
        }

        [HttpGet("crash/async-void1")]
        public IActionResult AsyncVoidCrash()
        {
            RaiseEvent();

            return Ok();
        }

        private void RaiseEvent() => RaiseEventVoidAsync();
        private async void RaiseEventVoidAsync() => throw new Exception("Error!");

        [HttpGet("crash/async-void2")]
        public async void AsyncVoidCrash2()
        {
            await Task.Delay(1000);

            // THIS will crash the process since we're writing after the response has completed on a background thread
            await Response.WriteAsync("Hello World");
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
    }
}
