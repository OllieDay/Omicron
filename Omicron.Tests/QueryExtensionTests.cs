using System;
using System.Net.Http;
using System.Threading.Tasks;
using MockableHttp;
using NSubstitute;
using Xunit;

namespace Omicron.Tests
{
	public sealed class QueryExtensionTests
	{
		[Fact]
		public async Task ShouldAddQueryWithNameAsFirstQuery()
			=> await SetQueryAndVerifyIsSet(request => request.With.Query("first"), "https://example.com", "https://example.com/?first");

		[Fact]
		public async Task ShouldAddQueryWithNameAndValueAsFirstQuery()
			=> await SetQueryAndVerifyIsSet(request => request.With.Query("first", "first"), "https://example.com", "https://example.com/?first=first");

		[Fact]
		public async Task ShouldAddQueryWithNameAsSecondQuery()
			=> await SetQueryAndVerifyIsSet(request => request.With.Query("second"), "https://example.com?first", "https://example.com/?first&second");

		[Fact]
		public async Task ShouldAddQueryWithNameAndValueAsSecondQuery()
			=> await SetQueryAndVerifyIsSet(request => request.With.Query("second", "second"), "https://example.com?first=first", "https://example.com/?first=first&second=second");

		private static async Task SetQueryAndVerifyIsSet(Action<IRequest> setter, string baseUri, string absoluteUri)
		{
			var httpService = Substitute.For<IHttpService>();
			var request = new Request(httpService, HttpMethod.Head, baseUri);

			setter(request);
			request.Assert(Stubs.Noop);

			await httpService.Received().SendAsync(Arg.Is<HttpRequestMessage>(httpRequestMessage =>
				httpRequestMessage.RequestUri.AbsoluteUri == absoluteUri
			));
		}
	}
}
