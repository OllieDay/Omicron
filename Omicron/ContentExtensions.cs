using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Omicron
{
	public static class ContentExtensions
	{
		public static IRequest Content(this IRequest @this, HttpContent content)
			=> @this.Modify(request => request.Content = content);

		public static IRequest ByteArray(this IRequest @this, byte[] content)
			=> @this.Content(new ByteArrayContent(content));

		public static IRequest ByteArray(this IRequest @this, byte[] content, int offset, int count)
			=> @this.Content(new ByteArrayContent(content, offset, count));

		public static IRequest FormUrlEncoded(this IRequest @this, IEnumerable<KeyValuePair<string, string>> nameValueCollection)
			=> @this.Content(new FormUrlEncodedContent(nameValueCollection));

		public static IRequest String(this IRequest @this, string content)
			=> @this.Content(new StringContent(content));

		public static IRequest String(this IRequest @this, string content, Encoding encoding)
			=> @this.Content(new StringContent(content, encoding));

		public static IRequest String(this IRequest @this, string content, Encoding encoding, string mediaType)
			=> @this.Content(new StringContent(content, encoding, mediaType));

		public static IRequest Multipart(this IRequest @this, params HttpContent[] values)
			=> @this.Multipart(new MultipartContent(), values);

		public static IRequest Multipart(this IRequest @this, string subtype, params HttpContent[] values)
			=> @this.Multipart(new MultipartContent(subtype), values);

		public static IRequest Multipart(this IRequest @this, string subtype, string boundary, params HttpContent[] values)
			=> @this.Multipart(new MultipartContent(subtype, boundary), values);

		public static IRequest MultipartFormData(this IRequest @this, params HttpContent[] values)
			=> @this.MultipartFormData(new MultipartFormDataContent(), values);

		public static IRequest MultipartFormData(this IRequest @this, string boundary, params HttpContent[] values)
			=> @this.MultipartFormData(new MultipartFormDataContent(boundary), values);

		public static IRequest Stream(this IRequest @this, Stream content)
			=> @this.Content(new StreamContent(content));

		public static IRequest Stream(this IRequest @this, Stream content, int bufferSize)
			=> @this.Content(new StreamContent(content, bufferSize));

		public static IRequest Json(this IRequest @this, object content)
			=> @this.Json(JsonConvert.SerializeObject(content));

		public static IRequest Json(this IRequest @this, string content)
			=> @this.Content(new StringContent(content, Encoding.UTF8, "application/json"));

		private static IRequest Multipart(this IRequest @this, MultipartContent content, params HttpContent[] values)
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

		private static IRequest MultipartFormData(this IRequest @this, MultipartFormDataContent content, params HttpContent[] values)
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
