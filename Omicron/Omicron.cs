using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MockableHttp;

namespace Omicron
{
	public sealed class Omicron
	{
		private static IHttpService _httpService;

		private readonly HttpMethod _method;
		private readonly string _uri;
		private readonly IList<Action<HttpResponseMessage>> _assertions = new List<Action<HttpResponseMessage>>();

		internal Omicron(IHttpService httpService, HttpMethod method, string uri)
		{
			// instance should not be shared between tests so we can mock the behaviour
			_httpService = httpService;
			_method = method;
			_uri = uri;
		}

		private Omicron(HttpMethod method, string uri)
		{
			// instances should be shared between tests to prevent a new socket being created for each instance
			if (_httpService == null)
			{
				_httpService = new HttpService();
			}

			_method = method;
			_uri = uri;
		}

		public Omicron Has => this;

		public static Omicron Request(HttpMethod method, string uri)
		{
			return new Omicron(method, uri);
		}

		public Omicron Status(int statusCode)
		{
			_assertions.Add(response =>
			{
				var responseStatusCode = (int)response.StatusCode;

				if (responseStatusCode != statusCode)
				{
					ThrowAssertionException("status", statusCode, responseStatusCode);
				}
			});

			return this;
		}

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

		private void ThrowAssertionException(string targetName, object expectedValue, object actualValue)
		{
			var message = CreateMessage(targetName, expectedValue, actualValue);

			throw new OmicronAssertionException(message);
		}

		private string CreateMessage(string targetName, object expectedValue, object actualValue)
		{
			return $"Expected {targetName} {expectedValue} but got {actualValue}";
		}
	}
}
