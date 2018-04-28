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
		public void ShouldNotThrowExceptionWhenReasonPhraseSucceeds(int statusCode, string reasonPhrase)
		{
			var httpService = Substitute.For<IHttpService>();

			httpService.SendAsync(Arg.Any<HttpRequestMessage>()).ReturnsForAnyArgs(new HttpResponseMessage
			{
				StatusCode = (HttpStatusCode)statusCode
			});

			var request = new Request(httpService, HttpMethod.Head, string.Empty);

			Action run = () => request.Has.ReasonPhrase(reasonPhrase);

			run.Should().NotThrow();
		}

		[Theory]
		[InlineData(200, "OK")]
		[InlineData(400, "Bad Request")]
		[InlineData(500, "Internal Server Error")]
		public void ShouldThrowExceptionWhenReasonPhraseFails(int statusCode, string reasonPhrase)
		{
			var httpService = Substitute.For<IHttpService>();

			httpService.SendAsync(Arg.Any<HttpRequestMessage>()).ReturnsForAnyArgs(new HttpResponseMessage
			{
				StatusCode = (HttpStatusCode)statusCode
			});

			var request = new Request(httpService, HttpMethod.Head, string.Empty);

			Action run = () => request.Has.ReasonPhrase("Continue");

			run.Should().Throw<Exception>().WithMessage($@"Expected reason phrase ""Continue"" but got ""{reasonPhrase}""");
		}
	}
}
