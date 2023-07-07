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
            //Write your initialization code here
        }

        [Benchmark]
        public void ProcessTextBasedDataSource()
        {
            string someText = string.Empty;
            for (int i = 0; i < 10_000; i++)
            {
                someText += "sometexttoconcatenate";
            }
        }
    }
}