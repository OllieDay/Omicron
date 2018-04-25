using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Omicron
{
	public static class OmicronHeaderExtensions
	{
		public static IRequest Header(this IRequest @this, string name, params string[] values)
			=> @this.Modify(request => request.Headers.Add(name, values));

		public static IRequest Accept(this IRequest @this, MediaTypeWithQualityHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.Accept, value);

		public static IRequest Accept(this IRequest @this, string mediaType)
			=> @this.Accept(new MediaTypeWithQualityHeaderValue(mediaType));

		public static IRequest Accept(this IRequest @this, string mediaType, double quality)
			=> @this.Accept(new MediaTypeWithQualityHeaderValue(mediaType, quality));

		public static IRequest AcceptCharset(this IRequest @this, StringWithQualityHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.AcceptCharset, value);

		public static IRequest AcceptCharset(this IRequest @this, string value)
			=> @this.AcceptCharset(new StringWithQualityHeaderValue(value));

		public static IRequest AcceptCharset(this IRequest @this, string value, double quality)
			=> @this.AcceptCharset(new StringWithQualityHeaderValue(value, quality));

		public static IRequest AcceptEncoding(this IRequest @this, StringWithQualityHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.AcceptEncoding, value);

		public static IRequest AcceptEncoding(this IRequest @this, string value)
			=> @this.AcceptEncoding(new StringWithQualityHeaderValue(value));

		public static IRequest AcceptEncoding(this IRequest @this, string value, double quality)
			=> @this.AcceptEncoding(new StringWithQualityHeaderValue(value, quality));

		public static IRequest AcceptLanguage(this IRequest @this, StringWithQualityHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.AcceptLanguage, value);

		public static IRequest AcceptLanguage(this IRequest @this, string value)
			=> @this.AcceptLanguage(new StringWithQualityHeaderValue(value));

		public static IRequest AcceptLanguage(this IRequest @this, string value, double quality)
			=> @this.AcceptLanguage(new StringWithQualityHeaderValue(value, quality));

		public static IRequest Authorization(this IRequest @this, AuthenticationHeaderValue value)
			=> @this.Modify(request => request.Headers.Authorization = value);

		public static IRequest Authorization(this IRequest @this, string scheme)
			=> @this.Authorization(new AuthenticationHeaderValue(scheme));

		public static IRequest Authorization(this IRequest @this, string scheme, string parameter)
			=> @this.Authorization(new AuthenticationHeaderValue(scheme, parameter));

		public static IRequest CacheControl(this IRequest @this, CacheControlHeaderValue value)
			=> @this.Modify(request => request.Headers.CacheControl = value);

		public static IRequest Connection(this IRequest @this, string value)
			=> @this.AddHeaderValue(headers => headers.Connection, value);

		public static IRequest ConnectionClose(this IRequest @this, bool value)
			=> @this.Modify(request => request.Headers.ConnectionClose = value);

		public static IRequest Date(this IRequest @this, DateTimeOffset value)
			=> @this.Modify(request => request.Headers.Date = value);

		public static IRequest Expect(this IRequest @this, NameValueWithParametersHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.Expect, value);

		public static IRequest Expect(this IRequest @this, string name)
			=> @this.Expect(new NameValueWithParametersHeaderValue(name));

		public static IRequest Expect(this IRequest @this, string name, string value)
			=> @this.Expect(new NameValueWithParametersHeaderValue(name, value));

		public static IRequest ExpectContinue(this IRequest @this, bool value)
			=> @this.Modify(request => request.Headers.ExpectContinue = value);

		public static IRequest From(this IRequest @this, string value)
			=> @this.Modify(request => request.Headers.From = value);

		public static IRequest Host(this IRequest @this, string value)
			=> @this.Modify(request => request.Headers.Host = value);

		public static IRequest IfMatch(this IRequest @this, EntityTagHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.IfMatch, value);

		public static IRequest IfMatch(this IRequest @this, string tag)
			=> @this.IfMatch(new EntityTagHeaderValue(tag));

		public static IRequest IfMatch(this IRequest @this, string tag, bool isWeak)
			=> @this.IfMatch(new EntityTagHeaderValue(tag, isWeak));

		public static IRequest IfModifiedSince(this IRequest @this, DateTimeOffset value)
			=> @this.Modify(request => request.Headers.IfModifiedSince = value);

		public static IRequest IfNoneMatch(this IRequest @this, EntityTagHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.IfNoneMatch, value);

		public static IRequest IfNoneMatch(this IRequest @this, string tag)
		=> @this.IfNoneMatch(new EntityTagHeaderValue(tag));

		public static IRequest IfNoneMatch(this IRequest @this, string tag, bool isWeak)
			=> @this.IfNoneMatch(new EntityTagHeaderValue(tag, isWeak));

		public static IRequest IfRange(this IRequest @this, RangeConditionHeaderValue value)
			=> @this.Modify(request => request.Headers.IfRange = value);

		public static IRequest IfRange(this IRequest @this, DateTimeOffset date)
			=> @this.IfRange(new RangeConditionHeaderValue(date));

		public static IRequest IfRange(this IRequest @this, string tag)
			=> @this.IfRange(new RangeConditionHeaderValue(new EntityTagHeaderValue(tag)));

		public static IRequest IfRange(this IRequest @this, string tag, bool isWeak)
			=> @this.IfRange(new RangeConditionHeaderValue(new EntityTagHeaderValue(tag, isWeak)));

		public static IRequest IfUnmodifiedSince(this IRequest @this, DateTimeOffset value)
			=> @this.Modify(request => request.Headers.IfUnmodifiedSince = value);

		public static IRequest MaxForwards(this IRequest @this, int value)
			=> @this.Modify(request => request.Headers.MaxForwards = value);

		public static IRequest Pragma(this IRequest @this, NameValueHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.Pragma, value);

		public static IRequest Pragma(this IRequest @this, string name)
			=> @this.Pragma(new NameValueHeaderValue(name));

		public static IRequest ProxyAuthorization(this IRequest @this, AuthenticationHeaderValue value)
			=> @this.Modify(request => request.Headers.Authorization = value);

		public static IRequest ProxyAuthorization(this IRequest @this, string scheme)
			=> @this.ProxyAuthorization(new AuthenticationHeaderValue(scheme));

		public static IRequest ProxyAuthorization(this IRequest @this, string scheme, string parameter)
			=> @this.ProxyAuthorization(new AuthenticationHeaderValue(scheme, parameter));

		public static IRequest Range(this IRequest @this, RangeHeaderValue value)
			=> @this.Modify(request => request.Headers.Range = value);

		public static IRequest Range(this IRequest @this, long from, long to)
			=> @this.Range(new RangeHeaderValue(from, to));

		public static IRequest Referrer(this IRequest @this, Uri value)
			=> @this.Modify(request => request.Headers.Referrer = value);

		public static IRequest Referrer(this IRequest @this, string uriString)
			=> @this.Referrer(new Uri(uriString));

		public static IRequest TE(this IRequest @this, TransferCodingWithQualityHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.TE, value);

		public static IRequest TE(this IRequest @this, string value)
			=> @this.TE(new TransferCodingWithQualityHeaderValue(value));

		public static IRequest TE(this IRequest @this, string value, double quality)
			=> @this.TE(new TransferCodingWithQualityHeaderValue(value, quality));

		public static IRequest Trailer(this IRequest @this, string value)
			=> @this.AddHeaderValue(headers => headers.Trailer, value);

		public static IRequest TransferEncoding(this IRequest @this, TransferCodingHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.TransferEncoding, value);

		public static IRequest TransferEncoding(this IRequest @this, string value)
			=> @this.TransferEncoding(new TransferCodingHeaderValue(value));

		public static IRequest TransferEncodingChunked(this IRequest @this, bool value)
			=> @this.Modify(request => request.Headers.TransferEncodingChunked = value);

		public static IRequest Upgrade(this IRequest @this, ProductHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.Upgrade, value);

		public static IRequest Upgrade(this IRequest @this, string name)
			=> @this.Upgrade(new ProductHeaderValue(name));

		public static IRequest Upgrade(this IRequest @this, string name, string version)
			=> @this.Upgrade(new ProductHeaderValue(name, version));

		public static IRequest UserAgent(this IRequest @this, ProductInfoHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.UserAgent, value);

		public static IRequest UserAgent(this IRequest @this, ProductHeaderValue product)
			=> @this.UserAgent(new ProductInfoHeaderValue(product));

		public static IRequest UserAgent(this IRequest @this, string comment)
			=> @this.UserAgent(new ProductInfoHeaderValue(comment));

		public static IRequest UserAgent(this IRequest @this, string productName, string productVersion)
			=> @this.UserAgent(new ProductInfoHeaderValue(productName, productVersion));

		public static IRequest Via(this IRequest @this, ViaHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.Via, value);

		public static IRequest Via(this IRequest @this, string protocolVersion, string receivedBy)
			=> @this.Via(new ViaHeaderValue(protocolVersion, receivedBy));

		public static IRequest Via(this IRequest @this, string protocolVersion, string receivedBy, string protocolName)
			=> @this.Via(new ViaHeaderValue(protocolVersion, receivedBy, protocolName));

		public static IRequest Via(this IRequest @this, string protocolVersion, string receivedBy, string protocolName, string comment)
			=> @this.Via(new ViaHeaderValue(protocolVersion, receivedBy, protocolName, comment));

		public static IRequest Warning(this IRequest @this, WarningHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.Warning, value);

		public static IRequest Warning(this IRequest @this, int code, string agent, string text)
			=> @this.Warning(new WarningHeaderValue(code, agent, text));

		public static IRequest Warning(this IRequest @this, int code, string agent, string text, DateTimeOffset date)
			=> @this.Warning(new WarningHeaderValue(code, agent, text, date));

		internal static IRequest AddHeaderValue<T>(this IRequest @this, Func<HttpRequestHeaders, ICollection<T>> selector, T value)
			=> @this.Modify(request => selector(request.Headers).Add(value));
	}
}
