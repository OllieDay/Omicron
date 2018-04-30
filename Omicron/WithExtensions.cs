using System;
using System.Net.Http;

namespace Omicron
{
	internal static class WithExtensions
	{
		public static IRequest Modify(this IWith @this, Action<HttpRequestMessage> modification)
			=> ((IRequest)@this).Modify(modification);
	}
}
