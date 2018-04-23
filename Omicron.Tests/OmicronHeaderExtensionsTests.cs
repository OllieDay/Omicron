using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MockableHttp;
using NSubstitute;
using Xunit;

namespace Omicron.Tests
{
	public sealed class OmicronHeaderExtensionsTests
	{
		[Fact]
		public async Task ShouldAddHeaderWithValues()
			=> await SetHeaderAndVerifyIsSet(omicron => omicron.With.Header("X-Omicron", "Omicron 1", "Omicron 2"), "X-Omicron", "Omicron 1", "Omicron 2");

		[Fact]
		public async Task ShouldAddHeaderWithValue()
			=> await SetHeaderAndVerifyIsSet(omicron => omicron.With.Header("X-Omicron", "Omicron"), "X-Omicron", "Omicron");

		[Fact]
		public async Task ShouldAddAcceptHeaderWithMediaTypeWithQualityHeaderValue()
			=> await SetHeaderAndVerifyIsSet(omicron => omicron.With.Accept(new MediaTypeWithQualityHeaderValue("text/plain")), "Accept", "text/plain");

		[Fact]
		public async Task ShouldAddAcceptHeaderWithMediaType()
			=> await SetHeaderAndVerifyIsSet(omicron => omicron.With.Accept("text/plain"), "Accept", "text/plain");

		[Fact]
		public async Task ShouldAddAcceptHeaderWithMediaTypeAndQuality()
			=> await SetHeaderAndVerifyIsSet(omicron => omicron.With.Accept("text/plain", 0), "Accept", "text/plain; q=0.0");

		private async Task SetHeaderAndVerifyIsSet(Action<Omicron> setter, string name, params string[] expectedValues)
		{
			var httpService = Substitute.For<IHttpService>();
			var omicron = new Omicron(httpService, HttpMethod.Head, string.Empty);

			setter(omicron);

			await omicron.Run();

			await httpService.Received().SendAsync(Arg.Is<HttpRequestMessage>(request =>
				IsHeaderSet(request.Headers, name, expectedValues)
			));
		}

		private bool IsHeaderSet(HttpRequestHeaders headers, string name, params string[] expectedValues)
		{
			if (!headers.TryGetValues(name, out var actualValues))
			{
				return false;
			}

			return actualValues.SequenceEqual(expectedValues);
		}
	}
}
