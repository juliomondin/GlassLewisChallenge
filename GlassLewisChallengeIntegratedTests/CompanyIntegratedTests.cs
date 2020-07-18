using GlassLewisChallange.Domain;
using GlassLewisChallenge;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GlassLewisChallengeIntegratedTests
{
    public class CompanyIntegratedTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public CompanyIntegratedTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Can_Get_Companies()
        {
            var httpResponse = await _client.GetAsync("/company");
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var companies = JsonConvert.DeserializeObject<IEnumerable<Company>>(stringResponse);
            Assert.Contains(companies, p => p.Isin == "AR123");
            Assert.Contains(companies, p => p.Isin == "OI123");
        }

        [Fact]
        public async Task Can_Get_Company_By_Id()
        {
            var httpResponse = await _client.GetAsync("/company/1");
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var company = JsonConvert.DeserializeObject<Company>(stringResponse);
            Assert.True(company.Name == "GlassLewis");
        }

        [Fact]
        public async Task Can_Get_Company_By_Isin()
        {
            var httpResponse = await _client.GetAsync("/company/isin/AR123");
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var company = JsonConvert.DeserializeObject<Company>(stringResponse);
            Assert.True(company.Name == "GlassLewis");
        }
    }
}
