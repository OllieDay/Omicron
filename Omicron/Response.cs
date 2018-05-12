using System;
using System.Net.Http;

namespace Omicron
{
	internal class Response : IResponse, IIs, IHas
	{
		private readonly HttpResponseMessage _response;

		public Response(HttpResponseMessage response)
		{
			_response = response;
		}

		public Assertion Assertion { get; set; }

		public IIs Is
		{
			get
			{
				Assertion = Assertion.Positive;

				return this;
			}
		}

		public IHas Has
		{
			get
			{
				Assertion = Assertion.Positive;

				return this;
			}
		}

		IIs IIs.Not => Not;
		IHas IHas.Not => Not;

		internal Response Not
		{
			get
			{
				Assertion = Assertion.Negative;

				return this;
			}
		}

		public IResponse Assert(Action<HttpResponseMessage> assertion)
		{
			assertion(_response);

			return this;
		}

		public IResponse AssertPositive(Action<HttpResponseMessage> assertion)
		{
			if (Assertion == Assertion.Positive)
			{
				Assert(assertion);
			}

			return this;
		}

		public IResponse AssertNegative(Action<HttpResponseMessage> assertion)
		{
			if (Assertion == Assertion.Negative)
			{
				Assert(assertion);
			}

			return this;
		}

		public void Dispose()
		{
			_response.Dispose();
		}
	}
}
