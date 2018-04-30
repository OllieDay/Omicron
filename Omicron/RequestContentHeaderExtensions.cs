using System.Net.Http.Headers;

namespace Omicron
{
	public static class RequestContentHeaderExtensions
	{
		public static IRequest ContentLength(this IWith @this, long value)
			=> @this.Modify(request => request.Content.Headers.ContentLength = value);

		public static IRequest ContentMD5(this IWith @this, byte[] value)
			=> @this.Modify(request => request.Content.Headers.ContentMD5 = value);

		public static IRequest ContentType(this IWith @this, MediaTypeHeaderValue value)
			=> @this.Modify(request => request.Content.Headers.ContentType = value);

		public static IRequest ContentType(this IWith @this, string mediaType)
			=> @this.ContentType(new MediaTypeHeaderValue(mediaType));
	}
}
