using System;

namespace Omicron
{
	public static class ResponseReasonPhraseExtensions
	{
		public static IResponse ReasonPhrase(this IResponse @this, string reasonPhrase)
		{
			return @this.Assert(response =>
			{
				if (response.ReasonPhrase != reasonPhrase)
				{
					throw new OmicronException($@"Expected reason phrase ""{reasonPhrase}"" but got ""{response.ReasonPhrase}""");
				}
			});
		}
	}
}
