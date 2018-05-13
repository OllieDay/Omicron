using System;
using System.Net.Http;

namespace Omicron
{
	/// <summary>
	/// Represents an HTTP request that supports modifications and assertions.
	/// </summary>
	public interface IRequest : IWith, IAsserter, IDisposable
	{
		/// <summary>
		/// Gets the <see cref="IWith"/> object that can be used to add modifications to the request.
		/// </summary>
		/// <returns>The <see cref="IWith"/> object used to add modifications.</returns>
		IWith With { get; }

		/// <summary>
		/// Gets the <see cref="IIs"/> object that can be used to add assertions to the response.
		/// </summary>
		/// <returns>The <see cref="IIs"/> object used to add assertions.</returns>
		IIs Is { get; }

		/// <summary>
		/// Gets the <see cref="IHas"/> object that can be used to add assertions to the response.
		/// </summary>
		/// <returns>The <see cref="IHas"/> object used to add assertions.</returns>
		IHas Has { get; }
	}
}
