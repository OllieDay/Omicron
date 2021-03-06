using System;
using System.Collections.Generic;
using System.Net.Http;
using MockableHttp;

namespace Omicron
{
	internal class Request : IRequest
	{
		private readonly IHttpService _httpService;
		private readonly HttpRequestMessage _request;
		private readonly IList<Action<HttpRequestMessage>> _modifications = new List<Action<HttpRequestMessage>>();

		public Request(IHttpService httpService, HttpMethod method, string uri)
		{
			_httpService = httpService;
			_request = new HttpRequestMessage(method, uri);
		}

		public IWith With => this;
		public IIs Is => SendRequest();
		public IHas Has => SendRequest();

		public IRequest Modify(Action<HttpRequestMessage> modification)
		{
			_modifications.Add(modification);

			return this;
		}

		public IResponse Assert(Action<HttpResponseMessage> assertion)
			=> SendRequest().Assert(assertion);

		public IResponse AssertPositive(Action<HttpResponseMessage> assertion)
			=> SendRequest().AssertPositive(assertion);

		public IResponse AssertNegative(Action<HttpResponseMessage> assertion)
			=> SendRequest().AssertNegative(assertion);

		public void Dispose()
		{
			_request.Dispose();
		}

		private Response SendRequest()
		{
			foreach (var modification in _modifications)
			{
				modification(_request);
			}

			// This must be called synchronously to preserve the fluent interface
			var response = _httpService.SendAsync(_request).GetAwaiter().GetResult();

			return new Response(response);
		}
	}
}
