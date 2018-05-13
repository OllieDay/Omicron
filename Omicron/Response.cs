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
		public IIs Is => SetAssertion(Assertion.Positive);
		public IHas Has => SetAssertion(Assertion.Positive);
		IIs IIs.Not => SetAssertion(Assertion.Negative);
		IHas IHas.Not => SetAssertion(Assertion.Negative);

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

		private Response SetAssertion(Assertion assertion)
		{
			Assertion = assertion;

			return this;
		}
	}
}
