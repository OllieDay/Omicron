using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Omicron
{
	public static class OmicronHeaderExtensions
	{
		public static Omicron Header(this Omicron @this, string name, params string[] values)
			=> @this.AddModification(request => request.Headers.Add(name, values));

		public static Omicron Accept(this Omicron @this, MediaTypeWithQualityHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.Accept, value);

		public static Omicron Accept(this Omicron @this, string mediaType)
			=> @this.Accept(new MediaTypeWithQualityHeaderValue(mediaType));

		public static Omicron Accept(this Omicron @this, string mediaType, double quality)
			=> @this.Accept(new MediaTypeWithQualityHeaderValue(mediaType, quality));

		public static Omicron AcceptCharset(this Omicron @this, StringWithQualityHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.AcceptCharset, value);

		public static Omicron AcceptCharset(this Omicron @this, string value)
			=> @this.AcceptCharset(new StringWithQualityHeaderValue(value));

		public static Omicron AcceptCharset(this Omicron @this, string value, double quality)
			=> @this.AcceptCharset(new StringWithQualityHeaderValue(value, quality));

		public static Omicron AcceptEncoding(this Omicron @this, StringWithQualityHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.AcceptEncoding, value);

		public static Omicron AcceptEncoding(this Omicron @this, string value)
			=> @this.AcceptEncoding(new StringWithQualityHeaderValue(value));

		public static Omicron AcceptEncoding(this Omicron @this, string value, double quality)
			=> @this.AcceptEncoding(new StringWithQualityHeaderValue(value, quality));

		public static Omicron AcceptLanguage(this Omicron @this, StringWithQualityHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.AcceptLanguage, value);

		public static Omicron AcceptLanguage(this Omicron @this, string value)
			=> @this.AcceptLanguage(new StringWithQualityHeaderValue(value));

		public static Omicron AcceptLanguage(this Omicron @this, string value, double quality)
			=> @this.AcceptLanguage(new StringWithQualityHeaderValue(value, quality));

		public static Omicron Authorization(this Omicron @this, AuthenticationHeaderValue value)
			=> @this.AddModification(request => request.Headers.Authorization = value);

		public static Omicron Authorization(this Omicron @this, string scheme)
			=> @this.Authorization(new AuthenticationHeaderValue(scheme));

		public static Omicron Authorization(this Omicron @this, string scheme, string parameter)
			=> @this.Authorization(new AuthenticationHeaderValue(scheme, parameter));

		public static Omicron CacheControl(this Omicron @this, CacheControlHeaderValue value)
			=> @this.AddModification(request => request.Headers.CacheControl = value);

		public static Omicron Connection(this Omicron @this, string value)
			=> @this.AddHeaderValue(headers => headers.Connection, value);

		public static Omicron ConnectionClose(this Omicron @this, bool value)
			=> @this.AddModification(request => request.Headers.ConnectionClose = value);

		public static Omicron Date(this Omicron @this, DateTimeOffset value)
			=> @this.AddModification(request => request.Headers.Date = value);

		public static Omicron Expect(this Omicron @this, NameValueWithParametersHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.Expect, value);

		public static Omicron Expect(this Omicron @this, string name)
			=> @this.Expect(new NameValueWithParametersHeaderValue(name));

		public static Omicron Expect(this Omicron @this, string name, string value)
			=> @this.Expect(new NameValueWithParametersHeaderValue(name, value));

		public static Omicron ExpectContinue(this Omicron @this, bool value)
			=> @this.AddModification(request => request.Headers.ExpectContinue = value);

		public static Omicron From(this Omicron @this, string value)
			=> @this.AddModification(request => request.Headers.From = value);

		public static Omicron Host(this Omicron @this, string value)
			=> @this.AddModification(request => request.Headers.Host = value);

		public static Omicron IfMatch(this Omicron @this, EntityTagHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.IfMatch, value);

		public static Omicron IfMatch(this Omicron @this, string tag)
			=> @this.IfMatch(new EntityTagHeaderValue(tag));

		public static Omicron IfMatch(this Omicron @this, string tag, bool isWeak)
			=> @this.IfMatch(new EntityTagHeaderValue(tag, isWeak));

		public static Omicron IfModifiedSince(this Omicron @this, DateTimeOffset value)
			=> @this.AddModification(request => request.Headers.IfModifiedSince = value);

		public static Omicron IfNoneMatch(this Omicron @this, EntityTagHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.IfNoneMatch, value);

		public static Omicron IfNoneMatch(this Omicron @this, string tag)
		=> @this.IfNoneMatch(new EntityTagHeaderValue(tag));

		public static Omicron IfNoneMatch(this Omicron @this, string tag, bool isWeak)
			=> @this.IfNoneMatch(new EntityTagHeaderValue(tag, isWeak));

		public static Omicron IfRange(this Omicron @this, RangeConditionHeaderValue value)
			=> @this.AddModification(request => request.Headers.IfRange = value);

		public static Omicron IfRange(this Omicron @this, DateTimeOffset date)
			=> @this.IfRange(new RangeConditionHeaderValue(date));

		public static Omicron IfRange(this Omicron @this, string tag)
			=> @this.IfRange(new RangeConditionHeaderValue(new EntityTagHeaderValue(tag)));

		public static Omicron IfRange(this Omicron @this, string tag, bool isWeak)
			=> @this.IfRange(new RangeConditionHeaderValue(new EntityTagHeaderValue(tag, isWeak)));

		internal static Omicron AddHeaderValue<T>(this Omicron @this, Func<HttpRequestHeaders, ICollection<T>> selector, T value)
			=> @this.Return(() => @this.AddModification(request => selector(request.Headers).Add(value)));
	}
}
