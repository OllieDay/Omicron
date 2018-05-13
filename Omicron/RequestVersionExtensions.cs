using System;

namespace Omicron
{
	public static class RequestVersionExtensions
	{
		/// <summary>
		/// Sets the HTTP version of the request to the specified version.
		/// </summary>
		/// <param name="version">The HTTP request version.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Version(this IWith @this, Version version)
			=> @this.Modify(request => request.Version = version);

		/// <summary>
		/// Sets the HTTP version of the request to the specified version.
		/// </summary>
		/// <param name="version">The HTTP request version.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Version(this IWith @this, string version)
			=> @this.Version(new Version(version));
	}
}
