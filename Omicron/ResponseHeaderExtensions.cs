using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Omicron
{
	public static class ResponseHeaderExtensions
	{
		public static IResponse Header(this IHas @this, string name)
		{
			@this.AssertPositive(AssertHeaderExists(name));
			@this.AssertNegative(AssertHeaderDoesNotExist(name));

			return (IResponse)@this;
		}

		public static IResponse Header(this IHas @this, string name, string value)
		{
			@this.AssertPositive(AssertHeaderValueEqual(name, value));
			@this.AssertNegative(AssertHeaderValueNotEqual(name, value));

			return (IResponse)@this;
		}

		public static IResponse Header(this IHas @this, string name, Func<IEnumerable<string>, bool> predicate)
			=> @this.Assert(AssertHeaderMatches(name, predicate));

		public static IResponse AcceptRanges(this IHas @this)
			=> @this.Header("Accept-Ranges");

		public static IResponse AcceptRanges(this IHas @this, string value)
			=> @this.Header("Accept-Ranges", value);

		public static IResponse AcceptRanges(this IHas @this, Func<HttpHeaderValueCollection<string>, bool> predicate)
			=> @this.AssertHeaderMatches("Accept-Ranges", headers => headers.AcceptRanges, predicate);

		public static IResponse Via(this IHas @this)
			=> @this.Header("Via");

		public static IResponse Via(this IHas @this, string value)
			=> @this.Header("Via", value);

		public static IResponse Via(this IHas @this, Func<HttpHeaderValueCollection<ViaHeaderValue>, bool> predicate)
			=> @this.AssertHeaderMatches("Via", headers => headers.Via, predicate);

		public static IResponse Vary(this IHas @this)
			=> @this.Header("Vary");

		public static IResponse Vary(this IHas @this, string value)
			=> @this.Header("Vary", value);

		public static IResponse Vary(this IHas @this, Func<HttpHeaderValueCollection<string>, bool> predicate)
			=> @this.AssertHeaderMatches("Vary", headers => headers.Vary, predicate);

		public static IResponse Upgrade(this IHas @this)
			=> @this.Header("Upgrade");

		public static IResponse Upgrade(this IHas @this, string value)
			=> @this.Header("Upgrade", value);

		public static IResponse Upgrade(this IHas @this, Func<HttpHeaderValueCollection<ProductHeaderValue>, bool> predicate)
			=> @this.AssertHeaderMatches("Upgrade", headers => headers.Upgrade, predicate);

		public static IResponse TransferEncodingChunked(this IHas @this)
			=> @this.Header("Transfer-Encoding", "chunked");

		public static IResponse TransferEncoding(this IHas @this)
			=> @this.Header("Transfer-Encoding");

		public static IResponse TransferEncoding(this IHas @this, string value)
			=> @this.Header("Transfer-Encoding", value);

		public static IResponse TransferEncoding(this IHas @this, Func<HttpHeaderValueCollection<TransferCodingHeaderValue>, bool> predicate)
			=> @this.AssertHeaderMatches("Transfer-Encoding", headers => headers.TransferEncoding, predicate);

		public static IResponse Trailer(this IHas @this)
			=> @this.Header("Trailer");

		public static IResponse Trailer(this IHas @this, string value)
			=> @this.Header("Trailer", value);

		public static IResponse Trailer(this IHas @this, Func<HttpHeaderValueCollection<string>, bool> predicate)
			=> @this.AssertHeaderMatches("Trailer", headers => headers.Trailer, predicate);

		public static IResponse Server(this IHas @this)
			=> @this.Header("Server");

		public static IResponse Server(this IHas @this, string value)
			=> @this.Header("Server", value);

		public static IResponse Server(this IHas @this, Func<HttpHeaderValueCollection<ProductInfoHeaderValue>, bool> predicate)
			=> @this.AssertHeaderMatches("Server", headers => headers.Server, predicate);

		public static IResponse RetryAfter(this IHas @this)
			=> @this.Header("Retry-After");

		public static IResponse RetryAfter(this IHas @this, string value)
			=> @this.Header("Retry-After", value);

		public static IResponse RetryAfter(this IHas @this, Func<RetryConditionHeaderValue, bool> predicate)
			=> @this.AssertHeaderMatches("Retry-After", headers => headers.RetryAfter, predicate);

		public static IResponse ProxyAuthenticate(this IHas @this)
			=> @this.Header("Proxy-Authenticate");

		public static IResponse ProxyAuthenticate(this IHas @this, string value)
			=> @this.Header("Proxy-Authenticate", value);

		public static IResponse ProxyAuthenticate(this IHas @this, Func<HttpHeaderValueCollection<AuthenticationHeaderValue>, bool> predicate)
			=> @this.AssertHeaderMatches("Proxy-Authenticate", headers => headers.ProxyAuthenticate, predicate);

		public static IResponse Pragma(this IHas @this)
			=> @this.Header("Pragma");

		public static IResponse Pragma(this IHas @this, string value)
			=> @this.Header("Pragma", value);

		public static IResponse Pragma(this IHas @this, Func<HttpHeaderValueCollection<NameValueHeaderValue>, bool> predicate)
			=> @this.AssertHeaderMatches("Pragma", headers => headers.Pragma, predicate);

		public static IResponse Location(this IHas @this)
			=> @this.Header("Location");

		public static IResponse Location(this IHas @this, string value)
			=> @this.Header("Location", value);

		public static IResponse Location(this IHas @this, Func<Uri, bool> predicate)
			=> @this.AssertHeaderMatches("Location", headers => headers.Location, predicate);

		public static IResponse ETag(this IHas @this)
			=> @this.Header("ETag");

		public static IResponse ETag(this IHas @this, string value)
			=> @this.Header("ETag", value);

		public static IResponse ETag(this IHas @this, Func<EntityTagHeaderValue, bool> predicate)
			=> @this.AssertHeaderMatches("ETag", headers => headers.ETag, predicate);

		public static IResponse Date(this IHas @this)
			=> @this.Header("Date");

		public static IResponse Date(this IHas @this, string value)
			=> @this.Header("Date", value);

		public static IResponse Date(this IHas @this, Func<DateTimeOffset?, bool> predicate)
			=> @this.AssertHeaderMatches("Date", headers => headers.Date, predicate);

		public static IResponse ConnectionClose(this IHas @this)
			=> @this.Header("Connection", "close");

		public static IResponse Connection(this IHas @this)
			=> @this.Header("Connection");

		public static IResponse Connection(this IHas @this, string value)
			=> @this.Header("Connection", value);

		public static IResponse Connection(this IHas @this, Func<HttpHeaderValueCollection<string>, bool> predicate)
			=> @this.AssertHeaderMatches("Connection", headers => headers.Connection, predicate);

		public static IResponse CacheControl(this IHas @this)
			=> @this.Header("Cache-Control");

		public static IResponse CacheControl(this IHas @this, string value)
			=> @this.Header("Cache-Control", value);

		public static IResponse CacheControl(this IHas @this, Func<CacheControlHeaderValue, bool> predicate)
			=> @this.AssertHeaderMatches("Cache-Control", headers => headers.CacheControl, predicate);

		public static IResponse Age(this IHas @this)
			=> @this.Header("Age");

		public static IResponse Age(this IHas @this, string value)
			=> @this.Header("Age", value);

		public static IResponse Age(this IHas @this, Func<TimeSpan?, bool> predicate)
			=> @this.AssertHeaderMatches("Age", headers => headers.Age, predicate);

		public static IResponse Warning(this IHas @this)
			=> @this.Header("Warning");

		public static IResponse Warning(this IHas @this, string value)
			=> @this.Header("Warning", value);

		public static IResponse Warning(this IHas @this, Func<HttpHeaderValueCollection<WarningHeaderValue>, bool> predicate)
			=> @this.AssertHeaderMatches("Warning", headers => headers.Warning, predicate);

		public static IResponse WwwAuthenticate(this IHas @this)
			=> @this.Header("WWW-Authenticate");

		public static IResponse WwwAuthenticate(this IHas @this, string value)
			=> @this.Header("WWW-Authenticate", value);

		public static IResponse WwwAuthenticate(this IHas @this, Func<HttpHeaderValueCollection<AuthenticationHeaderValue>, bool> predicate)
			=> @this.AssertHeaderMatches("WWW-Authenticate", headers => headers.WwwAuthenticate, predicate);

		private static IResponse AssertHeaderMatches<T>(this IHas @this, string name, Func<HttpResponseHeaders, T> selector, Func<T, bool> predicate)
		{
			return @this.Assert(response =>
			{
				AssertHeaderExists(name)(response);

				var value = selector(response.Headers);

				if (!predicate(value))
				{
					throw new OmicronException($@"Expected header ""{name}"" to match");
				}
			});
		}

		private static Action<HttpResponseMessage> AssertHeaderExists(string name)
		{
			return response =>
			{
				if (response.Headers.TryGetValues(name, out var _))
				{
					return;
				}

				if (response.Content?.Headers.TryGetValues(name, out _) == true)
				{
					return;
				}

				throw new OmicronException($@"Expected header ""{name}"" to exist");
			};
		}

		private static Action<HttpResponseMessage> AssertHeaderDoesNotExist(string name)
		{
			return response =>
			{
				if (!response.Headers.TryGetValues(name, out var _))
				{
					if (response.Content?.Headers.TryGetValues(name, out _) != true)
					{
						return;
					}
				}

				throw new OmicronException($@"Expected header ""{name}"" to not exist");
			};
		}

		private static Action<HttpResponseMessage> AssertHeaderValueEqual(string name, string value)
		{
			return response =>
			{
				var headerValues = GetHeaderValues(response, name);

				if (!headerValues.Contains(value))
				{
					var message = new StringBuilder($@"Expected header ""{name}: {value}"" but got:");

					foreach (var actualHeaderValue in headerValues)
					{
						message.Append($@"\n""{name}: {actualHeaderValue}""");
					}

					throw new OmicronException(message.ToString());
				}
			};
		}

		private static Action<HttpResponseMessage> AssertHeaderValueNotEqual(string name, string value)
		{
			return response =>
			{
				var headerValues = GetHeaderValues(response, name);

				if (headerValues.Contains(value))
				{
					throw new OmicronException($@"Expected header ""{name}"" to not be ""{value}""");
				}
			};
		}

		private static Action<HttpResponseMessage> AssertHeaderMatches(string name, Func<IEnumerable<string>, bool> predicate)
		{
			return response =>
			{
				var headerValues = GetHeaderValues(response, name);

				if (!predicate(headerValues))
				{
					throw new OmicronException($@"Expected header ""{name}"" to match");
				}
			};
		}

		private static IEnumerable<string> GetHeaderValues(HttpResponseMessage response, string name)
		{
			if (response.Headers.TryGetValues(name, out var values))
			{
				return values;
			}

			if (response.Content?.Headers.TryGetValues(name, out values) == true)
			{
				return values;
			}

			throw new OmicronException($@"Expected header ""{name}"" to exist");
		}
	}
}
