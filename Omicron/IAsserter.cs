using System;
using System.Net.Http;

namespace Omicron
{
	public interface IAsserter
	{
		IResponse Assert(Action<HttpResponseMessage> assertion);
		IResponse AssertPositive(Action<HttpResponseMessage> assertion);
		IResponse AssertNegative(Action<HttpResponseMessage> assertion);
	}
}
