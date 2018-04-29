using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Omicron
{
	public static class ResponseContentHeaderExtensions
	{
		public static IResponse Allow(this IResponse @this)
			=> @this.Header("Allow");

		public static IResponse Allow(this IResponse @this, params string[] values)
			=> @this.Header("Allow", values);

		public static IResponse Allow(this IResponse @this, Func<IEnumerable<string>, bool> predicate)
			=> @this.AssertHeader("Allow", headers => headers.Allow, predicate);

		public static IResponse ContentDisposition(this IResponse @this)
			=> @this.Header("Content-Disposition");

		public static IResponse ContentDisposition(this IResponse @this, string value)
			=> @this.Header("Content-Disposition", value);

		public static IResponse ContentDisposition(this IResponse @this, Func<ContentDispositionHeaderValue, bool> predicate)
			=> @this.AssertHeader("Content-Disposition", headers => headers.ContentDisposition, predicate);

		public static IResponse ContentEncoding(this IResponse @this)
			=> @this.Header("Content-Encoding");

		public static IResponse ContentEncoding(this IResponse @this, params string[] values)
			=> @this.Header("Content-Encoding", values);

		public static IResponse ContentEncoding(this IResponse @this, Func<IEnumerable<string>, bool> predicate)
			=> @this.AssertHeader("Content-Encoding", headers => headers.ContentEncoding, predicate);

		public static IResponse ContentLanguage(this IResponse @this)
			=> @this.Header("Content-Language");

		public static IResponse ContentLanguage(this IResponse @this, params string[] values)
			=> @this.Header("Content-Language", values);

		public static IResponse ContentLanguage(this IResponse @this, Func<IEnumerable<string>, bool> predicate)
			=> @this.AssertHeader("Content-Language", headers => headers.ContentLanguage, predicate);

		public static IResponse ContentLength(this IResponse @this)
			=> @this.Header("Content-Length");

		public static IResponse ContentLength(this IResponse @this, string value)
			=> @this.Header("Content-Length", value);

		public static IResponse ContentLength(this IResponse @this, Func<long?, bool> predicate)
			=> @this.AssertHeader("Content-Length", headers => headers.ContentLength, predicate);

		public static IResponse ContentLocation(this IResponse @this)
			=> @this.Header("Content-Location");

		public static IResponse ContentLocation(this IResponse @this, string value)
			=> @this.Header("Content-Location", value);

		public static IResponse ContentLocation(this IResponse @this, Func<Uri, bool> predicate)
			=> @this.AssertHeader("Content-Location", headers => headers.ContentLocation, predicate);

		public static IResponse ContentMD5(this IResponse @this)
			=> @this.Header("Content-MD5");

		public static IResponse ContentMD5(this IResponse @this, string value)
			=> @this.Header("Content-MD5", value);

		public static IResponse ContentMD5(this IResponse @this, Func<byte[], bool> predicate)
			=> @this.AssertHeader("Content-MD5", headers => headers.ContentMD5, predicate);

		public static IResponse ContentRange(this IResponse @this)
			=> @this.Header("Content-Range");

		public static IResponse ContentRange(this IResponse @this, string value)
			=> @this.Header("Content-Range", value);

		public static IResponse ContentRange(this IResponse @this, Func<ContentRangeHeaderValue, bool> predicate)
			=> @this.AssertHeader("Content-Range", headers => headers.ContentRange, predicate);

		public static IResponse ContentType(this IResponse @this)
			=> @this.Header("Content-Type");

		public static IResponse ContentType(this IResponse @this, string value)
			=> @this.Header("Content-Type", value);

		public static IResponse ContentType(this IResponse @this, Func<MediaTypeHeaderValue, bool> predicate)
			=> @this.AssertHeader("Content-Type", headers => headers.ContentType, predicate);

		public static IResponse Expires(this IResponse @this)
			=> @this.Header("Expires");

		public static IResponse Expires(this IResponse @this, string value)
			=> @this.Header("Expires", value);

		public static IResponse Expires(this IResponse @this, Func<DateTimeOffset?, bool> predicate)
			=> @this.AssertHeader("Expires", headers => headers.Expires, predicate);

		public static IResponse LastModified(this IResponse @this)
			=> @this.Header("Last-Modified");

		public static IResponse LastModified(this IResponse @this, string value)
			=> @this.Header("Last-Modified", value);

		public static IResponse LastModified(this IResponse @this, Func<DateTimeOffset?, bool> predicate)
			=> @this.AssertHeader("Last-Modified", headers => headers.LastModified, predicate);

		private static IResponse AssertHeader<T>(this IResponse @this, string name, Func<HttpContentHeaders, T> selector, Func<T, bool> predicate)
		{
			// Check header exists before asserting
			return @this.Header(name).Assert(response =>
			{
				var value = selector(response.Content.Headers);

				if (!predicate(value))
				{
					throw new OmicronException($@"Expected header ""{name}"" to match");
				}
			});
		}
	}
}
