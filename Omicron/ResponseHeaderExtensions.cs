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

		public static IResponse Header(this IResponse @this, string name, params string[] values)
		{
			return @this.Assert(response =>
			{
				if (!response.Headers.TryGetValues(name, out var headerValues))
				{
					throw new OmicronException($@"Expected header ""{name}""");
				}

				if (values.Except(headerValues).Any())
				{
					var message = new StringBuilder($@"Expected headers:");

					foreach (var expectedHeaderValue in values)
					{
						message.Append($@"\n\t""{name}: {expectedHeaderValue}""");
					}

					message.Append("\nbut got:");

					foreach (var actualHeaderValue in headerValues)
					{
						message.Append($@"\n\t""{name}: {actualHeaderValue}""");
					}

					throw new OmicronException(message.ToString());
				}
			});
		}

		public static IResponse Header(this IResponse @this, string name, Func<IEnumerable<string>, bool> predicate)
		{
			return @this.Assert(response =>
			{
				if (!response.Headers.TryGetValues(name, out var values))
				{
					throw new OmicronException($@"Expected headers ""{name}""");
				}

				if (!predicate(values))
				{
					throw new OmicronException($@"Expected headers ""{name}"" to match");
				}
			});
		}
	}
}
