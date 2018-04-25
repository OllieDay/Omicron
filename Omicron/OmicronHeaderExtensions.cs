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

		public static Omicron IfUnmodifiedSince(this Omicron @this, DateTimeOffset value)
			=> @this.AddModification(request => request.Headers.IfUnmodifiedSince = value);

		public static Omicron MaxForwards(this Omicron @this, int value)
			=> @this.AddModification(request => request.Headers.MaxForwards = value);

		public static Omicron Pragma(this Omicron @this, NameValueHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.Pragma, value);

		public static Omicron Pragma(this Omicron @this, string name)
			=> @this.Pragma(new NameValueHeaderValue(name));

		public static Omicron ProxyAuthorization(this Omicron @this, AuthenticationHeaderValue value)
			=> @this.AddModification(request => request.Headers.Authorization = value);

		public static Omicron ProxyAuthorization(this Omicron @this, string scheme)
			=> @this.ProxyAuthorization(new AuthenticationHeaderValue(scheme));

		public static Omicron ProxyAuthorization(this Omicron @this, string scheme, string parameter)
			=> @this.ProxyAuthorization(new AuthenticationHeaderValue(scheme, parameter));

		public static Omicron Range(this Omicron @this, RangeHeaderValue value)
			=> @this.AddModification(request => request.Headers.Range = value);

		public static Omicron Range(this Omicron @this, long from, long to)
			=> @this.Range(new RangeHeaderValue(from, to));

		public static Omicron Referrer(this Omicron @this, Uri value)
			=> @this.AddModification(request => request.Headers.Referrer = value);

		public static Omicron Referrer(this Omicron @this, string uriString)
			=> @this.Referrer(new Uri(uriString));

		public static Omicron TE(this Omicron @this, TransferCodingWithQualityHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.TE, value);

		public static Omicron TE(this Omicron @this, string value)
			=> @this.TE(new TransferCodingWithQualityHeaderValue(value));

		public static Omicron TE(this Omicron @this, string value, double quality)
			=> @this.TE(new TransferCodingWithQualityHeaderValue(value, quality));

		public static Omicron Trailer(this Omicron @this, string value)
			=> @this.AddHeaderValue(headers => headers.Trailer, value);

		public static Omicron TransferEncoding(this Omicron @this, TransferCodingHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.TransferEncoding, value);

		public static Omicron TransferEncoding(this Omicron @this, string value)
			=> @this.TransferEncoding(new TransferCodingHeaderValue(value));

		public static Omicron TransferEncodingChunked(this Omicron @this, bool value)
			=> @this.AddModification(request => request.Headers.TransferEncodingChunked = value);

		public static Omicron Upgrade(this Omicron @this, ProductHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.Upgrade, value);

		public static Omicron Upgrade(this Omicron @this, string name)
			=> @this.Upgrade(new ProductHeaderValue(name));

		public static Omicron Upgrade(this Omicron @this, string name, string version)
			=> @this.Upgrade(new ProductHeaderValue(name, version));

		public static Omicron UserAgent(this Omicron @this, ProductInfoHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.UserAgent, value);

		public static Omicron UserAgent(this Omicron @this, ProductHeaderValue product)
			=> @this.UserAgent(new ProductInfoHeaderValue(product));

		public static Omicron UserAgent(this Omicron @this, string comment)
			=> @this.UserAgent(new ProductInfoHeaderValue(comment));

		public static Omicron UserAgent(this Omicron @this, string productName, string productVersion)
			=> @this.UserAgent(new ProductInfoHeaderValue(productName, productVersion));

		public static Omicron Via(this Omicron @this, ViaHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.Via, value);

		public static Omicron Via(this Omicron @this, string protocolVersion, string receivedBy)
			=> @this.Via(new ViaHeaderValue(protocolVersion, receivedBy));

		public static Omicron Via(this Omicron @this, string protocolVersion, string receivedBy, string protocolName)
			=> @this.Via(new ViaHeaderValue(protocolVersion, receivedBy, protocolName));

		public static Omicron Via(this Omicron @this, string protocolVersion, string receivedBy, string protocolName, string comment)
			=> @this.Via(new ViaHeaderValue(protocolVersion, receivedBy, protocolName, comment));

		public static Omicron Warning(this Omicron @this, WarningHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.Warning, value);

		public static Omicron Warning(this Omicron @this, int code, string agent, string text)
			=> @this.Warning(new WarningHeaderValue(code, agent, text));

		public static Omicron Warning(this Omicron @this, int code, string agent, string text, DateTimeOffset date)
			=> @this.Warning(new WarningHeaderValue(code, agent, text, date));

		internal static Omicron AddHeaderValue<T>(this Omicron @this, Func<HttpRequestHeaders, ICollection<T>> selector, T value)
			=> @this.Return(() => @this.AddModification(request => selector(request.Headers).Add(value)));
	}
}
