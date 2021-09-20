using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BuggyDemoCode.Services
{
    public class LegacyService
    {
        public string EndPointUri = "https://www.poppastring.com/blog/photos/a.197028616990372.62904.196982426994991/1186500984709792/?type=1&permPage=1";

        public async Task<string> DoAsyncOperationWell()
        {
            var random = new Random();

            await Task.Delay(random.Next(10) * 1000);

            return Guid.NewGuid().ToString();
        }

        public async Task<int> ValidateUrl(string url)
        {
            var result = await Task.Run(() =>
            {
                return Regex.IsMatch(url, @"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$");
            });

            return Convert.ToInt32(result);
        }

        public async Task<int> ProcessBigDataFile()
        {
            var total = 0;

            for (int i = 0; i < 2000; i++)
            {
                total += i;
                await WriteToFileBackgroundOperationAsync(@"C:\dev\for.txt", string.Format("Iteration {0}", total));
            }

            return total;
        }

        public async Task<int> ProcessBigDataFile2()
        {
            int total = 0;

            do
            {
                total++;
                await WriteToFileBackgroundOperationAsync(@"C:\dev\while.txt", string.Format("Iteration {0}", total));
            } while (total < 2000);

            return total;
        }

        private async Task WriteToFileBackgroundOperationAsync(string filePath, string text)
        {
            byte[] encodedText = Encoding.Unicode.GetBytes(text);

            using (FileStream sourceStream = new FileStream(filePath,
                FileMode.Append, FileAccess.Write, FileShare.None,
                bufferSize: 4096, useAsync: true))
            {
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            };
        }
    }
}
