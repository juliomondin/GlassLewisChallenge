using FluentAssertions;
using GlassLewisChallange.Controllers;
using GlassLewisChallange.Interfaces;
using GlassLewisChallenge.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using Xunit;

namespace GlassLewisChallengeTests.Controllers
{
    public class TokenControllerTests
    {
        
        public readonly ITokenManager _mock = Substitute.For<ITokenManager>();
        public readonly ILogger<TokenController> _logger = Substitute.For<ILogger<TokenController>>();


        [Fact]
        public void CheckNull_Argument_ITokenService()
        {
            Assert.Throws<ArgumentNullException>(() => new TokenController(_logger, null));
            Assert.Throws<ArgumentNullException>(() => new TokenController(null, _mock));
        }


        [Fact]
        public void Authenticate_Method_When_User_Exists()
        {
            var user = new User { Id = 1, Password = "123", Username = "GlassLewis" };
            _mock.Authenticate(user.Username, user.Password).Returns(user);
            var controller = new TokenController(_logger, _mock);

            var result = controller.Authenticate(user);
            result.Result.Should().BeOfType<OkObjectResult>();

        }

        [Fact]
        public void Authenticate_Method_When_Doest_Not_User_Exist()
        {
            var user = new User { Id = 1, Password = "wrongpassword", Username = "wronguser" };
            _mock.Authenticate(user.Username,user.Password).Returns(null as User);
            var controller = new TokenController(_logger, _mock);

            var result =  controller.Authenticate(user);
            result.Result.Should().BeOfType<BadRequestObjectResult>();
        }
    }

}
