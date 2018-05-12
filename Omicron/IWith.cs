using System;
using System.Net.Http;

namespace Omicron
{
	public interface IWith
	{
		IRequest Modify(Action<HttpRequestMessage> modification);
	}
}
