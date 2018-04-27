using System;
using System.Net.Http;
using System.Threading.Tasks;
using MockableHttp;
using NSubstitute;
using Xunit;

namespace Omicron.Tests
{
	public sealed class RequestVersionExtensionTests
	{
		[Theory]
		[InlineData(1, 0)]
		[InlineData(1, 1)]
		[InlineData(2, 0)]
		public async Task ShouldSetVersion(int major, int minor)
		{
			var version = new Version(major, minor);
			var httpService = Substitute.For<IHttpService>();
			var request = new Request(httpService, HttpMethod.Head, string.Empty);

			request.Version(version);
			request.Assert(Stubs.Noop);

			await httpService.Received().SendAsync(Arg.Is<HttpRequestMessage>(httpRequestMessage =>
				httpRequestMessage.Version == version
			));
		}
	}
}
