using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using MockableHttp;
using NSubstitute;
using Xunit;

namespace Omicron.Tests
{
	public sealed class ResponseVersionExtensionTests
	{
		[Theory]
		[InlineData(1, 0)]
		[InlineData(1, 1)]
		[InlineData(2, 0)]
		public void ShouldNotThrowExceptionWhenVersionSucceeds(int major, int minor)
		{
			var version = new Version(major, minor);
			var response = CreateResponseWithVersion(version);

			Action run = () => response.Has.Version(version);

			run.Should().NotThrow();
		}

		[Theory]
		[InlineData(1, 0)]
		[InlineData(1, 1)]
		[InlineData(2, 0)]
		public void ShouldThrowExceptionWhenVersionFails(int major, int minor)
		{
			var version = new Version(major, minor);
			var response = CreateResponseWithVersion(new Version(0, 0));

			Action run = () => response.Has.Version(version);

			run.Should().Throw<Exception>().WithMessage($@"Expected version ""{version}"" but got ""0.0""");
		}

		private static IResponse CreateResponseWithVersion(Version version)
		{
			var httpService = Substitute.For<IHttpService>();

			httpService.SendAsync(Arg.Any<HttpRequestMessage>()).ReturnsForAnyArgs(new HttpResponseMessage
			{
				Version = version
			});

			var request = new Request(httpService, HttpMethod.Head, string.Empty);

			return request.Assert(Stubs.Noop);
		}
	}
}
