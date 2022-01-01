using Microsoft.AspNetCore.Hosting;
using NUnit.Framework;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Ninject.Extensions.Interception.AttributeProxy.MvcTest
{
    public class Tests
    {
        const string URL = "http://localhost:8080/";
        private readonly CancellationToken token = new CancellationToken();

        [SetUp]
        public void Setup()
        {
            var host = new WebHostBuilder()
                .UseUrls(URL)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.RunAsync(token);
        }

        [Ignore("Aun no esta listo")]
        [Test]
        public async Task Test1()
        {
            await Task.Delay(100000);
        }

        [TearDown]
        public void TearDown()
        {
            token.ThrowIfCancellationRequested();
        }
    }
}