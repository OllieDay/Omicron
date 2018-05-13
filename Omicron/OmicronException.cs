using System;

namespace Omicron
{
	/// <summary>
	/// The exception that is thrown when an assertion fails.
	/// </summary>
	public sealed class OmicronException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="OmicronException"/> class with the specified error message.
		/// </summary>
		/// <param name="message">The error message that explains the reason for this exception.</param>
		public OmicronException(string message) : base(message)
		{

		}
	}
}
