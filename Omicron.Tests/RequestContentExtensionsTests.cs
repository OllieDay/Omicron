using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MockableHttp;
using NSubstitute;
using Xunit;

namespace Omicron.Tests
{
	public sealed class RequestContentExtensionTests
	{
		[Fact]
		public async Task ShouldSetContent()
			=> await SetContentAndVerifyIsSet(request => request.With.Content(new StringContent("...")), "...");

		[Fact]
		public async Task ShouldSetContentToByteArrayContent()
		{
			var content = new byte[] { 1, 2, 3 };

			await SetContentAndVerifyIsSet(request => request.With.ByteArray(content), content);
		}

		[Fact]
		public async Task ShouldSetContentToByteArrayContentWithOffsetAndCount()
		{
			var offset = 1;
			var count = 1;

			var content = new byte[] { 1, 2, 3 };
			var expectedContent = content.Skip(offset).Take(count);

			await SetContentAndVerifyIsSet(request => request.With.ByteArray(content, offset, count), expectedContent);
		}

		[Fact]
		public async Task ShouldSetContentToFormUrlEncodedContent()
		{
			var content = new Dictionary<string, string>
			{
				["key"] = "value"
			};

			await SetContentAndVerifyIsSet(request => request.With.FormUrlEncoded(content), "key=value");
		}

		[Fact]
		public async Task ShouldSetContentTypeHeaderToXWwwFormUrlEncodedContentWhenContentIsFormUrlEncodedContent()
			=> await SetContentAndVerifyContentTypeHeaderIsSet(request => request.With.FormUrlEncoded(new Dictionary<string, string>()), "application/x-www-form-urlencoded");

		[Fact]
		public async Task ShouldSetContentToStringContent()
			=> await SetContentAndVerifyIsSet(request => request.With.String("..."), "...");

		[Fact]
		public async Task ShouldSetContentTypeHeaderToTextPlainWhenContentIsStringContent()
			=> await SetContentAndVerifyContentTypeHeaderIsSet(request => request.With.String("..."), "text/plain; charset=utf-8");

		[Fact]
		public async Task ShouldSetContentToStringContentWithEncoding()
			=> await SetContentAndVerifyIsSet(request => request.With.String("...", Encoding.ASCII), "...");

		[Fact]
		public async Task ShouldSetContentTypeHeaderToTextPlainWhenContentIsStringContentWithEncoding()
			=> await SetContentAndVerifyContentTypeHeaderIsSet(request => request.With.String("...", Encoding.ASCII), "text/plain; charset=us-ascii");

		[Fact]
		public async Task ShouldSetContentToStringContentWithEncodingAndMediaType()
			=> await SetContentAndVerifyIsSet(request => request.With.String("...", Encoding.ASCII, "text/plain"), "...");

		[Fact]
		public async Task ShouldSetContentTypeHeaderToTextPlainWhenContentIsStringContentWithEncodingAndMediaType()
			=> await SetContentAndVerifyContentTypeHeaderIsSet(request => request.With.String("...", Encoding.ASCII, "text/plain"), "text/plain; charset=us-ascii");

		[Fact]
		public async Task ShouldSetContentToStreamContent()
		{
			var content = new MemoryStream(new byte[] { 1, 2, 3 });

			await SetContentAndVerifyIsSet(request => request.With.Stream(content), content);
		}

		[Fact]
		public async Task ShouldSetContentToStreamContentWithBufferSize()
		{
			var content = new MemoryStream(new byte[] { 1, 2, 3 });

			await SetContentAndVerifyIsSet(request => request.With.Stream(content, 8), content);
		}

		[Fact]
		public async Task ShouldSetContentToJsonObject()
			=> await SetContentAndVerifyIsSet(request => request.With.Json(new {}), "{}");

		[Fact]
		public async Task ShouldSetContentTypeHeaderToApplicationJsonWhenContentIsJsonObject()
			=> await SetContentAndVerifyContentTypeHeaderIsSet(request => request.With.Json(new {}), "application/json; charset=utf-8");

