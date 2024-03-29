namespace IntegrationTest
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;

    using WebApiIntegrationTest;

    using Xunit;

    /// <summary>
    /// The values controller integration test.
    /// Source: https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-2.2
    /// </summary>
    public class ValuesControllerIntegrationTest
    {
        /// <summary>
        /// The client.
        /// </summary>
        private readonly HttpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValuesControllerIntegrationTest"/> class.
        /// </summary>
        public ValuesControllerIntegrationTest()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>());

            this.client = server.CreateClient();
        }

        /// <summary>
        /// The test get.
        /// </summary>
        /// <param name="method">
        /// The method.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Theory]
        [InlineData("GET")]
        public async Task TestGet(string method)
        {
            // ARRANGE
            var request = new HttpRequestMessage(new HttpMethod(method), "/api/values");

            // ACT
            var response = await this.client.SendAsync(request);

            // ASSERT
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        /// <summary>
        /// The test get by id.
        /// </summary>
        /// <param name="method">
        /// The method.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Theory]
        [InlineData("GET", 1)]
        public async Task TestGetById(string method, int id)
        {
            // ARRANGE
            var request = new HttpRequestMessage(new HttpMethod(method), $"/api/values/{id}");
            
            // ACT
            var response = await this.client.SendAsync(request);

            // ASSERT
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
