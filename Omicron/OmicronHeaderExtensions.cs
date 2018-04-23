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

		internal static Omicron AddHeaderValue<T>(this Omicron @this, Func<HttpRequestHeaders, ICollection<T>> selector, T value)
			=> @this.Return(() => @this.AddModification(request => selector(request.Headers).Add(value)));
	}
}
