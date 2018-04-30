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
		public static IResponse Content(this IResponse @this)
		{
			return @this.Assert(response =>
			{
				if (response.Content == null)
				{
					throw new OmicronException("Expected content but got nothing");
				}
			});
		}

		public static IResponse Content(this IResponse @this, Func<HttpContent, bool> predicate)
			=> @this.AssertContent(content => content, predicate);

		public static IResponse ByteArray(this IResponse @this, byte[] content)
			=> @this.ByteArray(responseContent => responseContent.SequenceEqual(content));

		public static IResponse ByteArray(this IResponse @this, Func<byte[], bool> predicate)
			=> @this.AssertContent(content => content.ReadAsByteArrayAsync().GetAwaiter().GetResult(), predicate);

		public static IResponse String(this IResponse @this, string content)
		{
			return @this.Content().Assert(response =>
			{
				var actualContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

				if (actualContent != content)
				{
					var message = $@"Expected content:\n""{content}""\nbut got:\n""{actualContent}""";

					throw new OmicronException(message.ToString());
				}
			});
		}

		public static IResponse String(this IResponse @this, Func<string, bool> predicate)
			=> @this.AssertContent(content => content.ReadAsStringAsync().GetAwaiter().GetResult(), predicate);

		public static IResponse Json<T>(this IResponse @this, T content)
		{
			return @this.Content().Assert(response =>
			{
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
			});
		}

		public static IResponse Json(this IResponse @this, string content)
			=> @this.String(content);

		public static IResponse Json<T>(this IResponse @this, Func<T, bool> predicate)
		{
			return @this.Assert(response =>
			{
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
			});
		}

		public static IResponse Xml(this IResponse @this, XDocument content)
		{
			return @this.Content().Assert(response =>
			{
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
			});
		}

		public static IResponse Xml(this IResponse @this, string content)
			=> @this.String(content);

		public static IResponse Xml(this IResponse @this, Func<XDocument, bool> predicate)
		{
			return @this.Content().Assert(response =>
			{
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
			});
		}

		private static IResponse AssertContent<T>(this IResponse @this, Func<HttpContent, T> selector, Func<T, bool> predicate)
		{
			return @this.Content().Assert(response =>
			{
				var value = selector(response.Content);

				if (!predicate(value))
				{
					throw new OmicronException("Expected content to match");
				}
			});
		}
	}
}
