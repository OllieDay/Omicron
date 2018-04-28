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
		public void ShouldNotThrowExceptionWhenReasonPhraseWithReasonPhraseSucceeds(int statusCode, string reasonPhrase)
		{
			var response = CreateResponseWithReasonPhrase(statusCode);

			Action run = () => response.Has.ReasonPhrase(reasonPhrase);

			run.Should().NotThrow();
		}

		[Theory]
		[InlineData(200, "OK")]
		[InlineData(400, "Bad Request")]
		[InlineData(500, "Internal Server Error")]
		public void ShouldThrowExceptionWhenReasonPhraseWithReasonPhraseFails(int statusCode, string reasonPhrase)
		{
			var response = CreateResponseWithReasonPhrase(statusCode);

			Action run = () => response.Has.ReasonPhrase("Continue");

			run.Should().Throw<Exception>().WithMessage($@"Expected reason phrase ""Continue"" but got ""{reasonPhrase}""");
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenReasonPhraseWithPredicateSucceeds()
		{
			var response = CreateResponseWithReasonPhrase(200);

			Action run = () => response.Has.ReasonPhrase(_ => true);

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenReasonPhraseWithPredicateFails()
		{
			var response = CreateResponseWithReasonPhrase(200);

			Action run = () => response.Has.ReasonPhrase(_ => false);

			run.Should().Throw<Exception>().WithMessage($@"Expected reason phrase ""OK"" to match");
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
