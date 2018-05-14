using System;
using System.Net.Http;

namespace Omicron
{
	/// <summary>
	/// Represents an object used to add assertions to the response.
	/// </summary>
	public interface IAsserter
	{
		/// <summary>
		/// Adds the specified assertion to the response.
		/// </summary>
		/// <param name="assertion">The action used to assert the underlying <see cref="HttpResponseMessage"/> response.</param>
		/// <returns>An <see cref="IResponse"/> object that represents the response.</returns>
		IResponse Assert(Action<HttpResponseMessage> assertion);

		/// <summary>
		/// Adds the specified assertion to the response when performing a positive assertion. A positive assertion asserts that something is true.
		/// </summary>
		/// <param name="assertion">The action used to assert the underlying <see cref="HttpResponseMessage"/> response.</param>
		/// <returns>An <see cref="IResponse"/> object that represents the response.</returns>
		IResponse AssertPositive(Action<HttpResponseMessage> assertion);

		/// <summary>
		/// Adds the specified assertion to the response when performing a negative assertion. A negative assertion asserts that something is false.
		/// </summary>
		/// <param name="assertion">The action used to assert the underlying <see cref="HttpResponseMessage"/> response.</param>
		/// <returns>An <see cref="IResponse"/> object that represents the response.</returns>
		IResponse AssertNegative(Action<HttpResponseMessage> assertion);
	}
}
