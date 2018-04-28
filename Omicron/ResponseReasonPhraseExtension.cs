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

		public static IResponse ReasonPhrase(this IResponse @this, Func<string, bool> predicate)
		{
			return @this.Assert(response =>
			{
				if (!predicate(response.ReasonPhrase))
				{
					throw new OmicronException($@"Expected reason phrase ""{response.ReasonPhrase}"" to match");
				}
			});
		}
	}
}
