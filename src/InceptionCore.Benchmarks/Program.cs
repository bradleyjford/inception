using System;
using BenchmarkDotNet.Running;

namespace InceptionCore.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<ContainerBenchmarks>();

            Console.ReadKey();
        }
    }
}
