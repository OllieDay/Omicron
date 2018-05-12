using System;
using System.Net.Http;

namespace Omicron
{
	public interface IResponse : IDisposable
	{
		IIs Is { get; }
		IHas Has { get; }

		IResponse Assert(Action<HttpResponseMessage> assertion);
		IResponse AssertPositive(Action<HttpResponseMessage> assertion);
		IResponse AssertNegative(Action<HttpResponseMessage> assertion);
	}
}
