using System;
using System.Net.Http;

namespace Omicron
{
	public static class ResponseStatusExtensions
	{
		public static IResponse Status(this IHas @this, int statusCode)
		{
			@this.AssertPositive(AssertStatusEqual(statusCode));
			@this.AssertNegative(AssertStatusNotEqual(statusCode));

			return (IResponse)@this;
		}

		public static IResponse Status(this IHas @this, Func<int, bool> predicate)
			=> @this.Assert(AssertStatusMatches(predicate));

		public static IResponse Continue(this IIs @this)
			=> @this.Status(100);

		public static IResponse SwitchingProtocols(this IIs @this)
			=> @this.Status(101);

		public static IResponse OK(this IIs @this)
			=> @this.Status(200);

		public static IResponse Created(this IIs @this)
			=> @this.Status(201);

		public static IResponse Accepted(this IIs @this)
			=> @this.Status(202);

		public static IResponse NonAuthoritativeInformation(this IIs @this)
			=> @this.Status(203);

		public static IResponse NoContent(this IIs @this)
			=> @this.Status(204);

		public static IResponse ResetContent(this IIs @this)
			=> @this.Status(205);

		public static IResponse PartialContent(this IIs @this)
			=> @this.Status(206);

		public static IResponse Ambiguous(this IIs @this)
			=> @this.Status(300);

		public static IResponse MultipleChoices(this IIs @this)
			=> @this.Status(300);

		public static IResponse Moved(this IIs @this)
			=> @this.Status(301);

		public static IResponse MovedPermanently(this IIs @this)
			=> @this.Status(301);

		public static IResponse Found(this IIs @this)
			=> @this.Status(302);

		public static IResponse Redirect(this IIs @this)
			=> @this.Status(302);

		public static IResponse RedirectMethod(this IIs @this)
			=> @this.Status(303);

		public static IResponse SeeOther(this IIs @this)
			=> @this.Status(303);

		public static IResponse NotModified(this IIs @this)
			=> @this.Status(304);

		public static IResponse UseProxy(this IIs @this)
			=> @this.Status(305);

		public static IResponse Unused(this IIs @this)
			=> @this.Status(306);

		public static IResponse RedirectKeepVerb(this IIs @this)
			=> @this.Status(307);

		public static IResponse TemporaryRedirect(this IIs @this)
			=> @this.Status(307);

		public static IResponse BadRequest(this IIs @this)
			=> @this.Status(400);

		public static IResponse Unauthorized(this IIs @this)
			=> @this.Status(401);

		public static IResponse PaymentRequired(this IIs @this)
			=> @this.Status(402);

		public static IResponse Forbidden(this IIs @this)
			=> @this.Status(403);

		public static IResponse NotFound(this IIs @this)
			=> @this.Status(404);

		public static IResponse MethodNotAllowed(this IIs @this)
			=> @this.Status(405);

		public static IResponse NotAcceptable(this IIs @this)
			=> @this.Status(406);

		public static IResponse ProxyAuthenticationRequired(this IIs @this)
			=> @this.Status(407);

		public static IResponse RequestTimeout(this IIs @this)
			=> @this.Status(408);

		public static IResponse Conflict(this IIs @this)
			=> @this.Status(409);

		public static IResponse Gone(this IIs @this)
			=> @this.Status(410);

		public static IResponse LengthRequired(this IIs @this)
			=> @this.Status(411);

		public static IResponse PreconditionFailed(this IIs @this)
			=> @this.Status(412);

		public static IResponse RequestEntityTooLarge(this IIs @this)
			=> @this.Status(413);

		public static IResponse RequestUriTooLong(this IIs @this)
			=> @this.Status(414);

		public static IResponse UnsupportedMediaType(this IIs @this)
			=> @this.Status(415);

		public static IResponse RequestedRangeNotSatisfiable(this IIs @this)
			=> @this.Status(416);

		public static IResponse ExpectationFailed(this IIs @this)
			=> @this.Status(417);

		public static IResponse UpgradeRequired(this IIs @this)
			=> @this.Status(426);

		public static IResponse InternalServerError(this IIs @this)
			=> @this.Status(500);

		public static IResponse NotImplemented(this IIs @this)
			=> @this.Status(501);

		public static IResponse BadGateway(this IIs @this)
			=> @this.Status(502);

		public static IResponse ServiceUnavailable(this IIs @this)
			=> @this.Status(503);

		public static IResponse GatewayTimeout(this IIs @this)
			=> @this.Status(504);

		public static IResponse HttpVersionNotSupported(this IIs @this)
			=> @this.Status(505);

		private static IResponse Status(this IIs @this, int statusCode)
			=> ((IHas)@this).Status(statusCode);

		private static Action<HttpResponseMessage> AssertStatusEqual(int statusCode)
		{
			return response =>
			{
				var responseStatusCode = (int)response.StatusCode;

				if (responseStatusCode != statusCode)
				{
					throw new OmicronException($@"Expected status ""{statusCode}"" but got ""{responseStatusCode}""");
				}
			};
		}

		private static Action<HttpResponseMessage> AssertStatusNotEqual(int statusCode)
		{
			return response =>
			{
				if ((int)response.StatusCode == statusCode)
				{
					throw new OmicronException($@"Expected status to not be ""{statusCode}""");
				}
			};
		}

		private static Action<HttpResponseMessage> AssertStatusMatches(Func<int, bool> predicate)
		{
			return response =>
			{
				var responseStatusCode = (int)response.StatusCode;

				if (!predicate(responseStatusCode))
				{
					throw new OmicronException($@"Expected status ""{responseStatusCode}"" to match");
				}
			};
		}
	}
}
