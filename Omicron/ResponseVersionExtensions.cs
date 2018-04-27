using System;

namespace Omicron
{
	public static class ResponseVersionExtensions
	{
		public static IResponse Version(this IResponse @this, Version version)
		{
			return @this.Assert(response =>
			{
				if (response.Version != version)
				{
					throw new OmicronException($"Expected version {version} but got {response.Version}");
				}
			});
		}
	}
}
