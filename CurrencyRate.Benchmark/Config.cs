using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyRate.Benchmark
{
    public class BenchmarkConfig : ManualConfig
    {
        public BenchmarkConfig()
        {
            Add(DefaultConfig.Instance // A configuration for our benchmarks
                .With(Job.Default // Adding second job
               .AsBaseline() // It will be marked as baseline
               .WithWarmupCount(0)
               )
               .With(ConfigOptions.DisableOptimizationsValidator));
            // *** add default loggers, reporters etc? ***
            // Disable warm-up stage
        }

    }

}
