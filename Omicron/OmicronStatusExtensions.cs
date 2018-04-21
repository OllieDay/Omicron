namespace Omicron
{
	public static class OmicronStatusExtensions
	{
		public static Omicron Status(this Omicron @this, int statusCode)
		{
			@this.Assert(response =>
			{
				var responseStatusCode = (int)response.StatusCode;

				if (responseStatusCode != statusCode)
				{
					@this.Fail("status", statusCode, responseStatusCode);
				}
			});

			return @this;
		}

		public static Omicron Continue(this Omicron @this)
			=> @this.Status(100);

		public static Omicron SwitchingProtocols(this Omicron @this)
			=> @this.Status(101);

		public static Omicron OK(this Omicron @this)
			=> @this.Status(200);

		public static Omicron Created(this Omicron @this)
			=> @this.Status(201);

		public static Omicron Accepted(this Omicron @this)
			=> @this.Status(202);

		public static Omicron NonAuthoritativeInformation(this Omicron @this)
			=> @this.Status(203);

		public static Omicron NoContent(this Omicron @this)
			=> @this.Status(204);

		public static Omicron ResetContent(this Omicron @this)
			=> @this.Status(205);

		public static Omicron PartialContent(this Omicron @this)
			=> @this.Status(206);

		public static Omicron Ambiguous(this Omicron @this)
			=> @this.Status(300);

		public static Omicron MultipleChoices(this Omicron @this)
			=> @this.Status(300);

		public static Omicron Moved(this Omicron @this)
			=> @this.Status(301);

		public static Omicron MovedPermanently(this Omicron @this)
			=> @this.Status(301);

		public static Omicron Found(this Omicron @this)
			=> @this.Status(302);

		public static Omicron Redirect(this Omicron @this)
			=> @this.Status(302);

		public static Omicron RedirectMethod(this Omicron @this)
			=> @this.Status(303);

		public static Omicron SeeOther(this Omicron @this)
			=> @this.Status(303);

		public static Omicron NotModified(this Omicron @this)
			=> @this.Status(304);

		public static Omicron UseProxy(this Omicron @this)
			=> @this.Status(305);

		public static Omicron Unused(this Omicron @this)
			=> @this.Status(306);

		public static Omicron RedirectKeepVerb(this Omicron @this)
			=> @this.Status(307);

		public static Omicron TemporaryRedirect(this Omicron @this)
			=> @this.Status(307);

		public static Omicron BadRequest(this Omicron @this)
			=> @this.Status(400);

		public static Omicron Unauthorized(this Omicron @this)
			=> @this.Status(401);

		public static Omicron PaymentRequired(this Omicron @this)
			=> @this.Status(402);

		public static Omicron Forbidden(this Omicron @this)
			=> @this.Status(403);

		public static Omicron NotFound(this Omicron @this)
			=> @this.Status(404);

		public static Omicron MethodNotAllowed(this Omicron @this)
			=> @this.Status(405);

		public static Omicron NotAcceptable(this Omicron @this)
			=> @this.Status(406);

		public static Omicron ProxyAuthenticationRequired(this Omicron @this)
			=> @this.Status(407);

		public static Omicron RequestTimeout(this Omicron @this)
			=> @this.Status(408);

		public static Omicron Conflict(this Omicron @this)
			=> @this.Status(409);

		public static Omicron Gone(this Omicron @this)
			=> @this.Status(410);

		public static Omicron LengthRequired(this Omicron @this)
			=> @this.Status(411);

		public static Omicron PreconditionFailed(this Omicron @this)
			=> @this.Status(412);

		public static Omicron RequestEntityTooLarge(this Omicron @this)
			=> @this.Status(413);

		public static Omicron RequestUriTooLong(this Omicron @this)
			=> @this.Status(414);

		public static Omicron UnsupportedMediaType(this Omicron @this)
			=> @this.Status(415);

		public static Omicron RequestedRangeNotSatisfiable(this Omicron @this)
			=> @this.Status(416);

		public static Omicron ExpectationFailed(this Omicron @this)
			=> @this.Status(417);

		public static Omicron UpgradeRequired(this Omicron @this)
			=> @this.Status(426);

		public static Omicron InternalServerError(this Omicron @this)
			=> @this.Status(500);

		public static Omicron NotImplemented(this Omicron @this)
			=> @this.Status(501);

		public static Omicron BadGateway(this Omicron @this)
			=> @this.Status(502);

		public static Omicron ServiceUnavailable(this Omicron @this)
			=> @this.Status(503);

		public static Omicron GatewayTimeout(this Omicron @this)
			=> @this.Status(504);

		public static Omicron HttpVersionNotSupported(this Omicron @this)
			=> @this.Status(505);
	}
}
