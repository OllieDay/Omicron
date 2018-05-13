using System;

namespace Omicron
{
	public static class RequestQueryExtensions
	{
		/// <summary>
		/// Adds a parameter to the query string of the request with the specified name.
		/// </summary>
		/// <param name="name">The parameter name.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Query(this IWith @this, string name)
			=> @this.Modify(request => request.RequestUri = AppendQuery(request.RequestUri, name));

		/// <summary>
		/// Adds a parameter to the query string of the request with the specified name and value.
		/// </summary>
		/// <param name="name">The parameter name.</param>
		/// <param name="value">The parameter value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Query(this IWith @this, string name, object value)
			=> @this.Modify(request => request.RequestUri = AppendQuery(request.RequestUri, $"{name}={value}"));

		private static Uri AppendQuery(Uri uri, string query)
		{
			var uriBuilder = new UriBuilder(uri);

			if (!string.IsNullOrEmpty(uriBuilder.Query))
			{
				// Remove ? as it is always added when setting UriBuilder.Query
				query = $"{uriBuilder.Query.Substring(1)}&{query}";
			}

			uriBuilder.Query = query;

			return uriBuilder.Uri;
		}
	}
}
