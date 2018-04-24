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
		internal static Omicron AddHeaderValue<T>(this Omicron @this, Func<HttpRequestHeaders, ICollection<T>> selector, T value)
			=> @this.Return(() => @this.AddModification(request => selector(request.Headers).Add(value)));
	}
}
