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
		public void ShouldNotThrowExceptionWhenContentExists()
		{
			var request = CreateResponseWithContent(new StringContent(string.Empty));

			Action run = () => request.Has.Content();

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenNotContentDoesNotExist()
		{
			var request = CreateResponseWithContent(null);

			Action run = () => request.Has.Not.Content();

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenContentDoesNotExist()
		{
			var request = CreateResponseWithContent(null);

			Action run = () => request.Has.Content();

			run.Should().Throw<Exception>().WithMessage("Expected content to exist");
		}

		[Fact]
		public void ShouldThrowExceptionWhenNotContentExists()
		{
			var request = CreateResponseWithContent(new StringContent("..."));

			Action run = () => request.Has.Not.Content();

			run.Should().Throw<Exception>().WithMessage("Expected content to not exist");
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenContetWithPredicateMatches()
		{
			var request = CreateResponseWithContent(new StringContent(string.Empty));

			Action run = () => request.Has.Content(_ => true);

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenContentWithPredicateDoesNotMatch()
		{
			var request = CreateResponseWithContent(new StringContent(string.Empty));

			Action run = () => request.Has.Content(_ => false);

			run.Should().Throw<Exception>().WithMessage("Expected content to match");
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenByteArrayWithByteArrayMatches()
		{
			var content = new byte[] { 1, 2, 3 };
			var request = CreateResponseWithContent(new ByteArrayContent(content));

			Action run = () => request.Has.ByteArray(content);

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenNotByteArrayWithByteArrayDoesNotMatch()
		{
			var request = CreateResponseWithContent(new ByteArrayContent(new byte[0]));

			Action run = () => request.Has.Not.ByteArray(new byte[] { 1, 2, 3 });

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenByteArrayWithByteArrayDoesNotMatch()
		{
			var request = CreateResponseWithContent(new ByteArrayContent(new byte[0]));

			Action run = () => request.Has.ByteArray(new byte[] { 1, 2, 3, });

			run.Should().Throw<Exception>().WithMessage("Expected content to match");
		}

		[Fact]
		public void ShouldThrowExceptionWhenNotByteArrayWithByteArrayMatches()
		{
			var content = new byte[] { 1, 2, 3 };
			var request = CreateResponseWithContent(new ByteArrayContent(content));

			Action run = () => request.Has.Not.ByteArray(content);

			run.Should().Throw<Exception>().WithMessage("Expected content to not match");
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenByteArrayWithPredicateMatches()
		{
			var request = CreateResponseWithContent(new ByteArrayContent(new byte[0]));

			Action run = () => request.Has.ByteArray(_ => true);

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenByteArrayWithPredicateDoesNotMatch()
		{
			var request = CreateResponseWithContent(new ByteArrayContent(new byte[0]));

			Action run = () => request.Has.ByteArray(_ => false);

			run.Should().Throw<Exception>().WithMessage("Expected content to match");
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenStringWithStringMatches()
		{
			const string content = "Omicron";

			var request = CreateResponseWithContent(new StringContent(content));

			Action run = () => request.Has.String(content);

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenNotStringWithStringDoesNotMatch()
		{
			var request = CreateResponseWithContent(new StringContent(string.Empty));

			Action run = () => request.Has.Not.String("Omicron");

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenStringWithStringDoesNotMatch()
		{
			var request = CreateResponseWithContent(new StringContent("Omicron"));

			Action run = () => request.Has.String("Norcimo");

			run.Should().Throw<Exception>().WithMessage(@"Expected content:\n""Norcimo""\nbut got:\n""Omicron""");
		}

		[Fact]
		public void ShouldThrowExceptionWhenNotStringWithStringMatches()
		{
			var request = CreateResponseWithContent(new StringContent("Omicron"));

			Action run = () => request.Has.Not.String("Omicron");

			run.Should().Throw<Exception>().WithMessage(@"Expected content to not be ""Omicron""");
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenStringWithPredicateDoesNotMatch()
		{
			var request = CreateResponseWithContent(new StringContent(string.Empty));

			Action run = () => request.Has.String(_ => true);

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenStringWithPredicateDoesNotMatch()
		{
			var request = CreateResponseWithContent(new StringContent(string.Empty));

			Action run = () => request.Has.String(_ => false);

			run.Should().Throw<Exception>().WithMessage("Expected content to match");
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenJsonWithObjectMatches()
		{
			var request = CreateResponseWithContent(new StringContent(@"{""name"":""Omicron""}"));

			Action run = () => request.Has.Json(new
			{
				Name = "Omicron"
			});

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenNotJsonWithObjectDoesNotMatch()
		{
			var request = CreateResponseWithContent(new StringContent(@"{""name"":""Omicron""}"));

			Action run = () => request.Has.Not.Json(new
			{
				Name = "Norcimo"
			});

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenJsonWithObjectDoesNotMatch()
		{
			var request = CreateResponseWithContent(new StringContent(@"{""name"":""Omicron""}"));

			Action run = () => request.Has.Json(new
			{
				Name = "Norcimo"
			});

			run.Should().Throw<Exception>().WithMessage(@"Expected content:\n""{ Name = Norcimo }""\nbut got:\n""{""name"":""Omicron""}""");
		}

		[Fact]
		public void ShouldThrowExceptionWhenNotJsonWithObjectMatches()
		{
			var request = CreateResponseWithContent(new StringContent(@"{""name"":""Omicron""}"));

			Action run = () => request.Has.Not.Json(new
			{
				Name = "Omicron"
			});

			run.Should().Throw<Exception>().WithMessage(@"Expected content to not be ""{ Name = Omicron }""");
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenJsonWithStringMatches()
		{
			const string json = @"{""name"":""Omicron""}";

			var request = CreateResponseWithContent(new StringContent(json));

			Action run = () => request.Has.Json(json);

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenNotJsonWithStringDoesNotMatch()
		{
			var request = CreateResponseWithContent(new StringContent(@"{""name"":""Omicron""}"));

			Action run = () => request.Has.Not.Json(@"{""name"":""Norcimo""}");

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenJsonWithStringDoesNotMatch()
		{
			var request = CreateResponseWithContent(new StringContent(@"{""name"":""Omicron""}"));

			Action run = () => request.Has.Json(@"{""name"":""Norcimo""}");

			run.Should().Throw<Exception>().WithMessage(@"Expected content:\n""{""name"":""Norcimo""}""\nbut got:\n""{""name"":""Omicron""}""");
		}

		[Fact]
		public void ShouldThrowExceptionWhenNotJsonWithStringMatches()
		{
			const string content = @"{""name"":""Omicron""}";

			var request = CreateResponseWithContent(new StringContent(content));

			Action run = () => request.Has.Not.Json(content);

			run.Should().Throw<Exception>().WithMessage(@"Expected content to not be ""{""name"":""Omicron""}""");
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenJsonWithPredicateMatches()
		{
			var request = CreateResponseWithContent(new StringContent(@"{""name"":""Omicron""}"));

			Action run = () => request.Has.Json<object>(_ => true);

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenJsonWithPredicateDoesNotMatch()
		{
			var request = CreateResponseWithContent(new StringContent(@"{""name"":""Omicron""}"));

			Action run = () => request.Has.Json<object>(_ => false);

			run.Should().Throw<Exception>().WithMessage("Expected content to match");
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenXmlWithXDocumentMatches()
		{
			var request = CreateResponseWithContent(new StringContent("<Omicron />"));

			Action run = () => request.Has.Xml(new XDocument(new XElement("Omicron")));

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenNotXmlWithXDocumentDoesNotMatch()
		{
			var request = CreateResponseWithContent(new StringContent("<Omicron />"));

			Action run = () => request.Has.Not.Xml(new XDocument(new XElement("Norcimo")));

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenXmlWithXDocumentDoesNotMatch()
		{
			var request = CreateResponseWithContent(new StringContent("<Omicron />"));

			Action run = () => request.Has.Xml(new XDocument(new XElement("Norcimo")));

			run.Should().Throw<Exception>().WithMessage(@"Expected content:\n""<Norcimo />""\nbut got:\n""<Omicron />""");
		}

		[Fact]
		public void ShouldThrowExceptionWhenNotXmlWithXDocumentMatches()
		{
			var request = CreateResponseWithContent(new StringContent("<Omicron />"));

			Action run = () => request.Has.Not.Xml(new XDocument(new XElement("Omicron")));

			run.Should().Throw<Exception>().WithMessage(@"Expected content to not be ""<Omicron />""");
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenXmlWithStringMatches()
		{
			const string content = "<Omicron />";

			var request = CreateResponseWithContent(new StringContent(content));

			Action run = () => request.Has.Xml(content);

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenNotXmlWithStringDoesNotMatch()
		{
			var request = CreateResponseWithContent(new StringContent("<Omicron />"));

			Action run = () => request.Has.Not.Xml("<Norcimo />");

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenXmlWithStringDoesNotMatch()
		{
			var request = CreateResponseWithContent(new StringContent("<Omicron />"));

			Action run = () => request.Has.Xml("<Norcimo />");

			run.Should().Throw<Exception>().WithMessage(@"Expected content:\n""<Norcimo />""\nbut got:\n""<Omicron />""");
		}

		[Fact]
		public void ShouldThrowExceptionWhenNotXmlWithStringMatches()
		{
			var request = CreateResponseWithContent(new StringContent("<Omicron />"));

			Action run = () => request.Has.Not.Xml("<Omicron />");

			run.Should().Throw<Exception>().WithMessage(@"Expected content to not be ""<Omicron />""");
		}

		[Fact]
		public void ShouldNotThrowExceptionWhenXmlWithPredicateMatches()
		{
			var request = CreateResponseWithContent(new StringContent("<Omicron />"));

			Action run = () => request.Has.Xml(_ => true);

			run.Should().NotThrow();
		}

		[Fact]
		public void ShouldThrowExceptionWhenXmlWithPredicateDoesNotMatch()
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
