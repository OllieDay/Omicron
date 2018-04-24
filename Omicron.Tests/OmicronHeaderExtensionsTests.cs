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

		[Fact]
		public async Task ShouldAddAcceptCharsetHeaderWithStringWithQualityHeaderValue()
			=> await SetHeaderAndVerifyIsSet(omicron => omicron.With.AcceptCharset(new StringWithQualityHeaderValue("utf-8")), "Accept-Charset", "utf-8");

		[Fact]
		public async Task ShouldAddAcceptCharsetHeaderWithValue()
			=> await SetHeaderAndVerifyIsSet(omicron => omicron.With.AcceptCharset("utf-8"), "Accept-Charset", "utf-8");

		[Fact]
		public async Task ShouldAddAcceptCharsetHeaderWithValueAndQuality()
			=> await SetHeaderAndVerifyIsSet(omicron => omicron.With.AcceptCharset("utf-8", 0), "Accept-Charset", "utf-8; q=0.0");

		[Fact]
		public async Task ShouldAddAcceptEncodingHeaderWithStringWithQualityHeaderValue()
			=> await SetHeaderAndVerifyIsSet(omicron => omicron.With.AcceptEncoding(new StringWithQualityHeaderValue("gzip")), "Accept-Encoding", "gzip");

		[Fact]
		public async Task ShouldAddAcceptEncodingHeaderWithValue()
			=> await SetHeaderAndVerifyIsSet(omicron => omicron.With.AcceptEncoding("gzip"), "Accept-Encoding", "gzip");

		[Fact]
		public async Task ShouldAddAcceptEncodingHeaderWithValueAndQuality()
			=> await SetHeaderAndVerifyIsSet(omicron => omicron.With.AcceptEncoding("gzip", 0), "Accept-Encoding", "gzip; q=0.0");

		[Fact]
		public async Task ShouldAddAcceptLanguageHeaderWithStringWithQualityHeaderValue()
			=> await SetHeaderAndVerifyIsSet(omicron => omicron.With.AcceptLanguage(new StringWithQualityHeaderValue("en")), "Accept-Language", "en");

		[Fact]
		public async Task ShouldAddAcceptLanguageHeaderWithValue()
			=> await SetHeaderAndVerifyIsSet(omicron => omicron.With.AcceptLanguage("en"), "Accept-Language", "en");

		[Fact]
		public async Task ShouldAddAcceptLanguageHeaderWithValueAndQuality()
			=> await SetHeaderAndVerifyIsSet(omicron => omicron.With.AcceptLanguage("en", 0), "Accept-Language", "en; q=0.0");

		[Fact]
		public async Task ShouldAddAuthorizationHeaderWithAuthenticationHeaderValue()
			=> await SetHeaderAndVerifyIsSet(omicron => omicron.With.Authorization(new AuthenticationHeaderValue("...")), "Authorization", "...");

		[Fact]
		public async Task ShouldAddAuthorizationHeaderWithScheme()
			=> await SetHeaderAndVerifyIsSet(omicron => omicron.With.Authorization("..."), "Authorization", "...");

		[Fact]
		public async Task ShouldAddAuthorizationHeaderWithSchemeAndParameter()
			=> await SetHeaderAndVerifyIsSet(omicron => omicron.With.Authorization("Basic", "..."), "Authorization", "Basic ...");

		[Fact]
		public async Task ShouldAddCacheControlHeaderWithCacheControlHeaderValue()
		{
			var value = new CacheControlHeaderValue
			{
				NoCache = true,
				MaxAge = TimeSpan.FromSeconds(1),
				MaxStale = true
			};

			await SetHeaderAndVerifyIsSet(omicron => omicron.With.CacheControl(value), "Cache-Control", "no-cache, max-age=1, max-stale");
		}

		[Fact]
		public async Task ShouldAddConnectionHeaderWithValue()
			=> await SetHeaderAndVerifyIsSet(omicron => omicron.With.Connection("keep-alive"), "Connection", "keep-alive");

		[Fact]
		public async Task ShouldAddConnectionCloseHeaderWithValue()
			=> await SetHeaderAndVerifyIsSet(omicron => omicron.With.ConnectionClose(true), "Connection", "close");

		[Fact]
		public async Task ShouldNotAddConnectionCloseHeaderWithValue()
			=> await SetHeaderAndVerifyIsNotSet(omicron => omicron.With.ConnectionClose(false), "Connection");

		[Fact]
		public async Task ShouldAddDateHeaderWithValue()
		{
			var value = DateTimeOffset.Now;

			await SetHeaderAndVerifyIsSet(omicron => omicron.With.Date(value), "Date", value.ToString("r"));
		}

		[Fact]
		public async Task ShouldAddExpectHeaderWithNameValueWithParametersHeaderValue()
			=> await SetHeaderAndVerifyIsSet(omicron => omicron.With.Expect(new NameValueWithParametersHeaderValue("name", "value")), "Expect", "name=value");

		[Fact]
		public async Task ShouldAddExpectHeaderWithName()
			=> await SetHeaderAndVerifyIsSet(omicron => omicron.With.Expect("..."), "Expect", "...");

		[Fact]
		public async Task ShouldAddExpectHeaderWithNameAndValue()
			=> await SetHeaderAndVerifyIsSet(omicron => omicron.With.Expect("name", "value"), "Expect", "name=value");

		[Fact]
		public async Task ShouldAddExpectContinueHeaderWithNameAndValue()
			=> await SetHeaderAndVerifyIsSet(omicron => omicron.With.ExpectContinue(true), "Expect", "100-continue");

		[Fact]
		public async Task ShouldNotAddExpectContinueHeaderWithNameAndValue()
			=> await SetHeaderAndVerifyIsNotSet(omicron => omicron.With.ExpectContinue(false), "Expect");

		[Fact]
		public async Task ShouldAddFromHeaderWithValue()
			=> await SetHeaderAndVerifyIsSet(omicron => omicron.With.From("user@example.com"), "From", "user@example.com");

		[Fact]
		public async Task ShouldAddHostHeaderWithValue()
			=> await SetHeaderAndVerifyIsSet(omicron => omicron.With.Host("example.com:80"), "Host", "example.com:80");

		[Fact]
		public async Task ShouldAddIfMatchHeaderWithEntityTagHeaderValue()
			=> await SetHeaderAndVerifyIsSet(omicron => omicron.With.IfMatch(new EntityTagHeaderValue(@"""abc""")), "If-Match", @"""abc""");

		[Fact]
		public async Task ShouldAddIfMatchHeaderWithTag()
			=> await SetHeaderAndVerifyIsSet(omicron => omicron.With.IfMatch(@"""abc"""), "If-Match", @"""abc""");

		[Fact]
		public async Task ShouldAddIfMatchHeaderWithTagAndIsWeakTrue()
			=> await SetHeaderAndVerifyIsSet(omicron => omicron.With.IfMatch(@"""abc""", true), "If-Match", @"W/""abc""");

		[Fact]
		public async Task ShouldAddIfMatchHeaderWithTagAndIsWeakFalse()
			=> await SetHeaderAndVerifyIsSet(omicron => omicron.With.IfMatch(@"""abc""", false), "If-Match", @"""abc""");

		private static async Task SetHeaderAndVerifyIsSet(Action<Omicron> setter, string name, params string[] expectedValues)
		{
			var httpService = Substitute.For<IHttpService>();
			var omicron = new Omicron(httpService, HttpMethod.Head, string.Empty);

			setter(omicron);

			await omicron.Run();

			await httpService.Received().SendAsync(Arg.Is<HttpRequestMessage>(request =>
				IsHeaderSet(request.Headers, name, expectedValues)
			));
		}

		private static async Task SetHeaderAndVerifyIsNotSet(Action<Omicron> setter, string name)
		{
			var httpService = Substitute.For<IHttpService>();
			var omicron = new Omicron(httpService, HttpMethod.Head, string.Empty);

			setter(omicron);

			await omicron.Run();

			await httpService.Received().SendAsync(Arg.Is<HttpRequestMessage>(request =>
				!IsHeaderSet(request.Headers, name)
			));
		}

		private static bool IsHeaderSet(HttpRequestHeaders headers, string name, params string[] expectedValues)
		{
			if (!headers.TryGetValues(name, out var actualValues))
			{
				return false;
			}

			return actualValues.SequenceEqual(expectedValues);
		}
	}
}
