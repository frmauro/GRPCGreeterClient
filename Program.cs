using System;
using System.Net.Http;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Channels;
using Grpc.Net.Client;
using Grpc.Core;

namespace GrpcProductClient
{
    class Program
    {
        private const string urlapi = "https://localhost:5001";


        static SslCredentials GetSslCredentials()
        {
            var CERT_PATH = Path.Combine(Environment.CurrentDirectory, "Certs");
            var cacert = File.ReadAllText(Path.Combine(CERT_PATH, "localhost.crt"));
            var cert = File.ReadAllText(Path.Combine(CERT_PATH, "localhost.pfx"));
            var key = File.ReadAllText(Path.Combine(CERT_PATH, "localhost.key"));

            var keyPair = new KeyCertificatePair(cert, key);
            var Creds = new SslCredentials(cacert, keyPair);
            return Creds;
        }


        static string GetFileNameSslCredentials()
        {
            var CERT_PATH = Path.Combine(Environment.CurrentDirectory, "Certs");
            var cacert = Path.Combine(CERT_PATH, "localhost.crt");
            var cert = Path.Combine(CERT_PATH, "localhost.pfx");
            //var key = File.ReadAllText(Path.Combine(CERT_PATH, "localhost.key"));

            return cert;
        }


        static async Task Main(string[] args)
        {

            //const int Port = 8001;
            //const string Host = "127.0.0.1";

            //var creds = GetSslCredentials();

            //var fileNameSSLCertificate = GetFileNameSslCredentials();
            //var certificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(fileNameSSLCertificate, "123");
            //var handler = new HttpClientHandler();
            //handler.ClientCertificates.Add(certificate);

            //var PcName = Environment.MachineName;

            
            // var channelOptions = new List<ChannelOption>
            //     {
            //         new ChannelOption(ChannelOptions.SslTargetNameOverride, PcName)
            //     };

            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            // Return "true" to allow certificates that are untrusted/invalid
            // var httpHandler = new HttpClientHandler
            // {
            //     ServerCertificateCustomValidationCallback =
            //         HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            // };

            // The port number(5001) must match the port of the gRPC server.
            //using var channel = GrpcChannel.ForAddress("https://localhost:8001");

            //var channelCredentials = new Grpc.Core.SslCredentials(System.IO.File.ReadAllText("roots.pem")); 

            //var channel = new System.Threading.Channels.Channel("localhost", 8001, creds);

            //var handler = new HttpClientHandler();
            //handler.ClientCertificates.Add(creds);

            using var channel = GrpcChannel.ForAddress("http://127.0.0.1:8081", new GrpcChannelOptions { Credentials = ChannelCredentials.Insecure });

            var client = new ProductServiceProto.ProductServiceProtoClient(channel);
            var reply = await client.SendProductAsync(new ProductRequest { Id = 1,  Description = "Product 001", Amount = "200", Price = "200", Status = "Active"});
            Console.WriteLine("Response Product: " + reply.Message);
            Console.WriteLine("Press any key to exit...");
             //Console.ReadKey();
            //Console.WriteLine("Hello World!");
        }
    }
}
