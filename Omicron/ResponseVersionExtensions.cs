using System;

namespace Omicron
{
	public static class ResponseVersionExtensions
	{
		public static IResponse Version(this IHas @this, Version version)
		{
			return @this.Assert(response =>
			{
				if (response.Version != version)
				{
					throw new OmicronException($@"Expected version ""{version}"" but got ""{response.Version}""");
				}
			});
		}

		public static IResponse Version(this IHas @this, string version)
			=> @this.Version(new Version(version));

		public static IResponse Version(this IHas @this, Func<Version, bool> predicate)
		{
			return @this.Assert(response =>
			{
				if (!predicate(response.Version))
				{
					throw new OmicronException($@"Expected version ""{response.Version}"" to match");
				}
			});
		}
	}
}
