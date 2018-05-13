using System.Net.Http;
using MockableHttp;

namespace Omicron
{
	public static class Omicron
	{
		private static readonly IHttpService HttpService = new HttpService();

		public static IRequest Request(HttpMethod method, string uri)
			=> new Request(HttpService, method, uri);

		public static IRequest Delete(string uri)
			=> Request(HttpMethod.Delete, uri);

		public static IRequest Get(string uri)
			=> Request(HttpMethod.Get, uri);

		public static IRequest Head(string uri)
			=> Request(HttpMethod.Head, uri);

		public static IRequest Options(string uri)
			=> Request(HttpMethod.Options, uri);

		public static IRequest Post(string uri)
			=> Request(HttpMethod.Post, uri);

		public static IRequest Put(string uri)
			=> Request(HttpMethod.Put, uri);

		public static IRequest Trace(string uri)
			=> Request(HttpMethod.Trace, uri);
	}
}
