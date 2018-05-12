using System;
using System.Net.Http;

namespace Omicron
{
	public static class ResponseVersionExtensions
	{
		public static IResponse Version(this IHas @this, Version version)
		{
			@this.AssertPositive(AssertVersionEqual(version));
			@this.AssertNegative(AssertVersionNotEqual(version));

			return (IResponse)@this;
		}

		public static IResponse Version(this IHas @this, string version)
			=> @this.Version(new Version(version));

		public static IResponse Version(this IHas @this, Func<Version, bool> predicate)
			=> @this.Assert(AssertVersionMatches(predicate));

		private static Action<HttpResponseMessage> AssertVersionEqual(Version version)
		{
			return response =>
			{
				if (response.Version != version)
				{
					throw new OmicronException($@"Expected version ""{version}"" but got ""{response.Version}""");
				}
			};
		}

		private static Action<HttpResponseMessage> AssertVersionNotEqual(Version version)
		{
			return response =>
			{
				if (response.Version == version)
				{
					throw new OmicronException($@"Expected version to not be ""{version}""");
				}
			};
		}

		private static Action<HttpResponseMessage> AssertVersionMatches(Func<Version, bool> predicate)
		{
			return response =>
			{
				if (!predicate(response.Version))
				{
					throw new OmicronException($@"Expected version ""{response.Version}"" to match");
				}
			};
		}
	}
}
