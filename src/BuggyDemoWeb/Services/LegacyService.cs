using BuggyDemoWeb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace BuggyDemoCode.Services
{
    public class LegacyService
    {
        private static int counter = 0;
        private const int OUTPUT_FREQUENCY = 1000;
        public string EndPointUri = "https://www.poppastring.com/blog/photos/a.197028616990372.62904.196982426994991/1186500984709792/?type=1&permPage=1";

        public async Task<string> DoAsyncOperationWell()
        {
            var random = new Random();

            await Task.Delay(random.Next(10) * 1000);

            return Guid.NewGuid().ToString();
        }

        public string DoSyncOperationWell()
        {
            var random = new Random();

            Thread.Sleep(random.Next(10) * 1000);

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

        public string ProcessDataHighCPU(int seconds)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            while (true)
            {
                watch.Stop();
                if (watch.ElapsedMilliseconds > 1000 * seconds)
                    break;
                watch.Start();
            }

            return Guid.NewGuid().ToString();
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

        public async Task SimpleDeadLockAsyncOperation()
        {
            var taskscomplete = new TaskCompletionSource<bool>();

            Task? t2 = null;
            Task? t1 = Task.Run(async () =>
            {
                await taskscomplete.Task;
                await t2;
            });

            t2 = Task.Run(async () =>
            {
                await taskscomplete.Task;
                await t1;
            });

            taskscomplete.SetResult(true);

            await Task.WhenAll(t1, t2);
        }

        public async Task SemaphoreDeadLockAsyncOperations()
        {
            var semaphore = new SemaphoreSlim(1, 1);

            await Task.Run(async () =>
            {
                await semaphore.WaitAsync();
                await Task.Run(async () =>
                {
                    await semaphore.WaitAsync();
                    semaphore.Release();
                });
            });

            semaphore.Release();
        }

        public async Task ReallyBadYieldReturn()
        {
            object _syncObj = new object();

            foreach (var i in GetEnum())
            {
                await UseAsync(i).ConfigureAwait(false);
            }

            IEnumerable<int> GetEnum()
            {
                lock (_syncObj)
                {
                    yield return GetValue(0);
                    yield return GetValue(1);
                }
            }

            int GetValue(int value)
            {
                lock (_syncObj)
                {
                    return value + 1;
                }
            }

            Task UseAsync(int i) => Task.Delay(i + 5000);
        }

        public Task Transfer(Account fromaccount, Account toaccount, int sum)
        {
            var task = Task.Run(() =>
            {
                lock (fromaccount)
                {
                    Thread.Sleep(1000);
                    lock (toaccount)
                    {
                        fromaccount.Balance -= sum;
                        toaccount.Balance += sum;
                    }
                }
            });

            return task;
        }

        public async Task<string> RetrieveData(int delay)
        {
            var random = new Random();

            await Task.Delay(delay * 1000);

            return Guid.NewGuid().ToString();
        }

        public void CreateStreamReadByte()
        {
            var ms = new MemoryStream(16);
            ms.Close();
            ms.ReadByte();
        }

        public string ValidateThisCollection()
        {
            var sb = new StringBuilder();
            var list = new DataRecord();

            for (int ctr = 0; ctr <= list.TotalCount; ctr++)
            {
                sb.Append(string.Format("Index {0}: {1}\r\n", ctr, list.MyList[ctr].LastName));

                if (list.MyList[ctr].LastName == "test")
                    break;
            }

            return sb.ToString();
        }

        public void InsertIntoAStringBuilder()
        {
            StringBuilder sb = new StringBuilder(15, 15);
            sb.Append("Substring #1 ");
            sb.Insert(0, "Substring #2 ", 1);
        }

        public void TypicalRecurrsionExample()
        {
            counter++;

            if (counter % OUTPUT_FREQUENCY == 0)
            {
                Console.WriteLine($"Current count: {counter}");
            }

            TypicalRecurrsionExample();
        }

        public void ATypicalRecurrsionExample()
        {
            var tag = new ValidTag();

            tag.MyTag = "<i>";
        }
    }
}