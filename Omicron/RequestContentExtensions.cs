using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Omicron
{
	public static class RequestContentExtensions
	{
		public static IRequest Content(this IWith @this, HttpContent content)
			=> @this.Modify(request => request.Content = content);

		public static IRequest ByteArray(this IWith @this, byte[] content)
			=> @this.Content(new ByteArrayContent(content));

		public static IRequest ByteArray(this IWith @this, byte[] content, int offset, int count)
			=> @this.Content(new ByteArrayContent(content, offset, count));

		public static IRequest FormUrlEncoded(this IWith @this, IEnumerable<KeyValuePair<string, string>> nameValueCollection)
			=> @this.Content(new FormUrlEncodedContent(nameValueCollection));

		public static IRequest String(this IWith @this, string content)
			=> @this.Content(new StringContent(content));

		public static IRequest String(this IWith @this, string content, Encoding encoding)
			=> @this.Content(new StringContent(content, encoding));

		public static IRequest String(this IWith @this, string content, Encoding encoding, string mediaType)
			=> @this.Content(new StringContent(content, encoding, mediaType));

		public static IRequest Multipart(this IWith @this, params HttpContent[] values)
			=> @this.Multipart(new MultipartContent(), values);

		public static IRequest Multipart(this IWith @this, string subtype, params HttpContent[] values)
			=> @this.Multipart(new MultipartContent(subtype), values);

		public static IRequest Multipart(this IWith @this, string subtype, string boundary, params HttpContent[] values)
			=> @this.Multipart(new MultipartContent(subtype, boundary), values);

		public static IRequest MultipartFormData(this IWith @this, params HttpContent[] values)
			=> @this.MultipartFormData(new MultipartFormDataContent(), values);

		public static IRequest MultipartFormData(this IWith @this, string boundary, params HttpContent[] values)
			=> @this.MultipartFormData(new MultipartFormDataContent(boundary), values);

		public static IRequest Stream(this IWith @this, Stream content)
			=> @this.Content(new StreamContent(content));

		public static IRequest Stream(this IWith @this, Stream content, int bufferSize)
			=> @this.Content(new StreamContent(content, bufferSize));

		public static IRequest Json(this IWith @this, object content)
			=> @this.Json(JsonConvert.SerializeObject(content));

		public static IRequest Json(this IWith @this, string content)
			=> @this.Content(new StringContent(content, Encoding.UTF8, "application/json"));

		public static IRequest Xml(this IWith @this, XDocument content)
			=> @this.Xml(content.ToString());

		public static IRequest Xml(this IWith @this, string content)
			=> @this.Content(new StringContent(content, Encoding.UTF8, "application/xml"));

		private static IRequest Multipart(this IWith @this, MultipartContent content, params HttpContent[] values)
		{
			return @this.Modify(request =>
			{
				foreach (var value in values)
				{
					content.Add(value);
				}

				request.Content = content;
			});
		}

		private static IRequest MultipartFormData(this IWith @this, MultipartFormDataContent content, params HttpContent[] values)
		{
			return @this.Modify(request =>
			{
				foreach (var value in values)
				{
					content.Add(value);
				}

				request.Content = content;
			});
		}
	}
}
