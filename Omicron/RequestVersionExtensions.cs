using System;

namespace Omicron
{
	public static class RequestVersionExtensions
	{
		public static IRequest Version(this IWith @this, Version version)
			=> @this.Modify(request => request.Version = version);

		public static IRequest Version(this IWith @this, string version)
			=> @this.Version(new Version(version));
	}
}
