using System;
using System.Collections.Generic;
using System.Net.Http;
using MockableHttp;

namespace Omicron
{
	internal class Request : IRequest
	{
		private readonly IHttpService _httpService;
		private readonly HttpMethod _method;
		private readonly string _uri;
		private readonly IList<Action<HttpRequestMessage>> _modifications = new List<Action<HttpRequestMessage>>();

		public Request(IHttpService httpService, HttpMethod method, string uri)
		{
			_httpService = httpService;
			_method = method;
			_uri = uri;
		}

		public IRequest With => this;
		public IResponse Is => SendRequest();
		public IResponse Has => SendRequest();

		public IRequest Modify(Action<HttpRequestMessage> modification)
		{
			_modifications.Add(modification);

			return this;
		}

		public IResponse Assert(Action<HttpResponseMessage> assertion)
			=> SendRequest().Assert(assertion);

		private IResponse SendRequest()
		{
			using (var request = new HttpRequestMessage(_method, _uri))
			{
				foreach (var modification in _modifications)
				{
					modification(request);
				}

				// This must be called synchronously to preserve the fluent interface
				var response = _httpService.SendAsync(request).GetAwaiter().GetResult();

				return new Response(response);
			}
		}
	}
}
