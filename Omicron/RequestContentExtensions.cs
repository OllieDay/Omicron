using System;
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
		/// <summary>
		/// Sets the content of the request to the specified content.
		/// </summary>
		/// <param name="content">The HTTP content.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Content(this IWith @this, HttpContent content)
			=> @this.Modify(request => request.Content = content);

		/// <summary>
		/// Sets the content of the request to the specified content.
		/// </summary>
		/// <param name="content">The HTTP content.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest ByteArray(this IWith @this, ByteArrayContent content)
			=> @this.Content(content);

		/// <summary>
		/// Sets the content of the request to the specified content.
		/// </summary>
		/// <param name="content">The HTTP content.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest ByteArray(this IWith @this, byte[] content)
			=> @this.ByteArray(new ByteArrayContent(content));

		/// <summary>
		/// Sets the content of the request to the specified content with offset and count.
		/// </summary>
		/// <param name="content">The HTTP content.</param>
		/// <param name="offset">The offset, in bytes, in the <paramref name="content"/> parameter.</param>
		/// <param name="count">The number of bytes in the <paramref name="content"/> starting from the <paramref name="offset"/> parameter.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest ByteArray(this IWith @this, byte[] content, int offset, int count)
			=> @this.ByteArray(new ByteArrayContent(content, offset, count));

		/// <summary>
		/// Sets the content of the request to the specified content.
		/// </summary>
		/// <param name="content">The HTTP content.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest FormUrlEncoded(this IWith @this, FormUrlEncodedContent content)
			=> @this.Content(content);

		/// <summary>
		/// Sets the content of the request to the specified content.
		/// </summary>
		/// <param name="nameValueCollection">The collection of name/value pairs.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest FormUrlEncoded(this IWith @this, IEnumerable<KeyValuePair<string, string>> nameValueCollection)
			=> @this.FormUrlEncoded(new FormUrlEncodedContent(nameValueCollection));

		/// <summary>
		/// Sets the content of the request to the specified content.
		/// </summary>
		/// <param name="content">The HTTP content.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest String(this IWith @this, StringContent content)
			=> @this.Content(content);

		/// <summary>
		/// Sets the content of the request to the specified content.
		/// </summary>
		/// <param name="content">The HTTP content.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest String(this IWith @this, string content)
			=> @this.String(new StringContent(content));

		/// <summary>
		/// Sets the content of the request to the specified content with encoding.
		/// </summary>
		/// <param name="content">The HTTP content.</param>
		/// <param name="encoding">The encoding to use for the content.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest String(this IWith @this, string content, Encoding encoding)
			=> @this.String(new StringContent(content, encoding));

		/// <summary>
		/// Sets the content of the request to the specified content with encoding and media type.
		/// </summary>
		/// <param name="content">The HTTP content.</param>
		/// <param name="encoding">The encoding to use for the content.</param>
		/// <param name="mediaType">The media type to use for the content.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest String(this IWith @this, string content, Encoding encoding, string mediaType)
			=> @this.String(new StringContent(content, encoding, mediaType));

		/// <summary>
		/// Sets the content of the request to the specified content with values.
		/// </summary>
		/// <param name="content">The HTTP content.</param>
		/// <param name="values">TThe values for the multipart content.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		private static IRequest Multipart(this IWith @this, MultipartContent content, params HttpContent[] values)
			=> @this.Modify(SetMultipartContentWithValues(content, values));

		/// <summary>
		/// Sets the content of the request to the specified values.
		/// </summary>
		/// <param name="values">The values for the multipart content.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Multipart(this IWith @this, params HttpContent[] values)
			=> @this.Multipart(new MultipartContent(), values);

		/// <summary>
		/// Sets the content of the request to the specified values with subtype.
		/// </summary>
		/// <param name="subtype">The subtype of the multipart content.</param>
		/// <param name="values">The values for the multipart content.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Multipart(this IWith @this, string subtype, params HttpContent[] values)
			=> @this.Multipart(new MultipartContent(subtype), values);

		/// <summary>
		/// Sets the content of the request to the specified values with subtype and boundary.
		/// </summary>
		/// <param name="subtype">The subtype of the multipart content.</param>
		/// <param name="boundary">The boundary string for the multipart content.</param>
		/// <param name="values">The values for the multipart content.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Multipart(this IWith @this, string subtype, string boundary, params HttpContent[] values)
			=> @this.Multipart(new MultipartContent(subtype, boundary), values);

		/// <summary>
		/// Sets the content of the request to the specified content with values.
		/// </summary>
		/// <param name="content">The HTTP content.</param>
		/// <param name="values">The values for the multipart form data content.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		private static IRequest MultipartFormData(this IWith @this, MultipartFormDataContent content, params HttpContent[] values)
			=> @this.Modify(SetMultipartFormDataContentWithValues(content, values));

		/// <summary>
		/// Sets the content of the request to the specified values.
		/// </summary>
		/// <param name="values">The values for the multipart form data content.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest MultipartFormData(this IWith @this, params HttpContent[] values)
			=> @this.MultipartFormData(new MultipartFormDataContent(), values);

		/// <summary>
		/// Sets the content of the request to the specified values with boundary.
		/// </summary>
		/// <param name="boundary">The boundary string for the multipart form data content.</param>
		/// <param name="values">The values for the multipart form data content.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest MultipartFormData(this IWith @this, string boundary, params HttpContent[] values)
			=> @this.MultipartFormData(new MultipartFormDataContent(boundary), values);

		/// <summary>
		/// Sets the content of the request to the specified content.
		/// </summary>
		/// <param name="content">The HTTP content.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Stream(this IWith @this, Stream content)
			=> @this.Content(new StreamContent(content));

		/// <summary>
		/// Sets the content of the request to the specified content with buffer size.
		/// </summary>
		/// <param name="content">The HTTP content.</param>
		/// <param name="bufferSize">The size, in bytes, of the buffer for the <paramref name="content"/>.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Stream(this IWith @this, Stream content, int bufferSize)
			=> @this.Content(new StreamContent(content, bufferSize));

		/// <summary>
		/// Sets the content of the request to the specified content.
		/// </summary>
		/// <param name="content">The HTTP content.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Json(this IWith @this, object content)
			=> @this.Json(JsonConvert.SerializeObject(content));

		/// <summary>
		/// Sets the content of the request to the specified content.
		/// </summary>
		/// <param name="content">The HTTP content.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Json(this IWith @this, string content)
			=> @this.Content(new StringContent(content, Encoding.UTF8, "application/json"));

		/// <summary>
		/// Sets the content of the request to the specified content.
		/// </summary>
		/// <param name="content">The HTTP content.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Xml(this IWith @this, XDocument content)
			=> @this.Xml(content.ToString());

		/// <summary>
		/// Sets the content of the request to the specified content.
		/// </summary>
		/// <param name="content">The HTTP content.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Xml(this IWith @this, string content)
			=> @this.Content(new StringContent(content, Encoding.UTF8, "application/xml"));

		private static Action<HttpRequestMessage> SetMultipartContentWithValues(MultipartContent content, IEnumerable<HttpContent> values)
		{
			return request =>
			{
				foreach (var value in values)
				{
					content.Add(value);
				}

				request.Content = content;
			};
		}

		private static Action<HttpRequestMessage> SetMultipartFormDataContentWithValues(MultipartFormDataContent content, IEnumerable<HttpContent> values)
		{
			return request =>
			{
				foreach (var value in values)
				{
					content.Add(value);
				}

				request.Content = content;
			};
		}
	}
}
