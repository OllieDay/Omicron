using System;
using System.Net.Http;

namespace Omicron
{
	/// <summary>
	/// Represents an object that supports modifications.
	/// </summary>
	public interface IWith
	{
		/// <summary>
		/// Adds the specified modification to the request.
		/// </summary>
		/// <param name="modification">The action used to modify the underlying <see cref="HttpRequestMessage"/> request.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		IRequest Modify(Action<HttpRequestMessage> modification);
	}
}
