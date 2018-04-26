using System;

namespace Omicron
{
	public static class VersionExtensions
	{
		public static IRequest Version(this IRequest @this, Version version)
			=> @this.Modify(request => request.Version = version);
	}
}