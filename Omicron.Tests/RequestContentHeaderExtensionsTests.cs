using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MockableHttp;
using NSubstitute;
using Xunit;

namespace Omicron.Tests
{
	public sealed class RequestContentHeaderExtensionsTests
	{
		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(100)]
		public async Task ShouldAddContentLengthHeader(long contentLength)
			=> await SetContentHeaderAndVerifyIsSet(request => request.With.ContentLength(contentLength), "Content-Length", $"{contentLength}");

		[Fact]
		public async Task ShouldAddContentMD5Header()
		{
			byte[] hash;

			using (var md5 = MD5.Create())
			{
				hash = md5.ComputeHash(Encoding.UTF8.GetBytes("..."));
			}

			await SetContentHeaderAndVerifyIsSet(request => request.With.ContentMD5(hash), "Content-MD5", "L0O0L9gz0ed0IKja50GQAA==");
		}

		[Fact]
		public async Task ShouldAddContentTypeHeaderWithMediaTypeHeaderValue()
			=> await SetContentHeaderAndVerifyIsSet(request => request.With.ContentType(new MediaTypeHeaderValue("application/json")), "Content-Type", "application/json");

		[Fact]
		public async Task ShouldAddContentTypeHeaderWithMediaType()
			=> await SetContentHeaderAndVerifyIsSet(request => request.With.ContentType("application/json"), "Content-Type", "application/json");

		private static async Task SetContentHeaderAndVerifyIsSet(Action<IRequest> setter, string name, string value)
		{
			var httpService = Substitute.For<IHttpService>();
			var request = new Request(httpService, HttpMethod.Head, string.Empty);

			// Must set content to something otherwise we're unable to set content headers
			request.String("...");
			setter(request);
			request.Assert(Stubs.Noop);

			await httpService.Received().SendAsync(Arg.Is<HttpRequestMessage>(httpRequestMessage =>
				IsContentHeaderSet(httpRequestMessage.Content.Headers, name, value)
			));
		}

		private static bool IsContentHeaderSet(HttpContentHeaders headers, string name, string value)
		{
			if (!headers.TryGetValues(name, out var actualValues))
			{
				return false;
			}

			return actualValues.Contains(value);
		}
	}
}
