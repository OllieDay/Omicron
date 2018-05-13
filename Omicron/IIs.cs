using System;
using System.Net.Http;

namespace Omicron
{
	public interface IIs : IAsserter
	{
		IIs Not { get; }
	}
}
