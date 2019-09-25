using BenchmarkDotNet.Running;
using System;

namespace RESTvsGRPC
{
    class Program
    {
        static void Main(string[] args)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            BenchmarkRunner.Run<BenchmarkHarness>();
            Console.ReadKey();
        }
    }
}
