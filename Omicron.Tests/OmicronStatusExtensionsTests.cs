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
	public sealed class OmicronStatusExtensionsTests
	{
		[Theory]
		[InlineData(100)]
		[InlineData(200)]
		[InlineData(400)]
		public void ShouldNotThrowExceptionWhenStatusAssertionSucceeds(int statusCode)
		{
			var httpService = Substitute.For<IHttpService>();
			httpService.SendAsync(Arg.Any<HttpRequestMessage>()).ReturnsForAnyArgs(new HttpResponseMessage((HttpStatusCode)statusCode));
			var request = new Request(httpService, HttpMethod.Head, string.Empty);

			Action run = () => request.Has.Status(statusCode);

			run.Should().NotThrow();
		}

		[Theory]
		[InlineData(100, 101)]
		[InlineData(200, 400)]
		[InlineData(500, 200)]
		public void ShouldThrowExceptionWhenStatusAssertionFails(int expectedStatusCode, int actualStatusCode)
		{
			var httpService = Substitute.For<IHttpService>();
			httpService.SendAsync(Arg.Any<HttpRequestMessage>()).ReturnsForAnyArgs(new HttpResponseMessage((HttpStatusCode)actualStatusCode));
			var request = new Request(httpService, HttpMethod.Head, string.Empty);

			Action run = () => request.Has.Status(expectedStatusCode);

			run.Should().Throw<OmicronException>().WithMessage($"Expected status {expectedStatusCode} but got {actualStatusCode}");
		}
	}
}
