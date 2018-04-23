using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Omicron
{
	public static class OmicronHeaderExtensions
	{
		public static Omicron Header(this Omicron @this, string name, IEnumerable<string> values)
			=> @this.AddModification(request => request.Headers.Add(name, values));

		public static Omicron Header(this Omicron @this, string name, string value)
			=> @this.AddModification(request => request.Headers.Add(name, value));

		internal static Omicron AddHeaderValue<T>(this Omicron @this, Func<HttpRequestHeaders, ICollection<T>> selector, T value)
			=> @this.Return(() => @this.AddModification(request => selector(request.Headers).Add(value)));
	}
}
