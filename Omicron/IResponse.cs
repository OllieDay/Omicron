using System;
using System.Net.Http;

namespace Omicron
{
	/// <summary>
	/// Represents an object used to add assertions to the response.
	/// </summary>
	public interface IResponse : IAsserter, IDisposable
	{
		/// <summary>
		/// Gets the <see cref="IIs"/> object used to add assertions to the response.
		/// </summary>
		/// <returns>The <see cref="IIs"/> object used to add assertions.</returns>
		IIs Is { get; }

		/// <summary>
		/// Gets the <see cref="IHas"/> object used to add assertions to the response.
		/// </summary>
		/// <returns>The <see cref="IHas"/> object used to add assertions.</returns>
		IHas Has { get; }
	}
}
