using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace Omicron
{
	public static class ResponseHeaderExtensions
	{
		public static IResponse Header(this IHas @this, string name)
			=> @this.Header(name, _ => true);

		public static IResponse Header(this IHas @this, string name, params string[] values)
		{
			return @this.Assert(response =>
			{
				// Standard headers
				if (!response.Headers.TryGetValues(name, out var headerValues))
				{
					// Content headers
					if (!response.Content.Headers.TryGetValues(name, out headerValues))
					{
						throw new OmicronException($@"Expected header ""{name}""");
					}
				}

				if (values.Except(headerValues).Any())
				{
					var message = new StringBuilder($@"Expected header:");

					foreach (var expectedHeaderValue in values)
					{
						message.Append($@"\n""{name}: {expectedHeaderValue}""");
					}

					message.Append("\nbut got:");

					foreach (var actualHeaderValue in headerValues)
					{
						message.Append($@"\n""{name}: {actualHeaderValue}""");
					}

					throw new OmicronException(message.ToString());
				}
			});
		}

		public static IResponse Header(this IHas @this, string name, Func<IEnumerable<string>, bool> predicate)
		{
			return @this.Assert(response =>
			{
				// Standard headers
				if (!response.Headers.TryGetValues(name, out var headerValues))
				{
					if (response.Content == null)
					{
						throw new OmicronException($@"Expected header ""{name}""");
					}

					// Content headers
					if (!response.Content.Headers.TryGetValues(name, out headerValues))
					{
						throw new OmicronException($@"Expected header ""{name}""");
					}
				}

				if (!predicate(headerValues))
				{
					throw new OmicronException($@"Expected header ""{name}"" to match");
				}
			});
		}

		public static IResponse AcceptRanges(this IHas @this)
			=> @this.Header("Accept-Ranges");

		public static IResponse AcceptRanges(this IHas @this, params string[] values)
			=> @this.Header("Accept-Ranges", values);

		public static IResponse AcceptRanges(this IHas @this, Func<HttpHeaderValueCollection<string>, bool> predicate)
			=> @this.AssertHeader("Accept-Ranges", headers => headers.AcceptRanges, predicate);

		public static IResponse Via(this IHas @this)
			=> @this.Header("Via");

		public static IResponse Via(this IHas @this, params string[] values)
			=> @this.Header("Via", values);

		public static IResponse Via(this IHas @this, Func<HttpHeaderValueCollection<ViaHeaderValue>, bool> predicate)
			=> @this.AssertHeader("Via", headers => headers.Via, predicate);

		public static IResponse Vary(this IHas @this)
			=> @this.Header("Vary");

		public static IResponse Vary(this IHas @this, params string[] values)
			=> @this.Header("Vary", values);

		public static IResponse Vary(this IHas @this, Func<HttpHeaderValueCollection<string>, bool> predicate)
			=> @this.AssertHeader("Vary", headers => headers.Vary, predicate);

		public static IResponse Upgrade(this IHas @this)
			=> @this.Header("Upgrade");

		public static IResponse Upgrade(this IHas @this, params string[] values)
			=> @this.Header("Upgrade", values);

		public static IResponse Upgrade(this IHas @this, Func<HttpHeaderValueCollection<ProductHeaderValue>, bool> predicate)
			=> @this.AssertHeader("Upgrade", headers => headers.Upgrade, predicate);

		public static IResponse TransferEncodingChunked(this IHas @this)
			=> @this.Header("Transfer-Encoding", "chunked");

		public static IResponse TransferEncoding(this IHas @this)
			=> @this.Header("Transfer-Encoding");

		public static IResponse TransferEncoding(this IHas @this, params string[] values)
			=> @this.Header("Transfer-Encoding", values);

		public static IResponse TransferEncoding(this IHas @this, Func<HttpHeaderValueCollection<TransferCodingHeaderValue>, bool> predicate)
			=> @this.AssertHeader("Transfer-Encoding", headers => headers.TransferEncoding, predicate);

		public static IResponse Trailer(this IHas @this)
			=> @this.Header("Trailer");

		public static IResponse Trailer(this IHas @this, params string[] values)
			=> @this.Header("Trailer", values);

		public static IResponse Trailer(this IHas @this, Func<HttpHeaderValueCollection<string>, bool> predicate)
			=> @this.AssertHeader("Trailer", headers => headers.Trailer, predicate);

		public static IResponse Server(this IHas @this)
			=> @this.Header("Server");

		public static IResponse Server(this IHas @this, params string[] values)
			=> @this.Header("Server", values);

		public static IResponse Server(this IHas @this, Func<HttpHeaderValueCollection<ProductInfoHeaderValue>, bool> predicate)
			=> @this.AssertHeader("Server", headers => headers.Server, predicate);

		public static IResponse RetryAfter(this IHas @this)
			=> @this.Header("Retry-After");

		public static IResponse RetryAfter(this IHas @this, string value)
			=> @this.Header("Retry-After", value);

		public static IResponse RetryAfter(this IHas @this, Func<RetryConditionHeaderValue, bool> predicate)
			=> @this.AssertHeader("Retry-After", headers => headers.RetryAfter, predicate);

		public static IResponse ProxyAuthenticate(this IHas @this)
			=> @this.Header("Proxy-Authenticate");

		public static IResponse ProxyAuthenticate(this IHas @this, params string[] values)
			=> @this.Header("Proxy-Authenticate", values);

		public static IResponse ProxyAuthenticate(this IHas @this, Func<HttpHeaderValueCollection<AuthenticationHeaderValue>, bool> predicate)
			=> @this.AssertHeader("Proxy-Authenticate", headers => headers.ProxyAuthenticate, predicate);

		public static IResponse Pragma(this IHas @this)
			=> @this.Header("Pragma");

		public static IResponse Pragma(this IHas @this, params string[] values)
			=> @this.Header("Pragma", values);

		public static IResponse Pragma(this IHas @this, Func<HttpHeaderValueCollection<NameValueHeaderValue>, bool> predicate)
			=> @this.AssertHeader("Pragma", headers => headers.Pragma, predicate);

		public static IResponse Location(this IHas @this)
			=> @this.Header("Location");

		public static IResponse Location(this IHas @this, string value)
			=> @this.Header("Location", value);

		public static IResponse Location(this IHas @this, Func<Uri, bool> predicate)
			=> @this.AssertHeader("Location", headers => headers.Location, predicate);

		public static IResponse ETag(this IHas @this)
			=> @this.Header("ETag");

		public static IResponse ETag(this IHas @this, string value)
			=> @this.Header("ETag", value);

		public static IResponse ETag(this IHas @this, Func<EntityTagHeaderValue, bool> predicate)
			=> @this.AssertHeader("ETag", headers => headers.ETag, predicate);

		public static IResponse Date(this IHas @this)
			=> @this.Header("Date");

		public static IResponse Date(this IHas @this, string value)
			=> @this.Header("Date", value);

		public static IResponse Date(this IHas @this, Func<DateTimeOffset?, bool> predicate)
			=> @this.AssertHeader("Date", headers => headers.Date, predicate);

		public static IResponse ConnectionClose(this IHas @this)
			=> @this.Header("Connection", "close");

		public static IResponse Connection(this IHas @this)
			=> @this.Header("Connection");

		public static IResponse Connection(this IHas @this, params string[] values)
			=> @this.Header("Connection", values);

		public static IResponse Connection(this IHas @this, Func<HttpHeaderValueCollection<string>, bool> predicate)
			=> @this.AssertHeader("Connection", headers => headers.Connection, predicate);

		public static IResponse CacheControl(this IHas @this)
			=> @this.Header("Cache-Control");

		public static IResponse CacheControl(this IHas @this, string value)
			=> @this.Header("Cache-Control", value);

		public static IResponse CacheControl(this IHas @this, Func<CacheControlHeaderValue, bool> predicate)
			=> @this.AssertHeader("Cache-Control", headers => headers.CacheControl, predicate);

		public static IResponse Age(this IHas @this)
			=> @this.Header("Age");

		public static IResponse Age(this IHas @this, string value)
			=> @this.Header("Age", value);

		public static IResponse Age(this IHas @this, Func<TimeSpan?, bool> predicate)
			=> @this.AssertHeader("Age", headers => headers.Age, predicate);

		public static IResponse Warning(this IHas @this)
			=> @this.Header("Warning");

		public static IResponse Warning(this IHas @this, params string[] values)
			=> @this.Header("Warning", values);

		public static IResponse Warning(this IHas @this, Func<HttpHeaderValueCollection<WarningHeaderValue>, bool> predicate)
			=> @this.AssertHeader("Warning", headers => headers.Warning, predicate);

		public static IResponse WwwAuthenticate(this IHas @this)
			=> @this.Header("WWW-Authenticate");

		public static IResponse WwwAuthenticate(this IHas @this, params string[] values)
			=> @this.Header("WWW-Authenticate", values);

		public static IResponse WwwAuthenticate(this IHas @this, Func<HttpHeaderValueCollection<AuthenticationHeaderValue>, bool> predicate)
			=> @this.AssertHeader("WWW-Authenticate", headers => headers.WwwAuthenticate, predicate);

		private static IResponse AssertHeader<T>(this IHas @this, string name, Func<HttpResponseHeaders, T> selector, Func<T, bool> predicate)
		{
			// Check header exists before asserting
			return @this.Header(name).Assert(response =>
			{
				var value = selector(response.Headers);

				if (!predicate(value))
				{
					throw new OmicronException($@"Expected header ""{name}"" to match");
				}
			});
		}
	}
}
