using System;

namespace Omicron
{
	public static class QueryExtensions
	{
		public static IRequest Query(this IRequest @this, string name)
			=> @this.Modify(request => request.RequestUri = AppendQuery(request.RequestUri, name));

		public static IRequest Query(this IRequest @this, string name, object value)
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
