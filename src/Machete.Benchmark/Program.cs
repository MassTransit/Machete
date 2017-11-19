namespace Machete.Benchmark
{
    using BenchmarkDotNet.Running;
    using HL7;
    using HL7Schema.V26;


    class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<HL7Benchmark>();
        }
    }
}