using System;
using System.Net.Http;

namespace Omicron
{
	/// <summary>
	/// Represents an object that supports adding assertions.
	/// </summary>
	public interface IAsserter
	{
		/// <summary>
		/// Adds the specified assertion to the response.
		/// </summary>
		/// <param name="assertion">An <see cref="Action<HttpResponseMessage>"/> used to modify the underlying <see cref="HttpResponseMessage"/> response.</param>
		/// <returns>An <see cref="IResponse"/> object that represents the response.
		IResponse Assert(Action<HttpResponseMessage> assertion);

		/// <summary>
		/// Adds the specified assertion to the response when performing a positive assertion. A positive assertion asserts that some expression is true.
		/// </summary>
		/// <param name="assertion">An <see cref="Action<HttpResponseMessage>"/> used to modify the underlying <see cref="HttpResponseMessage"/> response.</param>
		/// <returns>An <see cref="IResponse"/> object that represents the response.
		IResponse AssertPositive(Action<HttpResponseMessage> assertion);

		/// <summary>
		/// Adds the specified assertion to the response when performing a negative assertion. A negative assertion asserts that some expression is false.
		/// </summary>
		/// <param name="assertion">An <see cref="Action<HttpResponseMessage>"/> used to modify the underlying <see cref="HttpResponseMessage"/> response.</param>
		/// <returns>An <see cref="IResponse"/> object that represents the response.
		IResponse AssertNegative(Action<HttpResponseMessage> assertion);
	}
}
