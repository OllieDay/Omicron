using System;
using System.Net.Http;

namespace Omicron
{
	public interface IResponse : IDisposable
	{
		IResponse Is { get; }
		IResponse Has { get; }

		IResponse Assert(Action<HttpResponseMessage> assertion);
	}
}
