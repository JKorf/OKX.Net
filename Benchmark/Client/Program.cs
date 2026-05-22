using BenchmarkDotNet.Running;
using System.Diagnostics;

namespace OKX.Net.Benchmark.Client
{
    public class Program
    {
        public static int ServerPort = 5000;

        public static void Main(string[] args)
        {
            // For manual testing:

            //var test = new LibraryComparisonBenchmarksSocket();
            //test.Setup();
            //Console.ReadLine();
            //Console.WriteLine("Starting");
            //var sw = Stopwatch.StartNew();
            //for (var i = 0; i < 2; i++)
            //{
            //    test.OKXApi_Trades().Wait();
            //}
            //sw.Stop();
            //Console.WriteLine($"Finished in {sw.ElapsedMilliseconds} ms");
            //Console.ReadLine();
            //test.GlobalCleanup();

            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}
