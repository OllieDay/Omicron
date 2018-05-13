using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Omicron
{
	public static class RequestHeaderExtensions
	{
		/// <summary>
		/// Sets the header of the request to the specified name and values.
		/// </summary>
		/// <param name="name">The header name.</param>
		/// <param name="values">The header values.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Header(this IWith @this, string name, params string[] values)
			=> @this.Modify(request => request.Headers.Add(name, values));

		/// <summary>
		/// Sets the Accept header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Accept header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Accept(this IWith @this, MediaTypeWithQualityHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.Accept, value);

		/// <summary>
		/// Sets the Accept header of the request to the specified media type.
		/// </summary>
		/// <param name="mediaType">The Accept header media type.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Accept(this IWith @this, string mediaType)
			=> @this.Accept(new MediaTypeWithQualityHeaderValue(mediaType));

		/// <summary>
		/// Sets the Accept header of the request to the specified media type and quality.
		/// </summary>
		/// <param name="mediaType">The Accept header media type.</param>
		/// <param name="quality">The Accept header quality.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Accept(this IWith @this, string mediaType, double quality)
			=> @this.Accept(new MediaTypeWithQualityHeaderValue(mediaType, quality));

		/// <summary>
		/// Sets the Accept-Charset header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Accept-Charset header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest AcceptCharset(this IWith @this, StringWithQualityHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.AcceptCharset, value);

		/// <summary>
		/// Sets the Accept-Charset header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Accept-Charset header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest AcceptCharset(this IWith @this, string value)
			=> @this.AcceptCharset(new StringWithQualityHeaderValue(value));

		/// <summary>
		/// Sets the Accept-Charset header of the request to the specified value and quality.
		/// </summary>
		/// <param name="value">The Accept-Charset header value.</param>
		/// <param name="quality">The Accept-Charset header quality.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest AcceptCharset(this IWith @this, string value, double quality)
			=> @this.AcceptCharset(new StringWithQualityHeaderValue(value, quality));

		/// <summary>
		/// Sets the Accept-Encoding header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Accept-Encoding header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest AcceptEncoding(this IWith @this, StringWithQualityHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.AcceptEncoding, value);

		/// <summary>
		/// Sets the Accept-Encoding header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Accept-Encoding header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest AcceptEncoding(this IWith @this, string value)
			=> @this.AcceptEncoding(new StringWithQualityHeaderValue(value));

		/// <summary>
		/// Sets the Accept-Encoding header of the request to the specified value and quality.
		/// </summary>
		/// <param name="value">The Accept-Encoding header value.</param>
		/// <param name="quality">The Accept-Encoding header quality.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest AcceptEncoding(this IWith @this, string value, double quality)
			=> @this.AcceptEncoding(new StringWithQualityHeaderValue(value, quality));

		/// <summary>
		/// Sets the Accept-Language header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Accept-Language header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest AcceptLanguage(this IWith @this, StringWithQualityHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.AcceptLanguage, value);

		/// <summary>
		/// Sets the Accept-Language header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Accept-Language header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest AcceptLanguage(this IWith @this, string value)
			=> @this.AcceptLanguage(new StringWithQualityHeaderValue(value));

		/// <summary>
		/// Sets the Accept-Language header of the request to the specified value and quality.
		/// </summary>
		/// <param name="value">The Accept-Language header value.</param>
		/// <param name="quality">The Accept-Language header quality.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest AcceptLanguage(this IWith @this, string value, double quality)
			=> @this.AcceptLanguage(new StringWithQualityHeaderValue(value, quality));

		/// <summary>
		/// Sets the Authorization header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Authorization header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Authorization(this IWith @this, AuthenticationHeaderValue value)
			=> @this.Modify(request => request.Headers.Authorization = value);

		/// <summary>
		/// Sets the Authorization header of the request to the specified scheme.
		/// </summary>
		/// <param name="scheme">The Authorization header scheme.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Authorization(this IWith @this, string scheme)
			=> @this.Authorization(new AuthenticationHeaderValue(scheme));

		/// <summary>
		/// Sets the Authorization header of the request to the specified scheme and parameter.
		/// </summary>
		/// <param name="scheme">The Authorization header scheme.</param>
		/// <param name="parameter">The Authorization header parameter.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Authorization(this IWith @this, string scheme, string parameter)
			=> @this.Authorization(new AuthenticationHeaderValue(scheme, parameter));

		/// <summary>
		/// Sets the Cache-Control header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Cache-Control header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest CacheControl(this IWith @this, CacheControlHeaderValue value)
			=> @this.Modify(request => request.Headers.CacheControl = value);

		/// <summary>
		/// Sets the Cache-Control header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Cache-Control header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Connection(this IWith @this, string value)
			=> @this.AddHeaderValue(headers => headers.Connection, value);

		/// <summary>
		/// Sets the Connection header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Connection header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest ConnectionClose(this IWith @this, bool value)
			=> @this.Modify(request => request.Headers.ConnectionClose = value);

		/// <summary>
		/// Sets the Date header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Date header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Date(this IWith @this, DateTimeOffset value)
			=> @this.Modify(request => request.Headers.Date = value);

		/// <summary>
		/// Sets the Expect header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Expect header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Expect(this IWith @this, NameValueWithParametersHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.Expect, value);

		/// <summary>
		/// Sets the Expect header of the request to the specified name.
		/// </summary>
		/// <param name="name">The Expect header name.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Expect(this IWith @this, string name)
			=> @this.Expect(new NameValueWithParametersHeaderValue(name));

		/// <summary>
		/// Sets the Expect header of the request to the specified name and value.
		/// </summary>
		/// <param name="name">The Expect header name.</param>
		/// <param name="value">The Expect header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Expect(this IWith @this, string name, string value)
			=> @this.Expect(new NameValueWithParametersHeaderValue(name, value));

		/// <summary>
		/// Sets the Expect header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Expect header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest ExpectContinue(this IWith @this, bool value)
			=> @this.Modify(request => request.Headers.ExpectContinue = value);

		/// <summary>
		/// Sets the From header of the request to the specified value.
		/// </summary>
		/// <param name="value">The From header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest From(this IWith @this, string value)
			=> @this.Modify(request => request.Headers.From = value);

		/// <summary>
		/// Sets the Host header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Host header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Host(this IWith @this, string value)
			=> @this.Modify(request => request.Headers.Host = value);

		/// <summary>
		/// Sets the If-Match header of the request to the specified value.
		/// </summary>
		/// <param name="value">The If-Match header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest IfMatch(this IWith @this, EntityTagHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.IfMatch, value);

		/// <summary>
		/// Sets the If-Match header of the request to the specified tag.
		/// </summary>
		/// <param name="tag">The If-Match header tag.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest IfMatch(this IWith @this, string tag)
			=> @this.IfMatch(new EntityTagHeaderValue(tag));

		/// <summary>
		/// Sets the If-Match header of the request to the specified tag and is weak.
		/// </summary>
		/// <param name="tag">The If-Match header tag.</param>
		/// <param name="isWeak">The If-Match header is weak.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest IfMatch(this IWith @this, string tag, bool isWeak)
			=> @this.IfMatch(new EntityTagHeaderValue(tag, isWeak));

		/// <summary>
		/// Sets the If-Modified-Since header of the request to the specified value.
		/// </summary>
		/// <param name="value">The If-Modified-Since header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest IfModifiedSince(this IWith @this, DateTimeOffset value)
			=> @this.Modify(request => request.Headers.IfModifiedSince = value);

		/// <summary>
		/// Sets the If-None-Match header of the request to the specified value.
		/// </summary>
		/// <param name="value">The If-None-Match header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest IfNoneMatch(this IWith @this, EntityTagHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.IfNoneMatch, value);

		/// <summary>
		/// Sets the If-None-Match header of the request to the specified tag.
		/// </summary>
		/// <param name="tag">The If-None-Match header tag.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest IfNoneMatch(this IWith @this, string tag)
		=> @this.IfNoneMatch(new EntityTagHeaderValue(tag));

		/// <summary>
		/// Sets the If-None-Match header of the request to the specified tag and is weak.
		/// </summary>
		/// <param name="tag">The If-None-Match header tag.</param>
		/// <param name="tag">The If-None-Match header is weak.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest IfNoneMatch(this IWith @this, string tag, bool isWeak)
			=> @this.IfNoneMatch(new EntityTagHeaderValue(tag, isWeak));

		/// <summary>
		/// Sets the If-Range header of the request to the specified value.
		/// </summary>
		/// <param name="value">The If-Range header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest IfRange(this IWith @this, RangeConditionHeaderValue value)
			=> @this.Modify(request => request.Headers.IfRange = value);

		/// <summary>
		/// Sets the If-Range header of the request to the specified date.
		/// </summary>
		/// <param name="date">The If-Range header date.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest IfRange(this IWith @this, DateTimeOffset date)
			=> @this.IfRange(new RangeConditionHeaderValue(date));

		/// <summary>
		/// Sets the If-Range header of the request to the specified tag.
		/// </summary>
		/// <param name="tag">The If-Range header tag.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest IfRange(this IWith @this, string tag)
			=> @this.IfRange(new RangeConditionHeaderValue(new EntityTagHeaderValue(tag)));

		/// <summary>
		/// Sets the If-Range header of the request to the specified tag and is weak.
		/// </summary>
		/// <param name="tag">The If-Range header tag.</param>
		/// <param name="tag">The If-Range header is weak.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest IfRange(this IWith @this, string tag, bool isWeak)
			=> @this.IfRange(new RangeConditionHeaderValue(new EntityTagHeaderValue(tag, isWeak)));

		/// <summary>
		/// Sets the If-Unmodified-Since header of the request to the specified value.
		/// </summary>
		/// <param name="value">The If-Unmodified-Since header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest IfUnmodifiedSince(this IWith @this, DateTimeOffset value)
			=> @this.Modify(request => request.Headers.IfUnmodifiedSince = value);

		/// <summary>
		/// Sets the Max-Forwards header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Max-Forwards header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest MaxForwards(this IWith @this, int value)
			=> @this.Modify(request => request.Headers.MaxForwards = value);

		/// <summary>
		/// Sets the Pragma header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Pragma header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Pragma(this IWith @this, NameValueHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.Pragma, value);

		/// <summary>
		/// Sets the Pragma header of the request to the specified name.
		/// </summary>
		/// <param name="name">The Pragma header name.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Pragma(this IWith @this, string name)
			=> @this.Pragma(new NameValueHeaderValue(name));

		/// <summary>
		/// Sets the Proxy-Authorization header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Proxy-Authorization header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest ProxyAuthorization(this IWith @this, AuthenticationHeaderValue value)
			=> @this.Modify(request => request.Headers.ProxyAuthorization = value);

		/// <summary>
		/// Sets the Proxy-Authorization header of the request to the specified scheme.
		/// </summary>
		/// <param name="scheme">The Proxy-Authorization header scheme.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest ProxyAuthorization(this IWith @this, string scheme)
			=> @this.ProxyAuthorization(new AuthenticationHeaderValue(scheme));

		/// <summary>
		/// Sets the Proxy-Authorization header of the request to the specified scheme and parameter.
		/// </summary>
		/// <param name="scheme">The Proxy-Authorization header scheme.</param>
		/// <param name="parameter">The Proxy-Authorization header parameter.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest ProxyAuthorization(this IWith @this, string scheme, string parameter)
			=> @this.ProxyAuthorization(new AuthenticationHeaderValue(scheme, parameter));

		/// <summary>
		/// Sets the Range header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Range header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Range(this IWith @this, RangeHeaderValue value)
			=> @this.Modify(request => request.Headers.Range = value);

		/// <summary>
		/// Sets the Range header of the request to the specified from and to.
		/// </summary>
		/// <param name="from">The Range header from.</param>
		/// <param name="to">The Range header to.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Range(this IWith @this, long from, long to)
			=> @this.Range(new RangeHeaderValue(from, to));

		/// <summary>
		/// Sets the Referer header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Referer header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Referrer(this IWith @this, Uri value)
			=> @this.Modify(request => request.Headers.Referrer = value);

		/// <summary>
		/// Sets the Referer header of the request to the specified URI.
		/// </summary>
		/// <param name="uriString">The Referer header URI.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Referrer(this IWith @this, string uriString)
			=> @this.Referrer(new Uri(uriString));

		/// <summary>
		/// Sets the TE header of the request to the specified value.
		/// </summary>
		/// <param name="value">The TE header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest TE(this IWith @this, TransferCodingWithQualityHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.TE, value);

		/// <summary>
		/// Sets the TE header of the request to the specified value.
		/// </summary>
		/// <param name="value">The TE header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest TE(this IWith @this, string value)
			=> @this.TE(new TransferCodingWithQualityHeaderValue(value));

		/// <summary>
		/// Sets the TE header of the request to the specified value and quality.
		/// </summary>
		/// <param name="value">The TE header value.</param>
		/// <param name="value">The TE header quality.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest TE(this IWith @this, string value, double quality)
			=> @this.TE(new TransferCodingWithQualityHeaderValue(value, quality));

		/// <summary>
		/// Sets the Trailer header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Trailer header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Trailer(this IWith @this, string value)
			=> @this.AddHeaderValue(headers => headers.Trailer, value);

		/// <summary>
		/// Sets the Transfer-Encoding header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Transfer-Encoding header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest TransferEncoding(this IWith @this, TransferCodingHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.TransferEncoding, value);

		/// <summary>
		/// Sets the Transfer-Encoding header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Transfer-Encoding header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest TransferEncoding(this IWith @this, string value)
			=> @this.TransferEncoding(new TransferCodingHeaderValue(value));

		/// <summary>
		/// Sets the Transfer-Encoding header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Transfer-Encoding header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest TransferEncodingChunked(this IWith @this, bool value)
			=> @this.Modify(request => request.Headers.TransferEncodingChunked = value);

		/// <summary>
		/// Sets the Upgrade header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Upgrade header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Upgrade(this IWith @this, ProductHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.Upgrade, value);

		/// <summary>
		/// Sets the Upgrade header of the request to the specified name.
		/// </summary>
		/// <param name="name">The Upgrade header name.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Upgrade(this IWith @this, string name)
			=> @this.Upgrade(new ProductHeaderValue(name));

		/// <summary>
		/// Sets the Upgrade header of the request to the specified name and version.
		/// </summary>
		/// <param name="name">The Upgrade header name.</param>
		/// <param name="version">The Upgrade header version.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Upgrade(this IWith @this, string name, string version)
			=> @this.Upgrade(new ProductHeaderValue(name, version));

		/// <summary>
		/// Sets the User-Agent header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Upgrade header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest UserAgent(this IWith @this, ProductInfoHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.UserAgent, value);

		/// <summary>
		/// Sets the User-Agent header of the request to the specified product.
		/// </summary>
		/// <param name="product">The Upgrade header product.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest UserAgent(this IWith @this, ProductHeaderValue product)
			=> @this.UserAgent(new ProductInfoHeaderValue(product));

		/// <summary>
		/// Sets the User-Agent header of the request to the specified comment.
		/// </summary>
		/// <param name="comment">The Upgrade header comment.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest UserAgent(this IWith @this, string comment)
			=> @this.UserAgent(new ProductInfoHeaderValue(comment));

		/// <summary>
		/// Sets the User-Agent header of the request to the specified product name and product version.
		/// </summary>
		/// <param name="productName">The Upgrade header product name.</param>
		/// <param name="productVersion">The Upgrade header product version.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest UserAgent(this IWith @this, string productName, string productVersion)
			=> @this.UserAgent(new ProductInfoHeaderValue(productName, productVersion));

		/// <summary>
		/// Sets the Via header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Via header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Via(this IWith @this, ViaHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.Via, value);

		/// <summary>
		/// Sets the Via header of the request to the specified protocol version and received by.
		/// </summary>
		/// <param name="protocolVersion">The Via header protocol version.</param>
		/// <param name="receivedBy">The Via header received by.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Via(this IWith @this, string protocolVersion, string receivedBy)
			=> @this.Via(new ViaHeaderValue(protocolVersion, receivedBy));

		/// <summary>
		/// Sets the Via header of the request to the specified protocol version, received by, and protocol name.
		/// </summary>
		/// <param name="protocolVersion">The Via header protocol version.</param>
		/// <param name="receivedBy">The Via header received by.</param>
		/// <param name="protocolName">The Via header protocol name.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Via(this IWith @this, string protocolVersion, string receivedBy, string protocolName)
			=> @this.Via(new ViaHeaderValue(protocolVersion, receivedBy, protocolName));

		/// <summary>
		/// Sets the Via header of the request to the specified protocol version, received by, protocol name, and comment.
		/// </summary>
		/// <param name="protocolVersion">The Via header protocol version.</param>
		/// <param name="receivedBy">The Via header received by.</param>
		/// <param name="protocolName">The Via header protocol name.</param>
		/// <param name="comment">The Via header comment.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Via(this IWith @this, string protocolVersion, string receivedBy, string protocolName, string comment)
			=> @this.Via(new ViaHeaderValue(protocolVersion, receivedBy, protocolName, comment));

		/// <summary>
		/// Sets the Warning header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Warning header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Warning(this IWith @this, WarningHeaderValue value)
			=> @this.AddHeaderValue(headers => headers.Warning, value);

		/// <summary>
		/// Sets the Warning header of the request to the specified code, agent, and text.
		/// </summary>
		/// <param name="code">The Warning header code.</param>
		/// <param name="agent">The Warning header agent.</param>
		/// <param name="text">The Warning header text.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Warning(this IWith @this, int code, string agent, string text)
			=> @this.Warning(new WarningHeaderValue(code, agent, text));

		/// <summary>
		/// Sets the Warning header of the request to the specified code, agent, text, and date.
		/// </summary>
		/// <param name="code">The Warning header code.</param>
		/// <param name="agent">The Warning header agent.</param>
		/// <param name="text">The Warning header text.</param>
		/// <param name="date">The Warning header date.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Warning(this IWith @this, int code, string agent, string text, DateTimeOffset date)
			=> @this.Warning(new WarningHeaderValue(code, agent, text, date));

		private static IRequest AddHeaderValue<T>(this IWith @this, Func<HttpRequestHeaders, ICollection<T>> selector, T value)
			=> @this.Modify(request => selector(request.Headers).Add(value));
	}
}
