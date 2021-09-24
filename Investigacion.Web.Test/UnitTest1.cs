using NUnit.Framework;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Investigacion.Web.Test.App_Start;
using System.Linq;

namespace Investigacion.Web.Test
{
    public class Tests
    {
        const string URL = "http://localhost:8080/";
        private IDisposable website;

        [SetUp]
        public void Setup()
        {
            website = WebApp.Start<Startup>(URL);
        }

        [TearDown]
        public void TearDown()
        {
            website.Dispose();
        }

        [Test]
        public async Task Test1()
        {
            var value = await Request();
            Assert.AreEqual(2, value);
        }

        [Test]
        public async Task RequestSimultaneos()
        {
            var req1 = Request();
            var req2 = Task.Delay(100).ContinueWith(t => Request().Result);
            var req3 = Task.Delay(200).ContinueWith(t => Request().Result);

            var values = await Task.WhenAll(
                    req1,
                    req2,
                    req3
                    );

            foreach (var value in values)
            {
                Assert.AreEqual(2, value);
            }
        }

        [Test]
        public async Task RequestSerie()
        {
            var values = new[] { await Request(), await Request(), await Request() };

            foreach (var value in values)
            {
                Assert.AreEqual(2, value);
            }
        }

        [Test]
        public async Task StressTest()
        {
            var sequence = Enumerable.Range(1,1000).ToDictionary(c => c, c => Test1());

            var timerInit = DateTime.UtcNow;

            await Task.WhenAll(sequence.Values);

            var timerEnd = DateTime.UtcNow;

            var demora = timerEnd.Subtract(timerInit);
            Console.WriteLine(demora);
        }

        private async Task<int> Request()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL);
                using (var response = await client.GetAsync("api/item"))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        Assert.Fail(content.ToString());
                    }

                    var value = await response.Content.ReadAsAsync<int>();

                    return value;
                }
            }
        }
    }
}