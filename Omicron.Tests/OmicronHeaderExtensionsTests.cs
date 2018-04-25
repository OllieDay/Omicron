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
			=> await SetHeaderAndVerifyIsSet(request => request.With.Header("X-Omicron", "Omicron 1", "Omicron 2"), "X-Omicron", "Omicron 1", "Omicron 2");

		[Fact]
		public async Task ShouldAddHeaderWithValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.Header("X-Omicron", "Omicron"), "X-Omicron", "Omicron");

		[Fact]
		public async Task ShouldAddAcceptHeaderWithMediaTypeWithQualityHeaderValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.Accept(new MediaTypeWithQualityHeaderValue("text/plain")), "Accept", "text/plain");

		[Fact]
		public async Task ShouldAddAcceptHeaderWithMediaType()
			=> await SetHeaderAndVerifyIsSet(request => request.With.Accept("text/plain"), "Accept", "text/plain");

		[Fact]
		public async Task ShouldAddAcceptHeaderWithMediaTypeAndQuality()
			=> await SetHeaderAndVerifyIsSet(request => request.With.Accept("text/plain", 0), "Accept", "text/plain; q=0.0");

		[Fact]
		public async Task ShouldAddAcceptCharsetHeaderWithStringWithQualityHeaderValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.AcceptCharset(new StringWithQualityHeaderValue("utf-8")), "Accept-Charset", "utf-8");

		[Fact]
		public async Task ShouldAddAcceptCharsetHeaderWithValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.AcceptCharset("utf-8"), "Accept-Charset", "utf-8");

		[Fact]
		public async Task ShouldAddAcceptCharsetHeaderWithValueAndQuality()
			=> await SetHeaderAndVerifyIsSet(request => request.With.AcceptCharset("utf-8", 0), "Accept-Charset", "utf-8; q=0.0");

		[Fact]
		public async Task ShouldAddAcceptEncodingHeaderWithStringWithQualityHeaderValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.AcceptEncoding(new StringWithQualityHeaderValue("gzip")), "Accept-Encoding", "gzip");

		[Fact]
		public async Task ShouldAddAcceptEncodingHeaderWithValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.AcceptEncoding("gzip"), "Accept-Encoding", "gzip");

		[Fact]
		public async Task ShouldAddAcceptEncodingHeaderWithValueAndQuality()
			=> await SetHeaderAndVerifyIsSet(request => request.With.AcceptEncoding("gzip", 0), "Accept-Encoding", "gzip; q=0.0");

		[Fact]
		public async Task ShouldAddAcceptLanguageHeaderWithStringWithQualityHeaderValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.AcceptLanguage(new StringWithQualityHeaderValue("en")), "Accept-Language", "en");

		[Fact]
		public async Task ShouldAddAcceptLanguageHeaderWithValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.AcceptLanguage("en"), "Accept-Language", "en");

		[Fact]
		public async Task ShouldAddAcceptLanguageHeaderWithValueAndQuality()
			=> await SetHeaderAndVerifyIsSet(request => request.With.AcceptLanguage("en", 0), "Accept-Language", "en; q=0.0");

		[Fact]
		public async Task ShouldAddAuthorizationHeaderWithAuthenticationHeaderValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.Authorization(new AuthenticationHeaderValue("...")), "Authorization", "...");

		[Fact]
		public async Task ShouldAddAuthorizationHeaderWithScheme()
			=> await SetHeaderAndVerifyIsSet(request => request.With.Authorization("..."), "Authorization", "...");

		[Fact]
		public async Task ShouldAddAuthorizationHeaderWithSchemeAndParameter()
			=> await SetHeaderAndVerifyIsSet(request => request.With.Authorization("Basic", "..."), "Authorization", "Basic ...");

		[Fact]
		public async Task ShouldAddCacheControlHeaderWithCacheControlHeaderValue()
		{
			var value = new CacheControlHeaderValue
			{
				NoCache = true,
				MaxAge = TimeSpan.FromSeconds(1),
				MaxStale = true
			};

			await SetHeaderAndVerifyIsSet(request => request.With.CacheControl(value), "Cache-Control", "no-cache, max-age=1, max-stale");
		}

		[Fact]
		public async Task ShouldAddConnectionHeaderWithValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.Connection("keep-alive"), "Connection", "keep-alive");

		[Fact]
		public async Task ShouldAddConnectionHeaderWithValueTrue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.ConnectionClose(true), "Connection", "close");

		[Fact]
		public async Task ShouldNotAddConnectionHeaderWithValueFalse()
			=> await SetHeaderAndVerifyIsNotSet(request => request.With.ConnectionClose(false), "Connection");

		[Fact]
		public async Task ShouldAddDateHeaderWithValue()
		{
			var value = DateTimeOffset.Now;

			await SetHeaderAndVerifyIsSet(request => request.With.Date(value), "Date", value.ToString("r"));
		}

		[Fact]
		public async Task ShouldAddExpectHeaderWithNameValueWithParametersHeaderValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.Expect(new NameValueWithParametersHeaderValue("name", "value")), "Expect", "name=value");

		[Fact]
		public async Task ShouldAddExpectHeaderWithName()
			=> await SetHeaderAndVerifyIsSet(request => request.With.Expect("..."), "Expect", "...");

		[Fact]
		public async Task ShouldAddExpectHeaderWithNameAndValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.Expect("name", "value"), "Expect", "name=value");

		[Fact]
		public async Task ShouldAddExpectHeaderWithValueTrue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.ExpectContinue(true), "Expect", "100-continue");

		[Fact]
		public async Task ShouldNotAddExpectContinueHeaderWithValueFalse()
			=> await SetHeaderAndVerifyIsNotSet(request => request.With.ExpectContinue(false), "Expect");

		[Fact]
		public async Task ShouldAddFromHeaderWithValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.From("user@example.com"), "From", "user@example.com");

		[Fact]
		public async Task ShouldAddHostHeaderWithValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.Host("example.com:80"), "Host", "example.com:80");

		[Fact]
		public async Task ShouldAddIfMatchHeaderWithEntityTagHeaderValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.IfMatch(new EntityTagHeaderValue(@"""...""")), "If-Match", @"""...""");

		[Fact]
		public async Task ShouldAddIfMatchHeaderWithTag()
			=> await SetHeaderAndVerifyIsSet(request => request.With.IfMatch(@"""..."""), "If-Match", @"""...""");

		[Fact]
		public async Task ShouldAddIfMatchHeaderWithTagAndIsWeakTrue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.IfMatch(@"""...""", true), "If-Match", @"W/""...""");

		[Fact]
		public async Task ShouldAddIfMatchHeaderWithTagAndIsWeakFalse()
			=> await SetHeaderAndVerifyIsSet(request => request.With.IfMatch(@"""...""", false), "If-Match", @"""...""");

		[Fact]
		public async Task ShouldAddIfModifiedSinceHeaderWithValue()
		{
			var value = DateTimeOffset.Now;

			await SetHeaderAndVerifyIsSet(request => request.With.IfModifiedSince(value), "If-Modified-Since", value.ToString("r"));
		}

		[Fact]
		public async Task ShouldAddIfNoneMatchHeaderWithEntityTagHeaderValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.IfNoneMatch(new EntityTagHeaderValue(@"""...""")), "If-None-Match", @"""...""");

		[Fact]
		public async Task ShouldAddIfNoneMatchHeaderWithTag()
			=> await SetHeaderAndVerifyIsSet(request => request.With.IfNoneMatch(@"""..."""), "If-None-Match", @"""...""");

		[Fact]
		public async Task ShouldAddIfNoneMatchHeaderWithTagAndIsWeakTrue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.IfNoneMatch(@"""...""", true), "If-None-Match", @"W/""...""");

		[Fact]
		public async Task ShouldAddIfNoneMatchHeaderWithTagAndIsWeakFalse()
			=> await SetHeaderAndVerifyIsSet(request => request.With.IfNoneMatch(@"""...""", false), "If-None-Match", @"""...""");

		[Fact]
		public async Task ShouldAddIfRangeHeaderWithRangeConditionHeaderValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.IfRange(new RangeConditionHeaderValue(@"""...""")), "If-Range", @"""...""");

		[Fact]
		public async Task ShouldAddIfRangeHeaderWithValue()
		{
			var value = DateTimeOffset.Now;

			await SetHeaderAndVerifyIsSet(request => request.With.IfRange(value), "If-Range", value.ToString("r"));
		}

		[Fact]
		public async Task ShouldAddIfRangeHeaderWithTag()
			=> await SetHeaderAndVerifyIsSet(request => request.With.IfRange(@"""..."""), "If-Range", @"""...""");

		[Fact]
		public async Task ShouldAddIfRangeHeaderWithTagAndIsWeakTrue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.IfRange(@"""...""", true), "If-Range", @"W/""...""");

		[Fact]
		public async Task ShouldAddIfRangeHeaderWithTagAndIsWeakFalse()
			=> await SetHeaderAndVerifyIsSet(request => request.With.IfRange(@"""...""", false), "If-Range", @"""...""");

		[Fact]
		public async Task ShouldAddIfUnmodifiedSinceHeaderWithValue()
		{
			var value = DateTimeOffset.Now;

			await SetHeaderAndVerifyIsSet(request => request.With.IfUnmodifiedSince(value), "If-Unmodified-Since", value.ToString("r"));
		}

		[Fact]
		public async Task ShouldAddMaxForwardsHeaderWithValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.MaxForwards(0), "Max-Forwards", "0");

		[Fact]
		public async Task ShouldAddPragmaHeaderWithNameValueHeaderValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.Pragma(new NameValueHeaderValue("no-cache")), "Pragma", "no-cache");

		[Fact]
		public async Task ShouldAddPragmaHeaderWithName()
			=> await SetHeaderAndVerifyIsSet(request => request.With.Pragma("no-cache"), "Pragma", "no-cache");

		[Fact]
		public async Task ShouldAddProxyAuthorizationHeaderWithAuthenticationHeaderValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.ProxyAuthorization(new AuthenticationHeaderValue("...")), "Authorization", "...");

		[Fact]
		public async Task ShouldAddProxyAuthorizationHeaderWithScheme()
			=> await SetHeaderAndVerifyIsSet(request => request.With.ProxyAuthorization("..."), "Authorization", "...");

		[Fact]
		public async Task ShouldAddProxyAuthorizationHeaderWithSchemeAndParameter()
			=> await SetHeaderAndVerifyIsSet(request => request.With.ProxyAuthorization("Basic", "..."), "Authorization", "Basic ...");

		[Fact]
		public async Task ShouldAddRangeHeaderWithRangeHeaderValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.Range(new RangeHeaderValue(0, 1)), "Range", "bytes=0-1");

		[Fact]
		public async Task ShouldAddRangeHeaderWithFromAndTo()
			=> await SetHeaderAndVerifyIsSet(request => request.With.Range(0, 1), "Range", "bytes=0-1");

		[Fact]
		public async Task ShouldAddRefererHeaderWithValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.Referrer(new Uri("https://example.com/")), "Referer", "https://example.com/");

		[Fact]
		public async Task ShouldAddRefererHeaderWithUriString()
			=> await SetHeaderAndVerifyIsSet(request => request.With.Referrer("https://example.com/"), "Referer", "https://example.com/");


		[Fact]
		public async Task ShouldAddTEHeaderWithTransferCodingWithQualityHeaderValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.TE(new TransferCodingWithQualityHeaderValue("gzip")), "TE", "gzip");

		[Fact]
		public async Task ShouldAddTEHeaderWithValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.TE("gzip"), "TE", "gzip");

		[Fact]
		public async Task ShouldAddTEHeaderWithValueAndQuality()
			=> await SetHeaderAndVerifyIsSet(request => request.With.TE("gzip", 0), "TE", "gzip; q=0.0");

		[Fact]
		public async Task ShouldAddTrailerHeaderWithValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.Trailer("Expires"), "Trailer", "Expires");

		[Fact]
		public async Task ShouldAddTransferEncodingHeaderTransferCodingHeaderValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.TransferEncoding("gzip"), "Transfer-Encoding", "gzip");

		[Fact]
		public async Task ShouldAddTransferEncodingHeaderWithValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.TransferEncoding("gzip"), "Transfer-Encoding", "gzip");

		[Fact]
		public async Task ShouldAddTransferEncodingHeaderWithValueTrue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.TransferEncodingChunked(true), "Transfer-Encoding", "chunked");

		[Fact]
		public async Task ShouldNotAddTransferEncodingHeaderWithValueFalse()
			=> await SetHeaderAndVerifyIsNotSet(request => request.With.TransferEncodingChunked(false), "Transfer-Encoding-Chunked");

		[Fact]
		public async Task ShouldAddUpgradeHeaderWithProductHeaderValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.Upgrade(new ProductHeaderValue("h2c")), "Upgrade", "h2c");

		[Fact]
		public async Task ShouldAddUpgradeHeaderWithName()
			=> await SetHeaderAndVerifyIsSet(request => request.With.Upgrade("h2c"), "Upgrade", "h2c");

		[Fact]
		public async Task ShouldAddUpgradeHeaderWithNameAndVersion()
			=> await SetHeaderAndVerifyIsSet(request => request.With.Upgrade("HTTP", "2"), "Upgrade", "HTTP/2");

		[Fact]
		public async Task ShouldAddUserAgentHeaderWithProductInfoHeaderValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.UserAgent(new ProductInfoHeaderValue("Mozilla", "1.0")), "User-Agent", "Mozilla/1.0");

		[Fact]
		public async Task ShouldAddUserAgentHeaderWithProductHeaderValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.UserAgent(new ProductHeaderValue("Mozilla", "1.0")), "User-Agent", "Mozilla/1.0");

		[Fact]
		public async Task ShouldAddUserAgentHeaderWithComment()
			=> await SetHeaderAndVerifyIsSet(request => request.With.UserAgent("(...)"), "User-Agent", "(...)");

		[Fact]
		public async Task ShouldAddUserAgentHeaderWithProductNameAndProductVersion()
			=> await SetHeaderAndVerifyIsSet(request => request.With.UserAgent("Mozilla", "1.0"), "User-Agent", "Mozilla/1.0");

		[Fact]
		public async Task ShouldAddViaHeaderWithViaHeaderValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.Via(new ViaHeaderValue("1.0", "received-by")), "Via", "1.0 received-by");

		[Fact]
		public async Task ShouldAddViaHeaderWithProtocolVersionAndReceivedBy()
			=> await SetHeaderAndVerifyIsSet(request => request.With.Via("1.0", "received-by"), "Via", "1.0 received-by");

		[Fact]
		public async Task ShouldAddViaHeaderWithProtocolVersionAndReceivedByAndProtocolName()
			=> await SetHeaderAndVerifyIsSet(request => request.With.Via("1.0", "received-by", "HTTP"), "Via", "HTTP/1.0 received-by");

		[Fact]
		public async Task ShouldAddViaHeaderWithProtocolVersionAndReceivedByAndProtocolNameAndComment()
			=> await SetHeaderAndVerifyIsSet(request => request.With.Via("1.0", "received-by", "HTTP", "(comment)"), "Via", "HTTP/1.0 received-by (comment)");

		[Fact]
		public async Task ShouldAddWarningHeaderWithWarningHeaderValue()
			=> await SetHeaderAndVerifyIsSet(request => request.With.Warning(new WarningHeaderValue(110, "agent", @"""Response is stale""")), "Warning", @"110 agent ""Response is stale""");

		[Fact]
		public async Task ShouldAddWarningHeaderWithCodeAndAgentAndText()
			=> await SetHeaderAndVerifyIsSet(request => request.With.Warning(110, "agent", @"""Response is stale"""), "Warning", @"110 agent ""Response is stale""");

		[Fact]
		public async Task ShouldAddWarningHeaderWithCodeAndAgentAndTextAndDate()
		{
			var date = DateTimeOffset.Now;

			await SetHeaderAndVerifyIsSet(request => request.With.Warning(110, "agent", @"""Response is stale""", date), "Warning", $@"110 agent ""Response is stale"" ""{date.ToString("r")}""");
		}

		private static async Task SetHeaderAndVerifyIsSet(Action<IRequest> setter, string name, params string[] expectedValues)
		{
			var httpService = Substitute.For<IHttpService>();
			var request = new Request(httpService, HttpMethod.Head, string.Empty);

			setter(request);
			request.Assert(Stubs.Noop);

			await httpService.Received().SendAsync(Arg.Is<HttpRequestMessage>(httpRequestMessage =>
				IsHeaderSet(httpRequestMessage.Headers, name, expectedValues)
			));
		}

		private static async Task SetHeaderAndVerifyIsNotSet(Action<IRequest> setter, string name)
		{
			var httpService = Substitute.For<IHttpService>();
			var request = new Request(httpService, HttpMethod.Head, string.Empty);

			setter(request);
			request.Assert(Stubs.Noop);

			await httpService.Received().SendAsync(Arg.Is<HttpRequestMessage>(httpRequestMessage =>
				!IsHeaderSet(httpRequestMessage.Headers, name)
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
