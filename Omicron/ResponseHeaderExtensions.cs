using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omicron
{
	public static class ResponseHeaderExtensions
	{
		public static IResponse Header(this IResponse @this, string name)
			=> @this.Header(name, _ => true);

		public static IResponse Header(this IResponse @this, string name, string value)
		{
			return @this.Assert(response =>
			{
				if (!response.Headers.TryGetValues(name, out var values))
				{
					throw new OmicronException($@"Expected header ""{name}""");
				}

				if (!values.Contains(value))
				{
					var message = new StringBuilder($@"Expected header ""{name}: {value}"" but got:");

					foreach (var headerValue in values)
					{
						message.Append($@"\n\t""{name}: {headerValue}""");
					}

					throw new OmicronException(message.ToString());
				}
			});
		}

		public static IResponse Header(this IResponse @this, string name, Func<string, bool> predicate)
		{
			return @this.Assert(response =>
			{
				if (!response.Headers.TryGetValues(name, out var values))
				{
					throw new OmicronException($@"Expected header ""{name}""");
				}

				if (!values.Any(predicate))
				{
					throw new OmicronException($@"Expected header ""{name}"" to match");
				}
			});
		}
	}
}
