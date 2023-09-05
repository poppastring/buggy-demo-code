using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Text;

namespace BuggyBenchMark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<ProcessingServices>();
        }
    }

    [MemoryDiagnoser]
    public class ProcessingServices
    {
        [GlobalSetup]
        public void GlobalSetup()
        {
        }

        [Benchmark]
        public void ProcessTextBasedDataSource()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < 10_000; i++)
            {
                stringBuilder.Append("sometexttoconcatenate");
            }
        }
    }
}