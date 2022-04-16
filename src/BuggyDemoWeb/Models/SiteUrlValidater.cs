using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BuggyDemoWeb.Models
{
    public class SiteUrlValidater
    {
        public async Task<int> Search(string url)
        {
            var result = await Task.Run(() =>
            {
                return Regex.IsMatch(url, @"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$");
            });

            return Convert.ToInt32(result);
        }

        public async Task<int> BigForLoop()
        {
            var total = 0;

            for (int i = 0; i < 10000; i++)
            {
                total += i;
                await WriteToFileBackgroundOperationAsync(@"C:\dev\for.txt", string.Format("Iteration {0}", total));
            }

            return total;
        }

        public async Task<int> BigWhileLoop()
        {
            int total = 0;

            do
            {
                total++;
                await WriteToFileBackgroundOperationAsync(@"C:\dev\while.txt", string.Format("Iteration {0}", total));
            } while (total < 100);

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
