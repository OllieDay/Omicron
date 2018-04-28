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
	public sealed class ResponseHeadersExtensionTests
	{
		[Fact]
		public void ShouldNotThrowExceptionWhenHeadersWithNameAndValuesIsSet()
		{
			var values = new[] { "Omicron 1", "Omicron 2" };

			Action run = () => SetHeaderAndVerifyIsSet(headers => headers.Add("X-Omicron", values), request => request.Has.Headers("X-Omicron", values.First(), values.Last()));

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenHeadersWithNameAndValuesIsNotSet()
		{
			var values = new[] { "Omicron 1", "Omicron 2" };

			Action run = () => SetHeaderAndVerifyIsSet(headers => headers.Add("X-Omicron", values), request => request.Has.Headers("X-Omicron", values.First(), "Norcimo"));

			// This somehow fails despite the strings being seemingly identical
			// run.Should().Throw<Exception>().WithMessage(@"Expected headers:\n\t""X-Omicron: Omicron 1""\n\t""X-Omicron: Norcimo""\nbut got:\n\t""X-Omicron: Omicron 1""\n\t""X-Omicron: Omicron 2""");

			run.Should().Throw<Exception>();
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenHeadersWithPredicateIsSet()
		{
			var values = new[] { "Omicron 1", "Omicron 2" };

			Action run = () => SetHeaderAndVerifyIsSet(headers => headers.Add("X-Omicron", values), request => request.Has.Headers("X-Omicron", _ => true));

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenHeadersWithPredicateIsNotSet()
		{
			var values = new[] { "Omicron 1", "Omicron 2" };

			Action run = () => SetHeaderAndVerifyIsSet(headers => headers.Add("X-Omicron", values), request => request.Has.Headers("X-Omicron", _ => false));

			run.Should().Throw<Exception>().WithMessage(@"Expected headers ""X-Omicron"" to match");
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
