using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FluentAssertions;
using MockableHttp;
using NSubstitute;
using Xunit;

namespace Omicron.Tests
{
	public sealed class ResponseHeaderExtensionTests
	{
		[Fact]
		public void ShouldNotThrowExceptionWhenHeaderWithNameExists()
		{
			Action run = () => SetHeaderAndVerifyIsSet(headers => headers.Add("X-Omicron", "Omicron"), request => request.Has.Header("X-Omicron"));

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenNotHeaderWithNameDoesNotExist()
		{
			Action run = () => SetHeaderAndVerifyIsSet(headers => headers.Add("X-Omicron", "Omicron"), request => request.Has.Not.Header("X-Norcimo"));

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenHeaderWithNameDoesNotExist()
		{
			Action run = () => SetHeaderAndVerifyIsSet(headers => headers.Add("X-Omicron", "Omicron"), request => request.Has.Header("X-Norcimo"));

			run.Should().Throw<Exception>().WithMessage(@"Expected header ""X-Norcimo"" to exist");
		}

		[Fact]
		public void ShouldThrowExceptionWhenNotHeaderWithNameExists()
		{
			Action run = () => SetHeaderAndVerifyIsSet(headers => headers.Add("X-Omicron", "Omicron"), request => request.Has.Not.Header("X-Omicron"));

			run.Should().Throw<Exception>().WithMessage(@"Expected header ""X-Omicron"" to not exist");
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenHeaderWithNameAndValueExists()
		{
			Action run = () => SetHeaderAndVerifyIsSet(headers => headers.Add("X-Omicron", "Omicron"), request => request.Has.Header("X-Omicron", "Omicron"));

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenNotHeaderWithNameAndValueDoesNotExist()
		{
			Action run = () => SetHeaderAndVerifyIsSet(headers => headers.Add("X-Omicron", "Omicron"), request => request.Has.Not.Header("X-Omicron", "Norcimo"));

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenHeaderWithNameAndValueDoesNotExist()
		{
			Action run = () => SetHeaderAndVerifyIsSet(headers => headers.Add("X-Omicron", "Omicron"), request => request.Has.Header("X-Omicron", "Norcimo"));

			run.Should().Throw<Exception>();
		}

		[Fact]
		public void ShouldThrowExceptionWhenNotHeaderWithNameAndValueExists()
		{
			Action run = () => SetHeaderAndVerifyIsSet(headers => headers.Add("X-Omicron", "Omicron"), request => request.Has.Not.Header("X-Omicron", "Omicron"));

			run.Should().Throw<Exception>();
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenHeaderWithPredicateMatches()
		{
			Action run = () => SetHeaderAndVerifyIsSet(headers => headers.Add("X-Omicron", "Omicron"), request => request.Has.Header("X-Omicron", _ => true));

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenHeaderWithPredicateDoesNotMatch()
		{
			Action run = () => SetHeaderAndVerifyIsSet(headers => headers.Add("X-Omicron", "Omicron"), request => request.Has.Header("X-Omicron", _ => false));

			run.Should().Throw<Exception>().WithMessage(@"Expected header ""X-Omicron"" to match");
		}

		private static void SetHeaderAndVerifyIsSet(Action<HttpResponseHeaders> setter, Action<IRequest> verifier)
		{
			var httpService = Substitute.For<IHttpService>();
			var request = new Request(httpService, HttpMethod.Head, string.Empty);
			var response = new HttpResponseMessage();

			setter(response.Headers);

			httpService.SendAsync(Arg.Any<HttpRequestMessage>()).ReturnsForAnyArgs(response);

			verifier(request);
		}
	}
}
