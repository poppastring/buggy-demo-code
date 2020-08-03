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
    }
}
