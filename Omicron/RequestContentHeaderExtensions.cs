using System.Net.Http.Headers;

namespace Omicron
{
	public static class RequestContentHeaderExtensions
	{
		/// <summary>
		/// Sets the Content-Length header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Content-Length header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest ContentLength(this IWith @this, long value)
			=> @this.Modify(request => request.Content.Headers.ContentLength = value);

		/// <summary>
		/// Sets the Content-MD5 header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Content-MD5 header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest ContentMD5(this IWith @this, byte[] value)
			=> @this.Modify(request => request.Content.Headers.ContentMD5 = value);

		/// <summary>
		/// Sets the Content-Type header of the request to the specified value.
		/// </summary>
		/// <param name="value">The Content-Type header value.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest ContentType(this IWith @this, MediaTypeHeaderValue value)
			=> @this.Modify(request => request.Content.Headers.ContentType = value);

		/// <summary>
		/// Sets the Content-Type header of the request to the specified media type.
		/// </summary>
		/// <param name="mediaType">The Content-Type header media type.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest ContentType(this IWith @this, string mediaType)
			=> @this.ContentType(new MediaTypeHeaderValue(mediaType));
	}
}
