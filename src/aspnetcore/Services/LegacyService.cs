using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BuggyDemoCode.Services
{
    public class LegacyService
    {
        public async Task<string> DoAsyncOperationWell()
        {
            var random = new Random();

            await Task.Delay(random.Next(10) * 1000);

            return Guid.NewGuid().ToString();
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
    }
}
