using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;

namespace KudoCode.Web.Blazor.Application
{
	public class CsrfTokenCookieMiddleware
	{
		private readonly RequestDelegate _next;
		private static string _tokenId = Guid.NewGuid().ToString();
		public CsrfTokenCookieMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			if (context.Request.Cookies[$"{_tokenId}-TOKEN"] == null)
			{
				context.Response.Cookies.Append($"{_tokenId}-TOKEN", Guid.NewGuid().ToString(),
					new CookieOptions { HttpOnly = false, Expires = DateTime.Now.AddMinutes(20) });
			}

			await _next(context);
		}
	}
}