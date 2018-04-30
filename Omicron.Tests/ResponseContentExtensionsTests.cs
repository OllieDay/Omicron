using System;
using System.Net.Http;
using System.Xml.Linq;
using FluentAssertions;
using MockableHttp;
using NSubstitute;
using Xunit;

namespace Omicron.Tests
{
	public sealed class ResponseContentExtensionTests
	{
		[Fact]
		public void ShouldNotThrowExceptionWhenContentIsSet()
		{
			var request = CreateResponseWithContent(new StringContent(string.Empty));

			Action run = () => request.Has.Content();

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenContentIsNotSet()
		{
			var request = CreateResponseWithContent(null);

			Action run = () => request.Has.Content();

			run.Should().Throw<Exception>().WithMessage("Expected content but got nothing");
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenContetWithPredicateSucceeds()
		{
			var request = CreateResponseWithContent(new StringContent(string.Empty));

			Action run = () => request.Has.Content(_ => true);

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenContentWithPredicateFails()
		{
			var request = CreateResponseWithContent(new StringContent(string.Empty));

			Action run = () => request.Has.Content(_ => false);

			run.Should().Throw<Exception>().WithMessage("Expected content to match");
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenByteArrayWithByteArrayIsSet()
		{
			var content = new byte[] { 1, 2, 3 };
			var request = CreateResponseWithContent(new ByteArrayContent(content));

			Action run = () => request.Has.ByteArray(content);

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenByteArrayWithByteArrayIsNotSet()
		{
			var request = CreateResponseWithContent(new ByteArrayContent(new byte[0]));

			Action run = () => request.Has.ByteArray(new byte[] { 1, 2, 3, });

			run.Should().Throw<Exception>().WithMessage("Expected content to match");
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenByteArrayWithPredicateSucceeds()
		{
			var request = CreateResponseWithContent(new ByteArrayContent(new byte[0]));

			Action run = () => request.Has.ByteArray(_ => true);

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenByteArrayWithPredicateFails()
		{
			var request = CreateResponseWithContent(new ByteArrayContent(new byte[0]));

			Action run = () => request.Has.ByteArray(_ => false);

			run.Should().Throw<Exception>().WithMessage("Expected content to match");
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenStringWithStringIsSet()
		{
			const string content = "Omicron";

			var request = CreateResponseWithContent(new StringContent(content));

			Action run = () => request.Has.String(content);

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenStringWithStringIsNotSet()
		{
			var request = CreateResponseWithContent(new StringContent("Omicron"));

			Action run = () => request.Has.String("Norcimo");

			run.Should().Throw<Exception>().WithMessage(@"Expected content:\n""Norcimo""\nbut got:\n""Omicron""");
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenStringWithPredicateSucceeds()
		{
			var request = CreateResponseWithContent(new StringContent(string.Empty));

			Action run = () => request.Has.String(_ => true);

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenStringWithPredicateFails()
		{
			var request = CreateResponseWithContent(new StringContent(string.Empty));

			Action run = () => request.Has.String(_ => false);

			run.Should().Throw<Exception>().WithMessage("Expected content to match");
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenJsonWithObjectIsSet()
		{
			var request = CreateResponseWithContent(new StringContent(@"{""name"":""Omicron""}"));

			Action run = () => request.Has.Json(new
			{
				Name = "Omicron"
			});

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenJsonWithObjectIsNotSet()
		{
			var request = CreateResponseWithContent(new StringContent(@"{""name"":""Omicron""}"));

			Action run = () => request.Has.Json(new
			{
				Name = "Norcimo"
			});

			run.Should().Throw<Exception>().WithMessage(@"Expected content:\n""{ Name = Norcimo }""\nbut got:\n""{""name"":""Omicron""}""");
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenJsonWithStringIsSet()
		{
			const string json = @"{""name"":""Omicron""}";

			var request = CreateResponseWithContent(new StringContent(json));

			Action run = () => request.Has.Json(json);

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenJsonWithStringIsNotSet()
		{
			var request = CreateResponseWithContent(new StringContent(@"{""name"":""Omicron""}"));

			Action run = () => request.Has.Json(@"{""name"":""Norcimo""}");

			run.Should().Throw<Exception>().WithMessage(@"Expected content:\n""{""name"":""Norcimo""}""\nbut got:\n""{""name"":""Omicron""}""");
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenJsonWithPredicateSucceeds()
		{
			var request = CreateResponseWithContent(new StringContent(@"{""name"":""Omicron""}"));

			Action run = () => request.Has.Json<object>(_ => true);

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenJsonWithPredicateFails()
		{
			var request = CreateResponseWithContent(new StringContent(@"{""name"":""Omicron""}"));

			Action run = () => request.Has.Json<object>(_ => false);

			run.Should().Throw<Exception>().WithMessage("Expected content to match");
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenXmlWithXDocumentIsSet()
		{
			var request = CreateResponseWithContent(new StringContent("<Omicron />"));

			Action run = () => request.Has.Xml(new XDocument(new XElement("Omicron")));

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenXmlWithXDocumentIsNotSet()
		{
			var request = CreateResponseWithContent(new StringContent("<Omicron />"));

			Action run = () => request.Has.Xml(new XDocument(new XElement("Norcimo")));

			run.Should().Throw<Exception>().WithMessage(@"Expected content:\n""<Norcimo />""\nbut got:\n""<Omicron />""");
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenXmlWithStringIsSet()
		{
			const string content = "<Omicron />";

			var request = CreateResponseWithContent(new StringContent(content));

			Action run = () => request.Has.Xml(content);

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenXmlWithStringIsNotSet()
		{
			var request = CreateResponseWithContent(new StringContent("<Omicron />"));

			Action run = () => request.Has.Xml("<Norcimo />");

			run.Should().Throw<Exception>().WithMessage(@"Expected content:\n""<Norcimo />""\nbut got:\n""<Omicron />""");
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenXmlWithPredicateSucceeds()
		{
			var request = CreateResponseWithContent(new StringContent("<Omicron />"));

			Action run = () => request.Has.Xml(_ => true);

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenXmlWithPredicateFails()
		{
			var request = CreateResponseWithContent(new StringContent("<Omicron />"));

			Action run = () => request.Has.Xml(_ => false);

			run.Should().Throw<Exception>().WithMessage("Expected content to match");
		}

		private static IResponse CreateResponseWithContent(HttpContent content)
		{
			var httpService = Substitute.For<IHttpService>();
			httpService.SendAsync(Arg.Any<HttpRequestMessage>()).ReturnsForAnyArgs(new HttpResponseMessage
			{
				Content = content
			});
			var request = new Request(httpService, HttpMethod.Head, string.Empty);

			return request.Assert(Stubs.Noop);
		}
	}
}
