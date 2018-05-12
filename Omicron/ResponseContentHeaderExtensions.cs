using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Omicron
{
	public static class ResponseContentHeaderExtensions
	{
		public static IResponse Allow(this IHas @this)
			=> @this.Header("Allow");

		public static IResponse Allow(this IHas @this, string value)
			=> @this.Header("Allow", value);

		public static IResponse Allow(this IHas @this, Func<IEnumerable<string>, bool> predicate)
			=> @this.AssertHeaderMatches("Allow", headers => headers.Allow, predicate);

		public static IResponse ContentDisposition(this IHas @this)
			=> @this.Header("Content-Disposition");

		public static IResponse ContentDisposition(this IHas @this, string value)
			=> @this.Header("Content-Disposition", value);

		public static IResponse ContentDisposition(this IHas @this, Func<ContentDispositionHeaderValue, bool> predicate)
			=> @this.AssertHeaderMatches("Content-Disposition", headers => headers.ContentDisposition, predicate);

		public static IResponse ContentEncoding(this IHas @this)
			=> @this.Header("Content-Encoding");

		public static IResponse ContentEncoding(this IHas @this, string value)
			=> @this.Header("Content-Encoding", value);

		public static IResponse ContentEncoding(this IHas @this, Func<IEnumerable<string>, bool> predicate)
			=> @this.AssertHeaderMatches("Content-Encoding", headers => headers.ContentEncoding, predicate);

		public static IResponse ContentLanguage(this IHas @this)
			=> @this.Header("Content-Language");

		public static IResponse ContentLanguage(this IHas @this, string value)
			=> @this.Header("Content-Language", value);

		public static IResponse ContentLanguage(this IHas @this, Func<IEnumerable<string>, bool> predicate)
			=> @this.AssertHeaderMatches("Content-Language", headers => headers.ContentLanguage, predicate);

		public static IResponse ContentLength(this IHas @this)
			=> @this.Header("Content-Length");

		public static IResponse ContentLength(this IHas @this, string value)
			=> @this.Header("Content-Length", value);

		public static IResponse ContentLength(this IHas @this, Func<long?, bool> predicate)
			=> @this.AssertHeaderMatches("Content-Length", headers => headers.ContentLength, predicate);

		public static IResponse ContentLocation(this IHas @this)
			=> @this.Header("Content-Location");

		public static IResponse ContentLocation(this IHas @this, string value)
			=> @this.Header("Content-Location", value);

		public static IResponse ContentLocation(this IHas @this, Func<Uri, bool> predicate)
			=> @this.AssertHeaderMatches("Content-Location", headers => headers.ContentLocation, predicate);

		public static IResponse ContentMD5(this IHas @this)
			=> @this.Header("Content-MD5");

		public static IResponse ContentMD5(this IHas @this, string value)
			=> @this.Header("Content-MD5", value);

		public static IResponse ContentMD5(this IHas @this, Func<byte[], bool> predicate)
			=> @this.AssertHeaderMatches("Content-MD5", headers => headers.ContentMD5, predicate);

		public static IResponse ContentRange(this IHas @this)
			=> @this.Header("Content-Range");

		public static IResponse ContentRange(this IHas @this, string value)
			=> @this.Header("Content-Range", value);

		public static IResponse ContentRange(this IHas @this, Func<ContentRangeHeaderValue, bool> predicate)
			=> @this.AssertHeaderMatches("Content-Range", headers => headers.ContentRange, predicate);

		public static IResponse ContentType(this IHas @this)
			=> @this.Header("Content-Type");

		public static IResponse ContentType(this IHas @this, string value)
			=> @this.Header("Content-Type", value);

		public static IResponse ContentType(this IHas @this, Func<MediaTypeHeaderValue, bool> predicate)
			=> @this.AssertHeaderMatches("Content-Type", headers => headers.ContentType, predicate);

		public static IResponse Expires(this IHas @this)
			=> @this.Header("Expires");

		public static IResponse Expires(this IHas @this, string value)
			=> @this.Header("Expires", value);

		public static IResponse Expires(this IHas @this, Func<DateTimeOffset?, bool> predicate)
			=> @this.AssertHeaderMatches("Expires", headers => headers.Expires, predicate);

		public static IResponse LastModified(this IHas @this)
			=> @this.Header("Last-Modified");

		public static IResponse LastModified(this IHas @this, string value)
			=> @this.Header("Last-Modified", value);

		public static IResponse LastModified(this IHas @this, Func<DateTimeOffset?, bool> predicate)
			=> @this.AssertHeaderMatches("Last-Modified", headers => headers.LastModified, predicate);

		private static IResponse AssertHeaderMatches<T>(this IHas @this, string name, Func<HttpContentHeaders, T> selector, Func<T, bool> predicate)
		{
			return @this.Assert(response =>
			{
				AssertHeaderExists(name)(response);

				var value = selector(response.Content.Headers);

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
				if (response.Content?.Headers.TryGetValues(name, out _) == true)
				{
					return;
				}

				throw new OmicronException($@"Expected header ""{name}"" to exist");
			};
		}
	}
}
