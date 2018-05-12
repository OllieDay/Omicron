using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Omicron
{
	public static class ResponseContentExtensions
	{
		public static IResponse Content(this IHas @this)
		{
			@this.AssertPositive(AssertContentExists);
			@this.AssertNegative(AssertContentDoesNotExist);

			return (IResponse)@this;
		}

		public static IResponse Content(this IHas @this, Func<HttpContent, bool> predicate)
			=> @this.AssertContentMatches(content => content, predicate);

		public static IResponse ByteArray(this IHas @this, byte[] content)
		{
			@this.AssertPositive(AssertContentEqual(content));
			@this.AssertNegative(AssertContentNotEqual(content));

			return (IResponse)@this;
		}

		public static IResponse ByteArray(this IHas @this, Func<byte[], bool> predicate)
			=> @this.AssertContentMatches(content => content.ReadAsByteArrayAsync().GetAwaiter().GetResult(), predicate);

		public static IResponse String(this IHas @this, string content)
		{
			@this.AssertPositive(AssertContentEqual(content));
			@this.AssertNegative(AssertContentNotEqual(content));

			return (IResponse)@this;
		}

		public static IResponse String(this IHas @this, Func<string, bool> predicate)
			=> @this.AssertContentMatches(content => content.ReadAsStringAsync().GetAwaiter().GetResult(), predicate);

		public static IResponse Json<T>(this IHas @this, T content)
		{
			@this.AssertPositive(AssertContentEqual(content));
			@this.AssertNegative(AssertContentNotEqual(content));

			return (IResponse)@this;
		}

		public static IResponse Json(this IHas @this, string content)
			=> @this.String(content);

		public static IResponse Json<T>(this IHas @this, Func<T, bool> predicate)
			=> @this.Assert(AssertContentMatches(predicate));

		public static IResponse Xml(this IHas @this, XDocument content)
		{
			@this.AssertPositive(AssertContentEqual(content));
			@this.AssertNegative(AssertContentNotEqual(content));

			return (IResponse)@this;
		}

		public static IResponse Xml(this IHas @this, string content)
			=> @this.String(content);

		public static IResponse Xml(this IHas @this, Func<XDocument, bool> predicate)
			=> @this.Assert(AssertContentMatches(predicate));

		private static void AssertContentExists(HttpResponseMessage response)
		{
			if (response.Content == null)
			{
				throw new OmicronException("Expected content to exist");
			}
		}

		private static void AssertContentDoesNotExist(HttpResponseMessage response)
		{
			if (response.Content != null)
			{
				throw new OmicronException("Expected content to not exist");
			}
		}

		private static Action<HttpResponseMessage> AssertContentEqual(byte[] content)
		{
			return response =>
			{
				AssertContentExists(response);

				if (!response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult().SequenceEqual(content))
				{
					throw new OmicronException("Expected content to match");
				}
			};
		}

		private static Action<HttpResponseMessage> AssertContentNotEqual(byte[] content)
		{
			return response =>
			{
				AssertContentExists(response);

				if (response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult().SequenceEqual(content))
				{
					throw new OmicronException("Expected content to not match");
				}
			};
		}

		private static Action<HttpResponseMessage> AssertContentEqual(string content)
		{
			return response =>
			{
				AssertContentExists(response);

				var actualContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

				if (actualContent != content)
				{
					var message = $@"Expected content:\n""{content}""\nbut got:\n""{actualContent}""";

					throw new OmicronException(message);
				}
			};
		}

		private static Action<HttpResponseMessage> AssertContentNotEqual(string content)
		{
			return response =>
			{
				AssertContentExists(response);

				var actualContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

				if (actualContent == content)
				{
					var message = $@"Expected content to not be ""{content}""";

					throw new OmicronException(message);
				}
			};
		}

		private static Action<HttpResponseMessage> AssertContentEqual<T>(T content)
		{
			return response =>
			{
				AssertContentExists(response);

				var actualContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

				try
				{
					var expected = JObject.FromObject(content);
					var actual = JObject.FromObject(JsonConvert.DeserializeObject<T>(actualContent));

					if (JObject.DeepEquals(actual, expected))
					{
						return;
					}
				}
				catch (JsonException)
				{

				}

				var message = $@"Expected content:\n""{content}""\nbut got:\n""{actualContent}""";

				throw new OmicronException(message);
			};
		}

		private static Action<HttpResponseMessage> AssertContentNotEqual<T>(T content)
		{
			return response =>
			{
				AssertContentExists(response);

				var actualContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

				try
				{
					var expected = JObject.FromObject(content);
					var actual = JObject.FromObject(JsonConvert.DeserializeObject<T>(actualContent));

					if (!JObject.DeepEquals(actual, expected))
					{
						return;
					}
				}
				catch (JsonException)
				{

				}

				var message = $@"Expected content to not be ""{content}""";

				throw new OmicronException(message);
			};
		}

		private static Action<HttpResponseMessage> AssertContentEqual(XDocument content)
		{
			return response =>
			{
				AssertContentExists(response);

				var actualContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

				try
				{
					var actualXmlContent = XDocument.Parse(actualContent);

					if (XDocument.DeepEquals(content, actualXmlContent))
					{
						return;
					}
				}
				catch (XmlException)
				{

				}

				var message = $@"Expected content:\n""{content}""\nbut got:\n""{actualContent}""";

				throw new OmicronException(message);
			};
		}

		private static Action<HttpResponseMessage> AssertContentNotEqual(XDocument content)
		{
			return response =>
			{
				AssertContentExists(response);

				var actualContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

				try
				{
					var actualXmlContent = XDocument.Parse(actualContent);

					if (!XDocument.DeepEquals(content, actualXmlContent))
					{
						return;
					}
				}
				catch (XmlException)
				{

				}

				var message = $@"Expected content to not be ""{content}""";

				throw new OmicronException(message);
			};
		}

		private static Action<HttpResponseMessage> AssertContentMatches<T>(Func<T, bool> predicate)
		{
			return response =>
			{
				AssertContentExists(response);

				var actualContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

				try
				{
					var actualJsonContent = JsonConvert.DeserializeObject<T>(actualContent);

					if (predicate(actualJsonContent))
					{
						return;
					}
				}
				catch (JsonException)
				{

				}

				throw new OmicronException("Expected content to match");
			};
		}

		public static Action<HttpResponseMessage> AssertContentMatches(Func<XDocument, bool> predicate)
		{
			return response =>
			{
				AssertContentExists(response);

				var actualContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

				try
				{
					var actualXmlContent = XDocument.Parse(actualContent);

					if (predicate(actualXmlContent))
					{
						return;
					}
				}
				catch (XmlException)
				{

				}

				throw new OmicronException("Expected content to match");
			};
		}

		private static IResponse AssertContentMatches<T>(this IHas @this, Func<HttpContent, T> selector, Func<T, bool> predicate)
		{
			return @this.Assert(response =>
			{
				AssertContentExists(response);

				var value = selector(response.Content);

				if (!predicate(value))
				{
					throw new OmicronException("Expected content to match");
				}
			});
		}
	}
}
