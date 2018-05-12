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
		[InlineData(0, 9)]
		[InlineData(1, 0)]
		[InlineData(1, 1)]
		[InlineData(2, 0)]
		public async Task ShouldSetVersionWithVersion(int major, int minor)
		{
			var version = new Version(major, minor);

			await SetVersionAndVerifyIsSet(request => request.With.Version(version), version);
		}

		[Theory]
		[InlineData("0.9")]
		[InlineData("1.0")]
		[InlineData("1.1")]
		[InlineData("2.0")]
		public async Task ShouldSetVersionWithString(string version)
			=> await SetVersionAndVerifyIsSet(request => request.With.Version(version), new Version(version));

		private static async Task SetVersionAndVerifyIsSet(Action<IRequest> setter, Version version)
		{
			var httpService = Substitute.For<IHttpService>();
			var request = new Request(httpService, HttpMethod.Head, string.Empty);

			setter(request);
			request.Assert(Stubs.Noop);

			await httpService.Received().SendAsync(Arg.Is<HttpRequestMessage>(httpRequestMessage =>
				httpRequestMessage.Version == version
			));
		}
	}
}
