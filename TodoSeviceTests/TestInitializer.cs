using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WebAppBasedDesignPatterns;

namespace TodoSeviceTests
{
    [TestClass]
    public class TestInitializer
    {
        public static HttpClient TestHttpClient;
       // public static Mock<IEmployeeRepository> MockEmployeeRepository;

        [AssemblyInitialize]
        public static void InitializeTestServer(TestContext testContext)
        {
            var testServer = new TestServer(new WebHostBuilder()
               .UseStartup<TestStartup>()
               // this would cause it to use StartupIntegrationTest class
               // or ConfigureServicesIntegrationTest / ConfigureIntegrationTest
               // methods (if existing)
               // rather than Startup, ConfigureServices and Configure
               .UseEnvironment("IntegrationTest"));

            TestHttpClient = testServer.CreateClient();
        }

        //public static void RegisterMockRepositories(IServiceCollection services)
        //{
        //    MockEmployeeRepository = (new Mock<IEmployeeRepository>());
        //    services.AddSingleton(MockEmployeeRepository.Object);

        //    //add more mock repositories below
        //}
    }
}
