using System;
using System.Net.Http;

namespace Omicron
{
	internal static class HasExtensions
	{
		public static IResponse Assert(this IHas @this, Action<HttpResponseMessage> assertion)
			=> ((IResponse)@this).Assert(assertion);
	}
}
