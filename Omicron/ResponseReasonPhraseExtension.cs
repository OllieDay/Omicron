using System;
using System.Net.Http;

namespace Omicron
{
	public static class ResponseReasonPhraseExtensions
	{
		public static IResponse ReasonPhrase(this IHas @this, string reasonPhrase)
		{
			@this.AssertPositive(AssertReasonPhraseEqual(reasonPhrase));
			@this.AssertNegative(AssertReasonPhraseNotEqual(reasonPhrase));

			return (IResponse)@this;
		}

		public static IResponse ReasonPhrase(this IHas @this, Func<string, bool> predicate)
			=>@this.Assert(AssertReasonPhraseMatches(predicate));

		private static Action<HttpResponseMessage> AssertReasonPhraseEqual(string reasonPhrase)
		{
			return response =>
			{
				if (response.ReasonPhrase != reasonPhrase)
				{
					throw new OmicronException($@"Expected reason phrase ""{reasonPhrase}"" but got ""{response.ReasonPhrase}""");
				}
			};
		}

		private static Action<HttpResponseMessage> AssertReasonPhraseNotEqual(string reasonPhrase)
		{
			return response =>
			{
				if (response.ReasonPhrase == reasonPhrase)
				{
					throw new OmicronException($@"Expected reason phrase to not be ""{reasonPhrase}""");
				}
			};
		}

		private static Action<HttpResponseMessage> AssertReasonPhraseMatches(Func<string, bool> predicate)
		{
			return response =>
			{
				if (!predicate(response.ReasonPhrase))
				{
					throw new OmicronException($@"Expected reason phrase ""{response.ReasonPhrase}"" to match");
				}
			};
		}
	}
}
