using System;
using System.Collections.Generic;
using System.Linq;
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

        public IActionResult AsyncVoidCrash()
        {
            WriteToFileBackgroundOperationAsync("", "Hello World\r\n");

            return Ok();
        }

        public Task ParallelAsyncCrash()
        {
            var list = new List<int>();

            var tasks = new Task[10];

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = GetNumberAsync(list, i);
            }

            return Task.WhenAll(tasks);
        }

        private async Task GetNumberAsync(List<int> results, int number)
        {
            await Task.Delay(300); // We want an IO bound call that will take some indeterminate time 200-300ms

            results.Add(number);
        }
    }
}
