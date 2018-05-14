using System;
using System.Net.Http;

namespace Omicron
{
	/// <summary>
	/// Represents an object that supports adding assertions.
	/// </summary>
	public interface IIs : IAsserter
	{
		/// <summary>
		/// Gets the <see cref="IIs"/> object used to add assertions to the response. The added assertion will be marked as a negative assertion
		/// </summary>
		/// <returns>The <see cref="IIs"/> object used to add assertions.</returns>
		IIs Not { get; }
	}
}
