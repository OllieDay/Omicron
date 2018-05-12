using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using MockableHttp;
using NSubstitute;
using Xunit;

namespace Omicron.Tests
{
	public sealed class ResponseStatusExtensionsTests
	{
		[Theory]
		[InlineData(100)]
		[InlineData(200)]
		[InlineData(400)]
		public void ShouldNotThrowExceptionWhenStatusWithIntMatches(int statusCode)
		{
			var response = CreateResponseWithStatusCode(statusCode);

			Action run = () => response.Has.Status(statusCode);

			run.Should().NotThrow();
		}

		[Theory]
		[InlineData(100)]
		[InlineData(200)]
		[InlineData(400)]
		public void ShouldNotThrowExceptionWhenNotStatusWithIntDoesNotMatch(int statusCode)
		{
			var response = CreateResponseWithStatusCode(statusCode);

			Action run = () => response.Has.Not.Status(0);

			run.Should().NotThrow();
		}

		[Theory]
		[InlineData(100, 101)]
		[InlineData(200, 400)]
		[InlineData(500, 200)]
		public void ShouldThrowExceptionWhenStatusWithIntDoesNotMatch(int expectedStatusCode, int actualStatusCode)
		{
			var response = CreateResponseWithStatusCode(actualStatusCode);

			Action run = () => response.Has.Status(expectedStatusCode);

			run.Should().Throw<Exception>().WithMessage($@"Expected status ""{expectedStatusCode}"" but got ""{actualStatusCode}""");
		}

		[Theory]
		[InlineData(100)]
		[InlineData(200)]
		[InlineData(500)]
		public void ShouldThrowExceptionWhenNotStatusWithIntMatches(int statusCode)
		{
			var response = CreateResponseWithStatusCode(statusCode);

			Action run = () => response.Has.Not.Status(statusCode);

			run.Should().Throw<Exception>().WithMessage($@"Expected status to not be ""{statusCode}""");
		}

		[Theory]
		[InlineData(100)]
		[InlineData(200)]
		[InlineData(400)]
		public void ShouldNotThrowExceptionWhenStatusWithPredicateMatches(int statusCode)
		{
			var response = CreateResponseWithStatusCode(statusCode);

			Action run = () => response.Has.Status(_ => true);

			run.Should().NotThrow();
		}

		[Theory]
		[InlineData(100)]
		[InlineData(200)]
		[InlineData(400)]
		public void ShouldThrowExceptionWhenStatusWithPredicateDoesNotMatch(int statusCode)
		{
			var response = CreateResponseWithStatusCode(statusCode);

			Action run = () => response.Has.Status(_ => false);

			run.Should().Throw<Exception>().WithMessage($@"Expected status ""{statusCode}"" to match");
		}

		private static IResponse CreateResponseWithStatusCode(int statusCode)
		{
			var httpService = Substitute.For<IHttpService>();
			httpService.SendAsync(Arg.Any<HttpRequestMessage>()).ReturnsForAnyArgs(new HttpResponseMessage((HttpStatusCode)statusCode));
			var request = new Request(httpService, HttpMethod.Head, string.Empty);

			return request.Assert(Stubs.Noop);
		}
	}
}
