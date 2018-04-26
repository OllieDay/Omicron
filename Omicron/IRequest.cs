using System;
using System.Net.Http;

namespace Omicron
{
	public interface IRequest : IDisposable
	{
		IRequest With { get; }
		IResponse Is { get; }
		IResponse Has { get; }

		IRequest Modify(Action<HttpRequestMessage> modification);
		IResponse Assert(Action<HttpResponseMessage> assertion);
	}
}
