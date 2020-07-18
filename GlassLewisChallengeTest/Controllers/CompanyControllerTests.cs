using AutoFixture;
using FluentAssertions;
using GlassLewisChallenge.Controllers;
using GlassLewisChallenge.Domain;
using GlassLewisChallenge.Interfaces;
using GlassLewisChallenge.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace GlassLewisChallengeTest.Controllers
{
    public class CompanyControllerTests
    {
        public readonly ICompanyService _service = Substitute.For<ICompanyService>();
        public readonly ILogger<CompanyController> _logger = Substitute.For<ILogger<CompanyController>>();
        public readonly IValidatorService _validator = Substitute.For<IValidatorService>();
        public readonly Fixture _fix = new Fixture();

        [Fact]
        public void CheckNull_Argument_ILogger()
        {
            Assert.Throws<ArgumentNullException>(() => new CompanyController(null, _service, _validator));
        }

        [Fact]
        public void CheckNull_Argument_ICompanyService()
        {
            Assert.Throws<ArgumentNullException>(() => new CompanyController(_logger, null, _validator));
        }

        [Fact]
        public void GetAll_Companies()
        {
            var mockReturn = _fix.Create<List<Company>>();
            _service.GetAll().Returns(mockReturn);
            var controller = new CompanyController(_logger, _service, _validator);

            var result = controller.GetAll();

            result.Result.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public void Get_Company()
        {
            var mockReturn = _fix.Create<Company>();
            _service.Get(1).Returns(mockReturn);
            var controller = new CompanyController(_logger, _service, _validator);

            var result = controller.Get(1);

            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GetByIsin_Company()
        {
            var mockReturn = _fix.Create<Company>();
            _service.GetByIsin("1").Returns(mockReturn);
            var controller = new CompanyController(_logger, _service, _validator);

            var result = controller.GetByIsin("1");

            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void Insert_With_Invalid_Isin_Company()
        {
            var request = _fix.Build<Company>().With(x => x.Isin, "123").Create();

            var controller = new CompanyController(_logger, _service, _validator);
            var result = controller.Insert(request);
            result.Result.Should().BeOfType<BadRequestResult>();

        }

        [Fact]
        public void Insert_With_Null_Isin_Company()
        {
            var request = _fix.Create<Company>();
            request.Isin = null;

            var controller = new CompanyController(_logger, _service, _validator);
            var result = controller.Insert(request);
            result.Result.Should().BeOfType<BadRequestResult>();

        }

        [Fact]
        public void Update_Fails_When_Validation_Fail()
        {
            var request = _fix.Create<Company>();
            _validator.Validate(request, _service).Returns(false);
            var controller = new CompanyController(_logger, _service, _validator);
            var result = controller.Update(request.Id, request);
            result.Result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void Insert_Fails_When_Validation_Fail()
        {
            var request = _fix.Create<Company>();
            _validator.Validate(request,_service).Returns(false);
            var controller = new CompanyController(_logger, _service, _validator);
            var result = controller.Insert(request);
            result.Result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void Insert_Company()
        {
            var request = _fix.Build<Company>().With(x => x.Isin, "BR123").Create();
            _validator.Validate(request, _service).Returns(true);
            var controller = new CompanyController(_logger, _service, _validator);
            var result = controller.Insert(request);
            result.Result.Should().BeOfType<CreatedResult>();
        }

        [Fact]
        public void Update_Company()
        {
            var request = _fix.Build<Company>().With(x => x.Isin, "BR123").Create();
            _validator.Validate(request, _service,true).Returns(true);
            var controller = new CompanyController(_logger, _service, _validator);
            var result = controller.Update(request.Id, request);
            result.Result.Should().BeOfType<OkObjectResult>();
        }
    }
}
