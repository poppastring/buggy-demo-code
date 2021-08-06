using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuggyDemoWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BuggyDemoWeb.Controllers
{
    public class MemoryLeakController : Controller
    {
        private IMemoryCache cache;

        public MemoryLeakController(IMemoryCache cache)
        {
            this.cache = cache;
        }

        public IActionResult Index()
        {
            return Ok();
        }

        [HttpGet("memoryleak/static-references")]
        public IActionResult StaticObjects()
        {
            var val = new DataRecord() { FirstName = "Mark", LastName = "Smith", Address1 = "Wem Street", Address2 = "", City = "Lichfield", State = "Ohio" };

            var val1 = new DataRecord() { FirstName = "Pete", LastName = "Jones", Address1 = "Lawrence Ave", Address2 = "on the corner", City = "", State = "Oregon" };

            var val2 = new DataRecord() { FirstName = "Andrew", LastName = "Jordan", Address1 = "Coleman Ave", Address2 = "", City = "", State = "Michigan" };

            var val3 = new DataRecord() { FirstName = "Marco", LastName = "Stephen", Address1 = "Lichfield Road", Address2 = "", City = "", State = "Indiana" };

            return Ok(val3.TotalCount);
        }

        [HttpGet("memoryleak/references-in-cache/{data}")]
        public IActionResult ReferencesInCache(string data)
        {
            var id = Guid.NewGuid();
            var customer = new CustomerRecord() { FirstName = "Marco", LastName = "Stephen", Address1 = "Lichfield Road", Address2 = "", City = "", State = "Indiana", Id = id.ToString(), Data = data };

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                                        .SetSlidingExpiration(TimeSpan.FromSeconds(10))
                                        .RegisterPostEvictionCallback(callback: CodeCleanUp, state: this);

            cache.Set(id.ToString(), customer, cacheEntryOptions);

            return Ok();
        }

        private void CodeCleanUp(object key, object value, EvictionReason reason, object state)
        {
            var message = $"Entry was evicted. Reason: {reason}.";
        }
    }
}
