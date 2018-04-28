using System;
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
		public void ShouldNotThrowExceptionWhenHeaderWithNameIsSet()
		{
			Action run = () => SetHeaderAndVerifyIsSet(headers => headers.Add("X-Omicron", "Omicron"), request => request.Has.Header("X-Omicron"));

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenHeaderWithNameIsNotSet()
		{
			Action run = () => SetHeaderAndVerifyIsSet(headers => headers.Add("X-Omicron", "Omicron"), request => request.Has.Header("Norcimo"));

			run.Should().Throw<Exception>().WithMessage("Expected header Norcimo");
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenHeaderWithNameAndValueIsSet()
		{
			Action run = () => SetHeaderAndVerifyIsSet(headers => headers.Add("X-Omicron", "Omicron"), request => request.Has.Header("X-Omicron", "Omicron"));

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenHeaderWithNameAndValueIsNotSet()
		{
			Action run = () => SetHeaderAndVerifyIsSet(headers => headers.Add("X-Omicron", "Omicron"), request => request.Has.Header("X-Omicron", "Norcimo"));

			run.Should().Throw<Exception>().WithMessage("Expected header X-Omicron: Norcimo but got:\n\tX-Omicron: Omicron");
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenHeaderWithPredicateIsSet()
		{
			Action run = () => SetHeaderAndVerifyIsSet(headers => headers.Add("X-Omicron", "Omicron"), request => request.Has.Header("X-Omicron", _ => true));

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenHeaderWithPredicateIsNotSet()
		{
			Action run = () => SetHeaderAndVerifyIsSet(headers => headers.Add("X-Omicron", "Omicron"), request => request.Has.Header("X-Omicron", _ => false));

			run.Should().Throw<Exception>().WithMessage("Expected header X-Omicron to match");
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
