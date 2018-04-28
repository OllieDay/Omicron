using System;
using System.Collections.Generic;
using System.Linq;

namespace Omicron
{
	public static class ResponseHeaderExtensions
	{
		public static IResponse Header(this IResponse @this, string name)
			=> @this.Header(name, _ => true);

		public static IResponse Header(this IResponse @this, string name, string value)
			=> @this.Header(name, headerValue => headerValue == value);

		public static IResponse Header(this IResponse @this, string name, Func<string, bool> predicate)
		{
			return @this.Assert(response =>
			{
				if (response.Headers.TryGetValues(name, out var values))
				{
					if (values.Any(predicate))
					{
						return;
					}
				}

				throw new OmicronException(string.Empty);
			});
		}
	}
}
