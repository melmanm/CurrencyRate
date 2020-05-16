using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using System;

namespace CurrencyRate.Benchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<CurrencyRepositoryBenchmarkTest>();
        }
    }
}
