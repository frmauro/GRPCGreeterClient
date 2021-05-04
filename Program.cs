using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;

namespace GrpcProductClient
{
    class Program
    {
        static async Task Main(string[] args)
        {

            // Return "true" to allow certificates that are untrusted/invalid
            var httpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpHandler = httpHandler });
            var client = new ProductServiceProto.ProductServiceProtoClient(channel);
             var reply = await client.SendProductAsync(new ProductRequest { Id = 1,  Description = "Product 001", Amount = "200", Price = "200", Status = "Active"});
             Console.WriteLine("Response Product: " + reply.Message);
             Console.WriteLine("Press any key to exit...");
             Console.ReadKey();
            //Console.WriteLine("Hello World!");
        }
    }
}
