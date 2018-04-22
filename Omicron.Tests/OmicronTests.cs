using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using MockableHttp;
using NSubstitute;
using Xunit;

namespace Omicron.Tests
{
	public sealed class OmicronTests
	{
		[Fact]
		public void ShouldNotThrowExceptionWhenNoAssertionsAreSet()
		{
			var httpService = Substitute.For<IHttpService>();
			httpService.SendAsync(Arg.Any<HttpRequestMessage>()).ReturnsForAnyArgs(new HttpResponseMessage());

			var omicron = new Omicron(httpService, HttpMethod.Head, string.Empty);

			Func<Task> run = omicron.Run;

			run.Should().NotThrow();
		}
	}
}
