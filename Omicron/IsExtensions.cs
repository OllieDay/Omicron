using System;
using System.Net.Http;

namespace Omicron
{
	internal static class IsExtensions
	{
		public static IResponse Status(this IIs @this, int statusCode)
			=> ((IHas)@this).Status(statusCode);
	}
}
