using System;
using System.Collections.Generic;
using System.Linq;

namespace Omicron
{
	public static class ResponseHeadersExtensions
	{
		public static IResponse Headers(this IResponse @this, string name, params string[] values)
			=> @this.Headers(name, headerValues => !values.Except(headerValues).Any());

		public static IResponse Headers(this IResponse @this, string name, Func<IEnumerable<string>, bool> predicate)
		{
			return @this.Assert(response =>
			{
				if (response.Headers.TryGetValues(name, out var values))
				{
					if (predicate(values))
					{
						return;
					}
				}

				throw new OmicronException(string.Empty);
			});
		}
	}
}
