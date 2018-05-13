using System;
using System.Net.Http;

namespace Omicron
{
	public interface IHas : IAsserter
	{
		IHas Not { get; }
	}
}
