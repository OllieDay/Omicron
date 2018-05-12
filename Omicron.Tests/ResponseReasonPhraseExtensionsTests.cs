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
	public sealed class ResponseReasonPhraseExtensionTests
	{
		[Theory]
		[InlineData(200, "OK")]
		[InlineData(400, "Bad Request")]
		[InlineData(500, "Internal Server Error")]
		public void ShouldNotThrowExceptionWhenReasonPhraseWithStringMatches(int statusCode, string reasonPhrase)
		{
			var response = CreateResponseWithReasonPhrase(statusCode);

			Action run = () => response.Has.ReasonPhrase(reasonPhrase);

			run.Should().NotThrow();
		}

		[Theory]
		[InlineData(200, "Internal Server Error")]
		[InlineData(400, "OK")]
		[InlineData(500, "Bad Request")]
		public void ShouldNotThrowExceptionWhenNotReasonPhraseWithStringDoesNotMatch(int statusCode, string reasonPhrase)
		{
			var response = CreateResponseWithReasonPhrase(statusCode);

			Action run = () => response.Has.Not.ReasonPhrase(reasonPhrase);

			run.Should().NotThrow();
		}

		[Theory]
		[InlineData(200, "OK")]
		[InlineData(400, "Bad Request")]
		[InlineData(500, "Internal Server Error")]
		public void ShouldThrowExceptionWhenReasonPhraseWithStringDoesNotMatch(int statusCode, string reasonPhrase)
		{
			var response = CreateResponseWithReasonPhrase(statusCode);

			Action run = () => response.Has.ReasonPhrase("Continue");

			run.Should().Throw<Exception>().WithMessage($@"Expected reason phrase ""Continue"" but got ""{reasonPhrase}""");
		}

		[Theory]
		[InlineData(200, "OK")]
		[InlineData(400, "Bad Request")]
		[InlineData(500, "Internal Server Error")]
		public void ShouldThrowExceptionWhenNotReasonPhraseWithStringMatches(int statusCode, string reasonPhrase)
		{
			var response = CreateResponseWithReasonPhrase(statusCode);

			Action run = () => response.Has.Not.ReasonPhrase(reasonPhrase);

			run.Should().Throw<Exception>().WithMessage($@"Expected reason phrase to not be ""{reasonPhrase}""");
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenReasonPhraseWithPredicateMatches()
		{
			var response = CreateResponseWithReasonPhrase(200);

			Action run = () => response.Has.ReasonPhrase(_ => true);

			run.Should().NotThrow();
		}

		[Theory]
		[InlineData(200, "OK")]
		[InlineData(400, "Bad Request")]
		[InlineData(500, "Internal Server Error")]
		public void ShouldThrowExceptionWhenReasonPhraseWithPredicateDoestNotMatch(int statusCode, string reasonPhrase)
		{
			var response = CreateResponseWithReasonPhrase(statusCode);

			Action run = () => response.Has.ReasonPhrase(_ => false);

			run.Should().Throw<Exception>().WithMessage($@"Expected reason phrase ""{reasonPhrase}"" to match");
		}

		private static IResponse CreateResponseWithReasonPhrase(int statusCode)
		{
			var httpService = Substitute.For<IHttpService>();
			httpService.SendAsync(Arg.Any<HttpRequestMessage>()).ReturnsForAnyArgs(new HttpResponseMessage((HttpStatusCode)statusCode));
			var request = new Request(httpService, HttpMethod.Head, string.Empty);

			return request.Assert(Stubs.Noop);
		}
	}
}
