using System;
using System.Net.Http;

namespace Omicron
{
	public interface IRequest : IDisposable
	{
		IWith With { get; }
		IIs Is { get; }
		IHas Has { get; }

		IRequest Modify(Action<HttpRequestMessage> modification);
		IResponse Assert(Action<HttpResponseMessage> assertion);
	}
}
