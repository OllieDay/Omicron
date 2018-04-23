using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MockableHttp;

namespace Omicron
{
	public sealed class Omicron : IDisposable
	{
		private readonly IHttpService _httpService;
		private readonly HttpMethod _method;
		private readonly string _uri;
		private readonly IList<Action<HttpResponseMessage>> _assertions = new List<Action<HttpResponseMessage>>();

		internal Omicron(IHttpService httpService, HttpMethod method, string uri)
		{
			_httpService = httpService;
			_method = method;
			_uri = uri;
		}

		private Omicron(HttpMethod method, string uri) : this(new HttpService(), method, uri)
		{

		}

		public Omicron Has => this;
		public Omicron Is => this;

		public static Omicron Request(HttpMethod method, string uri)
			=> new Omicron(method, uri);

		public static Omicron Delete(string uri)
			=> Request(HttpMethod.Delete, uri);

		public static Omicron Get(string uri)
			=> Request(HttpMethod.Get, uri);

		public static Omicron Head(string uri)
			=> Request(HttpMethod.Head, uri);

		public static Omicron Options(string uri)
			=> Request(HttpMethod.Options, uri);

		public static Omicron Post(string uri)
			=> Request(HttpMethod.Post, uri);

		public static Omicron Put(string uri)
			=> Request(HttpMethod.Put, uri);

		public static Omicron Trace(string uri)
			=> Request(HttpMethod.Trace, uri);

		public async Task Run()
		{
			using (var request = new HttpRequestMessage(_method, _uri))
			{
				using (var response = await _httpService.SendAsync(request))
				{
					foreach (var assertion in _assertions)
					{
						assertion(response);
					}
				}
			}
		}

		public void Dispose()
			=> _httpService.Dispose();

		internal Omicron Return(Action action)
		{
			action();

			return this;
		}

		internal Omicron AddAssertion(Action<HttpResponseMessage> assertion)
			=> Return(() => _assertions.Add(assertion));

		internal void Fail(string targetName, object expectedValue, object actualValue)
		{
			var message = CreateMessage(targetName, expectedValue, actualValue);

			throw new OmicronAssertionException(message);
		}

		private string CreateMessage(string targetName, object expectedValue, object actualValue)
			=> $"Expected {targetName} {expectedValue} but got {actualValue}";
	}
}
