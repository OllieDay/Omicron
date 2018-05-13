using System;
using System.Net.Http;

namespace Omicron
{
	/// <summary>
	/// Represents an object that supports adding assertions.
	/// </summary>
	public interface IHas : IAsserter
	{
		/// <summary>
		/// Gets the <see cref="IHas"/> object that can be used to add assertions to the response. The added assertion will be marked as a negative assertion.
		/// </summary>
		/// <returns>The <see cref="IHas"/> object used to add assertions.</returns>
		IHas Not { get; }
	}
}
