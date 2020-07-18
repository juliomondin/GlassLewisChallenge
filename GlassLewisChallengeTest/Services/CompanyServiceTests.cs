using FluentAssertions;
using GlassLewisChallenge.Domain;
using GlassLewisChallenge.Interfaces;
using GlassLewisChallenge.Services;
using NSubstitute;
using System.Collections.Generic;
using Xunit;

namespace GlassLewisChallengeTests.Services
{
    public class CompanyServiceTests
    {
        private readonly IRepository _mock = Substitute.For<IRepository>();
        
        [Fact]
        public void GetAll_Companies()
        {
            var mockReturn = new List<Company>();
            _mock.GetAll().Returns(mockReturn);
            var service = new CompanyService(_mock);
            service.GetAll().Should().AllBeEquivalentTo(mockReturn);
        }

        [Fact]
        public void Get_Company()
        {
            var mockReturn = new Company();
            _mock.Get(1).Returns(mockReturn);
            var service = new CompanyService(_mock);
            service.Get(1).Should().BeEquivalentTo(mockReturn);
        }

        [Fact]
        public void Get_Company_By_Isin()
        {
            var mockReturn = new Company();
            _mock.GetByIsin("1").Returns(mockReturn);
            var service = new CompanyService(_mock);
            service.GetByIsin("1").Should().BeEquivalentTo(mockReturn);
        }

        [Fact]
        public void Update_Company()
        {
            var request = new Company();
            var mockReturn = new Company();
            _mock.Update(request).Returns(mockReturn);
            var service = new CompanyService(_mock);
            service.Update(request).Should().BeEquivalentTo(mockReturn);
        }


        [Fact]
        public void Insert_Company()
        {
            var request = new Company();
            var mockReturn = new Company();
            _mock.Insert(request).Returns(mockReturn);
            var service = new CompanyService(_mock);
            service.Insert(request).Should().BeEquivalentTo(mockReturn);
        }

    }
}
