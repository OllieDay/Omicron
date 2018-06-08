using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using MockableHttp;
using NSubstitute;
using Xunit;

namespace Omicron.Tests
{
	public sealed class RequestTests
	{
		[Fact]
		public void ShouldNotThrowExceptionWhenNoAssertionsFail()
		{
			var httpService = Substitute.For<IHttpService>();
			httpService.SendAsync(Arg.Any<HttpRequestMessage>()).ReturnsForAnyArgs(new HttpResponseMessage());
			var request = new Request(httpService, HttpMethod.Head, string.Empty);

			Action run = () => request.Assert(Stubs.Noop);

			run.Should().NotThrow();
		}
	}
}
