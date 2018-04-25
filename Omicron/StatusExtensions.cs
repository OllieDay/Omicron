namespace Omicron
{
	public static class StatusExtensions
	{
		public static IResponse Status(this IResponse @this, int statusCode)
		{
			return @this.Assert(response =>
			{
				var responseStatusCode = (int)response.StatusCode;

				if (responseStatusCode != statusCode)
				{
					throw new OmicronException($"Expected status {statusCode} but got {responseStatusCode}");
				}
			});
		}

		public static IResponse Continue(this IResponse @this)
			=> @this.Status(100);

		public static IResponse SwitchingProtocols(this IResponse @this)
			=> @this.Status(101);

		public static IResponse OK(this IResponse @this)
			=> @this.Status(200);

		public static IResponse Created(this IResponse @this)
			=> @this.Status(201);

		public static IResponse Accepted(this IResponse @this)
			=> @this.Status(202);

		public static IResponse NonAuthoritativeInformation(this IResponse @this)
			=> @this.Status(203);

		public static IResponse NoContent(this IResponse @this)
			=> @this.Status(204);

		public static IResponse ResetContent(this IResponse @this)
			=> @this.Status(205);

		public static IResponse PartialContent(this IResponse @this)
			=> @this.Status(206);

		public static IResponse Ambiguous(this IResponse @this)
			=> @this.Status(300);

		public static IResponse MultipleChoices(this IResponse @this)
			=> @this.Status(300);

		public static IResponse Moved(this IResponse @this)
			=> @this.Status(301);

		public static IResponse MovedPermanently(this IResponse @this)
			=> @this.Status(301);

		public static IResponse Found(this IResponse @this)
			=> @this.Status(302);

		public static IResponse Redirect(this IResponse @this)
			=> @this.Status(302);

		public static IResponse RedirectMethod(this IResponse @this)
			=> @this.Status(303);

		public static IResponse SeeOther(this IResponse @this)
			=> @this.Status(303);

		public static IResponse NotModified(this IResponse @this)
			=> @this.Status(304);

		public static IResponse UseProxy(this IResponse @this)
			=> @this.Status(305);

		public static IResponse Unused(this IResponse @this)
			=> @this.Status(306);

		public static IResponse RedirectKeepVerb(this IResponse @this)
			=> @this.Status(307);

		public static IResponse TemporaryRedirect(this IResponse @this)
			=> @this.Status(307);

		public static IResponse BadRequest(this IResponse @this)
			=> @this.Status(400);

		public static IResponse Unauthorized(this IResponse @this)
			=> @this.Status(401);

		public static IResponse PaymentRequired(this IResponse @this)
			=> @this.Status(402);

		public static IResponse Forbidden(this IResponse @this)
			=> @this.Status(403);

		public static IResponse NotFound(this IResponse @this)
			=> @this.Status(404);

		public static IResponse MethodNotAllowed(this IResponse @this)
			=> @this.Status(405);

		public static IResponse NotAcceptable(this IResponse @this)
			=> @this.Status(406);

		public static IResponse ProxyAuthenticationRequired(this IResponse @this)
			=> @this.Status(407);

		public static IResponse RequestTimeout(this IResponse @this)
			=> @this.Status(408);

		public static IResponse Conflict(this IResponse @this)
			=> @this.Status(409);

		public static IResponse Gone(this IResponse @this)
			=> @this.Status(410);

		public static IResponse LengthRequired(this IResponse @this)
			=> @this.Status(411);

		public static IResponse PreconditionFailed(this IResponse @this)
			=> @this.Status(412);

		public static IResponse RequestEntityTooLarge(this IResponse @this)
			=> @this.Status(413);

		public static IResponse RequestUriTooLong(this IResponse @this)
			=> @this.Status(414);

		public static IResponse UnsupportedMediaType(this IResponse @this)
			=> @this.Status(415);

		public static IResponse RequestedRangeNotSatisfiable(this IResponse @this)
			=> @this.Status(416);

		public static IResponse ExpectationFailed(this IResponse @this)
			=> @this.Status(417);

		public static IResponse UpgradeRequired(this IResponse @this)
			=> @this.Status(426);

		public static IResponse InternalServerError(this IResponse @this)
			=> @this.Status(500);

		public static IResponse NotImplemented(this IResponse @this)
			=> @this.Status(501);

		public static IResponse BadGateway(this IResponse @this)
			=> @this.Status(502);

		public static IResponse ServiceUnavailable(this IResponse @this)
			=> @this.Status(503);

		public static IResponse GatewayTimeout(this IResponse @this)
			=> @this.Status(504);

		public static IResponse HttpVersionNotSupported(this IResponse @this)
			=> @this.Status(505);
	}
}
