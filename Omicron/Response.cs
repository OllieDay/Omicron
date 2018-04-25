using System;
using System.Net.Http;

namespace Omicron
{
	internal class Response : IResponse
	{
		private readonly HttpResponseMessage _response;

		public Response(HttpResponseMessage response)
		{
			_response = response;
		}

		public IResponse Is => this;
		public IResponse Has => this;

		public IResponse Assert(Action<HttpResponseMessage> assertion)
		{
			assertion(_response);

			return this;
		}
	}
}
