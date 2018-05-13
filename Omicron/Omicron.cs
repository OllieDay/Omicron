using System.Net.Http;
using MockableHttp;

namespace Omicron
{
	/// <summary>
	/// Used to create <see cref="IRequest"/> objects that represent an HTTP request.
	/// </summary>
	public static class Omicron
	{
		private static readonly IHttpService HttpService = new HttpService();

		/// <summary>
		/// Sends an HTTP request to the specified URI.
		/// </summary>
		/// <param name="method">The HTTP method.</param>
		/// <param name="uri">The URI the request is sent to.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Request(HttpMethod method, string uri)
			=> new Request(HttpService, method, uri);

		/// <summary>
		/// Sends an HTTP DELETE request to the specified URI.
		/// </summary>
		/// <param name="uri">The URI the request is sent to.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Delete(string uri)
			=> Request(HttpMethod.Delete, uri);

		/// <summary>
		/// Sends an HTTP GET request to the specified URI.
		/// </summary>
		/// <param name="uri">The URI the request is sent to.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Get(string uri)
			=> Request(HttpMethod.Get, uri);

		/// <summary>
		/// Sends an HTTP HEAD request to the specified URI.
		/// </summary>
		/// <param name="uri">The URI the request is sent to.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Head(string uri)
			=> Request(HttpMethod.Head, uri);

		/// <summary>
		/// Sends an HTTP OPTIONS request to the specified URI.
		/// </summary>
		/// <param name="uri">The URI the request is sent to.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Options(string uri)
			=> Request(HttpMethod.Options, uri);

		/// <summary>
		/// Sends an HTTP POST request to the specified URI.
		/// </summary>
		/// <param name="uri">The URI the request is sent to.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Post(string uri)
			=> Request(HttpMethod.Post, uri);

		/// <summary>
		/// Sends an HTTP PUT request to the specified URI.
		/// </summary>
		/// <param name="uri">The URI the request is sent to.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Put(string uri)
			=> Request(HttpMethod.Put, uri);

		/// <summary>
		/// Sends an HTTP TRACE request to the specified URI.
		/// </summary>
		/// <param name="uri">The URI the request is sent to.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Trace(string uri)
			=> Request(HttpMethod.Trace, uri);
	}
}
