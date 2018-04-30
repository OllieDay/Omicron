using System;
using System.Net.Http;

namespace Omicron
{
	internal class Response : IResponse, IIs, IHas
	{
		private readonly HttpResponseMessage _response;

		public Response(HttpResponseMessage response)
		{
			_response = response;
		}

		public IIs Is => this;
		public IHas Has => this;

		public IResponse Assert(Action<HttpResponseMessage> assertion)
		{
			assertion(_response);

			return this;
		}

		public void Dispose()
		{
			_response.Dispose();
		}
	}
}
