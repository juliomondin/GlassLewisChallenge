using AutoFixture;
using FluentAssertions;
using GlassLewisChallenge.Domain;
using GlassLewisChallenge.Interfaces;
using GlassLewisChallenge.Services;
using NSubstitute;
using Xunit;

namespace GlassLewisChallengeTest.Services
{
    public class ValidatorServiceTests
    {

        private readonly ICompanyService _mock = Substitute.For<ICompanyService>();
        private readonly Fixture _fix = new Fixture();

        [Fact]
        public void Validation_Method_When_Isis_Not_Valid()
        {
            var request = _fix.Build<Company>().With(x => x.Isin, "123").Create();

            var validator = new ValidatorService();
            var result = validator.Validate(request, _mock);
            result.Should().BeFalse();
        }

        [Fact]
        public void Validation_Method_When_Everything_is_Correct()
        {
            var request = _fix.Build<Company>().With(x => x.Isin, "BR123").Create();
            var validator = new ValidatorService();
            var result = validator.Validate(request, _mock);
            result.Should().BeTrue();

        }

        [Fact]
        public void Validation_Method_When_ObrigatoryField_Is_Null()
        {
            var request = _fix.Build<Company>().With(x => x.Isin, "BR123").Create();
            request.Name = "";
            var validator = new ValidatorService();
            var result = validator.Validate(request, _mock);
            result.Should().BeFalse();
        }

        [Fact]
        public void Validation_Method_When_Isin_Is_AlreadyTaken()
        {
            var request = _fix.Build<Company>().With(x => x.Isin, "BR123").Create();
            _mock.GetByIsin(request.Isin).Returns(request);
            var validator = new ValidatorService();
            var result = validator.Validate(request, _mock);
            result.Should().BeFalse();

        }
    }
}