		[Fact]
		public async Task ShouldSetContentToJsonString()
			=> await SetContentAndVerifyIsSet(request => request.With.Json("{}"), "{}");

		[Fact]
		public async Task ShouldSetContentTypeHeaderToApplicationJsonWhenContentIsJsonString()
			=> await SetContentAndVerifyContentTypeHeaderIsSet(request => request.With.Json("{}"), "application/json; charset=utf-8");

		[Fact]
		public async Task ShouldSetContentToXmlObject()
			=> await SetContentAndVerifyIsSet(request => request.With.Xml(new XDocument(new XElement("root"))), "<root />");

		[Fact]
		public async Task ShouldSetContentTypeHeaderToApplicationXmlWhenContentIsXmlObject()
			=> await SetContentAndVerifyContentTypeHeaderIsSet(request => request.With.Xml(new XDocument(new XElement("root"))), "application/xml; charset=utf-8");

		[Fact]
		public async Task ShouldSetContentToXmlString()
			=> await SetContentAndVerifyIsSet(request => request.With.Xml("<root />"), "<root />");

		[Fact]
		public async Task ShouldSetContentTypeHeaderToApplicationXmlWhenContentIsXmlString()
			=> await SetContentAndVerifyContentTypeHeaderIsSet(request => request.With.Xml("<root />"), "application/xml; charset=utf-8");

		private static async Task SetContentAndVerifyIsSet(Action<IRequest> setter, string content)
		{
			var httpService = Substitute.For<IHttpService>();
			var request = new Request(httpService, HttpMethod.Head, string.Empty);

			setter(request);
			request.Assert(Stubs.Noop);

			await httpService.Received().SendAsync(Arg.Is<HttpRequestMessage>(httpRequestMessage =>
				httpRequestMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult() == content
			));
		}

		private static async Task SetContentAndVerifyIsSet(Action<IRequest> setter, IEnumerable<byte> content)
		{
			var httpService = Substitute.For<IHttpService>();
			var request = new Request(httpService, HttpMethod.Head, string.Empty);

			setter(request);
			request.Assert(Stubs.Noop);

			await httpService.Received().SendAsync(Arg.Is<HttpRequestMessage>(httpRequestMessage =>
				httpRequestMessage.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult().SequenceEqual(content)
			));
		}

		private static async Task SetContentAndVerifyIsSet(Action<IRequest> setter, Stream content)
		{
			var httpService = Substitute.For<IHttpService>();
			var request = new Request(httpService, HttpMethod.Head, string.Empty);

			setter(request);
			request.Assert(Stubs.Noop);

			await httpService.Received().SendAsync(Arg.Is<HttpRequestMessage>(httpRequestMessage =>
				IsStreamContentSet(httpRequestMessage.Content.ReadAsStreamAsync().GetAwaiter().GetResult(), content)
			));
		}

		private static bool IsStreamContentSet(Stream actual, Stream expected)
		{
			const int bufferSize = 16;

			var actualBuffer = new byte[bufferSize];
			var expectedBuffer = new byte[bufferSize];

			actual.Read(actualBuffer, 0, bufferSize);

			// Must seek to beginning before reading
			expected.Seek(0, SeekOrigin.Begin);
			expected.Read(expectedBuffer, 0, bufferSize);

			return actualBuffer.SequenceEqual(expectedBuffer);
		}

		private static async Task SetContentAndVerifyContentTypeHeaderIsSet(Action<IRequest> setter, string value)
		{
			var httpService = Substitute.For<IHttpService>();
			var request = new Request(httpService, HttpMethod.Head, string.Empty);

			setter(request);
			request.Assert(Stubs.Noop);

			await httpService.Received().SendAsync(Arg.Is<HttpRequestMessage>(httpRequestMessage =>
				IsContentTypeHeaderSet(httpRequestMessage.Content.Headers, value)
			));
		}

		private static bool IsContentTypeHeaderSet(HttpContentHeaders headers, string value)
		{
			if (!headers.TryGetValues("Content-Type", out var values))
			{
				return false;
			}

			return values.Contains(value);
		}
	}
}
