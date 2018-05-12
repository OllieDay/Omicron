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
		[InlineData(0, 9)]
		[InlineData(1, 0)]
		[InlineData(1, 1)]
		[InlineData(2, 0)]
		public void ShouldNotThrowExceptionWhenVersionWithVersionMatches(int major, int minor)
		{
			var version = new Version(major, minor);
			var response = CreateResponseWithVersion(version);

			Action run = () => response.Has.Version(version);

			run.Should().NotThrow();
		}

		[Theory]
		[InlineData(0, 9)]
		[InlineData(1, 0)]
		[InlineData(1, 1)]
		[InlineData(2, 0)]
		public void ShouldNotThrowExceptionWhenNotVersionWithVersionDoesNotMatch(int major, int minor)
		{
			var version = new Version(major, minor);
			var response = CreateResponseWithVersion(version);

			Action run = () => response.Has.Not.Version(new Version(0, 0));

			run.Should().NotThrow();
		}

		[Theory]
		[InlineData(0, 9)]
		[InlineData(1, 0)]
		[InlineData(1, 1)]
		[InlineData(2, 0)]
		public void ShouldThrowExceptionWhenVersionWithVersionDoesNotMatch(int major, int minor)
		{
			var version = new Version(major, minor);
			var response = CreateResponseWithVersion(new Version(0, 0));

			Action run = () => response.Has.Version(version);

			run.Should().Throw<Exception>().WithMessage($@"Expected version ""{version}"" but got ""0.0""");
		}

		[Theory]
		[InlineData(0, 9)]
		[InlineData(1, 0)]
		[InlineData(1, 1)]
		[InlineData(2, 0)]
		public void ShouldThrowExceptionWhenNotVersionWithVersionMatches(int major, int minor)
		{
			var version = new Version(major, minor);
			var response = CreateResponseWithVersion(version);

			Action run = () => response.Has.Not.Version(version);

			run.Should().Throw<Exception>().WithMessage($@"Expected version to not be ""{version}""");
		}

		[Theory]
		[InlineData("0.9")]
		[InlineData("1.0")]
		[InlineData("1.1")]
		[InlineData("2.0")]
		public void ShouldNotThrowExceptionWhenVersionWithStringMatches(string version)
		{
			var response = CreateResponseWithVersion(new Version(version));

			Action run = () => response.Has.Version(version);

			run.Should().NotThrow();
		}

		[Theory]
		[InlineData("0.9")]
		[InlineData("1.0")]
		[InlineData("1.1")]
		[InlineData("2.0")]
		public void ShouldNotThrowExceptionWhenNotVersionWithStringDoesNotMatch(string version)
		{
			var response = CreateResponseWithVersion(new Version(version));

			Action run = () => response.Has.Not.Version("0.0");

			run.Should().NotThrow();
		}

		[Theory]
		[InlineData("0.9")]
		[InlineData("1.0")]
		[InlineData("1.1")]
		[InlineData("2.0")]
		public void ShouldThrowExceptionWhenVersionWithStringDoesNotMatch(string version)
		{
			var response = CreateResponseWithVersion(new Version(0, 0));

			Action run = () => response.Has.Version(version);

			run.Should().Throw<Exception>().WithMessage($@"Expected version ""{version}"" but got ""0.0""");
		}

		[Theory]
		[InlineData("0.9")]
		[InlineData("1.0")]
		[InlineData("1.1")]
		[InlineData("2.0")]
		public void ShouldThrowExceptionWhenNotVersionWithStringMatches(string version)
		{
			var response = CreateResponseWithVersion(new Version(version));

			Action run = () => response.Has.Not.Version(version);

			run.Should().Throw<Exception>().WithMessage($@"Expected version to not be ""{version}""");
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenVersionWithPredicateMatches()
		{
			var response = CreateResponseWithVersion(new Version(1, 0));

			Action run = () => response.Has.Version(_ => true);

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenVersionWithPredicateDoesNotMatch()
		{
			var version = new Version(1, 0);
			var response = CreateResponseWithVersion(version);

			Action run = () => response.Has.Version(_ => false);

			run.Should().Throw<Exception>().WithMessage($@"Expected version ""{version}"" to match");
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
