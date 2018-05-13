using System;
using System.Net.Http;

namespace Omicron
{
	public interface IResponse : IAsserter, IDisposable
	{
		IIs Is { get; }

		IHas Has { get; }
	}
}
