using System.Text.Encodings.Web;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Moq;
using WebApiProject.Handlers;
using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using WebApiProject.Repository;
using WebApiProject.Models;
using Microsoft.AspNetCore.Authentication;

namespace WebApiProjectTest.Systems.Handler
{
    public class BasicAuthenticationHandlerTest
    {
        private readonly Mock<IOptionsMonitor<AuthenticationSchemeOptions>> _options;
        private readonly Mock<ILoggerFactory> _logger;
        private readonly Mock<UrlEncoder> _encoder;
        private readonly Mock<ISystemClock> _clock;
        private readonly Mock<IUserRepository> _userRepository;
        public BasicAuthenticationHandlerTest()
        {
            _options = new Mock<IOptionsMonitor<AuthenticationSchemeOptions>>();
            _options.Setup(x => x.Get(It.IsAny<string>())).Returns(new AuthenticationSchemeOptions());
            var logger = new Mock<ILogger<BasicAuthenticationHandler>>();
            _logger = new Mock<ILoggerFactory>();
            _logger.Setup(x => x.CreateLogger(It.IsAny<String>())).Returns(logger.Object);
            _encoder = new Mock<UrlEncoder>();
            _clock = new Mock<ISystemClock>();
            _userRepository = new Mock<IUserRepository>();
        }

        [Fact(DisplayName = "Authorization header not provided should return missing authorization header")]
        public async Task AuthorizationHeaderNotProvidedShouldReturnInvalidAuthorizationHeader()
        {
            ///arrange
            var context = new DefaultHttpContext();
            var _handler = new BasicAuthenticationHandler(_options.Object,_logger.Object,_encoder.Object,_clock.Object,_userRepository.Object);
            await _handler.InitializeAsync(new AuthenticationScheme(AuthenticationSchemes.Basic.ToString(), null, typeof(BasicAuthenticationHandler)),context);
            
            ///act
            var result = await _handler.AuthenticateAsync();

            ///assert
            Assert.False(result.Succeeded);
            Assert.Equal("Missing Authorization Header", result.Failure.Message);
        }

        [Fact(DisplayName="Authorization header is empty should return invalid authorization header")]
        public async Task AuthorizationHeaderEmptyShouldReturnInvalidAuthorizationHeader()
        {
            ///arrange
            var context = new DefaultHttpContext();
            var authorizationHeader = new StringValues(String.Empty);
            context.Request.Headers.Add(HeaderNames.Authorization, authorizationHeader);
            var _handler = new BasicAuthenticationHandler(_options.Object,_logger.Object,_encoder.Object,_clock.Object,_userRepository.Object);
            await _handler.InitializeAsync(new AuthenticationScheme(AuthenticationSchemes.Basic.ToString(), null, typeof(BasicAuthenticationHandler)),context);
            
            ///act
            var result = await _handler.AuthenticateAsync();

            ///assert
            Assert.False(result.Succeeded);
            Assert.Equal("Invalid Authorization Header", result.Failure.Message);
        }

        [Fact(DisplayName="Authorization header is invalid should return invalid authorization header")]
        public async Task AuthorizationHeaderInvalidShouldReturnInvalidAuthorizationHeader()
        {
            ///arrange
            var context = new DefaultHttpContext();
            var authorizationHeader = new StringValues("aaaaa");
            context.Request.Headers.Add(HeaderNames.Authorization, authorizationHeader);
            var _handler = new BasicAuthenticationHandler(_options.Object,_logger.Object,_encoder.Object,_clock.Object,_userRepository.Object);
            await _handler.InitializeAsync(new AuthenticationScheme(AuthenticationSchemes.Basic.ToString(), null, typeof(BasicAuthenticationHandler)),context);
            
            ///act
            var result = await _handler.AuthenticateAsync();

            ///assert
            Assert.False(result.Succeeded);
            Assert.Equal("Invalid Authorization Header", result.Failure.Message);
        }

        [Fact(DisplayName = "Invalid credentials should return fail")]
        public async Task InvalidCredentialsShouldReturnFail()
        {
            ///arrange
            var context = new DefaultHttpContext();
            var authorizationHeader = new StringValues("Basic ZGVlcGFrOnRlc3Rjb3JlMQ==");
            context.Request.Headers.Add(HeaderNames.Authorization, authorizationHeader);
            _userRepository.Setup(x => x.Authenticate("deepak1", "testcore1")).ReturnsAsync(null as User);
            var _handler = new BasicAuthenticationHandler(_options.Object, _logger.Object, _encoder.Object, _clock.Object, _userRepository.Object);
            await _handler.InitializeAsync(new AuthenticationScheme(AuthenticationSchemes.Basic.ToString(), null, typeof(BasicAuthenticationHandler)), context);
            
            ///act
            var result = await _handler.AuthenticateAsync();

            ///assert
            Assert.False(result.Succeeded);
            Assert.Equal("Invalid Username or Password", result.Failure.Message);
        }

        [Fact(DisplayName="Valid credentials should return success")]
        public async Task ValidCredentialsShouldReturnSuccess()
        {
            ///arrange
            var context = new DefaultHttpContext();
            var authorizationHeader = new StringValues("Basic ZGVlcGFrOnRlc3Rjb3JlMQ==");
            context.Request.Headers.Add(HeaderNames.Authorization, authorizationHeader);
            _userRepository.Setup(x => x.Authenticate("deepak","testcore1")).ReturnsAsync(new User{Username="deepak"});
            var _handler = new BasicAuthenticationHandler(_options.Object, _logger.Object, _encoder.Object, _clock.Object, _userRepository.Object);
            await _handler.InitializeAsync(new AuthenticationScheme(AuthenticationSchemes.Basic.ToString(), null, typeof(BasicAuthenticationHandler)), context);
            
            ///act
            var result = await _handler.AuthenticateAsync();

            ///assert
            Assert.True(result.Succeeded);
            Assert.Equal(1,result.Principal.Claims.Count());
        }
    }
}